using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Jwell.Framework.XmlDoc.Models
{
    [XmlRoot("dataRoot")]
    public class DataRoot
    {
        [XmlElement("dataCommand")]
        public DataCommand[] DataCommand { get; set; }
    }
}
