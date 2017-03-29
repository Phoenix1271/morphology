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
    public class CollectionConversionPolicyTests
    {
        [Fact]
        public void TryConvert_DictionaryWithScalarKey_ReturnsFalse()
        {
            var value = new Dictionary<string, int>();
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new CollectionConversionPolicy();

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, value, out token));
        }

        [Fact]
        public void TryConvert_EmptyArray_ReturnsSequenceToken()
        {
            var value = new int[0];
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new CollectionConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, value, out token));

            var sequence = token as SequenceToken;
            Assert.NotNull(sequence);
            Assert.Equal(0, sequence.Elements.Count);
        }

        [Fact]
        public void TryConvert_EmptyEnumerable_ReturnsSequenceToken()
        {
            var value = Enumerable.Empty<bool>();
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new CollectionConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, value, out token));

            var sequence = token as SequenceToken;
            Assert.NotNull(sequence);
            Assert.Equal(0, sequence.Elements.Count);
        }

        [Fact]
        public void TryConvert_NonEmptyArray_ReturnsSequenceToken()
        {
            var converterMock = new Mock<IPropertyConverter>();
            converterMock.Setup(m => m.Convert(It.IsAny<object>()))
                .Returns<object>(v => new ScalarToken(v));

            int[] value = {1};
            var converter = converterMock.Object;
            var policy = new CollectionConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, value, out token));

            var sequence = token as SequenceToken;
            Assert.NotNull(sequence);
            Assert.Equal(1, sequence.Elements.Count);
            Assert.IsType<ScalarToken>(sequence.Elements[0]);
        }

        [Fact]
        public void TryConvert_NonEmptyEnumerable_ReturnsSequenceToken()
        {
            var converterMock = new Mock<IPropertyConverter>();
            converterMock
                .Setup(m => m.Convert(It.IsAny<object>()))
                .Returns<object>(v => new ScalarToken(v));

            var value = new List<int> {1};
            var converter = converterMock.Object;
            var policy = new CollectionConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, value, out token));

            var sequence = token as SequenceToken;
            Assert.NotNull(sequence);
            Assert.Equal(1, sequence.Elements.Count);
            Assert.IsType<ScalarToken>(sequence.Elements[0]);
        }

        [Fact]
        public void TryConvert_NullConverter_ThrowsArgumentNullException()
        {
            var policy = new CollectionConversionPolicy();

            IPropertyToken token;
            Assert.Throws<ArgumentNullException>(() => policy.TryConvert(null, null, out token));
        }

        [Fact]
        public void TryConvert_NullValue_ReturnsFalse()
        {
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new CollectionConversionPolicy();

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, null, out token));
        }

        [Fact]
        public void TryConvert_ScalarValue_ReturnsFalse()
        {
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new CollectionConversionPolicy();

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, false, out token));
        }
    }
}
