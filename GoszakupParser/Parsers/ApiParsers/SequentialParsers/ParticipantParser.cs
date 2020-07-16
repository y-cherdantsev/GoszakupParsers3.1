using System;
using System.Net;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;

// ReSharper disable CommentTypo

// ReSharper disable once IdentifierTypo
namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 28.02.2020 13:56:44
    /// <summary>
    /// Participant Parser
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public sealed class ParticipantParser : ApiSequentialParser<ParticipantDto, ParticipantGoszakup>
    {
        /// <inheritdoc />
        public ParticipantParser(Configuration.ParserSettings parserSettings, WebProxy proxy, string authToken) : base(
            parserSettings,
            proxy, authToken)
        {
        }

        /// <inheritdoc />
        protected override ParticipantGoszakup DtoToModel(ParticipantDto dto)
        {
            long.TryParse(dto.bin, out var bin);
            long.TryParse(dto.iin, out var iin);
            DateTime.TryParse(dto.regdate, out var regdate);
            DateTime.TryParse(dto.index_date, out var indexDate);
            DateTime.TryParse(dto.last_update_date, out var lastUpdateDate);
            DateTime.TryParse($"{dto.crdate.ToString()}-01-01 00:00:00", out var crdate);
            DateTime.TryParse($"{dto.year.ToString()}-01-01 00:00:00", out var year);

            var participant = new ParticipantGoszakup
            {
                Pid = dto.pid,
                Bin = bin != 0 ? bin : (long?) null,
                Iin = iin != 0 ? iin : (long?) null,
                Inn = !string.IsNullOrEmpty(dto.inn) ? dto.iin : null,
                Unp = !string.IsNullOrEmpty(dto.unp) ? dto.unp : null,
                NumberReg = !string.IsNullOrEmpty(dto.number_reg) ? dto.number_reg : null,
                Series = !string.IsNullOrEmpty(dto.series) ? dto.series : null,
                NameRu = !string.IsNullOrEmpty(dto.name_ru) ? dto.name_ru : null,
                NameKz = !string.IsNullOrEmpty(dto.name_kz) ? dto.name_kz : null,
                FullNameRu = !string.IsNullOrEmpty(dto.full_name_ru) ? dto.full_name_ru : null,
                FullNameKz = !string.IsNullOrEmpty(dto.full_name_kz) ? dto.full_name_kz : null,
                CountryCode = Convert.ToInt32(dto.country_code),
                Customer = dto.customer == 1,
                Organizer = dto.organizer == 1,
                MarkNationalCompany = dto.mark_national_company == 1,
                RefKopfCode = !string.IsNullOrEmpty(dto.ref_kopf_code) ? dto.ref_kopf_code : null,
                MarkAssocWithDisab = dto.mark_assoc_with_disab == 1,
                SystemId = dto.system_id != 0 ? dto.system_id : null,
                Supplier = dto.supplier == 1,
                TypeSupplier = dto.type_supplier,
                KrpCode = dto.krp_code,
                OkedList = dto.oked_list != 0 ? dto.oked_list : (int?) null,
                KseCode = dto.kse_code,
                MarkWorldCompany = dto.mark_world_company == 1,
                MarkStateMonopoly = dto.mark_state_monopoly == 1,
                MarkNaturalMonopoly = dto.mark_natural_monopoly == 1,
                MarkPatronymicProducer = dto.mark_patronymic_producer == 1,
                MarkPatronymicSupplier = dto.mark_patronymic_supplyer == 1,
                MarkSmallEmployer = dto.mark_small_employer == 1,
                IsSingleOrg = dto.is_single_org == 1,
                Email = !string.IsNullOrEmpty(dto.email) ? dto.email : null,
                Phone = !string.IsNullOrEmpty(dto.phone) ? dto.phone : null,
                Website = !string.IsNullOrEmpty(dto.website) ? dto.website : null,
                Qvazi = dto.qvazi == 1,
                MarkResident = dto.mark_resident == 1,
                Regdate = regdate,
                IndexDate = indexDate,
                LastUpdateDate = lastUpdateDate,
                Crdate = crdate,
                Year = year
            };

            return participant;
        }
    }
}