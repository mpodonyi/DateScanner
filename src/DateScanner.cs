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
        private static readonly IReadOnlyDictionary<string, Func<DateTime, int, DateTime>> DatePattern = new PatternCollection()
        {
            ["tomorrow"] = (date, _) => date.AddDays(1),
            ["day after tomorrow"] = (date, _) => date.AddDays(2),
            [@"in (\d+) days"] = (date, i) => date.AddDays(i),

        };

        private static readonly Lazy<Regex> rx = new Lazy<Regex>(() =>
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in DatePattern.Keys)
            {
                sb.Append($@"(?<{key.GetHashCode()}>key)|");
            }

            return new Regex(sb.ToString(0, sb.Length - 1), RegexOptions.Compiled | RegexOptions.IgnoreCase);
        },);



        private readonly DateTime? Seed;

        public DateScanner(DateTime? seed = default) => Seed = seed;

        public DateScannerResult Scan(string value)
        {
            return new DateScannerResult { Found = true };
        }
    }
}
