using System.Collections.Generic;
using Morphology.Conversion;

namespace Morphology.Configuration
{
    public interface IConversionConfig
    {
        #region Public Properties

        /// <summary>
        /// Limits number of bytes that are processed from byte arrays.
        /// </summary>
        /// <remarks>
        /// Default value is set to 1024 bytes before array gets truncated.
        /// </remarks>
        /// <remarks>
        /// Setting <see cref="ByteArrayLimit"/> to 0 will always return whole byte array.
        /// </remarks>
        int ByteArrayLimit { get; }

        /// <summary>
        /// Limits destructuring of the object to given depth.
        /// </summary>
        /// <remarks>
        /// Default value is set to 10 conversion levels deep.
        /// </remarks>
        /// <remarks>
        /// Setting <see cref="ConversionLimit"/> to 0 will apply conversion policies non-recursively.
        /// </remarks>
        int ConversionLimit { get; }

        /// <summary>
        /// Limits the number of items that are processed from types that implements <see langword="IEnumerable"/>.
        /// </summary>
        /// <remarks>
        /// Default value is up to 1000 items from type that implements <see langword="IEnumerable"/>.
        /// </remarks>
        /// <remarks>
        /// Setting <see cref="ItemLimit"/> to 0 all enumerables will return empty sequences.
        /// </remarks>
        int ItemLimit { get; }

        /// <summary>
        /// Additional conversion policies to apply during object destructuring.
        /// </summary>
        /// <remarks>
        /// Policies will be applied before default policy stack.
        /// </remarks>
        IEnumerable<IConversionPolicy> Policies { get; }

        /// <summary>
        /// Limits the number of character that are retrieved from <see langword="string"/>.
        /// </summary>
        /// <remarks>
        /// Default value is 0 that will alwyas return whole string.
        /// </remarks>
        int StringLimit { get; }

        #endregion
    }
}
