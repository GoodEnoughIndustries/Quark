using Quark.Abstractions;

namespace Quark.Linux;

public static class QuarkSystemExtensions
{
    public static IQuarkTargetBuilder ConfigureService(
        this IQuarkTargetBuilder builder,
        string name,
        SystemdServiceOptions options)
    {
        return builder;
    }
}
