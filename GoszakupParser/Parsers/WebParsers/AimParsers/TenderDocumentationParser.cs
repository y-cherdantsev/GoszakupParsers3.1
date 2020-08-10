using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using GoszakupParser.Contexts;
using GoszakupParser.Models.ParsingModels;
using GoszakupParser.Models.WebModels;

// ReSharper disable CognitiveComplexity
// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

namespace GoszakupParser.Parsers.WebParsers.AimParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 29.02.2020 12:17:39
    /// <summary>
    /// Tender Documentation Parser
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public class TenderDocumentationParser : WebAimParser<TenderDocumentGoszakup>
    {
        /// <inheritdoc />
        public TenderDocumentationParser(Configuration.ParserSettings parserSettings) : base(parserSettings)
        {
        }

        /// <inheritdoc />
        protected override Dictionary<string, string> LoadAims()
        {
            // var announcementContext = new ParserContext<AnnouncementGoszakup>();
            var statusesContext = new AdataContext<StatusWeb>(DatabaseConnections.WebAdataTender);
            var combinedIds = statusesContext.Models.ToDictionary(x => x.Id, x => x.CombinedId ?? 0);
            statusesContext.Dispose();
            var announcementContext = new AdataContext<AdataAnnouncementWeb>(DatabaseConnections.ParsingAvroradata);
            var aims = announcementContext.Models
                .AsEnumerable()
                .Where(x => combinedIds[x.SourceId] < (long) 3 && x.SourceId == 2
                ).Select(x => new {x.SourceNumber, x.Id})
                .ToDictionary(x => x.SourceNumber, y => y.Id.ToString());
            announcementContext.Dispose();
            return aims;
        }

        /// <inheritdoc />
        protected override async Task ParseArray(IEnumerable<string> list, WebProxy webProxy)
        {
            await Task.Delay(500);
            foreach (var announcement in list)
            {
                var docs = await GetAnnouncementDocuments(announcement, webProxy);
                var context = new AdataContext<TenderDocumentGoszakup>(DatabaseConnections.ParsingAvroradata);
                // ReSharper disable once AccessToDisposedClosure
                foreach (var tenderDocumentGoszakup in docs)
                    await ProcessObject(tenderDocumentGoszakup, context);
                lock (Lock)
                {
                    --Total;
                    Logger.Trace($"{LogMessage()}");
                }
            }
        }

        /// <summary>
        /// Parses documents and returns list of docs for given announcement
        /// </summary>
        /// <param name="announcementNumber">Announcement Number that will be parsed</param>
        /// <param name="webProxy">Proxy for requests</param>
        /// <returns></returns>
        private async Task<IEnumerable<TenderDocumentGoszakup>> GetAnnouncementDocuments(string announcementNumber,
            WebProxy webProxy)
        {
            // Console.WriteLine(announcementNumber);
            await Task.Delay(0);
            var announcementNumberShort = announcementNumber.Split("-")[0];
            var documents = new List<TenderDocumentGoszakup>();
            var announcementPage = GetPage($"{Url}/{announcementNumberShort}?tab=documents", webProxy);
            var announcementDom = new HtmlParser().ParseDocument(announcementPage);
            var announcementTable = announcementDom
                .QuerySelectorAll("table")
                .FirstOrDefault(x =>
                    x.ClassName != null && x.ClassName.Equals("table table-bordered table-hover table-striped"));
            var announcementTableRows = announcementTable?.QuerySelectorAll("tr");

            if (announcementTableRows == null)
            {
                return new List<TenderDocumentGoszakup>();
            }

            var announcementTableRowsWithLink =
                announcementTableRows.Where(x => x.InnerHtml.Contains("https://v3bl.goszakup.gov.kz/"));
            foreach (var rowWithLink in announcementTableRowsWithLink)
            {
                var type = rowWithLink.QuerySelectorAll("td")[0].QuerySelector("a").InnerHtml
                    .Replace("\n", string.Empty)
                    .Replace("\r", string.Empty).Trim();
                var link = rowWithLink.QuerySelectorAll("td")[0].QuerySelector("a").GetAttribute("href");
                var title = link.Split("/").LastOrDefault();
                var document = new TenderDocumentGoszakup
                {
                    Identity = "Announcement",
                    Number = announcementNumber,
                    Type = type,
                    Title = title,
                    Link = link
                };
                documents.Add(document);
            }

            var announcementTableRowsWithInnerLinks =
                announcementTableRows.Where(x => x.InnerHtml.Contains("</button>"));


            foreach (var rowWithInnerLinks in announcementTableRowsWithInnerLinks)
            {
                var columns = rowWithInnerLinks.QuerySelectorAll("td");
                var type = columns[0].InnerHtml.Replace("\n", string.Empty).Replace("\r", string.Empty).Trim();
                var linkNumber = columns[2].QuerySelector("button").GetAttribute("onclick").Split(",")[1].Split(")")[0];
                var innerDocumentsPage =
                    GetPage(
                        $"https://www.goszakup.gov.kz/ru/announce/actionAjaxModalShowFiles/{announcementNumberShort}/{linkNumber}",
                        webProxy);
                var innerDocumentsDom = new HtmlParser().ParseDocument(innerDocumentsPage);
                var innerTables = innerDocumentsDom.QuerySelectorAll("table");
                foreach (var table in innerTables)
                {
                    var innerRows = table.QuerySelectorAll("tr");
                    if (innerRows[0].QuerySelectorAll("th")[0].InnerHtml.Contains("Номер лота"))
                    {
                        foreach (var innerRow in innerRows)
                        {
                            if (innerRow.InnerHtml.Contains("https://v3bl.goszakup.gov.kz/"))
                            {
                                var innerColumns = innerRow.QuerySelectorAll("td");
                                var link = innerColumns[1].QuerySelector("a").GetAttribute("href");
                                var title = innerColumns[1].QuerySelector("a").InnerHtml.Trim();
                                var lotNumber = innerColumns[0].InnerHtml.Trim();
                                var document = new TenderDocumentGoszakup
                                {
                                    Identity = "Lot",
                                    Number = lotNumber,
                                    Type = type,
                                    Title = title,
                                    Link = link
                                };
                                documents.Add(document);
                            }
                        }
                    }
                    else
                    {
                        var linkObject = innerRows[1].QuerySelector("td").QuerySelector("a");
                        if (linkObject == null) continue;
                        var link = linkObject.GetAttribute("href");
                        var title = linkObject.InnerHtml.Trim();
                        var document = new TenderDocumentGoszakup
                        {
                            Identity = "Announcement",
                            Number = announcementNumber,
                            Type = type,
                            Title = title,
                            Link = link
                        };
                        documents.Add(document);
                    }
                }
            }

            return documents;
        }
    }
}