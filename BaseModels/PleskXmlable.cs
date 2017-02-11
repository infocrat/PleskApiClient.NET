using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;

using PleskApi;

namespace PleskApi.BaseModels
{
    /// <summary>
    /// Base class which implements serialization of plesk model properties which has PleskXmlLabelAttribute
    /// </summary>
    public class PleskXmlable
    {
        public List<XElement> SerializeProperties()
        {
            return this.GetType().GetProperties()
                .Where(p => p.CanWrite)
                .Where(p => p.GetValue(this)!=null)
                .Where(p => !String.IsNullOrWhiteSpace(p.GetValue(this).ToString()))
                .Select(p =>
                    new XElement(p.GetCustomAttribute<PleskXmlLabelAttribute>().Label, ValueToXmlObject(p.GetValue(this)))
                )
                .ToList();
        }

        public List<XElement> SerializeProperties(ModelXmlDataType dataType)
        {
            return this.GetType().GetProperties()
                .Where(p => p.CanWrite)
                .Where(p => p.GetValue(this) != null)
                .Where(p => !String.IsNullOrWhiteSpace(p.GetValue(this).ToString()))
                .Where(p => p.GetCustomAttribute<PleskXmlDataTypeAttribute>().DataType == dataType)
                .Select(p =>
                    new XElement(p.GetCustomAttribute<PleskXmlLabelAttribute>().Label, ValueToXmlObject(p.GetValue(this)))
                )
                .ToList();
        }

        public PleskXmlable DeserializeProperties(List<XElement> xml, ModelXmlDataType dataType)
        {
            foreach (var property in this.GetType().GetProperties()
                .Where(p => p.CanWrite)
                .Where(p => p.GetCustomAttributes<PleskXmlDataTypeAttribute>().Any(a => a.DataType == dataType))
                .Where(p => xml.Any(x => x.Name == p.GetCustomAttribute<PleskXmlLabelAttribute>().Label)))
            {
                var val = xml.FirstOrDefault(e => e.Name == property.GetCustomAttribute<PleskXmlLabelAttribute>().Label)?.Value;
                property.SetValue(this, XmlObjectToValue(val, property.PropertyType));
            }
            return this;
        }

        private object ValueToXmlObject(object value)
        {
            if (value.GetType().IsEnum)
            {
                if (value.GetType().GetCustomAttribute<PleskXmlEnumTypeAttribute>().IsInt)
                    return (int)value;
                else
                    return value.ToString();
            }
            else return value;
        }

        private object XmlObjectToValue(string obj, Type type)
        {
            if (obj == null) return null;

            if (IsNullable(type))
            {
                // Move to underlying type from nullable
                type = Nullable.GetUnderlyingType(type);

                if (type == typeof(DateTime))
                    return DateTime.Parse(obj.ToString());
                else if (type.IsEnum)
                {
                    if (type.GetCustomAttribute<PleskXmlEnumTypeAttribute>().IsInt)
                        return Enum.ToObject(type, Convert.ToInt32(obj));
                    else
                        return Enum.Parse(type, obj);
                }
                else if (type == typeof(int))
                    return Convert.ToInt32(obj);
                else return obj;
            }
            else if (type.IsEnum)
            {
                if (type.GetCustomAttribute<PleskXmlEnumTypeAttribute>().IsInt)
                    return Enum.ToObject(type, Convert.ToInt32(obj));
                else
                    return Enum.Parse(type, obj);
            }
            else if (type == typeof(DateTime))
                return DateTime.Parse(obj);
            else
                return obj;
        }

        private bool IsNullable(Type type)
        {
            if (type == typeof(String)) return false;

            return type.IsClass || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        private bool IsNullableEnum(Type type)
        {
            if (IsNullable(type))
                if (Nullable.GetUnderlyingType(type).IsEnum)
                    return true;
                else
                    return false;
            else
                return false;
        }

        /*public List<XElement> SerializeProperties(List<string> toInclude = null, List<string> toExclude = null)
        {
            if (toInclude == null && toExclude == null)
                return SerializeProperties();

            if (toInclude != null && toExclude != null)
                return SerializeProperties(toInclude, null);

            return this.GetType().GetProperties()
                .Where(p => p.CanWrite)
                .Where(p => p.GetValue(this) != null)
                .Where(p => !String.IsNullOrWhiteSpace(p.GetValue(this).ToString()))
                .Where(p => toInclude!=null && toInclude.Any(i => i == p.GetCustomAttribute<PleskXmlLabelAttribute>().Label))
                .Where(p => toExclude!=null && !toExclude.Any(i => i == p.GetCustomAttribute<PleskXmlLabelAttribute>().Label))
                .Select(p =>
                    new XElement(p.GetCustomAttribute<PleskXmlLabelAttribute>().Label, p.GetValue(this))
                )
                .ToList();
        }*/
    }
}
