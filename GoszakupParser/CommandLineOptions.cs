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
                                                            "\n    Rnu")]
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
    }
}