using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DateScanner
{
    public class PatternCollection : Dictionary<string, Func<DateTime, int, DateTime>>
    {

    }


    public class DateScannerResult
    {
        public bool Found { get; set; }
    }

    public class DateScanner
    {
        private static readonly Regex regex;
        private static readonly IReadOnlyDictionary<string, Func<DateTime, int, DateTime>> DatePattern = new PatternCollection()
        {
            ["tomorrow"] = (date, _) => date.AddDays(1),
            ["day after tomorrow"] = (date, _) => date.AddDays(2),
            [@"in (\d+) days"] = (date, i) => date.AddDays(i),

        };

        static DateScanner()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in DatePattern.Keys)
            {


                var cleanedupkey = Convert.ToBase64String(Encoding.ASCII.GetBytes(key)).Replace("=","");
                sb.Append($@"(?<{cleanedupkey}>{key})|");
            }

            regex = new Regex(sb.ToString(0, sb.Length - 1), RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }



        private readonly DateTime? Seed;

        public DateScanner(DateTime? seed = default) => Seed = seed;

        public DateScannerResult Scan(string value)
        {
            var match = regex.Match(value);

            return new DateScannerResult { Found = true };
        }
    }
}
