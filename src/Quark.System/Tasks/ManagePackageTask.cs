using Microsoft.Extensions.Logging;
using Quark.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Quark.Systems.Tasks;

public class ManagePackageTask : IQuarkTask
{
    public IQuarkPackage PackageDescription { get; }
    public bool ShouldExist { get; }

    public List<IQuarkTarget> Targets { get; init; } = new();

    public ManagePackageTask(IQuarkPackage packageDescription, bool shouldExist)
    {
        this.PackageDescription = packageDescription;
        this.ShouldExist = shouldExist;
    }

    public async Task<IQuarkResult> ExecuteAsync(QuarkContext context, IQuarkTarget target)
    {
        var result = target.Type == QuarkTargetTypes.Windows
            ? await WindowsExecuteAsync(context, target)
            : QuarkResult.GetResult(RunResult.NotImplemented, target, this);

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
            if (result.Result is RunResult.Fail)
            {
                result = QuarkResult.GetResult(
                    RunResult.Skipped,
                    result.Target,
                    result.Task);
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

                return QuarkResult.GetResult(RunResult.Skipped, target, this);
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

            // TODO: real Process handler, so we can capture logs
            // have remote Process, etc.
            var psi = new ProcessStartInfo
            {
                Arguments = this.PackageDescription.GetArguments(),
                FileName = uninstallFile.FullName,
                Verb = "runas",
                UseShellExecute = true,
            };

            using var process = await pp.Start(psi);

            if (process is null)
            {
                return QuarkResult.GetFailed(target, this);
            }
            else
            {
                await process.WaitForExitAsync();
                // TODO: put a some checks on this.
                // It can take a brief amount of time before the files is gone.
                while (uninstallFile is not null)
                {
                    uninstallFile = await fs.GetFileAsync(uninstall.GetUninstallPath());
                }
            }
        }

        return QuarkResult.GetResult(RunResult.Success, target, this);
    }

    private async Task<IQuarkResult> InstallPackage(QuarkContext context, IQuarkTarget target)
    {
        var fs = context.FileSystem;
        var pp = context.ProcessProvider;

        var installFile = await fs.GetFileAsync(this.PackageDescription.GetInstalledPath());

        if (installFile is null)
        {
            var logger = context.GetLogger<ManagePackageTask>();

            var psi = new ProcessStartInfo
            {
                Arguments = this.PackageDescription.GetArguments(),
                FileName = this.PackageDescription.GetInstallerPath(),
                Verb = "runas",
                UseShellExecute = true,
            };

            logger.LogInformation("{PackageDescription} is being installed: {ProcessStartInfo}", this.PackageDescription, psi);

            using var process = await pp.Start(psi);
            if (process is null)
            {
                return QuarkResult.GetFailed(target, this);
            }
            else
            {
                await process.WaitForExitAsync();
            }
        }

        return QuarkResult.GetResult(RunResult.Success, target, this);
    }
}
