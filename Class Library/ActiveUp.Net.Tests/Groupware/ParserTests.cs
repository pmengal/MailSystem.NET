using ActiveUp.Net.Groupware.vCalendar;
using NUnit.Framework;
using System;

namespace ActiveUp.Net.Tests.Groupware
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void should_parse_simple_file()
        {
            var vcal = Parser.Parse("TODO:");

            Assert.AreEqual(0, vcal.Todos.Count);
            Assert.AreEqual(0, vcal.Events.Count);
        }

        [Test]
        public void should_parse_simple_date()
        {
            var date = Parser.ParseDate("2016-11-21");
            Assert.AreEqual(new DateTime(2016, 11, 21, 00, 00, 00), date);
            var date2 = Parser.ParseDate("2016-11-21 15:30:25");
            Assert.AreEqual(new DateTime(2016,11,21,15,30,25), date2);
        }

    }
}
