using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MapSerializer.Test
{
    public static class ComparisonSerializer
    {
        public static string SerializeToJson<T>(T instance)
        {
            using (var writer = new StringWriter())
            {
                var xmlSerializer = new JsonSerializer();
                xmlSerializer.Serialize(writer, instance);

                return writer.ToString();
            }
        }

        public static string SerializeToXml<T>(T instance)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(T));
            var settings = new XmlWriterSettings();
            settings.Indent = false;
            settings.OmitXmlDeclaration = true;

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, instance, emptyNamespaces);

                return RemoveNamespaces(stream.ToString());
            }
        }

        private static string RemoveNamespaces(string xml)
        {
            var document = XDocument.Parse(xml);
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";

            document.Descendants()
                    .Attributes(xsi + "type")
                    .Remove();

            document.Descendants()
                    .Attributes()
                    .Where(a => a.IsNamespaceDeclaration && a.Value == xsi)
                    .Remove();

            return document.ToString(SaveOptions.DisableFormatting);
        }
    }
}
