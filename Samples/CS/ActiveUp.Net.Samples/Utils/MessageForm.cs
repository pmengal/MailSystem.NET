using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ActiveUp.Net.Samples.Utils
{
    public partial class MessageForm : Form
    {
        ActiveUp.Net.Mail.Message _message = null;

        public MessageForm(ActiveUp.Net.Mail.Message message)
        {
            InitializeComponent();
            _message = message;
            LoadMessage();
        }

        private void LoadMessage()
        {
            if (_message != null)
            {
                foreach (ActiveUp.Net.Mail.Address address in _message.To)
                {
                    _tbToEmail.Text += FormatAddress(address) + " ; ";
                }

                _tbFromEmail.Text = FormatAddress(_message.From);

                _tbSubject.Text = _message.Subject;

                _wbBody.DocumentText = _message.BodyHtml.Text == string.Empty ? _message.BodyText.Text : _message.BodyHtml.Text;

            }
        }

        private string FormatAddress(ActiveUp.Net.Mail.Address address)
        {
            string result = string.Empty;

            if (address.Name.TrimEnd() != string.Empty)
            {
                result = string.Format("{0} ({1})",address.Name,address.Email);
            }
            else
            {
                result = address.Email;
            }

            return result;
        }

        private void _bOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}