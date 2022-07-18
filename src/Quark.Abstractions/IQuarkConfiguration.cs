using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Quark.Abstractions
{
    public interface IQuarkConfiguration
    {
        List<IQuarkTargetGroup> TargetGroups { get; }
        List<DirectoryInfo> FileLocations { get; }
        List<IQuarkTask> QuarkTasks { get; }
        QuarkResult Run(CancellationToken token = default);
    }
}
