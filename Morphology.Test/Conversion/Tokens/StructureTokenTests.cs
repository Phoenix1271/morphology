using Morphology.Conversion.Tokens;
using Xunit;

namespace Morphology.Test.Conversion.Tokens
{
    public class StructureTokenTests
    {
        [Fact]
        public void New_NullProperties_StructureContainsZeroProperties()
        {
            var structure = new StructureToken(null);

            Assert.Equal(0, structure.Properties.Count);
        }

        [Fact]
        public void New_TokenSequence_SequenceContainsInputElements()
        {
            var property = new Property("Foo", new ScalarToken(null));
            string typeName = "FooBar";

            var structure = new StructureToken(new[] {property}, typeName);

            Assert.Equal(1, structure.Properties.Count);
            Assert.Equal(property, structure.Properties[0]);
            Assert.Equal(typeName, structure.TypeName);
        }
    }
}
