using System;
using System.Collections.Generic;
using System.Text;

namespace ActiveUp.MailSystem.DesktopClient
{

    /// <summary>
    /// This class represents the settings for mail message find.
    /// </summary>
    public class FindSettings
    {

        #region Attributes

        /// <summary>
        /// The mail message 'from' field.
        /// </summary>
        private string _from;

        /// <summary>
        /// The mail message 'to' field.
        /// </summary>
        private string _to;

        /// <summary>
        /// The mail message subject.
        /// </summary>
        private string _subject;

        /// <summary>
        /// The mail message text.
        /// </summary>
        private string _message;

        /// <summary>
        /// The mail message date from.
        /// </summary>
        private string _dateFrom;

        /// <summary>
        /// The mail message date to.
        /// </summary>
        private string _dateTo;

        /// <summary>
        /// The has attachments bool value.
        /// </summary>
        private bool _attachemnt;

        #endregion

        #region Contructor

        /// <summary>
        /// Default Constructor for Find Settings class.
        /// </summary>
        public FindSettings()
        {
            this._from = string.Empty;
            this._to = string.Empty;
            this._message = string.Empty;
            this._subject = string.Empty;
            this._dateFrom = string.Empty;
            this._dateTo = string.Empty;
        }

        /// <summary>
        /// Constructor for Find Settings class.
        /// </summary>
        /// <param name="from">The mail message 'from' field.</param>
        /// <param name="to">The mail message 'to' field.</param>
        /// <param name="subject">The mail message subject.</param>
        /// <param name="message">The mail message text.</param>
        /// <param name="dateFrom">The mail message date from.</param>
        /// <param name="dateTo">The mail message date to.</param>
        /// <param name="attachemnt">The has attachments bool value.</param>
        public FindSettings(string from, string to, string subject, 
            string message, string dateFrom, string dateTo, bool attachemnt)
        {
            this._from = from;
            this._to = to;
            this._message = message; 
            this._subject = subject;
            this._dateFrom = dateFrom;
            this._dateTo = dateTo;
            this._attachemnt = attachemnt;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The mail message 'from' field.
        /// </summary>
        public string From
        {
            get { return _from; }
            set { _from = value; }
        }

        /// <summary>
        /// The mail message 'to' field.
        /// </summary>
        public string To
        {
            get { return _to; }
            set { _to = value; }
        }

        /// <summary>
        /// The mail message subject.
        /// </summary>
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        /// <summary>
        /// The mail message text.
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// The mail message date from.
        /// </summary>
        public string DateFrom
        {
            get { return _dateFrom; }
            set { _dateFrom = value; }
        }

        /// <summary>
        /// The mail message date to.
        /// </summary>
        public string DateTo
        {
            get { return _dateTo; }
            set { _dateTo = value; }
        }

        /// <summary>
        /// The has attachments bool value.
        /// </summary>
        public bool Attachemnt
        {
            get { return _attachemnt; }
            set { _attachemnt = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method for verify if the message match the filter criteria.
        /// </summary>
        /// <param name="mailMessage">The mail message object.</param>
        /// <returns>The bool value indicating if the message is filtered or not.</returns>
        public bool IsMessageFiltered(MailMessage mailMessage)
        {
            bool ret = false;

            // verify the from field.
            if (!ret && !this.From.Equals(string.Empty))
            {
                if (!mailMessage.From.Contains(this.From))
                {
                    ret = true;
                }
            }

            // verify the to field.
            if (!ret && !this.To.Equals(string.Empty))
            {
                if (!mailMessage.To.Contains(this.To))
                {
                    ret = true;
                }
            }

            // verify the subject field.
            if (!ret && !this.Subject.Equals(string.Empty))
            {
                if (!mailMessage.Subject.Contains(this.Subject))
                {
                    ret = true;
                }
            }

            // verify the message body field.
            if (!ret && !this.Message.Equals(string.Empty))
            {
                Facade facade = Facade.GetInstance();
                string mailbox = MainForm.GetInstance().GetSelectedMailbox();
                string body = facade.GetMessageBodyString(mailMessage, mailbox);
                if (!body.Contains(this.Message))
                {
                    ret = true;
                }
            }

            // verify the message sent date.
            if (!ret && !this.DateFrom.Equals(string.Empty) || !this.DateTo.Equals(string.Empty))
            {
                DateTime dtFrom = DateTime.MinValue;
                DateTime dtTo = DateTime.MaxValue;

                DateTime.TryParse(this.DateFrom, out dtFrom);
                DateTime.TryParse(this.DateTo, out dtTo);

                if (mailMessage.SentDate < dtFrom || mailMessage.SentDate > dtTo)
                {
                    ret = true;
                }
            }

            return ret;
        }

        #endregion

    }
}