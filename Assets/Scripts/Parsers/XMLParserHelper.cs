using System.Xml;
using System.ComponentModel;

namespace Assets.Parsers
{
    public static class XMLParserHelper
    {
        public static T BasicDataGrab<T>(XmlReader reader)
        {
            reader.Read();
            TypeConverter convert = TypeDescriptor.GetConverter(typeof(T));
            return (T)convert.ConvertFromString(reader.Value.Trim());
        }
    }
}