using System;
using System.Linq.Expressions;

namespace MapSerializer
{
    public interface IPropertyMap<T, TProp>
    {
        IPropertyMap<T, TAnotherProp> MapProperty<TAnotherProp>(Expression<Func<T, TAnotherProp>> propertyLambda);
        ITypeMap<TProp> MapType();
    }
}
