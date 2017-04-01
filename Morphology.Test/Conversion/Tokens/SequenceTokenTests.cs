using System;
using System.Linq;
using Moq;
using Morphology.Conversion.Tokens;
using Morphology.Formatting;
using Xunit;

namespace Morphology.Test.Conversion.Tokens
{
    public class SequenceTokenTests
    {
        [Fact]
        public void Render_NullFormatter_ThrowsArgumentNullException()
        {
            var sequence = new SequenceToken(null);

            Assert.Throws<ArgumentNullException>(() => sequence.Render(null));
        }

        [Fact]
        public void Render_SomeFormatter_FormatterIsCalled()
        {
            var formatterMock = new Mock<IPropertyFormatter>();

            var formatter = formatterMock.Object;
            var sequence = new SequenceToken(null);

            sequence.Render(formatter);

            formatterMock.Verify(m => m.Format(sequence), Times.Once);
        }

        [Fact]
        public void SequenceToken_NullSequence_SequenceContainsZeroElements()
        {
            var sequence = new SequenceToken(null);

            Assert.Equal(0, sequence.Elements.Count);
        }

        [Fact]
        public void SequenceToken_TokenSequence_SequenceContainsInputElements()
        {
            var elements = Enumerable.Range(0, 3).Select(v => new ScalarToken(v)).ToList();

            var sequence = new SequenceToken(elements);

            Assert.Equal(3, sequence.Elements.Count);
            Assert.True(sequence.Elements.SequenceEqual(elements));
        }
    }
}
