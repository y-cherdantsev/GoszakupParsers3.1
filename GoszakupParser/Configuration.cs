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
        //TODO(Database data provided in configuration)

        /// <summary>
        /// Goszakup authentication bearer token
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