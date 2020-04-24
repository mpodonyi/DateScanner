using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using DateScanner.Pattern;
using DateScanner.Utils;

namespace DateScanner
{
    internal class PatternLoader
    {
        private static readonly IReadOnlyDictionary<string, Func<PatternBase>> PatternRegistry =
            new Dictionary<string, Func<PatternBase>>
            {
                ["de"] = () => new GermanPattern(), 
                ["en"] = () => new EnglishPattern(),
            };


        internal static PatternBase LoadPattern(string languagename)
        {
            return PatternRegistry["en"]();
            //MP: more work here
        }

    }
}
