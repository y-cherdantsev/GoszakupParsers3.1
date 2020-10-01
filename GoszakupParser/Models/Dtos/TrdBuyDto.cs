// ReSharper disable IdentifierTypo
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable CommentTypo
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace GoszakupParser.Models.Dtos
{
    /// @author Yevgeniy Cherdantsev
    /// @date 26.09.2020 12:17:23
    /// <summary>
    /// API GraphQl TrdBuy object
    /// </summary>
    public class TrdBuyDto
    {
        public string numberAnno { get; set; }
        public string nameRu { get; set; }
        public string orgBin { get; set; }
        public double totalSum { get; set; }
        public int countLots { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string publishDate { get; set; }
        public TrdBuyType RefTypeTrade { get; set; }
        public TrdBuyType RefTradeMethods { get; set; }
        public TrdBuyType RefSubjectType { get; set; }
        public TrdBuyType RefBuyStatus { get; set; }
        public TrdBuyFile[] Files { get; set; }
        public TrdBuyLot[] Lots { get; set; }

        public class TrdBuyType
        {
            public string nameRu { get; set; }
        }

        public class TrdBuyLot
        {
            public string lotNumber { get; set; }
            public TrdBuyType RefLotsStatus { get; set; }
            public string customerBin { get; set; }
            public string descriptionRu { get; set; }
            public double amount { get; set; }
            public double count { get; set; }
            public string nameRu { get; set; }
            public TrdBuyFile[] Files { get; set; }
            public TrdBuyPlan[] Plans { get; set; }

            public class TrdBuyPlan
            {
                public string refEnstruCode { get; set; }
                public string supplyDateRu { get; set; }
                
                public string nameRu { get; set; }
                public string descRu { get; set; }
                public TrdBuyType RefUnits { get; set; }
                public TrdBuyPlanKato[] PlansKato { get; set; }

                public class TrdBuyPlanKato
                {
                    public string fullDeliveryPlaceNameRu { get; set; }
                }
            }
        }

        public class TrdBuyFile
        {
            public string filePath { get; set; }
            public string originalName { get; set; }
            public string nameRu { get; set; }
        }
    }
}