using Quark.Abstractions;

namespace Quark.Systems;

public class PackageDescription : IQuarkPackage, IQuarkUninstallablePackage
{
    private readonly string file;
    private readonly string uninstallPath;
    private readonly string location;
    private readonly string arguments;
    private readonly string version;

    public PackageDescription(
        string file,
        string installedLocation,
        string version)
    {
        this.file = file;
        this.location = installedLocation;
        this.arguments = string.Empty;
        this.uninstallPath = string.Empty;
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

    public PackageDescription(
        string file,
        string installedLocation,
        string arguments,
        string uninstallPath,
        string version)
        : this(file, installedLocation, arguments, version)
    {
        this.uninstallPath = uninstallPath; ;
    }

    public string GetArguments() => this.arguments;
    public string GetInstalledPath() => this.location;
    public string GetInstallerPath() => this.file;
    public string GetUninstallPath() => this.uninstallPath;
    public string GetVersion() => this.version;
}
