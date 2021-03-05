using System;
using System.Net;
using System.Linq;
using System.Data;
using AngleSharp.Html.Parser;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GoszakupParser.Models.ParsingModels;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace GoszakupParser.Parsers.WebParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 05.03.2021 12:17:39
    /// <summary>
    /// Qualified Parser
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public class QualifiedParser : WebSequentialParser<QualifiedGoszakup>
    {
        /// <inheritdoc />
        public QualifiedParser(Configuration.ParserSettings parserSettings) : base(parserSettings)
        {
        }

        /// <inheritdoc />
        [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
        protected override async Task ParsePage(int page, WebProxy proxy)
        {
            var pageContent = string.Empty;

            for (var i = 0; i < 5; i++)
            {
                pageContent = GetPage($"{Url}{page}", proxy);
                if (pageContent != null)
                    break;
            }

            if (pageContent == null)
                throw new DataException($"Page {page} hasn't been loaded");

            var elements = await ParseQualifiedPage(pageContent);


            elements = elements.Distinct().ToList();
            foreach (var qualifiedGoszakupElement in elements)
            {
                await using var qualifiedGoszakupContext =
                    new GeneralContext<QualifiedGoszakup>(Configuration.ParsingDbConnectionString);
                await ProcessObject(qualifiedGoszakupElement, qualifiedGoszakupContext);
            }

            Logger.Trace($"{page} page has parsed");
        }

        /// <summary>
        /// Parses qualified page 
        /// </summary>
        /// <param name="pageContent">HTML representation of the page</param>
        /// <returns>List of qualified participants</returns>
        /// <exception cref="DataException">If page is empty and has no elements</exception>
        private static async Task<List<QualifiedGoszakup>> ParseQualifiedPage(string pageContent)
        {
            var elements = new List<QualifiedGoszakup>();
            var doc = await new HtmlParser().ParseDocumentAsync(pageContent);

            var items = doc.QuerySelectorAll("tr").ToList();
            items.Remove(items.First());

            if (items.Count == 0)
                throw new DataException("Page is empty");

            foreach (var columns in items.Select(row => row.QuerySelectorAll("td").ToList()))
            {
                var name = columns[1].QuerySelector("strong").InnerHtml;
                var biin = long.Parse(columns[2].InnerHtml);
                var joinedColumnElements = columns[3].InnerHtml.Split(",");
                long.TryParse(joinedColumnElements[0].Replace(" ", string.Empty), out var docNumber);
                DateTime.TryParse(joinedColumnElements[1].Trim(), out var docIssueDate);

                elements.Add(new QualifiedGoszakup
                {
                    Biin = biin, CompanyName = name,
                    DocNumber = docNumber,
                    DocIssueDate = docIssueDate
                });
            }

            return elements;
        }
    }
}