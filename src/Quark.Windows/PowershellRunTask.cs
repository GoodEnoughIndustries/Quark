using Quark.Abstractions;

namespace Quark.Windows
{
    internal class PowershellRunTask : IQuarkTask
    {
        private readonly string path;

        public PowershellRunTask(string path)
            => this.path = path;

        public List<IQuarkTarget> Targets { get; init; } = new();

        public async Task<IQuarkResult> ExecuteAsync(QuarkContext context, IQuarkTarget target)
        {
            return QuarkResult.GetResult(RunResult.NotImplemented, target, this);
        }
    }
}
