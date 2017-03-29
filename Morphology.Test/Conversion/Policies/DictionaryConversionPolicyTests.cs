using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Morphology.Conversion;
using Morphology.Conversion.Policies;
using Morphology.Conversion.Tokens;
using Xunit;

namespace Morphology.Test.Conversion.Policies
{
    public class DictionaryConversionPolicyTests
    {
        private class ComplexKey
        {
        }

        [Fact]
        public void TryConvert_DictionaryKeyIsComplex_ReturnsFalse()
        {
            var value = new Dictionary<ComplexKey, string> {{new ComplexKey(), "hello"}};
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new DictionaryConversionPolicy();

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, value, out token));
        }

        [Fact]
        public void TryConvert_DictionaryKeyIsScalar_ReturnsDictionaryToken()
        {
            var converterMock = new Mock<IPropertyConverter>();
            converterMock.Setup(m => m.Convert(It.IsAny<object>())).Returns<object>(o => new ScalarToken(o));

            var value = new Dictionary<int, string> {{1, "hello"}};
            var converter = converterMock.Object;
            var policy = new DictionaryConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, value, out token));

            var dictionary = token as DictionaryToken;
            Assert.NotNull(dictionary);
            Assert.Equal(1, dictionary.Elements.Count);
            Assert.IsType<ScalarToken>(dictionary.Elements.First().Key);
        }

        [Fact]
        public void TryConvert_NullConverter_ThrowsArgumentNullException()
        {
            var policy = new DictionaryConversionPolicy();

            IPropertyToken token;
            Assert.Throws<ArgumentNullException>(() => policy.TryConvert(null, null, out token));
        }

        [Fact]
        public void TryConvert_NullValue_ReturnsFalse()
        {
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new DictionaryConversionPolicy();

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, null, out token));
        }

        [Fact]
        public void TryConvert_ScalarValue_ReturnsFalse()
        {
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new DictionaryConversionPolicy();

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, false, out token));
        }
    }
}
