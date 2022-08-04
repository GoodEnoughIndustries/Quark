using Quark.Abstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Quark;

public class QuarkExecutionContext : IQuarkExecutionContext
{
    public QuarkExecutionContext(IQuarkConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    public QuarkContext? QuarkContext { get; set; }
    public IQuarkConfiguration Configuration { get; }
    public List<IQuarkTarget> Targets { get; } = new();
    public List<IQuarkResult> Results { get; } = new();
    public List<IQuarkTask> Tasks { get; } = new();

    public async Task BuildAllAsync(QuarkContext context, CancellationToken token)
    {
        // Copy all global tasks to each target group.
        foreach (var targetGroup in this.Configuration.TargetGroups)
        {
            targetGroup.QuarkTasks.AddRange(this.Configuration.QuarkTasks);
        }

        // Now we have each TargetGroup expand and do any prerun task stuff
        var targetExpander = new QuarkTargetExpander();
        foreach (var targetGroup in this.Configuration.TargetGroups)
        {
            await targetGroup.BuildTasksAsync(this, targetExpander, token);
            context.CredentialProvider.AddCredentials(targetGroup.Credentials);
        }

        // Now we load everything into this context.
        // At this point, all Targets should be expanded and all
        // appropriate task in each target
        foreach (var targetGroup in this.Configuration.TargetGroups)
        {
            this.Tasks.AddRange(targetGroup.QuarkTasks);
            this.Targets.AddRange(targetGroup.Targets);
        }
    }

    public async Task ExecuteTasksAsync(QuarkContext context, CancellationToken token)
    {
        foreach (var target in this.Targets)
        {
            if (target.Status.HasFlag(TargetStatus.Faulted))
            {
                continue;
            }

            foreach (var task in target.Tasks)
            {
                var result = await task.ExecuteAsync(context, target);
                this.Results.Add(result);

                if (result.Result is RunResult.Fail)
                {
                    target.Status = TargetStatus.Faulted;
                    break;
                }

            }
        }
    }
}
