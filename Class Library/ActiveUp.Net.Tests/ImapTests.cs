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
        public void dump_emails_for_tests()
        {
            var headers = DumpHeadersFromImap();

            using (var writer = new StreamWriter("headers.txt"))
            {
                foreach (var header in headers)
                    writer.WriteLine(header);
            }
        }

        [Test, Ignore("Manual tests")]
        public void download_imap_test()
        {
            try
            {
                var _selectedMailBox = "INBOX";
                using (var _clientImap4 = new Imap4Client())
                {

                    _clientImap4.ConnectSsl(_imapServerAddress, _imapPort);
                    //_clientImap4.Connect(_mailServer.address, _mailServer.port);

                    _clientImap4.Login(_imapLogin, _imapPassword); // Efetua login e carrega as MailBox da conta.
                    //_clientImap4.LoginFast(_imapLogin, _imapPassword); // Efetua login e não carrega as MailBox da conta.

                    var _mailBox = _clientImap4.SelectMailbox(_selectedMailBox);

                    foreach (var messageId in _mailBox.Search("ALL").AsEnumerable())
                    {
                        var message = _mailBox.Fetch.Message(messageId);
                        var _imapMessage = Parser.ParseMessage(message);
                    }

                    _clientImap4.Disconnect();
                }

                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail("Don't work.", e);
            }
        }

        [Test, Ignore("Manual tests")]
        public void download_imap_async_test()
        {
            try
            {
                var callBack = new AsyncCallback(ImapAsyncCallBack);

                using (var _clientImap4 = new Imap4Client())
                {

                    var result = _clientImap4.BeginConnectSsl(_imapServerAddress, _imapPort, callBack);

                    while (!result.CompletedSynchronously)
                    {
                        Console.WriteLine("Waiting execution....");
                    }
                }

                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail("Dont work.", e);
            }
        }

        [Test, Ignore("Manual tests")]
        public void delete_inbox_undeleted_messages_not_gmail()
        {
            var _selectedMailBox = "INBOX";
            using (var _clientImap4 = new Imap4Client())
            {
                _clientImap4.ConnectSsl(_imapServerAddress, _imapPort);
                _clientImap4.LoginFast(_imapLogin, _imapPassword);

                var mails = _clientImap4.SelectMailbox(_selectedMailBox);
                var ids = mails.Search("UNDELETED");
                foreach (var id in ids)
                {
                    mails.DeleteMessage(id, expunge: true);
                }

                var mailsUndeleted = _clientImap4.SelectMailbox(_selectedMailBox);
                Assert.AreEqual(0, mailsUndeleted.Search("UNDELETED"));

                _clientImap4.Disconnect();
            }
        }

        [Test, Ignore("Manual tests")]
        public void delete_inbox_undeleted_messages_gmail()
        {
            var _selectedMailBox = "INBOX";
            using (var _clientImap4 = new Imap4Client())
            {
                _clientImap4.ConnectSsl(_imapServerAddress, _imapPort);
                _clientImap4.LoginFast(_imapLogin, _imapPassword);

                // To see the names os all MailBox and found  Trash
                _clientImap4.LoadMailboxes();
                var allMailBox = _clientImap4.AllMailboxes;

                var mails = _clientImap4.SelectMailbox(_selectedMailBox);
                var ids = mails.Search("UNDELETED");
                foreach (var id in ids)
                {
                    mails.DeleteMessage(id, expunge: false);
                    mails.MoveMessage(id, "[Gmail]/Lixeira");
                }

                var mailsUndeleted = _clientImap4.SelectMailbox(_selectedMailBox);
                Assert.AreEqual(0, mailsUndeleted.Search("UNDELETED"));

                _clientImap4.Disconnect();
            }
        }

        private void ImapAsyncCallBack(IAsyncResult result)
        {
            var resultStatus = result.AsyncState;
            using (var _clientImap4 = (Imap4Client)((System.Delegate)result.AsyncState).Target)
            {
                _clientImap4.Login(_imapLogin, _imapPassword); // Efetua login e carrega as MailBox da conta.
                //_clientImap4.LoginFast(_imapLogin, _imapPassword); // Efetua login e não carrega as MailBox da conta.
                var _selectedMailBox = "INBOX";
                var _mailBox = _clientImap4.SelectMailbox(_selectedMailBox);

                foreach (var messageId in _mailBox.Search("ALL").AsEnumerable())
                {
                    var message = _mailBox.Fetch.Message(messageId);
                    var _imapMessage = Parser.ParseMessage(message);
                }

                _clientImap4.Disconnect();
            }
        }

        private IEnumerable<string> DumpHeadersFromImap()
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