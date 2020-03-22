using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace DateScanner.Test
{
    public class DateScannerTests
    {

         [Fact]
         public void Scan_Theory()
         {
             string value = "we see us in 6 days at 5 o clock friend";

            var scanner = new DateScanner();
            var result = scanner.Scan(value);

            result.Should().NotBeNull();
         }

        


        // [Theory]
        // [MemberData(nameof(Data))]
        // public void Scan_Theory(string value, DateScannerResult expected)
        // {
        //     var scanner = new DateScanner();
        //     var result = scanner.Scan(value);

        //     result.Should().BeEquivalentTo(expected);
        // }

        // public static IEnumerable<object[]> Data => new List<object[]>
        // {
        //     new object[] { "we see us tomorrow my friend", new DateScannerResult{Found=true} },
        //     new object[] { "we see us tomorrow my friend", new DateScannerResult{Found=true} },

        // };
    }
}
