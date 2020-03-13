using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using GoszakupParser.Contexts;
using GoszakupParser.Models.ParsingModels;
using GoszakupParser.Models.WebModels;
using NLog;

namespace GoszakupParser.Parsers.WebParsers.AimParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 29.02.2020 12:17:39
    /// @version 1.0
    public class DirectorParser : WebAimParser<DirectorGoszakup>
    {
        public DirectorParser(Configuration.ParserSettings parserSettings) : base(parserSettings)
        {
        }

        protected override Dictionary<string, string> LoadAims()
        {
            var aims = new Dictionary<string, string>();
            using (var participantWebContext = new WebContext<ParticipantGoszakupWeb>())
            {
                var list = participantWebContext.Models.ToList();
                foreach (var participantGoszakupWeb in list)
                    aims.Add(participantGoszakupWeb.Pid.ToString(), participantGoszakupWeb.BiinCompanies.ToString());
            }

            return aims;
        }

        protected override Logger InitLogger()
        {
            return LogManager.GetCurrentClassLogger();
        }

        protected override async Task ParseArray(string[] list, WebProxy webProxy)
        {
            await using var context = new ParserContext<DirectorGoszakup>();
            foreach (var pid in list)
            {
                var response = GetPage($"{Url}/{pid}", webProxy);
                var director = ParseParticipantPage(response);
                director.Bin = long.Parse(Aims[pid]);
                director.Rnn = director.Rnn != 0 ? director.Rnn : null;
                director.Iin = director.Iin != 0 ? director.Iin : null;
                await ProcessObject(director, context);
                Thread.Sleep(1000);
            }
        }

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
                    {
                        director.Iin = iin;
                    }
                }
                else if (item.InnerHtml.Contains(">РНН<"))
                {
                    long.TryParse(item.InnerHtml.Split("<td>")[1].Split("<")[0], out var rnn);
                    director.Rnn = rnn;
                }
                else if (item.InnerHtml.Contains(">ФИО<"))
                {
                    director.Fullname = item.InnerHtml.Split("<td>")[1].Split("<")[0];
                }
            }

            return director;
        }
    }
}