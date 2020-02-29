using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using GoszakupParser.Models;
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
        private Dictionary<string, string> ParserMonitoringNames { get; set; } = new Dictionary<string, string>();

        public ParserService(Configuration configuration)
        {
            _configuration = configuration;
            _logger = LogManager.GetCurrentClassLogger();
            ParserMonitoringNames.Add("UnscrupulousGoszakup", "UnscrupulousParser");
            ParserMonitoringNames.Add("ParticipantGoszakup", "ParticipantParser");
            ParserMonitoringNames.Add("LotGoszakup", "LotParser");
            ParserMonitoringNames.Add("ContractGoszakup", "ContractParser");
            ParserMonitoringNames.Add("AnnouncementGoszakup", "AnnouncementParser");
        }

        public async Task StartParsing()
        {
            _logger.Info("Started parsing service!");
            var parsersSettings = _configuration.Parsers;
            var parsers = new List<IParser>();
            parsers.Add(new AnnouncementParser(parsersSettings.FirstOrDefault(x => x.Name.Equals("AnnouncementParser")),
                _configuration.AuthToken));
            parsers.Add(new LotParser(parsersSettings.FirstOrDefault(x => x.Name.Equals("LotParser")),
                _configuration.AuthToken));
            parsers.Add(new ContractParser(parsersSettings.FirstOrDefault(x => x.Name.Equals("ContractParser")),
                _configuration.AuthToken));
            parsers.Add(new ParticipantParser(parsersSettings.FirstOrDefault(x => x.Name.Equals("ParticipantParser")),
                _configuration.AuthToken));
            parsers.Add(new UnscrupulousParser(parsersSettings.FirstOrDefault(x => x.Name.Equals("UnscrupulousParser")),
                _configuration.AuthToken));

            var available = new List<ParserMonitoring>();
            using (var monitoringContext = new ParserMonitoringContext())
            {
                available = monitoringContext.ParserMonitorings.Where(x => (x.Active == true && x.Parsed == false))
                    .ToList();
            }

            var parserName = "LotGoszakup";
            if (available.FirstOrDefault(x => x.Name.Equals(parserName)) != null)
            {
                await new LotParser(parsersSettings.FirstOrDefault(x => x.Name.Equals(ParserMonitoringNames[parserName])),
                    _configuration.AuthToken).ParseAsync();
            }
        }
    }
}