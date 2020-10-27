using Npgsql;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Net.Security;
using GoszakupParser.Models;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using Microsoft.EntityFrameworkCore;

// ReSharper disable CommentTypo
// ReSharper disable InconsistentlySynchronizedField

// ReSharper disable once IdentifierTypo
namespace GoszakupParser.Parsers.WebParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 16.07.2020 17:57:58
    /// <summary>
    /// Parent parser used for creating parsers that gets information from web
    /// </summary>
    /// <typeparam name="TModel">Model that will be parsed and inserted into DB</typeparam>
    public abstract class WebParser<TModel> : Parser where TModel : BaseModel, new()
    {
        /// <summary>
        /// Generates object of given web parser and starts it
        /// </summary>
        /// <param name="parserSettings">Parser settings from configuration</param>
        protected WebParser(Configuration.ParserSettings parserSettings) : base(parserSettings)
        {
        }


        /// <inheritdoc />
        public abstract override Task ParseAsync();

        /// <summary>
        /// Inserts model into DB
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="context">Parsing DB context</param>
        protected async Task ProcessObject(TModel model, GeneralContext<TModel> context)
        {
            context.Models.Add(model);
            InsertDataOperation:
            try
            {
                await context.SaveChangesAsync();
            }
            // Appears while network card error occurs
            catch (InvalidOperationException e)
            {
                Logger.Warn(e.Message);
                Thread.Sleep(15000);
                goto InsertDataOperation;
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
        /// Gets html response from web request
        /// </summary>
        /// <param name="url">Url that gonna be requested</param>
        /// <param name="proxy">Proxy for request</param>
        /// <returns>html representation of response</returns>
        /// \todo(Unstable method, should be rewritten)
        protected string GetPage(string url, WebProxy proxy)
        {
            var pageResponse = "";
            try
            {
                var request = WebRequest.Create(url);
                request.Method = WebRequestMethods.Http.Get;
                request.AuthenticationLevel = AuthenticationLevel.None;
                request.Proxy = proxy;
                request.Timeout = 10000;
                var response = request.GetResponse();
                var objReader =
                    new StreamReader(response.GetResponseStream() ?? throw new NullReferenceException());

                var sLine = "";

                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                        pageResponse += sLine;
                }

                return pageResponse;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}