using Quark.Abstractions;

namespace Quark;

public class QuarkResult : IQuarkResult
{
    public QuarkRunResult QuarkRunResult { get; init; } = QuarkRunResult.Unknown;
    public IQuarkTask QuarkTask { get; init; } = default!;
    public IQuarkTarget QuarkTarget { get; init; } = default!;

    public static QuarkResult GetFailed(IQuarkTarget target, IQuarkTask task)
        => GetResult(QuarkRunResult.Fail, target, task);

    public static QuarkResult GetResult(QuarkRunResult result, IQuarkTarget target, IQuarkTask task)
        => new()
        {
            QuarkRunResult = result,
            QuarkTarget = target,
            QuarkTask = task,
        };
}
