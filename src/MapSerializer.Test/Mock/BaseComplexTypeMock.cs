using System.Collections.Generic;

namespace MapSerializer.Test.Mock
{
    internal class BaseComplexTypeMock
    {
        public ComplexTypeMock ComplexProperty { get; set; }

        public ComplexTypeMock ComplexField;

        public IEnumerable<ComplexTypeMock> ComplexTypeList { get; set; }

        public ComplexTypeMock GetValue() => new ComplexTypeMock();
    }
}
