// Copyright (c) 2022 Jonathan Lang

//---------- ----------------------------- ----------
//---------- !!! AUTOGENERATED CONTENT !!! ----------
//---------- ----------------------------- ----------

//Runtime Monitoring
//File generated: 2022-10-02 14:54:53Z
//Please dont change the contents of this file. Otherwise IL2CPP runtime may not work with runtime monitoring!
//Ensure that this file is located in Assembly-CSharp. Otherwise this file may not compile.
//If this file contains any errors please contact me and/or create an issue in the linked repository.
//https://github.com/JohnBaracuda/Runtime-Monitoring

#if ENABLE_IL2CPP && !DISABLE_MONITORING

internal class IL2CPP_AOT
{
    //Value Processor Method Definitions

    [UnityEngine.Scripting.PreserveAttribute]
    [System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.NoOptimization)]
    private static void AOT()
    {
        // --- [Field Definitions] ---

        // Baracuda.Example.Scripts.PlayerMovement::_isJumping (System.Boolean)
        // Baracuda.Example.Scripts.PlayerMovement::_isFalling (System.Boolean)
        // Baracuda.Example.Scripts.PlayerMovement::_isDashing (System.Boolean)
        Baracuda.Monitoring.Core.IL2CPP.IL2CPPTypeDefinitions.TypeDefField<System.Object, System.Boolean>();

        // Baracuda.Example.Scripts.GameManager::_deathTimer (System.Int32)
        // Baracuda.Example.Scripts.PlayerMovement::_jumpsLeft (System.Int32)
        // Baracuda.Example.Scripts.PlayerWeapon::_currentAmmunition (System.Int32)
        Baracuda.Monitoring.Core.IL2CPP.IL2CPPTypeDefinitions.TypeDefField<System.Object, System.Int32>();

        // Baracuda.Monitoring.Modules.SystemMonitor::_operatingSystem (System.String)
        // Baracuda.Monitoring.Modules.SystemMonitor::_operatingSystemFamily (System.String)
        // Baracuda.Monitoring.Modules.SystemMonitor::_deviceType (System.String)
        // Baracuda.Monitoring.Modules.SystemMonitor::_deviceModel (System.String)
        // Baracuda.Monitoring.Modules.SystemMonitor::_deviceName (System.String)
        // Baracuda.Monitoring.Modules.SystemMonitor::_processorType (System.String)
        // Baracuda.Monitoring.Modules.SystemMonitor::_processorFrequency (System.String)
        // Baracuda.Monitoring.Modules.SystemMonitor::_processorCount (System.String)
        // Baracuda.Monitoring.Modules.SystemMonitor::_systemMemory (System.String)
        // Baracuda.Monitoring.Modules.SystemMonitor::_graphicsDeviceName (System.String)
        // Baracuda.Monitoring.Modules.SystemMonitor::_graphicsDeviceType (System.String)
        // Baracuda.Monitoring.Modules.SystemMonitor::_graphicsMemorySize (System.String)
        // Baracuda.Monitoring.Modules.SystemMonitor::_graphicsMultiThreaded (System.String)
        // Baracuda.Monitoring.Modules.SystemMonitor::_batteryLevel (System.String)
        // Baracuda.Monitoring.Modules.SystemMonitor::_batteryStatus (System.String)
        // Baracuda.Monitoring.Modules.SystemMonitor::_dataPath (System.String)
        // Baracuda.Monitoring.Modules.SystemMonitor::_persistentDataPath (System.String)
        // Baracuda.Monitoring.Modules.SystemMonitor::_consoleLogPath (System.String)
        // Baracuda.Monitoring.Modules.SystemMonitor::_streamingAssetsPath (System.String)
        // Baracuda.Monitoring.Modules.SystemMonitor::_temporaryCachePath (System.String)
        Baracuda.Monitoring.Core.IL2CPP.IL2CPPTypeDefinitions.TypeDefField<System.Object, System.Object>();

        // Baracuda.Example.Scripts.PlayerMovement::_dashEnergy (System.Single)
        // Baracuda.Example.Scripts.ShootingTarget::_currentHealth (System.Single)
        // Baracuda.Example.Scripts.ShootingTarget::_cooldown (System.Single)
        // Baracuda.Monitoring.Modules.FPSMonitor::_fps (System.Single)
        Baracuda.Monitoring.Core.IL2CPP.IL2CPPTypeDefinitions.TypeDefField<System.Object, System.Single>();

        // --- [Property Definitions] ---

        // Baracuda.Example.Scripts.GameManager::GameState (Baracuda.Example.Scripts.GameState)
        Baracuda.Monitoring.Core.IL2CPP.IL2CPPTypeDefinitions.TypeDefProperty<System.Object, Baracuda.Example.Scripts.GameState>();

        // Baracuda.Monitoring.Modules.FPSMonitor::TargetFrameRate (System.Int32)
        // Baracuda.Monitoring.Modules.FPSMonitor::Vsync (System.Int32)
        Baracuda.Monitoring.Core.IL2CPP.IL2CPPTypeDefinitions.TypeDefProperty<System.Object, System.Int32>();

        // Baracuda.Monitoring.Modules.ConsoleMonitor::Console (System.Collections.Generic.Queue<System.String>)
        // Baracuda.Monitoring.Modules.ConsoleMonitor::LastLogStacktrace (System.String)
        Baracuda.Monitoring.Core.IL2CPP.IL2CPPTypeDefinitions.TypeDefProperty<System.Object, System.Object>();

        // Baracuda.Example.Scripts.PlayerMovement::Position (UnityEngine.Vector3)
        Baracuda.Monitoring.Core.IL2CPP.IL2CPPTypeDefinitions.TypeDefProperty<System.Object, UnityEngine.Vector3>();

        // --- [Event Definitions] ---

        // Baracuda.Example.Scripts.GameManager::GameStateChanged (System.Action<Baracuda.Example.Scripts.GameState>)
        Baracuda.Monitoring.Core.IL2CPP.IL2CPPTypeDefinitions.TypeDefEvent<System.Object, System.Delegate>();

        // --- [Collection Definitions] ---

        // System.Collections.Generic.Queue<System.String>
        Baracuda.Monitoring.Core.IL2CPP.IL2CPPTypeDefinitions.TypeDefEnumerable<System.Object>();
    }
}

#endif //ENABLE_IL2CPP && !DISABLE_MONITORING

//----------------------------------------------------------------------------------------------------------------------

//--- Stats ---

//General

//Monitored Member:               37
//Monitored Member Instance:      37

//MemberInfo

//Monitored Events:                1
//Monitored Events Instance:       1
//Monitored Fields:               30
//Monitored Fields Instance:      30
//Monitored Properties:            6
//Monitored Properties Instance:   6

//Monitored Types

//Monitored Action<GameState>:     1
//Monitored bool:                  3
//Monitored float:                 4
//Monitored GameState:             1
//Monitored int:                   5
//Monitored Queue<string>:         1
//Monitored string:               21
//Monitored Vector3:               1

//----------------------------------------------------------------------------------------------------------------------

//If this file contains any errors please contact me and/or create an issue in the linked repository.
//https://github.com/JohnBaracuda/Runtime-Monitoring