using Quark.Abstractions;
using Quark.Systems;
using System.Threading.Tasks;

namespace Quark;

public static class QuarkSystemExtensions
{
    public static QuarkConfigurationBuilder ManagePackage(
        this QuarkConfigurationBuilder builder,
        string taskName,
        IQuarkPackage package,
        bool shouldExist = true)
    {
        var manager = (IQuarkTargetManager)builder;

        manager.RunQuarkTask((context, manager, target)
            => new ManagePackageTask(taskName, package, shouldExist)
            .ExecuteAsync(context, manager, target));

        return builder;
    }

    public static IQuarkTargetManager ManagePackage(
        this IQuarkTargetManager manager,
        string taskName,
        IQuarkPackage package,
        bool shouldExist = true)
    {
        manager.RunQuarkTask((context, manager, target) =>
        {
            return new ManagePackageTask(taskName, package, shouldExist)
            .ExecuteAsync(context, manager, target);
        });

        return manager;
    }

    public static Task<IQuarkTargetManager> DownloadFile(
        this IQuarkTargetManager manager,
        string taskName,
        string url,
        string destination)
    => manager.RunQuarkTask((context, manager, target)
        => new DownloadTask(taskName, url, destination)
        .ExecuteAsync(context, manager, target));
}
