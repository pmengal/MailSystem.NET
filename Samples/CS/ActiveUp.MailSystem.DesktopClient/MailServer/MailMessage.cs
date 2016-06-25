#region Using directives

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using ActiveUp.Net.Mail;

#endregion

namespace ActiveUp.MailSystem.DesktopClient
{
    /// <summary>
    /// This class represents a Mail Message object.
    /// It's used for persistence of messages and for all message management.
    /// Changes in this class should be reflected in Inbox.xsd XML schema.
    /// </summary>
	public class MailMessage : INotifyPropertyChanged
	{
        private string      _id;
        private string      _from;
		private string		_cc;
		private string		_to;
		private string		_subject;
		private bool		_read;
		private string	    _sentDate;
		private string		_path;
        private int         _baseIndex;
        private string      _parentFolder;

		private event PropertyChangedEventHandler _changed;

		/// <summary>
		/// Constructor.
		/// </summary>
        public MailMessage()
		{
            this._id = string.Empty;
            this._from = string.Empty;
            this._cc = string.Empty;
            this._to = string.Empty;
            this._subject = string.Empty;
            this._path = string.Empty;
            this._sentDate = DateTime.MinValue.ToString();
            this._baseIndex = 1;
            this._parentFolder = string.Empty;
		}

		#region Properties

        /// <summary>
        /// Represents the message identifier.
        /// </summary>
        public string Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        /// <summary>
        /// Represents the from email.
        /// </summary>
        public string From
		{
			get
			{
				return _from;
			}

			set
			{
				_from = value;
				OnPropertyChanged("From");
			}
		}

        /// <summary>
        /// Represents the recipient email.
        /// </summary>
		public string To
		{
			get
			{
				return _to;
			}

			set
			{
				_to = value;
				OnPropertyChanged("To");
			}
		}

        /// <summary>
        /// Represents the recipient copy email.
        /// </summary>
		public string Cc
		{
			get
			{
				return _cc;
			}

			set
			{
				_cc = value;
				OnPropertyChanged("Cc");
			}
		}

        /// <summary>
        /// Property for message subject.
        /// </summary>
		public string Subject
		{
			get
			{
				return _subject;
			}

			set
			{
				_subject = value;
				OnPropertyChanged("Subject");
			}
		}

        /// <summary>
        /// Property for indicate if a message was read or not.
        /// </summary>
		public bool Read
		{
			get
			{
				return _read;
			}

			set
			{
				_read = value;
				OnPropertyChanged("Read");
			}
		}

        /// <summary>
        /// Property for the sent date.
        /// </summary>
        public DateTime SentDate
        {
            get
            {
                DateTime date = DateTime.MinValue;
                DateTime.TryParse(_sentDate, out date);
                return date;
            }

            set
            {
                _sentDate = value.ToString();
                OnPropertyChanged("SentDate");
            }
        }

        /// <summary>
        /// Property used for the message path.
        /// </summary>
        public string Path
        {
            get
            {
                return _path;
            }

            set
            {
                _path = value;
                OnPropertyChanged("Path");
            }
        }

        /// <summary>
        /// Property used for the message Base Index.
        /// </summary>
        public int BaseIndex
        {
            get
            {
                return _baseIndex;
            }

            set
            {
                _baseIndex = value;
                OnPropertyChanged("BaseIndex");
            }
        }

        /// <summary>
        /// Property used for the message parent folder.
        /// </summary>
        public string ParentFolder
        {
            get
            {
                return _parentFolder;
            }

            set
            {
                _parentFolder = value;
                OnPropertyChanged("ParentFolder");
            }
        }

        #endregion

		#region INotifyPropertyChanged Members
		
        /// <summary>
        /// OnPropertyChanged.
        /// </summary>
        /// <param name="property">The string property.</param>
        protected void OnPropertyChanged(string property)
		{
			if (null != _changed)
			{
				_changed(this, new PropertyChangedEventArgs(property));
			}
		}

        /// <summary>
        /// PropertyChangedEventHandler.
        /// </summary>
		public event PropertyChangedEventHandler PropertyChanged
		{
			add { _changed += new PropertyChangedEventHandler(value); }
			remove { _changed -= new PropertyChangedEventHandler(value); }
		}

		#endregion

        #region Methods


        /// <summary>
        /// Method for override the Equals implementation.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>The result for comparison.</returns>
        public override bool Equals(object obj)
        {
            bool ret = false;
            if (obj is MailMessage)
            {
                MailMessage mailMessage = (MailMessage)obj;
                if (this.Id.Equals(mailMessage.Id))
                {
                    ret = true;
                }
            }
            else
            {
                ret = base.Equals(obj);
            }
            return ret;
        }

        /// <summary>
        /// Override implementation for GetHashCode.
        /// Use the base implementation.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Fill the mail message object based on Header object.
        /// </summary>
        /// <param name="message">The ActiveUp.Net.Mail.Header.</param>
        public void FillMessage(Header header)
        {
            this.Id = header.MessageId;
            this.From = header.From.ToString();
            this.Subject = header.Subject;
            this.SentDate = header.Date;

            int index = 1;
            foreach (ActiveUp.Net.Mail.Address address in header.To)
            {
                this.To = string.Concat(this.To, address.ToString());
                if (index < header.To.Count)
                {
                    this.To = string.Concat(this.To, ",");
                }
                index++;
            }

            // verify if the messages directory exist, if not create it.
            string directory = "Messages";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // create the file with the message.
            //string fileName = string.Concat(this.Id, ".eml");
            //string path = System.IO.Path.Combine(directory, fileName);
            //if (!File.Exists(path))
            //{
            //    FileStream fileStream = File.Create(path);
            //    StreamWriter sw = new StreamWriter(fileStream);
            //    sw.Write(message.BodyHtml.Text);
            //    sw.Close();
            //}

            //this.Path = header.Path;
        }
        
        ///// <summary>
        ///// Fill the mail message object based on ActiveUp.Net.Mail.Message object.
        ///// </summary>
        ///// <param name="message">The ActiveUp.Net.Mail.Message.</param>
        //public void FillMessage(ActiveUp.Net.Mail.Message message)
        //{
        //    this.Id = message.MessageId;
        //    this.From = message.From.ToString();
        //    this.Subject = message.Subject;
        //    this.SentDate = message.Date;

        //    int index = 1;
        //    foreach (ActiveUp.Net.Mail.Address address in message.To)
        //    {
        //        this.To = string.Concat(this.To, address.ToString());
        //        if (index < message.To.Count)
        //        {
        //            this.To = string.Concat(this.To, ",");
        //        }
        //        index++;
        //    }

        //    // verify if the messages directory exist, if not create it.
        //    string directory = "Messages";
        //    if (!Directory.Exists(directory))
        //    {
        //        Directory.CreateDirectory(directory);
        //    }

        //    // create the file with the message.
        //    string fileName = string.Concat(this.Id, ".eml");
        //    string path = System.IO.Path.Combine(directory, fileName);
        //    if (!File.Exists(path))
        //    {
        //        FileStream fileStream = File.Create(path);
        //        StreamWriter sw = new StreamWriter(fileStream);
        //        sw.Write(message.BodyHtml.Text);
        //        sw.Close();
        //    }
            
        //    this.Path = path;
        //}

        #endregion
	}
}
