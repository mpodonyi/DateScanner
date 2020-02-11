using System;
using System.Collections.Generic;

namespace DateScanner
{
    public class DateScannerResult
    { 
        public bool Found { get; set; }
    }
    
    public class DateScanner
    {
        private static IReadOnlyDictionary<string,string> DatePattern=new Dictionary<string,string>{
            ["mike"] = "was",
            ["mike2"] = "was"
        };

        public DateScanner()
        { }

        public DateScannerResult Scan(string value)
        {
            return new DateScannerResult{Found=true};
        }
    }
}
