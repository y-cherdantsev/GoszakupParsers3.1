using System;
using System.Net;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;

// ReSharper disable CommentTypo

// ReSharper disable once IdentifierTypo
namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 28.02.2020 23:55:36
    /// <summary>
    /// Lot Parser
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public sealed class LotParser : ApiSequentialParser<LotDto, LotGoszakup>
    {
        /// <inheritdoc />
        public LotParser(Configuration.ParserSettings parserSettings, WebProxy proxy, string authToken) : base(
            parserSettings,
            proxy, authToken)
        {
        }

        /// <inheritdoc />
        protected override LotGoszakup DtoToDb(LotDto dto)
        {
            long.TryParse(dto.customer_bin, out var customerBin);
            DateTime.TryParse(dto.index_date, out var indexDate);
            DateTime.TryParse(dto.last_update_date, out var lastUpdateDate);

            var lot = new LotGoszakup
            {
                Id = dto.id,
                LotNumber = dto.lot_number,
                RefLotStatusId = dto.ref_lot_status_id,
                UnionLots = dto.union_lots == 1,
                Count = dto.count,
                Amount = dto.amount,
                NameRu = dto.name_ru,
                NameKz = dto.name_kz,
                DescriptionRu = dto.description_ru,
                DescriptionKz = dto.description_kz,
                CustomerId = dto.customer_id,
                CustomerBin = customerBin,
                TrdBuyNumberAnno = dto.trd_buy_number_anno,
                TrdBuyId = dto.trd_buy_id,
                Dumping = dto.dumping == 1,
                DumpingLotPrice = dto.dumping_lot_price != 0 ? dto.dumping_lot_price : (int?) null,
                PsdSign = dto.psd_sign != 0 ? dto.psd_sign : (int?) null,
                ConsultingServices = dto.consulting_services == 1,
                SingleOrgSign = dto.singl_org_sign,
                IsLightIndustry = dto.is_light_industry == 1,
                IsConstructionWork = dto.is_construction_work == 1,
                DisablePersonId = dto.disable_person_id == 1,
                CustomerNameKz = dto.customer_name_kz,
                CustomerNameRu = dto.customer_name_ru,
                RefTradeMethodsId = dto.ref_trade_methods_id,
                SystemId = dto.system_id,
                IndexDate = indexDate,
                LastUpdateDate = lastUpdateDate,
                PlanPoint = dto.point_list.Length > 0 ? dto.point_list[0] : (int?) null
            };

            return lot;
        }
    }
}