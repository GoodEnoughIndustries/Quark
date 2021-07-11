using Quark.Abstractions;

namespace Quark.System
{
    public class PackageDescription : IQuarkPackage
    {
        private readonly string file;
        private readonly string location;
        private readonly string arguments;

        public PackageDescription(string file, string installedLocation)
        {
            this.file = file;
            this.location = installedLocation;
            this.arguments = string.Empty;
        }

        public PackageDescription(
            string file,
            string installedLocation,
            string arguments)
            : this(file, installedLocation)
        {
            this.arguments = arguments;
        }
    }
}
