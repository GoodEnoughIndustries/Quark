using Quark.Abstractions;

namespace Quark.Windows;

public static class QuarkSystemExtensions
{
    public static IQuarkTargetBuilder PowershellRun(
        this IQuarkTargetBuilder builder,
        string path)
    {
        builder.AddQuarkTask((builder) => new PowershellRunTask(path));
        return builder;
    }
}
