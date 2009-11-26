using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using ActiveUp.Net.Mail;
using ActiveUp.MailSystem.DesktopClient;

namespace ActiveUp.MailSystem.DesktopClient
{

    /// <summary>
    /// This class represents a Wizard for add/modify account information.
    /// </summary>
    public partial class AddAccountWizardForm : Form
    {
        AccountSettings.AccountInfo _accInfo = null;
        bool m_bFinish = false;

        /// <summary>
        /// Property for access/modify the account information.
        /// </summary>
        public AccountSettings.AccountInfo NewAccountInfo
        {
            get
            {
                return _accInfo;
            }
            set
            {
                _accInfo = value;
                this.LoadFields();
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public AddAccountWizardForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method for load the fields.
        /// </summary>
        private void LoadFields()
        {
            this.txtEmailAddress.Text = this._accInfo.EmailAddress;
            this.txtPassword.Text = this._accInfo.Password;
            this.txtName.Text = this._accInfo.UserName;
            this.chkRememberPassword.Checked = this._accInfo.RememberPassword;
            this.ddlServerType.Text = this._accInfo.MailAccountType.ToString();
            this.txtIncomingServer.Text = this._accInfo.IncomingServerName;
            this.txtIncomingPortNumber.Text = this._accInfo.InPort.ToString();
            this.chkSSL.Checked = this._accInfo.IncomingIsSSL;
            this.txtLogInID.Text = this._accInfo.LoginName;
            this.txtOutgoingServer.Text = this._accInfo.OutgoingServerName;
            this.txtOutgoingPort.Text = this._accInfo.OutPort.ToString();
            this.chkOutgoingNeedsSSL.Checked = this._accInfo.OutgoingIsSSL;
            this.chkOutgoingNeedsAuth.Checked = this._accInfo.OutgoingServerNeedsAuthentication;

            this.chkInPort.Checked = this._accInfo.InPortEnabled;
            this.chkOutPort.Checked = this._accInfo.OutPortEnabled;

            if (!this.chkInPort.Checked)
            {
                this.txtIncomingPortNumber.Text = string.Empty; 
            }

            if (!this.chkOutPort.Checked)
            {
                this.txtOutgoingPort.Text = string.Empty;
            }
        }
        
        // Free email link
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Redirect to www.gmail.com
            Process.Start("http://www.gmail.com");
        }


        // prompt the user and close
        private void btnCancel_Click(object sender, EventArgs e)
        {
                this.Close();
        }

        // Current step
        private enum Step
        {
            Screen1,
            Screen2,
            Screen3,
            Finish
        }



        // Set the current screen
        private Step m_CurrStep = Step.Screen1;

        private void AddAccountWizardForm_Load(object sender, EventArgs e)
        {
            panelScreen1.Visible = true;
            panelScreen2.Visible = false;
            panelScreen3.Visible = false;

            if (_accInfo == null)
            {
                _accInfo = new AccountSettings.AccountInfo();
            }
            ddlAuthenticationType.Items.Clear();
            ddlAuthenticationType.Items.AddRange(Enum.GetNames(typeof(EncryptionType)));
        }

        /// <summary>
        /// Button next click.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            // validate the fields.
            if (this.ValidateFields(m_CurrStep))
            {
                m_CurrStep++;

                if (m_CurrStep == Step.Finish)
                {
                    this.DialogResult = DialogResult.OK;
                    m_bFinish = true;
                    this.Close();
                }

                if (m_CurrStep < Step.Screen3)
                    btnNext.Text = "&Next";
                else
                    btnNext.Text = "&Finish";

                if (m_CurrStep < (Step)0)
                {
                    m_CurrStep = Step.Screen1;
                }

                if (m_CurrStep > (Step)2)
                    m_CurrStep = Step.Screen3;

                try
                {
                    switch (m_CurrStep)
                    {
                        case Step.Screen1:
                            panelScreen1.Visible = true;
                            panelScreen2.Visible = false;
                            panelScreen3.Visible = false;
                            btnBack.Visible = false;
                            break;

                        case Step.Screen2:
                            if (string.IsNullOrEmpty(ddlServerType.SelectedText))
                            {
                                if (_accInfo.MailAccountType == AccountType.IMAP)
                                {
                                    ddlServerType.SelectedIndex = 1;
                                }
                                else
                                {
                                    ddlServerType.SelectedIndex = 0;
                                    _accInfo.MailAccountType = AccountType.POP3;
                                }
                            }

                            if (string.IsNullOrEmpty(ddlAuthenticationType.SelectedText))
                            {
                                _accInfo.EncType = EncryptionType.None;
                                ddlAuthenticationType.SelectedIndex = 0;
                            }
                            _accInfo.EmailAddress = txtEmailAddress.Text;
                            _accInfo.Password = txtPassword.Text;
                            _accInfo.RememberPassword = chkRememberPassword.Checked;
                            _accInfo.UserName = txtName.Text;


                            panelScreen1.Visible = false;
                            panelScreen2.Visible = true;
                            panelScreen3.Visible = false;
                            btnBack.Visible = true;
                            break;

                        case Step.Screen3:
                            _accInfo.MailAccountType = (AccountType)Enum.Parse(typeof(AccountType), ddlServerType.SelectedItem.ToString());
                            _accInfo.EncType = (EncryptionType)(Enum.Parse(typeof(EncryptionType), ddlAuthenticationType.SelectedItem.ToString()));
                            _accInfo.IncomingServerName = txtIncomingServer.Text;
                            _accInfo.IncomingIsSSL = chkSSL.Checked;
                            _accInfo.InPortEnabled = this.chkInPort.Checked;
                            
                            if (_accInfo.InPortEnabled)
                            {
                                _accInfo.InPort = int.Parse(txtIncomingPortNumber.Text);
                            }

                            _accInfo.LoginName = txtLogInID.Text;
                            _accInfo.OutgoingServerName = txtOutgoingServer.Text;
                            _accInfo.OutgoingIsSSL = chkOutgoingNeedsSSL.Checked;
                            _accInfo.OutgoingServerNeedsAuthentication = chkOutgoingNeedsAuth.Checked;
                            _accInfo.OutPortEnabled = this.chkOutPort.Checked;
                            
                            if (_accInfo.OutPortEnabled)
                            {
                                _accInfo.OutPort = int.Parse(txtOutgoingPort.Text);
                            }

                            panelScreen1.Visible = false;
                            panelScreen2.Visible = false;

                            DelegateTryConnectAsynch delegateTryConnect = TryConnect;
                            delegateTryConnect.BeginInvoke(null, null);

                            panelScreen3.Visible = true;


                            btnBack.Visible = true;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    btnBack_Click(this, EventArgs.Empty);
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Delegate for try to connect asynch.
        /// </summary>
        /// <returns>String retun.</returns>
        public delegate string DelegateTryConnectAsynch();
        
        /// <summary>
        /// Method for try to connect
        /// </summary>
        /// <returns>The string return.</returns>
        public string TryConnect()
        {
            this.progressBar.Visible = true;
            this.btnNext.Enabled = false;

            this.labelStatus.Text = "Verifying email configurations...";

            bool success = true;
            string msg = "You have just successfully completed adding a new account." + 
                Environment.NewLine + "Please select finish to save the changes or cancel to discard them.";

            // verify incoming server email configurations.

            try
            {
                if (this._accInfo.MailAccountType == AccountType.POP3)
                {
                    Pop3Controller pop3Controller = new Pop3Controller(this._accInfo);
                    pop3Controller.Disconnect();
                }
                else if (this._accInfo.MailAccountType == AccountType.POP3)
                {
                    Imap4Controller imap4Controller = new Imap4Controller(this._accInfo);
                    imap4Controller.Disconnect();
                }
            }
            catch (Exception)
            {
                success = false;
                msg = "The incoming server email configuration is not valid.";
            }

            // verify outcoming server email configurations.

            if (success)
            {
                this.labelStatus.Text = "Sending test e-mail...";

                try
                {
                    SmtpController smtpController = new SmtpController();
                    ActiveUp.Net.Mail.Message message = smtpController.SendMessage(this._accInfo,
                        this._accInfo.EmailAddress, "ActiveUp Test", "ActiveUp Test", new string[0]);
                }
                catch (Exception)
                {
                    success = false;
                    msg = "The outcoming server email configuration is not valid.";
                }
            }
            
            if (success)
            {
                this.btnNext.Enabled = true;
            }

            this.labelStatus.Text = msg;
            this.progressBar.Visible = false;

            return string.Empty;
        }

        /// <summary>
        /// Method for validate fields.
        /// </summary>
        /// <param name="m_CurrStep"></param>
        /// <returns></returns>
        private bool ValidateFields(Step m_CurrStep)
        {
            bool valid = true;
            string msgError = string.Empty;
            int temp;
            
            // screen 1.
            if (m_CurrStep == Step.Screen1)
            {
                if (this.txtEmailAddress.Text.Trim().Equals(string.Empty) ||
                    !this.txtEmailAddress.Text.Contains("@"))
                {
                    msgError = "Enter a valid email address.";
                }
                else if (this.txtPassword.Text.Trim().Equals(string.Empty))
                {
                    msgError = "Enter a valid password.";
                }
                else if (this.txtName.Text.Trim().Equals(string.Empty))
                {
                    msgError = "Enter a valid display name.";
                }
            }

            // screen 2.
            else if (m_CurrStep == Step.Screen2)
            {
                if (!int.TryParse(this.txtIncomingPortNumber.Text, out temp) &&
                    this.chkOutPort.Checked)
                {
                    msgError = "Enter a valid outgoing server port number.";
                }
                if (this.txtIncomingServer.Text.Trim().Equals(string.Empty))
                {
                    msgError = "Enter a valid outgoing server.";
                }
                if (!int.TryParse(this.txtIncomingPortNumber.Text, out temp) && 
                    this.chkInPort.Checked)
                {
                    msgError = "Enter a valid incoming server port number.";
                }
                if (this.txtIncomingServer.Text.Trim().Equals(string.Empty))
                {
                    msgError = "Enter a valid incoming server.";
                }
            }

            // show error message.
            if (!msgError.Equals(string.Empty))
            {
                valid = false;
                MessageBox.Show(msgError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return valid;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            btnNext.Text = "&Next";
            btnNext.Enabled = true;

            m_CurrStep--;
            if (m_CurrStep < (Step)0)
                m_CurrStep = Step.Screen1;

            if (m_CurrStep > (Step)2)
                m_CurrStep = Step.Screen3;

            switch (m_CurrStep)
            {
                case Step.Screen1:
                    panelScreen1.Visible = true;
                    panelScreen2.Visible = false;
                    panelScreen3.Visible = false;
                    btnBack.Visible = false;
                    break;

                case Step.Screen2:
                    panelScreen1.Visible = false;
                    panelScreen2.Visible = true;
                    panelScreen3.Visible = false;
                    btnBack.Visible = true;
                    break;

                case Step.Screen3:
                    panelScreen1.Visible = false;
                    panelScreen2.Visible = false;
                    panelScreen3.Visible = true;
                    btnBack.Visible = true;
                    btnNext.Text = "&Finish";
                    break;
            }
        }

        private void AddAccountWizardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_bFinish)
                return;

            if (MessageBox.Show("Are you sure you wish to cancel adding/modifying this account?", "ActiveUp Email Client", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            e.Cancel = true;
        }

        /// <summary>
        /// Event for check box InPort changed.
        /// This reflect the enabled status for the incoming port number field.
        /// Allow the used to determine if the port number will be used or not
        /// for the incoming conection.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void chkInPort_CheckedChanged(object sender, EventArgs e)
        {
            this.txtIncomingPortNumber.Enabled = this.chkInPort.Checked;
        }

        /// <summary>
        /// Event for check box OutPort changed.
        /// This reflect the enabled status for the outcoming port number field.
        /// Allow the used to determine if the port number will be used or not
        /// for the outcoming conection.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void chkOutPort_CheckedChanged(object sender, EventArgs e)
        {
            this.txtOutgoingPort.Enabled = this.chkOutPort.Checked;
        }

        /// <summary>
        /// Tick event for timer.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (progressBar.Value >= 100)
            {
                progressBar.Value = 0;
            }
            else
            {
                progressBar.Value += 5;
            }
        }



    }
}
