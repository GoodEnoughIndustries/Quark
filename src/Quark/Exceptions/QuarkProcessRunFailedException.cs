using Quark.Abstractions;
using System;

namespace Quark;

[Serializable]
public class QuarkProcessRunFailedException : Exception
{
    public QuarkProcessRunFailedException(ProcessResult processResult)
        : this(processResult, null)
    {
    }

    public QuarkProcessRunFailedException(ProcessResult processResult, string? message)
        : this(processResult, message, null)
    {
    }

    public QuarkProcessRunFailedException(ProcessResult processResult, string? message, Exception? innerException)
        : base(message, innerException)
        => this.Result = processResult;

    public ProcessResult Result { get; init; }
}
