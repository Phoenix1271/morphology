using System;
using Moq;
using Morphology.Conversion;
using Morphology.Conversion.Policies;
using Morphology.Conversion.Tokens;
using Xunit;

namespace Morphology.Test.Conversion.Policies
{
    public class ScalarConversionPolicyTests
    {
        [Fact]
        public void TryConvert_Array_ReturnsFalse()
        {
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ScalarConversionPolicy();

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, new int[0], out token));
        }

        [Fact]
        public void TryConvert_BoolValue_ReturnsScalarToken()
        {
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ScalarConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, true, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(scalar.Value, true);
        }

        [Fact]
        public void TryConvert_ByteValue_ReturnsScalarToken()
        {
            byte payload = 10;
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ScalarConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, payload, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(scalar.Value, payload);
        }

        [Fact]
        public void TryConvert_DateTimeOffsetValue_ReturnsScalarToken()
        {
            var payload = DateTimeOffset.Now;
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ScalarConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, payload, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(scalar.Value, payload);
        }

        [Fact]
        public void TryConvert_DateTimeValue_ReturnsScalarToken()
        {
            var payload = DateTime.Now;
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ScalarConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, payload, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(scalar.Value, payload);
        }

        [Fact]
        public void TryConvert_DecimalValue_ReturnsScalarToken()
        {
            decimal payload = 10.0M;
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ScalarConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, payload, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(scalar.Value, payload);
        }

        [Fact]
        public void TryConvert_DoubleValue_ReturnsScalarToken()
        {
            double payload = 10.0f;
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ScalarConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, payload, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(scalar.Value, payload);
        }

        [Fact]
        public void TryConvert_FloatValue_ReturnsScalarToken()
        {
            float payload = 10.0f;
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ScalarConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, payload, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(scalar.Value, payload);
        }

        [Fact]
        public void TryConvert_GuidValue_ReturnsScalarToken()
        {
            var payload = Guid.NewGuid();
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ScalarConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, payload, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(scalar.Value, payload);
        }

        [Fact]
        public void TryConvert_CharValue_ReturnsScalarToken()
        {
            char payload = 'A';
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ScalarConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, payload, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(scalar.Value, payload);
        }

        [Fact]
        public void TryConvert_Int16Value_ReturnsScalarToken()
        {
            ushort payload = 10;
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ScalarConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, payload, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(scalar.Value, payload);
        }

        [Fact]
        public void TryConvert_Int64Value_ReturnsScalarToken()
        {
            long payload = 10;
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ScalarConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, payload, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(scalar.Value, payload);
        }

        [Fact]
        public void TryConvert_NullConverter_ThrowsArgumentNullException()
        {
            var policy = new ScalarConversionPolicy();

            IPropertyToken token;
            Assert.Throws<ArgumentNullException>(() => policy.TryConvert(null, null, out token));
        }

        [Fact]
        public void TryConvert_NullValue_ReturnsFalse()
        {
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ScalarConversionPolicy();

            IPropertyToken token;
            Assert.False(policy.TryConvert(converter, null, out token));
        }

        [Fact]
        public void TryConvert_ShortValue_ReturnsScalarToken()
        {
            short payload = 10;
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ScalarConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, payload, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(scalar.Value, payload);
        }

        [Fact]
        public void TryConvert_StringValue_ReturnsScalarToken()
        {
            string payload = "Hello World!";
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ScalarConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, payload, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(scalar.Value, payload);
        }

        [Fact]
        public void TryConvert_TimeSpanValue_ReturnsScalarToken()
        {
            var payload = TimeSpan.FromHours(1);
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ScalarConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, payload, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(scalar.Value, payload);
        }

        [Fact]
        public void TryConvert_UInt16Value_ReturnsScalarToken()
        {
            ushort payload = 10;
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ScalarConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, payload, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(scalar.Value, payload);
        }

        [Fact]
        public void TryConvert_UInt32Value_ReturnsScalarToken()
        {
            ushort payload = 10;
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ScalarConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, payload, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(scalar.Value, payload);
        }

        [Fact]
        public void TryConvert_UInt64Value_ReturnsScalarToken()
        {
            ulong payload = 10;
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ScalarConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, payload, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(scalar.Value, payload);
        }

        [Fact]
        public void TryConvert_UriValue_ReturnsScalarToken()
        {
            var payload = new Uri("http://localhost/");
            var converter = Mock.Of<IPropertyConverter>();
            var policy = new ScalarConversionPolicy();

            IPropertyToken token;
            Assert.True(policy.TryConvert(converter, payload, out token));

            var scalar = token as ScalarToken;
            Assert.NotNull(scalar);
            Assert.Equal(scalar.Value, payload);
        }
    }
}
