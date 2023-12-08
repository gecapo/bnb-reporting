using Reporting.BNB.XmlStructure.Interfaces;
using System.Xml.Linq;

namespace Reporting.BNB.XmlStructure
{
    public class BnbReportFooter : IXElementEntity
    {
        public required ContactPerson Person { get; set; }
        public XElement ToXmlString() => Person.AsXElement();
        public XElement AsXElement() => Person.AsXElement();

    }
}
