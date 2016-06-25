namespace ActiveUp.Net.Common.Rfc2047
{
    internal class AsciiWord : TokenBase
    {
        private readonly string _rawValue;

        public AsciiWord(string rawValue)
        {
            _rawValue = rawValue;
        }

        public override string GetStringValue()
        {
            return _rawValue;
        }
    }
}