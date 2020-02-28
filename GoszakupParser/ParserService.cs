using System.Threading.Tasks;
using GoszakupParser.Parsers;
using NLog;

namespace GoszakupParser
{

    /// @author Yevgeniy Cherdantsev
    /// @date 26.02.2020 18:18:55
    /// @version 1.0
    /// <summary>
    /// INPUT
    /// </summary>
    public class ParserService
    {
        private readonly Configuration _configuration;
        private readonly Logger _logger;
        public ParserService(Configuration configuration)
        {
            _configuration = configuration;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task StartParsing()
        {
            _logger.Info("Started parsing service!");
            var parsers = _configuration.Parsers;
            // foreach (var parser in parsers)
            // {
            //     
            // }
            await new UnscrupulousParser(parsers[4], _configuration.AuthToken).ParseApiAsync();
        }
    }
}