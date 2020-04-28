using DateScanner.Pattern;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DateScanner.Regex
{
    internal class RegexBuilder
    {
        private readonly PatternBase _patternBase;
        private readonly System.Text.RegularExpressions.Regex _regex;

        internal RegexBuilder(bool compile, string languages)
        {
            _patternBase = PatternRegistry.LoadPattern(languages);
            _regex = BuildRegex(compile);
        }

        internal (DateTime? date, DateTime? time) Match(string value, DateTime seed)
        {
            DateTime? resultDate = null;
            DateTime? resultTime = null;
            Match match = _regex.Match(value);

            if (!match.Success)
            {
                return (resultDate, resultTime);
            }

            string[] groupNames = _regex.GetGroupNames();

            int? dateMatchKey = GetKey(groupNames, match.Groups, "da");
            if (dateMatchKey.HasValue)
            {
                string[] dateMatchArgs = GetParams(match.Groups, "db" + dateMatchKey);
                resultDate = _patternBase.DatePattern[dateMatchKey.Value](seed, dateMatchArgs);
            }

            int? timeMatchKey = GetKey(groupNames, match.Groups, "ta");
            if (timeMatchKey.HasValue)
            {
                seed = (resultDate ?? seed).Date;
                string[] timeMatchArgs = GetParams(match.Groups, "tb" + timeMatchKey);
                resultTime = _patternBase.TimePattern[timeMatchKey.Value](seed, timeMatchArgs);
            }

            return (resultDate, resultTime);
        }




        private string BuildRegex(PatternCollection pattern, char prefix)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (string key in pattern.Keys)
            {
                string key2 = key.Replace("(", $"(?<{prefix}b{i}>");
                sb.Append($"(?<{prefix}a{i}>{key2})|");
                i++;
            }

            return "(" + sb.ToString(0, sb.Length - 1) + ")";
        }



        private System.Text.RegularExpressions.Regex BuildRegex(bool compile)
        {
            string datePattern = BuildRegex(_patternBase.DatePattern, 'd');
            string timePattern = BuildRegex(_patternBase.TimePattern, 't');

            string regex = $@"({datePattern}\s*{timePattern})|{datePattern}|{timePattern}";

            RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

            if (compile)
            {
                options |= RegexOptions.Compiled;
            }

            return new System.Text.RegularExpressions.Regex(regex, options);
        }

        private int? GetKey(string[] groupNames, GroupCollection collection, string prefix)
        {
            foreach (string groupName in groupNames.Where(x => x.StartsWith(prefix)))
            {
                Group group = collection[groupName];
                if (group.Success)
                {
                    return Convert.ToInt32(groupName.Substring(prefix.Length));
                }
            }

            return null;
        }

        private string[] GetParams(GroupCollection collection, string key)
        {
            Group group = collection[key];
            if (group.Success)
            {
                return group.Captures.OfType<Capture>().Select(c => c.Value).ToArray();
            }
            return Array.Empty<string>();
        }
    }
}
