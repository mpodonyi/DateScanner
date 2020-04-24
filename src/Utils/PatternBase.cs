using System;
using System.Collections.Generic;
using System.Text;

namespace DateScanner.Utils
{
    internal abstract class PatternBase
    {
        internal virtual PatternCollection DatePattern { get; } =new PatternCollection();

        internal virtual PatternCollection TimePattern { get; } = new PatternCollection();

        public void Deconstruct(out PatternCollection datePattern, out PatternCollection timePattern)
        {
            datePattern = DatePattern;
            timePattern = TimePattern;
        }
    }
}
