using Quark.Abstractions;

namespace Quark;

public interface IQuarkResult
{
    RunResult Result { get; init; }
    IQuarkTask Task { get; init; }
    IQuarkTarget Target { get; init; }
}

public enum RunResult
{
    Unknown = 0,
    Success,
    Fail,
    Skipped,
    NotImplemented,
}
