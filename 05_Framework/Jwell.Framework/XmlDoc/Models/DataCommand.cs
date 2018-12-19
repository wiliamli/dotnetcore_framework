using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Jwell.Framework.XmlDoc.Models
{
    public class DataCommand
    {
        [XmlAttribute("name")]
        public string Name { get; set; }


        [XmlAttribute("dbName")]
        public string DbName { get; set; }

        /// <summary>
        /// Sql文本
        /// </summary>
        [XmlElement("commandText")]
        public string CommandText { get; set; }
    }
}
