// ReSharper disable IdentifierTypo
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CommentTypo
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.Dtos
{
    /// @author Yevgeniy Cherdantsev
    /// @date 26.09.2020 12:17:23
    /// <summary>
    /// API GraphQl Contract object
    /// </summary>
    public class ContractDto
    {
        public string trdBuyNumberAnno { get; set; }
        public string contractNumber { get; set; }
        public string contractNumberSys { get; set; }
        public string supplierBiin { get; set; }
        public string customerBin { get; set; }
        public string supplierIik { get; set; }
        public string customerIik { get; set; }
        public string supplierBik { get; set; }
        public string customerBik { get; set; }
        public int finYear { get; set; }
        public string signDate { get; set; }
        public string crdate { get; set; }
        public string ecEndDate { get; set; }
        public string descriptionRu { get; set; }
        public double contractSumWnds { get; set; }
        public double faktSumWnds { get; set; }
        public string supplierBankNameRu { get; set; }
        public string customerBankNameRu { get; set; }
        public ContractTypeDto RefitemsAgrForm { get; set; }
        public ContractTypeDto RefitemsYearType { get; set; }
        public ContractTypeDto FaktTradeMethods { get; set; }
        public ContractTypeDto RefitemsStatus { get; set; }
        public ContractTypeDto RefitemsType { get; set; }
        public ContractUnitDto[] itemsUnits { get; set; }
        public ContractFileDto File { get; set; }

        public class ContractUnitDto
        {
            public long id { get; set; }
            public double itemPrice { get; set; }
            public double itemPriceWnds { get; set; }
            public double quantity { get; set; }
            public double totalSum { get; set; }
            public double totalSumWnds { get; set; }
            public ContractUnitPlanDto Plans { get; set; }

            public class ContractUnitPlanDto
            {
                public long Id { get; set; }
                public ContractUnitPlanActDto PlanActs { get; set; }
                public ContractTypeDto RefPlnPointStatus { get; set; }
                public string nameRu { get; set; }
                public string descRu { get; set; }
                public string extraDescRu { get; set; }
                public ContractTypeDto RefTradeMethods { get; set; }
                public ContractTypeDto RefUnits { get; set; }
                public double count { get; set; }
                public double price { get; set; }
                public double amount { get; set; }
                public int refMonthsId { get; set; }
                public string refEnstruCode { get; set; }
                public int isQvazi { get; set; }
                public string dateCreate { get; set; }
                public string supplyDateRu { get; set; }
                public double prepayment { get; set; }
                public string subjectBiin { get; set; }

                public class ContractUnitPlanActDto
                {
                    public string planActNumber { get; set; }
                    public int planFinYear { get; set; }
                    public string dateApproved { get; set; }
                }
            }
        }
        
        public class ContractFileDto
        {
            public string nameRu { get; set; }
            public string filePath { get; set; }
            public string originalName { get; set; }
        }
        
        public class ContractTypeDto
        {
            public string nameRu { get; set; }
        }
    }
}