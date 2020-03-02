using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using GoszakupParser.Models;
using GoszakupParser.Parsers;
using GoszakupParser.Parsers.ApiParsers.SequentialParsers;
using GoszakupParser.Parsers.WebParsers;
using NLog;
using NLog.Fluent;

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
        private string[] Args { get; set; }
        private Dictionary<string, string> ParserMonitoringNames { get; set; } = new Dictionary<string, string>();

        public ParserService(Configuration configuration, string[] args)
        {
            Args = args;
            _configuration = configuration;
            _logger = LogManager.GetCurrentClassLogger();
            ParserMonitoringNames.Add("UnscrupulousParser", "UnscrupulousGoszakup");
            ParserMonitoringNames.Add("ParticipantParser", "ParticipantGoszakup");
            ParserMonitoringNames.Add("LotParser", "LotGoszakup");
            ParserMonitoringNames.Add("ContractParser", "ContractGoszakup");
            ParserMonitoringNames.Add("AnnouncementParser", "AnnouncementGoszakup");
            ParserMonitoringNames.Add("DirectorParser", "DirectorGoszakup");
        }

        public async Task StartParsing()
        {
            _logger.Info("Started parsing service!");
            var parsersSettings = _configuration.Parsers;
            var parsers = new List<Parser>();
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
            var avaliable = new List<string>();
            await using (var parserMonitoringContext = new ParserMonitoringContext())
            {
                var avaliableFromContext = parserMonitoringContext.ParserMonitorings.Where(x => (x.Parsed == false && x.Active == true)).ToList();
                avaliable.AddRange(avaliableFromContext.Select(parserMonitoring => parserMonitoring.Name));
            }

            foreach (var arg in Args)
            {
                try
                {
                    var parser = parsers.FirstOrDefault(x => x.GetType().Name.Equals(arg));
                    if (parser != null && !avaliable.Contains(ParserMonitoringNames[arg]))
                    {
                        _logger.Warn($"Parser '{arg}' hasn't been migrated yet");
                        continue;
                    }
                    if (parser != null)
                    {
                        await using var parserMonitoringContext = new ParserMonitoringContext();
                        await parser.ParseAsync();
                        var parsed = parserMonitoringContext.ParserMonitorings.FirstOrDefault(x => x.Name.Equals(ParserMonitoringNames[arg]));
                        if (parsed != null)
                        {
                            parsed.LastParsed = DateTime.Now;
                            parsed.Parsed = true;
                            parserMonitoringContext.ParserMonitorings.Update(parsed);
                        }

                        await parserMonitoringContext.SaveChangesAsync();
                    }
                    else
                    {
                        _logger.Warn($"Can't find such parser '{arg}'");
                    }
                }
                catch (Exception e)
                {
                    _logger.Error(e.StackTrace);
                }
            }
        }
    }
}