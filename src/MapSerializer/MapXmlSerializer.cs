using System;
using System.Collections;
using System.IO;
using System.Reflection;

namespace MapSerializer
{

    public sealed class MapXmlSerializer : MapSerializerBase, IMapSerializer
    {
        public override void Serialize(TextWriter writer, object reference)
        {
            if (reference == null)
                return;

            var type = reference.GetType();
            var typeName = IsNativeType(type) ? NormalizeName(type) : type.Name;

            writer.Write($"<{typeName}>");

            SerializeWithoutTypeName(writer, reference);

            writer.Write($"</{typeName}>");
        }

        private static string NormalizeName(Type type)
        {
            if(IsNumeric(type))
            {
                return type.Name.ToLowerInvariant().Trim('1', '2', '3', '4', '6');
            }
            return type.Name.ToLowerInvariant();
        }

        private void SerializeWithoutTypeName(TextWriter writer, object reference)
        {
            var type = reference.GetType();

            if (IsEnumerable(type))
                SerializeEnumerable(writer, reference);
            else if (IsMapped(type))
                SerializeMappedType(writer, reference, this.MappedTypes[type]);
            else
                writer.Write(reference);
        }

        private bool IsMapped(Type type)
        {
            return this.MappedTypes.ContainsKey(type);
        }

        private void SerializeMappedType(TextWriter writer, object reference, TypeMapBase map)
        {
            foreach (var propMap in map.MappedProperties)
                SerializeMappedProperty(writer, propMap.PropertyInfo, reference);
        }

        private void SerializeMappedProperty(TextWriter writer, PropertyInfo propertyInfo, object reference)
        {
            writer.Write($"<{propertyInfo.Name}>");

            var value = propertyInfo.GetValue(reference);
            if (value != null)
            {
                if (IsNativeType(propertyInfo.PropertyType))
                {
                    if (IsDateTime(propertyInfo.PropertyType))
                        writer.Write(value.ToDateTimeString());
                    else
                        writer.Write(value);
                }
                else if (IsEnumerable(propertyInfo.PropertyType))
                {
                    SerializeEnumerable(writer, value);
                }
                else
                {
                    SerializeWithoutTypeName(writer, value);
                }
            }

            writer.Write($"</{propertyInfo.Name}>");
        }

        private void SerializeEnumerable(TextWriter writer, object value)
        {
            var enumerable = value as IEnumerable;

            foreach (var item in enumerable)
                Serialize(writer, item);
        }
    }
}
