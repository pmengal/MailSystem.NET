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
    /// This Form allows the users to add&Remove and edit their email accounts.
    /// </summary>
    public partial class AccountSettingsForm : Form
    {

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        public AccountSettingsForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Method for load account settings.
        /// </summary>
        private void LoadAccoutnSetings()
        {
            this.lstAccounts.Items.Clear();

            AccountSettings accSettings = Facade.GetInstance().GetAllAccountInfo();
            foreach (AccountSettings.AccountInfo info in accSettings)
            {
                this.lstAccounts.Items.Add(info);
            }
        }

        /// <summary>
        /// Close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Button add event click.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            AddAccountWizardForm frmAdd = new AddAccountWizardForm();
            if (frmAdd.ShowDialog() == DialogResult.OK)
            {
                AccountSettings.AccountInfo accountInfo = frmAdd.NewAccountInfo;
                Facade.GetInstance().AddAccountInfo(accountInfo);
                Facade.GetInstance().SaveAccountSettings();
                this.lstAccounts.Items.Add(accountInfo);
            }
        }

        /// <summary>
        /// Form Load event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void AccountSettingsForm_Load(object sender, EventArgs e)
        {
            this.LoadAccoutnSetings();
        }

        /// <summary>
        /// Selected intex changed event handler for accounts list.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void lstAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Button remove event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstAccounts.SelectedIndex >= 0)
            {
                AccountSettings.AccountInfo accountInfo = 
                    (AccountSettings.AccountInfo)(this.lstAccounts.SelectedItem);

                Facade.GetInstance().DeleteAccountInfo(accountInfo);
                this.lstAccounts.Items.Remove(accountInfo); 
            }
        }

        /// <summary>
        /// Button properties click.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnProperties_Click(object sender, EventArgs e)
        {
            if (lstAccounts.SelectedIndex >= 0)
            {
                AccountSettings.AccountInfo accountInfo = 
                    (AccountSettings.AccountInfo)(this.lstAccounts.SelectedItem);
                
                AddAccountWizardForm frmAdd = new AddAccountWizardForm();
                frmAdd.NewAccountInfo = accountInfo;
                DialogResult dr = frmAdd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    Facade.GetInstance().SaveAccountSettings();
                    this.LoadAccoutnSetings();
                }
            }
        }

        /// <summary>
        /// Button event for set the default mail settings.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnSetDefault_Click(object sender, EventArgs e)
        {
            if (lstAccounts.SelectedIndex >= 0)
            {
                AccountSettings.AccountInfo accountInfo = 
                    (AccountSettings.AccountInfo) (this.lstAccounts.SelectedItem);
                Facade.GetInstance().SetDefaultAccountInfo(accountInfo);
                Facade.GetInstance().SaveClientSettings();
            }
        }
    }
}
