using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ActiveUp.MailSystem.DesktopClient.AccountSetupUI
{
    /// <summary>
    /// This class represens a SettingsReminderForm.
    /// It is used for user configure the mail account settings
    /// at application first startup or empty settings.
    /// </summary>
    public partial class SettingsReminderForm : Form
    {
        /// <summary>
        /// SettingsReminderForm constructor.
        /// </summary>
        public SettingsReminderForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Event handler for button configure account click.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnConfigureAccounts_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Event handler for button not now click.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnNotNow_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    }
}