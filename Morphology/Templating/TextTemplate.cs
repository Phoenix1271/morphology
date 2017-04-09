using System.Collections.Generic;
using System.Linq;

namespace Morphology.Templating
{
    /// <summary>
    /// Represent text template for given message
    /// </summary>
    public class TextTemplate
    {
        #region Constructors

        /// <summary>
        /// Copy constructor that initializes a new instance of the <see cref="TextTemplate"/> class.
        /// </summary>
        internal TextTemplate(TextTemplate template)
        {
            Template = template.Template;
            Tokens = template.Tokens;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextTemplate"/> class.
        /// </summary>
        /// <param name="template">The underlying text temmplate.</param>
        /// <param name="tokens">The tokens that belong to given text template.</param>
        internal TextTemplate(string template, IEnumerable<ITemplateToken> tokens)
        {
            Template = template;
            Tokens = tokens.ToArray();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Underlying text template.
        /// </summary>
        public string Template { get; }

        /// <summary>
        /// Template tokens associated with template.
        /// </summary>
        public IReadOnlyList<ITemplateToken> Tokens { get; }

        #endregion
    }
}