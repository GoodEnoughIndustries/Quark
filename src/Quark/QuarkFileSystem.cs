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
        Directory.CreateDirectory(this.tempPath);
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

    public async Task<bool> TryDeleteFile(string path)
    {
        var fp = this.FileProviders.FirstOrDefault(fp => fp.FileExists(path));

        return fp is not null
        ? await fp.DeleteFile(path)
        : false;
    }

    public string GetTemporaryDirectory()
        => this.tempPath;
}
