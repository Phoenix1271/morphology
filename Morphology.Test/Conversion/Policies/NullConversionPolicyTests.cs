using System;
using Moq;
using Morphology.Conversion;
using Morphology.Conversion.Policies;
using Morphology.Conversion.Tokens;
using Xunit;

namespace Morphology.Test.Conversion.Policies
{
    public class NullConversionPolicyTests
    {
#if NET45 || NET46

        [Fact]
        public void TryConvert_DBNull_ReturnsScalarToken()
        {
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new NullConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, DBNull.Value, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(null, scalar.Value);
        }

#endif

        [Fact]
        public void TryConvert_NotNullValue_ReturnsFalse()
        {
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new NullConversionPolicy();

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, false, out token));
        }

        [Fact]
        public void TryConvert_Null_ReturnsScalarToken()
        {
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new NullConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, null, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(null, scalar.Value);
        }

        [Fact]
        public void TryConvert_NullConverter_ThrowsArgumentNullException()
        {
            var policy = new NullConversionPolicy();

            IPropertyToken token;
            Assert.Throws<ArgumentNullException>(() => policy.TryConvert(null, null, out token));
        }
    }
}
