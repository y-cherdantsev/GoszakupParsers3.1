﻿using System.Collections.Generic;

 namespace ParticipantsParse.Units
{
/*!

@author Yevgeniy Cherdantsev
@date 03.02.2020 16:44:14
@version 1.0
@brief Класс для создания объектов возвращаемых api после запроса
     
    */
    public class MainResponseParticipants
    {
        public int total { get; set; }
        public int limit { get; set; }
        public string next_page { get; set; }
        public List<Participant> items { get; set; }
    }
}