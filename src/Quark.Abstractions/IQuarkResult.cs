using Quark.Abstractions;

namespace Quark;

public interface IQuarkResult
{
    QuarkRunResult QuarkRunResult { get; init; }
    IQuarkTask QuarkTask { get; init; }
    IQuarkTarget QuarkTarget { get; init; }
}

public enum QuarkRunResult
{
    Unknown = 0,
    Success,
    Fail,
    Skipped,
    NotImplemented,
}
