using Reporting.BNB.XmlStructure.Interfaces;
using System.Globalization;
using System.Xml.Linq;

namespace Reporting.BNB.XmlStructure.Entities
{
    public class DecimalEntity : Entity, IXElementEntity
    {
        public new decimal Value { get; set; }
        public int DecimalPrecision { get; set; } = 2;

        private DecimalEntity()
        {
        }

        public DecimalEntity(string tag, decimal? value = null, int? precision = null)
        {
            Tag = tag;
            Value = value ?? 0;
            DecimalPrecision = precision ?? 0;
        }

        public override string ToXmlString() => $@"<tr><td code=""{Tag ?? throw new ArgumentNullException(nameof(Tag))}"">{GetValueString()}</td></tr>";
        public override XElement AsXElement() => new("tr", new XElement("td", new XAttribute("code", Tag), GetValueString()));

        private string GetValueString() => Value.ToString($"F{DecimalPrecision}", CultureInfo.InvariantCulture);
    }
}