namespace MapSerializer.Test.Mock
{
    internal class BaseComplexTypeMock
    {
        public ComplexTypeMock ComplexProperty { get; set; }

        public ComplexTypeMock ComplexField;

        public ComplexTypeMock GetValue() => new ComplexTypeMock();
    }
}
