using Quark.Abstractions;
using System.Collections.Generic;
using System.IO;
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

        public Task<IQuarkResult> ExecuteAsync(QuarkContext context, IQuarkTarget target)
        {
            var dirName = Path.GetDirectoryName(this.destination);
            return Task.FromResult((IQuarkResult)QuarkResult.GetResult(RunResult.NotImplemented, target, this));
        }
    }
}
