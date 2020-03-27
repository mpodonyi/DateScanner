using System;

namespace DateScanner
{
    public class DateScannerResult
    {
        public bool Found => FoundDate || FoundTime;
        public bool FoundDate { get; set; }
        public bool FoundTime { get; set; }

        public DateTime Date { get; set; }

    }
}
