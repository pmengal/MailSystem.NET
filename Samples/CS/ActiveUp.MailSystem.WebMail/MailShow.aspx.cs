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
using System.Collections.Generic;
using ActiveUp.Net.Mail;
using System.Text;
using System.IO;
using System.Threading;
using System.Globalization;

/// <summary>
/// This class is responsabile to display an email that is in inbox
/// </summary>

public partial class MailShow : System.Web.UI.Page
{

    /// <summary>
    /// MailShow load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
            //The message id passed through the url
            //Request.Params.Count
            if (((string)Request["type"]) == Constants.INBOX_QUERY)
            {
                int id = Convert.ToInt32(Request["id"]);
                Facade facade = Facade.GetInstance();
                Message msg = facade.getMessageByIndex(id);
                facade.CurrentMailShow = msg;

                if (msg != null) this.LoadMessage(msg);
            }
            else if ((string)Request["type"] == Constants.SENTMAIL_QUERY) {
            
                string id = (string)(Request["id"]);
                Facade facade = Facade.GetInstance();
                Message msg = facade.getSentMail(id);
                facade.CurrentMailShow = msg;
                if (msg != null) this.LoadMessage(msg);
            }
        }
    }

    #region Methods

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
    /// Load the message attributess
    /// </summary>
    /// <param name="msg">The Message object</param>
    private void LoadMessage(Message msg)
    {
        int i = 0;
        foreach (Address a in msg.To)
        {
            i++;
            ToLabel.Text += a.Email;
            if (msg.To.Count != i) ToLabel.Text += ", ";
        }

        FromLabel.Text = msg.From.Email;
        SubjectLabel.Text = msg.Subject;
        MailContent.InnerHtml = msg.BodyText.Text;

        Repeater1.DataSource = msg.Attachments;
        Repeater1.DataBind();
    }

    /// <summary>
    /// Method for Forwards the mail
    /// </summary>
    /// <param name="sender">The sender object</param>
    /// <param name="e">The event arguments</param>
    protected void ForwardButton_Click(object sender, EventArgs e)
    {
        Facade facade = Facade.GetInstance();
        Message msg = new Message();
        msg.From.Email = FromLabel.Text;
        //msg.Attachments = facade.CurrentMailShow.Attachments;

        if (!SubjectLabel.Text.Contains("Fwd: "))
        {
            msg.Subject = String.Concat("Fwd: ", SubjectLabel.Text);
        }
        else
        {
            msg.Subject = SubjectLabel.Text;
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(Environment.NewLine);
        sb.Append(Environment.NewLine);
        sb.Append("---------- Forwarded message ----------");
        sb.Append(Environment.NewLine);
        sb.Append("From: ");
        sb.Append(msg.From.Email);
        sb.Append(Environment.NewLine);
        sb.Append("Date: ");
        sb.Append(facade.CurrentMailShow.Date.ToString("d"));
        sb.Append(Environment.NewLine);
        sb.Append("Subject: ");
        sb.Append(msg.Subject);
        sb.Append(Environment.NewLine);
        sb.Append("To: ");
        sb.Append(ToLabel.Text);

        int i = 0;
        foreach (Address add in msg.To)
        {
            i++;
            sb.Append(add.Email);
            if (msg.To.Count != i) sb.Append(", ");
        }

        sb.Append(Environment.NewLine);
        sb.Append(Environment.NewLine);
        sb.Append(MailContent.InnerHtml);
        sb.Append(Environment.NewLine);
        msg.BodyText.Text = sb.ToString();

        //This session will be accessed in Composer.aspx
        Session["MessageReplyForward"] = msg;
        Response.Redirect("~/Composer.aspx");
    }

    /// <summary>
    /// Method for replys the mail to who sent it
    /// </summary>
    /// <param name="sender">The sender object</param>
    /// <param name="e">The event arguments</param>
    protected void ReplyButton_Click(object sender, EventArgs e)
    {
        Message msg = new Message();
        msg.To.Add(FromLabel.Text);
        Facade facade = Facade.GetInstance();
        //msg.Attachments = facade.CurrentMailShow.Attachments;

        if (!SubjectLabel.Text.Contains("Re: "))
        {
            msg.Subject = String.Concat("Re: ", SubjectLabel.Text);
        }
        else
        {
            msg.Subject = SubjectLabel.Text;
        }

        StringBuilder sb = new StringBuilder();
        sb.Append(Environment.NewLine);
        sb.Append(Environment.NewLine);
        sb.Append("On ");
        sb.Append(facade.CurrentMailShow.Date.ToString("d"));
        sb.Append(", ");
        sb.Append(FromLabel.Text);
        sb.Append(" wrote:");
        sb.Append(Environment.NewLine);
        sb.Append(MailContent.InnerHtml);

        //This session will be accessed in Composer.aspx
        msg.BodyText.Text = sb.ToString();
        Session["MessageReplyForward"] = msg;
        Response.Redirect("~/Composer.aspx");
    }

    /// <summary>
    /// Method for replys the mail to all
    /// </summary>
    /// <param name="sender">The sender object</param>
    /// <param name="e">The event arguments</param>
    protected void ReplyAllButton_Click(object sender, EventArgs e)
    {
        Facade facade = Facade.GetInstance();

        Message msg = new Message();
        msg.To.Add(FromLabel.Text);        
        msg.Cc.Add(ToLabel.Text);
        
        if (!SubjectLabel.Text.Contains("Re: "))
        {
            msg.Subject = String.Concat("Re: ", SubjectLabel.Text);
        }
        else
        {
            msg.Subject = SubjectLabel.Text;
        }

        StringBuilder sb = new StringBuilder();
        sb.Append(Environment.NewLine);
        sb.Append(Environment.NewLine);
        sb.Append("On ");
        sb.Append(facade.CurrentMailShow.Date.ToString("d"));
        sb.Append(", ");
        sb.Append(FromLabel.Text);
        sb.Append(" wrote:");
        sb.Append(Environment.NewLine);
        sb.Append(MailContent.InnerHtml);

        //This session will be accessed in Composer.aspx
        msg.BodyText.Text = sb.ToString();
        Session["MessageReplyForward"] = msg;
        Response.Redirect("~/Composer.aspx");

    }

    /// <summary>
    /// Event that will save the email attachment
    /// </summary>
    /// <param name="source">The source object</param>
    /// <param name="e">The repeat command event arguments</param>
    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string fileName = (string)e.CommandArgument;

        if (e.CommandName.Equals("Download"))
        {
            Facade facade = Facade.GetInstance();
            Message msg = facade.CurrentMailShow;
            
            for (int i = 0; i < msg.Attachments.Count; i++)
            {
                if (msg.Attachments[i].Filename == fileName)
                {
                    msg.Attachments[i].StoreToFile(Path.Combine(Path.GetTempPath(), fileName));
                }
            }

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            Response.WriteFile(Path.Combine(Path.GetTempPath(), fileName));
            Response.Flush();
            Response.Close();            
            Response.End();
        }
    }
    #endregion
}

