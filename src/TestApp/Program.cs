using Microsoft.Extensions.Hosting;
using Quark;
using Quark.Abstractions;
using Quark.PowerShell;
using System.Threading.Tasks;
using TestApp;

var adminCredential = new QuarkUserNamePasswordCredential("blah@gmail.com", "password");

var configuration = new QuarkConfigurationBuilder()
    .WithQuarkFiles(path: @"C:\QuarkFiles")
    .ManagePackage(Packages.WinDirStat, shouldExist: false)
    .WithTarget(target: "localhost", QuarkTargetTypes.Windows, async (context, manager, target) =>
    {
        manager.ManagePackage(Packages.WinDirStat, shouldExist: true);
        await InstallChocolately(manager);
    })
    .Build();

var runner = new QuarkRunnerBuilder(args)
    .ManageWindows()
    .ManageSystems()
    .AddQuarkConfiguration(configuration)
    .Build();

await runner.RunAsync();

return 0;

static async Task InstallChocolately(IQuarkTargetManager manager)
{
    await manager.DownloadFile(url: "https://community.chocolatey.org/install.ps1", destination: "install.ps1");
    manager.PowerShellRun(path: "install.ps1", creates: @"C:\ProgramData\chocolatey\choco.exe");
}

//        builder.PowershellRun(path: "install.ps1");
//        builder.PowershellRun(script: """
//Set-ExecutionPolicy Bypass -Scope Process
//-Force;
//[System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072;
//iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))
//""");
//
//        builder.Execute("choco", exitCode: 0);
//        builder.EnsureFile(path: "install.ps1", FileState.Absent);
