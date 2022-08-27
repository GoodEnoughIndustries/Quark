using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quark.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Quark;

public class QuarkExecutionContext : IQuarkExecutionContext
{
    private readonly ILogger<QuarkExecutionContext> logger;

    public QuarkExecutionContext(ILogger<QuarkExecutionContext> logger)
    {
        this.logger = logger;
    }

    public IQuarkConfiguration? CurrentConfiguration { get; private set; }
    public List<IQuarkTarget> Targets { get; } = new();
    public List<IQuarkResult> Results { get; } = new();
    public List<IQuarkTask> Tasks { get; } = new();

    public Task BuildAllAsync(QuarkContext context, CancellationToken token)
    {
        this.CurrentConfiguration ??= context.QuarkConfigurations.First();

        foreach (var configuration in context.QuarkConfigurations)
        {
            // Copy all global tasks to each target group.
            foreach (var targetGroup in configuration.TargetGroups)
            {
                targetGroup.QuarkTasks.AddRange(configuration.QuarkTasks);
                targetGroup.ManageActions.AddRange(configuration.GlobalManageActions);
                targetGroup.ManageActions.Add(targetGroup.Manager);
            }

            // Now we have each TargetGroup expand and do any prerun task stuff
            var targetExpander = new QuarkTargetExpander();
            foreach (var targetGroup in configuration.TargetGroups)
            {
                //await targetGroup.BuildTasksAsync(this, targetExpander, token);
                context.CredentialProvider.AddCredentials(targetGroup.Credentials);
            }

            // Now we load everything into this context.
            // At this point, all Targets should be expanded and all
            // appropriate task in each target
            foreach (var targetGroup in configuration.TargetGroups)
            {
                targetGroup.Targets.ForEach(tg => tg.ManageActions.AddRange(configuration.GlobalManageActions));
                targetGroup.Targets.ForEach(tg => tg.ManageActions.AddRange(targetGroup.ManageActions));

                this.Tasks.AddRange(targetGroup.QuarkTasks);
                this.Targets.AddRange(targetGroup.Targets);
            }
        }

        return Task.CompletedTask;
    }

    public async Task ExecuteTasksAsync(QuarkContext context, CancellationToken token)
    {
        foreach (var target in this.Targets)
        {
            if (target.Status.HasFlag(TargetStatus.Faulted))
            {
                continue;
            }

            foreach (var manageAction in target.ManageActions)
            {
                var tm = ActivatorUtilities.CreateInstance<QuarkTargetRunner>(context.ServiceProvider, context, target);
                await manageAction(context, tm, target);
            }
        }
    }
}
