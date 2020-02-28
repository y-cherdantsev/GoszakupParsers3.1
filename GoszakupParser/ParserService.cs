using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoszakupParser.Parsers;
using GoszakupParser.Parsers.SequentialParsers;
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
            var parsersSettings = _configuration.Parsers;
            var parsers = new List<IParser>();
            parsers.Add(new AnnouncementParser(parsersSettings.FirstOrDefault(x => x.Name.Equals("AnnouncementParser")), _configuration.AuthToken));
            parsers.Add(new LotParser(parsersSettings.FirstOrDefault(x => x.Name.Equals("LotParser")), _configuration.AuthToken));
            parsers.Add(new ContractParser(parsersSettings.FirstOrDefault(x => x.Name.Equals("ContractParser")), _configuration.AuthToken));
            parsers.Add(new ParticipantParser(parsersSettings.FirstOrDefault(x => x.Name.Equals("ParticipantParser")), _configuration.AuthToken));
            parsers.Add(new UnscrupulousParser(parsersSettings.FirstOrDefault(x => x.Name.Equals("UnscrupulousParser")), _configuration.AuthToken));
            // await new ContractParser(parsers[1], _configuration.AuthToken).ParseApiAsync();
        }
    }
}