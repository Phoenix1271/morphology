using Moq;
using Morphology.Configuration;
using Morphology.Conversion.Converters;
using Morphology.Conversion.Tokens;
using Morphology.Test.Support;
using Xunit;

// ReSharper disable UnusedMember.Local
// ReSharper disable UnassignedGetOnlyAutoProperty

namespace Morphology.Test.Conversion.Converters
{
    public class ScalarConverterTests
    {
        private class Structure
        {
            #region Public Properties

            public string Foo { get; }

            #endregion
        }

        [Fact]
        public void Convert_Null_ReturnsScalarToken()
        {
            var logger = Mock.Of<ILogger>();
            var config = new DefaultConversionConfig(logger);
            var converter = new ScalarConverter(config, logger);

            var token = converter.Convert(null);

            Assert.NotNull(token);
            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(null, scalar.Value);
        }

        [Fact]
        public void Convert_ScalarType_ReturnsScalarToken()
        {
            var logger = Mock.Of<ILogger>();
            var config = new DefaultConversionConfig(logger);
            var converter = new ScalarConverter(config, logger);
            int value = Some.Int();

            var token = converter.Convert(value);

            Assert.NotNull(token);
            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(value.ToString(), scalar.Value);
        }

        [Fact]
        public void Convert_StructureValue_ReturnsScalarToken()
        {
            var logger = Mock.Of<ILogger>();
            var config = new DefaultConversionConfig(logger);
            var converter = new ScalarConverter(config, logger);
            var value = new Structure();

            var token = converter.Convert(value);

            Assert.NotNull(token);
            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(value.ToString(), scalar.Value);
        }
    }
}
