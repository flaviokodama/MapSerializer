using System.Xml.Serialization;

namespace MapSerializer.Test.Mock
{
    [XmlInclude(typeof(TypeConcrete))]
    public class TypeWithAbstractProperty
    {
        public TypeAbstract AbstractProperty { get; set; }
    }
}
