namespace Reporting.BNB.Validation.Models
{
    public class BnbReportValidationResult
    {
        public bool IsValid { get; set; } = true;
        public List<BnbValidationResult> Errors { get; set; } = new List<BnbValidationResult>();

    }
}