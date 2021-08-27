using System;
using System.Linq.Expressions;

namespace MapSerializer
{
    /// <summary>
    /// Represents a map to the type of which objects will be serialized.
    /// </summary>
    /// <typeparam name="T">Type marked to serialization.</typeparam>
    public interface ITypeMap<T>
    {
        /// <summary>
        /// Maps a property to be serialized.
        /// </summary>
        /// <typeparam name="TProp">Property type.</typeparam>
        /// <param name="propertyExpression">Expression used to select a property to be serialized.</param>
        /// <returns>Returns an <see cref="IPropertyMap{T, TProp}"/> object that allows more properties to be mapped easily.</returns>
        IPropertyMap<T, TProp> MapProperty<TProp>(Expression<Func<T, TProp>> propertyExpression);
    }
}
