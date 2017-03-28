using System;
using System.Collections.Generic;
using System.Linq;
using Morphology.Conversion.Converters;
using Morphology.Conversion.Tokens;
using Xunit;
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

        [Fact]
        public void Convert_Array_ReturnsSequenceToken()
        {
            var converter = new PropertyConverter();

            var token = converter.Convert(new[] {1, 2, 3});

            Assert.NotNull(token);
            Assert.IsType<SequenceToken>(token);
        }

        [Fact]
        public void Convert_ByteArray_ReturnsScalarToken()
        {
            var bytes = Enumerable.Range(0, 10).Select(b => (byte) b).ToArray();
            var converter = new PropertyConverter();

            var token = converter.Convert(bytes);

            Assert.NotNull(token);
            Assert.IsType<ScalarToken>(token);
        }

        [Fact]
        public void Convert_CyclicCollections_DoNotStackOverflow()
        {
            var value = new C { D = new D() };
            value.D.C = new List<C?> { value };
            var converter = new PropertyConverter();

            var token = converter.Convert(value);

            Assert.NotNull(token);
            Assert.IsType<StructureToken>(token);
        }

        [Fact]
        public void Convert_CyclicStructure_DoesNotStackOverflow()
        {
            var value = new A { B = new B() };
            value.B.A = value;
            var converter = new PropertyConverter();

            var token = converter.Convert(value);

            Assert.NotNull(token);
            Assert.IsType<StructureToken>(token);
        }

        [Fact]
        public void Convert_DateTime_ReturnsScalarToken()
        {
            var converter = new PropertyConverter();

            var token = converter.Convert(DateTime.Now);

            Assert.NotNull(token);
            Assert.IsType<ScalarToken>(token);
        }

        [Fact]
        public void Convert_DateTimeOffset_ReturnsScalarToken()
        {
            var converter = new PropertyConverter();

            var token = converter.Convert(DateTimeOffset.Now);

            Assert.NotNull(token);
            Assert.IsType<ScalarToken>(token);
        }

        [Fact]
        public void Convert_Delegate_ReturnsScalarToken()
        {
            Action del = Convert_Delegate_ReturnsScalarToken;
            var converter = new PropertyConverter();

            var token = converter.Convert(del);

            Assert.NotNull(token);
            Assert.IsType<ScalarToken>(token);
        }

        [Fact]
        public void Convert_Enum_ReturnsScalarToken()
        {
            var converter = new PropertyConverter();

            var token = converter.Convert(E.Foo);

            Assert.NotNull(token);
            Assert.IsType<ScalarToken>(token);
        }

        [Fact]
        public void Convert_Guid_ReturnsScalarToken()
        {
            var converter = new PropertyConverter();

            var token = converter.Convert(Guid.NewGuid());

            Assert.NotNull(token);
            Assert.IsType<ScalarToken>(token);
        }

        [Fact]
        public void Convert_Null_ReturnsScalarToken()
        {
            var converter = new PropertyConverter();

            var token = converter.Convert(null);

            Assert.NotNull(token);
            Assert.IsType<ScalarToken>(token);
        }

        [Fact]
        public void Convert_ScalarType_ReturnsScalarToken()
        {
            var converter = new PropertyConverter();

            var token = converter.Convert(123);

            Assert.NotNull(token);
            Assert.IsType<ScalarToken>(token);
        }

        [Fact]
        public void Convert_SystemType_ReturnsScalarToken()
        {
            var converter = new PropertyConverter();

            var token = converter.Convert(typeof(string));

            Assert.NotNull(token);
            Assert.IsType<ScalarToken>(token);
        }
    }
}
