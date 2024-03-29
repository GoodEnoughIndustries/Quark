using Quark.Abstractions;
using System;

namespace Quark;

public class QuarkWrongTargetTypeException(
    IQuarkTarget target,
    string? message,
    Exception? innerException)
    : Exception(message, innerException)
{
    public QuarkWrongTargetTypeException(IQuarkTarget target)
        : this(target, null, null)
    {
    }

    public QuarkWrongTargetTypeException(IQuarkTarget target, string? message)
        : this(target, message, null)
    {
    }

    public IQuarkTarget Target { get; init; } = target;
}
