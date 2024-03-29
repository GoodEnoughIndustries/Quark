using System;
using System.Collections.Generic;
using System.IO;

namespace Quark.Abstractions;

public interface IQuarkConfiguration
{
    List<ExecutingRunnerAsync> GlobalManageActions { get; }
    List<IQuarkTargetGroup> TargetGroups { get; }
    List<DirectoryInfo> FileLocations { get; }
    List<IQuarkTask> QuarkTasks { get; }
    IQuarkConfiguration Build();
}
