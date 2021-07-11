using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Quark.Abstractions
{
    public interface IQuarkConfiguration
    {
        List<string> TargetNames { get; }
        List<DirectoryInfo> FileLocations { get; }
        List<IQuarkTask> QuarkTasks { get; }

        QuarkResult Run(CancellationToken token = default);
    }
}
