using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace DateScanner.Test
{

    public class DateScannerTests
    {
        [Theory]
        [ClassData(typeof(DateScannerTestData))]
        public void Scan_Test(DateTime seed, string value, DateScannerResult expected)
        {
            var scanner = new DateScanner(new DateScannerOptions { Seed = seed });
            var result = scanner.Scan(value);

            result.Should().BeEquivalentTo(expected);
        }
    }
}
