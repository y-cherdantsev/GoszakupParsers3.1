using System.Collections.Generic;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.Dtos
{
    /// @author Yevgeniy Cherdantsev
    /// @date 03.02.2020 18:45:45
    /// <summary>
    /// API response object
    /// </summary>
    /// <typeparam name="T">Type of response elements</typeparam>
    public class ApiResponse<T>
    {
        public int total { get; set; }
        public int limit { get; set; }
        public int code { get; set; }
        public int status { get; set; }
        public string next_page { get; set; }
        public string name { get; set; }
        public string message { get; set; }
        public string type { get; set; }
        public List<T> items = new List<T>();
    }
}