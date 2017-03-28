using System;
using Moq;
using Morphology.Conversion;
using Morphology.Conversion.Policies;
using Morphology.Conversion.Tokens;
using Xunit;

namespace Morphology.Test.Conversion.Policies
{
    public class DelegateConversionPolicyTests
    {
        [Fact]
        public void TryConvert_Delegate_ReturnsScalarToken()
        {
            Action<string> action = Console.WriteLine;
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new DelegateConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, action, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);

            string value = scalar.Value as string;
            Assert.NotNull(value);
            Assert.True(value.Contains(nameof(Console.WriteLine)));
        }


        [Fact]
        public void TryConvert_NullConverter_ThrowsArgumentNullException()
        {
            var policy = new DelegateConversionPolicy();

            IPropertyToken token;
            Assert.Throws<ArgumentNullException>(() => policy.TryConvert(null, null, out token));
        }

        [Fact]
        public void TryConvert_NullValue_ReturnsFalse()
        {
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new DelegateConversionPolicy();

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, null, out token));
        }


        [Fact]
        public void TryConvert_Object_ReturnsFalse()
        {
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new DelegateConversionPolicy();

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, new object(), out token));
        }
    }
}
