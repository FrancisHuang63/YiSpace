using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YiSpace.Attributes
{
    public class HeaderAttribute: Attribute
    {
        /// <summary>
        /// Header Name
        /// </summary>
        public string Name { get; set; }
        public string Type { get; set; }
        public object[] EnumValues { get; set; }
        public bool Required { get; set; }
        public object Default { get; set; }
    }
}