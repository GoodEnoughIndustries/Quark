using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.CommandLine.Parsing;
using System.CommandLine.Rendering;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Quark.CLI;

public partial class Program
{
    private static IConfiguration configuration = new ConfigurationBuilder()
        .Build();

    public async static Task<int> Main(string[] args)
    {
        var rootCommand = GetRootCommand();

        var parser = new CommandLineBuilder(rootCommand)
            .UseQuarkCommandLineOptions()
            .RegisterWithDotnetSuggest()
            .UseExceptionHandler(HandleException)
            .UseHost(CreateHostBuilder, host =>
            {
                host.ConfigureHostConfiguration(config =>
                {
                    configuration = AddConfigurationSources(args, config).Build();
                });

                host.ConfigureServices(ConfigureServices);
            })
            .Build();

        var result = await parser
            .InvokeAsync(args)
            .ConfigureAwait(false);

        return result;
    }

    private static IConfigurationBuilder AddConfigurationSources(string[] args, IConfigurationBuilder config)
    {
        config
            .AddUserSecrets(typeof(Program).Assembly, optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args);

        return config;
    }

    private static Command GetRootCommand()
    {
        var root = new RootCommand
        {
            Description = "Quark",
             
        };

        root.Add(CheckCommand());

        return root;
    }

    private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
        .ConfigureHostConfiguration(builder =>
        {
        });

    private static void HandleException(Exception exception, InvocationContext context)
    {
        if (exception is TargetInvocationException tie
            && tie.InnerException is object)
        {
            exception = tie.InnerException;
        }

        if (exception is OperationCanceledException)
        {
            context.Console.Error.WriteLine("Operation canceled.");
        }
        else
        {
            context.Console.Error.WriteLine("An unhandled exception has occurred, how unseemly: ");
            context.Console.Error.WriteLine(exception.ToString());
        }
    }
}
