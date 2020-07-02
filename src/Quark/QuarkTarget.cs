using Quark.Abstractions;

namespace Quark
{
    public class QuarkTarget : IQuarkTarget
    {
        public string Name { get; }

        public QuarkTarget(string th) => this.Name = th;
    }
}
