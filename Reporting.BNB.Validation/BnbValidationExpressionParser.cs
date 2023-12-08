namespace Reporting.BNB.Validation
{
    using Reporting.BNB.Validation.Models;
    using Superpower;
    using Superpower.Parsers;
    using System.Linq.Expressions;

    internal class BnbValidationExpressionParser
    {
        static readonly TokenListParser<BnbValidationTokenType, ExpressionType> Add =
            Token.EqualTo(BnbValidationTokenType.Plus).Value(ExpressionType.Add);

        static readonly TokenListParser<BnbValidationTokenType, ExpressionType> ConditionalGreater =
            Token.EqualTo(BnbValidationTokenType.Greater).Value(ExpressionType.GreaterThan);

        static readonly TokenListParser<BnbValidationTokenType, ExpressionType> ConditionalGreaterAndEquals =
        Token.EqualTo(BnbValidationTokenType.GreaterAndEquals).Value(ExpressionType.GreaterThanOrEqual);

        static readonly TokenListParser<BnbValidationTokenType, ExpressionType> ConditionalEqual =
            Token.EqualTo(BnbValidationTokenType.Equals).Value(ExpressionType.Equal);

        static readonly TokenListParser<BnbValidationTokenType, Expression> Constant =
                Token.EqualTo(BnbValidationTokenType.Parameter)
                .Apply(Numerics.DecimalDecimal)
                .Select(n => (Expression)Expression.Constant(n));

        public static readonly TokenListParser<BnbValidationTokenType, Expression> Term =
           Parse.Chain(Add, Constant, Expression.MakeBinary);

        public static readonly TokenListParser<BnbValidationTokenType, Expression> Expr =
            Parse.Chain(ConditionalGreater.Or(ConditionalGreaterAndEquals).Or(ConditionalEqual), Term, Expression.MakeBinary);

        public static readonly TokenListParser<BnbValidationTokenType, Expression<Func<bool>>>
           Lambda = Expr.AtEnd().Select(body => Expression.Lambda<Func<bool>>(body));
    }
}
