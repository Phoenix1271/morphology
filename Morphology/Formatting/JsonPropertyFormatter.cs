using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Morphology.Conversion.Tokens;

namespace Morphology.Formatting
{
    /// <summary>
    /// Formats content of properties to JSON output format.
    /// </summary>
    public class JsonPropertyFormatter : IPropertyFormatter
    {
        #region Private Fields

        private readonly IFormatProvider _formatProvider;
        private readonly TextWriter _output;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a <see cref="JsonPropertyFormatter"/> to format tokens into readable output.
        /// </summary>
        /// <param name="output">The output where content of property should be written.</param>
        /// <param name="cultureInfo">Specific culture to be used for formatting.</param>
        public JsonPropertyFormatter(TextWriter output, CultureInfo cultureInfo = null)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
            _formatProvider = cultureInfo ?? CultureInfo.InvariantCulture;
        }

        #endregion

        #region IPropertyFormatter

        /// <summary>
        /// Formats content of <see cref="ScalarToken"/>.
        /// </summary>
        /// <param name="token">Token to be formatted.</param>
        public void Format(ScalarToken token)
        {
            if (token.Value == null)
            {
                _output.Write("null");
                return;
            }

            string text = token.Value as string;
            if (text != null)
            {
                _output.Write("\"");
                _output.Write(text.Replace("\"", "\\\""));
                _output.Write("\"");
                return;
            }

            //If object has custom formatter then use that one
            var custom = (ICustomFormatter) _formatProvider.GetFormat(typeof(ICustomFormatter));
            if (custom != null)
            {
                _output.Write(custom.Format(null, token.Value, _formatProvider));
                return;
            }

            // Use defaults for all other formatting.
            var f = token.Value as IFormattable;
            _output.Write(f?.ToString(null, _formatProvider) ?? token.Value.ToString());
        }

        /// <summary>
        /// Formats content of <see cref="DictionaryToken"/>.
        /// </summary>
        /// <param name="token">Token to be formatted.</param>
        public void Format(DictionaryToken token)
        {
            _output.Write('[');
            var elements = token.Elements.ToArray();
            int allButLast = elements.Length - 1;
            for (int i = 0; i < allButLast; i++)
            {
                _output.Write("{\"key\": ");
                elements[i].Key.Render(this);
                _output.Write(", \"value\": ");
                elements[i].Value.Render(this);
                _output.Write("}, ");
            }

            if (elements.Length > 0)
            {
                var last = elements[elements.Length - 1];
                _output.Write("{\"key\": ");
                last.Key.Render(this);
                _output.Write(", \"value\": ");
                last.Value.Render(this);
                _output.Write('}');
            }

            _output.Write(']');
        }

        /// <summary>
        /// Formats content of <see cref="SequenceToken"/>.
        /// </summary>
        /// <param name="token">Token to be formatted.</param>
        public void Format(SequenceToken token)
        {
            _output.Write('[');

            int allButLast = token.Elements.Count - 1;
            for (int i = 0; i < allButLast; ++i)
            {
                token.Elements[i].Render(this);
                _output.Write(", ");
            }

            if (token.Elements.Count > 0)
            {
                token.Elements[token.Elements.Count - 1].Render(this);
            }

            _output.Write(']');
        }

        /// <summary>
        /// Formats content of <see cref="StructureToken"/>.
        /// </summary>
        /// <param name="token">Token to be formatted.</param>
        public void Format(StructureToken token)
        {
            if (token.TypeName != null)
            {
                _output.Write($"\"{token.TypeName}\": ");
            }

            _output.Write("{ ");
            int allButLast = token.Properties.Count - 1;
            for (int i = 0; i < allButLast; i++)
            {
                Format(token.Properties[i]);
                _output.Write(", ");
            }

            if (token.Properties.Count > 0)
            {
                var last = token.Properties[token.Properties.Count - 1];
                Format(last);
            }

            _output.Write(" }");
        }

        /// <summary>
        /// Formats content of <see cref="PropertyToken"/>.
        /// </summary>
        /// <param name="property">Token to be formatted.</param>
        public void Format(PropertyToken property)
        {
            _output.Write($"\"{property.Name}\": ");
            property.Value.Render(this);
        }

        #endregion
    }
}
