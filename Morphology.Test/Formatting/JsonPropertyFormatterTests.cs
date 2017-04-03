using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Morphology.Conversion;
using Morphology.Conversion.Tokens;
using Morphology.Formatting;
using Morphology.Test.Support;
using Xunit;

namespace Morphology.Test.Formatting
{
    public class JsonPropertyFormatterTests
    {
        private class Nonformatable
        {
            #region Public Methods

            public override string ToString()
            {
                return "Hello world";
            }

            #endregion
        }

        [Fact]
        public void Format_DecimalScalarToken_IsFormatedAsNumber()
        {
            var output = new StringWriter();
            var formater = new JsonPropertyFormatter(output);
            decimal value = Some.Decimal();
            var token = new ScalarToken(value);

            token.Render(formater);

            Assert.Equal(value.ToString(CultureInfo.InvariantCulture), output.ToString());
        }

        [Fact]
        public void Format_DecimalScalarToken_IsFormatedUsingSpecificCulture()
        {
            var formatProvider = new CultureInfo("cs-CZ");
            var output = new StringWriter();
            var formater = new JsonPropertyFormatter(output, formatProvider);
            decimal value = Some.Decimal();
            var token = new ScalarToken(value);

            token.Render(formater);

            Assert.Equal(value.ToString(formatProvider), output.ToString());
        }

        [Fact]
        public void Format_EmptySequenceToken_IsFormattedAsArray()
        {
            var output = new StringWriter();
            var formater = new JsonPropertyFormatter(output);
            var token = new SequenceToken(null);

            token.Render(formater);

            Assert.Equal("[]", output.ToString());
        }

        [Fact]
        public void Format_IntScalarToken_IsFormatedAsNumber()
        {
            var output = new StringWriter();
            var formater = new JsonPropertyFormatter(output);
            decimal value = Some.Int();
            var token = new ScalarToken(value);

            token.Render(formater);

            Assert.Equal(value.ToString(CultureInfo.InvariantCulture), output.ToString());
        }

        [Fact]
        public void Format_NonEmptySequenceToken_IsFormattedAsArray()
        {
            var output = new StringWriter();
            var formater = new JsonPropertyFormatter(output);
            var token = new SequenceToken(new[] {new ScalarToken(1), new ScalarToken("foo")});

            token.Render(formater);

            Assert.Equal("[1, \"foo\"]", output.ToString());
        }

        [Fact]
        public void Format_NonFormattableScalarToken_IsFormatedFromToString()
        {
            var output = new StringWriter();
            var formater = new JsonPropertyFormatter(output);
            var value = new Nonformatable();
            var token = new ScalarToken(value);

            token.Render(formater);

            Assert.Equal(value.ToString(), output.ToString());
        }

        [Fact]
        public void Format_NullScalarToken_IsFormatedAsNullString()
        {
            var output = new StringWriter();
            var formater = new JsonPropertyFormatter(output);
            var token = new ScalarToken(null);

            token.Render(formater);

            Assert.Equal("null", output.ToString());
        }

        [Fact]
        public void Format_StringScalarToken_SpecialCharactersAreEscaped()
        {
            var output = new StringWriter();
            var formater = new JsonPropertyFormatter(output);
            var token = new ScalarToken(" \" ");

            token.Render(formater);

            Assert.Equal("\" \\\" \"", output.ToString());
        }

        [Fact]
        public void JsonPropertyFormatter_OutputIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonPropertyFormatter(null));
        }

        [Fact]
        public void Render_EmptyDictionaryToken_IsFormattedAsEmptyArray()
        {
            var output = new StringWriter();
            var formater = new JsonPropertyFormatter(output);
            var token = new DictionaryToken(null);

            token.Render(formater);

            Assert.Equal("[]", output.ToString());
        }

        [Fact]
        public void Render_NonEmptyDictionaryToken_IsFormattedAsArrayOfKeyValuePairs()
        {
            var output = new StringWriter();
            var formater = new JsonPropertyFormatter(output);
            var token = new DictionaryToken(new[]
            {
                new KeyValuePair<ScalarToken, IPropertyToken>(
                    new ScalarToken("foo"), new ScalarToken(1)),
                new KeyValuePair<ScalarToken, IPropertyToken>(
                    new ScalarToken("bar"), new SequenceToken(new[] {new ScalarToken(1.2)}))
            });

            token.Render(formater);

            Assert.Equal("[{\"key\": \"foo\", \"value\": 1}, {\"key\": \"bar\", \"value\": [1.2]}]", output.ToString());
        }

        [Fact]
        public void Render_PropertyToken_IsFormattedAsProperty()
        {
            var output = new StringWriter();
            var formater = new JsonPropertyFormatter(output);
            var property = new PropertyToken("property", new ScalarToken(1));

            property.Render(formater);

            Assert.Equal("\"property\": 1", output.ToString());
        }

        [Fact]
        public void Render_StructureToken_IsFormattedAsSetOfProperties()
        {
            var output = new StringWriter();
            var formater = new JsonPropertyFormatter(output);
            var token = new StructureToken(new[]
            {
                new PropertyToken("Foo", new ScalarToken(1)),
                new PropertyToken("Bar", new ScalarToken("bar"))
            }, "MyType");


            token.Render(formater);

            Assert.Equal("\"MyType\": { \"Foo\": 1, \"Bar\": \"bar\" }", output.ToString());
        }
    }
}
