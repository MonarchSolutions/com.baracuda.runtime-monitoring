using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Baracuda.Monitoring.Interface;
using Baracuda.Monitoring.Internal.Pooling.Concretions;
using Baracuda.Monitoring.Internal.Profiling;
using Baracuda.Monitoring.Internal.Reflection;
using Baracuda.Monitoring.Internal.Units;
using Baracuda.Threading;

namespace Baracuda.Monitoring.API
{
    /// <summary>
    /// Class manages monitoring and offers public API
    /// </summary>
    public static class MonitoringManager
    {
        #region --- API ---

        /// <summary>
        /// Value indicated whether or not monitoring profiling has completed and monitoring is fully initialized.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public static bool IsInitialized
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => isInitialized;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private set
            {
                Dispatcher.GuardAgainstIsNotMainThread("set" + nameof(IsInitialized));
                isInitialized = value;
            }
        }
        
        /// <summary>
        /// Event is invoked when profiling process for the current system has been completed.
        /// This might even be before an Awake call which is why subscribing to this event will instantly invoke
        /// a callback when subscribing after profiling was already completed.
        /// </summary>
        public static event ProfilingCompletedListener ProfilingCompleted
        {
            add
            {
                if (IsInitialized)
                {
                    value.Invoke(staticUnits, instanceUnits);
                    return;
                }
                profilingCompleted += value;
            }
            remove => profilingCompleted -= value;
        }
        
        /// <summary>
        /// Event is called when a new <see cref="MonitorUnit"/> was created.
        /// </summary>
        public static event Action<IMonitorUnit> UnitCreated;
        
        /// <summary>
        /// Event is called when a <see cref="MonitorUnit"/> was disposed.
        /// </summary>
        public static event Action<IMonitorUnit> UnitDisposed;
        
        public delegate void ProfilingCompletedListener(IReadOnlyList<IMonitorUnit> staticUnits, IReadOnlyList<IMonitorUnit> instanceUnits);


        /*
         * Target Object Registration   
         */
        
        /// <summary>
        /// Register an object that is monitored during runtime.
        /// </summary>
        public static void RegisterTarget(object target)
        {
            RegisterTargetInternal(target);
        }
        
        /// <summary>
        /// Unregister an object that is monitored during runtime.
        /// </summary>
        public static void UnregisterTarget(object target)
        {
            UnregisterTargetInternal(target);
        }

        /*
         * Getter   
         */        
        
        /// <summary>
        /// Get a list of monitoring units for static targets.
        /// </summary>
        public static IReadOnlyList<MonitorUnit> GetStaticUnits() => staticUnits;
        
        /// <summary>
        /// Get a list of monitoring units for instance targets.
        /// </summary>
        public static IReadOnlyList<MonitorUnit> GetInstanceUnits() => instanceUnits;
        
        #endregion

        //--------------------------------------------------------------------------------------------------------------
        
        #region --- Private Fields ---

        private static readonly List<MonitorUnit> staticUnits = new List<MonitorUnit>(30);
        private static readonly List<MonitorUnit> instanceUnits = new List<MonitorUnit>(30);
        
        private static readonly Dictionary<object, MonitorUnit[]> activeInstanceUnits = new Dictionary<object, MonitorUnit[]>();
        
        private static readonly List<object> registeredTargets = new List<object>(300);
        private static bool initialInstanceUnitsCreated = false;
        
        private static volatile bool isInitialized = false;
        private static ProfilingCompletedListener profilingCompleted;
        
        #endregion
        
        //--------------------------------------------------------------------------------------------------------------

        #region --- Raise Events ---

        internal static void RaiseUnitCreated(MonitorUnit monitorUnit)
        {
            if (!Dispatcher.IsMainThread())
            {
                UnitCreated.Dispatch(monitorUnit);
                return;
            }
            UnitCreated?.Invoke(monitorUnit);
        }
        
        internal static void RaiseUnitDisposed(MonitorUnit monitorUnit)
        {
            if (!Dispatcher.IsMainThread())
            {
                UnitDisposed.Dispatch(monitorUnit);
                return;
            }
            UnitDisposed?.Invoke(monitorUnit);
        }
        
        internal static void ProfilingCompletedInternal(MonitorUnit[] staticUnits, MonitorUnit[] instanceUnits)
        {
            IsInitialized = true;
            profilingCompleted?.Invoke(staticUnits, instanceUnits);
            profilingCompleted = null;
        }
        
        #endregion
        
        //--------------------------------------------------------------------------------------------------------------
        
        #region --- Internal Target Registration ---

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RegisterTargetInternal(object target)
        {
            registeredTargets.Add(target);
            if (initialInstanceUnitsCreated)
            {
                CreateInstanceUnits(target, target.GetType());
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnregisterTargetInternal(object target)
        {
            DestroyInstanceUnits(target);
            registeredTargets.Remove(target);
        }

        #endregion
        
        #region --- Internal Complete Profiling ---

        internal static async Task CompleteProfilingAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            CreateStaticUnits(MonitoringProfiler.StaticProfiles.ToArray());
            await Dispatcher.InvokeAsync(CreateInitialInstanceUnits, ct);
            await Dispatcher.InvokeAsync(() => ProfilingCompletedInternal(staticUnits.ToArray(), instanceUnits.ToArray()), ct);
        }
        
        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region --- Instantiate: Instance Units ---
        
        private static void CreateInitialInstanceUnits()
        {
            for (var i = 0; i < registeredTargets.Count; i++)
            {
                CreateInstanceUnits(registeredTargets[i], registeredTargets[i].GetType());
            }
            initialInstanceUnitsCreated = true;
        }

        private static void CreateInstanceUnits(object target, Type type)
        {
            var validTypes = type.GetBaseTypes(true);
            // create a new array to cache the units instances that will be created. 
            var units = ConcurrentListPool<MonitorUnit>.Get();
            var guids = ConcurrentListPool<MemberInfo>.Get();
            
            for (var i = 0; i < validTypes.Length; i++)
            {
                if(validTypes[i].IsGenericType)
                {
                    continue;
                }

                if (!MonitoringProfiler.InstanceProfiles.TryGetValue(validTypes[i], out var profiles))
                {
                    continue;
                }

                // loop through the profiles and create a new unit for each profile.
                for (var j = 0; j < profiles.Count; j++)
                {
                    if(guids.Contains(profiles[j].MemberInfo))
                    {
                        continue;
                    }

                    guids.Add(profiles[j].MemberInfo);
                        
                    var unit = profiles[j].CreateUnit(target);
                    units.Add(unit);
                    instanceUnits.Add(unit);
                    RaiseUnitCreated(unit);
                }
            }

            // cache the created units in a dictionary that allows access by the units target.
            // this dictionary will be used to dispose the units if the target gets destroyed 
            if (units.Count > 0 && !activeInstanceUnits.ContainsKey(target))
            {
                activeInstanceUnits.Add(target, units.ToArray());
            }
            ConcurrentListPool<MemberInfo>.Release(guids);
            ConcurrentListPool<MonitorUnit>.Release(units);
        }

        #endregion

        #region --- Dispose: Instance Units ---

        private static void DestroyInstanceUnits(object target)
        {
            if (!activeInstanceUnits.TryGetValue(target, out var units))
            {
                return;
            }

            for (var i = 0; i < units.Length; i++)
            {
                units[i].Dispose();
                RaiseUnitDisposed(units[i]);
            }
                
            activeInstanceUnits.Remove(target);
        }

        #endregion
        
        #region --- Instantiate: Static Units ---
        
        private static void CreateStaticUnits(MonitorProfile[] staticProfiles)
        {
            for (var i = 0; i < staticProfiles.Length; i++)
            {
                CreateStaticUnit(staticProfiles[i]);
            }
        }
        
        private static void CreateStaticUnit(MonitorProfile staticProfile)
        {
            staticUnits.Add(staticProfile.CreateUnit(null));
        }

        #endregion
    }
}