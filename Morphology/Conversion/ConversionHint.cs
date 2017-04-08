namespace Morphology.Conversion
{
    /// <summary>
    /// Hint to determine how the property should be converted
    /// </summary>
    public enum ConversionHint
    {
        /// <summary>
        /// Default behavior set by configuration
        /// </summary>
        Default,

        /// <summary>
        /// Converts property to string representation
        /// </summary>
        String,

        /// <summary>
        /// Converts property to structure representation
        /// </summary>
        Structure
    }
}