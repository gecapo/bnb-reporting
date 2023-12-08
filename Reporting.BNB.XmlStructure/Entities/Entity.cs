using Reporting.BNB.XmlStructure.Interfaces;
using System.Xml.Linq;

namespace Reporting.BNB.XmlStructure.Entities
{
    public abstract class Entity : IXElementEntity
    {
        public required string Tag { get; set; }
        public object? Value { get; set; }

        public abstract string ToXmlString();
        public abstract XElement AsXElement();
    }

}