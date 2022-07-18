using Quark.Abstractions;
using System.Collections.Generic;

namespace Quark
{
    public class QuarkTarget : IQuarkTarget
    {
        public string Name { get; init; } = string.Empty;
        public QuarkTargetTypes Type { get; init; } = QuarkTargetTypes.Unknown;
        public List<IQuarkTask> QuarkTasks { get; init; } = new();

        public override string ToString() => $"{this.Type}: {this.Name}";
    }
}
