using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PleskApi.BaseModels;

namespace PleskApi
{
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = true)]
    public class PleskXmlDataTypeAttribute : System.Attribute
    {
        public ModelXmlDataType DataType { get; }

        public PleskXmlDataTypeAttribute(ModelXmlDataType dataType)
        {
            DataType = dataType;
        }
    }
}
