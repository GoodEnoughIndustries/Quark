using Quark.Abstractions;

namespace Quark.System
{
    public class PackageDescription : IQuarkPackage
    {
        private readonly string file;
        private readonly string location;
        private readonly string arguments;
        private readonly string version;

        public PackageDescription(string file, string installedLocation, string version)
        {
            this.file = file;
            this.location = installedLocation;
            this.arguments = string.Empty;
            this.version = version;
        }

        public PackageDescription(
            string file,
            string installedLocation,
            string arguments,
            string version)
            : this(file, installedLocation, version)
        {
            this.arguments = arguments;
        }
    }
}
