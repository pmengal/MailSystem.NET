using System;
using System.Collections.Generic;
using System.Linq;

namespace ActiveUp.Net.Common.Utils
{
    public static class ExtendString
    {
        public static IEnumerable<string> SplitAndKeepDelimiters(this string input, params char[] delimiters)
        {
            return SplitAndKeepDelimiters(input, StringSplitOptions.None, delimiters);
        }

        public static IEnumerable<string> SplitAndKeepDelimiters(this string input, StringSplitOptions options, params char[] delimiters)
        {
            var previousDelimiterIndex = 0;
            for (var i = 0; i < input.Length; ++i)
            {
                if (delimiters.Contains(input[i]))
                {
                    var token = input.Substring(previousDelimiterIndex, i - previousDelimiterIndex);
                    if (options != StringSplitOptions.RemoveEmptyEntries || !string.IsNullOrEmpty(token))
                        yield return token;
                    yield return new string(input[i], 1);
                    previousDelimiterIndex = i + 1;
                }
            }

            var lastToken = input.Substring(previousDelimiterIndex, input.Length - previousDelimiterIndex);
            if (options != StringSplitOptions.RemoveEmptyEntries || !string.IsNullOrEmpty(lastToken))
                yield return lastToken;
        }
    }
}