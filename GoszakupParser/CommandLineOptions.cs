using System.Collections.Generic;
using CommandLine;

namespace GoszakupParser
{
    public sealed class CommandLineOptions
    {
        /// <summary>
        /// List of parsers
        /// </summary>
        [Option('p', "parsers", Required = true, HelpText = "Defining parsers using spaces;" +
                                                            "\nList of available parsers:" +
                                                            "\n    Participant" +
                                                            "\n    Unscrupulous" +
                                                            "\n    Contract" +
                                                            "\n    Announcement" +
                                                            "\n    Lot" +
                                                            "\n    Plan" +
                                                            "\n    Director" +
                                                            "\n    RnuReference")]
        public IEnumerable<string> Parsers { get; set; }

        /// <summary>
        /// Force flag
        /// </summary>
        [Option('f', "force", Default = false, HelpText = "Ignores 'active' and 'parsed' fields")]
        public bool Force { get; set; }

        /// <summary>
        /// Ignore flag
        /// </summary>
        [Option('i', "ignore", Default = false, HelpText = "Ignores 'parsed' field")]
        public bool Ignore { get; set; }

        /// <summary>
        /// Truncate flag
        /// </summary>
        [Option('t', "truncate", Default = false, HelpText = "Truncates parsing table before parsing (Not Implemented)")]
        public bool Truncate { get; set; }
    }
}