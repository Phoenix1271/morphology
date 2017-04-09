using Moq;
using Morphology.Configuration;
using Morphology.Conversion.Tokens;
using Morphology.Templating;
using Morphology.Templating.Tokens;
using Xunit;

namespace Morphology.Test.Templating
{
    public class TemplateProcessorTests
    {
        [Fact]
        public void Create_InvalidNumberOfIndexParameters_ReturnsTextTemplate()
        {
            var logger = Mock.Of<ILogger>();
            var config = new DefaultConversionConfig(logger);
            var processor = new TemplateProcessor(config, logger);
            object[] properties = {1};

            var template = processor.Create("{0} {1}", properties);
            Assert.NotNull(template);
            Assert.Equal(3, template.Tokens.Count);

            var token = template.Tokens[0] as BoundToken;
            Assert.NotNull(token);
            Assert.NotNull(token.Property);
            Assert.Equal("{0}", token.Property.Name);

            token = template.Tokens[2] as BoundToken;
            Assert.NotNull(token);
            Assert.NotNull(token.Property);
            Assert.Equal("{1}", token.Property.Name);

            var scalar = token.Property.Value as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal("<Property not bound>", scalar.Value);
        }

        [Fact]
        public void Create_InvalidNumberOfNamedParameters_ReturnsTextTemplate()
        {
            var logger = Mock.Of<ILogger>();
            var config = new DefaultConversionConfig(logger);
            var processor = new TemplateProcessor(config, logger);
            object[] properties = {1};

            var template = processor.Create("{Hello} {World}", properties);
            Assert.NotNull(template);
            Assert.Equal(3, template.Tokens.Count);

            var token = template.Tokens[0] as BoundToken;
            Assert.NotNull(token);
            Assert.NotNull(token.Property);
            Assert.Equal("Hello", token.Property.Name);

            token = template.Tokens[2] as BoundToken;
            Assert.NotNull(token);
            Assert.NotNull(token.Property);
            Assert.Equal("World", token.Property.Name);

            var scalar = token.Property.Value as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal("<Property not bound>", scalar.Value);
        }

        [Fact]
        public void Create_NegativeIndexedProperty_ReturnsTextTemplate()
        {
            var logger = Mock.Of<ILogger>();
            var config = new DefaultConversionConfig(logger);
            var processor = new TemplateProcessor(config, logger);
            object[] properties = {1};

            var template = processor.Create("{-1}", properties);
            Assert.NotNull(template);
            Assert.Equal(1, template.Tokens.Count);

            var token = template.Tokens[0] as BoundToken;
            Assert.NotNull(token);
            Assert.NotNull(token.Property);
            Assert.Equal("{-1}", token.Property.Name);

            var scalar = token.Property.Value as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal("<Property not bound>", scalar.Value);
        }

        [Fact]
        public void Create_NullParameters_ReturnsTextTemplate()
        {
            var logger = Mock.Of<ILogger>();
            var config = new DefaultConversionConfig(logger);
            var processor = new TemplateProcessor(config, logger);

            var template = processor.Create(string.Empty, null);
            Assert.NotNull(template);
            Assert.Equal(1, template.Tokens.Count);
            Assert.IsType<TextToken>(template.Tokens[0]);
        }

        [Fact]
        public void Create_NullTemplate_ReturnsTextTemplate()
        {
            var logger = Mock.Of<ILogger>();
            var config = new DefaultConversionConfig(logger);
            var processor = new TemplateProcessor(config, logger);
            object[] properties = {1};

            var template = processor.Create(null, properties);
            Assert.NotNull(template);
            Assert.Equal(1, template.Tokens.Count);
            Assert.IsType<TextToken>(template.Tokens[0]);
        }

        [Fact]
        public void Create_TemplaHasMixedParameters_ReturnsTextTemplate()
        {
            var logger = Mock.Of<ILogger>();
            var config = new DefaultConversionConfig(logger);
            var processor = new TemplateProcessor(config, logger);
            object[] properties = {1};

            var template = processor.Create("{0} {Second}", properties);
            Assert.NotNull(template);
            Assert.Equal(3, template.Tokens.Count);

            var token = template.Tokens[0] as BoundToken;
            Assert.NotNull(token);
            Assert.NotNull(token.Property);
            Assert.Equal("{0}", token.Property.Name);

            Assert.IsType<TextToken>(template.Tokens[1]);

            token = template.Tokens[2] as BoundToken;
            Assert.NotNull(token);
            Assert.NotNull(token.Property);
            Assert.Equal("Second", token.Property.Name);
        }

        [Fact]
        public void Create_TemplateHasIndexParameters_ReturnsTextTemplate()
        {
            var logger = Mock.Of<ILogger>();
            var config = new DefaultConversionConfig(logger);
            var processor = new TemplateProcessor(config, logger);
            object[] properties = {1, 2};

            var template = processor.Create("{0} {1}", properties);
            Assert.NotNull(template);
            Assert.Equal(3, template.Tokens.Count);

            var token = template.Tokens[0] as BoundToken;
            Assert.NotNull(token);
            Assert.NotNull(token.Property);
            Assert.Equal("{0}", token.Property.Name);

            Assert.IsType<TextToken>(template.Tokens[1]);

            token = template.Tokens[2] as BoundToken;
            Assert.NotNull(token);
            Assert.NotNull(token.Property);
            Assert.Equal("{1}", token.Property.Name);
        }

        [Fact]
        public void Create_TemplateHasNamedParameters_ReturnsTextTemplate()
        {
            var logger = Mock.Of<ILogger>();
            var config = new DefaultConversionConfig(logger);
            var processor = new TemplateProcessor(config, logger);
            object[] properties = {1, 2};

            var template = processor.Create("{Hello} {World}", properties);
            Assert.NotNull(template);
            Assert.Equal(3, template.Tokens.Count);

            var token = template.Tokens[0] as BoundToken;
            Assert.NotNull(token);
            Assert.NotNull(token.Property);
            Assert.Equal("Hello", token.Property.Name);

            Assert.IsType<TextToken>(template.Tokens[1]);

            token = template.Tokens[2] as BoundToken;
            Assert.NotNull(token);
            Assert.NotNull(token.Property);
            Assert.Equal("World", token.Property.Name);
        }
    }
}
