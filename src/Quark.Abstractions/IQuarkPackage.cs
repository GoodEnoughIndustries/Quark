namespace Quark.Abstractions;

public interface IQuarkPackage
{
    string GetInstallerPath();
    string GetArguments();
    string GetVersion();
    string GetInstalledPath();
}
