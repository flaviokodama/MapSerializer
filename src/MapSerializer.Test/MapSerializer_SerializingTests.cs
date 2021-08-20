using System;
using System.IO;
using FluentAssertions;
using Xunit;

namespace MapSerializer.Test
{
    public class MapSerializer_SerializingTests
    {
        private MapSerializer serializer;

        public MapSerializer_SerializingTests()
        {
            this.serializer = new MapSerializer();
        }

        [Fact]
        public void MapSerializer_MustSerialize_StringType_ToTextRepresentation_WithoutTypeTag()
        {
            //SETUP
            var writer = new StringWriter();
            var instance = new ComplexTypeMock() { StringProperty = "123" };

            this.serializer.MapType<ComplexTypeMock>().MapProperty(p => p.StringProperty);

            //ACTION
            this.serializer.Serialize(writer, instance);
            var serializedContent = writer.ToString();

            //CHECK
            serializedContent.Should().Be("<ComplexTypeMock><StringProperty>123</StringProperty></ComplexTypeMock>");
        }

        [Fact]
        public void MapSerializer_MustSerialize_IntegerType_ToTextRepresentation_WithoutTypeTag()
        {
            //SETUP
            var writer = new StringWriter();
            var instance = new ComplexTypeMock() { IntegerProperty = 123 };

            this.serializer.MapType<ComplexTypeMock>().MapProperty(p => p.IntegerProperty);

            //ACTION
            this.serializer.Serialize(writer, instance);
            var serializedContent = writer.ToString();

            //CHECK
            serializedContent.Should().Be("<ComplexTypeMock><IntegerProperty>123</IntegerProperty></ComplexTypeMock>");
        }

        [Fact]
        public void MapSerializer_MustSerialize_DoubleType_ToTextRepresentation_WithoutTypeTag()
        {
            //SETUP
            var writer = new StringWriter();
            var instance = new ComplexTypeMock() { DoubleProperty = 123.456 };

            this.serializer.MapType<ComplexTypeMock>().MapProperty(p => p.DoubleProperty);

            //ACTION
            this.serializer.Serialize(writer, instance);
            var serializedContent = writer.ToString();

            //CHECK
            serializedContent.Should().Be("<ComplexTypeMock><DoubleProperty>123.456</DoubleProperty></ComplexTypeMock>");
        }

        [Fact]
        public void MapSerializer_MustSerialize_DecimalType_ToTextRepresentation_WithoutTypeTag()
        {
            //SETUP
            var writer = new StringWriter();
            var instance = new ComplexTypeMock() { DecimalProperty = 123.456M };

            this.serializer.MapType<ComplexTypeMock>().MapProperty(p => p.DecimalProperty);

            //ACTION
            this.serializer.Serialize(writer, instance);
            var serializedContent = writer.ToString();

            //CHECK
            serializedContent.Should().Be("<ComplexTypeMock><DecimalProperty>123.456</DecimalProperty></ComplexTypeMock>");
        }

        [Fact]
        public void MapSerializer_MustSerialize_DateTimeType_ToTextRepresentation_WithoutTypeTag()
        {
            //SETUP
            var writer = new StringWriter();
            var instance = new ComplexTypeMock() { DateTimeProperty = new DateTime(2021, 08, 20) };

            this.serializer.MapType<ComplexTypeMock>().MapProperty(p => p.DateTimeProperty);

            //ACTION
            this.serializer.Serialize(writer, instance);
            var serializedContent = writer.ToString();

            //CHECK
            serializedContent.Should().Be($"<ComplexTypeMock><DateTimeProperty>{new DateTime(2021, 08, 20)}</DateTimeProperty></ComplexTypeMock>");
        }

        [Fact]
        public void MapSerializer_MustSerialize_CharType_ToTextRepresentation_WithoutTypeTag()
        {
            //SETUP
            var writer = new StringWriter();
            var instance = new ComplexTypeMock() { CharProperty = 'c' };

            this.serializer.MapType<ComplexTypeMock>().MapProperty(p => p.CharProperty);

            //ACTION
            this.serializer.Serialize(writer, instance);
            var serializedContent = writer.ToString();

            //CHECK
            serializedContent.Should().Be($"<ComplexTypeMock><CharProperty>c</CharProperty></ComplexTypeMock>");
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
        public string StringProperty { get; set; }
        public DateTime DateTimeProperty { get; set; }
        public double DoubleProperty { get; set; }
        public int IntegerProperty { get; set; }
        public char CharProperty { get; set; }

        public decimal DecimalProperty { get; set; }
    }
    
    public class AnotherComplexTypeMock
    {

    }
}
