using System;
using System.IO;
using Xunit;
using FluentAssertions;
using MapSerializer.Test.Blackbox.Mock;

namespace MapSerializer.Test.Blackbox
{
    public class MapXmlSerializerBlackBoxTests
    {
        private MapXmlSerializer serializer;

        public MapXmlSerializerBlackBoxTests()
        {
            this.serializer = new MapXmlSerializer();
        }

        [Fact]
        public void MapSerializer_MustSerialize_1000_Depth_CompositeObjects()
        {
            //SETUP
            var writer = new StringWriter();
            var instance = CompositeType.CreateDeepComposition(1000);

            this.serializer.MapType<CompositeType>().MapProperty(p => p.Depth)
                                                    .MapProperty(p => p.Child);

            //ACTION
            this.serializer.Serialize(writer, instance);
            var serializedContent = writer.ToString();

            //CHECK
            serializedContent.Should().Contain("<Depth>1</Depth>");
            serializedContent.Should().Contain("<Depth>1000</Depth>");
        }
    }
}
