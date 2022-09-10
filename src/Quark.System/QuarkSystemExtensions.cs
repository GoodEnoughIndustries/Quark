using Quark.Abstractions;
using Quark.Systems;
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
            .ExecuteAsync(context, manager, target));

        return builder;
    }

    public static IQuarkTargetManager ManagePackage(
        this IQuarkTargetManager manager,
        IQuarkPackage package,
        bool shouldExist = true)
    {
        manager.RunQuarkTask((context, manager, target) =>
        {
            return new ManagePackageTask(package, shouldExist)
            .ExecuteAsync(context, manager, target);
        });

        return manager;
    }

    public static Task<IQuarkTargetManager> DownloadFile(
        this IQuarkTargetManager manager,
        string url,
        string destination)
    => manager.RunQuarkTask((context, manager, target)
        => new DownloadTask(url, destination)
        .ExecuteAsync(context, manager, target));
}
