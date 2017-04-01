using System;
using Moq;
using Morphology.Configuration;
using Morphology.Conversion;
using Morphology.Conversion.Policies;
using Morphology.Conversion.Tokens;
using Xunit;

namespace Morphology.Test.Conversion.Policies
{
    public class StringConversionPolicyTests
    {
        [Fact]
        public void StringConversionPolicy_NullConfig_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new StringConversionPolicy(null));
        }

        [Fact]
        public void TryConvert_Scalar_ReturnsFalse()
        {
            var config = Mock.Of<IConversionConfig>();
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new StringConversionPolicy(config);

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, false, out token));
        }

        [Fact]
        public void TryConvert_String_ReturnsScalarToken()
        {
            var config = Mock.Of<IConversionConfig>();
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new StringConversionPolicy(config);
            string value = "Hello World";

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, value, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(value, scalar.Value);
        }

        [Fact]
        public void TryConvert_StringLimitSet_ReturnsScalarToken()
        {
            var configMock = new Mock<IConversionConfig>();
            configMock.Setup(m => m.StringLimit).Returns(5);

            var config = configMock.Object;
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new StringConversionPolicy(config);
            string value = "Hello World";

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, value, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            string text = scalar.Value as string;
            Assert.True(text.StartsWith("Hello"));
            Assert.True(text.EndsWith("…"));
        }
    }
}
