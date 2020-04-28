using DateScanner.Utils;

namespace DateScanner.Pattern.Localizations
{
    internal class GermanPattern : PatternBase
    {
        internal override PatternCollection DatePattern { get; } = new PatternCollection()
        {
            ["tomorrow"] = (date, _) => date.AddDays(1),
            ["today"] = (date, _) => date,
            [@"in (\d+) days"] = (date, i) => date.AddDays(i[0].ToPNumber())
        };

        internal override PatternCollection TimePattern { get; } = new PatternCollection()
        {
            [@"at (\d+)"] = (date, i) => date.AddHours(i[0].ToPNumber() % 12 + 12)
        };
    }
}