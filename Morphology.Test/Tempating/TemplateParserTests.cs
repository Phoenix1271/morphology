using System.Linq;
using System.Text;
using Morphology.Conversion;
using Morphology.Templating;
using Morphology.Templating.Tokens;
using Xunit;

namespace Morphology.Test.Tempating
{
    public class TemplateParserTests
    {
        [Fact]
        public void Parse_ConversionHintIsInvalid_TokenHasDefaultHint()
        {
            var tokens = TemplateParser.Parse("{?Invalid}").ToArray();
            Assert.Equal(1, tokens.Length);

            var token = tokens[0] as HoleToken;
            Assert.NotNull(token);
            Assert.Equal("Invalid", token.Name);
            Assert.Equal(ConversionHint.Default, token.ConversionHint);
        }

        [Fact]
        public void Parse_ConversionHintIsString_TokenHasStringHint()
        {
            var tokens = TemplateParser.Parse("{$Stringify}").ToArray();
            Assert.Equal(1, tokens.Length);

            var token = tokens[0] as HoleToken;
            Assert.NotNull(token);
            Assert.Equal("Stringify", token.Name);
            Assert.Equal(ConversionHint.String, token.ConversionHint);
        }

        [Fact]
        public void Parse_ConversionHintIsStructure_TokenHasStructureHint()
        {
            var tokens = TemplateParser.Parse("{@Destructure}").ToArray();
            Assert.Equal(1, tokens.Length);

            var token = tokens[0] as HoleToken;
            Assert.NotNull(token);
            Assert.Equal("Destructure", token.Name);
            Assert.Equal(ConversionHint.Structure, token.ConversionHint);
        }

        [Fact]
        public void Parse_MultiplePropertiesMultiline_TokensAreInCorrectOrder()
        {
            var sb = new StringBuilder();
            sb.AppendLine("{Greeting},");
            sb.AppendLine("{Name} {{!}}");
            string value = sb.ToString();

            var tokens = TemplateParser.Parse(value).ToArray();
            Assert.Equal(4, tokens.Length);

            Assert.IsType<HoleToken>(tokens[0]);
            Assert.IsType<TextToken>(tokens[1]);
            Assert.IsType<HoleToken>(tokens[2]);
            Assert.IsType<TextToken>(tokens[3]);
        }

        [Fact]
        public void Parse_MultiplePropertiesSingleLine_TokensAreInCorrectOrder()
        {
            var tokens = TemplateParser.Parse("{Greeting}, {Name} {{!}}").ToArray();
            Assert.Equal(4, tokens.Length);

            Assert.IsType<HoleToken>(tokens[0]);
            Assert.IsType<TextToken>(tokens[1]);
            Assert.IsType<HoleToken>(tokens[2]);
            Assert.IsType<TextToken>(tokens[3]);
        }

        [Fact]
        public void Parse_PropertyContainsFormatting_TokenContainsFormat()
        {
            var tokens = TemplateParser.Parse("{Time:hh:mm}").ToArray();
            Assert.Equal(1, tokens.Length);

            var token = tokens[0] as HoleToken;
            Assert.NotNull(token);
            Assert.Equal("Time", token.Name);
            Assert.Equal("hh:mm", token.Format);
        }

        [Fact]
        public void Parse_PropertyContainsInvalidAlignment_TokenHasZeroAlignment()
        {
            var tokens = TemplateParser.Parse("{Hello,  -aa}").ToArray();
            Assert.Equal(1, tokens.Length);

            var token = tokens[0] as HoleToken;
            Assert.NotNull(token);
            Assert.Equal("Hello", token.Name);
            Assert.Equal(0, token.Alignment);
        }


        [Fact]
        public void Parse_PropertyContainsNegativeAlignment_TokenContainsAlignment()
        {
            var tokens = TemplateParser.Parse("{Hello,-10}").ToArray();
            Assert.Equal(1, tokens.Length);

            var token = tokens[0] as HoleToken;
            Assert.NotNull(token);
            Assert.Equal("Hello", token.Name);
            Assert.Equal(-10, token.Alignment);
        }

        [Fact]
        public void Parse_PropertyContainsPositiveAlignment_TokenContainsAlignment()
        {
            var tokens = TemplateParser.Parse("{Hello,+10}").ToArray();
            Assert.Equal(1, tokens.Length);

            var token = tokens[0] as HoleToken;
            Assert.NotNull(token);
            Assert.Equal("Hello", token.Name);
            Assert.Equal(10, token.Alignment);
        }

        [Fact]
        public void Parse_PropertyNameIsIndex_TokenHasIndex()
        {
            var tokens = TemplateParser.Parse("{0}").ToArray();
            Assert.Equal(1, tokens.Length);

            var token = tokens[0] as HoleToken;
            Assert.NotNull(token);
            Assert.Equal(0, token.Index);
            Assert.Equal(null, token.Name);
        }

        [Fact]
        public void Parse_PropertyNameIsInvalid_ReturnsTextToken()
        {
            string value = "{123 Hello World!}";

            var tokens = TemplateParser.Parse(value).ToArray();
            Assert.Equal(1, tokens.Length);

            var token = tokens[0] as TextToken;
            Assert.NotNull(token);
            Assert.Equal(value, token.RawValue);
        }

        [Fact]
        public void Parse_PropertyNameIsMissing_ReturnsTextToken()
        {
            string value = "{   }";

            var tokens = TemplateParser.Parse(value).ToArray();
            Assert.Equal(1, tokens.Length);

            var token = tokens[0] as TextToken;
            Assert.NotNull(token);
            Assert.Equal(value, token.RawValue);
        }

        [Fact]
        public void Parse_PropertyNameStartsWithNumber_ReturnsToken()
        {
            string value = "{123_Hello}";

            var tokens = TemplateParser.Parse(value).ToArray();
            Assert.Equal(1, tokens.Length);

            var token = tokens[0] as HoleToken;
            Assert.NotNull(token);
            Assert.Equal("123_Hello", token.Name);
        }

        [Fact]
        public void Parse_PropertyNameSurroundedByWhitespace_ReturnsToken()
        {
            string value = "{  Hello  }";

            var tokens = TemplateParser.Parse(value).ToArray();
            Assert.Equal(1, tokens.Length);

            var token = tokens[0] as HoleToken;
            Assert.NotNull(token);
            Assert.Equal("Hello", token.Name);
        }

        [Fact]
        public void Parse_PropertyNameWithUnderscores_ReturnsToken()
        {
            string value = "{ _123_Hello}";

            var tokens = TemplateParser.Parse(value).ToArray();
            Assert.Equal(1, tokens.Length);

            var token = tokens[0] as HoleToken;
            Assert.NotNull(token);
            Assert.Equal("_123_Hello", token.Name);
        }

        [Fact]
        public void Parse_TemplateIsEmpty_ReturnsTextToken()
        {
            var tokens = TemplateParser.Parse(string.Empty).ToArray();
            Assert.Equal(1, tokens.Length);

            var token = tokens[0] as TextToken;
            Assert.NotNull(token);
            Assert.Equal(string.Empty, token.RawValue);
        }

        [Fact]
        public void Parse_TemplateIsNull_ReturnsTextToken()
        {
            var tokens = TemplateParser.Parse(null).ToArray();
            Assert.Equal(1, tokens.Length);

            var token = tokens[0] as TextToken;
            Assert.NotNull(token);
            Assert.Equal(string.Empty, token.RawValue);
        }

        [Fact]
        public void Parse_TextWithDoubledBraces_ReturnsTextToken()
        {
            string value = "{{ Hello, World ! }}";

            var tokens = TemplateParser.Parse(value).ToArray();
            Assert.Equal(1, tokens.Length);

            var token = tokens[0] as TextToken;
            Assert.NotNull(token);
            Assert.Equal(value, token.RawValue);
        }
    }
}
