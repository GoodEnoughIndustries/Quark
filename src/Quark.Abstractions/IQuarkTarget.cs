using System.Collections.Generic;

namespace Quark.Abstractions
{
    public interface IQuarkTarget
    {
        public QuarkTargetTypes Type { get; init; }
        public string Name { get; init; }
        public List<IQuarkTask> QuarkTasks { get; init; }
    }
}
