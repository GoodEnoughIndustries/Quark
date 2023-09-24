using System;
using System.Threading.Tasks;

namespace Quark.Abstractions;

public interface IQuarkProcessProvider
{
    Task<ProcessResult> Start(string path, string? arguments = null, Func<string, bool>? filter = null, bool supressOutput = false);
}

public record ProcessResult(
    string Path,
    string? Arguments,
    int ExitCode,
    ProcessResultResult Result = ProcessResultResult.Unknown,
    string? StandardOut = null,
    string? StandardError = null);

public enum ProcessResultResult
{
    Unknown = 0,
    Ran,
    UnableToRun,
}
