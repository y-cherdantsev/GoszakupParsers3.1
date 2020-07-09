using System;
using System.Net;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;

namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 28.02.2020 23:59:50
    /// <summary>
    /// Announcement Parser
    /// </summary>
    public sealed class AnnouncementParser : ApiSequentialParser<AnnouncementDto, AnnouncementGoszakup>
    {
        /// <inheritdoc />
        public AnnouncementParser(Configuration.ParserSettings parserSettings, WebProxy proxy, string authToken) : base(
            parserSettings,
            proxy, authToken)
        {
        }

        /// <inheritdoc />
        protected override AnnouncementGoszakup DtoToDb(AnnouncementDto dto)
        {
            long.TryParse(dto.customer_bin, out var customerBin);
            long.TryParse(dto.org_bin, out var orgBin);
            long.TryParse(dto.biin_supplier, out var biinSupplier);
            DateTime.TryParse(dto.start_date, out var startDate);
            DateTime.TryParse(dto.repeat_start_date, out var repeatStartDate);
            DateTime.TryParse(dto.repeat_end_date, out var repeatEndDate);
            DateTime.TryParse(dto.end_date, out var endDate);
            DateTime.TryParse(dto.publish_date, out var publishDate);
            DateTime.TryParse(dto.itogi_date_public, out var itogiDatePublic);
            DateTime.TryParse(dto.discus_start_date, out var discusStartDate);
            DateTime.TryParse(dto.discus_end_date, out var discusEndDate);
            DateTime.TryParse(dto.index_date, out var indexDate);

            var announcement = new AnnouncementGoszakup
            {
                Id = dto.id,
                NumberAnno = dto.number_anno,
                NameRu = !string.IsNullOrEmpty(dto.name_ru) ? dto.name_ru : null,
                NameKz = !string.IsNullOrEmpty(dto.name_kz) ? dto.name_kz : null,
                TotalSum = dto.total_sum,
                RefTradeMethodsId = dto.ref_trade_methods_id,
                RefSubjectTypeId = dto.ref_subject_type_id,
                CustomerBin = customerBin != 0 ? customerBin : (long?) null,
                CustomerPid = dto.customer_pid != 0 ? dto.customer_pid : (int?) null,
                OrgBin = orgBin,
                OrgPid = dto.org_pid,
                RefBuyStatusId = dto.ref_buy_status_id,
                RefTypeTradeId = dto.ref_type_trade_id,
                DisablePersonId = dto.disable_person_id == 1,
                BiinSupplier = biinSupplier != 0 ? biinSupplier : (long?) null,
                IdSupplier = dto.id_supplier != 0 ? dto.id_supplier : (int?) null,
                ParentId = dto.parent_id != 0 ? dto.parent_id : (int?) null,
                SingleOrgSign = dto.singl_org_sign == 1,
                IsLightIndustry = dto.is_light_industry == 1,
                IsConstructionWork = dto.is_construction_work == 1,
                CustomerNameKz = !string.IsNullOrEmpty(dto.customer_name_kz) ? dto.customer_name_kz : null,
                CustomerNameRu = !string.IsNullOrEmpty(dto.customer_name_ru) ? dto.customer_name_ru : null,
                OrgNameKz = !string.IsNullOrEmpty(dto.org_name_kz) ? dto.org_name_kz : null,
                OrgNameRu = !string.IsNullOrEmpty(dto.org_name_ru) ? dto.org_name_ru : null,
                SystemId = dto.system_id,
                StartDate = startDate,
                RepeatStartDate = repeatStartDate,
                RepeatEndDate = repeatEndDate,
                EndDate = endDate,
                PublishDate = publishDate,
                ItogiDatePublic = itogiDatePublic,
                DiscusStartDate = discusStartDate,
                DiscusEndDate = discusEndDate,
                IndexDate = indexDate
            };

            return announcement;
        }
    }
}