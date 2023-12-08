using Reporting.BNB.XmlStructure.Interfaces;
using System.Text;
using System.Xml.Linq;

namespace Reporting.BNB.XmlStructure
{
    public class BnbReportHeader : IXElementEntity
    {
        public required string ReportCode { get; set; }
        public required string UnitCode { get; set; } = null!;
        public required string UnitName { get; set; } = null!;
        public string? Eik { get; set; }
        public required string CountryOfOrigin { get; set; }
        public required DateTime PeriodFrom { get; set; }
        public required DateTime PeriodTo { get; set; }

        public string ToXmlString() => new StringBuilder()
            .Append($"<header>")
            .Append($"<reportCode>{ReportCode ?? throw new ArgumentNullException(nameof(ReportCode))}</reportCode>")
            .Append($"<unitCode>{UnitCode ?? throw new ArgumentNullException(nameof(UnitCode))}</unitCode>")
            .Append($"{(Eik != null ? $"<EIK>{Eik}</EIK>" : "")}")
            .Append($"{(CountryOfOrigin != null ? $"<countryOfOrigin>{CountryOfOrigin}</countryOfOrigin>" : "")}")
            .Append($"<unitName>{UnitName ?? throw new ArgumentNullException(nameof(CountryOfOrigin))}</unitName>")
            .Append($"<periodFrom>{PeriodFrom.ToString("dd.MM.yyyy") ?? throw new ArgumentNullException(nameof(PeriodFrom))}</periodFrom>")
            .Append($"<periodTo>{PeriodTo.ToString("dd.MM.yyyy") ?? throw new ArgumentNullException(nameof(PeriodTo))}</periodTo>")
            .Append($"</header>")
            .ToString();

        public XElement AsXElement() => new("Header",
            new XElement(nameof(ReportCode), ReportCode),
            new XElement(nameof(UnitCode), UnitCode),
            new XElement(nameof(UnitName), UnitName),
            new XElement(nameof(Eik), Eik),
            new XElement(nameof(CountryOfOrigin), CountryOfOrigin),
            new XElement(nameof(PeriodFrom), PeriodFrom.ToString("dd.MM.yyyy")),
            new XElement(nameof(PeriodTo), PeriodTo.ToString("dd.MM.yyyy")));
    }
}
