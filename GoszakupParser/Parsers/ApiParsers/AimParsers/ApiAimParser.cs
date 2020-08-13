using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using GoszakupParser.Models;
using GoszakupParser.Models.Dtos;
using Newtonsoft.Json;

// ReSharper disable CommentTypo
// ReSharper disable InconsistentlySynchronizedField

// ReSharper disable once IdentifierTypo
namespace GoszakupParser.Parsers.ApiParsers.AimParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 13.03.2020 14:01:53
    /// <summary>
    /// Parent parser used for creating parsers that gets information from api by aim elements
    /// </summary>
    /// <typeparam name="TDto">Dto that will be parsed</typeparam>
    /// <typeparam name="TResultModel">Model in which dto will be converted</typeparam>
    public abstract class ApiAimParser<TDto, TResultModel> : ApiParser<TDto, TResultModel>
        where TResultModel  : BaseModel, new()
    {
        /// <summary>
        /// Aims that's used for parsing
        /// </summary>
        private List<string> Aims { get; set; }

        /// <inheritdoc />
        protected ApiAimParser(Configuration.ParserSettings parserSettings, string authToken) : base(
            parserSettings, authToken)
        {
        }

        /// <summary>
        /// Loads aims from the (Mostly from DB, but also can be file or smt. similar)
        /// </summary>
        /// <returns>List of aims</returns>
        protected abstract IEnumerable<string> LoadAims();

        /// <inheritdoc />
        public override async Task ParseAsync()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Aims = (List<string>) LoadAims();
            Total = Aims.Count;
            
            Logger.Info("Starting Parsing");
            var tasks = new List<Task>();

            for (var i = 0; i < Threads; i++)
            {
                var dividedList = DivideList(Aims, i);
                tasks.Add(ParseArray((IReadOnlyList<string>) dividedList));
            }

            await Task.WhenAll(tasks);
            Logger.Info("End Of Parsing");
        }

        /// <summary>
        /// Parses list of aims
        /// </summary>
        /// <param name="list">List of aims to parse</param>
        private async Task ParseArray(IReadOnlyList<string> list)
        {
            Logger.Trace("Start");
            await Task.Delay(500);
            Logger.Trace("Start Async");
            var tasks = new List<Task>();
            foreach (var element in list)
            {
                // Gets response for aim
                var response = await GetApiPageResponse($"{Url}/{element}");
                lock (Lock)
                {
                    --Total;
                }


                // If responce contains only one object =>
                if (!response.Contains("total"))
                {
                    var apiResponse = JsonConvert.DeserializeObject<TDto>(response);
                    tasks.Add(ProcessObjects(new []{(object)apiResponse}));
                }
                // Else
                else
                {
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<TDto>>(response);
                    //{"name":"Not Found","message":"*** не найден в реестре","code":0,"status":404,"type":"yii\\web\\NotFoundHttpException"}
                    if (apiResponse.status != (int) HttpStatusCode.NotFound)
                        tasks.Add(ProcessObjects((IEnumerable<object>) apiResponse.items));
                }
            }

            await Task.WhenAll(tasks);
        }
    }
}