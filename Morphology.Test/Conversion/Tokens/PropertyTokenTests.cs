using System;
using Moq;
using Morphology.Conversion;
using Morphology.Conversion.Tokens;
using Morphology.Formatting;
using Morphology.Test.Support;
using Xunit;

namespace Morphology.Test.Conversion.Tokens
{
    public class PropertyTokenTests
    {
        [Fact]
        public void PropertyToken_EmptyPropertyName_ThrowsNullArgumentException()
        {
            var value = Mock.Of<IPropertyToken>();

            Assert.Throws<ArgumentNullException>(() => new PropertyToken(null, value));
            Assert.Throws<ArgumentNullException>(() => new PropertyToken(string.Empty, value));
        }

        [Fact]
        public void PropertyToken_NameAndValueIsValid_PropertyTokenContainsInputValues()
        {
            string name = Some.String();
            var value = Mock.Of<IPropertyToken>();

            var token = new PropertyToken(name, value);

            Assert.Equal(name, token.Name);
            Assert.Equal(value, token.Value);
        }

        [Fact]
        public void PropertyToken_NullValue_ThrowsNullArgumentException()
        {
            Assert.Throws<ArgumentNullException>(() => new PropertyToken(Some.String(), null));
        }

        [Fact]
        public void Render_NullFormatter_ThrowsArgumentNullException()
        {
            var property = new PropertyToken(Some.String(), Mock.Of<IPropertyToken>());

            Assert.Throws<ArgumentNullException>(() => property.Render(null));
        }

        [Fact]
        public void Render_NullFormatter_ThrowsNullArgumentException()
        {
            var property = new PropertyToken(Some.String(), Mock.Of<IPropertyToken>());

            Assert.Throws<ArgumentNullException>(() => property.Render(null));
        }

        [Fact]
        public void Render_SomeFormatter_FormatterIsCalled()
        {
            var formatterMock = new Mock<IPropertyFormatter>();

            var formatter = formatterMock.Object;
            var property = new PropertyToken(Some.String(), Mock.Of<IPropertyToken>());

            property.Render(formatter);

            formatterMock.Verify(m => m.Format(property), Times.Once);
        }
    }
}
