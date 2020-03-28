using System;
using Xunit;

namespace DateScanner.Test
{
    public class DateScannerTestData : TheoryData<DateTime, string, DateScannerResult>
    {
        public DateScannerTestData()
        {
            DateTime seed = DateTime.Now;
            Add(seed, "we see us tomorrow my friend", new DateScannerResult { FoundDate = true, Date=seed.AddDays(1).Date });
        }
    }
}
