using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Quark.Abstractions;

public interface IQuarkTargetGroup
{
    IQuarkTargetGroup WithTags(params object[] tags);
    IQuarkTargetGroup WithTag(object tag);
    QuarkTargetTypes Type { get; }
    string Pattern { get; }
    List<IQuarkTarget> Targets { get; set; }
    void BuildTasks(IHostBuilder hostBuilder);
    Task BuildTasksAsync(IQuarkExecutionContext context, IQuarkTargetExpander expander, CancellationToken token);
    Action<IQuarkTargetBuilder> Builder { get; }
    List<IQuarkTask> QuarkTasks { get; }
    List<IQuarkCredential> Credentials { get; }
    List<Func<IHostBuilder, IQuarkTask>> BuildActions { get; }
}
