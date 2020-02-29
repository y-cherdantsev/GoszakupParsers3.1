using System;
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
    public sealed class ParticipantParser : ApiSequentialParser<ParticipantDto, ParticipantGoszakup> {
        public ParticipantParser(Configuration.ParserSettings parserSettings, string authToken) : base(parserSettings, authToken)
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
            long.TryParse(dto.iin, out var iin);
            participant.Iin = iin;
            participant.Inn = dto.inn;
            participant.Unp = dto.unp;
            try { participant.Regdate = DateTime.Parse(dto.regdate); }catch (Exception) { }
            try { participant.IndexDate = DateTime.Parse(dto.index_date); }catch (Exception) { }
            try { participant.LastUpdateDate = DateTime.Parse(dto.last_update_date); }catch (Exception) { }
            try { participant.Crdate = DateTime.Parse($"{dto.crdate.ToString()}-01-01 00:00:00"); }catch (Exception) { }
            try { participant.Year = DateTime.Parse($"{dto.year.ToString()}-01-01 00:00:00"); }catch (Exception) { }
            participant.NumberReg = dto.number_reg;
            participant.Series = dto.series;
            participant.NameRu = dto.name_ru;
            participant.NameKz = dto.name_kz;
            participant.FullNameRu = dto.full_name_ru;
            participant.FullNameKz = dto.full_name_kz;
            participant.CountryCode = Convert.ToInt32(dto.country_code);
            participant.Customer = dto.customer == 1;
            participant.Organizer = dto.organizer == 1;
            participant.MarkNationalCompany = dto.mark_national_company == 1;
            participant.RefKopfCode = dto.ref_kopf_code;
            participant.MarkAssocWithDisab = dto.mark_assoc_with_disab == 1;
            participant.SystemId = dto.system_id;
            participant.Supplier = dto.supplier == 1;
            participant.TypeSupplier = dto.type_supplier;
            participant.KrpCode = dto.krp_code;
            participant.OkedList = dto.oked_list;
            participant.KseCode = dto.kse_code;
            participant.MarkWorldCompany = dto.mark_world_company == 1;
            participant.MarkStateMonopoly = dto.mark_state_monopoly == 1;
            participant.MarkNaturalMonopoly = dto.mark_natural_monopoly == 1;
            participant.MarkPatronymicProducer = dto.mark_patronymic_producer == 1;
            participant.MarkPatronymicSupplier = dto.mark_patronymic_supplyer == 1;
            participant.MarkSmallEmployer = dto.mark_small_employer == 1;
            participant.IsSingleOrg = dto.is_single_org == 1;
            participant.Email = dto.email;
            participant.Phone = dto.phone;
            participant.Website = dto.website;
            participant.Qvazi = dto.qvazi == 1;
            participant.MarkResident = dto.mark_resident == 1;

            return participant;
        }
    }
}