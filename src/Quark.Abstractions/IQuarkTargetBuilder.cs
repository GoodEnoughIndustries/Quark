using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace Quark.Abstractions;

public interface IQuarkTargetBuilder
{
    List<IQuarkCredential> Credentials { get; }
    List<IQuarkTask> QuarkTasks { get; }
    List<IQuarkTargetGroup> TargetGroups { get; }
    List<Func<IHostBuilder, IQuarkTask>> BuildActions { get; }

    IQuarkTargetBuilder AddQuarkTask(Func<IHostBuilder, IQuarkTask> taskRun);
    IQuarkTargetBuilder AddQuarkTask2(IQuarkTask task);
}
