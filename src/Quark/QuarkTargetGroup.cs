using Quark.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Quark
{
    public class QuarkTargetGroup : IQuarkTargetGroup
    {
        public QuarkTargetGroup(
            string pattern,
            QuarkTargetTypes targetType,
            params object[] tags)
        {

        }

        public QuarkTargetGroup()
        {
        }

        public IEnumerable<IQuarkTargetGroup> WithTag(string tag) => Enumerable.Empty<IQuarkTargetGroup>();

        IQuarkTargetGroup IQuarkTargetGroup.WithTag(string tag) => throw new System.NotImplementedException();

        public string Name { get; }
        public IEnumerable<IQuarkTarget> Targets { get; }
    }
}
