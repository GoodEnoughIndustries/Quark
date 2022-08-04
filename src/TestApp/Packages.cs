using Quark.Systems;

namespace TestApp;

public static class Packages
{
    public static PackageDescription WinDirStat = new(
        file: "windirstat1_1_2_setup.exe",
        installedLocation: @"C:\Program Files (x86)\WinDirStat\windirstat.exe",
        uninstallPath: @"C:\Program Files (x86)\WinDirStat\Uninstall.exe",
        arguments: "/S",
        version: "1.1.2.80 (Unicode)");
}
