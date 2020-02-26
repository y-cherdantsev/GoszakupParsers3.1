﻿using System.Collections.Generic;

 namespace GoszakupParser.Models
{
/*!

@author Yevgeniy Cherdantsev
@date 03.02.2020 16:44:14
@version 1.0
@brief Класс для создания объектов возвращаемых api после запроса
     
    */
    public class ApiResponse
    {
       
        public int total { get; set; }
        public int limit { get; set; }
        public string next_page { get; set; }
    }
    
    public class AnnouncementApiResponse : ApiResponse
    {
        public List<AnnouncementDto> items { get; set; }
    }
    public class UnscrupulousApiResponse : ApiResponse
    {
        public List<UnscrupulousDto> items { get; set; }
    }
}