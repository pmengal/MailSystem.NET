using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ActiveUp.Net.Mail;
using ActiveUp.Net.Security;
using NUnit.Framework;

namespace ActiveUp.Net.Tests
{
    public class ImapTests
    {
        private const string _imapLogin = "[login]";
        private const string _imapPassword = "[password]";
        private const int _imapPort = 993;
        private const string _imapServerAddress = "imap.gmail.com";


        [Test, Ignore("Manual tests")]
        public void GetGmailLabels()
        {
            var client = new Imap4Client();
            client.ConnectSsl(_imapServerAddress, _imapPort);
            client.Login(_imapLogin, _imapPassword);
            var inbox = client.SelectMailbox("ToImport");

            var message = inbox.Fetch.MessageObjectWithGMailExtensions(1);

            message.HeaderFields["X-GM-LABELS"].ShouldEqual(@"""\\Important""");
            message.HeaderFields["X-GM-MSGID"].ShouldEqual(@"13c48ecc5057ff3c");
            message.HeaderFields["X-MSG-UID"].ShouldEqual(@"151");
        }

        [Test, Ignore("Manual tests")]
        public void PeekAtGmailLabels()
        {
            var client = new Imap4Client();
            client.ConnectSsl(_imapServerAddress, _imapPort);
            client.Login(_imapLogin, _imapPassword);
            var inbox = client.SelectMailbox("ToImport");

            var message = inbox.Fetch.MessageObjectPeekWithGMailExtensions(1);

            message.HeaderFields["X-GM-LABELS"].ShouldEqual(@"""\\Important""");
            message.HeaderFields["X-GM-MSGID"].ShouldEqual(@"13c48ecc5057ff3c");
            message.HeaderFields["X-MSG-UID"].ShouldEqual(@"151");
        }


        [Test, Ignore("Manual tests")]
        public void GetsBase64MimeVersion()
        {
            var client = new Imap4Client();
            client.ConnectSsl(_imapServerAddress, _imapPort);
            client.Login(_imapLogin, _imapPassword);
            var inbox = client.SelectMailbox("ToImport");

            var message = inbox.Fetch.MessageObjectWithGMailExtensions(1);
            
            var tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempFolder);
            File.WriteAllText(Path.Combine(tempFolder, "normal_message.eml"),  message.ToMimeString());
            File.WriteAllText(Path.Combine(tempFolder, "base64_message.eml"),  message.ToMimeString(false, true));
            Process.Start(tempFolder);
        }

        [Test, Ignore("Manual tests")]
        public void dump_emails_for_tests()
        {
            var headers = DumpHeadersFromImap();

            using (var writer = new StreamWriter("headers.txt"))
            {
                foreach (var header in headers)
                    writer.WriteLine(header);
            }   
        }

        public IEnumerable<string> DumpHeadersFromImap()
        {
            var client = new Imap4Client();
            client.ConnectSsl(_imapServerAddress, _imapPort);
            client.Login(_imapLogin, _imapPassword);


            var allMail = client.SelectMailbox(@"[Gmail]/Tous les messages");

            var matchingOrdinals = allMail.Search("FROM \"interfax@interfax.ru\"").Take(500).ToList();
            var matchingRussianOrdinals = allMail.Search("FROM \"paubin@bloomberg.net\"").Take(500).ToList();

            foreach (var matchingOrdinal in matchingOrdinals.Concat(matchingRussianOrdinals))
            {
                var response = client.Command("fetch " + matchingOrdinal + " (rfc822.header UID)", new CommandOptions { IsPlusCmdAllowed = false });
                yield return response.Substring(response.IndexOf("}") + 3, response.LastIndexOf(")") - response.IndexOf("}") - 3);
            }
        }
    }
}