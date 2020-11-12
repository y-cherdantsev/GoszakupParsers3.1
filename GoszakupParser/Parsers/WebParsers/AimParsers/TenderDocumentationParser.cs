using System;
using System.Net;
using System.Linq;
using AngleSharp.Html.Parser;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using System.Collections.Generic;
using GoszakupParser.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using GoszakupParser.Models.ParsingModels;
using GoszakupParser.Contexts.ProductionContexts;

// ReSharper disable StringLiteralTypo

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
            var productionTenderContext = new ProductionTenderContext();
            return productionTenderContext.Announcements.FromSqlRaw(
                    "SELECT * FROM adata_tender.announcements WHERE source_id=2 AND id NOT IN (SELECT DISTINCT announcement_id FROM adata_tender.announcement_documentations)")
                .Select(x => new KeyValuePair<string, string>(x.SourceNumber, x.Id.ToString()))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        /// <inheritdoc />
        [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
        protected override async Task ParseAim(string aim, WebProxy webProxy)
        {
            await Task.Delay(1000);
            
            await using var tenderDocumentContext =
                new GeneralContext<TenderDocumentGoszakup>(Configuration.ParsingDbConnectionString);

            lock (Lock)
            {
                --Total;
                Logger.Trace($"{LogMessage()}");
            }

            var tenderDocuments = ParseTender(aim, webProxy);
            foreach (var tenderDocument in tenderDocuments)
                await ProcessObject(new TenderDocumentGoszakup
                {
                    Identity = tenderDocument.Identity,
                    Link = tenderDocument.Link,
                    Relevance = DateTime.Now,
                    Title = tenderDocument.Title,
                    Type = tenderDocument.Type,
                    Number = tenderDocument.Number
                }, tenderDocumentContext);
        }

        /// <summary>
        /// Parse defined tender documentation
        /// </summary>
        /// <param name="announcementNumber">Announcement number</param>
        /// <param name="proxy">Proxy for parsing documentation</param>
        /// <returns>List of tender documentation</returns>
        [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
        private IEnumerable<TenderDocumentDto> ParseTender(string announcementNumber, WebProxy proxy)
        {
            var announcementNumberShort = announcementNumber.Split("-").First();
            var announcementPage = GetPage($"{Url}/{announcementNumberShort}?tab=documents", proxy);
            var announcementDom = new HtmlParser().ParseDocument(announcementPage);
            var announcementTable = announcementDom
                .QuerySelectorAll("table")
                .FirstOrDefault(x =>
                    x.ClassName != null && x.ClassName.Equals("table table-bordered table-hover table-striped"));
            var announcementTableRows = announcementTable?.QuerySelectorAll("tr");

            if (announcementTableRows == null) return new List<TenderDocumentDto>();

            var announcementTableRowsWithLink =
                announcementTableRows.Where(x => x.InnerHtml.Contains("https://v3bl.goszakup.gov.kz/"));
            var documents = (from rowWithLink in announcementTableRowsWithLink
                let type = rowWithLink.QuerySelectorAll("td").First()
                    .QuerySelector("a")
                    .InnerHtml.Replace("\n", string.Empty)
                    .Replace("\r", string.Empty)
                    .Trim()
                let link = rowWithLink.QuerySelectorAll("td").First().QuerySelector("a").GetAttribute("href")
                let title = link.Split("/").LastOrDefault()
                select new TenderDocumentDto
                {
                    Identity = "Announcement",
                    Type = type,
                    Title = title,
                    Number = announcementNumber,
                    Link = link
                }).ToList();

            var announcementTableRowsWithInnerLinks =
                announcementTableRows.Where(x => x.InnerHtml.Contains("</button>"));


            foreach (var rowWithInnerLinks in announcementTableRowsWithInnerLinks)
            {
                var columns = rowWithInnerLinks.QuerySelectorAll("td");
                var type = columns.First().InnerHtml.Replace("\n", string.Empty).Replace("\r", string.Empty).Trim();
                var linkNumber = columns[2].QuerySelector("button").GetAttribute("onclick").Split(",")[1].Split(")").First();
                var innerDocumentsPage =
                    GetPage(
                        $"https://www.goszakup.gov.kz/ru/announce/actionAjaxModalShowFiles/{announcementNumberShort}/{linkNumber}",
                        proxy);
                var innerDocumentsDom = new HtmlParser().ParseDocument(innerDocumentsPage);
                var innerTables = innerDocumentsDom.QuerySelectorAll("table");
                foreach (var table in innerTables)
                {
                    var innerRows = table.QuerySelectorAll("tr");
                    if (innerRows.First().QuerySelectorAll("th").First().InnerHtml.Contains("Номер лота"))
                    {
                        documents.AddRange(from innerRow in innerRows
                            where innerRow.InnerHtml.Contains("https://v3bl.goszakup.gov.kz/")
                            select innerRow.QuerySelectorAll("td")
                            into innerColumns
                            let link = innerColumns[1].QuerySelector("a").GetAttribute("href")
                            let title = innerColumns[1].QuerySelector("a").InnerHtml.Trim()
                            let lotNumber = innerColumns.First().InnerHtml.Trim()
                            select new TenderDocumentDto
                            {
                                Identity = "Lot",
                                Number = lotNumber,
                                Type = type,
                                Title = title,
                                Link = link
                            });
                    }
                    else
                    {
                        var linkObject = innerRows[1].QuerySelector("td").QuerySelector("a");
                        if (linkObject == null) continue;
                        var link = linkObject.GetAttribute("href");
                        var title = linkObject.InnerHtml.Trim();
                        var document = new TenderDocumentDto
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