using Quark.Abstractions;

namespace Quark.PowerShell;

public static class QuarkSystemExtensions
{
    /// <summary>
    /// Executes a PowerShell script.
    /// </summary>
    /// <param name="manager">The <seealso cref="IQuarkTargetManager"/> executing this.</param>
    /// <param name="path">The path to the PowerShell script to run.</param>
    /// <param name="creates">The file or folder that exists after this the script is ran.</param>
    /// <returns>The <seealso cref="IQuarkTargetManager"/> to chain tasks with.</returns>
    public static IQuarkTargetManager PowerShellRun(
        this IQuarkTargetManager manager,
        string path,
        string creates)
    {
        manager.RunQuarkTask((context, manager, target)
            => new PowershellRunTask(path, creates)
            .ExecuteAsync(context, manager, target));

        return manager;
    }
}
