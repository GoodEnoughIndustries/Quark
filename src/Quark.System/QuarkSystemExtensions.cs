using Quark.Abstractions;
using Quark.System;
using Quark.System.Tasks;

namespace Quark
{
    public static class QuarkSystemExtensions
    {
        public static QuarkConfigurationBuilder ManagePackage(
            this QuarkConfigurationBuilder builder,
            string file,
            string installedLocation,
            string arguments = "",
            bool shouldExist = true)
        {
            return ManagePackage(
                builder,
                new PackageDescription(
                    file,
                    installedLocation,
                    arguments),
                shouldExist);
        }

        public static QuarkConfigurationBuilder ManagePackage(
            this QuarkConfigurationBuilder builder,
            IQuarkPackage package,
            bool shouldExist = true)
        {
            builder.AddQuarkTask(new ManagePackageTask(package, shouldExist));
            return builder;
        }

        public static QuarkConfigurationBuilder ConfigureService(
            this QuarkConfigurationBuilder builder,
            string name,
            SystemdServiceOptions options)
        {
            return builder;
        }

    }
}
