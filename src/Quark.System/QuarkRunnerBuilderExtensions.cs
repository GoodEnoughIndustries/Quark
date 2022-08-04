using Microsoft.Extensions.DependencyInjection;
using Quark.Abstractions;
using Quark.Systems;
using Serilog;
using Serilog.Events;

namespace Quark;

public static class QuarkRunnerBuilderExtensions
{
    public static QuarkRunnerBuilder ManageSystems(this QuarkRunnerBuilder builder)
    {
        builder
            .UseSerilog((context, services, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .Enrich.FromLogContext()
                    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                    .DestructureQuark();
            })
            .ConfigureServices((c, sc) =>
            {
                sc.AddSingleton<IQuarkFactProvider, EnvironmentFactsProvider>();
                sc.AddSingleton<IQuarkProcessProvider, QuarkProcessProvider>();
                sc.AddSingleton<IQuarkFileProvider, FileSystemFileProvider>();
                sc.AddSingleton<IQuarkFileProvider, QuarkFilesFileProvider>();
                sc.AddSingleton<IQuarkFactProvider, MachineFactsProvider>();
                sc.AddSingleton<IQuarkFileSystem, QuarkFileSystem>();
            });

        return builder;
    }
}
