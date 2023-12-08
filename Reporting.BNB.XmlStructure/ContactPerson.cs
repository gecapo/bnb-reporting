using Reporting.BNB.XmlStructure.Interfaces;
using System.Text;
using System.Xml.Linq;

namespace Reporting.BNB.XmlStructure
{
    public class ContactPerson : IXElementEntity
    {
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;

        public XElement AsXElement() => new(nameof(ContactPerson),
            new XElement(nameof(Name), Name),
            new XElement(nameof(Phone), Phone),
            new XElement(nameof(Email), Email));

        public string ToXmlString() => new StringBuilder()
            .AppendLine($"<contactPerson>")
            .AppendLine($"<name>{Name ?? throw new ArgumentNullException(nameof(Name))}</name>")
            .AppendLine($"<phone>{Phone ?? throw new ArgumentNullException(nameof(Phone))}</phone>")
            .AppendLine($"<email>{Email ?? throw new ArgumentNullException(nameof(Email))}</email>")
            .AppendLine($"</contactPerson>")
            .ToString();
    }
}
