using Microsoft.Extensions.Logging;
using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Quark.Systems;

public class ManagePackageTask(string taskName, IQuarkPackage packageDescription, bool shouldExist) : IQuarkTask
{
    string IQuarkTask.TaskName { get; init; } = taskName;
    public IQuarkPackage PackageDescription { get; } = packageDescription;
    public bool ShouldExist { get; } = shouldExist;

    public List<IQuarkTarget> Targets { get; init; } = new();

    public async Task<IQuarkResult> ExecuteAsync(QuarkContext context, IQuarkTargetManager manager, IQuarkTarget target)
    {
        var result = target.TargetType == QuarkTargetTypes.Windows
            ? await WindowsExecuteAsync(context, target)
            : QuarkResult.GetResult(QuarkRunResult.NotImplemented, target, this);

        return result;
    }

    private async Task<IQuarkResult> WindowsExecuteAsync(QuarkContext context, IQuarkTarget target)
    {
        if (this.ShouldExist)
        {
            return await EnsurePackage(context, target);
        }
        else
        {
            var result = await UninstallPackage(context, target);
            if (result.QuarkRunResult is QuarkRunResult.Fail)
            {
                result = QuarkResult.GetResult(
                    QuarkRunResult.Skipped,
                    result.QuarkTarget,
                    result.QuarkTask);
            }

            return result;
        }
    }

    public async Task<IQuarkResult> EnsurePackage(QuarkContext context, IQuarkTarget target)
    {
        var file = await context.FileSystem.GetFileAsync(this.PackageDescription.GetInstalledPath());

        if (file is not null)
        {
            var version = FileVersionInfo.GetVersionInfo(file.FullName);
            if (version?.FileVersion?.Equals(this.PackageDescription.GetVersion(), StringComparison.OrdinalIgnoreCase) ?? false)
            {
                var logger = context.GetLogger<ManagePackageTask>();

                logger.LogInformation("{PackageDescription} is already installed", this.PackageDescription);

                return QuarkResult.GetResult(QuarkRunResult.Skipped, target, this);
            }
            else
            {
                // if we're here, file exists, but is wrong version.
                // uninstall if possible, and later install right package.
                await UninstallPackage(context, target);
            }
        }

        // if we're here, install.
        return await InstallPackage(context, target);
    }

    private async Task<IQuarkResult> UninstallPackage(QuarkContext context, IQuarkTarget target)
    {
        if (this.PackageDescription is IQuarkUninstallablePackage uninstall)
        {
            var logger = context.GetLogger<ManagePackageTask>();

            logger.LogInformation("{PackageDescription} is being uninstalled", this.PackageDescription);
            var fs = context.FileSystem;
            var pp = context.ProcessProvider;

            var uninstallFile = await fs.GetFileAsync(uninstall.GetUninstallPath());

            if (uninstallFile is null)
            {
                logger.LogInformation("Uninstall file path does not exist.");

                return QuarkResult.GetFailed(target, this);
            }

            var processResult = await pp.Start(uninstallFile.FullName, this.PackageDescription.GetArguments());

            if (processResult.ExitCode != 0)
            {
                return QuarkResult.GetFailed(target, this);
            }
            else
            {
                // await processResult.WaitForExitAsync();
                // TODO: put a some checks on this.
                // It can take a brief amount of time before the files is gone.
                while (uninstallFile is not null)
                {
                    uninstallFile = await fs.GetFileAsync(uninstall.GetUninstallPath());
                }
            }
        }

        return QuarkResult.GetResult(QuarkRunResult.Success, target, this);
    }

    private async Task<IQuarkResult> InstallPackage(QuarkContext context, IQuarkTarget target)
    {
        var fs = context.FileSystem;
        var pp = context.ProcessProvider;

        var installFile = await fs.GetFileAsync(this.PackageDescription.GetInstalledPath());

        if (installFile is null)
        {
            var logger = context.GetLogger<ManagePackageTask>();

            installFile = await fs.GetFileAsync(this.PackageDescription.GetInstallerPath());

            if (installFile is null)
            {
                logger.LogInformation("{PackageDescription} cannot find {FilePath}", this.PackageDescription, this.PackageDescription.GetInstallerPath());
                return QuarkResult.GetFailed(target, this);
            }

            logger.LogInformation("{PackageDescription} is being installed: {FilePath} {Arguments}", this.PackageDescription, installFile.FullName, this.PackageDescription.GetArguments());

            var processResult = await pp.Start(installFile.FullName, this.PackageDescription.GetArguments());
            if (processResult.ExitCode != 0
                || processResult.Result != ProcessResultResult.Ran)
            {
                return QuarkResult.GetFailed(target, this);
            }
        }

        return QuarkResult.GetResult(QuarkRunResult.Success, target, this);
    }
}
