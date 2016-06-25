using System;
using System.Collections.Generic;
using System.Text;
using ActiveUp.Net.Mail;

namespace ActiveUp.MailSystem.DesktopClient
{
    /// <summary>
    /// This controller is used for send
    /// mail messages for SMTP protocol
    /// </summary>
    public class SmtpController
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SmtpController()
        {
        }

        /// <summary>
        /// Method used for send a message.
        /// </summary>
        /// <param name="accountInfo">The account information.</param>
        /// <param name="recipient">The recipients email.</param>
        /// <param name="subject">The message subject.</param>
        /// <param name="body">The message body.</param>
        /// <param name="attachments">The message attachements.</param>
        /// <returns>The ActiveUp.Net.Mail.Message sent or null if the message was not sent.</returns>
        public ActiveUp.Net.Mail.Message SendMessage(AccountSettings.AccountInfo accountInfo,
            string recipient, string subject, string body, string[] attachments)
        {
            // We create the message object.
            ActiveUp.Net.Mail.Message message = new ActiveUp.Net.Mail.Message();

            try
            {
                // We assign the sender email.
                message.From.Email = accountInfo.EmailAddress;

                string separator = ",";
                string[] recipients = recipient.Split(separator.ToCharArray());
                foreach (string r in recipients)
                {
                    // We assign the recipient email
                    message.To.Add(r);
                }

                // We assign the subject
                message.Subject = subject;

                // We assign the body text
                message.BodyText.Text = body;

                // We assign the attachments.
                foreach (string attachment in attachments)
                {
                    message.Attachments.Add(attachment, false);
                }

                int port = accountInfo.OutPort;
                bool ssl = accountInfo.OutgoingIsSSL;
                string serverName = accountInfo.OutgoingServerName;
                string password = accountInfo.Password;
                bool useOutPort = accountInfo.OutPortEnabled;
                SaslMechanism saslMechanism = SaslMechanism.Login; // TODO

                string user = accountInfo.LoginName;
                if (user == null || user.Equals(string.Empty))
                {
                    user = accountInfo.EmailAddress;
                }

                if (ssl)
                {
                    if (useOutPort)
                    {
                        SmtpClient.SendSsl(message, serverName, port, user, password, saslMechanism);
                    }
                    else
                    {
                        SmtpClient.SendSsl(message, serverName, user, password, saslMechanism);
                    }
                }
                else
                {
                    if (useOutPort)
                    {
                        SmtpClient.Send(message, serverName, port, user, password, saslMechanism);
                    }
                    else
                    {
                        SmtpClient.Send(message, serverName, user, password, saslMechanism);
                    }
                }
            }
            catch (Exception ex)
            {
                message = null;

                // TODO - Manipulate the exception properly.
                throw ex;
            }

            return message;
        }
    }
}
