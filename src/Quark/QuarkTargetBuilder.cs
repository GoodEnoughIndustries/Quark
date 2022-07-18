using Quark.Abstractions;
using System;
using System.Collections.Generic;

namespace Quark
{
    public class QuarkTargetBuilder : IQuarkTargetBuilder
    {
        public List<IQuarkTask> QuarkTasks { get; } = new List<IQuarkTask>();
        public List<IQuarkTargetGroup> TargetGroups { get; } = new List<IQuarkTargetGroup>();

        public IQuarkTargetBuilder AddQuarkTask(IQuarkTask task)
        {
            ArgumentNullException.ThrowIfNull(task);

            this.QuarkTasks.Add(task);

            return this;
        }
    }
}
