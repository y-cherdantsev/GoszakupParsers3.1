using System;
using NpgsqlTypes;

namespace ParticipantsParse.Units
{
/*!
@author Yevgeniy Cherdantsev
@date 31.01.2020 18:40:33
@version 1.0
@brief Объект участника с корректными типами данных 
    
    */

    public class UnscrupulousDb
    {
        public UnscrupulousDb(Unscrupulous unscrupulous)
        {
            pid = unscrupulous.pid;
            Int64.TryParse(unscrupulous.supplier_biin, out var supplier_biin);
            this.supplier_biin = supplier_biin;
            supplier_innunp = unscrupulous.supplier_innunp;
            supplier_name_ru = unscrupulous.supplier_name_ru;
            supplier_name_kz = unscrupulous.supplier_name_kz;
            try { index_date = NpgsqlDateTime.Parse(unscrupulous.index_date); }catch (Exception) { }
            system_id = unscrupulous.system_id;
        }

        public int pid { get; set; }
        public long? supplier_biin { get; set; }
        public string supplier_innunp{ get; set; }
        public string supplier_name_ru { get; set; }
        public string supplier_name_kz { get; set; }
        public int system_id { get; set; }
        public NpgsqlDateTime index_date { get; set; }
    }
}