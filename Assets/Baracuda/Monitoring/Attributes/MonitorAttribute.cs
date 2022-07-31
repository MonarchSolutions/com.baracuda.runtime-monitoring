// Copyright (c) 2022 Jonathan Lang

using System;
using UnityEngine.Scripting;

namespace Baracuda.Monitoring
{
    /// <summary>
    /// Mark a Field, Property or Event to be monitored at runtime.
    /// When monitoring non static members, instances of the monitored class must be registered and unregistered.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Method)]
    [Preserve]
    public class MonitorAttribute : Attribute
    {
    }
}