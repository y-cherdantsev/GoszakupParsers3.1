using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoszakupParser.Contexts;
using GoszakupParser.Models;
using NLog;

namespace GoszakupParser.Parsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 25.02.2020 12:10:21
    /// @version 1.0
    /// <summary>
    /// Парсер недобросовестных учатников
    /// </summary>
    public class UnscrupulousParser : ApiParser<UnscrupulousGoszakupContext, UnscrupulousDto, UnscrupulousGoszakup>
    {
        public UnscrupulousParser(Configuration.ParserSettings parserSettings, string authToken) : base(parserSettings,
            authToken)
        {
        }

        protected override Logger InitLogger()
        {
            return LogManager.GetCurrentClassLogger();
        }

        protected override async Task ProcessObjects(List<UnscrupulousDto> entities)
        {
            var unscrupulousGoszakupContext = Contexts[entities[0].pid % NumOfDbConnections];
            foreach (var entity in entities)
            {
                var unscrupulous = DtoToDb(entity);
                unscrupulousGoszakupContext.UnscrupulousGoszakup.Add(unscrupulous);
                await unscrupulousGoszakupContext.SaveChangesAsync();
                lock (Lock)
                    Logger.Trace($"Left:{--Total}");
            }
        }

        protected override UnscrupulousGoszakup DtoToDb(UnscrupulousDto dto)
        {
            var unscrupulousDto = dto;
            var unscrupulousGoszakup = new UnscrupulousGoszakup();
            unscrupulousGoszakup.Pid = unscrupulousDto.pid;
            try
            {
                unscrupulousGoszakup.IndexDate = DateTime.Parse(unscrupulousDto.index_date);
            }
            catch (Exception)
            {
            }

            Int64.TryParse(unscrupulousDto.supplier_biin, out var supplier_biin);
            unscrupulousGoszakup.SupplierBiin = supplier_biin;
            unscrupulousGoszakup.SupplierInnunp = unscrupulousDto.supplier_innunp;
            unscrupulousGoszakup.SupplierNameRu = unscrupulousDto.supplier_name_ru;
            unscrupulousGoszakup.SupplierNameKz = unscrupulousDto.supplier_name_kz;
            unscrupulousGoszakup.SystemId = unscrupulousDto.system_id;
            unscrupulousGoszakup.Relevance = DateTime.Now;
            return unscrupulousGoszakup;
        }
    }
}