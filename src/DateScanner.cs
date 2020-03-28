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

            var result = RegexBuilder.Instance.Match(value,_options.Seed ?? DateTime.Now);


            if (result is (null,null))
            {
                return new DateScannerResult { FoundDate = false };
            }





            return new DateScannerResult();
        }

    }
}
