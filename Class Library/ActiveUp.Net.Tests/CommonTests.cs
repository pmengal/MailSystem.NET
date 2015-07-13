using System;
using ActiveUp.Net.Mail;
using NUnit.Framework;

namespace ActiveUp.Net.Tests
{
    /// <summary>
    ///    ActiveUp.Net.Common library related tests.
    /// </summary>
    [TestFixture(Description = "ActiveUp.Net.Common library related tests")]
    public class CommonTests
    {
        [Test(Description = "Verify the correct parsing of multiple trace entries in a mail and let not affect the message.Date")]
        public void ParseMultipleMessageTraceMessageDate()
        {
            DateTime expected = new DateTime(2013, 01, 04, 03, 04, 04);
            Message message = Parser.ParseMessageFromFile("resource\\testmail.eml");
            Assert.AreEqual(expected, message.Date);
        }

        [Test(Description = "Verify the correct parsing of multiple trace entries in a mail and use the latest one as message.ReceivedDate")]
        public void ParseMultipleMessageTraceReceivedDate()
        {
            //relevant entry is "Mon, 7 Jan 2013 07:07:07 +0100" so Parser.ParseAsUniversalDateTime will give the following datetime as Result
            DateTime expected = new DateTime(2013, 01, 07, 06, 07, 07);
            Message message = Parser.ParseMessageFromFile("resource\\testmail.eml");
            Assert.AreEqual(expected, message.ReceivedDate);
        }
        
		/// <summary>
		/// TODO: Change test to fail process correct action in attachment without filename. Actualy the system process.
		/// </summary>
        [Test(Description = "Attachment without filename")]
        public void ParseAttachmentWitoutFilename()
        {
            Message message = Parser.ParseMessageFromFile("resource\\AttachmentWitoutFilename.eml");
            for (int i = 0; i < message.Attachments.Count; i++)
                Assert.IsNotNull(message.Attachments[i].Filename);
        }
    }
}