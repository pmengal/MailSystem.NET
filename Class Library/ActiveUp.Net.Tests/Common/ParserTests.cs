using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActiveUp.Net.Mail;
using NUnit.Framework;

namespace ActiveUp.Net.Tests.Common
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void should_parse_simple_date()
        {
            var utcDate = Parser.ParseAsUniversalDateTime("Mon, 24 Jun 2013 10:37:36 +0100");

            utcDate.ShouldEqual(new DateTime(2013, 06, 24, 09, 37, 36));
        }

        [Test]
        public void should_clean_input()
        {
            var utcDate = Parser.ParseAsUniversalDateTime("(noise\\input)Mon, 24 Jun 2013 10:37:36 +0100");

            utcDate.ShouldEqual(new DateTime(2013, 06, 24, 09, 37, 36));
        }

        [Test]
        public void should_return_resulting_date_in_utc()
        {
            var utcDate = Parser.ParseAsUniversalDateTime("Mon, 24 Jun 2013 10:37:36 +0100");

            utcDate.Kind.ShouldEqual(DateTimeKind.Utc);
        }
        
        [Test]
        public void should_parse_date_with_no_day_of_week()
        {
            var utcDate = Parser.ParseAsUniversalDateTime("24 Jun 2013 10:37:36 +0100");

            utcDate.ShouldEqual(new DateTime(2013, 06, 24, 09, 37, 36));
        }
        
        [Test]
        public void should_parse_date_with_two_digits_year()
        {
            var utcDate = Parser.ParseAsUniversalDateTime("Mon, 24 Jun 13 10:37:36 +0100");

            utcDate.ShouldEqual(new DateTime(2013, 06, 24, 09, 37, 36));
        }
        
        [Test]
        public void should_parse_date_with_no_seconds()
        {
            var utcDate = Parser.ParseAsUniversalDateTime("Mon, 24 Jun 2013 10:37 +0100");

            utcDate.ShouldEqual(new DateTime(2013, 06, 24, 09, 37, 00));
        }
    }
}
