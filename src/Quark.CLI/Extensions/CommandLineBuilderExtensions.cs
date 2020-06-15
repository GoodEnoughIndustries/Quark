using System.CommandLine.Parsing;
using System.CommandLine.Rendering;

namespace System.CommandLine.Builder
{
    public static class CommandLineBuilderExtensions
    {
        public static CommandLineBuilder UseQuarkCommandLineOptions(this CommandLineBuilder builder)
            => builder
                .UseHelp()
                .UseVersionOption()
                .UseDebugDirective()
                .UseParseErrorReporting()
                .ParseResponseFileAs(ResponseFileHandling.ParseArgsAsSpaceSeparated)
                .CancelOnProcessTermination()
                .UseAnsiTerminalWhenAvailable()
                .UseDefaults();
    }
}
