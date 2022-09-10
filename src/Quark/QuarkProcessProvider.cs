using CliWrap;
using CliWrap.EventStream;
using Microsoft.Extensions.Logging;
using Quark.Abstractions;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
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

    // TODO: This is almost exactly from Cupboard - will work it more into a Quark pattern later.
    // Never used CliWrap before, still getting a feel.
    public async Task<ProcessResult> Start(string path, string? arguments = null, Func<string, bool>? filter = null, bool supressOutput = false)
    {
        var cli = Cli.Wrap(path).WithValidation(CommandResultValidation.None);

        if (!string.IsNullOrWhiteSpace(arguments))
        {
            cli = cli.WithArguments(arguments);
        }

        var standardOut = new StringBuilder();
        var standardError = new StringBuilder();
        var exitCode = -1;

        await foreach (var cmdEvent in cli.ListenAsync())
        {
            switch (cmdEvent)
            {
                case StartedCommandEvent started:
                    break;
                case StandardOutputCommandEvent output:
                    standardOut.AppendLine(output.Text);
                    if (!supressOutput && !string.IsNullOrWhiteSpace(output.Text) && (filter?.Invoke(output.Text) ?? true))
                    {
                        this.logger.LogInformation("OUT> {Text}", output.Text/*.EscapeMarkup()*/.TrimStart());
                    }

                    break;
                case StandardErrorCommandEvent error:
                    if (!supressOutput && !string.IsNullOrWhiteSpace(error.Text) && (filter?.Invoke(error.Text) ?? true))
                    {
                        this.logger.LogError("ERR> {Text}", error.Text/*.EscapeMarkup()*/.TrimStart());
                    }

                    standardError.AppendLine(error.Text);
                    break;
                case ExitedCommandEvent exited:
                    exitCode = exited.ExitCode;
                    break;
            }
        }

        return new ProcessResult(
            path,
            arguments,
            exitCode,
            standardOut.ToString(),
            standardError.ToString());
    }
}
