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
    void BuildTargets(IHostBuilder hostBuilder);
    //Task BuildTasksAsync(IQuarkExecutionContext context, IQuarkTargetExpander expander, CancellationToken token);
    //Action<IQuarkTargetManager> Manager { get; }
    ExecutingRunnerAsync Manager { get; }
    List<IQuarkTask> QuarkTasks { get; }
    List<IQuarkCredential> Credentials { get; }
    List<ExecutingRunnerAsync> ManageActions { get; }
}
