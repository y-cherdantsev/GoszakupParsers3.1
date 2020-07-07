using System.Collections.Generic;

namespace GoszakupParser.Models.Dtos
{
    /// @author Yevgeniy Cherdantsev
    /// @date 03.02.2020 18:45:45
    /// <summary>
    /// API response object
    /// </summary>
    public class ApiResponse<T>
    {
        public int total { get; set; }
        public int limit { get; set; }
        public string next_page { get; set; }
        public List<T> items = new List<T>();
    }
}