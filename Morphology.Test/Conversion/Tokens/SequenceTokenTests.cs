using System.Linq;
using Morphology.Conversion.Tokens;
using Xunit;

namespace Morphology.Test.Conversion.Tokens
{
    public class SequenceTokenTests
    {
        [Fact]
        public void New_NullSequence_SequenceContainsZeroElements()
        {
            var sequence = new SequenceToken(null);

            Assert.Equal(0, sequence.Elements.Count);
        }

        [Fact]
        public void New_TokenSequence_SequenceContainsInputElements()
        {
            var elements = Enumerable.Range(0, 3).Select(v => new ScalarToken(v)).ToList();

            var sequence = new SequenceToken(elements);

            Assert.Equal(3, sequence.Elements.Count);
            Assert.True(sequence.Elements.SequenceEqual(elements));
        }
    }
}
