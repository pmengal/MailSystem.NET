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
using ActiveUp.Net.Mail;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Globalization;

public partial class Composer : System.Web.UI.Page
{
    //Facade attribute
    private Facade facade;

    /// <summary>
    /// Composer page load
    /// </summary>
    /// <param name="sender">the sender object</param>
    /// <param name="e">the event arguments</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!File.Exists(Constants.ACCOUNT_FILE_NAME_SETTINGS)) Response.Redirect("~/Settings.aspx?permit=false");
                
            ErrorLabel.Visible = false;
            Label5.Visible = false;
            FileUpload1.Visible = true;
            if (!Page.IsPostBack)
            {
                this.facade = Facade.GetInstance();
                Message msg = (Message)Session["MessageReplyForward"];
                Session["MessageReplyForward"] = null;

                if (msg != null)
                {
                    LoadMessageReSend(msg);
                }
            }
        }

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
    /// Load message in forward, reply, and reoly all
    /// </summary>
    /// <param name="msg">A message</param>
    private void LoadMessageReSend(Message msg)
    {
        FileUpload1.Visible = false;
        int i = 0;
        string mail = "";
        foreach (Address add in msg.To)
        {
            i++;
            mail += add.Email;
            if (msg.To.Count != i) mail += ", ";
        }
        ToTextBox.Text = mail;

        SubjectTextBox.Text = msg.Subject;
        BodyTextBox.Text = msg.BodyText.Text;

        int j = 0;
        mail = "";
        foreach (Address add in msg.Cc)
        {
            j++;
            mail += add.Email;
            if (msg.Cc.Count != j) mail += ", ";
        }
        CCTextBox.Text = mail;        
    }

    /// <summary>
    /// Send the the current email
    /// </summary>
    public void SendMail()
    {
        try
        {
            ActiveUp.Net.Mail.Message message = new ActiveUp.Net.Mail.Message();

            //ak att em bytes com arquivo do tutorial!!!!    
            if (FileUpload1.HasFile)
            {
                message.Attachments.Add(FileUpload1.PostedFile.FileName, true);
            }

            message.Subject = SubjectTextBox.Text;
            bool val1 = this.validateEmailsTo(ToTextBox.Text, message);
            bool val2 = this.validateEmailsCc(CCTextBox.Text, message);
            message.Date = DateTime.Now;

            if (!(val1 || val2)) throw new Exception();

            message.BodyText.Text = BodyTextBox.Text;
            Facade.GetInstance().sendMail(message);

            // save message for loading in SentMail.aspx

            this.saveSentMail(message);           

            //Facade.GetInstance().
        }
        catch (Exception)
        {
            ErrorLabel.Visible = true;
            ErrorLabel.Text = "One or more fields are bad formated";
        }        

        Label5.Visible = true;

    }

    private void saveSentMail(Message message)
    {
        try
        {

            string path = Path.Combine(Path.GetTempPath(), Constants.SENT_MAILS_FOLDER);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            message.StoreToFile(Path.Combine(path, Guid.NewGuid().ToString()));
        }
        catch (Exception)
        {
            Session["ErrorMessage"] = "The message could not save into SentMail";
            Response.Redirect("~/ErrorPage.aspx");
        }
    }

    /// <summary>
    /// Validate or not the "To" email field
    /// </summary>
    /// <param name="mails">The mail object</param>
    /// <param name="message">The message object</param>
    /// <returns>A boolean, if true, the "To" was validated</returns>

    private bool validateEmailsTo(string mails, Message message)
    {
        bool ret = true;
        mails = mails.Trim();
        string[] toArray = mails.Split(new char[] { ',', ';' });
        string pattern = "\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

        for (int i = 0; i < toArray.Length; i++)
        {
            if (toArray[i] != String.Empty)
            {
                toArray[i] = toArray[i].Trim();
                if (Regex.IsMatch(toArray[i], pattern))
                {
                    message.To.Add(toArray[i]);
                }
                else
                {
                    return false;
                }
            }
        }
        return ret;
    }

    /// <summary>
    /// Validate or not the "Cc" email field
    /// </summary>
    /// <param name="mails">The mails object</param>
    /// <param name="message">The message object</param>
    /// <returns>A boolean, if true, the "To" was validated</returns>

    private bool validateEmailsCc(string mails, Message message)
    {
        bool ret = true;
        mails = mails.Trim();
        string[] ccArray = mails.Split(new char[] { ',', ';' });
        //regular expression for email
        string pattern = "\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

        for (int i = 0; i < ccArray.Length; i++)
        {
            if (ccArray[i] != String.Empty)
            {
                if (Regex.IsMatch(ccArray[i], pattern))
                {
                    message.Cc.Add(ccArray[i]);
                }
                else
                {
                    return false;
                }
            }
        }
        return ret;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        this.SendMail();
    }
}
