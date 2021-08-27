using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using MapSerializer.Test.Mock;
using Moq;
using Xunit;

namespace MapSerializer.Test
{
    public class MapSerializerBaseTests
    {
        private Mock<MapSerializerBase> serializer;

        public MapSerializerBaseTests()
        {
            this.serializer = new Mock<MapSerializerBase>();
        }

        [Fact]
        public void MapSerializerBase_MustCheckWhether_TypeIsAlreadyMapped()
        {
            //SETUP
            var instance = new BaseComplexTypeMock() { ComplexProperty = new ComplexTypeMock() };

            //ACTION
            var action = new Action(delegate
            {
                this.serializer.Object.MapType<BaseComplexTypeMock>();
                this.serializer.Object.MapType<BaseComplexTypeMock>();
            });

            //CHECK
            action.Should().Throw<InvalidOperationException>().WithMessage("Type 'MapSerializer.Test.Mock.BaseComplexTypeMock' is already mapped.");
        }

        [Fact]
        public void MapSerializerBase_MustCheckWhether_MappedPropertyIsReadable()
        {
            //SETUP
            var instance = new BaseComplexTypeMock() { ComplexField = new ComplexTypeMock() };

            //ACTION
            var action = new Action(delegate
            {
                this.serializer.Object.MapType<BaseComplexTypeMock>().MapProperty(p => p.ComplexField);
            });

            //CHECK
            action.Should().Throw<ArgumentException>().WithMessage("Invalid expression 'p => p.ComplexField': reference to fields is not supported.");
        }

        [Fact]
        public void MapSerializerBase_MustCheckWhether_MappedMemberIsMethod()
        {
            //SETUP
            var instance = new BaseComplexTypeMock();

            //ACTION
            var action = new Action(delegate
            {
                this.serializer.Object.MapType<BaseComplexTypeMock>().MapProperty(p => p.GetValue());
            });

            //CHECK
            action.Should().Throw<ArgumentException>().WithMessage("Invalid expression 'p => p.GetValue()': reference to methods is not supported.");
        }

        [Fact]
        public void MapSerializerBase_MustRecognize_Integer_AsNativeType()
        {
            //SETUP
            var type = typeof(int);

            //ACTION
            var isNative = MapSerializerBase.IsNativeType(type);

            //CHECK
            isNative.Should().BeTrue();
        }

        [Fact]
        public void MapSerializerBase_MustRecognize_Long_AsNativeType()
        {
            //SETUP
            var type = typeof(long);

            //ACTION
            var isNative = MapSerializerBase.IsNativeType(type);

            //CHECK
            isNative.Should().BeTrue();
        }

        [Fact]
        public void MapSerializerBase_MustRecognize_String_AsNativeType()
        {
            //SETUP
            var type = typeof(string);

            //ACTION
            var isNative = MapSerializerBase.IsNativeType(type);

            //CHECK
            isNative.Should().BeTrue();
        }

        [Fact]
        public void MapSerializerBase_MustRecognize_Float_AsNativeType()
        {
            //SETUP
            var type = typeof(float);

            //ACTION
            var isNative = MapSerializerBase.IsNativeType(type);

            //CHECK
            isNative.Should().BeTrue();
        }

        [Fact]
        public void MapSerializerBase_MustRecognize_Double_AsNativeType()
        {
            //SETUP
            var type = typeof(double);

            //ACTION
            var isNative = MapSerializerBase.IsNativeType(type);

            //CHECK
            isNative.Should().BeTrue();
        }

        [Fact]
        public void MapSerializerBase_MustRecognize_Decimal_AsNativeType()
        {
            //SETUP
            var type = typeof(float);

            //ACTION
            var isNative = MapSerializerBase.IsNativeType(type);

            //CHECK
            isNative.Should().BeTrue();
        }

        [Fact]
        public void MapSerializerBase_MustRecognize_DateTime_AsNativeType()
        {
            //SETUP
            var type = typeof(DateTime);

            //ACTION
            var isNative = MapSerializerBase.IsNativeType(type);

            //CHECK
            isNative.Should().BeTrue();
        }

        [Fact]
        public void MapSerializerBase_MustRecognize_Enum_AsNativeType()
        {
            //SETUP
            var type = typeof(EnumMock);

            //ACTION
            var isNative = MapSerializerBase.IsNativeType(type);

            //CHECK
            isNative.Should().BeTrue();
        }

        [Fact]
        public void MapSerializerBase_MustRecognize_GenericList_AsEnumerable()
        {
            //SETUP
            var type = typeof(List<string>);

            //ACTION
            var isNative = MapSerializerBase.IsEnumerable(type);

            //CHECK
            isNative.Should().BeTrue();
        }

        [Fact]
        public void MapSerializerBase_MustRecognize_GenericDictionary_AsEnumerable()
        {
            //SETUP
            var type = typeof(Dictionary<int, string>);

            //ACTION
            var isNative = MapSerializerBase.IsEnumerable(type);

            //CHECK
            isNative.Should().BeTrue();
        }

        [Fact]
        public void MapSerializerBase_MustRecognize_Array_AsEnumerable()
        {
            //SETUP
            var type = typeof(string[]);

            //ACTION
            var isNative = MapSerializerBase.IsEnumerable(type);

            //CHECK
            isNative.Should().BeTrue();
        }
    }
}
