using Microsoft.Extensions.Logging;
using Quark.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Quark;

public class QuarkFileSystem : IQuarkFileSystem
{
    private readonly ILogger<QuarkFileSystem> logger;
    private string tempPath;

    public QuarkFileSystem(
        ILogger<QuarkFileSystem> logger,
        IEnumerable<IQuarkFileProvider> fileProviders)
    {
        this.logger = logger;
        this.FileProviders = fileProviders;

        this.tempPath = Path.GetTempPath();
        Directory.CreateDirectory(tempPath);
    }

    public IEnumerable<IQuarkFileProvider> FileProviders { get; init; }

    public Task<FileInfo?> GetFileAsync(string path)
    {
        var fp = this.FileProviders.FirstOrDefault(fp => fp.FileExists(path));

        if (fp is not null)
        {
            return fp.GetFileAsync(path);
        }

        return Task.FromResult((FileInfo?)null);
    }

    public string GetTemporaryDirectory()
        => this.tempPath;
}
