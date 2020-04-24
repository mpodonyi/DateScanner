using System;
using System.Collections.Generic;

namespace DateScanner
{

    public class DateScanner
    {
        private readonly DateScannerOptions _options;
        private readonly RegexBuilder _regexBuilder;

        public DateScanner(DateScannerOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _regexBuilder=new RegexBuilder(_options.CompileRegex);
        }

        public DateScannerResult Scan(string value)
        {
            (DateTime? date, DateTime? time) = _regexBuilder.Match(value, _options.Seed);

            if (date != null && time != null)
            {
                return new DateScannerResult {Date = time.Value, FoundDate = true, FoundTime = true};
            }

            if (date != null)
            {
                return new DateScannerResult {Date = date.Value, FoundDate = true};
            }

            if (time != null)
            {
                return new DateScannerResult {Date = time.Value, FoundTime = true};
            }

            return new DateScannerResult();
        }
    }
}
