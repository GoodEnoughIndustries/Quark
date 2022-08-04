using Quark.Abstractions;
using Quark.Systems;
using Quark.Systems.Tasks;

namespace Quark;

public static class QuarkSystemExtensions
{
    public static QuarkConfigurationBuilder ManagePackage(
        this QuarkConfigurationBuilder builder,
        IQuarkPackage package,
        bool shouldExist = true)
    {
        builder.AddQuarkTask((hostBuilder) => new ManagePackageTask(package, shouldExist));

        return builder;
    }

    public static IQuarkTargetBuilder ManagePackage(
        this IQuarkTargetBuilder builder,
        IQuarkPackage package,
        bool shouldExist = true)
    {
        builder.AddQuarkTask((hostBuilder) => new ManagePackageTask(package, shouldExist));

        return builder;
    }

    public static IQuarkTargetBuilder DownloadFile(
        this IQuarkTargetBuilder builder,
        string url,
        string destination)
    {
        builder.AddQuarkTask((hostBuilder) => new DownloadTask(url, destination));
        return builder;
    }
}
