using System;
using Moq;
using Morphology.Conversion.Tokens;
using Morphology.Formatting;
using Xunit;

namespace Morphology.Test.Conversion.Tokens
{
    public class ScalarTokenTests
    {
        [Fact]
        public void Render_NullFormatter_ThrowsArgumentNullException()
        {
            var scalar = new ScalarToken(null);

            Assert.Throws<ArgumentNullException>(() => scalar.Render(null));
        }

        [Fact]
        public void Render_SomeFormatter_FormatterIsCalled()
        {
            var formatterMock = new Mock<IPropertyFormatter>();

            var formatter = formatterMock.Object;
            var scalar = new ScalarToken(null);

            scalar.Render(formatter);

            formatterMock.Verify(m => m.Format(scalar), Times.Once);
        }

        [Fact]
        public void ScalarToken_ScalarType_ScalarContainsInputValue()
        {
            string value = "Hello World!";

            var scalar = new ScalarToken(value);

            Assert.Equal(scalar.Value, value);
        }
    }
}
