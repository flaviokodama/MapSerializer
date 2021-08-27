using System;
using System.IO;
using FluentAssertions;
using MapSerializer.Test.Mock;
using Xunit;

namespace MapSerializer.Test
{
    public class MapJsonSerializerTests
    {
        private MapJsonSerializer serializer;

        public MapJsonSerializerTests()
        {
            this.serializer = new MapJsonSerializer();
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
            serializedContent.Should().Be("{\"StringProperty\":\"123\"}");
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
            serializedContent.Should().Be("{\"IntegerProperty\":123}");
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
            serializedContent.Should().Be("{\"DoubleProperty\":123.456}");
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
            serializedContent.Should().Be("{\"DecimalProperty\":123.456}");
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
            serializedContent.Should().Be($"{{\"DateTimeProperty\":\"{new DateTime(2021, 08, 20)}\"}}");
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
            serializedContent.Should().Be("{\"CharProperty\":\"c\"}");
        }

        [Fact]
        public void MapSerializer_MustSerialize_ComplexType_ToTextRepresentation_WithTypeTag_WhenPropertyType_IsNotMapped()
        {
            //SETUP
            var writer = new StringWriter();
            var instance = new BaseComplexTypeMock() { ComplexProperty = new ComplexTypeMock() { StringProperty = "Value" } };

            this.serializer.MapType<BaseComplexTypeMock>().MapProperty(p => p.ComplexProperty);

            //ACTION
            this.serializer.Serialize(writer, instance);
            var serializedContent = writer.ToString();

            //CHECK
            serializedContent.Should().Be("{\"ComplexProperty\":{\"ComplexTypeMock\":\"MapSerializer.Test.Mock.ComplexTypeMock\"}}");
        }

        [Fact]
        public void MapSerializer_MustSerialize_ComplexType_ToTextRepresentation_WithTypeTag()
        {
            //SETUP
            var writer = new StringWriter();
            var instance = new BaseComplexTypeMock() { ComplexProperty = new ComplexTypeMock() { StringProperty = "Value" } };

            this.serializer.MapType<BaseComplexTypeMock>().MapProperty(p => p.ComplexProperty);
            this.serializer.MapType<ComplexTypeMock>().MapProperty(p => p.StringProperty);

            //ACTION
            this.serializer.Serialize(writer, instance);
            var serializedContent = writer.ToString();

            //CHECK
            serializedContent.Should().Be("{\"ComplexProperty\":{\"ComplexTypeMock\":{\"StringProperty\":\"Value\"}}}");
        }
    }
}
