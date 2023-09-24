using Quark.Abstractions;
using System.Collections.Generic;
using System.IO;
using Flurl.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Quark.Systems
{
    public class DownloadTask(string taskName, string url, string destination) : IQuarkTask
    {
        public List<IQuarkTarget> Targets { get; init; } = new();
        public string TaskName { get; init; } = taskName;

        public async Task<IQuarkResult> ExecuteAsync(QuarkContext context, IQuarkTargetManager manager, IQuarkTarget target)
        {
            var fs = context.FileSystem;

            var finalPath = Path.Combine(fs.GetTemporaryDirectory(), destination);

            await fs.TryDeleteFile(finalPath);

            var result = await url.DownloadFileAsync(fs.GetTemporaryDirectory(), destination);

            if (File.Exists(result))
            {
                return QuarkResult.GetResult(QuarkRunResult.Success, target, this);
            }

            return QuarkResult.GetResult(QuarkRunResult.Fail, target, this);
        }
    }
}
