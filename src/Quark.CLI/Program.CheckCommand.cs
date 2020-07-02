using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Threading.Tasks;

namespace Quark.CLI
{
    public partial class Program
    {
        private static Command CheckCommand()
        {
            var checkCommand = new Command("check", "Starts a dry-run of specified runbook(s) against provided target(s).")
            {
                TreatUnmatchedTokensAsErrors = true,
                Handler = CommandHandler.Create<QuarkCLIOptions, InvocationContext, IConsole, IHost>(RunCheck),
            };

            checkCommand.Add(RunbookOption(required: true));
            checkCommand.Add(TargetsOption(required: true));
            checkCommand.Add(MetadataOption(required: false));

            return checkCommand;
        }

        private static Task<int> RunCheck(
            QuarkCLIOptions options,
            InvocationContext context,
            IConsole console,
            IHost host)
        {
            var serviceProvider = host.Services;
            configuration = serviceProvider.GetRequiredService<IConfiguration>();

            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(typeof(Program));

            return Task.FromResult(1);
        }
    }
}
