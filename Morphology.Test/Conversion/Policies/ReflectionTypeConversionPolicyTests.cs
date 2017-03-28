using System;
using Moq;
using Morphology.Conversion;
using Morphology.Conversion.Policies;
using Morphology.Conversion.Tokens;
using Xunit;

namespace Morphology.Test.Conversion.Policies
{
    public class ReflectionTypeConversionPolicyTests
    {
        [Fact]
        public void TryConvert_NullConverter_ThrowsArgumentNullException()
        {
            var policy = new ReflectionTypeConversionPolicy();

            IPropertyToken token;
            Assert.Throws<ArgumentNullException>(() => policy.TryConvert(null, null, out token));
        }

        [Fact]
        public void TryConvert_NullValue_ReturnsFalse()
        {
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ReflectionTypeConversionPolicy();

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, null, out token));
        }

        [Fact]
        public void TryConvert_Object_ReturnsFalse()
        {
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ReflectionTypeConversionPolicy();

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, new object(), out token));
        }

        [Fact]
        public void TryConvert_ReflectionType_ReturnsScalarToken()
        {
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ReflectionTypeConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, typeof(string), out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(typeof(string).ToString(), scalar.Value);
        }
    }
}
