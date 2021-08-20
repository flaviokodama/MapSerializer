using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MapSerializer
{
    public class MapSerializer
    {
        internal Dictionary<Type, TypeMapBase> TypeMaps { get; }

        public MapSerializer()
        {
            this.TypeMaps = new Dictionary<Type, TypeMapBase>();
        }

        public ITypeMap<T> MapType<T>()
        {
            var type = typeof(T);

            if (this.TypeMaps.ContainsKey(type))
                throw new InvalidOperationException($"Type '{type.FullName}' is already mapped.");

            var typeMap = new TypeMap<T>(this);
            this.TypeMaps.Add(type, typeMap);

            return typeMap;
        }

        public void Serialize(TextWriter writer, object reference)
        {
            if (reference == null)
                return;

            var type = reference.GetType();

            if (IsMapped(type))
                SerializeMappedType(writer, reference, this.TypeMaps[type]);
            else
                writer.Write($"<{type.Name}>{reference}</{type.Name}>");
        }

        private bool IsMapped(Type type)
        {
            return this.TypeMaps.ContainsKey(type);
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
                if (IsNativeType(propertyInfo))
                    writer.Write(value);
                else if (IsEnumerable(propertyInfo))
                    SerializeEnumerable(writer, value);
                else
                    Serialize(writer, value);
            }

            writer.Write($"</{propertyInfo.Name}>");
        }

        private static bool IsNativeType(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.IsPrimitive ||
                   propertyInfo.PropertyType.Equals(typeof(string)) ||
                   propertyInfo.PropertyType.Equals(typeof(decimal)) ||
                   propertyInfo.PropertyType.Equals(typeof(DateTime));
        }

        private static bool IsEnumerable(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.IsAssignableFrom(typeof(IEnumerable)) ||
                   propertyInfo.PropertyType.GetInterfaces().Contains(typeof(IEnumerable));
        }

        private void SerializeEnumerable(TextWriter writer, object value)
        {
            var enumerable = value as IEnumerable;
            if (enumerable != null)
            {
                foreach (var item in enumerable)
                    Serialize(writer, item);
            }
        }
    }
}
