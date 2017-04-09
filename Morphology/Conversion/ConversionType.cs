namespace Morphology.Conversion
{
    /// <summary>
    /// Type of conversion to be used for given property
    /// </summary>
    public enum ConversionType
    {
        /// <summary>
        /// Convert known types to scalars, destructure objects and collections
        /// into sequences and structures. Prefix name with '@'.
        /// </summary>
        Destructure,

        /// <summary>
        /// Convert all types to scalar strings. Prefix property with '$'.
        /// </summary>
        Stringify
    }
}