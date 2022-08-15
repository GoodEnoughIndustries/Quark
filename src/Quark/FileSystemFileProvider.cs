using Microsoft.Extensions.Logging;
using Quark.Abstractions;
using System.IO;
using System.Threading.Tasks;

namespace Quark;

public class FileSystemFileProvider : IQuarkFileProvider
{
    private readonly ILogger<FileSystemFileProvider> logger;

    public FileSystemFileProvider(
        ILogger<FileSystemFileProvider> logger)
    {
        this.logger = logger;
    }

    public bool FileExists(string path)
    {
        // operator gave absolute path...
        if (File.Exists(path))
        {
            return true;
        }

        return false;
    }

    public Task<FileInfo?> GetFileAsync(string path)
    {
        var fi = new FileInfo(path);

        if (!fi.Exists)
        {
            return Task.FromResult((FileInfo?)null);
        }

        return Task.FromResult((FileInfo?)fi);
    }

    public Task<bool> DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }
}
