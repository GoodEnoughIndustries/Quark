using Quark.Abstractions;

namespace Quark
{
    public class QuarkTarget : IQuarkTarget
    {
        public string Name { get; }
        public QuarkTargetTypes Type { get; set; }

        public QuarkTarget(string name) => this.Name = name;
    }
}
