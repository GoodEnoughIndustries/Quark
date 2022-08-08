using Quark.Abstractions;
using Quark.Systems;
using Quark.Systems.Tasks;
using System.Threading.Tasks;

namespace Quark;

public static class QuarkSystemExtensions
{
    public static QuarkConfigurationBuilder ManagePackage(
        this QuarkConfigurationBuilder builder,
        IQuarkPackage package,
        bool shouldExist = true)
    {
        var manager = (IQuarkTargetManager)builder;

        manager.RunQuarkTask((context, manager, target)
            => new ManagePackageTask(package, shouldExist)
            .ExecuteAsync(context, target));

        return builder;
    }

    public static IQuarkTargetManager ManagePackage(
        this IQuarkTargetManager manager,
        IQuarkPackage package,
        bool shouldExist = true)
    {
        manager.RunQuarkTask((context, manager, target)
            => new ManagePackageTask(package, shouldExist)
            .ExecuteAsync(context, target));

        return manager;
    }

    public static async Task<IQuarkTargetManager> DownloadFile(
        this IQuarkTargetManager manager,
        string url,
        string destination)
    => await manager.RunQuarkTask((context, manager, target)
            => new DownloadTask(url, destination)
            .ExecuteAsync(context, target));
}
