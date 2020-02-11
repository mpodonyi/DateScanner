using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace DateScanner.Test
{
    public class DateScannerTests
    {

        [Fact]
        public void NullCheck()
        {
            var scanner = new DateScanner();
            var result = scanner.Scan(null);

            result.Found.Should().BeFalse();
            
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void Scan_Theory(string value, DateScannerResult expected)
        {
            var scanner = new DateScanner();
            var result = scanner.Scan(value);

            result.Should().BeEquivalentTo(expected);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                        new object[] { "we see us tomorrow my friend", new DateScannerResult{Found=true} },
                        new object[] { "we see us tomorrow my friend", new DateScannerResult{Found=true} },

            };

        // public static IEnumerable<object[]> Data =
        //      new List<object[]>
        //      {
        //         new object[] { "we see us tomorrow my friend", new DateScannerResult{Found=true} },
        //         new object[] { "we see us tomorrow my friend", new DateScannerResult{Found=true} },

        //      };
    }
}
