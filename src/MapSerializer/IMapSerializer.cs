using System.IO;

namespace MapSerializer
{
    public interface IMapSerializer
    {
        ITypeMap<T> MapType<T>();
        void Serialize(TextWriter writer, object reference);
    }
}
