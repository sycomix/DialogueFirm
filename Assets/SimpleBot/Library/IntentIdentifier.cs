﻿using System.Collections.Generic;
using System.Linq;
using System;
using SimpleBot.Matcher;

namespace SimpleBot
{
    public class IntentIdentifier
    {
        public const string NO_MATCH_EXIST = "NO_MATCH_EXIST";

        private List<IntentMatcher> matchers;

        public IntentIdentifier(Configuration config)
        {
            this.matchers = config.GetIntentConfigs().Select(c => this.generateMatcher(c, config.GetTypeConfigs())).ToList().ConvertAll(instance => (IntentMatcher)instance);
        }

        public IntentMatcher generateMatcher(IntentConfig intentConfig, TypeConfig typeConfig) {
            if (intentConfig.MatcherType() == "verbatim")
            {
                return new VerbatimMatcher(intentConfig.Name, intentConfig.Patterns());
            } else {
                throw new ArgumentException("No matcher type as " + intentConfig.MatcherType());
            }
        }

        public string Identify(string input)
        {
            var matches = this.matchers.Where(matcher => matcher.Match(input) == true);
            if (matches.Count() > 0)
            {
                return matches.First().Name();
            }
            return NO_MATCH_EXIST;
        }
    }
}