using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Common.Rfc2047
{
    public static class Rfc2047Codec
    {
        /// <summary>
        /// linear-white-space according to RFC822
        /// </summary>
        private static readonly char[] _linearWhiteSpaceChars = new[] { ' ', '\t', '\r', '\n' };
        private static readonly Regex _linearWhiteSpaceRegex = new Regex(string.Join("|", _linearWhiteSpaceChars), RegexOptions.Compiled);

        /// <summary>
        /// Detect encoded words as stated by RFC2047
        /// </summary>
        private static readonly Regex EncodedWordRegex = new Regex(@"(=\?)(?<charset>[^(\?)]*)(\?)(?<encoding>[BbQq])(\?)(?<message>[^ ?]*)(\?=)", RegexOptions.CultureInvariant);

        public static string Decode(string input)
        {
            var isSurroundedByComments = StripComments(ref input);
            var rawTokensWithSeparators = TokenizeOnLinearWhiteSpace(input);
            var tokenEnumerator = rawTokensWithSeparators.GetEnumerator();

            if (!tokenEnumerator.MoveNext())
                return string.Empty;

            var firstToken = WrapTokensAndLinkThem(tokenEnumerator);

            var output = OutputTokens(firstToken);
            if (isSurroundedByComments)
                return "(" + output + ")";
            return output;
        }

        private static TokenBase WrapTokensAndLinkThem(IEnumerator<string> tokenEnumerator)
        {
            var firstToken = WrapToken(tokenEnumerator.Current);
            var lastWord = (firstToken is Separator) ? null : firstToken;
            var lastSeparator = (firstToken is Separator) ? firstToken : null;

            while (tokenEnumerator.MoveNext())
            {
                var wrappedToken = WrapToken(tokenEnumerator.Current);
                if (wrappedToken is Separator)
                {
                    lastWord.NextSeparator = wrappedToken;
                    lastSeparator = wrappedToken;
                    continue;
                }

                if (lastWord != null)
                    lastWord.NextWord = wrappedToken;
                lastSeparator.NextWord = wrappedToken;
                lastWord = wrappedToken;
            }
            return firstToken;
        }

        private static string OutputTokens(TokenBase token)
        {
            if (token == null)
                return string.Empty;

            var nextSeparatorStringValue = token.NextSeparator != null ? token.NextSeparator.GetStringValue() : string.Empty;

            if (token is AsciiWord)
                return token.GetStringValue() + nextSeparatorStringValue + OutputTokens(token.NextWord);

            if (token is EncodedWord)
            {
                var encodedWordToken = token as EncodedWord;
                if (encodedWordToken.CanBeConcatenatedWith(encodedWordToken.NextWord))
                    return OutputTokens(EncodedWord.Concat(encodedWordToken, encodedWordToken.NextWord as EncodedWord));

                if (encodedWordToken.NextWord is EncodedWord)
                    return token.GetStringValue() + OutputTokens(token.NextWord);

                return token.GetStringValue() + nextSeparatorStringValue + OutputTokens(token.NextWord);   
            }
                
            if (token is Separator)
            {
                if (token.NextWord is EncodedWord)
                    return OutputTokens(token.NextWord);
                return token.GetStringValue() + OutputTokens(token.NextWord);
            }
                
            throw new InvalidOperationException("Unknown token type");
        }

        internal static IEnumerable<string> TokenizeOnLinearWhiteSpace(string input)
        {
            var previousDelimiterIndex = 0;
            for (var i = 0; i < input.Length; ++i)
            {
                if (!IsLinearWhiteSpace(input[i]))
                    continue;
                
                if (i != previousDelimiterIndex)
                    yield return input.Substring(previousDelimiterIndex, i - previousDelimiterIndex);
                var separatorChars = EatUpSeparator(input, ref i);
                yield return separatorChars;

                previousDelimiterIndex = i + 1;
            }
            if (input.Length != previousDelimiterIndex)
                yield return input.Substring(previousDelimiterIndex, input.Length - previousDelimiterIndex);
        }

        private static bool IsLinearWhiteSpace(char input)
        {
            return _linearWhiteSpaceChars.Contains(input);
        }

        private static string EatUpSeparator(string input, ref int i)
        {
            var separatorChars = new List<char> { input[i] };
            var j = i + 1;
            while (j < input.Length && IsLinearWhiteSpace(input[j]))
            {
                separatorChars.Add(input[j]);
                i = j++;
            }
            return new string(separatorChars.ToArray());
        }

        private static TokenBase WrapToken(string rawToken)
        {
            if (_linearWhiteSpaceRegex.IsMatch(rawToken))
                return new Separator(rawToken);
            
            var match = EncodedWordRegex.Match(rawToken);
            if (match.Success)
                return new EncodedWord(match.Groups["message"].Value, match.Groups["charset"].Value, match.Groups["encoding"].Value);

            return new AsciiWord(rawToken);
        }
        
        private static bool StripComments(ref string input)
        {
            var isSurroundedByComments = input.StartsWith("(") && input.EndsWith(")");
            input = input.Trim('(', ')');
            return isSurroundedByComments;

        }

        public static string Encode(string input, string charset)
        {
            return "=?" + charset + "?B?" + Convert.ToBase64String(Codec.GetEncoding(charset).GetBytes(input)) + "?=";
        }
    }
}