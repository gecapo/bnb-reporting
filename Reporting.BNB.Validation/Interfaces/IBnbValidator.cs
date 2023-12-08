namespace Reporting.BNB.Validation.Interfaces
{
    using Reporting.BNB.Validation.Models;
    using Reporting.BNB.XmlStructure;

    public interface IBnbReportValidator
    {
        BnbReportValidationResult Validate(BnbReport report);
        BnbReportValidationResult Validate(params BnbReport[] report);
    }
}