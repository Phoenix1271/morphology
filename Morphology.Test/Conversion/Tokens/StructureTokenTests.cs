using System;
using Moq;
using Morphology.Conversion.Tokens;
using Morphology.Formatting;
using Xunit;

namespace Morphology.Test.Conversion.Tokens
{
    public class StructureTokenTests
    {
        [Fact]
        public void Render_NullFormatter_ThrowsArgumentNullException()
        {
            var structure = new StructureToken(null);

            Assert.Throws<ArgumentNullException>(() => structure.Render(null));
        }

        [Fact]
        public void Render_SomeFormatter_FormatterIsCalled()
        {
            var formatterMock = new Mock<IPropertyFormatter>();

            var formatter = formatterMock.Object;
            var structure = new StructureToken(null);

            structure.Render(formatter);

            formatterMock.Verify(m => m.Format(structure), Times.Once);
        }

        [Fact]
        public void StructureToken_NullProperties_StructureContainsZeroProperties()
        {
            var structure = new StructureToken(null);

            Assert.Equal(0, structure.Properties.Count);
        }

        [Fact]
        public void StructureToken_TokenSequence_SequenceContainsInputElements()
        {
            var property = new PropertyToken("Foo", new ScalarToken(null));
            string typeName = "FooBar";

            var structure = new StructureToken(new[] {property}, typeName);

            Assert.Equal(1, structure.Properties.Count);
            Assert.Equal(property, structure.Properties[0]);
            Assert.Equal(typeName, structure.TypeName);
        }
    }
}
