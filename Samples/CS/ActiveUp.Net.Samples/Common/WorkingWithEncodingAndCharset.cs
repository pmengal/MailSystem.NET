using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Samples.Utils;
using ActiveUp.Net.Mail;

namespace ActiveUp.Net.Samples.Common
{
    public partial class WorkingWithEncodingAndCharset : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public WorkingWithEncodingAndCharset(SamplesConfiguration config)
        {
            this.Config = config;
            InitializeComponent();
            InitializeSample();
            InitCharset();

            _comboTransfertEncoding.SelectedIndex = 0;
            _comboCharset.SelectedIndex = 0;
        }

        protected void InitCharset()
        {
            _comboCharset.Items.Add(new ComboItem("Arabic Alphabet (ISO)", "iso-8859-6"));
            _comboCharset.Items.Add(new ComboItem("Korean (EUC)", "euc-kr"));
            _comboCharset.Items.Add(new ComboItem("Western Alphabet", "iso-8859-1"));
            _comboCharset.Items.Add(new ComboItem("Central European Alphabet (ISO)", "iso-8859-2"));
            _comboCharset.Items.Add(new ComboItem("Latin 3 Alphabet (ISO)", "iso-8859-3"));
            _comboCharset.Items.Add(new ComboItem("Baltic Alphabet (ISO)", "iso-8859-4"));
            _comboCharset.Items.Add(new ComboItem("Cyrillic Alphabet (ISO)", "iso-8859-5"));
            _comboCharset.Items.Add(new ComboItem("Greek Alphabet (ISO)", "iso-8859-7"));
            _comboCharset.Items.Add(new ComboItem("Hebrew Alphabet (ISO)", "iso-8859-8"));
            _comboCharset.Items.Add(new ComboItem("Cyrillic Alphabet (KOI8-R)", "koi8-r"));
            _comboCharset.Items.Add(new ComboItem("Japanese (Shift-JIS)", "shift-jis"));
            _comboCharset.Items.Add(new ComboItem("Japanese (EUC)", "x-euc"));
            _comboCharset.Items.Add(new ComboItem("Universal Alphabet (UTF-8)", "utf-8"));
            _comboCharset.Items.Add(new ComboItem("Central European Alphabet (Windows)", "windows-1250"));
            _comboCharset.Items.Add(new ComboItem("Cyrillic Alphabet (Windows)", "windows-1251"));
            _comboCharset.Items.Add(new ComboItem("Western Alphabet (Windows)", "windows-1252"));
            _comboCharset.Items.Add(new ComboItem("Greek Alphabet (Windows)", "windows-1253"));
            _comboCharset.Items.Add(new ComboItem("Turkish Alphabet", "windows-1254"));
            _comboCharset.Items.Add(new ComboItem("Hebrew Alphabet (Windows)", "windows-1255"));
            _comboCharset.Items.Add(new ComboItem("Baltic Alphabet (Windows)", "windows-1257"));
            _comboCharset.Items.Add(new ComboItem("Vietnamese Alphabet (Windows)", "windows-1258"));
            _comboCharset.Items.Add(new ComboItem("Thai (Windows)", "windows-874"));
        }

        protected override void InitializeSample()
        {
            base.InitializeSample();

            this._tbToEmail.Text = this.Config.ToEmail;
            this._tbFromEmail.Text = this.Config.FromEmail;
            this._tbSubject.Text = this.Config.DefaultSubject;
            this._tbBodyText.Text = this.Config.DefaultBodyText;
            this._tbSmtpServer.Text = this.Config.MainSmtpServer;
        }

        private void _tbSendMessage_Click(object sender, EventArgs e)
        {
            this.AddLogEntry("Creating message.");

            // We create the message object
            ActiveUp.Net.Mail.Message message = new ActiveUp.Net.Mail.Message();

            // We assign the sender email
            message.From.Email = this._tbFromEmail.Text;

            // We assign the recipient email
            message.To.Add(this._tbToEmail.Text);

            // We assign the subject
            message.Subject = this._tbSubject.Text;

            // We assign the body text
            message.BodyText.Text = this._tbBodyText.Text;

            // We use base 64 encoding
            message.ContentTransferEncoding = (ContentTransferEncoding)Enum.Parse(typeof(ContentTransferEncoding), (string)_comboTransfertEncoding.SelectedItem, true); 

            // We set the charset
            message.Charset = ((ComboItem)_comboCharset.SelectedItem).Value;

            // We send the email using the specified SMTP server
            this.AddLogEntry("Sending message.");

            try
            {
                message.Send(this._tbSmtpServer.Text);

                this.AddLogEntry("Message sent successfully.");
            }

            catch (SmtpException ex)
            {
                this.AddLogEntry(string.Format("Smtp Error: {0}", ex.Message));
            }

            catch (Exception ex)
            {
                this.AddLogEntry(string.Format("Failed: {0}", ex.Message));
            }
        }
    }
}

