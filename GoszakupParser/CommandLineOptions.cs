using System.Collections.Generic;
using CommandLine;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo
// ReSharper disable ClassNeverInstantiated.Global

namespace GoszakupParser
{
    /// @author Yevgeniy Cherdantsev
    /// @date 25.02.2020 10:17:01
    /// <summary>
    /// Command line arguments are converting to this class
    /// </summary>
    public sealed class CommandLineOptions
    {
        /// <summary>
        /// List of parsers
        /// </summary>
        [Option('p', "parsers", Required = true, HelpText = "Defining parsers using spaces;" +
                                                            "\nList of available parsers can be found in documentation or in README.md")]
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
        /// Reset flag
        /// </summary>
        [Option('r', "reset", Default = false,
            HelpText = "Resets 'parsed' field to false before checking it")]
        public bool Reset { get; set; }

        /// <summary>
        /// Truncate flag (not implemented)
        /// </summary>
        /// TODO(Implement parsing table truncating while using this flag)
        [Option('t', "truncate", Default = false,
            HelpText = "Truncates parsing table before parsing (Not Implemented)")]
        public bool Truncate { get; set; }
    }
}