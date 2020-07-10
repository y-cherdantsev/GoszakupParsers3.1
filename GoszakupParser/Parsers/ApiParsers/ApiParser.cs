using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using GoszakupParser.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using RestSharp;

namespace GoszakupParser.Parsers.ApiParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 29.02.2020 11:53:37
    /// <summary>
    /// Parent parser used for creating parsers that getting information using API
    /// </summary>
    /// <typeparam name="TDto">Dto that will be parsed</typeparam>
    /// <typeparam name="TResultModel">Model in which dto will be converted</typeparam>
    public abstract class ApiParser<TDto, TResultModel> : Parser where TResultModel : DbLoggerCategory.Model
    {
        /// <summary>
        /// Authentication bearer token
        /// </summary>
        private string AuthToken { get; set; }

        /// <summary>
        /// Total number of elements that should be parsed (decreasing with each iteration)
        /// </summary>
        protected int Total { get; set; }

        /// <summary>
        /// Generates object of given parser and starts it
        /// </summary>
        /// <param name="parserSettings">Parser settings from configuration</param>
        /// <param name="proxy">Parsing proxy</param>
        /// <param name="authToken">Authentication bearer token</param>
        protected ApiParser(Configuration.ParserSettings parserSettings, WebProxy proxy, string authToken) : base(
            parserSettings, proxy)
        {
            AuthToken = authToken;

            // Get total number of elements from api
            var response = GetApiPageResponse(Url).Result;
            Total = JsonSerializer.Deserialize<ApiResponse<TDto>>(response).total;
        }

        /// <inheritdoc />
        public abstract override Task ParseAsync();

        /// <summary>
        /// Converts general dto object to DB model
        /// </summary>
        /// <param name="dto">Object parsed from api</param>
        /// <returns>DB model</returns>
        protected abstract TResultModel DtoToDb(TDto dto);

        /// <summary>
        /// Processing list of dtos
        /// </summary>
        /// <param name="entities">List of parsed elements</param>
        protected async Task ProcessObjects(IEnumerable<object> entities)
        {
            //TODO(Fix error Подключение не установлено, т.к. конечный компьютер отверг запрос на подключение. 192.168.2.25:5432)

            await using var context = new ParserContext<TResultModel>();
            foreach (TDto dto in entities)
                await ProcessObject(dto, context);
        }

        /// <summary>
        /// Converts dto into model and inserts it into DB
        /// </summary>
        /// <param name="dto">Dto from Api</param>
        /// <param name="context">Parsing DB context</param>
        protected async Task ProcessObject(TDto dto, ParserContext<TResultModel> context)
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
        }

        /// <summary>
        /// Gets json response from api
        /// </summary>
        /// <param name="url">Url that gonna be requested</param>
        /// <param name="delay">Delay between attempts and timeout</param>
        /// <param name="allowedAttempts">Number of allowed requests before exception</param>
        /// <returns>JSON representation of response</returns>
        /// <exception cref="Exception">If number of attempts exceeded</exception>
        // ReSharper disable once CognitiveComplexity
        protected virtual async Task<string> GetApiPageResponse(string url, int delay = 15000, int allowedAttempts = 20)
        {
            var attempts = 0;

            // Loop that sends requests to api till successive result
            while (attempts < allowedAttempts)
            {
                // Creating a client that will send the requst
                var restClient = new RestClient($"https://ows.goszakup.gov.kz/{url}?limit=500")
                    {Proxy = Proxy, Timeout = delay};
                restClient.AddDefaultHeader("Content-Type", "application/json");
                restClient.AddDefaultHeader("Authorization", AuthToken);

                // Sending request and catching all exceptions and known problems
                try
                {
                    var cts = new CancellationTokenSource();
                    var awaitingTask = restClient.ExecuteAsync(new RestRequest(Method.GET), cts.Token);

                    // Some requests can lock parser and even timeout won't help, that's why custom timeout has been implemented
                    var tempDelay = delay;
                    while (true)
                    {
                        Thread.Sleep(50);
                        tempDelay -= 50;
                        if (tempDelay < 0 && !awaitingTask.IsCompleted)
                        {
                            Logger.Warn("Timeout exceeded");
                            cts.Cancel();

                            //If request loading more than delay time, another request starts
                            return await GetApiPageResponse(url, delay);
                        }

                        if (awaitingTask.IsCompleted)
                        {
                            break;
                        }
                    }

                    //After request proceeded checks all known problems, and sends request again if some error occured
                    IRestResponse restResponse;
                    if (awaitingTask.IsCompletedSuccessfully)
                    {
                        restResponse = awaitingTask.Result;
                    }
                    else
                    {
                        ++attempts;
                        Thread.Sleep(delay);
                        Logger.Warn(awaitingTask.Exception, $"Tried {attempts} times");
                        continue;
                    }

                    switch (restResponse.StatusCode)
                    {
                        case HttpStatusCode.Forbidden:
                        case HttpStatusCode.InternalServerError:
                            ++attempts;
                            Thread.Sleep(delay);
                            Logger.Warn($"{attempts} times, {restResponse.Content}");
                            continue;
                    }

                    if (restResponse.ErrorMessage != null &&
                        restResponse.ErrorMessage.Contains(
                            "An error occurred while sending the request. The response ended prematurely."))
                    {
                        ++attempts;

                        Thread.Sleep(delay);
                        Logger.Warn($"{attempts} times, {restResponse.ErrorMessage}");
                        continue;
                    }

                    if (restResponse.ErrorMessage != null &&
                        restResponse.ErrorMessage.Contains("The operation has timed out."))
                    {
                        ++attempts;

                        Thread.Sleep(delay);
                        Logger.Warn($"{attempts} times, {restResponse.ErrorMessage}");
                        continue;
                    }

                    if (restResponse.ContentLength == 0 ||
                        restResponse.ErrorMessage != null && restResponse.ErrorMessage.Contains(
                            "Подключение не установлено," +
                            " т.к. конечный компьютер отверг запрос на подключение."))
                    {
                        ++attempts;

                        Thread.Sleep(delay);
                        Logger.Warn($"{attempts} times, {restResponse.ErrorMessage}");
                        continue;
                    }

                    // If response message is too short, throws exception
                    var response = restResponse.Content;

                    // After checking, if all is OK returns response
                    Logger.Trace($"{url} - Left:[{Total}]");

                    return response;
                }
                catch (Exception e)
                {
                    Logger.Error(e, $"Link:'{url}'");
                    throw;
                }
            }

            throw new Exception($"Attempts exceeded their maximum({allowedAttempts}) value");
        }
    }
}