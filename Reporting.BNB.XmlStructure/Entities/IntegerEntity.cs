using Reporting.BNB.XmlStructure.Interfaces;
using System.Globalization;
using System.Xml.Linq;

namespace Reporting.BNB.XmlStructure.Entities
{
    public class IntegerEntity : Entity, IXElementEntity
    {
        public new int Value { get; set; }
        private IntegerEntity()
        {
        }

        public IntegerEntity(string tag, int? value)
        {
            Tag = tag;
            Value = value ?? 0;
        }

        public override string ToXmlString() => $@"<tr><td code=""{Tag ?? throw new ArgumentNullException(nameof(Tag))}"">{GetValueString()}</td></tr>";
        public override XElement AsXElement() => new("tr", new XElement("td", new XAttribute("code", Tag), GetValueString()));

        private string GetValueString() => Value.ToString(CultureInfo.InvariantCulture);
    }
}
