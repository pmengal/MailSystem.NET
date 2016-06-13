using System;
using NUnit.Framework;
using ActiveUp.Net.Common.Utils;

namespace ActiveUp.Net.Tests.Common.Utils
{
    [TestFixture]
    public class ExtendStringTests
    {
        [Test]
        public void should_split_string_using_a_single_token()
        {
            var input = "i like pineapples";
            
            var parts = input.SplitAndKeepDelimiters(' ');

            parts.ShouldEqual(new[] { "i", " ", "like", " ", "pineapples" });

        }
        [Test]
        public void should_split_string_using_a_repeated_token()
        {
            var input = "i like  pineapples";
            
            var parts = input.SplitAndKeepDelimiters(' ');

            parts.ShouldEqual(new[] { "i", " ", "like", " ", "", " ", "pineapples" });

        }

        [Test]
        public void should_remove_empty_tokens_if_asked()
        {
            var input = "i like  pineapples";
            
            var parts = input.SplitAndKeepDelimiters(StringSplitOptions.RemoveEmptyEntries, ' ');

            parts.ShouldEqual(new[] { "i", " ", "like", " ", " ", "pineapples" });

        }

        [Test]
        public void should_split_string_using_a_multiple_tokens()
        {
            var input = "i like\tpineapples";

            var parts = input.SplitAndKeepDelimiters(' ', '\t');

            parts.ShouldEqual(new[] { "i", " ", "like", "\t", "pineapples" });
        }

        [Test]
        public void should_handle_delimiters_on_the_sides_of_the_string()
        {
            var input = " i like\tpineapples ";

            var parts = input.SplitAndKeepDelimiters(' ', '\t');

            parts.ShouldEqual(new[] { "", " ", "i", " ", "like", "\t", "pineapples", " ", "" });
        }

        [Test]
        public void should_remove_empty_token_at_the_end_if_asked()
        {
            var input = " i like\tpineapples ";

            var parts = input.SplitAndKeepDelimiters(StringSplitOptions.RemoveEmptyEntries, ' ', '\t');

            parts.ShouldEqual(new[] { " ", "i", " ", "like", "\t", "pineapples", " " });
        }
    }
}