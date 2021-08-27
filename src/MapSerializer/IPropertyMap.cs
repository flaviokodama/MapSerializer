using System;
using System.Linq.Expressions;

namespace MapSerializer
{
    /// <summary>
    /// Represents a map to the property type of which objects will be serialized.
    /// </summary>
    /// <typeparam name="T">Parent type.</typeparam>
    /// <typeparam name="TProp">Property type.</typeparam>
    public interface IPropertyMap<T, TProp>
    {
        /// <summary>
        /// Maps a property to be serialized.
        /// </summary>
        /// <typeparam name="TAnotherProp">Property type.</typeparam>
        /// <param name="propertyLambda">Expression used to select Property being mapped.</param>
        /// <returns><see cref="IPropertyMap{T, TProp}"/> object that allows more properties to be mapped easily.</returns>
        IPropertyMap<T, TAnotherProp> MapProperty<TAnotherProp>(Expression<Func<T, TAnotherProp>> propertyExpression);

        /// <summary>
        /// Creates a map to <see cref="TProp"/> type.
        /// </summary>
        /// <returns><see cref="ITypeMap{TProp}"/> object that allows properties be mapped.</returns>
        ITypeMap<TProp> MapType();
    }
}
