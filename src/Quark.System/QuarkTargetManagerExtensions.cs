using Quark.Abstractions;

namespace Quark;

public static class QuarkTargetManagerExtensions
{
    public static IQuarkTargetManager ElevateAs(this IQuarkTargetManager manager, IQuarkCredential credential)
    {
        manager.Credentials.Add(credential);

        return manager;
    }
}
