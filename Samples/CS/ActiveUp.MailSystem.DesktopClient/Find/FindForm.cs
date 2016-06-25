using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ActiveUp.MailSystem.DesktopClient
{

    /// <summary>
    /// This class represents a dialog for Find mail messages.
    /// </summary>
    public partial class FindForm : Form
    {

        /// <summary>
        /// Attribute for find settings.
        /// </summary>
        private FindSettings _findSettings;

        /// <summary>
        /// Find form constructor.
        /// </summary>
        public FindForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Property for find settings.
        /// </summary>
        public FindSettings FindSettings
        {
            get { return _findSettings; }
            set { _findSettings = value; }
        }

        /// <summary>
        /// Button find envent handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            string from = this.txtFrom.Text;
            string to = this.txtTo.Text;
            string message = this.txtMessage.Text;
            string subject = this.txtSubject.Text;
            bool attachemnt = this.chkAttachments.Checked;

            string dateFrom = string.Empty;
            if (this.chkFromDate.Checked)
            {
                dateFrom = this.datePickerFrom.Text;
            }

            string dateTo = string.Empty;
            if (this.chkToDate.Checked)
            {
                dateTo = this.datePickerTo.Text;
            }

            this._findSettings = new FindSettings(from, to, subject, message, 
                dateFrom, dateTo, attachemnt);

            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Load the fields according the find settings.
        /// </summary>
        /// <param name="findSettings">The FindSettings object.</param>
        public void LoadFindSettings(FindSettings findSettings)
        {
            this.txtFrom.Text = findSettings.From;
            this.txtTo.Text = findSettings.To;
            this.txtMessage.Text = findSettings.Message;
            this.txtSubject.Text = findSettings.Subject;
            this.chkAttachments.Checked = findSettings.Attachemnt;

            if (findSettings.DateFrom.Equals(string.Empty))
            {
                this.chkFromDate.Checked = false;
            }
            else
            {
                this.chkFromDate.Checked = true;
                this.datePickerFrom.Text = findSettings.DateFrom;
            }

            if (findSettings.DateTo.Equals(string.Empty))
            {
                this.chkToDate.Checked = false;
            }
            else
            {
                this.chkToDate.Checked = true;
                this.datePickerTo.Text = findSettings.DateTo;
            }
        }

        /// <summary>
        /// Button cancel envent handler.
        /// Reset the filter.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this._findSettings = new FindSettings();
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Check box from date checked changed event.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void chkFromDate_CheckedChanged(object sender, EventArgs e)
        {
            this.datePickerFrom.Enabled = this.chkFromDate.Checked;
        }

        /// <summary>
        /// Check box to date checked changed event.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void chtToDate_CheckedChanged(object sender, EventArgs e)
        {
            this.datePickerTo.Enabled = this.chkToDate.Checked;
        }

    }
}