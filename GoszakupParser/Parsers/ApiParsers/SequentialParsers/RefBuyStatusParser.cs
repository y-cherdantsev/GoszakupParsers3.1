﻿using System.Net;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;

// ReSharper disable CommentTypo

// ReSharper disable once IdentifierTypo
namespace GoszakupParser.Parsers.ApiParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 09.07.2020 15:39:44
    /// <summary>
    /// Ref Buy Status Parser
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public sealed class RefBuyStatusParser : ApiSequentialParser<RefBuyStatusDto, RefBuyStatusGoszakup>
    {
        /// <inheritdoc />
        public RefBuyStatusParser(Configuration.ParserSettings parserSettings, WebProxy proxy, string authToken) : base(
            parserSettings, proxy, authToken)
        {
        }

        /// <inheritdoc />
        protected override RefBuyStatusGoszakup DtoToDb(RefBuyStatusDto dto)
        {
            var refBuyStatus = new RefBuyStatusGoszakup
            {
                Id = dto.id,
                NameRu = dto.name_ru,
                NameKz = dto.name_kz,
                Code = dto.code
            };
            return refBuyStatus;
        }
    }
}