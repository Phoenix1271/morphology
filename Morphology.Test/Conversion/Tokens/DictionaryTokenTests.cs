using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Morphology.Conversion;
using Morphology.Conversion.Tokens;
using Morphology.Formatting;
using Xunit;

namespace Morphology.Test.Conversion.Tokens
{
    public class DictionaryTokenTests
    {
        [Fact]
        public void DictionaryToken_NullSequence_DictionaryContainsZeroElements()
        {
            var dictionary = new DictionaryToken(null);

            Assert.Equal(0, dictionary.Elements.Count);
        }

        [Fact]
        public void DictionaryToken_TokenSequence_DictionaryContainsInputElements()
        {
            var value = new KeyValuePair<ScalarToken, IPropertyToken>(new ScalarToken("Foo"), new ScalarToken("Bar"));

            var dictionary = new DictionaryToken(new[] {value});

            Assert.Equal(1, dictionary.Elements.Count);
            Assert.True(dictionary.Elements.Contains(value));
        }

        [Fact]
        public void Render_NullFormatter_ThrowsNullArgumentException()
        {
            var value = new KeyValuePair<ScalarToken, IPropertyToken>(new ScalarToken("Foo"), new ScalarToken("Bar"));
            var dictionary = new DictionaryToken(new[] {value});

            Assert.Throws<ArgumentNullException>(() => dictionary.Render(null));
        }

        [Fact]
        public void Render_SomeFormatter_FormatterIsCalled()
        {
            var formatterMock = new Mock<IPropertyFormatter>();

            var formatter = formatterMock.Object;
            var value = new KeyValuePair<ScalarToken, IPropertyToken>(new ScalarToken("Foo"), new ScalarToken("Bar"));
            var dictionary = new DictionaryToken(new[] {value});

            dictionary.Render(formatter);

            formatterMock.Verify(m => m.Format(dictionary), Times.Once);
        }
    }
}
