using Reporting.BNB.XmlStructure.Interfaces;
using System.Xml.Linq;

namespace Reporting.BNB.XmlStructure
{
    public class BnbReport : IXElementEntity
    {
        public BnbReportHeader Header { get; set; } = null!;
        public BnbReportTable Table { get; set; } = null!;
        public BnbReportFooter Footer { get; set; } = null!;

        public string ToXmlString() =>
            $@"<?xml version=""1.0"" encoding=""WINDOWS-1251""?>
            <?xml-stylesheet type=""text/xsl"" href=""{Header.ReportCode}.xsl""?>
            <all xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:noNamespaceSchemaLocation=""{Header.ReportCode}.xsd"">
            {Header.ToXmlString()}{Table.ToXmlString()}
            {Footer.ToXmlString()}</all>";

        //TODO:Not fully implemented
        public XElement AsXElement() => new("All", Header.AsXElement(), Table.AsXElement(), Footer.AsXElement());

        public string AsXmlString()
        {
            var doc = new XDocument(new XDeclaration("1.0", "WINDOWS-1251", null), AsXElement());
            var wr = new StringWriter();
            doc.Save(wr);
            return wr.ToString();
        }
    }
}
