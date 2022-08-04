using System.Collections.Generic;
using System.IO;

namespace Quark.Abstractions;

public interface IQuarkConfiguration
{
    List<IQuarkTargetGroup> TargetGroups { get; }
    List<DirectoryInfo> FileLocations { get; }
    List<IQuarkTask> QuarkTasks { get; }
    IQuarkConfiguration Build();
}
