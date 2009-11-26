using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class ConfigurationEmailAccount : System.Web.UI.UserControl
{
    private AccountSettings acc;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.acc = new AccountSettings();

        if(!Page.IsPostBack) {        
            loadAccontSettings();
        }

        TextBoxPortIncoming.Attributes.Add("OnKeyDown", "this.value=clean_String(this.value)");
        TextBoxPortOutgoing.Attributes.Add("OnKeyDown", "this.value=clean_String(this.value)");
    }

    protected void CheckBoxPortIncoming_CheckedChanged(object sender, EventArgs e)
    {
        if (TextBoxPortIncoming.Enabled)
        {
            TextBoxPortIncoming.Enabled = false;
        }
        else
        {
            TextBoxPortIncoming.Enabled = true;
        }
    }
    protected void CheckBoxPortOutgoing_CheckedChanged(object sender, EventArgs e)
    {
        if (TextBoxPortOutgoing.Enabled)
        {
            TextBoxPortOutgoing.Enabled = false;
        }
        else
        {
            TextBoxPortOutgoing.Enabled = true;
        }

    }
    protected void ButtonOK_Action(object sender, EventArgs e)
    {
        string emailAddress;
        string password;
        string displayName;
        string incomingMailServer;
        string outgoingServer;
        string loginId;
        int portIncomingServer = 80;
        int portOutgoingServer = 80;
        bool isIncomeSecureConnection;
        bool isOutgoingSecureConnection;
        bool isOutgoingWithAuthentication;

        ErrorEmailAddress.Text = "";
        ErrorPassword.Text = "";
        ErrorDispName.Text = "";

        if (TextBoxEmailAddress.Text.Equals(string.Empty) || !TextBoxEmailAddress.Text.Contains("@"))
        {
            ErrorEmailAddress.Text = "Enter a valid email address!";
        }
        if (TextBoxPassword.Text.Equals(string.Empty))
        {
            ErrorPassword.Text = "Enter a valid password!";
        }
        if (TextBoxDisplayName.Text.Equals(string.Empty))
        {
            ErrorDispName.Text = "Enter a valid display name!";
        }

        emailAddress = TextBoxEmailAddress.Text;
        password = TextBoxPassword.Text;
        displayName = TextBoxDisplayName.Text;
        incomingMailServer = DropDownListIncomingServer.SelectedValue;

        if (CheckBoxPortIncoming.Checked)
        {
            if (!TextBoxPortIncoming.Text.Equals(string.Empty))
            {
                portIncomingServer = Convert.ToInt32(TextBoxPortIncoming.Text);
            }

            
        }
        if (CheckBoxPortOutgoing.Checked)
        {
            if (!TextBoxPortOutgoing.Text.Equals(string.Empty))
            {
                portOutgoingServer = Convert.ToInt32(TextBoxPortOutgoing.Text);
            }            
        }

        isIncomeSecureConnection = CheckBoxSecureConnection.Checked;
        isOutgoingSecureConnection = CheckBoxOutgoingSecure.Checked;
        isOutgoingWithAuthentication = CheckBoxOutgoingAuthentication.Checked;

        loginId = TextBoxLoginID.Text;
        outgoingServer = TextBoxOutgoingServer.Text;

        //These informations are going to save

        AccountSettings.AccountInfo acc_info = new AccountSettings.AccountInfo();
        acc_info.EmailAddress = emailAddress;
        acc_info.Password = password;
        acc_info.DisplayName = displayName;
        acc_info.IncomingMailServer = incomingMailServer;
        acc_info.OutgoingServer = outgoingServer;
        acc_info.LoginId = loginId;
        acc_info.PortIncomingServer = portIncomingServer;
        acc_info.PortOutgoingServer = portOutgoingServer;
        acc_info.IsIncomeSecureConnection = isIncomeSecureConnection;
        acc_info.IsOutgoingSecureConnection = isOutgoingSecureConnection;
        acc_info.IsOutgoingWithAuthentication = isOutgoingWithAuthentication;
        acc_info.PortIncomingChecked = CheckBoxPortIncoming.Checked;
        acc_info.PortOutgoingChecked = CheckBoxPortOutgoing.Checked;

        this.acc.Add(acc_info);
        AccountSettings.Save("AccountSettings",acc);
    }

    private void loadAccontSettings()
    {
        this.acc = AccountSettings.Load("AccountSettings");
        //AccountSettings.AccountInfo acc_info = (AccountSettings.AccountInfo)((IEnumerator)this.acc.Accounts.GetEnumerator()).Current;

        AccountSettings.AccountInfo [] arrayAcc_info = this.acc.Accounts;

        if (arrayAcc_info != null) {

            AccountSettings.AccountInfo acc_info = arrayAcc_info[0];
        
            if(acc_info != null) {

                TextBoxPassword.Text = acc_info.Password;
                TextBoxDisplayName.Text = acc_info.DisplayName;

                int i = 0;
                foreach (ListItem item in DropDownListIncomingServer.Items)
                {
                    if (item.Text == acc_info.IncomingMailServer)
                    {
                         DropDownListIncomingServer.SelectedIndex = i;
                    }
                    i++;
                }

                TextBoxEmailAddress.Text = acc_info.EmailAddress;
                TextBoxOutgoingServer.Text = acc_info.OutgoingServer;
                TextBoxLoginID.Text = acc_info.LoginId;
                TextBoxPortIncoming.Text = acc_info.PortIncomingServer.ToString();
                TextBoxPortOutgoing.Text = acc_info.PortOutgoingServer.ToString();
                CheckBoxSecureConnection.Checked = acc_info.IsIncomeSecureConnection;
                CheckBoxOutgoingSecure.Checked = acc_info.IsOutgoingSecureConnection;
                CheckBoxOutgoingAuthentication.Checked = acc_info.IsOutgoingWithAuthentication;
                CheckBoxPortIncoming.Checked = acc_info.PortIncomingChecked;
                CheckBoxPortOutgoing.Checked = acc_info.PortOutgoingChecked;
        }
    }
    }
}
