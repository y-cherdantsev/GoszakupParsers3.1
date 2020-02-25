using System.Collections.Generic;

namespace GoszakupParser
{
    /// @author Yevgeniy Cherdantsev
    /// @date 25.02.2020 10:17:01
    /// @version 1.0
    /// <summary>
    /// Configuration class
    /// </summary>
    public class Configuration
    {
        public string DbHost { get; set; }
        public int DbPort { get; set; }
        public string DbUser { get; set; }
        public string DbPassword { get; set; }
        public string DbName { get; set; }
        public string DbScheme { get; set; }
        public string AuthToken { get; set; }
        public List<ParserSettings> Parsers { get; set; }

        public class ParserSettings
        {
            public string Name { get; set; }
            public int NumberOfDbConnections { get; set; }
            public string Url { get; set; }
            public string Table { get; set; }
            public string AdditionalTable { get; set; }
        }
    }
}