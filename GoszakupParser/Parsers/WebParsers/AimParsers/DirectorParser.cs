using System.Net;
using System.Linq;
using System.Threading;
using AngleSharp.Html.Parser;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using System.Collections.Generic;
using GoszakupParser.Models.ParsingModels;
using GoszakupParser.Models.ProductionModels;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace GoszakupParser.Parsers.WebParsers.AimParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 29.02.2020 12:17:39
    /// <summary>
    /// Director Parser
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public sealed class DirectorParser : WebAimParser<DirectorGoszakup>
    {
        /// <inheritdoc />
        public DirectorParser(Configuration.ParserSettings parserSettings) : base(parserSettings)
        {
        }

        /// <inheritdoc />
        protected override Dictionary<string, string> LoadAims()
        {
            using var participantWebContext =
                new GeneralContext<ParticipantGoszakupWeb>(Configuration.ProductionDbConnectionString);
            var list = participantWebContext.Models.ToList();

            return list.ToDictionary(participantGoszakupWeb => participantGoszakupWeb.Pid.ToString(),
                participantGoszakupWeb => participantGoszakupWeb.BiinCompanies.ToString());
        }

        /// <inheritdoc />
        protected override async Task ParseAim(string aim, WebProxy webProxy)
        {
            await using var context = new GeneralContext<DirectorGoszakup>(Configuration.ProductionDbConnectionString);
            var response = GetPage($"{Url}/{aim}", webProxy);
            lock (Lock)
            {
                --Total;
                Logger.Trace($"{LogMessage()}");
            }

            var director = ParseParticipantPage(response);
            director.Bin = long.Parse(Aims[aim]);
            director.Rnn = director.Rnn != 0 ? director.Rnn : null;
            director.Iin = director.Iin != 0 ? director.Iin : null;
            await ProcessObject(director, context);
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Parses page and gets Director goszakup from it
        /// </summary>
        /// <param name="page">html page</param>
        /// <returns>DirectorGoszakup model</returns>
        private static DirectorGoszakup ParseParticipantPage(string page)
        {
            var director = new DirectorGoszakup();
            var doc = new HtmlParser().ParseDocument(page);

            var items = doc.QuerySelectorAll("tr");
            foreach (var item in items)
            {
                if (item.InnerHtml.Contains(">ИИН<"))
                {
                    if (long.TryParse(item.InnerHtml.Split("<td>")[1].Split("<")[0], out var iin))
                        director.Iin = iin;
                }
                else if (item.InnerHtml.Contains(">РНН<"))
                {
                    long.TryParse(item.InnerHtml.Split("<td>")[1].Split("<")[0], out var rnn);
                    director.Rnn = rnn;
                }
                else if (item.InnerHtml.Contains(">ФИО<"))
                    director.Fullname = item.InnerHtml.Split("<td>")[1].Split("<")[0];
            }

            return director;
        }
    }
}