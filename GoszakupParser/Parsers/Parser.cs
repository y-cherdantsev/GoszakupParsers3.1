using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;

namespace GoszakupParser.Parsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 25.02.2020 10:53:43
    /// @version 1.0
    /// <summary>
    /// Parsing abstract class
    /// </summary>
    public abstract class Parser
    {
        protected readonly Logger Logger;
        protected string Url { get; set; }
        protected int Threads { get; set; }

        protected readonly object Lock = new object();

        protected Parser(Configuration.ParserSettings parserSettings)
        {
            Logger = InitLogger();
            Threads = parserSettings.Threads;
            Url = parserSettings.Url;
        }

        protected abstract Logger InitLogger();
        public abstract Task ParseAsync();

        protected string[] DivideList(List<string> list, int i)
        {
            return list.Where(x => long.Parse(x) % Threads == i).ToArray();
        }
    }
}