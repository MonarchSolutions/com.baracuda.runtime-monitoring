﻿using System.Threading;
using Baracuda.Monitoring.API;

namespace Baracuda.Monitoring.Source.Interfaces
{
    internal interface IMonitoringProfiler : IMonitoringSystem<IMonitoringProfiler>
    {
        void BeginProfiling(CancellationToken ct);
    }
}

