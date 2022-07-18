using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quark
{
    public class QuarkExecutionContext : IQuarkExecutionContext
    {
        public QuarkExecutionContext(IQuarkConfiguration configuration)
            => this.Configuration = configuration;

        public IQuarkConfiguration Configuration { get; }
        public List<IQuarkTarget> Targets { get; } = new();

        public async Task BuildAllAsync(CancellationToken token)
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
                await targetGroup.BuildTasksAsync(targetExpander);
            }

            // Now we load everything into this context.
            // At this point, all Targets should be expanded and all
            // appropriate tasks in each target

        }

        public Task BuildResultsAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }

        public Task ExecuteTasksAsync(CancellationToken token)
        {
            // execute every task in order, across all machines.
            return Task.CompletedTask;
        }

        public QuarkResult GetFinalResult() => new();

        public Task ValidateAsync(CancellationToken token)
        {
            // validate things that need to be validated
            return Task.CompletedTask;
        }
    }
}
