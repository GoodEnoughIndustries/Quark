using Quark.Abstractions;

namespace Quark;

public class QuarkResult : IQuarkResult
{
    public RunResult Result { get; init; } = RunResult.Unknown;
    public IQuarkTask Task { get; init; } = default!;
    public IQuarkTarget Target { get; init; } = default!;

    public static QuarkResult GetFailed(IQuarkTarget target, IQuarkTask task)
        => GetResult(RunResult.Fail, target, task);

    public static QuarkResult GetResult(RunResult result, IQuarkTarget target, IQuarkTask task)
        => new()
        {
            Result = result,
            Target = target,
            Task = task,
        };
}
