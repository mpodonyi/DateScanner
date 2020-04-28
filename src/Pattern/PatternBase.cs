namespace DateScanner.Pattern
{
    internal abstract class PatternBase
    {
        internal virtual PatternCollection DatePattern { get; } = new PatternCollection();

        internal virtual PatternCollection TimePattern { get; } = new PatternCollection();
    }
}
