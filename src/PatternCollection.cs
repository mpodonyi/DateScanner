using System;
using System.Collections.Generic;
using System.Linq;

namespace DateScanner
{
    public class PatternCollection : List<KeyValuePair<string, Func<DateTime, string[], DateTime>>>
    {
        public IEnumerable<string> Keys => this.Select(x => x.Key);
        public new Func<DateTime, string[], DateTime> this[int index]
        {
            get
            {
                return base[index].Value;
            }

        }

        public Func<DateTime, string[], DateTime> this[string key]
        {

            set
            {
                base.Add(new KeyValuePair<string, Func<DateTime, string[], DateTime>>(key, value));
            }
        }
    }
}
