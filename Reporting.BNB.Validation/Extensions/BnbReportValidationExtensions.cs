namespace Reporting.BNB.Validation.Extensions
{
    using Reporting.BNB.XmlStructure;
    using Reporting.BNB.XmlStructure.Entities;
    using System.Linq;

    internal static class BnbReportValidationExtensions
    {
        public static Dictionary<string, decimal> GetTagDictionary(this BnbReport bnbReport)
            => bnbReport.Table?.Table?.ToDictionary(entity => entity.Tag, entity =>
            {
                if (entity is DecimalEntity amount)
                    return amount.Value;
                if (entity is IntegerEntity count)
                    return count.Value;

                throw new Exception();
            }) ?? [];
    }
}