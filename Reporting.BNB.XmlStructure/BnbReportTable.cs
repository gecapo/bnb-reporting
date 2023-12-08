using Reporting.BNB.XmlStructure.Entities;
using Reporting.BNB.XmlStructure.Interfaces;
using System.Xml.Linq;

namespace Reporting.BNB.XmlStructure
{
    public class BnbReportTable : IXElementEntity
    {
        public IEnumerable<Entity>? Table { get; set; }

        public string ToXmlString() => Table == null ? string.Empty :
        $@"<table>
            {string.Join(Environment.NewLine, Table.Select(entity => entity.ToXmlString()))}
        </table>";

        public XElement AsXElement() => new(nameof(Table), Table == null ? string.Empty : Table.Select(entity => entity.AsXElement()));
    }
}
