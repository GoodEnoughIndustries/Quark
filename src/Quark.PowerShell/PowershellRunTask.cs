using Quark.Abstractions;

namespace Quark.PowerShell;

public class PowershellRunTask : IQuarkTask
{
    private readonly string path;
    private readonly string createsPath;

    public PowershellRunTask(string path, string createsPath)
    {
        this.path = path;
        this.createsPath = createsPath;
    }

    public List<IQuarkTarget> Targets { get; init; } = new();

    public async Task<IQuarkResult> ExecuteAsync(QuarkContext context, IQuarkTarget target)
    {
        var fs = context.FileSystem;

        var existingPath = await fs.GetFileAsync(this.createsPath);

        if (existingPath is not null)
        {
            return QuarkResult.GetResult(RunResult.Skipped, target, this);
        }

        var tempDir = fs.GetTemporaryDirectory();
        var script = await fs.GetFileAsync(Path.Combine(tempDir, this.path));

        if (script is null)
        {
            return QuarkResult.GetFailed(target, this);
        }

        var pp = context.ProcessProvider;
        var sciptInstall = await pp.Start("powershell.exe", $"–noprofile & '{script.FullName}'");

        return QuarkResult.GetResult(sciptInstall.ExitCode == 0 ? RunResult.Success : RunResult.Fail, target, this);
    }
}
