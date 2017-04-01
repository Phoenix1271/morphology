using System;
using System.Collections.Generic;
using System.Linq;
using Morphology.Configuration;
using Morphology.Conversion.Converters;
using Morphology.Conversion.Tokens;
using Xunit;

// ReSharper disable CollectionNeverQueried.Local
// ReSharper disable MemberHidesStaticFromOuterClass
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Morphology.Test.Conversion.Converters
{
    public class PropertyConverterTests
    {
        private class A
        {
            #region Public Properties

            public B B { get; set; }

            #endregion
        }

        private class B
        {
            #region Public Properties

            public A A { get; set; }

            #endregion
        }

        private struct C
        {
            public D D { get; set; }
        }

        private class D
        {
            #region Public Properties

            public IList<C?> C { get; set; }

            #endregion
        }

        private enum E
        {
            Foo
        }

        private class Nested
        {
            #region Public Properties

            public Nested Next { get; set; }

            public int Value { get; set; }

            #endregion
        }


        [Fact]
        public void Convert_Array_ReturnsSequenceToken()
        {
            var config = new DefaultConversionConfig();
            var converter = new PropertyConverter(config);

            var token = converter.Convert(new[] {1, 2, 3});

            Assert.NotNull(token);
            Assert.IsType<SequenceToken>(token);
        }

        [Fact]
        public void Convert_ByteArray_ReturnsScalarToken()
        {
            var config = new DefaultConversionConfig();
            var converter = new PropertyConverter(config);
            var value = Enumerable.Range(0, 10).Select(b => (byte)b).ToArray();

            var token = converter.Convert(value);

            Assert.NotNull(token);
            Assert.IsType<ScalarToken>(token);
        }

        [Fact]
        public void Convert_CyclicCollections_DoNotStackOverflow()
        {
            var config = new DefaultConversionConfig();
            var converter = new PropertyConverter(config);
            var value = new C
            {
                D = new D
                {
                    C = new List<C?>()
                }
            };
            value.D.C.Add(value);

            var token = converter.Convert(value);

            Assert.NotNull(token);
            Assert.IsType<StructureToken>(token);
        }

        [Fact]
        public void Convert_CyclicStructure_DoesNotStackOverflow()
        {
            var config = new DefaultConversionConfig();
            var converter = new PropertyConverter(config);
            var value = new A
            {
                B = new B()
            };
            value.B.A = value;

            var token = converter.Convert(value);

            Assert.NotNull(token);
            Assert.IsType<StructureToken>(token);
        }

        [Fact]
        public void Convert_DateTime_ReturnsScalarToken()
        {
            var config = new DefaultConversionConfig();
            var converter = new PropertyConverter(config);

            var token = converter.Convert(DateTime.Now);

            Assert.NotNull(token);
            Assert.IsType<ScalarToken>(token);
        }

        [Fact]
        public void Convert_DateTimeOffset_ReturnsScalarToken()
        {
            var config = new DefaultConversionConfig();
            var converter = new PropertyConverter(config);

            var token = converter.Convert(DateTimeOffset.Now);

            Assert.NotNull(token);
            Assert.IsType<ScalarToken>(token);
        }

        [Fact]
        public void Convert_Delegate_ReturnsScalarToken()
        {
            var config = new DefaultConversionConfig();
            var converter = new PropertyConverter(config);
            Action value = Convert_Delegate_ReturnsScalarToken;

            var token = converter.Convert(value);

            Assert.NotNull(token);
            Assert.IsType<ScalarToken>(token);
        }

        [Fact]
        public void Convert_DepthLimitZero_ReturnsStructureToken()
        {
            var config = new DefaultConversionConfig();
            var converter = new PropertyConverter(config);
            var value = new Nested
            {
                Value = 1,
                Next = new Nested
                {
                    Value = 10,
                    Next = new Nested
                    {
                        Value = 100
                    }
                }
            };

            var structure = converter.Convert(value, 0) as StructureToken;

            Assert.NotNull(structure);
            Assert.Equal(2, structure.Properties.Count);
            Assert.Equal(nameof(Nested.Next), structure.Properties[0].Name);
            Assert.Equal(nameof(Nested.Value), structure.Properties[1].Name);

            var scalar = structure.Properties[0].Token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Null(scalar.Value);
        }

        [Fact]
        public void Convert_DictionaryKeyIsComplex_ReturnsSequenceToken()
        {
            var config = new DefaultConversionConfig();
            var converter = new PropertyConverter(config);
            var value = new Dictionary<A, string> { { new A(), "hello" } };

            var token = converter.Convert(value);

            Assert.NotNull(token);
            Assert.IsType<SequenceToken>(token);
        }

        [Fact]
        public void Convert_DictionaryKeyIsScalar_ReturnsDictionaryToken()
        {
            var config = new DefaultConversionConfig();
            var converter = new PropertyConverter(config);
            var value = new Dictionary<int, string> { { 1, "hello" } };

            var token = converter.Convert(value);

            Assert.NotNull(token);
            Assert.IsType<DictionaryToken>(token);
        }

        [Fact]
        public void Convert_Enum_ReturnsScalarToken()
        {
            var config = new DefaultConversionConfig();
            var converter = new PropertyConverter(config);

            var token = converter.Convert(E.Foo);

            Assert.NotNull(token);
            Assert.IsType<ScalarToken>(token);
        }

        [Fact]
        public void Convert_Guid_ReturnsScalarToken()
        {
            var config = new DefaultConversionConfig();
            var converter = new PropertyConverter(config);

            var token = converter.Convert(Guid.NewGuid());

            Assert.NotNull(token);
            Assert.IsType<ScalarToken>(token);
        }

        [Fact]
        public void Convert_Null_ReturnsScalarToken()
        {
            var config = new DefaultConversionConfig();
            var converter = new PropertyConverter(config);

            var token = converter.Convert(null);

            Assert.NotNull(token);
            Assert.IsType<ScalarToken>(token);
        }

        [Fact]
        public void Convert_ScalarType_ReturnsScalarToken()
        {
            var config = new DefaultConversionConfig();
            var converter = new PropertyConverter(config);

            var token = converter.Convert(123);

            Assert.NotNull(token);
            Assert.IsType<ScalarToken>(token);
        }

        [Fact]
        public void Convert_SystemType_ReturnsScalarToken()
        {
            var config = new DefaultConversionConfig();
            var converter = new PropertyConverter(config);

            var token = converter.Convert(typeof(string));

            Assert.NotNull(token);
            Assert.IsType<ScalarToken>(token);
        }
    }
}
