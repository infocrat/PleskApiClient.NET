using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PleskApi
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class PleskXmlLabelAttribute : System.Attribute
    {
        public string Label { get; }

        public PleskXmlLabelAttribute(string label)
        {
            Label = label;
        }
    }
}
