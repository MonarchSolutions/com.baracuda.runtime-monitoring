// Copyright (c) 2022 Jonathan Lang

using System.Runtime.CompilerServices;

namespace Baracuda.Monitoring
{
    /// <summary>
    /// Contains monitoring extensions
    /// </summary>
    public static class MonitoringExtensions
    {
        /// <summary>
        /// Register an object that is monitored during runtime.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RegisterMonitor<T>(this T target) where T : class
        {
            if (MonitoringSystems.Initialized)
            {
                MonitoringSystems.Manager.RegisterTarget(target);
            }
            else
            {
                MonitoringSystems.__RegisterTarget(target);
            }
        }

        /// <summary>
        /// Unregister an object that is monitored during runtime.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void UnregisterMonitor<T>(this T target) where T : class
        {
            if (MonitoringSystems.Initialized)
            {
                MonitoringSystems.Manager.UnregisterTarget(target);
            }
            else
            {
                MonitoringSystems.__UnregisterTarget(target);
            }
        }

        /// <summary>
        /// Register an object that is monitored during runtime.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BeginMonitor<T>(this T target) where T : class
        {
            MonitoringSystems.Manager.RegisterTarget(target);
        }

        /// <summary>
        /// Unregister an object that is monitored during runtime.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EndMonitor<T>(this T target) where T : class
        {
            MonitoringSystems.Manager.UnregisterTarget(target);
        }
    }
}