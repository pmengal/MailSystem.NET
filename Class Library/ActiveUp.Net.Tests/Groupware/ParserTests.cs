using ActiveUp.Net.Groupware.vCalendar;
using NUnit.Framework;

namespace ActiveUp.Net.Tests.Groupware
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void should_parse_simple_date()
        {
            var vcal = Parser.Parse("TODO:");

            Assert.AreEqual(0, vcal.Todos.Count);
            Assert.AreEqual(0, vcal.Events.Count);
        }

    }
}
