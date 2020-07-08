using System.Collections.Generic;

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
        /// Database host address
        /// </summary>
        public string DbHost { get; set; }

        /// <summary>
        /// Database port
        /// </summary>
        public int DbPort { get; set; }

        /// <summary>
        /// Database username
        /// </summary>
        public string DbUser { get; set; }

        /// <summary>
        /// Database password
        /// </summary>
        public string DbPassword { get; set; }

        /// <summary>
        /// Database name
        /// </summary>
        public string DbName { get; set; }

        /// <summary>
        /// Database parsing scheme
        /// </summary>
        public string DbScheme { get; set; }

        /// <summary>
        /// Goszakup auth bearer token
        /// </summary>
        public string AuthToken { get; set; }

        /// <summary>
        /// Parsers settings
        /// </summary>
        public List<ParserSettings> Parsers { get; set; }
        
        /// <summary>
        /// Mapped parsers names into parsing DB titles
        /// </summary>
        public Dictionary<string, string> ParserMonitoringNames { get; set; }

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
    }
}