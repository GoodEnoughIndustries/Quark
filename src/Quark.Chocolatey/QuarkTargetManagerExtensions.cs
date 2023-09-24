using Quark.Abstractions;
using Quark.PowerShell;

namespace Quark.Chocolatey;

public static class QuarkTargetManagerExtensions
{
    public static async Task<IQuarkTargetManager> InstallChocolatey(this IQuarkTargetManager manager)
    {
        await manager.DownloadFile("Downloading Chocolatey installer", url: "https://community.chocolatey.org/install.ps1", destination: "install.ps1");
        return manager.PowerShellRun("Run Chocolatey installer", path: "install.ps1", creates: @"C:\ProgramData\chocolatey\choco.exe");
    }

    public static async Task<IQuarkTargetManager> ChocolateyPackage(this IQuarkTargetManager manager, QuarkContext context, string packageName, bool shouldExist = true)
    {
        var pp = context.ProcessProvider;

        var listResult = await pp.Start("choco", $"list --limit-output --local-only --exact {packageName}");

        if (listResult.ExitCode != 0)
        {
            throw new QuarkProcessRunFailedException(listResult);
        }

        var (isInstalled, version) = IsPackageInstalled(packageName, listResult);

        if ((isInstalled && shouldExist) || (!isInstalled && !shouldExist))
        {
            // skip
        }
        else if (isInstalled && !shouldExist)
        {
            // uninstall
            var uninstallResult = await pp.Start("choco", $"uninstall {packageName} -y");
        }
        else if (!isInstalled && shouldExist)
        {
            // install
            var installResult = await InstallPackage(context, packageName);
        }

        return manager;
    }

    private static (bool isInstalled, string? version) IsPackageInstalled(string packageName, ProcessResult processResult)
    {
        var isInstalled = false;
        string? version = null;

        if (string.IsNullOrEmpty(processResult.StandardOut)
            || !processResult.StandardOut.Contains(packageName))
        {
            isInstalled = false;
        }
        else
        {
            var installedList = processResult.StandardOut.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (installedList.Length > 0)
            {
                var package = Array.Find(installedList, il => il.Contains(packageName, StringComparison.OrdinalIgnoreCase));

                if (package is not null)
                {
                    isInstalled = true;
                    version = package.Split('|').Last();
                }
            }
        }

        return (isInstalled, version);
    }

    private static async Task<ProcessResult> InstallPackage(QuarkContext context, string packageName)
    {
        var pp = context.ProcessProvider;

        return await pp.Start("choco", $"install {packageName} -y", l => !l.StartsWith("Progress: "));
    }
}
