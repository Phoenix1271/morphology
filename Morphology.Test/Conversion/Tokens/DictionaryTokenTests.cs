using System.Collections.Generic;
using System.Linq;
using Morphology.Conversion.Tokens;
using Xunit;

namespace Morphology.Test.Conversion.Tokens
{
    public class DictionaryTokenTests
    {
        [Fact]
        public void New_NullSequence_DictionaryContainsZeroElements()
        {
            var dictionary = new DictionaryToken(null);

            Assert.Equal(0, dictionary.Elements.Count);
        }

        [Fact]
        public void New_TokenSequence_DictionaryContainsInputElements()
        {
            var value = new KeyValuePair<ScalarToken, IPropertyToken>(new ScalarToken("Foo"), new ScalarToken("Bar"));

            var dictionary = new DictionaryToken(new[] {value});

            Assert.Equal(1, dictionary.Elements.Count);
            Assert.True(dictionary.Elements.Contains(value));
        }
    }
}
