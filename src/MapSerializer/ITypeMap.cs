using System;
using System.Linq.Expressions;

namespace MapSerializer
{
    public interface ITypeMap<T>
    {
        IPropertyMap<T, TProp> MapProperty<TProp>(Expression<Func<T, TProp>> propertyExpression);
    }
}
