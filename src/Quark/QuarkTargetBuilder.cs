using Microsoft.Extensions.Logging;
using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quark;

public class QuarkTargetRunner : IQuarkTargetManager
{
    private readonly ILogger<QuarkTargetRunner>? logger;

    public QuarkTargetRunner()
    {
    }

    public QuarkTargetRunner(ILogger<QuarkTargetRunner> logger, QuarkContext context, IQuarkTarget target)
    {
        this.Context = context;
        this.Target = target;
        this.logger = logger;
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
        try
        {
            if (this.Context is not null && this.Target is not null)
            {
                task = taskRun(this.Context, this, this.Target);
            }
            else
            {
                this.DeferredActions.Add(taskRun);
            }

            task?.Wait();

            if (task is Task<IQuarkResult> runResult)
            {
                var result = runResult.Result;

                var logLevel = result.QuarkRunResult switch
                {
                    QuarkRunResult.Unknown => LogLevel.Warning,
                    QuarkRunResult.Success => LogLevel.Information,
                    QuarkRunResult.Fail => LogLevel.Error,
                    QuarkRunResult.Skipped => LogLevel.Information,
                    QuarkRunResult.NotImplemented => LogLevel.Warning,
                    _ => LogLevel.Information,
                };

                this.logger?.Log(logLevel, "{Result}: {TaskName}", result.QuarkRunResult.ToString(), result.QuarkTask.TaskName);
                this.Context?.ExecutionContext.Results.Add(result);
            }
        }
        catch (Exception e)
        {
        }

        return Task.FromResult<IQuarkTargetManager>(this);
    }
}
