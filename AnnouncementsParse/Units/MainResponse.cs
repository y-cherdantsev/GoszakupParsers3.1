﻿using System.Collections.Generic;

 namespace AnnouncementsParse.Units
{
/*!

@author Yevgeniy Cherdantsev
@date 12.02.2020 19.12.19
@version 1.0
@brief Класс для создания объектов возвращаемых api после запроса
     
    */
    public class MainResponse
    {
        public int total { get; set; }
        public int limit { get; set; }
        public string next_page { get; set; }
        public List<Announcement> items { get; set; }
    }
}