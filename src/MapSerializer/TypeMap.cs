using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MapSerializer
{
    internal class TypeMap<T> : TypeMapBase, ITypeMap<T>
    {
        private MapSerializer serializer;

        internal TypeMap(MapSerializer serializer)
        {
            this.serializer = serializer;
            this.Type = typeof(T);
        }

        public IPropertyMap<T, TProp> MapProperty<TProp>(Expression<Func<T, TProp>> propertyExpression)
        {
            var member = propertyExpression.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException($"Invalid expression '{propertyExpression}': reference to methods is not supported.");

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException($"Invalid expression '{propertyExpression}': reference to fields is not supported.");

            if (this.Type != propInfo.ReflectedType &&
               !this.Type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException($"Invalid expression '{propertyExpression}': reference is not of type '{this.Type}'.");

            var newPropertyMap = new PropertyMap<T, TProp>(this, this.serializer, propInfo);
            this.MappedProperties.Add(newPropertyMap);

            return newPropertyMap;
        }
    }
}
