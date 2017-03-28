using System;
using System.Collections.Generic;
using Morphology.Extensions;
using Morphology.Test.Support;
using Xunit;

namespace Morphology.Test.Extensions
{
    public class TypeExtensionTests
    {
        private class Base
        {
            #region Public Properties

            public int Foo { get; }

            #endregion
        }

        private class Derived : Base
        {
            #region Public Properties

            public string Bar { get; }

            #endregion
        }

        [Fact]
        public void IsCompilerGenerated_AnonymousType_ReturnsTrue()
        {
            var foobar = new
            {
                Foo = Some.String(),
                Bar = Some.Int()
            };

            Assert.True(foobar.GetType().IsCompilerGenerated());
        }

        [Fact]
        public void IsCompilerGenerated_NullType_ThrowsArgumentNullException()
        {
            Type type = null;
            Assert.Throws<ArgumentNullException>(() => type.IsCompilerGenerated());
        }

        [Fact]
        public void IsCompilerGenerated_UserType_ReturnsFalse()
        {
            Assert.False(typeof(TypeExtensionTests).IsCompilerGenerated());
        }

        [Fact]
        public void IsDictionary_KeyIsComplexType_ReturnsFalse()
        {
            Assert.False(typeof(Dictionary<Derived, string>).IsDictionary());
        }

        [Fact]
        public void IsDictionary_KeyIsScalar_ReturnsTrue()
        {
            Assert.True(typeof(Dictionary<string, string>).IsDictionary());
        }

        [Fact]
        public void IsDictionary_NullValue_ThrowsArgumentNullException()
        {
            Type type = null;
            Assert.Throws<ArgumentNullException>(() => type.IsDictionary());
        }
    }
}
