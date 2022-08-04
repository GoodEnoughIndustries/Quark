using Quark.Abstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Quark.Systems
{
    public class DownloadTask : IQuarkTask
    {
        private readonly string url;
        private readonly string destination;

        public DownloadTask(string url, string destination)
        {
            this.url = url;
            this.destination = destination;
        }

        public List<IQuarkTarget> Targets { get; init; } = new();

        public Task BuildAsync(IQuarkExecutionContext context, CancellationToken token)
        {
            return Task.CompletedTask;
        }

        public Task<IQuarkResult> ExecuteAsync(QuarkContext context, IQuarkTarget target)
        {
            return Task.FromResult((IQuarkResult)QuarkResult.GetResult(RunResult.NotImplemented, target, this));
        }
    }
}
