using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PleskApi
{
    [System.AttributeUsage(System.AttributeTargets.Enum)]
    public class PleskXmlEnumTypeAttribute : System.Attribute
    {
        public bool IsInt { get; }

        public PleskXmlEnumTypeAttribute(bool isInt)
        {
            IsInt = isInt;
        }
    }
}
