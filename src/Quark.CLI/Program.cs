using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.IO;
using System.CommandLine.Parsing;
using System.Reflection;
using System.Threading.Tasks;

namespace Quark.CLI
{
    public class Program
    {
        private static IConfiguration configuration = new ConfigurationBuilder()
            .Build();

        public async static Task<int> Main(string[] args)
        {
            var rootCommand = GetRootCommand();

            var parser = new CommandLineBuilder(rootCommand)
                .UseQuarkCommandLineOptions()
                .UseExceptionHandler(HandleException)
                .UseHost(CreateHostBuilder, host =>
                {
                    host.ConfigureHostConfiguration(config =>
                    {
                        AddConfigurationSources(args, config);
                    });

                    host.ConfigureServices(ConfigureServices);
                })
                .Build();

            return await parser.InvokeAsync(args).ConfigureAwait(false);
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
                TreatUnmatchedTokensAsErrors = false,
                Handler = CommandHandler.Create<IHost>(Run),
            };

            root.Add(UrlArgument());

            return root;
        }

        private static Option<Uri> UrlArgument()
        => new Option<Uri>(
           alias: "--url",
           getDefaultValue: () => new Uri("http://localhost:9095/"),
           description: "Url to running Kapacitor instance")
           {
               Required = true,
           };

        private static void Run(IHost host)
        {
            var serviceProvider = host.Services;
            configuration = serviceProvider.GetRequiredService<IConfiguration>();

            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(typeof(Program));
            logger.LogInformation("Hello World!");
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
        }

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

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(builder =>
                {
                });
    }
}
