using CommandLine;
using System.Collections.Generic;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

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
        /// Verb for parsing data
        /// </summary>
        [Verb("parse", HelpText = "Parses data from source")]
        public class Parse : GeneralOptions
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
            /// Truncate flag (not implemented)
            /// </summary>
            /// \todo(Implement parsing table truncating while using this flag)
            [Option("truncate", Default = false,
                HelpText = "Truncates parsing tables before parsing (Not Implemented)")]
            public bool Truncate { get; set; }
        }

        /// <summary>
        /// Verb for downloading data
        /// </summary>
        [Verb("download", HelpText = "Downloads files from source")]
        public class Download : GeneralOptions
        {
            /// <summary>
            /// List of downloaders
            /// </summary>
            [Option('d', "downloaders", Required = true, HelpText = "Defining downloaders using spaces;" +
                                                                  "\nList of available downloaders can be found in documentation or in README.md")]
            public IEnumerable<string> Downloaders { get; set; }
            
            /// <summary>
            /// Folder path where documentation are stored
            /// </summary>
            [Option('f', "folder", Required = false, HelpText = "Redefining folder where documentation are stored")]
            public string Folder { get; set; }
        }

        /// <summary>
        /// General options for all activities
        /// </summary>
        public class GeneralOptions
        {
            /// <summary>
            /// Ignore flag
            /// </summary>
            [Option('t', "threads", HelpText = "Redefine number of threads")]
            public int Threads { get; set; }

            /// <summary>
            /// No trace flag
            /// </summary>
            [Option("no-trace", Default = false, HelpText = "Removes trace level logging")]
            public bool NoTrace { get; set; }

            /// <summary>
            /// No info flag
            /// </summary>
            [Option("no-info", Default = false, HelpText = "Removes info level logging")]
            public bool NoInfo { get; set; }

            /// <summary>
            /// No warn flag
            /// </summary>
            [Option("no-warn", Default = false, HelpText = "Removes warn level logging")]
            public bool NoWarn { get; set; }

            /// <summary>
            /// No error flag
            /// </summary>
            [Option("no-error", Default = false, HelpText = "Removes error level logging")]
            public bool NoError { get; set; }

            /// <summary>
            /// No fatal flag
            /// </summary>
            [Option("no-fatal", Default = false, HelpText = "Removes fatal level logging")]
            public bool NoFatal { get; set; }
        }
    }
}