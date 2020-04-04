using System;

namespace DateScanner
{
    public class DateScannerOptions
    {
        private DateTime? _Seed;
        public DateTime Seed
        {
            get => _Seed ?? DateTime.Now;
            set => _Seed = value;
        }
    }
}
