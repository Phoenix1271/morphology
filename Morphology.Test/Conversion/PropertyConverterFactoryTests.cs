using Moq;
using Morphology.Configuration;
using Morphology.Conversion;
using Morphology.Conversion.Converters;
using Xunit;

namespace Morphology.Test.Conversion
{
    public class PropertyConverterFactoryTests
    {
        [Fact]
        public void Create_TypeIsNull_ReturnsConverterBasedOnConfiguration()
        {
            var configMock = new Mock<IConversionConfig>();
            configMock.Setup(m => m.ConversionType).Returns(ConversionType.Stringify);

            var logger = Mock.Of<ILogger>();
            var config = configMock.Object;
            var factory = new PropertyConverterFactory(config, logger);

            var converter = factory.Create(null);

            Assert.NotNull(converter);
            Assert.IsType<ScalarConverter>(converter);
        }

        [Fact]
        public void Create_TypeIsStringify_ReturnsScalarConverter()
        {
            var logger = Mock.Of<ILogger>();
            var config = new DefaultConversionConfig(logger);
            var factory = new PropertyConverterFactory(config, logger);

            var converter = factory.Create(ConversionType.Stringify);

            Assert.NotNull(converter);
            Assert.IsType<ScalarConverter>(converter);
        }

        [Fact]
        public void Create_TypeIsDestructure_ReturnsStructuralConverter()
        {
            var logger = Mock.Of<ILogger>();
            var config = new DefaultConversionConfig(logger);
            var factory = new PropertyConverterFactory(config, logger);

            var converter = factory.Create(ConversionType.Destructure);

            Assert.NotNull(converter);
            Assert.IsType<StructuralConverter>(converter);
        }
    }
}
