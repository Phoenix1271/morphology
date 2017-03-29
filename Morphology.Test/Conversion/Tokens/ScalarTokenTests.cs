using Morphology.Conversion.Tokens;
using Xunit;

namespace Morphology.Test.Conversion.Tokens
{
    public class ScalarTokenTests
    {
        [Fact]
        public void New_ScalarType_ScalarContainsInputValue()
        {
            string value = "Hello World!";

            var scalar = new ScalarToken(value);

            Assert.Equal(scalar.Value, value);
        }
    }
}
