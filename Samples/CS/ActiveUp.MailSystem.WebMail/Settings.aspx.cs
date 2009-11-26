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
using System.Threading;
using System.Globalization;
using System.IO;

/// <summary>
/// Responsible to get and load account settings
/// </summary>

public partial class Settings : System.Web.UI.Page
{

    /// <summary>
    /// Settings page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    
   protected void Page_Load(object sender, EventArgs e)
    {       
        if (!Page.IsPostBack)
        {
            /*
            string permit = (string)Request.Params.Get("permit");

            if (permit != null && permit == "false")
            {
                Label3.Visible = true;
            }
            else
            {       
                loadAccontSettings();
                Label3.Visible = false;                
            }
             * */

            if (!File.Exists(Constants.ACCOUNT_FILE_NAME_SETTINGS))
            {
                Label3.Visible = true;
            }
            else
            {
                loadAccontSettings();
                Label3.Visible = false;
            }
        }
    }

    #region Methods

    /// <summary>
    /// Initialize changing to another language
    /// </summary>
    protected override void InitializeCulture()
    {
        String lang = string.Empty;
        HttpCookie cookie = Request.Cookies["DropDownLanguage"];

        if (cookie != null && cookie.Value != null)
        {
            lang = cookie.Value;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
        }
        base.InitializeCulture();
    }

    /// <summary>
    /// Method for loads account settings
    /// </summary>   
    private void loadAccontSettings()
    {
        AccountSettings.AccountInfo acc_info = Facade.GetInstance().GetDefaultAccountInfo();

        if (acc_info != null)
        {
                TextBoxPassword.Text = EncryptDescript.CriptDescript(acc_info.Password);
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
                
                TextBoxEmailAddress.Text = EncryptDescript.CriptDescript(acc_info.EmailAddress);
                TextBoxOutgoingServer.Text = acc_info.OutgoingServer;
                TextBoxLoginID.Text = acc_info.LoginId;
                TextBoxPortIncoming.Text = acc_info.PortIncomingServer.ToString();
                TextBoxPortOutgoing.Text = acc_info.PortOutgoingServer.ToString();
                CheckBoxSecureConnection.Checked = acc_info.IsIncomeSecureConnection;
                CheckBoxOutgoingSecure.Checked = acc_info.IsOutgoingSecureConnection;
                CheckBoxOutgoingAuthentication.Checked = acc_info.IsOutgoingWithAuthentication;
                CheckBoxPortIncoming.Checked = acc_info.PortIncomingChecked;
                CheckBoxPortOutgoing.Checked = acc_info.PortOutgoingChecked;
                TextBoxIncomingServer.Text = acc_info.IncomingNameMailServer;
            }
        }    

    /// <summary>
    /// Event for confirms the settings displayed in the page
    /// </summary>
    /// <param name="sender">The sender object</param>
    /// <param name="e">The event arguments</param>
    protected void ButtonOK_Click(object sender, EventArgs e)
    {
        string emailAddress;
        string password;
        string displayName;
        string incomingMailServer;
        string outgoingServer;
        string loginId;
        string incomingNameMailServer;
        int portIncomingServer = 0;
        int portOutgoingServer = 0;
        bool isIncomeSecureConnection;
        bool isOutgoingSecureConnection;
        bool isOutgoingWithAuthentication;

        emailAddress = TextBoxEmailAddress.Text;
        password = TextBoxPassword.Text;
        displayName = TextBoxDisplayName.Text;
        incomingNameMailServer = TextBoxIncomingServer.Text;
        incomingMailServer = DropDownListIncomingServer.SelectedValue;

        try
        {
            if (CheckBoxPortIncoming.Checked)
            {
                if (!TextBoxPortIncoming.Text.Equals(string.Empty))
                {
                    portIncomingServer = Convert.ToInt32(TextBoxPortIncoming.Text);

                    if (this.ImcomingWasChanged(portIncomingServer)) Facade.GetInstance().ChangeImcoming = true;
                }
            }
            if (CheckBoxPortOutgoing.Checked)
            {
                if (!TextBoxPortOutgoing.Text.Equals(string.Empty))
                {
                    portOutgoingServer = Convert.ToInt32(TextBoxPortOutgoing.Text);
                }
            }
        }
        catch (Exception)
        {
            Session["ErrorMessage"] = "The port must be an integer";
            Response.Redirect("~/ErrorPage.aspx");
        }

        isIncomeSecureConnection = CheckBoxSecureConnection.Checked;
        isOutgoingSecureConnection = CheckBoxOutgoingSecure.Checked;
        isOutgoingWithAuthentication = CheckBoxOutgoingAuthentication.Checked;

        loginId = TextBoxLoginID.Text;
        outgoingServer = TextBoxOutgoingServer.Text;

        //These informations are going to save

        AccountSettings.AccountInfo acc_info = new AccountSettings.AccountInfo();
        acc_info.EmailAddress = EncryptDescript.CriptDescript(emailAddress);
        acc_info.Password = EncryptDescript.CriptDescript(password);
        acc_info.DisplayName = displayName;
        acc_info.IncomingMailServer = incomingMailServer;
        acc_info.OutgoingServer = outgoingServer;
        acc_info.LoginId = loginId;
        acc_info.PortIncomingServer = portIncomingServer;
        acc_info.PortOutgoingServer = portOutgoingServer;
        acc_info.IncomingNameMailServer = incomingNameMailServer;
        acc_info.IsIncomeSecureConnection = isIncomeSecureConnection;
        acc_info.IsOutgoingSecureConnection = isOutgoingSecureConnection;
        acc_info.IsOutgoingWithAuthentication = isOutgoingWithAuthentication;
        acc_info.PortIncomingChecked = CheckBoxPortIncoming.Checked;
        acc_info.PortOutgoingChecked = CheckBoxPortOutgoing.Checked;

        Facade f = Facade.GetInstance();
        f.setAccountInfo(acc_info);
        f.SaveAccountSettings();

        try
        {
            f.Disconnect();
        }
        catch (Exception)
        {
            Facade.GetInstance().deleteAccountSettings();
            Session["ErrorMessage"] = "Could not be disconnected with imcoming server";
            Response.Redirect("~/ErrorPage.aspx");
        }
        try
        {
            f.Connect();
        }
        catch(Exception)
        {
            Facade.GetInstance().deleteAccountSettings();
            Session["ErrorMessage"] = "Could not be connected with imcoming server, review the account settings";
            //how the settings is not valid set null
            Facade.GetInstance().AccSettings = null;
            Response.Redirect("~/ErrorPage.aspx");
        }

        Session["SucessMessage"] = "Account Settings updated with success!";
        Response.Redirect("~/SucessPage.aspx");
    }

    private bool ImcomingWasChanged(int portIncomingServer)
    {
        AccountSettings.AccountInfo acc_info = Facade.GetInstance().GetDefaultAccountInfo();
        if (acc_info == null) return true;

        return (acc_info.PortIncomingServer != portIncomingServer);
    }

    /// <summary>
    /// Event fired when occurs a change in checkbox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckBoxPortIncoming_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBoxPortIncoming.Checked)
        {
            TextBoxPortIncoming.Enabled = true;
        } else {
            TextBoxPortIncoming.Enabled = false;
        }
    }

    /// <summary>
    /// Event fired when occurs a change in checkbox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckBoxPortOutgoing_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBoxPortOutgoing.Checked)
        {
            TextBoxPortOutgoing.Enabled = true;
        }
        else
        {
            TextBoxPortOutgoing.Enabled = false;
        }
    }

    #endregion
}
