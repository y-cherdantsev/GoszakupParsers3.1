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
    public sealed class Configuration
    {
        /// <summary>
        /// Goszakup authentication bearer token
        /// </summary>
        public string AuthToken { get; set; }
        
        public static string AuthTokenStatic { get; set; }

        /// <summary>
        /// Parsers settings
        /// </summary>
        public List<ParserSettings> Parsers { get; set; }
        
        public static List<ParserSettings> ParsersStatic { get; set; }

        /// <summary>
        /// Mapped parsers names into parsing DB titles
        /// </summary>
        public Dictionary<string, string> ParserMonitoringNames { get; set; }

        /// <summary>
        /// List of connection credentials for DB
        /// </summary>
        public List<DbConnectionCredential> DbConnectionCredentials { get; set; }

        /// \todo(Make configuration static)
        public static List<DbConnectionCredential> DbConnectionCredentialsStatic { get; set; }

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


        public sealed class DbConnectionCredential
        {
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public string Title { get; set; }
            public string Address { get; set; }
            public int Port { get; set; }
            public string Name { get; set; }
            public string SearchPath { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}