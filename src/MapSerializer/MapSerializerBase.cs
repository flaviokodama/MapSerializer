using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("MapSerializer.Test")]
namespace MapSerializer
{
    public abstract class MapSerializerBase : IMapSerializer
    {
        internal Dictionary<Type, TypeMapBase> MappedTypes;

        public MapSerializerBase()
        {
            this.MappedTypes = new Dictionary<Type, TypeMapBase>();
        }

        public ITypeMap<T> MapType<T>()
        {
            var type = typeof(T);

            if (this.MappedTypes.ContainsKey(type))
                throw new InvalidOperationException($"Type '{type.FullName}' is already mapped.");

            var typeMap = new TypeMap<T>(this);
            this.MappedTypes.Add(type, typeMap);

            return typeMap;
        }

        public abstract void Serialize(TextWriter writer, object reference);

        internal static bool IsNativeType(Type type)
        {
            return type.IsPrimitive ||
                   type.IsEnum ||
                   type.Equals(typeof(string)) ||
                   type.Equals(typeof(decimal)) ||
                   type.Equals(typeof(DateTime));
        }

        internal static bool IsDateTime(Type type)
        {
            return type.Equals(typeof(DateTime));
        }

        internal static bool IsEnumerable(Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type);// ||
                   //type.GetInterfaces().Contains(typeof(IEnumerable));
        }

        internal static bool IsNumeric(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}
