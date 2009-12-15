using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Tests
{
    [TestFixture]
    public class SmtpTests
    {
        [Test]
        public void MessageSslTest()
        {
            ActiveUp.Net.Mail.Message msg = new ActiveUp.Net.Mail.Message();
            msg.From.Name = "Pierre";
            msg.From.Email = "pmengal@activeup.com";
            msg.To.Add("pmengal@activeup.com");
            msg.Subject = "Test SMTP";
            //msg.BodyHtml.Text = "Body HTML";
            msg.BodyText.Text = "Body Text";

            //SmtpClient.SendSsl(msg, "smtp.gmail.com", "yourusername", "yourpassword", SaslMechanism.Login);
        }
    }
}
