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

            if (IsEnumerable(type))
                SerializeEnumerable(writer, reference);
            else if (IsMapped(type))
                SerializeMappedTypeWithBracket(writer, reference, this.MappedTypes[type]);
            else
                writer.Write($"\"{type.Name}\":\"{reference}\"");
        }

        private void SerializeWithBracket(TextWriter writer, object reference)
        {
            var type = reference.GetType();

            if (IsMapped(type))
            {
                writer.Write("{");
                SerializeMappedType(writer, reference, this.MappedTypes[type]);
                writer.Write("}");
            }
            else
            {
                writer.Write($"\"{reference}\"");
            }
        }

        private bool IsMapped(Type type)
        {
            return this.MappedTypes.ContainsKey(type);
        }

        private void SerializeMappedTypeWithBracket(TextWriter writer, object reference, TypeMapBase map)
        {
            writer.Write("{");
            SerializeMappedType(writer, reference, map);
            writer.Write("}");
        }

        private void SerializeMappedType(TextWriter writer, object reference, TypeMapBase map)
        {
            map.MappedProperties.ForEachAndBetween(item => SerializeMappedProperty(writer, item.PropertyInfo, reference), () => writer.Write(","));
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
                    else if (IsDateTime(propertyInfo.PropertyType))
                        writer.Write($"\"{value.ToDateTimeString()}\"");
                    else
                        writer.Write($"\"{value}\"");
                }
                else if (IsPrimitiveEnumerable(propertyInfo.PropertyType))
                {
                    SerializePrimitiveEnumerable(writer, value);
                }
                else if (IsEnumerable(propertyInfo.PropertyType))
                {
                    SerializeEnumerable(writer, value);
                }
                else
                {
                    SerializeWithBracket(writer, value);
                }
            }
            else
            {
                writer.Write($"null");
            }
        }

        private void SerializeEnumerable(TextWriter writer, object value)
        {
            writer.Write("[");

            var enumerable = value as IEnumerable;
            enumerable.ForEachAndBetween(item => Serialize(writer, item), () => writer.Write(","));

            writer.Write("]");
        }

        private void SerializePrimitiveEnumerable(TextWriter writer, object value)
        {
            writer.Write("[");

            var enumerable = value as IEnumerable;
            enumerable.ForEachAndBetween(item => SerializeWithBracket(writer, item), () => writer.Write(","));

            writer.Write("]");
        }
    }
}
