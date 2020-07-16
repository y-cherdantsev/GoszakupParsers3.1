using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoszakupParser.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// ReSharper disable CommentTypo

// ReSharper disable once IdentifierTypo
namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 28.02.2020 14:51:37
    /// <summary>
    /// Parent parser used for creating parsers that gets information from api sequentially
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
        /// <param name="authToken">Parsing proxy</param>
        protected ApiSequentialParser(Configuration.ParserSettings parserSettings, string authToken) :
            base(
                parserSettings, authToken)
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
                // Gets json response
                var response = GetApiPageResponse(Url).Result;
                try
                {
                    if (response == null)
                        break;

                    // If response is too short to be true (Used to find and fix new errors if occured)
                    if (response.Length < 500)
                        throw new Exception($"Unknown error, Response: {response}");

                    // Deserializes json into api response object
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<TDto>>(response);
                    Url = apiResponse?.next_page;
                    if (Url == null)
                        Logger.Warn("Url is null");

                    // Waits when previous processing ends
                    await Task.WhenAll(tasks);
                    tasks.Clear();

                    // Processing objects and loading them into DB
                    for (var i = 0; i < Threads; i++)
                        tasks.Add(ProcessObjects(DivideList((IEnumerable<object>) apiResponse?.items, i)));

                    lock (Lock)
                    {
                        if (apiResponse != null) Total -= apiResponse.items.Count;
                    }

                    // If Url == "" then finish parsing
                    if (Url != "") continue;

                    await Task.WhenAll(tasks);
                    foreach (var task in tasks.Where(task => task.IsFaulted))
                        Logger.Error(task.Exception);

                    if (tasks.Any(x => x.IsFaulted))
                        throw new Exception("Parsing hasn't been done");
                    tasks.Clear();
                    break;
                }
                catch (Exception e)
                {
                    Logger.Error($"{e} | {response}");
                }
            }

            Logger.Info("Parsing done");
        }
    }
}