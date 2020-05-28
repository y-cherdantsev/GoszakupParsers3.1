using System;
using System.Net;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;
using NLog;

namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 28.02.2020 23:55:36
    /// @version 1.0
    /// <summary>
    /// INPUT
    /// </summary>
    public class LotParser : ApiSequentialParser<LotDto, LotGoszakup>
    {
        public LotParser(Configuration.ParserSettings parserSettings, string authToken, WebProxy proxy) : base(parserSettings,
            authToken, proxy)
        {
        }

        protected override Logger InitLogger()
        {
            return LogManager.GetCurrentClassLogger();
        }

        protected override LotGoszakup DtoToDb(LotDto dto)
        {
            var lot = new LotGoszakup();

            lot.Id = dto.id;
            lot.LotNumber = dto.lot_number;
            lot.RefLotStatusId = dto.ref_lot_status_id;
            try
            {
                lot.IndexDate = DateTime.Parse(dto.index_date);
            }
            catch (Exception)
            {
                lot.IndexDate = null;
            }

            try
            {
                lot.LastUpdateDate = DateTime.Parse(dto.last_update_date);
            }
            catch (Exception)
            {
                lot.LastUpdateDate = null;
            }

            lot.UnionLots = dto.union_lots == 1;
            lot.Count = dto.count;
            lot.Amount = dto.amount;
            lot.NameRu = dto.name_ru;
            lot.NameKz = dto.name_kz;
            lot.DescriptionRu = dto.description_ru;
            lot.DescriptionKz = dto.description_kz;
            lot.CustomerId = dto.customer_id;
            long.TryParse(dto.customer_bin, out var customerBin);
            lot.CustomerBin = customerBin;
            lot.TrdBuyNumberAnno = dto.trd_buy_number_anno;
            lot.TrdBuyId = dto.trd_buy_id;
            lot.Dumping = dto.dumping == 1;
            lot.DumpingLotPrice = dto.dumping_lot_price != 0 ? dto.dumping_lot_price : (int?) null;
            lot.PsdSign = dto.psd_sign != 0 ? dto.psd_sign : (int?) null;
            lot.ConsultingServices = dto.consulting_services == 1;
            lot.SingleOrgSign = dto.singl_org_sign;
            lot.IsLightIndustry = dto.is_light_industry == 1;
            lot.IsConstructionWork = dto.is_construction_work == 1;
            lot.DisablePersonId = dto.disable_person_id == 1;
            lot.CustomerNameKz = dto.customer_name_kz;
            lot.CustomerNameRu = dto.customer_name_ru;
            lot.RefTradeMethodsId = dto.ref_trade_methods_id;
            lot.SystemId = dto.system_id;

            return lot;
        }
    }
}