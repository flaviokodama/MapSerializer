using System;
using FluentAssertions;
using Xunit;

namespace MapSerializer.Test
{
    public class MapSerializerTest
    {
        private MapSerializer serializer;

        public MapSerializerTest()
        {
            this.serializer = new MapSerializer();
        }

        [Fact]
        public void MapSerializer_MustCheckWhether_TypeIsAlreadyMapped()
        {
            //SETUP
            var instance = new TestTypeMock() { ComplexProperty = new ComplexTypeMock() };

            //ACTION
            var action = new Action(delegate
            {
                this.serializer.MapType<TestTypeMock>();
                this.serializer.MapType<TestTypeMock>();
            });

            //CHECK
            action.Should().Throw<InvalidOperationException>().WithMessage("Type 'MapSerializer.Test.TestTypeMock' is already mapped.");
        }

        [Fact]
        public void MapSerializer_MustCheckWhether_MappedPropertyIsReadable()
        {
            //SETUP
            var instance = new TestTypeMock() { ComplexField = new ComplexTypeMock() };

            //ACTION
            var action = new Action(delegate
            {
                this.serializer.MapType<TestTypeMock>().MapProperty(p => p.ComplexField);
            });

            //CHECK
            action.Should().Throw<ArgumentException>().WithMessage("Invalid expression 'p => p.ComplexField': reference to fields is not supported.");
        }

        [Fact]
        public void MapSerializer_MustCheckWhether_MappedMemberIsMethod()
        {
            //SETUP
            var instance = new TestTypeMock();

            //ACTION
            var action = new Action(delegate
            {
                this.serializer.MapType<TestTypeMock>().MapProperty(p => p.GetValue());
            });

            //CHECK
            action.Should().Throw<ArgumentException>().WithMessage("Invalid expression 'p => p.GetValue()': reference to methods is not supported.");
        }
    }

    public class TestTypeMock
    {
        public ComplexTypeMock ComplexProperty { get; set; }

        public ComplexTypeMock ComplexField;

        public ComplexTypeMock GetValue() => new ComplexTypeMock();
    }

    public class ComplexTypeMock
    {
        public string StringType { get; set; }
        public DateTime DateTimeType { get; set; }
        public double DoubleType { get; set; }
        public int IntegerType { get; set; }
    }
    
    public class AnotherComplexTypeMock
    {

    }
}
