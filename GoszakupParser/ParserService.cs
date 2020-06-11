using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using GoszakupParser.Parsers;
using GoszakupParser.Parsers.ApiParsers.AimParsers;
using GoszakupParser.Parsers.ApiParsers.SequentialParsers;
using GoszakupParser.Parsers.WebParsers.AimParsers;
using NLog;

namespace GoszakupParser
{
    /// @author Yevgeniy Cherdantsev
    /// @date 26.02.2020 18:18:55
    /// @version 1.0
    /// <summary>
    /// Proceeding of arguments and general logic
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
            ParserMonitoringNames.Add("RnuReferenceParser", "RnuReferenceGoszakup");
        }

        public async Task StartParsing()
        {
            _logger.Info("Started parsing service!");
            var parsersSettings = _configuration.Parsers;


            var avaliable = new List<string>();
            await using (var parserMonitoringContext = new ParserMonitoringContext())
            {
                var avaliableFromContext = parserMonitoringContext.ParserMonitorings
                    .Where(x => (x.Parsed == false && x.Active == true)).ToList();
                avaliable.AddRange(avaliableFromContext.Select(parserMonitoring => parserMonitoring.Name));
            }

            foreach (var arg in Args)
            {
                try
                {
                    Parser parser = null;
                    if (!avaliable.Contains(ParserMonitoringNames[arg]))
                    {
                        _logger.Warn($"Parser '{arg}' hasn't been migrated yet");
                        continue;
                    }

                    var proxy = new WebProxy(_configuration.Proxy.Address, true)
                    {
                        Credentials = new NetworkCredential
                            {UserName = _configuration.Proxy.UserName, Password = _configuration.Proxy.Password}
                    };
                    switch (arg)
                    {
                        case "AnnouncementParser":
                            parser = new AnnouncementParser(parsersSettings.FirstOrDefault(x => x.Name.Equals(arg)),
                                _configuration.AuthToken,proxy);
                            break;
                        case "LotParser":
                            parser = new LotParser(parsersSettings.FirstOrDefault(x => x.Name.Equals(arg)),
                                _configuration.AuthToken,proxy);
                            break;
                        case "ContractParser":
                            parser = new ContractParser(parsersSettings.FirstOrDefault(x => x.Name.Equals(arg)),
                                _configuration.AuthToken,proxy);
                            break;
                        case "ParticipantParser":
                            parser = new ParticipantParser(parsersSettings.FirstOrDefault(x => x.Name.Equals(arg)),
                                _configuration.AuthToken,proxy);
                            break;
                        case "UnscrupulousParser":
                            parser = new UnscrupulousParser(parsersSettings.FirstOrDefault(x => x.Name.Equals(arg)),
                                _configuration.AuthToken,proxy);
                            break;
                        case "DirectorParser":
                            parser = new DirectorParser(parsersSettings.FirstOrDefault(x => x.Name.Equals(arg)),proxy);
                            break;
                        case "RnuReferenceParser":
                            parser = new RnuReferenceParser(parsersSettings.FirstOrDefault(x => x.Name.Equals(arg)),
                                _configuration.AuthToken,proxy);
                            break;
                        default:
                            _logger.Warn($"Can't find such parser '{arg}'");
                            continue;
                    }

                    await using var parserMonitoringContext = new ParserMonitoringContext();
                    await parser.ParseAsync();
                    var parsed =
                        parserMonitoringContext.ParserMonitorings.FirstOrDefault(x =>
                            x.Name.Equals(ParserMonitoringNames[arg]));
                    if (parsed != null)
                    {
                        parsed.LastParsed = DateTime.Now;
                        parsed.Parsed = true;
                        parserMonitoringContext.ParserMonitorings.Update(parsed);
                    }

                    await parserMonitoringContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    _logger.Error(e);
                }
            }
        }
    }
}