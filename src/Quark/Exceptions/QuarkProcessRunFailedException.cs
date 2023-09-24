using Quark.Abstractions;
using System;

namespace Quark;

[Serializable]
public class QuarkProcessRunFailedException(
    ProcessResult processResult,
    string? message,
    Exception? innerException)
    : Exception(message, innerException)
{
    public QuarkProcessRunFailedException(ProcessResult processResult)
        : this(processResult, null)
    {
    }

    public QuarkProcessRunFailedException(ProcessResult processResult, string? message)
        : this(processResult, message, null)
    {
    }

    public ProcessResult Result { get; init; } = processResult;
}
