using FluentAssertions;
using MapSerializer.Test.Mock;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace MapSerializer.Test
{
    public class MapJsonSerializerTests : IDisposable
    {
        private StringWriter writer;
        private MapJsonSerializer serializer;

        public MapJsonSerializerTests()
        {
            this.writer = new StringWriter();
            this.serializer = new MapJsonSerializer();
        }

        public void Dispose()
        {
            writer.Dispose();
        }

        [Fact]
        public void MapJsonSerializer_MustSerialize_StringProperty_AccordingToSampleBase()
        {
            //SETUP
            var instance = new TypeWithStringProperty() { StringProperty = "123" };
            var sampleSerialization = ComparisonSerializer.SerializeToJson(instance);

            this.serializer.MapType<TypeWithStringProperty>().MapProperty(p => p.StringProperty);

            //ACTION
            this.serializer.Serialize(writer, instance);
            var serializedContent = writer.ToString();

            //CHECK
            serializedContent.Should().Be(sampleSerialization);
        }

        [Fact]
        public void MapJsonSerializer_MustSerialize_IntegerProperty_AccordingToSampleBase()
        {
            //SETUP
            var instance = new TypeWithIntegerProperty() { IntegerProperty = 123 };
            var sampleSerialization = ComparisonSerializer.SerializeToJson(instance);

            this.serializer.MapType<TypeWithIntegerProperty>().MapProperty(p => p.IntegerProperty);

            //ACTION
            this.serializer.Serialize(writer, instance);
            var serializedContent = writer.ToString();

            //CHECK
            serializedContent.Should().Be(sampleSerialization);
        }

        [Fact]
        public void MapJsonSerializer_MustSerialize_DoubleProperty_AccordingToSampleBase()
        {
            //SETUP
            var instance = new TypeWithDoubleProperty() { DoubleProperty = 123.456 };
            var sampleSerialization = ComparisonSerializer.SerializeToJson(instance);

            this.serializer.MapType<TypeWithDoubleProperty>().MapProperty(p => p.DoubleProperty);

            //ACTION
            this.serializer.Serialize(writer, instance);
            var serializedContent = writer.ToString();

            //CHECK
            serializedContent.Should().Be(sampleSerialization);
        }

        [Fact]
        public void MapJsonSerializer_MustSerialize_DecimalProperty_AccordingToSampleBase()
        {
            //SETUP
            var instance = new TypeWithDecimalProperty() { DecimalProperty = 123.456M };
            var sampleSerialization = ComparisonSerializer.SerializeToJson(instance);

            this.serializer.MapType<TypeWithDecimalProperty>().MapProperty(p => p.DecimalProperty);

            //ACTION
            this.serializer.Serialize(writer, instance);
            var serializedContent = writer.ToString();

            //CHECK
            serializedContent.Should().Be(sampleSerialization);
        }

        [Fact]
        public void MapJsonSerializer_MustSerialize_DateTimeProperty_AccordingToSampleBase()
        {
            //SETUP
            var instance = new TypeWithDateTimeProperty() { DateTimeProperty = new DateTime(2021, 08, 28, 23, 59, 59, 999) };

            this.serializer.MapType<TypeWithDateTimeProperty>().MapProperty(p => p.DateTimeProperty);

            //ACTION
            this.serializer.Serialize(writer, instance);
            var serializedContent = writer.ToString();

            //CHECK
            serializedContent.Should().Be($"{{\"DateTimeProperty\":\"2021-08-28T23:59:59.9990000\"}}");
        }

        [Fact]
        public void MapJsonSerializer_MustSerialize_CharProperty_AccordingToSampleBase()
        {
            //SETUP
            var instance = new TypeWithCharProperty() { CharProperty = 'c' };
            var sampleSerialization = ComparisonSerializer.SerializeToJson(instance);

            this.serializer.MapType<TypeWithCharProperty>().MapProperty(p => p.CharProperty);

            //ACTION
            this.serializer.Serialize(writer, instance);
            var serializedContent = writer.ToString();

            //CHECK
            serializedContent.Should().Be(sampleSerialization);
        }

        [Fact]
        public void MapJsonSerializer_MustSerialize_ComplexType_ToTextRepresentation_WithTypeTag_WhenPropertyType_IsNotMapped()
        {
            //SETUP
            var instance = new TypeWith1ComplexProperty() { ComplexProperty = new TypeWithStringProperty() { StringProperty = "Value" } };
            
            this.serializer.MapType<TypeWith1ComplexProperty>().MapProperty(p => p.ComplexProperty);

            //ACTION
            this.serializer.Serialize(writer, instance);
            var serializedContent = writer.ToString();

            //CHECK
            serializedContent.Should().Be("{\"ComplexProperty\":\"MapSerializer.Test.Mock.TypeWithStringProperty\"}");
        }

        [Fact]
        public void MapJsonSerializer_MustSerialize_ComplexType_ToTextRepresentation_WithTypeTag()
        {
            //SETUP
            var instance = new TypeWith1ComplexProperty() { ComplexProperty = new TypeWithStringProperty() { StringProperty = "Value" } };
            var sampleSerialization = ComparisonSerializer.SerializeToJson(instance);

            this.serializer.MapType<TypeWith1ComplexProperty>().MapProperty(p => p.ComplexProperty);
            this.serializer.MapType<TypeWithStringProperty>().MapProperty(p => p.StringProperty);

            //ACTION
            this.serializer.Serialize(writer, instance);
            var serializedContent = writer.ToString();

            //CHECK
            serializedContent.Should().Be(sampleSerialization);
        }

        [Fact]
        public void MapJsonSerializer_MustSerialize_IEnumerable_Property_WithTypeTag()
        {
            //SETUP
            var instance = new TypeWithEnumerableProperty() { ListProperty = new List<TypeWithStringProperty> { new TypeWithStringProperty() { StringProperty = "Value 1" }, new TypeWithStringProperty() { StringProperty = "Value 2" } } };
            var sampleSerialization = ComparisonSerializer.SerializeToJson(instance);

            this.serializer.MapType<TypeWithEnumerableProperty>().MapProperty(p => p.ListProperty);
            this.serializer.MapType<TypeWithStringProperty>().MapProperty(p => p.StringProperty);

            //ACTION
            this.serializer.Serialize(writer, instance);
            var serializedContent = writer.ToString();

            //CHECK
            serializedContent.Should().Be(sampleSerialization);
        }

        [Fact]
        public void MapJsonSerializer_MustSerialize_IEnumerable_AsRootObject()
        {
            //SETUP
            var instance = new List<TypeWithStringProperty>() { new TypeWithStringProperty() { StringProperty = "String Value 1" }, new TypeWithStringProperty() { StringProperty = "String Value 2" } };
            var sampleSerialization = ComparisonSerializer.SerializeToJson(instance);

            this.serializer.MapType<TypeWithStringProperty>().MapProperty(p => p.StringProperty);

            //ACTION
            this.serializer.Serialize(writer, instance);
            var serializedContent = writer.ToString();

            //CHECK
            serializedContent.Should().Be(sampleSerialization);
        }

        [Fact]
        public void MapJsonSerializer_MustSerialize_AbstractProperty_AsConcreteType()
        {
            //SETUP
            var instance = new TypeWithAbstractProperty { AbstractProperty = new TypeConcrete { StringProperty = "Value Concrete", StringPropertyBase = "Value Abstract" } };
            var sampleSerialization = ComparisonSerializer.SerializeToJson(instance);

            this.serializer.MapType<TypeWithAbstractProperty>().MapProperty(p => p.AbstractProperty);
            this.serializer.MapType<TypeConcrete>().MapProperty(p => p.StringProperty)
                                                   .MapProperty(p => p.StringPropertyBase);

            //ACTION
            this.serializer.Serialize(writer, instance);
            var serializedContent = writer.ToString();

            //CHECK
            serializedContent.Should().Be(sampleSerialization);
        }

    }
}
