using Quark.Abstractions;
using System;

namespace Quark;

public class QuarkWrongTargetTypeException : Exception
{
    public QuarkWrongTargetTypeException(IQuarkTarget target)
        : this(target, null, null)
    {
    }

    public QuarkWrongTargetTypeException(IQuarkTarget target, string? message)
        : this(target, message, null)
    {
    }

    public QuarkWrongTargetTypeException(IQuarkTarget target, string? message, Exception? innerException)
        : base(message, innerException)
        => this.Target = target;

    public IQuarkTarget Target { get; init; }
}
