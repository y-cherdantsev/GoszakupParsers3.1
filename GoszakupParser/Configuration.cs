using System;
using System.Collections.Generic;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser
{
    /// @author Yevgeniy Cherdantsev
    /// @date 25.02.2020 10:17:01
    /// <summary>
    /// Configuration class
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// Goszakup authentication bearer token
        /// </summary>
        public static string AuthToken { get; set; }

        /// <summary>
        /// Parsers settings
        /// </summary>
        public static List<ParserSettings> Parsers { get; set; }

        /// <summary>
        /// Downloaders settings
        /// </summary>
        public static List<DownloaderSettings> Downloaders { get; set; }

        /// <summary>
        /// Mapped parsers names into parsing DB titles
        /// </summary>
        public static Dictionary<string, string> ParserMonitoringMappings { get; set; }

        /// <summary>
        /// Parsing DB connection string
        /// </summary>
        public static string ParsingDbConnectionString { get; set; }

        /// <summary>
        /// Production DB connection string
        /// </summary>
        public static string ProductionDbConnectionString { get; set; }

        private static string _title;

        public static string Title
        {
            get => _title;
            set
            {
                _title = value;
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                    Console.Title = value;
            }
        }

        /// @author Yevgeniy Cherdantsev
        /// @date 25.02.2020 10:17:01
        /// <summary>
        /// Parser setting class with parser properties
        /// </summary>
        public sealed class ParserSettings
        {
            /// <summary>
            /// Parser name (depends on parser classname)
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Number of working threads (affects speed)
            /// </summary>
            public int Threads { get; set; }

            /// <summary>
            /// Link to the source that gonna be parsed
            /// </summary>
            public string Url { get; set; }
        }

        /// @author Yevgeniy Cherdantsev
        /// @date 25.02.2020 10:17:01
        /// <summary>
        /// Downloader setting class with downloader properties
        /// </summary>
        public sealed class DownloaderSettings
        {
            /// <summary>
            /// Downloader name (depends on downloader classname)
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Number of working threads (affects speed)
            /// </summary>
            public int Threads { get; set; }

            /// <summary>
            /// Folder path where documentation are stored
            /// </summary>
            public string Folder { get; set; }
        }
    }
}