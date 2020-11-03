// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.Dtos
{
    /// @author Yevgeniy Cherdantsev
    /// @date 07.08.2020 16:47:59
    /// <summary>
    /// Tender Document 
    /// </summary>
    public class TenderDocumentDto
    {
        public string Identity { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
    }
}