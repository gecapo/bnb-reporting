namespace Reporting.BNB.Validation
{
    using Microsoft.Extensions.Configuration;
    using Reporting.BNB.Validation.Extensions;
    using Reporting.BNB.Validation.Interfaces;
    using Reporting.BNB.Validation.Models;
    using Reporting.BNB.XmlStructure;
    using Superpower;
    using Superpower.Model;
    using System.Linq;

    public class BnbReportValidator : IBnbReportValidator
    {
        private readonly List<string> _expressionsStrings;
        private readonly BnbValidationTokenizer _tokenizer = new();

        //TODO: This is only test for now
        public BnbReportValidator(IConfiguration configuration)
        {
            try
            {
                _expressionsStrings = configuration!.GetSection("ValidationRules")!.Exists() ? configuration?.GetSection("ValidationRules")?.Get<List<string>>() : new List<string>();
            }
            catch
            {
                throw new ArgumentException("No Validation Rules Found");
            }
        }

        public BnbReportValidationResult Validate(params BnbReport[] reports)
        {
            var result = new BnbReportValidationResult();
            foreach (var report in reports)
            {
                var results = Validate(report);
                result.IsValid &= result.IsValid;
                result.Errors.AddRange(results.Errors);
            }

            return result;
        }

        public BnbReportValidationResult Validate(BnbReport report)
        {
            var result = new BnbReportValidationResult();
            var tags = report.GetTagDictionary();
            foreach (var expressionString in _expressionsStrings)
            {
                var expressionResult = Validate(expressionString, tags);
                result.IsValid &= expressionResult.IsValid ?? true;
                if (expressionResult.IsValid == false)
                    result.Errors.Add(expressionResult);
            }

            return result;
        }

        private BnbValidationResult Validate(string expressionString, Dictionary<string, decimal> tags)
        {
            var tokens = _tokenizer.Tokenize(expressionString).Select(token =>
            {
                if (token.Kind is BnbValidationTokenType.Parameter)
                    return new Token<BnbValidationTokenType>(
                            BnbValidationTokenType.Parameter,
                            new TextSpan(tags[token.ToStringValue()].ToString()));
                else
                    return token;
            }).ToArray();

            var expression = BnbValidationExpressionParser.Lambda.Parse(new TokenList<BnbValidationTokenType>(tokens));
            var expressionResult = expression.Compile()();
            return new BnbValidationResult
            {
                IsValid = expressionResult,
                ExpressionsReal = expression.ToString(),
                ExpressionsString = expressionString
            };
        }
    }
}