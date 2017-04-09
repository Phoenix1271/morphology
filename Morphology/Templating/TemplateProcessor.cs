using System.Collections.Generic;
using Morphology.Configuration;
using Morphology.Conversion;
using Morphology.Conversion.Tokens;
using Morphology.Templating.Templates;
using Morphology.Templating.Tokens;

namespace Morphology.Templating
{
    /// <summary>
    /// Processes text template and binds the supplied properties
    /// </summary>
    internal class TemplateProcessor
    {
        #region Private Fields

        private static readonly ScalarToken TokenNotBound = new ScalarToken("<Property not bound>");

        private readonly PropertyConverterFactory _factory;
        private readonly ILogger _logger;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="TemplateProcessor"/>
        /// </summary>
        /// <param name="config">Configuration for property conversion.</param>
        /// <param name="logger">Logger for logging conversion errors.</param>
        public TemplateProcessor(IConversionConfig config, ILogger logger)
        {
            _factory = new PropertyConverterFactory(config, logger);
            _logger = logger;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new log message from supplied values.
        /// </summary>
        /// <param name="message">The message that should be templated.</param>
        /// <param name="parameters">The parameters of the message.</param>
        /// <returns></returns>
        public TextTemplate Create(string message, object[] parameters)
        {
            if (message == null) message = string.Empty;

            //TODO This can be cached
            var tokens = TemplateParser.Parse(message);
            var template = new TextTemplate(message, tokens);

            var boundTokens = BindParameters(template, parameters);
            return new TextTemplate(template.Template, boundTokens);
        }

        #endregion

        #region Private Methods

        private IEnumerable<ITemplateToken> BindParameters(TextTemplate template, object[] parameters)
        {
            int emptyPostion = 0;
            var namedPosition = new Dictionary<string, int>();
            foreach (var token in template.Tokens)
            {
                var hole = token as HoleToken;
                if (hole == null)
                {
                    yield return token;
                    continue;
                }

                int position;
                if (hole.Index.HasValue)
                {
                    position = hole.Index.Value;
                }
                else
                {
                    if (!namedPosition.TryGetValue(hole.Name, out position))
                    {
                        namedPosition.Add(hole.Name, emptyPostion);
                        position = emptyPostion;
                        emptyPostion++;
                    }
                }

                if (position < 0 || position >= parameters.Length)
                {
                    _logger.Warning(
                        $"Required parameter not provided for position {position} in template {template.Template}");

                    yield return new BoundToken(hole, new PropertyToken(hole.Name, TokenNotBound));
                    continue;
                }


                var converter = _factory.Create(hole.ConversionHint);
                var propertyToken = converter.Convert(parameters[position]);
                var property = new PropertyToken(hole.Name, propertyToken);
                yield return new BoundToken(hole, property);
            }
        }

        #endregion
    }
}
