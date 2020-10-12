using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoszakupParser.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

// ReSharper disable CommentTypo

// ReSharper disable once IdentifierTypo
namespace GoszakupParser.Parsers.GraphQlParsers.SequentialParsers
{
    public abstract class GraphQlSequentialParser<TDto> : GraphQlParser<TDto>
    {
        /// <summary>
        /// Last parsed Id
        /// </summary>
        private int _lastId;

        /// <summary>
        /// Constructor for creating GraphQl sequential parsers
        /// </summary>
        /// <param name="parserSettings">Parser settings from configuration</param>
        /// <param name="authToken">Parsing proxy</param>
        protected GraphQlSequentialParser(Configuration.ParserSettings parserSettings, string authToken) : base(
            parserSettings, authToken)
        {
            // Get total number of elements from GraphQl api
            // ReSharper disable once VirtualMemberCallInConstructor

            var response = GetGraphQlResponse(GetQuery(0)).Result;
            Total = JsonSerializer.Deserialize<GraphQlResponse<TDto>>(response).extensions.pageInfo.totalCount;
        }

        /// <summary>
        /// Get query for next request
        /// </summary>
        /// <param name="after">Id that would be used as after parameter</param>
        /// <returns></returns>
        protected abstract string GetQuery(int after);

        /// <inheritdoc />
        // ReSharper disable once CognitiveComplexity
        public override async Task ParseAsync()
        {
            var tasks = new List<Task>();
            Logger.Info("Starting parsing");

            while (true)
            {
                var query = GetQuery(_lastId);

                // Gets json response
                var response = GetGraphQlResponse(query).Result;

                try
                {
                    if (response == null)
                        break;

                    // If response is too short to be true (Used to find and fix new errors if occured)
                    if (response.Length < 500)
                        throw new Exception($"Unknown error, Url: {Url}; Response: {response}");

                    // Deserializes json into api response object
                    response = response.Replace(typeof(TDto).Name.Replace("Dto", string.Empty), "items");
                    var graphQlResponse = JsonConvert.DeserializeObject<GraphQlResponse<TDto>>(response);

                    Logger.Trace(
                        $"LastId: {graphQlResponse.extensions.pageInfo.lastId} - {LogMessage(graphQlResponse.data.items)}");

                    _lastId = graphQlResponse.extensions.pageInfo.lastId;

                    // Waits when previous processing ends
                    await Task.WhenAll(tasks);
                    tasks.Clear();

                    // Processing objects and loading them into DB
                    if (graphQlResponse.data.items != null)
                    {
                        for (var i = 0; i < Threads; i++)
                            tasks.Add(ProcessObjects(DivideList((IEnumerable<object>) graphQlResponse.data.items, i)));

                        lock (Lock)
                        {
                            Total -= graphQlResponse.data.items.Count;
                        }
                    }

                    // If graphQlResponse has no next page then finish parsing
                    if (graphQlResponse.extensions.pageInfo.hasNextPage &&
                        !StopCondition(graphQlResponse.data.items)) continue;

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
                    Logger.Error($"{e} | LastId:{_lastId}");
                }
            }

            Logger.Info("Parsing done");
        }

        /// <inheritdoc />
        protected abstract override Task ProcessObjects(IEnumerable<object> entities);

        /// <inheritdoc />
        protected abstract override Task ProcessObject(TDto dto, DbContext context);
    }
}