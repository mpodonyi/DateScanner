using DateScanner.Pattern.Localizations;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DateScanner.Pattern
{
    internal static class PatternRegistry
    {
        private const string FallbackLanguage = "en";

        private static readonly IReadOnlyDictionary<string, Func<PatternBase>> _PatternRegistry =
            new Dictionary<string, Func<PatternBase>>
            {
                ["de"] = () => new GermanPattern(),
                [FallbackLanguage] = () => new EnglishPattern(),
            };


        internal static PatternBase LoadPattern(string languagename)
        {
            languagename ??= CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

            if (_PatternRegistry.ContainsKey(languagename))
            {
                _PatternRegistry[languagename]();
            }

            return _PatternRegistry[FallbackLanguage]();
            //MP: maybe allow multiple languages
        }

    }
}
