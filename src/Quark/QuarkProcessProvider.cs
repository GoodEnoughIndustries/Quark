using Microsoft.Extensions.Logging;
using Quark.Abstractions;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Quark;

public class QuarkProcessProvider : IQuarkProcessProvider
{
    private readonly ILogger<QuarkProcessProvider> logger;
    private readonly IQuarkExecutionContext context;
    private readonly IQuarkFileSystem fileSystem;
    private readonly IQuarkCredentialProvider credentials;

    public QuarkProcessProvider(
        ILogger<QuarkProcessProvider> logger,
        IQuarkExecutionContext context,
        IQuarkCredentialProvider credentialProvider,
        IQuarkFileSystem fileSystem)
    {
        this.logger = logger;
        this.context = context;
        this.fileSystem = fileSystem;
        this.credentials = credentialProvider;
    }

    public async Task<Process?> Start(ProcessStartInfo psi)
    {
        ArgumentNullException.ThrowIfNull(psi);

        var file = await this.fileSystem.GetFileAsync(psi.FileName);
        if (file is null)
        {
            throw new FileNotFoundException();
        }

        psi.FileName = file.FullName;

        // TODO: Hook up logging and all sorts of goodies.
        try
        {
            return Process.Start(psi);
        }
        catch (Win32Exception e) when (e.NativeErrorCode == 2)
        {
            this.logger.LogError("File not found in {ProcessStartInfo}", psi);

            return null;
        }
    }
}
