using System.Collections.Generic;
using System.Text.RegularExpressions;
using Morphology.Templating.Tokens;

namespace Morphology.Templating
{
    /// <summary>
    /// Parser for text templates
    /// </summary>
    internal static class TemplateParser
    {
        private static readonly Regex Grammar = InitializeGrammar();

        /// <summary>
        /// Parses the specified text template.
        /// </summary>
        /// <param name="template">The text template to parse.</param>
        /// <returns>Collection of tokens for specified text template.</returns>
        public static IEnumerable<ITemplateToken> Parse(string template)
        {
            if (string.IsNullOrWhiteSpace(template))
            {
                yield return new TextToken(template);
                yield break;
            }

            foreach (Match match in Grammar.Matches(template))
            {
                if (match.Groups["hole"].Success)
                {
                    yield return new HoleToken(match);
                }

                if (match.Groups["text"].Success)
                {
                    yield return new TextToken(match);
                }
            }
        }

        #region Private Methods

        private static Regex InitializeGrammar()
        {
            string hintPattern = @"((?<hint>[@|$])|\W)";
            string indexPattern = @"(?<index>\d+)";
            string namePattern = @"(?<name>\w+)";
            string alignmentPattern = @"(,\s*((?<alignment>[+-]?\d+[^\}:])|[^\}:]+))";
            string formatPattern = @"(:\s*(?<format>[^\}]+))";
            string propertyPattern = $"{hintPattern}?\\b({indexPattern}|{namePattern})\\b{alignmentPattern}?{formatPattern}?";
            string holePattern = $"(?<hole>{{\\s*{propertyPattern}\\s*}})";
            string textPattern = $"(?<text>([^\\{{]|{{{{|}}}}|{{(?!\\s*{propertyPattern}\\s*}}))+)";
            string templatePattern = $"({textPattern}|{holePattern})";

            var options = RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant;

            return new Regex(templatePattern, options);
        }

        #endregion
    }
}
