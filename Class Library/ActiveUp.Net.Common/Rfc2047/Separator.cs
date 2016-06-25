namespace ActiveUp.Net.Common.Rfc2047
{
    internal class Separator : TokenBase
    {
        private readonly string _value;

        public Separator(string value)
        {
            _value = value;
        }

        public override string GetStringValue()
        {
            return _value;
        }
    }
}