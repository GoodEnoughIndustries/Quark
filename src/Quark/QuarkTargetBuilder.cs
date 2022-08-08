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

    public async Task<IQuarkTargetManager> RunQuarkTask(ExecutingRunnerAsync taskRun)
    {
        if (this.Context is not null && this.Target is not null)
        {
            await taskRun(this.Context, this, this.Target);
        }
        else
        {
            this.DeferredActions.Add(taskRun);
        }

        return this;
    }
}
