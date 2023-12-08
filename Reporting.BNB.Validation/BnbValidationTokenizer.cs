namespace Reporting.BNB.Validation
{
    using Reporting.BNB.Validation.Models;
    using Superpower;
    using Superpower.Model;

    internal class BnbValidationTokenizer : Tokenizer<BnbValidationTokenType>
    {
        private readonly List<char> _operations = new() { '=', '>', '+' };
        protected override IEnumerable<Result<BnbValidationTokenType>> Tokenize(TextSpan input)
        {
            var next = SkipWhiteSpace(input);
            if (!next.HasValue)
                yield break;

            do
            {
                TextSpan keywordStart = next.Location;
                switch (next.Value)
                {
                    case '=':

                        yield return Result.Value(BnbValidationTokenType.Equals, next.Location, next.Remainder);
                        next = next.Remainder.ConsumeChar();
                        break;

                    case '>':

                        do
                        {
                            next = next.Remainder.ConsumeChar();
                        } while (next.HasValue && next.Value == _operations.First());
                        yield return Result.Value(BnbValidationTokenType.GreaterAndEquals, keywordStart, next.Location);
                        break;

                    case '+':

                        yield return Result.Value(BnbValidationTokenType.Plus, next.Location, next.Remainder);
                        next = next.Remainder.ConsumeChar();
                        break;

                    default:

                        do
                        {
                            next = next.Remainder.ConsumeChar();
                        } while (next.HasValue && !_operations.Contains(next.Value));
                        yield return Result.Value(BnbValidationTokenType.Parameter, keywordStart, next.Location);
                        break;
                }

                next = SkipWhiteSpace(next.Location);
            } while (next.HasValue);
        }
    }
}