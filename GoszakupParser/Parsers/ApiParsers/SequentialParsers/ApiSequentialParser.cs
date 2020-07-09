using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using GoszakupParser.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Npgsql;

namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 28.02.2020 14:51:37
    /// <summary>
    /// Parent parser used for creating parsers that getting information from api sequentially
    /// </summary>
    /// <typeparam name="TDto">Dto that will be parsed</typeparam>
    /// <typeparam name="TResultModel">Model in which dto will be converted</typeparam>
    public abstract class ApiSequentialParser<TDto, TResultModel> : ApiParser<TDto, TResultModel>
        where TResultModel : DbLoggerCategory.Model, new()
    {
        /// <summary>
        /// Constructor for creating API sequential parsers
        /// </summary>
        /// <param name="parserSettings">Parser settings from configuration</param>
        /// <param name="proxy">Authentication bearer token</param>
        /// <param name="authToken">Parsing proxy</param>
        protected ApiSequentialParser(Configuration.ParserSettings parserSettings, WebProxy proxy, string authToken) :
            base(
                parserSettings, proxy, authToken)
        {
        }

        /// <inheritdoc />
        // ReSharper disable once CognitiveComplexity
        public override async Task ParseAsync()
        {
            var tasks = new List<Task>();
            Logger.Info("Starting parsing");
            while (true)
            {
                var response = GetApiPageResponse(Url).Result;
                try
                {
                    if (response == null)
                        break;

                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<TDto>>(response);
                    Url = apiResponse?.next_page;
                    if (Url == null)
                        Console.WriteLine("Url is null");

                    await Task.WhenAll(tasks);
                    tasks.Clear();

                    for (var i = 0; i < Threads; i++)
                        tasks.Add(ProcessObjects(DivideList((IEnumerable<object>) apiResponse?.items, i)));

                    if (Url != "") continue;
                    await Task.WhenAll(tasks);
                    foreach (var task in tasks.Where(task => task.IsFaulted))
                    {
                        Logger.Error(task.Exception);
                    }

                    if (tasks.Any(x => x.IsFaulted))
                        throw new Exception("Parsing hasn't been done");
                    tasks.Clear();
                    break;
                }
                catch (Exception e)
                {
                    Logger.Error(e, response);
                }
            }

            Logger.Info("Parsing done");
        }

        /// <summary>
        /// Converts dtos into models and inserts them into DB
        /// </summary>
        /// <param name="entities">List of parsed elements</param>
        private async Task ProcessObjects(IEnumerable<object> entities)
        {
            //TODO(Fix error Подключение не установлено, т.к. конечный компьютер отверг запрос на подключение. 192.168.2.25:5432)

            await using var context = new ParserContext<TResultModel>();
            foreach (TDto dto in entities)
            {
                try
                {
                    var model = DtoToDb(dto);
                    context.Models.Add(model);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException is NpgsqlException)
                        Logger.Trace($"Message: {e.InnerException?.Data["MessageText"]}; " +
                                     $"{e.InnerException?.Data["Detail"]} " +
                                     $"{e.InnerException?.Data["SchemaName"]}.{e.InnerException?.Data["TableName"]}");
                    else
                        throw;
                }

                lock (Lock)
                {
                    --Total;
                }
            }
        }
    }
}