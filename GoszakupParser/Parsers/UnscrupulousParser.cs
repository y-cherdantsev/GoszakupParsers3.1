using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using GoszakupParser.Models;
using NLog;
using NpgsqlTypes;

namespace GoszakupParser.Parsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 25.02.2020 12:10:21
    /// @version 1.0
    /// <summary>
    /// Парсер недобросовестных учатников
    /// </summary>
    public class UnscrupulousParser : Parser
    {
        private string NextUrl { get; set; }
        private int Total { get; set; }

        public UnscrupulousParser(Configuration.ParserSettings parserSettings, string authToken)
        {
            AuthToken = authToken;
            NextUrl = parserSettings.Url;
            var response = GetApiPageResponse(NextUrl);
            Total = JsonSerializer.Deserialize<ApiResponse>(response).total;
        }

        protected override Logger InitLogger()
        {
            return LogManager.GetCurrentClassLogger();
        }

        public override async Task Parse()
        {
            var tasks = new List<Task>();
            while (true)
            {
                var response = GetApiPageResponse(NextUrl);
                if (response == null)
                    break;
                var apiResponse = JsonSerializer.Deserialize<UnscrupulousApiResponse>(response);

                NextUrl = apiResponse.next_page;
                await Task.WhenAll(tasks);
                if (NextUrl == "")
                    break;
                await ProcessObjects(new List<object> {apiResponse.items});
                Console.WriteLine(NextUrl);
            }
        }

        public async override Task ProcessObjects(List<object> entities)
        {
            await using (var unscrupulousGoszakupContext = new UnscrupulousGoszakupContext())
            {
                foreach (UnscrupulousDto entity in entities)
                {
                    await unscrupulousGoszakupContext.UnscrupulousGoszakup.AddAsync(new UnscrupulousGoszakup()
                    {
                        Pid = entity.pid,
                        IndexDate = DateTime.TryParse(entity.index_date, In) ?? null,
                    });
                }

                await unscrupulousGoszakupContext.SaveChangesAsync();
            }
        }
        
        
    }
}