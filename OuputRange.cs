using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfApplication1
{
    [XmlType(TypeName = "ORng")]
    public class OutputRange
    {

        [XmlAttribute(AttributeName = "min")]
        public uint Min;
        [XmlAttribute(AttributeName = "max")]
        public uint Max;

        public OutputRange()
            : this(0, 0)
        { }

        public OutputRange(uint min, uint max)
        {
            Min = min;
            Max = max;
        }

    }
}
