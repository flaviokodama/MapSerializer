using System.IO;

namespace MapSerializer
{
    /// <summary>
    /// Represents a serializer that works by writing mapped types and their properties.
    /// </summary>
    public interface IMapSerializer
    {
        /// <summary>
        /// Creates a map to type of which objects will be serialized.
        /// </summary>
        /// <typeparam name="T">Type being mapped.</typeparam>
        /// <returns><see cref="ITypeMap{T}"/> object that allows properties be mapped.</returns>
        ITypeMap<T> MapType<T>();

        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <param name="writer">Destination text writer.</param>
        /// <param name="reference">Object reference.</param>
        void Serialize(TextWriter writer, object reference);
    }
}
