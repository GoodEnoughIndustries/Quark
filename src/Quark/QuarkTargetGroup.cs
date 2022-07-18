using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quark
{
    public class QuarkTargetGroup : IQuarkTargetGroup
    {
        public QuarkTargetGroup(
            string pattern,
            QuarkTargetTypes targetType,
            Action<IQuarkTargetBuilder> targetBuilder,
            params object[] tags)
        {
            this.Pattern = pattern;
            this.Type = targetType;
            this.Tags = tags;
            this.Builder = targetBuilder;
        }

        public QuarkTargetTypes Type { get; }
        public string Pattern { get; }
        public List<IQuarkTarget> Targets { get; set; } = new();
        public IEnumerable<object> Tags { get; set; } = new List<object>();
        public Action<IQuarkTargetBuilder> Builder { get; set; }
        public List<IQuarkTask> QuarkTasks { get; set; } = new List<IQuarkTask>();

        public Task BuildTasksAsync(IQuarkTargetExpander expander)
        {
            ArgumentNullException.ThrowIfNull(expander);

            var targetBuilder = new QuarkTargetBuilder();
            this.Builder(targetBuilder);

            if (targetBuilder.TargetGroups.Any())
            {

            }

            this.QuarkTasks.AddRange(targetBuilder.QuarkTasks);

            if (expander.TryExpand(this, out var targets))
            {
                this.Targets.AddRange(targets);
            }
            else
            {
                this.Targets.Add(new QuarkTarget
                {
                    Name = this.Pattern,
                    Type = this.Type,
                });
            }

            foreach (var target in this.Targets)
            {
                target.QuarkTasks.AddRange(this.QuarkTasks);
            }

            List<Task> buildTasks = new();
            foreach (var target in this.Targets)
            {
                foreach (var task in target.QuarkTasks)
                {
                    buildTasks.Add(task.BuildAsync(default));
                }
            }

            return Task.WhenAll(buildTasks);
        }

        public IQuarkTargetGroup WithTag(object tag)
            => WithTags(new[] { tag });

        public IQuarkTargetGroup WithTags(params object[] tags)
        {
            return this;
        }
    }
}
