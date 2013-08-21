using System;

using ActiveUp.Net.Mail;
using NUnit.Framework;

namespace ActiveUp.Net.Tests
{
    /// <summary>
    /// ActiveUp.Net.Common library related tests.
    /// </summary>
    [TestFixture(Description = "ActiveUp.Net.Common library related tests")]
    public class CommonTests
    {
        [Test(Description = "Verify the correct parsing of multiple trace entries in a mail and use the latest one as message.ReceivedDate")]
        public void ParseMultipleMessageTrace()
        {
            Message message = new Message();
            message = Parser.ParseMessageFromFile("resource\\testmail.eml");
            // TODO: The code seems legit, is the test wrong?
            Assert.AreEqual(message.ReceivedDate, new DateTime(2013, 01, 06, 07, 07, 07));
            Assert.AreEqual(message.Date, new DateTime(2013, 01, 04, 03, 04, 04));
        }
    }
}
