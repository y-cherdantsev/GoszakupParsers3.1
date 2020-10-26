// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
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
        public TrdBuyTypeDto RefTypeTrade { get; set; }
        public TrdBuyTypeDto RefTradeMethods { get; set; }
        public TrdBuyTypeDto RefSubjectType { get; set; }
        public TrdBuyTypeDto RefBuyStatus { get; set; }
        public TrdBuyFileDto[] Files { get; set; }
        public TrdBuyLotDto[] Lots { get; set; }

        public class TrdBuyTypeDto
        {
            public string nameRu { get; set; }
        }

        public class TrdBuyLotDto
        {
            public string lotNumber { get; set; }
            public TrdBuyTypeDto RefLotsStatus { get; set; }
            public string customerBin { get; set; }
            public string descriptionRu { get; set; }
            public double amount { get; set; }
            public double count { get; set; }
            public string nameRu { get; set; }
            public TrdBuyFileDto[] Files { get; set; }
            public TrdBuyPlan[] Plans { get; set; }

            public class TrdBuyPlan
            {
                public string refEnstruCode { get; set; }
                public string supplyDateRu { get; set; }
                public TrdBuyTypeDto RefUnits { get; set; }
                public TrdBuyPlanKatoDto[] PlansKato { get; set; }

                public class TrdBuyPlanKatoDto
                {
                    public string fullDeliveryPlaceNameRu { get; set; }
                }
            }
        }

        public class TrdBuyFileDto
        {
            public string filePath { get; set; }
            public string originalName { get; set; }
            public string nameRu { get; set; }
        }
    }
}