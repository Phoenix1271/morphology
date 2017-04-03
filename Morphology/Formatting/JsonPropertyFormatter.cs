using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Morphology.Conversion.Tokens;

namespace Morphology.Formatting
{
    public class JsonPropertyFormatter : IPropertyFormatter
    {
        #region Private Fields

        private readonly IFormatProvider _formatProvider;
        private readonly TextWriter _output;

        #endregion

        #region Constructors

        public JsonPropertyFormatter(TextWriter output, CultureInfo formatProvider = null)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
            _formatProvider = formatProvider ?? CultureInfo.InvariantCulture;
        }

        #endregion

        #region IPropertyFormatter

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

        public void Format(PropertyToken property)
        {
            _output.Write($"\"{property.Name}\": ");
            property.Value.Render(this);
        }

        #endregion
    }
}
