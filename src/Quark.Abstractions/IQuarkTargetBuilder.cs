using System.Collections.Generic;

namespace Quark.Abstractions
{
    public interface IQuarkTargetBuilder
    {
        List<IQuarkTask> QuarkTasks { get; }
        List<IQuarkTargetGroup> TargetGroups { get; }
        IQuarkTargetBuilder AddQuarkTask(IQuarkTask task);
    }
}
