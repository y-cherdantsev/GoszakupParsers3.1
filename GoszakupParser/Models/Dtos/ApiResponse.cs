using System.Collections.Generic;

namespace GoszakupParser.Models.Dtos
{
/*!

@author Yevgeniy Cherdantsev
@date 03.02.2020 16:44:14
@version 1.0
@brief Класс для создания объектов возвращаемых api после запроса
     
    */
    public class ApiResponse <T>
    {
        public int total { get; set; }
        public int limit { get; set; }
        public string next_page { get; set; }
        public List<T> items = new List<T>();
    }
}