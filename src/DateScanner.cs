using System;
using System.Collections.Generic;

namespace DateScanner
{
    public class DateScannerOptions
    {
        public DateTime? Seed { get; set; }
    }

    public class DateScanner
    {
        private readonly DateScannerOptions _options;
        public DateScanner(DateScannerOptions options)
        {
            _options = options;
        }

        public DateScannerResult Scan(string value)
        {

            var (date, time) = RegexBuilder.Instance.Match(value, _options.Seed ?? DateTime.Now);

            if (date != null && time != null)
            {
                return new DateScannerResult { Date = time.Value, FoundDate = true, FoundTime = true };
            }
            else if (date != null)
            {
                return new DateScannerResult { Date = date.Value, FoundDate = true };
            }
            else if (time != null)
            {
                return new DateScannerResult { Date = time.Value, FoundTime = true };
            }

            return new DateScannerResult();
        }
    }
}
