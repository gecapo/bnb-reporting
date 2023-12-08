namespace Reporting.BNB.Validation.Models
{
    public class BnbValidationResult
    {
        public string? ExpressionsString { get; set; }
        public string? ExpressionsReal { get; set; }
        public bool? IsValid { get; set; }
    }
}