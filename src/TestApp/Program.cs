using Microsoft.Extensions.Hosting;
using Quark;
using Quark.Abstractions;
using Serilog;
using Serilog.Events;
using TestApp;

var adminCredential = new QuarkUserNamePasswordCredential("blah@gmail.com", "password");

var configuration = new QuarkConfigurationBuilder()
    .WithQuarkFiles(path: @"C:\QuarkFiles")
    .ManagePackage(Packages.WinDirStat, shouldExist: false)
    .WithTarget(target: "localhost", QuarkTargetTypes.Windows, builder =>
    {
        builder.ElevateAs(adminCredential);
        builder.ManagePackage(Packages.WinDirStat, shouldExist: true);
        builder.DownloadFile(url: "https://community.chocolatey.org/install.ps1", destination: "install.ps1");
    })
    .Build();

var runnerBuilder = new QuarkRunnerBuilder(args)
    .ManageWindows()
    .ManageSystems()
    .AddQuarkConfiguration(configuration);

var runner = runnerBuilder.Build();

await runner.RunAsync();

return 0;

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
