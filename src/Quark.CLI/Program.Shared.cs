using System;
using System.Collections.Generic;
using System.CommandLine;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quark.CLI
{
    public partial class Program
    {
        private static Option<IEnumerable<QuarkRunbook>> MetadataOption(bool required = false)
        {
            var metadataOption = new Option<IEnumerable<QuarkRunbook>>(
                alias: "--metadata",
                description: "Paths to files containing runbooks.")
            {
                Required = required,
                Argument = new Argument<IEnumerable<QuarkRunbook>>
                {
                    Arity = ArgumentArity.OneOrMore,
                }
                .LegalFilePathsOnly()
                .WithSuggestions("hosts.json", "hosts.xml", "targets.yml", "localhost", "server[01:10]"),
            };

            return metadataOption;
        }

        private static Option<IEnumerable<QuarkRunbook>> RunbookOption(bool required = false)
        {
            var runbookOptions = new Option<IEnumerable<QuarkRunbook>>(
                alias: "--runbooks",
                description: "Paths to files containing runbooks.")
            {
                Required = required,
                Argument = new Argument<IEnumerable<QuarkRunbook>>
                {
                    Arity = ArgumentArity.OneOrMore,
                }
                .LegalFilePathsOnly()
                .WithSuggestions("hosts.json", "hosts.xml", "targets.yml", "localhost", "server[01:10]"),
            };

            return runbookOptions;
        }

        private static Option<IEnumerable<QuarkTarget>> TargetsOption(bool required = false)
        {
            var targetsOption = new Option<IEnumerable<QuarkTarget>>(
                alias: "--targets",
                description: "Paths to files containing target machines, or comma delimited patterns of target names.")
            {
                Required = required,
                Argument = new Argument<IEnumerable<QuarkTarget>>
                {
                    Arity = ArgumentArity.OneOrMore,
                }
                .LegalFilePathsOnly()
                .WithSuggestions("hosts.json", "hosts.xml", "targets.yml", "localhost", "server[01:10]"),
            };

            targetsOption.AddAlias("--hosts");

            return targetsOption;
        }
    }
}
