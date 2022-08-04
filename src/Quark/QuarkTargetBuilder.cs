using Microsoft.Extensions.Hosting;
using Quark.Abstractions;
using System;
using System.Collections.Generic;

namespace Quark;

public class QuarkTargetBuilder : IQuarkTargetBuilder
{
    public List<IQuarkTask> QuarkTasks { get; } = new();
    public List<IQuarkTargetGroup> TargetGroups { get; } = new();
    public List<IQuarkCredential> Credentials { get; } = new();

    public List<Func<IHostBuilder, IQuarkTask>> BuildActions { get; } = new();

    public IQuarkTargetBuilder AddQuarkTask(Func<IHostBuilder, IQuarkTask> taskRun)
    {
        this.BuildActions.Add(taskRun);

        return this;
    }

    public IQuarkTargetBuilder AddQuarkTask2(IQuarkTask task)
    {
        ArgumentNullException.ThrowIfNull(task);

        this.QuarkTasks.Add(task);

        return this;
    }
}
