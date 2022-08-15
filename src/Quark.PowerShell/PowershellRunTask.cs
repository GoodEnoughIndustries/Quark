using Quark.Abstractions;

namespace Quark.PowerShell;

public class PowershellRunTask : IQuarkTask
{
    private readonly string path;

    public PowershellRunTask(string path)
        => this.path = path;

    public List<IQuarkTarget> Targets { get; init; } = new();

    public async Task<IQuarkResult> ExecuteAsync(QuarkContext context, IQuarkTarget target)
    {
        var fs = context.FileSystem;
        var tempDir = fs.GetTemporaryDirectory();
        var script = await fs.GetFileAsync(Path.Combine(tempDir, this.path));

        return QuarkResult.GetResult(RunResult.NotImplemented, target, this);
    }
}
