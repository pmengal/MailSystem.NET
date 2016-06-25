using System;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Common.Rfc2047
{
    internal class EncodedWord : TokenBase
    {
        private readonly string _rawValue;
        private readonly string _charset;
        private readonly string _encoding;

        private bool IsBase64 { get { return _encoding.Equals("B", StringComparison.OrdinalIgnoreCase); } }

        public EncodedWord(string rawValue, string charset, string encoding)
        {
            _rawValue = rawValue;
            _charset = charset;
            _encoding = encoding;
        }

        public override string GetStringValue()
        {
            if (IsBase64)
                return Codec.GetEncoding(_charset).GetString(Convert.FromBase64String(_rawValue));
            return Codec.FromQuotedPrintable(_rawValue.Replace("_", " "), _charset);
        }

        public bool CanBeConcatenatedWith(TokenBase other)
        {
            if (other == null)
                return false;
            var otherEncodedWord = other as EncodedWord;
            if (otherEncodedWord == null || otherEncodedWord._charset != _charset || otherEncodedWord._encoding != _encoding)
                return false;
            if (IsBase64 && _rawValue.EndsWith("="))
                return false;
            return true;
        }

        public static EncodedWord Concat(EncodedWord first, EncodedWord second)
        {
            if (first._charset != second._charset)
                throw new InvalidOperationException("Both words must have the same charset");
            if (first._encoding != second._encoding)
                throw new InvalidOperationException("Both words must have the same encoding");

            return new EncodedWord(first._rawValue.TrimEnd('=') + second._rawValue, first._charset, first._encoding) { NextSeparator = second.NextSeparator, NextWord = second.NextWord };
        }
    }
}