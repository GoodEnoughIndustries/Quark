using Quark.Abstractions;
using Quark.System;
using Quark.System.Tasks;

namespace Quark
{
    public static class QuarkSystemExtensions
    {
        public static QuarkConfigurationBuilder ManagePackage(
            this QuarkConfigurationBuilder builder,
            IQuarkPackage package,
            bool shouldExist = true)
        {
            builder.AddQuarkTask(new ManagePackageTask(package, shouldExist));

            return builder;
        }

        public static IQuarkTargetBuilder ManagePackage(
            this IQuarkTargetBuilder builder,
            IQuarkPackage package,
            bool shouldExist = true)
        {
            builder.AddQuarkTask(new ManagePackageTask(package, shouldExist));
            return builder;
        }

        public static IQuarkTargetBuilder ConfigureService(
            this IQuarkTargetBuilder builder,
            string name,
            SystemdServiceOptions options)
        {
            return builder;
        }
    }
}
