using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MapSerializer
{
    public sealed class MapJsonSerializer : MapSerializerBase, IMapSerializer
    {
        public override void Serialize(TextWriter writer, object reference)
        {
            if (reference == null)
                return;

            var type = reference.GetType();

            if (IsMapped(type))
                SerializeMappedType(writer, reference, this.MappedTypes[type]);
            else
                writer.Write($"\"{type.Name}\":\"{reference}\"");
        }

        public void SerializeNonRoot(TextWriter writer, object reference)
        {
            var type = reference.GetType();

            if (IsMapped(type))
                SerializeMappedTypeNonRoot(writer, reference, this.MappedTypes[type]);
            else
                writer.Write($"\"{type.Name}\":\"{reference}\"");
        }

        private bool IsMapped(Type type)
        {
            return this.MappedTypes.ContainsKey(type);
        }

        private void SerializeMappedType(TextWriter writer, object reference, TypeMapBase map)
        {
            writer.Write("{");
            map.MappedProperties.ForEachAndBetween(item => SerializeMappedProperty(writer, item.PropertyInfo, reference), () => writer.Write(","));
            writer.Write("}");
        }

        private void SerializeMappedTypeNonRoot(TextWriter writer, object reference, TypeMapBase map)
        {
            writer.Write($"\"{map.Type.Name}\":{{");

            map.MappedProperties.ForEachAndBetween(item => SerializeMappedProperty(writer, item.PropertyInfo, reference), () => writer.Write(","));

            writer.Write("}");
        }

        private void SerializeMappedProperty(TextWriter writer, PropertyInfo propertyInfo, object reference)
        {
            writer.Write($"\"{propertyInfo.Name}\":");

            var value = propertyInfo.GetValue(reference);
            if (value != null)
            {
                if (IsNativeType(propertyInfo.PropertyType))
                {
                    if (IsNumeric(propertyInfo.PropertyType))
                        writer.Write(value);
                    else
                        writer.Write($"\"{value}\"");
                }
                else if (IsEnumerable(propertyInfo.PropertyType))
                {
                    writer.Write("[");
                    SerializeEnumerable(writer, value);
                    writer.Write("]");
                }
                else
                {
                    writer.Write("{");
                    SerializeNonRoot(writer, value);
                    writer.Write("}");
                }
            }
            else
            {
                writer.Write($"null");
            }
        }

        private void SerializeEnumerable(TextWriter writer, object value)
        {
            var enumerable = value as IEnumerable;
            enumerable.ForEachAndBetween(item => SerializeNonRoot(writer, item), () => writer.Write(","));
        }
    }
}
