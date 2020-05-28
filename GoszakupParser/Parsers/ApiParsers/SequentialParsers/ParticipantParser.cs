using System;
using System.Net;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;
using NLog;

namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 28.02.2020 13:56:44
    /// @version 1.0
    /// <summary>
    /// INPUT
    /// </summary>
    public sealed class ParticipantParser : ApiSequentialParser<ParticipantDto, ParticipantGoszakup>
    {
        public ParticipantParser(Configuration.ParserSettings parserSettings, string authToken, WebProxy proxy) : base(parserSettings,
            authToken, proxy)
        {
        }

        protected override Logger InitLogger()
        {
            return LogManager.GetCurrentClassLogger();
        }


        protected override ParticipantGoszakup DtoToDb(ParticipantDto dto)
        {
            var participant = new ParticipantGoszakup();

            participant.Pid = dto.pid;
            long.TryParse(dto.bin, out var bin);
            participant.Bin = bin;
            participant.Bin = participant.Bin != 0 ? participant.Bin : null;
            long.TryParse(dto.iin, out var iin);
            participant.Iin = iin;
            participant.Iin = participant.Iin != 0 ? participant.Iin : null;
            participant.Inn = !string.IsNullOrEmpty(dto.inn) ? dto.iin : null;
            participant.Unp = !string.IsNullOrEmpty(dto.unp) ? dto.unp : null;
            try
            {
                participant.Regdate = DateTime.Parse(dto.regdate);
            }
            catch (Exception)
            {
                participant.Regdate = null;
            }

            try
            {
                participant.IndexDate = DateTime.Parse(dto.index_date);
            }
            catch (Exception)
            {
                participant.IndexDate = null;
            }

            try
            {
                participant.LastUpdateDate = DateTime.Parse(dto.last_update_date);
            }
            catch (Exception)
            {
                participant.LastUpdateDate = null;
            }

            try
            {
                participant.Crdate = DateTime.Parse($"{dto.crdate.ToString()}-01-01 00:00:00");
            }
            catch (Exception)
            {
                participant.Crdate = null;
            }

            try
            {
                participant.Year = DateTime.Parse($"{dto.year.ToString()}-01-01 00:00:00");
            }
            catch (Exception)
            {
                participant.Year = null;
            }

            participant.NumberReg = !string.IsNullOrEmpty(dto.number_reg) ? dto.number_reg : null;
            participant.Series = !string.IsNullOrEmpty(dto.series) ? dto.series : null;
            participant.NameRu = !string.IsNullOrEmpty(dto.name_ru) ? dto.name_ru : null;
            participant.NameKz = !string.IsNullOrEmpty(dto.name_kz) ? dto.name_kz : null;
            participant.FullNameRu = !string.IsNullOrEmpty(dto.full_name_ru) ? dto.full_name_ru : null;
            participant.FullNameKz = !string.IsNullOrEmpty(dto.full_name_kz) ? dto.full_name_kz : null;
            participant.CountryCode = Convert.ToInt32(dto.country_code);
            participant.Customer = dto.customer == 1;
            participant.Organizer = dto.organizer == 1;
            participant.MarkNationalCompany = dto.mark_national_company == 1;
            participant.RefKopfCode = !string.IsNullOrEmpty(dto.ref_kopf_code) ? dto.ref_kopf_code : null;
            participant.MarkAssocWithDisab = dto.mark_assoc_with_disab == 1;
            participant.SystemId = dto.system_id != 0 ? dto.system_id : null;
            participant.Supplier = dto.supplier == 1;
            participant.TypeSupplier = dto.type_supplier;
            participant.KrpCode = dto.krp_code;
            participant.OkedList = dto.oked_list != 0 ? dto.oked_list : (int?) null;
            participant.KseCode = dto.kse_code;
            participant.MarkWorldCompany = dto.mark_world_company == 1;
            participant.MarkStateMonopoly = dto.mark_state_monopoly == 1;
            participant.MarkNaturalMonopoly = dto.mark_natural_monopoly == 1;
            participant.MarkPatronymicProducer = dto.mark_patronymic_producer == 1;
            participant.MarkPatronymicSupplier = dto.mark_patronymic_supplyer == 1;
            participant.MarkSmallEmployer = dto.mark_small_employer == 1;
            participant.IsSingleOrg = dto.is_single_org == 1;
            participant.Email = !string.IsNullOrEmpty(dto.email) ? dto.email : null;
            participant.Phone = !string.IsNullOrEmpty(dto.phone) ? dto.phone : null;
            participant.Website = !string.IsNullOrEmpty(dto.website) ? dto.website : null;
            participant.Qvazi = dto.qvazi == 1;
            participant.MarkResident = dto.mark_resident == 1;

            return participant;
        }
    }
}