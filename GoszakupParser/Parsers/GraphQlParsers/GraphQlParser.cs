using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestSharp;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace GoszakupParser.Parsers.GraphQlParsers
{
    public abstract class GraphQlParser<TDto> : Parser
    {
        /// <summary>
        /// Authentication bearer token
        /// </summary>
        private string AuthToken { get; }

        protected GraphQlParser(Configuration.ParserSettings parserSettings, string authToken) : base(parserSettings)
        {
            AuthToken = authToken;
        }

        /// <inheritdoc />
        public abstract override Task ParseAsync();

        /// <summary>
        /// Processing list of dtos
        /// </summary>
        /// <param name="entities">List of parsed elements</param>
        protected abstract Task ProcessObjects(IEnumerable<object> entities);

        /// <summary>
        /// Converts dto into model and inserts it into DB
        /// </summary>
        /// <param name="dto">Dto from GraphQl API</param>
        /// <param name="context">Parsing DB context</param>
        protected abstract Task ProcessObject(TDto dto, DbContext context);
        
        /// <summary>
        /// Gets json response from GraphQl API
        /// </summary>
        /// <param name="requestBody">Query string into GraphQl</param>
        /// <param name="delay">Delay between attempts and timeout</param>
        /// <param name="allowedAttempts">Number of allowed requests before exception</param>
        /// <returns>JSON representation of response</returns>
        /// <exception cref="Exception">If number of attempts exceeded</exception>
        // ReSharper disable once CognitiveComplexity
        protected async Task<string> GetGraphQlResponse(string requestBody, int delay = 40000, int allowedAttempts = 25)
        {
            var attempts = 0;
            // Loop that sends requests to api till successive result
            while (attempts < allowedAttempts)
            {
                // Creating a client that will send the request
                var restClient = new RestClient(Url)
                {
                    Proxy = Proxies[0],
                    Timeout = delay,
                    UnsafeAuthenticatedConnectionSharing = true
                };
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", AuthToken);
                request.AddParameter("application/json", requestBody, ParameterType.RequestBody);
                
                // Sending request and catching all exceptions and known problems
                try
                {
                    var cts = new CancellationTokenSource();
                    var awaitingTask = restClient.ExecuteAsync(request, cts.Token);

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
                            return await GetGraphQlResponse(requestBody, delay, allowedAttempts);
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

                    // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
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
                        Logger.Warn($"{attempts} times, {restResponse.ErrorException}");
                        Logger.Warn($"{attempts} times, {restResponse.ErrorException.InnerException}");
                        continue;
                    }

                    if (restResponse.ErrorMessage != null &&
                        restResponse.ErrorMessage.Contains("The operation has timed out."))
                    {
                        ++attempts;

                        Thread.Sleep(delay);
                        Logger.Warn($"{attempts} times, {restResponse.ErrorMessage}");
                        Logger.Warn($"{attempts} times, {restResponse.ErrorException}");
                        Logger.Warn($"{attempts} times, {restResponse.ErrorException.InnerException}");
                        continue;
                    }

                    if (restResponse.ContentLength == 0 ||
                        restResponse.ErrorMessage != null && restResponse.ErrorMessage.Contains(
                            "Подключение не установлено," +
                            " т.к. конечный компьютер отверг запрос на подключение."))
                    {
                        ++attempts;

                        Thread.Sleep(delay);
                        Logger.Trace($"{attempts} times, {restResponse.ErrorMessage}");
                        continue;
                    }

                    // If response message is too short, throws exception
                    var response = restResponse.Content;

                    // After checking, if all is OK returns response
                    return response;
                }
                catch (Exception e)
                {
                    Logger.Error(e, e.Message);
                    throw;
                }
            }

            throw new Exception($"Attempts exceeded their maximum({allowedAttempts}) value");
        }
    }
}