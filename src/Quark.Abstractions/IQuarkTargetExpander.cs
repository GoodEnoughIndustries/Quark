using System.Collections.Generic;

namespace Quark.Abstractions
{
    public interface IQuarkTargetExpander
    {
        bool TryExpand(IQuarkTargetGroup targetGroup, out IEnumerable<IQuarkTarget> targets);
    }
}
