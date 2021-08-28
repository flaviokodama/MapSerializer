namespace MapSerializer.Test.Mock
{
    public class TypeWithComplexProperties
    {
        public string StringProperty { get; set; }

        public TypeWithStringProperty ComplexProperty { get; set; }

        public TypeWithStringProperty ComplexField;

        public TypeWithStringProperty GetValue() => new TypeWithStringProperty();
    }
}
