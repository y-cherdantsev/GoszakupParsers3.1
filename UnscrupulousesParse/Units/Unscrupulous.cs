﻿ namespace UnscrupulousesParse.Units
{
    /*!

@author Yevgeniy Cherdantsev
@date 03.02.2020 16:43:27
@version 1.0
@brief Объект возвращаемый из API
    
    */
    public class Unscrupulous
    {
        public int pid { get; set; }
        public string supplier_biin { get; set; } = "";
        public string supplier_innunp { get; set; } = "";
        public string supplier_name_ru { get; set; } = "";
        public string supplier_name_kz { get; set; } = "";
        public int system_id { get; set; }
        public string index_date { get; set; } = "";

    }
}