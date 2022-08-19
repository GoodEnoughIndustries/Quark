using Microsoft.Extensions.Hosting;
using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quark;

public class QuarkTargetRunner : IQuarkTargetManager
{
    public QuarkTargetRunner()
    {
    }

    public QuarkTargetRunner(QuarkContext context, IQuarkTarget target)
    {
        this.Context = context;
        this.Target = target;
    }

    public QuarkContext? Context { get; }
    public IQuarkTarget? Target { get; }

    public List<IQuarkTask> QuarkTasks { get; } = new();
    public List<IQuarkTargetGroup> TargetGroups { get; } = new();
    public List<IQuarkCredential> Credentials { get; } = new();

    public List<ExecutingRunnerAsync> DeferredActions { get; } = new();

    public Task<IQuarkTargetManager> RunQuarkTask(ExecutingRunnerAsync taskRun)
    {
        Task? task = null;
        if (this.Context is not null && this.Target is not null)
        {
            task = taskRun(this.Context, this, this.Target);
        }
        else
        {
            this.DeferredActions.Add(taskRun);
        }

        task?.Wait();

        return Task.FromResult<IQuarkTargetManager>(this);
    }
}
