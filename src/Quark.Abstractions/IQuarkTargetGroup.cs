using System.Collections.Generic;

namespace Quark.Abstractions
{
    public interface IQuarkTargetGroup
    {
        public IQuarkTargetGroup WithTag(string tag);
        public string Name { get; }
        public IEnumerable<IQuarkTarget> Targets { get; }
    }
}
