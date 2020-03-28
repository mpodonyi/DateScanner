using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace DateScanner
{

    public static class StringExtension
    {
        public static double ToPNumber(this string obj) => Convert.ToDouble(obj);

    }

    public class PatternCollection : List<KeyValuePair<string, Func<DateTime, string[], DateTime>>>
    {
        public IEnumerable<string> Keys => this.Select(x => x.Key);
        public new Func<DateTime, string[], DateTime> this[int index]
        {
            get
            {
                return base[index].Value;
            }

        }

        public Func<DateTime, string[], DateTime> this[string key]
        {

            set
            {
                base.Add(new KeyValuePair<string, Func<DateTime, string[], DateTime>>(key,value));
            }
        }
    }




    internal class RegexBuilder
    {
        private readonly Regex _regex;

        private RegexBuilder()
        {
            _regex = BuildRegex();
        }

        private static readonly Lazy<RegexBuilder> _Instance = new Lazy<RegexBuilder>(() => new RegexBuilder());
        public static RegexBuilder Instance => _Instance.Value;


        private static readonly PatternCollection DatePattern = new PatternCollection()
        {
            ["tomorrow"] = (date, _) => date.AddDays(1),
            [@"in (\d+) days"] = (date, i) => date.AddDays(i[0].ToPNumber())
        };

        private static readonly PatternCollection TimePattern = new PatternCollection()
        {
            [@"at (\d+) o clock"] = (date, _) => date.AddDays(2),
        };


        private string BuildRegex(PatternCollection pattern, char prefix)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (var key in pattern.Keys)
            {
                var key2 = key.Replace("(", $"(?<{prefix}b{i}>");
                sb.Append($"(?<{prefix}a{i}>{key2})|");
                i++;
            }

            return sb.ToString(0, sb.Length - 1);
        }



        private Regex BuildRegex()
        {
            var datePattern = BuildRegex(DatePattern, 'd');
            var timePattern = BuildRegex(TimePattern, 't');

            var regex = $@"({datePattern})({timePattern})?";




            return new Regex(regex, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
        }

        private int? GetKey(string[] groupNames, GroupCollection collection, string prefix)
        {
            foreach (var groupName in groupNames.Where(x => x.StartsWith(prefix)))
            {
                var group = collection[groupName];
                if (group.Success)
                {
                    return Convert.ToInt32(groupName.Substring(prefix.Length));
                }
            }

            return null;
        }

        private string[] GetParams(GroupCollection collection, string key)
        {
            var group = collection[key];
            if (group.Success)
            {
                return group.Captures.OfType<Capture>().Select(c => c.Value).ToArray();
            }
            return Array.Empty<string>();
        }


        public (DateTime? date, DateTime? time) Match(string value, DateTime seed)
        {
            var match = _regex.Match(value);

            if (!match.Success)
            {
                return (null, null);
            }

            var groupNames = _regex.GetGroupNames();

            int? dateMatchKey = GetKey(groupNames, match.Groups, "da");
            if (dateMatchKey.HasValue)
            {
                string[] dateMatchArgs = GetParams(match.Groups, "db" + dateMatchKey);
                DatePattern[dateMatchKey.Value](seed, dateMatchArgs);

            }




            return (null, null);

        }





    }
}
