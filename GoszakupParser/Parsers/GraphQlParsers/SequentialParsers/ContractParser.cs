using System;
using System.Threading.Tasks;
using GoszakupParser.Models.Dtos;
using GoszakupParser.Models.ParsingModels;
using GoszakupParser.Contexts.ParsingContexts;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo

namespace GoszakupParser.Parsers.GraphQlParsers.SequentialParsers
{
    /// @author Yevgeniy Cherdantsev
    /// @date 02.10.2020 11:12:13
    /// <summary>
    /// Contract Parser
    /// </summary>
    // ReSharper disable once UnusedType.Global
    public class ContractParser : GraphQlSequentialParser<ContractDto>
    {
        /// <summary>
        /// QueryTemplate for requests
        /// </summary>
        private const string QueryTemplate =
            "{\"query\":\"query{Contract(limit:200, filter:{finYear:2019}, _AFTER){trdBuyNumberAnno,contractNumber,contractNumberSys,supplierBiin,customerBin,supplierIik,customerIik,finYear,signDate,crdate,ecEndDate,descriptionRu,contractSumWnds,faktSumWnds,supplierBankNameRu,customerBankNameRu,supplierBik,customerBik,RefContractAgrForm{nameRu},RefContractYearType{nameRu},FaktTradeMethods{nameRu},RefContractStatus{nameRu},RefContractType{nameRu},File{nameRu,filePath,originalName},ContractUnits{id,itemPrice,itemPriceWnds,quantity,totalSum,totalSumWnds,Plans{id,PlanActs{planActNumber,planFinYear,dateApproved},RefPlnPointStatus{nameRu},nameRu,RefUnits{nameRu},RefTradeMethods{nameRu},count,price,amount,refMonthsId,refEnstruCode,isQvazi,dateCreate,descRu,extraDescRu,supplyDateRu,prepayment,subjectBiin}}}}\"}";

        public ContractParser(Configuration.ParserSettings parserSettings) : base(parserSettings)
        {
        }

        /// <inheritdoc />
        protected override string GetQuery(int after)
        {
            var query = QueryTemplate.Replace("_AFTER", after == 0 ? string.Empty : $"after:{after}");

            return query;
        }

        /// <inheritdoc />
        protected override async Task ProcessObject(ContractDto dto)
        {
            await using var ctx = new ParsingContractContext();
            ctx.ChangeTracker.AutoDetectChangesEnabled = false;

            long.TryParse(dto.customerBin, out var customerBin);
            long.TryParse(dto.supplierBiin, out var supplierBiin);
            DateTime.TryParse(dto.signDate, out var signDate);
            DateTime.TryParse(dto.crdate, out var createDate);
            DateTime.TryParse(dto.ecEndDate, out var ecEndDate);
            var contractGoszakup = new ContractGoszakup
            {
                Status = dto.RefitemsStatus?.nameRu,
                Type = dto.RefitemsType?.nameRu,
                AgrForm = dto.RefitemsAgrForm?.nameRu,
                AnnouncementNumber = string.IsNullOrEmpty(dto.trdBuyNumberAnno) ? null : dto.trdBuyNumberAnno,
                ContractNumber = string.IsNullOrEmpty(dto.contractNumber) ? null : dto.contractNumber,
                ContractNumberSys = dto.contractNumberSys,
                CustomerBin = customerBin,
                SupplierBiin = supplierBiin,
                CustomerBik = string.IsNullOrEmpty(dto.customerBik) ? null : dto.customerBik,
                SupplierBik = string.IsNullOrEmpty(dto.supplierBik) ? null : dto.supplierBik,
                CustomerIik = string.IsNullOrEmpty(dto.customerIik) ? null : dto.customerIik,
                SupplierIik = string.IsNullOrEmpty(dto.supplierIik) ? null : dto.supplierIik,
                DescriptionRu = dto.descriptionRu?
                    .Replace("&amp;", "")
                    .Replace("amp;", "")
                    .Replace("&quot;", "\"")
                    .Replace("quot;", "\""),
                FinYear = dto.finYear,
                SignDate = signDate.Year == 1 ? (DateTime?) null : signDate,
                EcEndDate = ecEndDate.Year == 1 ? (DateTime?) null : ecEndDate,
                TradeMethod = dto.FaktTradeMethods?.nameRu,
                YearType = dto.RefitemsYearType?.nameRu,
                ContractSumWnds = dto.contractSumWnds,
                FaktSumWnds = dto.faktSumWnds,
                CustomerBankNameRu = string.IsNullOrEmpty(dto.customerBankNameRu) ? null : dto.customerBankNameRu,
                SupplierBankNameRu = string.IsNullOrEmpty(dto.supplierBankNameRu) ? null : dto.supplierBankNameRu,
                CreateDate = createDate.Year == 1 ? (DateTime?) null : createDate,
                DocLink = dto.File?.filePath,
                DocName = dto.File?.originalName
            };

            await ctx.ContractsGoszakup.AddAsync(contractGoszakup);
            await ctx.SaveChangesAsync();

            if (dto.itemsUnits != null)
                foreach (var dtoContractUnit in dto.itemsUnits)
                {
                    var contractUnit = new ContractUnitGoszakup
                    {
                        SourceUniqueId = dtoContractUnit.id,
                        ItemPrice = dtoContractUnit.itemPrice,
                        ItemPriceWnds = dtoContractUnit.itemPriceWnds,
                        Quantity = dtoContractUnit.quantity,
                        TotalSum = dtoContractUnit.totalSum,
                        TotalSumWnds = dtoContractUnit.totalSumWnds,
                        ContractId = contractGoszakup.Id
                    };

                    await ctx.ContractUnitsGoszakup.AddAsync(contractUnit);
                    await ctx.SaveChangesAsync();

                    if (dtoContractUnit.Plans == null) continue;
                    
                    long.TryParse(dtoContractUnit.Plans.subjectBiin, out var planSupplierBiin);
                    DateTime.TryParse(dtoContractUnit.Plans.PlanActs?.dateApproved, out var planActDateApproved);
                    DateTime.TryParse(dtoContractUnit.Plans.dateCreate, out var planDateCreate);
                    var plan = new PlanGoszakup
                    {
                        SourceUniqueId = dtoContractUnit.Plans.Id,
                        Amount = dtoContractUnit.Plans.amount,
                        Count = dtoContractUnit.Plans.count,
                        Description = dtoContractUnit.Plans.descRu,
                        ExtraDescription = dtoContractUnit.Plans.extraDescRu,
                        Measure = dtoContractUnit.Plans.RefUnits?.nameRu,
                        Method = dtoContractUnit.Plans.RefTradeMethods?.nameRu,
                        Name = dtoContractUnit.Plans.nameRu,
                        Prepayment = dtoContractUnit.Plans.prepayment,
                        Price = dtoContractUnit.Plans.price,
                        Status = dtoContractUnit.Plans.RefPlnPointStatus?.nameRu,
                        ActNumber = dtoContractUnit.Plans.PlanActs?.planActNumber,
                        DateApproved = planActDateApproved.Year == 1 ? (DateTime?) null : planActDateApproved,
                        DateCreate = planDateCreate.Year == 1 ? (DateTime?) null : planDateCreate,
                        FinYear = dtoContractUnit.Plans.PlanActs?.planFinYear,
                        IsQvazi = dtoContractUnit.Plans.isQvazi == 1,
                        MonthId = dtoContractUnit.Plans.refMonthsId,
                        SubjectBiin = planSupplierBiin,
                        SupplyDate = dtoContractUnit.Plans.supplyDateRu,
                        TruCode = dtoContractUnit.Plans.refEnstruCode,
                        ContractUnitId = contractUnit.Id
                    };
                    await ctx.PlanGoszakup.AddAsync(plan);
                    await ctx.SaveChangesAsync();
                }
        }
    }
}
