namespace ActiveUp.Net.Common.Rfc2047
{
    internal abstract class TokenBase
    {
        public TokenBase NextWord { get; set; }
        public TokenBase NextSeparator { get; set; }

        public abstract string GetStringValue();

        public override string ToString()
        {
            return GetStringValue();
        }
    }
}