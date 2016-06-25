using ActiveUp.Net.Common.Rfc2047;
using NUnit.Framework;

namespace ActiveUp.Net.Tests.Common.RFC2047
{
    public partial class Rfc2047DecoderTests
    {
        [Test]
        public void should_split_string_using_a_single_token()
        {
            var input = "i like pineapples";
            
            var parts = Rfc2047Codec.TokenizeOnLinearWhiteSpace(input);

            parts.ShouldEqual(new[] { "i", " ", "like", " ", "pineapples" });

        }
        [Test]
        public void should_split_string_using_a_repeated_token()
        {
            var input = "i like  pineapples";

            var parts = Rfc2047Codec.TokenizeOnLinearWhiteSpace(input);

            parts.ShouldEqual(new[] { "i", " ", "like", "  ", "pineapples" });

        }

        [Test]
        public void should_split_string_using_a_multiple_tokens()
        {
            var input = "i like\tpineapples";

            var parts = Rfc2047Codec.TokenizeOnLinearWhiteSpace(input);

            parts.ShouldEqual(new[] { "i", " ", "like", "\t", "pineapples" });
        }

        [Test]
        public void should_handle_delimiters_on_the_sides_of_the_string()
        {
            var input = " i like\tpineapples ";

            var parts = Rfc2047Codec.TokenizeOnLinearWhiteSpace(input);

            parts.ShouldEqual(new[] { " ", "i", " ", "like", "\t", "pineapples", " " });
        }

        [Test]
        public void should_group_different_separators()
        {
            var input = " i like\t \r\npineapples";

            var parts = Rfc2047Codec.TokenizeOnLinearWhiteSpace(input);

            parts.ShouldEqual(new[] { " ", "i", " ", "like", "\t \r\n", "pineapples" });
        }
        
        [Test]
        public void should_group_different_separators_at_the_end_of_the_input()
        {
            var input = " i like pineapples\t \r\n";

            var parts = Rfc2047Codec.TokenizeOnLinearWhiteSpace(input);

            parts.ShouldEqual(new[] { " ", "i", " ", "like", " ", "pineapples", "\t \r\n" });
        }
    }
}