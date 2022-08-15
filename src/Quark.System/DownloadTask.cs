using Quark.Abstractions;
using System.Collections.Generic;
using System.IO;
using Flurl.Http;
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

        public async Task<IQuarkResult> ExecuteAsync(QuarkContext context, IQuarkTarget target)
        {
            var fs = context.FileSystem;

            var finalPath = Path.Combine(fs.GetTemporaryDirectory(), this.destination);

            await fs.TryDeleteFile(finalPath);

            var result = await this.url.DownloadFileAsync(fs.GetTemporaryDirectory(), this.destination);

            if (File.Exists(result))
            {
                return QuarkResult.GetResult(RunResult.Success, target, this);
            }

            return QuarkResult.GetResult(RunResult.Fail, target, this);
        }
    }
}
