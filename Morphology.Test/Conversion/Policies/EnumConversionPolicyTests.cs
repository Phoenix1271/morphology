using System;
using Moq;
using Morphology.Conversion;
using Morphology.Conversion.Policies;
using Morphology.Conversion.Tokens;
using Xunit;

namespace Morphology.Test.Conversion.Policies
{
    public class EnumConversionPolicyTests
    {
        private enum FooBar
        {
            Foo
        }

        [Fact]
        public void TryConvert_Enum_ReturnsScalarToken()
        {
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new EnumConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, FooBar.Foo, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(FooBar.Foo, scalar.Value);
        }

        [Fact]
        public void TryConvert_NullConverter_ThrowsArgumentNullException()
        {
            var policy = new EnumConversionPolicy();

            IPropertyToken token;
            Assert.Throws<ArgumentNullException>(() => policy.TryConvert(null, null, out token));
        }

        [Fact]
        public void TryConvert_NullValue_ReturnsFalse()
        {
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new EnumConversionPolicy();

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, null, out token));
        }

        [Fact]
        public void TryConvert_ScalarValue_ReturnsFalse()
        {
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new EnumConversionPolicy();

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, false, out token));
        }
    }
}
