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

public class QuarkProcessProvider(
    ILogger<QuarkProcessProvider> logger,
    IQuarkExecutionContext context,
    IQuarkCredentialProvider credentialProvider,
    IQuarkFileSystem fileSystem) : IQuarkProcessProvider
{
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
        var result = ProcessResultResult.Unknown;

        try
        {
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
                            logger.LogInformation("OUT> {Text}", output.Text/*.EscapeMarkup()*/.TrimStart());
                        }

                        break;
                    case StandardErrorCommandEvent error:
                        if (!supressOutput && !string.IsNullOrWhiteSpace(error.Text) && (filter?.Invoke(error.Text) ?? true))
                        {
                            logger.LogError("ERR> {Text}", error.Text/*.EscapeMarkup()*/.TrimStart());
                        }

                        standardError.AppendLine(error.Text);
                        break;
                    case ExitedCommandEvent exited:
                        exitCode = exited.ExitCode;
                        result = ProcessResultResult.Ran;
                        break;
                }
            }
        }
        catch (Exception e)
        {
            logger.LogError("An issue trying to run something: {Exception}", e.Message);
            if (result == ProcessResultResult.Unknown)
            {
                result = ProcessResultResult.UnableToRun;
            }
        }

        if (result == ProcessResultResult.Unknown)
        {
            logger.LogDebug("Process run {Result} shouldn't be Unknown.", result);
        }

        return new ProcessResult(
            path,
            arguments,
            exitCode,
            result,
            standardOut.ToString(),
            standardError.ToString());
    }
}
