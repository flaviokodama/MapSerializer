using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            if (IsMapped(type))
                SerializeMappedType(writer, reference, this.MappedTypes[type]);
            else
                writer.Write($"<{type.Name}>{reference}</{type.Name}>");
        }

        private bool IsMapped(Type type)
        {
            return this.MappedTypes.ContainsKey(type);
        }

        private void SerializeMappedType(TextWriter writer, object reference, TypeMapBase map)
        {
            writer.Write($"<{map.Type.Name}>");

            foreach (var propMap in map.MappedProperties)
                SerializeMappedProperty(writer, propMap.PropertyInfo, reference);

            writer.Write($"</{map.Type.Name}>");
        }

        private void SerializeMappedProperty(TextWriter writer, PropertyInfo propertyInfo, object reference)
        {
            writer.Write($"<{propertyInfo.Name}>");

            var value = propertyInfo.GetValue(reference);
            if (value != null)
            {
                if (IsNativeType(propertyInfo.PropertyType))
                    writer.Write(value);
                else if (IsEnumerable(propertyInfo.PropertyType))
                    SerializeEnumerable(writer, value);
                else
                    Serialize(writer, value);
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
