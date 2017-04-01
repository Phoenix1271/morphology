using System;
using System.Linq;
using Moq;
using Morphology.Configuration;
using Morphology.Conversion;
using Morphology.Conversion.Policies;
using Morphology.Conversion.Tokens;
using Xunit;

namespace Morphology.Test.Conversion.Policies
{
    public class ByteArrayConversionPolicyTests
    {
        [Fact]
        public void ByteArrayConversionPolicy_NullConfig_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CollectionConversionPolicy(null));
        }

        [Fact]
        public void TryConvert_ByteArray_ReturnsScalarToken()
        {
            var config = Mock.Of<IConversionConfig>();
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ByteArrayConversionPolicy(config);
            var bytes = new byte[] {1, 2, 3};

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, bytes, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(bytes, scalar.Value);
        }

        [Fact]
        public void TryConvert_ByteArrayLimitSet_ReturnsScalarToken()
        {
            var configMock = new Mock<IConversionConfig>();
            configMock.Setup(m => m.ByteArrayLimit).Returns(1024);

            var config = configMock.Object;
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ByteArrayConversionPolicy(config);
            var bytes = Enumerable.Range(0, 1025).Select(b => (byte) b).ToArray();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, bytes, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);

            string value = scalar.Value as string;
            Assert.NotNull(value);
            Assert.True(value.StartsWith("0x:"));
            Assert.True(value.EndsWith("(1025 bytes)"));
        }

        [Fact]
        public void TryConvert_IntArray_ReturnsFalse()
        {
            var config = Mock.Of<IConversionConfig>();
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ByteArrayConversionPolicy(config);

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, new int[0], out token));
        }

        [Fact]
        public void TryConvert_NullConverter_ThrowsArgumentNullException()
        {
            var config = Mock.Of<IConversionConfig>();
            var policy = new ByteArrayConversionPolicy(config);

            IPropertyToken token;
            Assert.Throws<ArgumentNullException>(() => policy.TryConvert(null, null, out token));
        }

        [Fact]
        public void TryConvert_NullValue_ReturnsFalse()
        {
            var config = Mock.Of<IConversionConfig>();
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ByteArrayConversionPolicy(config);

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, null, out token));
        }
    }
}
