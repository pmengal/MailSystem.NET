using System;
using System.Data;
using System.Configuration;
using System.Web;
using ActiveUp.Net.Mail;
using ActiveUp.Net.WhoIs;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Text;

/// <summary>
/// This controller is used for retrieve, delete and manipulate
/// mail messages for POP3 protocol
/// </summary>
namespace PrintMail
{
    public class Controller
    {
        /// <summary>
        /// Attribute for make connection to POP.
        /// </summary>
        private Pop3Client _pop3Client;
        Thread retrievesThread = null;

        private string pop3;
        private string login;
        private string password;
        public string printerName;

        private bool threadStop; 

        /// <summary>
        /// Attributes for store messages and headers respectively.
        /// </summary>

        private List<ActiveUp.Net.Mail.Message> ListMessageInbox;

        public List<ActiveUp.Net.Mail.Message> ListMessageInbox1
        {
            get { return ListMessageInbox; }
            set { ListMessageInbox = value; }
        }

        /// <summary>
        /// Represents the POP client
        /// </summary>
        public Pop3Client Pop3Client
        {
            get { return _pop3Client; }
            set { _pop3Client = value; }
        }

        public Controller()
        {
            this.ListMessageInbox = new List<ActiveUp.Net.Mail.Message>();
            this.pop3 = String.Empty;
            this.login = String.Empty;
            this.password = String.Empty;
            this.threadStop = false;
        }

        /// <summary>
        /// Connect the pop client.
        /// </summary>
        /// <param name="accountInfo">The information account</param>
        public void Connect(string pop, string login, string _password, string printer)
        {       
            try {               
                    this._pop3Client = new Pop3Client();
                    this.pop3 = pop;
                    this.login = login;
                    this.password = _password;

                    this._pop3Client.ConnectSsl(this.pop3, this.login, this.password);
               
            }            
            catch (Exception)
            {
                if (this._pop3Client.IsConnected) this._pop3Client = new Pop3Client();
                MessageBox.Show("Could not connect to Pop3 Server" + "\n" + "Verifies The settings", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }        

            if(printer == null || printer == String.Empty) 
            {
                MessageBox.Show("No printer has been selected", "No printer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.printerName = printer;
        }

        /// <summary>
        ///  Method for retrieve the mail messages for POP protocol
        /// </summary>
        /// <returns>A list of messages</returns>
        public void RetrieveMessages()
        {
            try
            {
                while (true)
                {
                    this.Reconnected();
                    this.ListMessageInbox.Clear();

                    ActiveUp.Net.Mail.Message msg = null;
                    this.ListMessageInbox.Clear();
                    int messageCount = this._pop3Client.MessageCount;

                    for (int i = 1; i <= messageCount; i = i + 1)
                    {
                        Header h = this._pop3Client.RetrieveHeaderObject(i);
                        msg = this._pop3Client.RetrieveMessageObject(i);
                        this.ListMessageInbox.Add(msg);
                        this._pop3Client.DeleteMessage(i);
                    }

                    this.printMails();

                    if (this.threadStop)
                    {
                        Thread.CurrentThread.Abort();
                    }

                    Thread.Sleep(60000);
                }                
            }           
            catch (ThreadAbortException)
            {
            }
            catch(Exception) 
            {
                MessageBox.Show("Could not retrieves the mail from the server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Reconnected()
        {
            if (this._pop3Client.IsConnected) this._pop3Client.Disconnect();
            this.Connect(this.pop3, this.login, this.password, this.printerName);
        }

        public void printMails()
        {
            try
            {
                foreach (ActiveUp.Net.Mail.Message msg in this.ListMessageInbox)
                {
                    string mailToPrint = this.FormatMailToPrint(msg);
                    RawPrinterHelper.SendStringToPrinter(this.printerName, mailToPrint);
                    msg.StoreToFile("C:/Mytext.txt");

                    if (msg.Attachments != null)
                    {
                        for (int i = 0; i < msg.Attachments.Count; i++)
                        {
                            string directory = Path.Combine(Path.GetTempPath(), Constants.SAVE_FOLDER);

                            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                            if (msg.Attachments[i] != null)
                            {
                                string fileDirectory = Path.Combine(directory, msg.Attachments[i].Filename);
                                msg.Attachments[i].StoreToFile(fileDirectory);

                                RawPrinterHelper.SendFileToPrinter(this.printerName, fileDirectory);
                                File.Delete(fileDirectory);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Could not print the mails", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FormatMailToPrint(ActiveUp.Net.Mail.Message msg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("From: ");
            sb.Append(msg.From.Email);
            sb.Append(Environment.NewLine);
            sb.Append("To: ");

            int i = 0;
            foreach (Address add in msg.To)
            {
                i++;
                sb.Append(add.Email);
                if (msg.To.Count != i) sb.Append(", ");
            }

            sb.Append(Environment.NewLine);
            sb.Append("Subject: ");
            sb.Append(msg.Subject);
            sb.Append(Environment.NewLine);
            sb.Append("Date: ");
            sb.Append(msg.Date.ToString("d"));
            sb.Append(Environment.NewLine);

            //Mail Body
            sb.Append("Mail Body: ");
            sb.Append(Environment.NewLine);
            sb.Append(msg.BodyText.Text);

            return sb.ToString();
        }

        /// <summary>
        /// Method for delete a message into pop client
        /// </summary>
        /// <param name="messageId">The id of the message</param>
        public void DeleteMessageInList(string messageId)
        {
            int index = 0;
            int messageCount = this._pop3Client.MessageCount;
            for (int i = 1; i <= messageCount; i++)
            {
                Header header = this._pop3Client.RetrieveHeaderObject(i);
                if (header.MessageId.Equals(messageId))
                {
                    index = i;
                    break;
                }
            }

            if (index > 0)
            {
                this._pop3Client.DeleteMessage(index);
            }
        }

        /// <summary>
        /// Method for get the message count.
        /// </summary>
        /// <returns>The message count number.</returns>
        public int GetMessageCount()
        {
            return this._pop3Client.MessageCount;
        }

        /// <summary>
        /// Gets a message through its MessageId
        /// </summary>
        /// <param name="id">The Message id</param>
        /// <returns>A message</returns>
        public ActiveUp.Net.Mail.Message getMessage(string id)
        {
            foreach (ActiveUp.Net.Mail.Message msg in this.ListMessageInbox)
            {
                if (msg.MessageId == id)
                {
                    return msg;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets a message through its index
        /// </summary>
        /// <param name="id">The index</param>
        /// <returns>A message</returns>
        public ActiveUp.Net.Mail.Message getMessageByIndex(int id)
        {
            return this._pop3Client.RetrieveMessageObject(id);
        }

        public void StopThread()
        {
            if (this.retrievesThread != null)
            {
                this.threadStop = true;
            }
        }

        public void StartThread()
        {
            if(this.retrievesThread == null || !this.retrievesThread.IsAlive)
            {
            this.retrievesThread = new Thread(new ThreadStart(RetrieveMessages));
            retrievesThread.Start();
            }
        }
    }
}