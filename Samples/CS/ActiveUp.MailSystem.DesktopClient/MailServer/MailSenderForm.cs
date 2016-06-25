using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Mail;
using System.Diagnostics;
using Microsoft.ConsultingServices.HtmlEditor;
using System.IO;

namespace ActiveUp.MailSystem.DesktopClient.Mail
{

    /// <summary>
    /// Enumeration for mail sender types.
    /// They are used for load the message properly in the dialog.
    /// </summary>
    public enum MailSenderType
    {
        NewMessage, Reply, ReplyToAll, Forward
    }

    /// <summary>
    /// This class represents a mail sender form.
    /// </summary>
    public partial class MailSenderForm : Form
    {

        /// <summary>
        /// Attribute for HtmlEditorControl.
        /// Reference at: http://windowsclient.net/articles/htmleditor.aspx
        /// </summary>
        private HtmlEditorControl htmlEditorControl;

        /// <summary>
        /// Mail message string representation for print.
        /// </summary>
        private StringReader mailMessageStringReader;

        /// <summary>
        /// Cosntructor.
        /// </summary>
        public MailSenderForm()
        {
            this.InitializeComponent();

            this.htmlEditorControl = new HtmlEditorControl();
            this.htmlEditorControl.Dock = DockStyle.Fill;
            this.htmlEditorControl.AutoWordWrap = true;
            this.htmlEditorControl.AutoScroll = true;
            this.htmlEditorControl.ToolbarVisible = true;
            this.htmlEditorControl.ToolbarDock = DockStyle.Top;
            this.panelEditor.Controls.Add(htmlEditorControl);
        }

        /// <summary>
        /// Button send click event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (this.ValidateFields())
            {
                // Current cursor.
                Cursor.Current = Cursors.WaitCursor;

                // Attachments list.
                List<string> attachments = new List<string>();
                
                // assign the attachments.
                foreach (ListViewItem item in this.listViewAttachments.Items)
                {
                    if (item.Checked)
                    {
                        attachments.Add(item.Text);
                    }
                }

                // send the mail message.
                Facade.GetInstance().SendMessage(this.tbTo.Text, this.tbSubject.Text,
                    this.htmlEditorControl.InnerText, attachments.ToArray());

                // Default cursor.
                Cursor.Current = Cursors.Default;

                // dialog result ok.
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Call on BeginSend
        /// </summary>
        /// <param name="ar">IAsyncResult.</param>
        private void SendingDone(IAsyncResult ar)
        {
            string msg = "Message sent successfully.";
            MessageBox.Show(msg, "ActiveUp", MessageBoxButtons.OK, MessageBoxIcon.Information);
        } 

        /// <summary>
        /// Button cancel click event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Method for validate fields before send the message.
        /// </summary>
        /// <returns></returns>
        private bool ValidateFields()
        {
            bool ret = true;
            string msgError = string.Empty;

            if (this.tbTo.Text.Trim().Equals(string.Empty))
            {
                msgError = "Please specify at least one recipient.";
            }

            if (!msgError.Equals(string.Empty))
            {
                ret = false;
                MessageBox.Show(msgError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ret;
        }

        /// <summary>
        /// Method for verify if the fields in email are empty.
        /// </summary>
        /// <returns></returns>
        private bool IsEmpty()
        {
            bool isEmpty = false;
            if (this.tbCc.Text.Trim().Equals(string.Empty) &&
                this.tbSubject.Text.Trim().Equals(string.Empty) && 
                this.tbTo.Text.Trim().Equals(string.Empty) &&
                this.listViewAttachments.Items.Count == 0 &&
                (this.htmlEditorControl.InnerText == null ||
                 this.htmlEditorControl.InnerText.Trim().Equals(string.Empty)))
            {
                isEmpty = true;
            }
            return isEmpty;
        }

        /// <summary>
        /// Event handler for copy action.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            Control activeControl = this.ActiveControl;
            if (activeControl != null && activeControl is TextBox)
            {
                TextBox textBox = (TextBox)activeControl;
                textBox.Copy();
            }
            else if (activeControl != null && activeControl is HtmlEditorControl)
            {
                HtmlEditorControl textBox = (HtmlEditorControl)activeControl;
                textBox.TextCopy();
            }
        }

        /// <summary>
        /// Event handler for paste action.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void toolStripButtonPaste_Click(object sender, EventArgs e)
        {
            Control activeControl = this.ActiveControl;
            if (activeControl != null && activeControl is TextBox)
            {
                TextBox textBox = (TextBox)activeControl;
                textBox.Paste();
            }
            else if (activeControl != null && activeControl is HtmlEditorControl)
            {
                HtmlEditorControl textBox = (HtmlEditorControl)activeControl;
                textBox.TextPaste();
            }
        }

        /// <summary>
        /// Event handler for cut action.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void toolStripButtonCut_Click(object sender, EventArgs e)
        {
            Control activeControl = this.ActiveControl;
            if (activeControl != null && activeControl is TextBox)
            {
                TextBox textBox = (TextBox)activeControl;
                textBox.Cut();
            }
            else if (activeControl != null && activeControl is HtmlEditorControl)
            {
                HtmlEditorControl textBox = (HtmlEditorControl)activeControl;
                textBox.TextCut();
            }
        }

        /// <summary>
        /// Event handler for undo action.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void toolStripButtonUndo_Click(object sender, EventArgs e)
        {
            Control activeControl = this.ActiveControl;
            if (activeControl != null && activeControl is TextBox)
            {
                TextBox textBox = (TextBox)activeControl;
                textBox.Undo();
            }
        }

        /// <summary>
        /// Event handler for select all action.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control activeControl = this.ActiveControl;
            if (activeControl != null && activeControl is TextBox)
            {
                TextBox textBox = (TextBox)activeControl;
                textBox.SelectAll();
            }
            else if (activeControl != null && activeControl is HtmlEditorControl)
            {
                HtmlEditorControl textBox = (HtmlEditorControl)activeControl;
                textBox.TextSelectAll();
            }
        }

        /// <summary>
        /// Event handler for new message action.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void mailMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MailSenderForm frm = new MailSenderForm();
            frm.Show();
        }

        /// <summary>
        /// Event handler for add attachment action.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void buttonAddAttachment_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;

            DialogResult dr = fileDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string fileName = fileDialog.FileName;
                ListViewItem item = this.listViewAttachments.Items.Add(fileName);
                item.Checked = true;
            }
        }

        /// <summary>
        /// Method for load messages.
        /// </summary>
        /// <param name="mailMessage">The mail messages.</param>
        /// <param name="type">The mail sender type.</param>
        public void LoadMessage(MailMessage mailMessage, MailSenderType type)
        {
            // the current mailbox.
            string mailbox = MainForm.GetInstance().GetSelectedMailbox();

            // retrieve the message body.
            string body = Facade.GetInstance().GetMessageBodyString(mailMessage, mailbox);

            // load message to.

            if (type == MailSenderType.Reply)
            {
                this.tbTo.Text = mailMessage.From;
            }
            else if (type == MailSenderType.ReplyToAll)
            {
                string to = mailMessage.From;

                if (to.Length > 0)
                {
                    to = string.Concat(to, ", ", mailMessage.To);
                }

                this.tbTo.Text = to;
            }

            // load message subject.

            if (type == MailSenderType.Reply || type == MailSenderType.ReplyToAll)
            {
                this.tbSubject.Text = string.Concat("Re: ", mailMessage.Subject);
            }
            else if (type == MailSenderType.Forward)
            {
                this.tbSubject.Text = string.Concat("Fwd: ", mailMessage.Subject);
            }

            // TODO: load message attachments.

            // load message body.

            if (type == MailSenderType.Reply || type == MailSenderType.ReplyToAll)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append("On ");
                sb.Append(mailMessage.SentDate);
                sb.Append(", ");
                sb.Append(mailMessage.From);
                sb.Append(" wrote:");
                sb.Append(Environment.NewLine);

                sb.Append(body);

                this.htmlEditorControl.InnerText = sb.ToString();
            }
            else if (type == MailSenderType.Forward)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append("---------- Forwarded message ----------");
                sb.Append(Environment.NewLine);
                sb.Append("From: ");
                sb.Append(mailMessage.From);
                sb.Append(Environment.NewLine);
                sb.Append("Date: ");
                sb.Append(mailMessage.SentDate);
                sb.Append(Environment.NewLine);
                sb.Append("Subject: ");
                sb.Append(mailMessage.Subject);
                sb.Append(Environment.NewLine);
                sb.Append("To: ");
                sb.Append(mailMessage.To);
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append(body);
                sb.Append(Environment.NewLine);

                this.htmlEditorControl.InnerText = sb.ToString();
            }

        }

        /// <summary>
        /// Button help event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void toolStripButtonHelp_Click(object sender, EventArgs e)
        {
            Process helpProcess = new Process();
            helpProcess.StartInfo.FileName = "Documentation.chm";
            helpProcess.Start();
        }

        /// <summary>
        /// Form closing event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void MailSenderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool discard = true;
            if (this.DialogResult != DialogResult.OK && !IsEmpty())
            {
                DialogResult dr =
                    MessageBox.Show("Your message has not been sent. Discard your message?",
                    "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    discard = false;
                }
            }

            if (!discard)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Save event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            string currentDir = Directory.GetCurrentDirectory();
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "ActiveUp Email Files |*.eml";
            DialogResult dr = saveDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                ActiveUp.Net.Mail.Message message = this.CreateMessage();
                message.StoreToFile(saveDialog.FileName);
            }

            Directory.SetCurrentDirectory(currentDir);
        }

        /// <summary>
        /// Print event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            string strText = this.CreateMessage().ToMimeString();
            mailMessageStringReader = new StringReader(strText);
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                this.printDocument.Print();
            }
        }

        /// <summary>
        /// Print preview event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strText = this.CreateMessage().ToMimeString();
            mailMessageStringReader = new StringReader(strText);
            printPreviewDialog.ShowDialog();
        }

        /// <summary>
        /// Print page for print document event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void printDocument_PrintPage(object sender, 
            System.Drawing.Printing.PrintPageEventArgs ev)
        {
            float linesPerPage = 0;
            float yPosition = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            string line = null;
            Font printFont = this.Font;
            SolidBrush myBrush = new SolidBrush(Color.Black);
            // Work out the number of lines per page, using the MarginBounds.
            linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);
            // Iterate over the string using the StringReader, printing each line.
            while (count < linesPerPage && ((line = mailMessageStringReader.ReadLine()) != null))
            {
                // calculate the next line position based on the height of the font according to the printing device
                yPosition = topMargin + (count * printFont.GetHeight(ev.Graphics));
                // draw the next line in the rich edit control
                ev.Graphics.DrawString(line, printFont, myBrush, leftMargin, yPosition, new StringFormat());
                count++;
            }
            // If there are more lines, print another page.
            if (line != null)
            {
                ev.HasMorePages = true;
            }
            else
            {
                ev.HasMorePages = false;
            }
            myBrush.Dispose();
        }

        /// <summary>
        /// Method for create a message.
        /// </summary>
        /// <returns></returns>
        private ActiveUp.Net.Mail.Message CreateMessage()
        {
            Facade facade = Facade.GetInstance();
            ActiveUp.MailSystem.DesktopClient.AccountSettings.AccountInfo accInfo =
                facade.GetDefaultAccountInfo();

            ActiveUp.Net.Mail.Message message = new ActiveUp.Net.Mail.Message();

            if (accInfo != null)
            {
                message.From.Email = accInfo.EmailAddress;
            }

            string separator = ",";
            string[] recipients = this.tbTo.Text.Split(separator.ToCharArray());
            foreach (string r in recipients)
            {
                // We assign the recipient email
                message.To.Add(r);
            }

            // We assign the subject
            message.Subject = this.tbSubject.Text;

            // We assign the body text
            message.BodyText.Text = this.htmlEditorControl.InnerText;

            // assign the attachments.
            foreach (ListViewItem item in this.listViewAttachments.Items)
            {
                if (item.Checked)
                {
                    message.Attachments.Add(item.Text, false);
                }
            }

            return message;
        }

    }
}