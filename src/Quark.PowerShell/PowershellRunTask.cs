using Microsoft.Extensions.Logging;
using Quark.Abstractions;

namespace Quark.PowerShell;

public class PowershellRunTask(string taskName, string path, string createsPath) : IQuarkTask
{
    public string TaskName { get; init; } = taskName;
    public List<IQuarkTarget> Targets { get; init; } = new();

    public async Task<IQuarkResult> ExecuteAsync(QuarkContext context, IQuarkTargetManager manager, IQuarkTarget target)
    {
        var fs = context.FileSystem;

        var existingPath = await fs.GetFileAsync(createsPath);

        if (existingPath is not null)
        {
            context.GetLogger<PowershellRunTask>().LogInformation("{CreatesPath} already exists, skipping.", createsPath);

            return QuarkResult.GetResult(QuarkRunResult.Skipped, target, this);
        }

        var tempDir = fs.GetTemporaryDirectory();
        var script = await fs.GetFileAsync(Path.Combine(tempDir, path));

        if (script is null)
        {
            return QuarkResult.GetFailed(target, this);
        }

        var pp = context.ProcessProvider;

        // TODO: powershell core and multiplat
        var sciptInstall = await pp.Start("powershell.exe", $"–noprofile & '{script.FullName}'");

        var finalFile = await fs.GetFileAsync(createsPath);

        if (finalFile is null)
        {
            return QuarkResult.GetFailed(target, this);
        }

        return QuarkResult.GetResult(sciptInstall.ExitCode == 0 ? QuarkRunResult.Success : QuarkRunResult.Fail, target, this);
    }
}
