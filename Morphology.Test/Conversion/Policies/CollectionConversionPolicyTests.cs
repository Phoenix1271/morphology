using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Morphology.Configuration;
using Morphology.Conversion;
using Morphology.Conversion.Policies;
using Morphology.Conversion.Tokens;
using Xunit;

namespace Morphology.Test.Conversion.Policies
{
    public class CollectionConversionPolicyTests
    {
        [Fact]
        public void CollectionConversionPolicy_NullConfig_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CollectionConversionPolicy(null));
        }

        [Fact]
        public void TryConvert_DictionaryWithScalarKey_ReturnsFalse()
        {
            var config = Mock.Of<IConversionConfig>();
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new CollectionConversionPolicy(config);
            var value = new Dictionary<string, int>();

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, value, out token));
        }

        [Fact]
        public void TryConvert_EmptyArray_ReturnsSequenceToken()
        {
            var config = Mock.Of<IConversionConfig>();
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new CollectionConversionPolicy(config);
            var value = new int[0];

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, value, out token));

            var sequence = token as SequenceToken;
            Assert.NotNull(sequence);
            Assert.Equal(0, sequence.Elements.Count);
        }

        [Fact]
        public void TryConvert_EmptyEnumerable_ReturnsSequenceToken()
        {
            var config = Mock.Of<IConversionConfig>();
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new CollectionConversionPolicy(config);
            var value = Enumerable.Empty<bool>();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, value, out token));

            var sequence = token as SequenceToken;
            Assert.NotNull(sequence);
            Assert.Equal(0, sequence.Elements.Count);
        }

        [Fact]
        public void TryConvert_ItemLimitSet_ReturnSequenceToken()
        {
            var configMock = new Mock<IConversionConfig>();
            configMock.Setup(m => m.ItemLimit).Returns(10);

            var converterMock = new Mock<IPropertyConverter>();
            converterMock
                .Setup(m => m.Convert(It.IsAny<object>()))
                .Returns<object>(v => new ScalarToken(v));

            var config = configMock.Object;
            var converter = converterMock.Object;
            var policy = new CollectionConversionPolicy(config);

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, Enumerable.Range(0, 20), out token));

            var sequence = token as SequenceToken;
            Assert.NotNull(sequence);
            Assert.Equal(config.ItemLimit, sequence.Elements.Count);
        }

        [Fact]
        public void TryConvert_NonEmptyArray_ReturnsSequenceToken()
        {
            var converterMock = new Mock<IPropertyConverter>();
            converterMock.Setup(m => m.Convert(It.IsAny<object>()))
                .Returns<object>(v => new ScalarToken(v));

            var config = Mock.Of<IConversionConfig>();
            var converter = converterMock.Object;
            var policy = new CollectionConversionPolicy(config);
            int[] value = {1};

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

            var config = Mock.Of<IConversionConfig>();
            var converter = converterMock.Object;
            var policy = new CollectionConversionPolicy(config);
            var value = new List<int> {1};

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
            var config = Mock.Of<IConversionConfig>();
            var policy = new CollectionConversionPolicy(config);

            IPropertyToken token;
            Assert.Throws<ArgumentNullException>(() => policy.TryConvert(null, null, out token));
        }

        [Fact]
        public void TryConvert_NullValue_ReturnsFalse()
        {
            var config = Mock.Of<IConversionConfig>();
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new CollectionConversionPolicy(config);

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, null, out token));
        }

        [Fact]
        public void TryConvert_ScalarValue_ReturnsFalse()
        {
            var config = Mock.Of<IConversionConfig>();
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new CollectionConversionPolicy(config);

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, false, out token));
        }
    }
}
