using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using GoszakupParser.Models;
using Microsoft.EntityFrameworkCore;
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
        private readonly object _lock = new object();

        public UnscrupulousParser(Configuration.ParserSettings parserSettings, string authToken)
        {
            AuthToken = authToken;
            NumOfDbConnections = parserSettings.NumberOfDbConnections;
            NextUrl = parserSettings.Url;
            var response = GetApiPageResponse(NextUrl);
            Total = JsonSerializer.Deserialize<ApiResponse>(response).total;
        }

        protected override Logger InitLogger()
        {
            return LogManager.GetCurrentClassLogger();
        }

        public override async Task ParseAsync()
        {
            var tasks = new List<Task>();
            _logger.Info($"Creating {NumOfDbConnections} contexts");
            for (var i = 0; i < NumOfDbConnections; i++)
                contexts.Add(new UnscrupulousGoszakupContext());
            _logger.Info($"{contexts.Count} contexts has been created");
            _logger.Info("Starting parsing");
            while (true)
            {
                var response = GetApiPageResponse(NextUrl);
                if (response == null)
                    break;
                var apiResponse = JsonSerializer.Deserialize<UnscrupulousApiResponse>(response);
                NextUrl = apiResponse.next_page;

                await Task.WhenAll(tasks);
                tasks.Clear();

                for (var i = 0; i < NumOfDbConnections; i++)
                    tasks.Add(ProcessObjects(apiResponse.items.Where(x => x.pid % NumOfDbConnections == i).ToArray()));

                if (NextUrl != "") continue;
                await Task.WhenAll(tasks);
                tasks.Clear();
                contexts.Clear();
                break;
            }

            _logger.Info("Parsing done");
            if (Total > 0)
                _logger.Info($"{Total} elements hasn't been parsed");
        }

        public override async Task ProcessObjects(object[] entities)
        {
            var unscrupulouses = (UnscrupulousDto[]) entities;
            var unscrupulousGoszakupContext =
                (UnscrupulousGoszakupContext) contexts[unscrupulouses[0].pid % NumOfDbConnections];
            foreach (var entity in unscrupulouses)
            {
                var unscrupulous = (UnscrupulousGoszakup) DtoToDb(entity);
                unscrupulousGoszakupContext.UnscrupulousGoszakup.Add(unscrupulous);
                await unscrupulousGoszakupContext.SaveChangesAsync();
                lock (_lock)
                    _logger.Trace($"Left:{--Total}");
            }
        }

        protected override object DtoToDb(object unscrupulousDto)
        {
            var unscrupulous = (UnscrupulousDto) unscrupulousDto;
            var unscrupulousGoszakup = new UnscrupulousGoszakup();
            unscrupulousGoszakup.Pid = unscrupulous.pid;
            try
            {
                unscrupulousGoszakup.IndexDate = DateTime.Parse(unscrupulous.index_date);
            }
            catch (Exception)
            {
            }

            Int64.TryParse(unscrupulous.supplier_biin, out var supplier_biin);
            unscrupulousGoszakup.SupplierBiin = supplier_biin;
            unscrupulousGoszakup.SupplierInnunp = unscrupulous.supplier_innunp;
            unscrupulousGoszakup.SupplierNameRu = unscrupulous.supplier_name_ru;
            unscrupulousGoszakup.SupplierNameKz = unscrupulous.supplier_name_kz;
            unscrupulousGoszakup.SystemId = unscrupulous.system_id;
            unscrupulousGoszakup.Relevance = DateTime.Now;
            return unscrupulousGoszakup;
        }
    }
}