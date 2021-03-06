using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace DialogFirm
{
    namespace Matcher
    {
        public class VerbatimMatcher : IntentMatcher
        {
            private HashSet<Regex> patterns;
            private string name;

            public VerbatimMatcher(string name, List<string> patterns)
            {
                this.name = name;
                IEnumerable<Regex> regexPatterns = patterns.Select(x => new Regex(x.ToLower()));
                this.patterns = new HashSet<Regex>(regexPatterns.ToList());
            }

            public override Intent Match(string input)
            {
                var matched = patterns.Where(pattern => pattern.IsMatch(input) == true);
                if (matched.Count() > 0)
                {
                    return new Intent(name, true, new Dictionary<string, string>());
                }
                return new Intent(name, false, new Dictionary<string, string>());
            }

            public override string Name()
            {
                return this.name;
            }
        }
    }
}
