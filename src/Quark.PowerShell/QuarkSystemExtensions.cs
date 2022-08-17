using Quark.Abstractions;

namespace Quark.PowerShell;

public static class QuarkSystemExtensions
{
    public static IQuarkTargetManager PowershellRun(
        this IQuarkTargetManager builder,
        string path,
        string creates)
    {
        builder.RunQuarkTask((context, manager, target)
            => new PowershellRunTask(path, creates)
            .ExecuteAsync(context, target));

        return builder;
    }
}
