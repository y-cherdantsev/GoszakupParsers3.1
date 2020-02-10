﻿using System.Collections.Generic;

 namespace ContractsParse.Units
{
/*!

@author Yevgeniy Cherdantsev
@date 03.02.2020 16:44:14
@version 1.0
@brief Класс для создания объектов возвращаемых api после запроса
     
    */
    public class MainResponse
    {
        public int total { get; set; }
        public int limit { get; set; }
        public string next_page { get; set; }
        public List<Contract> items { get; set; }
    }
}