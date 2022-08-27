using Microsoft.Extensions.Logging;
using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quark;

public class QuarkFilesFileProvider : IQuarkFileProvider
{
    private readonly ILogger<QuarkFilesFileProvider> logger;
    private readonly IQuarkExecutionContext context;

    public QuarkFilesFileProvider(ILogger<QuarkFilesFileProvider> logger,
        IQuarkExecutionContext context)
    {
        this.logger = logger;
        this.context = context;
    }

    public void DeleteFile(string path) => throw new NotImplementedException();

    public bool FileExists(string path)
    {
        // Go through all the Directories Operator provided that should
        // hold files for the run.

        foreach (var dir in this.context.CurrentConfiguration?.FileLocations ?? Enumerable.Empty<DirectoryInfo>())
        {
            var potentialPath = Path.Combine(dir.FullName, path);

            if (File.Exists(potentialPath))
            {
                return true;
            }
        }

        return false;
    }

    public Task<FileInfo?> GetFileAsync(string path)
    {
        FileInfo? fileInfo = null;
        // Go through all the Directories Operator provided that should
        // hold files for the run.

        foreach (var dir in this.context.CurrentConfiguration?.FileLocations ?? Enumerable.Empty<DirectoryInfo>())
        {
            var potentialPath = Path.Combine(dir.FullName, path);

            if (File.Exists(potentialPath))
            {
                fileInfo = new FileInfo(potentialPath);
            }
        }

        return Task.FromResult(fileInfo);
    }

    Task<bool> IQuarkFileProvider.DeleteFile(string path) => throw new NotImplementedException();
}
