using System;
using Xunit;

namespace DateScanner.Test
{
    public class DateScannerTestData : TheoryData<DateTime, string, DateScannerResult>
    {
        public DateScannerTestData()
        {
            DateTime seed = DateTime.Now;
             Add(seed, "we see us tomorrow my friend", new DateScannerResult { FoundDate = true, Date = seed.AddDays(1) });
             Add(seed, "we see us today my friend", new DateScannerResult { FoundDate = true, Date = seed });
             Add(seed, "we see us in 5 days friend", new DateScannerResult { FoundDate = true, Date = seed.AddDays(5) });

             Add(seed, "we see us at 5 my friend", new DateScannerResult { FoundTime = true, Date = seed.Date.AddHours(17) });
             Add(seed, "we see us at 15 my friend", new DateScannerResult { FoundTime = true, Date = seed.Date.AddHours(15) });

            Add(seed, "we see us tomorrow at 15 my friend", new DateScannerResult { FoundTime = true, FoundDate=true, Date = seed.AddDays(1).Date.AddHours(15) });

        }
    }
}
