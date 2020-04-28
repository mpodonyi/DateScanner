using System;

namespace DateScanner
{
    public class DateScannerOptions
    {
        public DateTime? Seed { get; set; }

        public string Language { get; set; }

        public bool CompileRegex { get; set; } = true;
    }
}
