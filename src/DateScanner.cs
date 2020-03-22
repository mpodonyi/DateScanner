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
        public DateScanner()
        {
            
        }

        public DateScannerResult Scan(string value)
        {
            var result = RegexBuilder.Regex.Match(value);

            if (!result.Success)
            {
                return new DateScannerResult { Found = false };
            }

            
            
           

            return new DateScannerResult { Found = true };
        }

    }
}
