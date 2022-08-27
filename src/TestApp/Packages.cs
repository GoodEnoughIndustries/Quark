using Quark.Systems;

namespace TestApp;

public static class Packages
{
    public static PackageDescription WinDirStat = new(
        file: "windirstat1_1_2_setup.exe",
        installedLocation: @"C:\Program Files (x86)\WinDirStat\windirstat.exe",
        arguments: "/S",
        uninstallPath: @"C:\Program Files (x86)\WinDirStat\Uninstall.exe",
        version: "1.1.2.80 (Unicode)");
}
