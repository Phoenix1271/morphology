using System;
using System.Runtime.CompilerServices;
using Moq;
using Morphology.Conversion;
using Morphology.Conversion.Policies;
using Morphology.Conversion.Tokens;
using Morphology.Test.Support;
using Xunit;

namespace Morphology.Test.Conversion.Policies
{
    public class StructureConversionPolicyTests
    {
        private class PropertyThrower
        {
            #region Public Properties

            public string Doesnt => "Hello";
            public string Throws => throw new NotSupportedException();

            #endregion
        }

        private class IndexerThrower
        {
            #region Public Properties

            public string Doesnt => "Hello";

            [IndexerName("Throws")]
            // ReSharper disable once UnusedMember.Local
            // ReSharper disable once UnusedParameter.Local
            public int this[int index] => throw new NotSupportedException();

            #endregion
        }

        [Fact]
        public void TryConvert_AnonymousType_ReturnsStructureToken()
        {
            var logger = Mock.Of<ILogger>();
            var policy = new StructureConversionPolicy(logger);
            var value = new
            {
                Foo = Some.String(),
                Bar = Some.Int()
            };

            var converterMock = new Mock<IPropertyConverter>();
            converterMock.Setup(m => m.Convert(It.IsAny<object>())).Returns<object>(o => new ScalarToken(o));
            var converter = converterMock.Object;

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, value, out token));

            var structure = token as StructureToken;
            Assert.NotNull(structure);
            Assert.Null(structure.TypeName);
            Assert.Equal(2, structure.Properties.Count);
        }

        [Fact]
        public void TryConvert_GetterThrows_eturnsStructureToken()
        {
            var logger = Mock.Of<ILogger>();
            var policy = new StructureConversionPolicy(logger);

            var converterMock = new Mock<IPropertyConverter>();
            converterMock.Setup(m => m.Convert(It.IsAny<object>())).Returns<object>(o => new ScalarToken(o));
            var converter = converterMock.Object;

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, new PropertyThrower(), out token));

            var structure = token as StructureToken;
            Assert.NotNull(structure);
            Assert.Equal(nameof(PropertyThrower), structure.TypeName);
            Assert.Equal(2, structure.Properties.Count);
            Assert.Equal(nameof(PropertyThrower.Doesnt), structure.Properties[0].Name);
            Assert.IsType<ScalarToken>(structure.Properties[0].Token);
            Assert.Equal(nameof(PropertyThrower.Throws), structure.Properties[1].Name);
            Assert.IsType<ScalarToken>(structure.Properties[1].Token);
        }

        [Fact]
        public void TryConvert_NamedIndexerThrows_ReturnsStructureToken()
        {
            var logger = Mock.Of<ILogger>();
            var policy = new StructureConversionPolicy(logger);

            var converterMock = new Mock<IPropertyConverter>();
            converterMock.Setup(m => m.Convert(It.IsAny<object>())).Returns<object>(o => new ScalarToken(o));
            var converter = converterMock.Object;

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, new IndexerThrower(), out token));

            var structure = token as StructureToken;
            Assert.NotNull(structure);
            Assert.Equal(nameof(IndexerThrower), structure.TypeName);
            Assert.Equal(1, structure.Properties.Count);
            Assert.Equal(nameof(IndexerThrower.Doesnt), structure.Properties[0].Name);
            Assert.IsType<ScalarToken>(structure.Properties[0].Token);
        }


        [Fact]
        public void TryConvert_NullConverter_ThrowsArgumentNullException()
        {
            var logger = Mock.Of<ILogger>();
            var policy = new StructureConversionPolicy(logger);

            IPropertyToken token;
            Assert.Throws<ArgumentNullException>(() => policy.TryConvert(null, null, out token));
        }

        [Fact]
        public void TryConvert_NullValue_ReturnsFalse()
        {
            var logger = Mock.Of<ILogger>();
            var policy = new StructureConversionPolicy(logger);

            var converter = Mock.Of<IPropertyConverter>();

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, null, out token));
        }

        [Fact]
        public void TryConvert_Object_ReturnsStructureToken()
        {
            var logger = Mock.Of<ILogger>();
            var policy = new StructureConversionPolicy(logger);

            var converter = Mock.Of<IPropertyConverter>();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, new object(), out token));

            var structure = token as StructureToken;
            Assert.NotNull(structure);
            Assert.Equal(typeof(object).Name, structure.TypeName);
            Assert.Equal(0, structure.Properties.Count);
        }
    }
}
