using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MapSerializer
{
    internal class PropertyMap<T, TProp> : PropertyMapBase, IPropertyMap<T, TProp>
    {
        private TypeMap<T> typeMap;
        private MapSerializerBase serializer;

        internal PropertyMap(TypeMap<T> typeMap, MapSerializerBase serializer, PropertyInfo propertyInfo) : base(propertyInfo)
        {
            this.typeMap = typeMap;
            this.serializer = serializer;
        }

        public IPropertyMap<T, TAnotherProp> MapProperty<TAnotherProp>(Expression<Func<T, TAnotherProp>> propertyLambda)
        {
            return this.typeMap.MapProperty(propertyLambda);
        }

        public ITypeMap<TProp> MapType()
        {
            return serializer.MapType<TProp>();
        }
    }
}
