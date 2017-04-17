using System;
using System.Linq;

namespace Lx.Utilities.Services.Infrastructure
{
    public static class StringExtensions
    {
        public static bool MatchesAnyInExpectedList(this string actual, string expectedList,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase,
            bool defaultReturnIfEitherNullOrWhiteSpace = true, string[] splitters = null)
        {
            if (string.IsNullOrWhiteSpace(actual) || string.IsNullOrWhiteSpace(expectedList))
                return defaultReturnIfEitherNullOrWhiteSpace;
            // No need to compare as the source user object doesn't have the value for this field
            // or the expected list doesn't supply allowed values
            var macthing = expectedList
                .Split(splitters ?? new[] {" ", ",", ";", "|"}, StringSplitOptions.RemoveEmptyEntries)
                .Any(x => x.Equals(actual, comparison));

            return macthing;
        }

        public static bool MatchesExpected(this string actual, string expected,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase,
            bool defaultReturnIfEitherNullOrWhiteSpace = true)
        {
            if (string.IsNullOrWhiteSpace(actual) || string.IsNullOrWhiteSpace(expected))
                return defaultReturnIfEitherNullOrWhiteSpace;

            return actual.Equals(expected, comparison);
        }
    }
}