using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DateScanner
{

    public static class StringExtension
    {
        public static double ToPNumber(this string obj) => Convert.ToDouble(obj);

    }

    public class PatternCollection : Dictionary<string, Func<DateTime, string, DateTime>>
    {}

    internal static class RegexBuilder
    {
        private static Lazy<Regex> _Regex =new Lazy<Regex>(BuildRegex);
        public static Regex Regex => _Regex.Value;
        
        
        private static readonly PatternCollection DatePattern = new PatternCollection()
        {
            ["tomorrow"] = (date, _) => date.AddDays(1),
            ["tom(o)r(r)ow"] = (date, _) => date.AddDays(1),
            ["day after tomorrow"] = (date, _) => date.AddDays(2),
            [@"in (\d+) days"] = (date, i) => date.AddDays(i.ToPNumber())
        };

        private static readonly PatternCollection TimePattern = new PatternCollection()
        {
            [@"at (\d+) o clock"] = (date, _) => date.AddDays(2),
        };


        private static string BuildRegex(PatternCollection pattern, char prefix)
        {
            StringBuilder sb = new StringBuilder();
            int i=1;
            foreach (var key in pattern.Keys)
            {
                var key2 = key.Replace("(",$"(?<{prefix}{i}1>");
                sb.Append($"(?<{prefix}{i}0>{key2})|");
                i++;
            }
                  
            return sb.ToString(0,sb.Length-1);
         }   

       

        private static Regex BuildRegex()
        {
            var datePattern =BuildRegex(DatePattern,'d');
            var timePattern =BuildRegex(TimePattern,'t');

            var regex = $@"({datePattern})\s*({timePattern})";
                            
            return new Regex(regex, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
        }

        





    }
}
