namespace Morphology.Templating
{
    /// <summary>
    /// Represents one token in text template
    /// </summary>
    public interface ITemplateToken
    {
        /// <summary>
        /// Gets raw value of the token.
        /// </summary>
        string RawValue { get; }
    }
}
