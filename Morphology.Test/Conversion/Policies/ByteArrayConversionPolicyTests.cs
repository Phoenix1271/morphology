using System;
using System.Linq;
using Moq;
using Morphology.Conversion;
using Morphology.Conversion.Policies;
using Morphology.Conversion.Tokens;
using Xunit;

namespace Morphology.Test.Conversion.Policies
{
    public class ByteArrayConversionPolicyTests
    {
        [Fact]
        public void TryConvert_ByteArray_ReturnsScalarToken()
        {
            var bytes = new byte[] {1, 2, 3};
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ByteArrayConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, bytes, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(bytes, scalar.Value);
        }

        [Fact]
        public void TryConvert_IntArray_ReturnsFalse()
        {
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ByteArrayConversionPolicy();

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, new int[0], out token));
        }

        [Fact]
        public void TryConvert_LargeByteArray_ReturnsScalarToken()
        {
            var bytes = Enumerable.Range(0, 1025).Select(b => (byte) b).ToArray();
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ByteArrayConversionPolicy();

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
        public void TryConvert_NullConverter_ThrowsArgumentNullException()
        {
            var policy = new ByteArrayConversionPolicy();

            IPropertyToken token;
            Assert.Throws<ArgumentNullException>(() => policy.TryConvert(null, null, out token));
        }

        [Fact]
        public void TryConvert_NullValue_ReturnsFalse()
        {
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ByteArrayConversionPolicy();

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, null, out token));
        }
    }
}
