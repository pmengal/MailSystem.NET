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

public partial class Composer : System.Web.UI.UserControl
{

    public System.Web.UI.WebControls.CheckBox cSave = new System.Web.UI.WebControls.CheckBox();
    public System.Web.UI.WebControls.DropDownList dBoxes = new System.Web.UI.WebControls.DropDownList();
    public System.Web.UI.HtmlControls.HtmlInputHidden iAction = new System.Web.UI.HtmlControls.HtmlInputHidden();
    public System.Web.UI.HtmlControls.HtmlInputFile iAttach = new System.Web.UI.HtmlControls.HtmlInputFile();
    public System.Web.UI.WebControls.TextBox iBcc = new System.Web.UI.WebControls.TextBox();
    public ActiveUp.WebControls.Editor iBody = new ActiveUp.WebControls.Editor();
    public System.Web.UI.WebControls.TextBox iCc = new System.Web.UI.WebControls.TextBox();
    public System.Web.UI.HtmlControls.HtmlInputHidden iCurrent = new System.Web.UI.HtmlControls.HtmlInputHidden();
    public System.Web.UI.WebControls.TextBox iFromEmail = new System.Web.UI.WebControls.TextBox();
    public System.Web.UI.WebControls.TextBox iFromName = new System.Web.UI.WebControls.TextBox();
    public System.Web.UI.HtmlControls.HtmlInputFile iImage = new System.Web.UI.HtmlControls.HtmlInputFile();
    public System.Web.UI.HtmlControls.HtmlInputHidden iMailboxName = new System.Web.UI.HtmlControls.HtmlInputHidden();
    public System.Web.UI.HtmlControls.HtmlInputHidden iOriginal = new System.Web.UI.HtmlControls.HtmlInputHidden();
    public System.Web.UI.WebControls.TextBox iReplyTo = new System.Web.UI.WebControls.TextBox();
    public System.Web.UI.HtmlControls.HtmlInputHidden iServer = new System.Web.UI.HtmlControls.HtmlInputHidden();
    public System.Web.UI.HtmlControls.HtmlInputHidden iSession = new System.Web.UI.HtmlControls.HtmlInputHidden();
    public System.Web.UI.WebControls.TextBox iSubject = new System.Web.UI.WebControls.TextBox();
    public System.Web.UI.WebControls.TextBox iTo = new System.Web.UI.WebControls.TextBox();
    public System.Web.UI.HtmlControls.HtmlInputHidden iUsername = new System.Web.UI.HtmlControls.HtmlInputHidden();
    public System.Web.UI.WebControls.LinkButton lAttach = new System.Web.UI.WebControls.LinkButton();
    public System.Web.UI.WebControls.LinkButton lCompose = new System.Web.UI.WebControls.LinkButton();
    public System.Web.UI.WebControls.Label lConfirm = new System.Web.UI.WebControls.Label();
    public System.Web.UI.WebControls.LinkButton lInsertImage = new System.Web.UI.WebControls.LinkButton();
    public System.Web.UI.WebControls.Literal lScript = new System.Web.UI.WebControls.Literal();
    public System.Web.UI.WebControls.Panel pConfirm = new System.Web.UI.WebControls.Panel();
    public System.Web.UI.WebControls.Panel pForm = new System.Web.UI.WebControls.Panel();
    public System.Web.UI.WebControls.Repeater rAttach = new System.Web.UI.WebControls.Repeater();


    public string ImapServer
    {
        get
        {
            return iServer.Value;
        }
        set
        {
            iServer.Value = value;
        }
    }

    public string MailboxName
    {
        get
        {
            return iMailboxName.Value;
        }
        set
        {
            iMailboxName.Value = value;
        }
    }

    public int MessageId
    {
        get
        {
            return System.Convert.ToInt32(iCurrent.Value);
        }
        set
        {
            iCurrent.Value = value.ToString();
        }
    }

    public int OriginalMessageId
    {
        get
        {
            return System.Convert.ToInt32(iOriginal.Value);
        }
        set
        {
            iOriginal.Value = value.ToString();
        }
    }

    public string Username
    {
        get
        {
            return iUsername.Value;
        }
        set
        {
            iUsername.Value = value;
        }
    }

    public void Attach(object sender, System.Web.UI.WebControls.CommandEventArgs e)
    {
        // trial
    }

    public void ComposeAgain(object sender, System.Web.UI.WebControls.CommandEventArgs e)
    {
        // trial
    }

    public void InsertImage(object sender, System.Web.UI.WebControls.CommandEventArgs e)
    {
        try
        {
            if ((iImage.PostedFile != null) && (iImage.PostedFile.ContentLength > 0))
            {
                iImage.PostedFile.SaveAs(Page.Server.MapPath(Application["writedirectory\uFFFD"].ToString() + Session.SessionID + "_Image_\uFFFD" + iImage.PostedFile.FileName.Substring(iImage.PostedFile.FileName.LastIndexOf("\\\uFFFD") + 1)));
                iBody.Text = "<img src=\"temp/\uFFFD" + Session.SessionID + "_Image_\uFFFD" + iImage.PostedFile.FileName.Substring(iImage.PostedFile.FileName.LastIndexOf('\\') + 1) + "\" />\uFFFD" + iBody.Text;
                LoadPage(true, false);
            }
        }
        catch (System.Exception e1)
        {
            Page.RegisterStartupScript("ShowError\uFFFD", "<script>ShowErrorDialog('\uFFFD" + ((Language)Session["language\uFFFD"]).Words[80].ToString() + "','\uFFFD" + System.Text.RegularExpressions.Regex.Escape(e1.Message + e1.StackTrace).Replace("'\uFFFD", "\\'\uFFFD") + "');</script>\uFFFD");
        }
    }

    public void LoadForward(string server, string username, string mailboxName, int messageId, bool showThisHideBounds)
    {
        // trial
    }

    public void LoadList()
    {
        // trial
    }

    public void LoadPage(bool showThisHideBounds, bool emptyFields)
    {
        // trial
    }

    public void LoadParams(string to, string cc, string subject, bool showThisHideBounds)
    {
        iTo.Text = to;
        iCc.Text = cc;
        iSubject.Text = subject;
        iBody.Text = System.String.Empty;
        if (Request.Cookies["fromemail\uFFFD"] != null)
            iFromEmail.Text = Request.Cookies["fromemail\uFFFD"].Value;
        if (Request.Cookies["fromname\uFFFD"] != null)
            iFromName.Text = Request.Cookies["fromname\uFFFD"].Value;
        if (Request.Cookies["replyto\uFFFD"] != null)
            iReplyTo.Text = Request.Cookies["replyto\uFFFD"].Value;
        if ((Request.Cookies["folder\uFFFD"] != null) && (dBoxes.Items.FindByText(Request.Cookies["folder\uFFFD"].Value) != null))
            dBoxes.Items.FindByText(Request.Cookies["folder\uFFFD"].Value).Selected = true;
        pConfirm.Visible = false;

    }

    public void LoadReply(string server, string username, string mailboxName, int messageId, bool showThisHideBounds)
    {
        try
        {
            string[] sArr1 = System.IO.Directory.GetFiles(Page.Server.MapPath(Application["writedirectory\uFFFD"].ToString()));
            for (int i = 0; i < sArr1.Length; i++)
            {
                string s = sArr1[i];
                if (s.IndexOf(Session.SessionID) != -1)
                    System.IO.File.Delete(s);
            }
            Username = username;
            ImapServer = server;
            MessageId = messageId;
            MailboxName = mailboxName;

            iAction.Value = "r\uFFFD";
            ActiveUp.Net.Mail.Mailbox mailbox = ((ActiveUp.Net.Mail.Imap4Client)Session["imapobject\uFFFD"]).SelectMailbox(MailboxName);
            byte[] bArr = mailbox.Fetch.Message(MessageId);
            WebMailMessage message = new WebMailMessage();
            message.Parsed = ActiveUp.Net.Mail.Parser.ParseMessage(bArr);
            message.Subject = message.Parsed.Subject;
            message.From = message.Parsed.From;
            if (Request.Cookies["fromemail\uFFFD"] != null)
                iFromEmail.Text = Request.Cookies["fromemail\uFFFD"].Value;
            if (Request.Cookies["fromname\uFFFD"] != null)
                iFromName.Text = Request.Cookies["fromname\uFFFD"].Value;
            if (Request.Cookies["replyto\uFFFD"] != null)
                iReplyTo.Text = Request.Cookies["replyto\uFFFD"].Value;
            if ((Request.Cookies["folder\uFFFD"] != null) && (dBoxes.Items.FindByText(Request.Cookies["folder\uFFFD"].Value) != null))
                dBoxes.Items.FindByText(Request.Cookies["folder\uFFFD"].Value).Selected = true;
            foreach (ActiveUp.Net.Mail.MimePart embeddedObject in message.Parsed.EmbeddedObjects)
            {
                if (embeddedObject.ContentType.MimeType.IndexOf("text\uFFFD") == -1)
                {
                    embeddedObject.StoreToFile(Page.Server.MapPath(Application["writedirectory\uFFFD"].ToString() + Session.SessionID + "_Image_\uFFFD" + embeddedObject.ContentName));
                    message.Parsed.BodyHtml.Text = message.Parsed.BodyHtml.Text.Replace("cid:\uFFFD" + embeddedObject.ContentId, "http://\uFFFD" + Request.ServerVariables["HTTP_HOST\uFFFD"] + Request.ServerVariables["URL\uFFFD"].Substring(0, Request.ServerVariables["URL\uFFFD"].LastIndexOf("/\uFFFD") + 1) + "temp/\uFFFD" + Session.SessionID + "_Image_\uFFFD" + embeddedObject.ContentName);
                }
            }
            iCc.Text = System.String.Empty;
            iBcc.Text = System.String.Empty;
            cSave.Checked = false;
            iSubject.Text = message.Subject.StartsWith("Re:\uFFFD") ? message.Subject : "Re:\uFFFD" + message.Subject;
            iTo.Text = (message.Parsed.ReplyTo.Email != null) && message.Parsed.ReplyTo.Email != "\uFFFD" ? message.Parsed.ReplyTo.Email : message.From.Email;
            if ((message.Parsed.BodyHtml.Text != null) && (message.Parsed.BodyHtml.Text.Length > 0))
            {
                System.DateTime dateTime1 = message.Date;
                System.DateTime dateTime2 = message.Date;
                iBody.Text = "<br /><br /><hr />\uFFFD" + ((Language)Session["language\uFFFD"]).Words[30].ToString().Replace("$$SUBJECT$$\uFFFD", message.Subject).Replace("$$FROM$$\uFFFD", message.From.Link).Replace("$$DATE$$\uFFFD", dateTime1.Year > 1800 ? dateTime2.ToString("r\uFFFD", System.Globalization.DateTimeFormatInfo.CurrentInfo) : message.Parsed.DateString) + "<br /><hr />\uFFFD" + message.Parsed.BodyHtml.Text;
            }
            else if ((message.Parsed.BodyText.Text != null) && (message.Parsed.BodyText.Text.Length > 0))
            {
                System.DateTime dateTime3 = message.Date;
                System.DateTime dateTime4 = message.Date;
                iBody.Text = "<br /><br /><hr />\uFFFD" + ((Language)Session["language\uFFFD"]).Words[30].ToString().Replace("$$SUBJECT$$\uFFFD", message.Subject).Replace("$$FROM$$\uFFFD", message.From.Link).Replace("$$DATE$$\uFFFD", dateTime3.Year > 1800 ? dateTime4.ToString("r\uFFFD", System.Globalization.DateTimeFormatInfo.CurrentInfo) : message.Parsed.DateString) + "<br /><hr />\uFFFD" + message.Parsed.BodyText.Text;
            }
            pForm.Visible = true;
            pConfirm.Visible = false;
        }
        catch (System.Exception e)
        {
            Page.RegisterStartupScript("ShowError\uFFFD", "<script>ShowErrorDialog('\uFFFD" + ((Language)Session["language\uFFFD"]).Words[78].ToString() + "','\uFFFD" + System.Text.RegularExpressions.Regex.Escape(e.Message + e.StackTrace).Replace("'\uFFFD", "\\'\uFFFD") + "');</script>\uFFFD");
        }
    }

    private void Page_Load(object sender, System.EventArgs e)
    {
        //if ((Session["imapobject\uFFFD"] != null) && Page.User.Identity.IsAuthenticated)
        //{
        //    iBody.EnsureToolsCreated();
        //    iBody.EditorModeSelector = ActiveUp.WebControls.EditorModeSelectorType.None;
        //    Emoticons emoticons = new Emoticons();
        //    emoticons.ImagesDir = "icons/emoticons/\uFFFD";
        //    iBody.Toolbars[0].Tools.Add(emoticons);
        //    if (dBoxes.Items.Count < 1)
        //        LoadList();
        //}
    }

    public void RemoveAttach(object sender, System.Web.UI.WebControls.CommandEventArgs e)
    {
        // trial
    }

    public void Send(object sender, System.EventArgs e)
    {
        ActiveUp.Net.Mail.Message message = new ActiveUp.Net.Mail.Message();
        try
        {
            message.Subject = iSubject.Text;
            message.To = ActiveUp.Net.Mail.Parser.ParseAddresses(iTo.Text);
            message.Cc = ActiveUp.Net.Mail.Parser.ParseAddresses(iCc.Text);
            message.Bcc = ActiveUp.Net.Mail.Parser.ParseAddresses(iBcc.Text);
            if (iReplyTo.Text.Length > 0)
                message.ReplyTo = ActiveUp.Net.Mail.Parser.ParseAddresses(iReplyTo.Text)[0];
            string s1 = iBody.Text;
            string[] sArr1 = System.IO.Directory.GetFiles(Page.Server.MapPath(Application["writedirectory\uFFFD"].ToString()));
            for (int i1 = 0; i1 < sArr1.Length; i1++)
            {
                string s2 = sArr1[i1];
                if ((s2.IndexOf(Session.SessionID + "_Image_\uFFFD") != -1) && (s1.IndexOf(s2.Substring(s2.IndexOf(Session.SessionID))) != -1))
                {
                    ActiveUp.Net.Mail.MimePart embeddedObject1 = new ActiveUp.Net.Mail.MimePart(s2, true, string.Empty);
                    embeddedObject1.ContentName = s2.Substring(s2.IndexOf("_Image_\uFFFD") + 7);
                    message.EmbeddedObjects.Add(embeddedObject1);
                    s1 = s1.Replace("http://\uFFFD" + Request.ServerVariables["HTTP_HOST\uFFFD"] + Request.ServerVariables["URL\uFFFD"].Substring(0, Request.ServerVariables["URL\uFFFD"].LastIndexOf("/\uFFFD") + 1) + "temp\uFFFD" + s2.Substring(s2.LastIndexOf('\\')).Replace("\\\uFFFD", "/\uFFFD"), "cid:\uFFFD" + embeddedObject1.ContentId);
                    s1 = s1.Replace("temp\uFFFD" + s2.Substring(s2.LastIndexOf('\\')).Replace("\\\uFFFD", "/\uFFFD"), "cid:\uFFFD" + embeddedObject1.ContentId);
                }
            }
            string[] sArr3 = System.IO.Directory.GetFiles(Server.MapPath("icons/emoticons/\uFFFD"));
            for (int i2 = 0; i2 < sArr3.Length; i2++)
            {
                string s3 = sArr3[i2];
                if (s1.IndexOf("icons/emoticons\uFFFD" + s3.Substring(s3.LastIndexOf("\\\uFFFD")).Replace("\\\uFFFD", "/\uFFFD")) != -1)
                {
                    ActiveUp.Net.Mail.MimePart embeddedObject2 = new ActiveUp.Net.Mail.MimePart(s3, true, string.Empty);
                    embeddedObject2.ContentName = s3.Substring(s3.LastIndexOf("\\\uFFFD") + 1);
                    message.EmbeddedObjects.Add(embeddedObject2);
                    s1 = s1.Replace("http://\uFFFD" + Request.ServerVariables["HTTP_HOST\uFFFD"] + Request.ServerVariables["URL\uFFFD"].Substring(0, Request.ServerVariables["URL\uFFFD"].LastIndexOf("/\uFFFD") + 1) + "icons/emoticons\uFFFD" + s3.Substring(s3.LastIndexOf('\\')).Replace("\\\uFFFD", "/\uFFFD"), "cid:\uFFFD" + embeddedObject2.ContentId);
                }
            }
            message.BodyHtml.Text = s1;
            message.BodyText.Text = iBody.TextStripped;
            //message.Headers.Add("x-sender-ip\uFFFD", Request.ServerVariables["REMOTE_ADDR\uFFFD"]);
            string[] sArr5 = System.IO.Directory.GetFiles(Page.Server.MapPath(Application["writedirectory\uFFFD"].ToString()));
            for (int i3 = 0; i3 < sArr5.Length; i3++)
            {
                string s4 = sArr5[i3];
                if (s4.IndexOf("\\temp\\\uFFFD" + Session.SessionID + "_Attach_\uFFFD") != -1)
                {
                    ActiveUp.Net.Mail.MimePart attachment = new ActiveUp.Net.Mail.MimePart(s4, true);
                    attachment.Filename = s4.Substring(s4.IndexOf("_Attach_\uFFFD") + 8);
                    attachment.ContentName = s4.Substring(s4.IndexOf("_Attach_\uFFFD") + 8);
                    message.Attachments.Add(attachment);
                }
            }
            message.From = new ActiveUp.Net.Mail.Address(iFromEmail.Text, iFromName.Text);
            if (((System.Web.UI.HtmlControls.HtmlInputButton)sender).ID == "iSubmit\uFFFD")
            {
                try
                {
                    message.Send((string)Application["smtpserver\uFFFD"], System.Convert.ToInt32(Application["smtpport\uFFFD"]), (string)Application["user\uFFFD"], (string)Application["password\uFFFD"], ActiveUp.Net.Mail.SaslMechanism.CramMd5);
                }
                catch
                {
                    try
                    {
                        message.Send((string)Application["smtpserver\uFFFD"], System.Convert.ToInt32(Application["smtpport\uFFFD"]), (string)Application["user\uFFFD"], (string)Application["password\uFFFD"], ActiveUp.Net.Mail.SaslMechanism.CramMd5);
                    }
                    catch
                    {
                        message.Send((string)Application["smtpserver\uFFFD"], System.Convert.ToInt32(Application["smtpport\uFFFD"]));
                    }
                }
                try
                {
                    if (iAction.Value == "r\uFFFD")
                    {
                        ActiveUp.Net.Mail.Mailbox mailbox1 = ((ActiveUp.Net.Mail.Imap4Client)Session["imapobject\uFFFD"]).SelectMailbox(MailboxName);
                        ActiveUp.Net.Mail.FlagCollection flagCollection = new ActiveUp.Net.Mail.FlagCollection();
                        flagCollection.Add("Answered\uFFFD");
                        if (mailbox1.Fetch.Flags(MessageId).Merged.ToLower().IndexOf("\\answered\uFFFD") == -1)
                            mailbox1.AddFlagsSilent(MessageId, flagCollection);
                    }
                    lConfirm.Text = ((Language)Session["language\uFFFD"]).Words[32].ToString() + " : <br /><br />\uFFFD" + message.To.Merged + message.Cc.Merged + message.Bcc.Merged.Replace(";\uFFFD", "<br />\uFFFD");
                    pForm.Visible = false;
                    pConfirm.Visible = true;
                    System.Web.HttpCookie httpCookie1 = new System.Web.HttpCookie("fromname\uFFFD", iFromName.Text);
                    System.Web.HttpCookie httpCookie2 = new System.Web.HttpCookie("fromemail\uFFFD", iFromEmail.Text);
                    System.Web.HttpCookie httpCookie3 = new System.Web.HttpCookie("replyto\uFFFD", iReplyTo.Text);
                    System.DateTime dateTime1 = System.DateTime.Now;
                    httpCookie1.Expires = dateTime1.AddMonths(2);
                    System.DateTime dateTime2 = System.DateTime.Now;
                    httpCookie2.Expires = dateTime2.AddMonths(2);
                    System.DateTime dateTime3 = System.DateTime.Now;
                    httpCookie3.Expires = dateTime3.AddMonths(2);
                    Response.Cookies.Add(httpCookie1);
                    Response.Cookies.Add(httpCookie2);
                    Response.Cookies.Add(httpCookie3);
                    if (cSave.Checked)
                    {
                        System.Web.HttpCookie httpCookie4 = new System.Web.HttpCookie("folder\uFFFD", dBoxes.SelectedItem.Text);
                        System.DateTime dateTime4 = System.DateTime.Now;
                        httpCookie4.Expires = dateTime4.AddMonths(2);
                        Response.Cookies.Add(httpCookie4);
                        ActiveUp.Net.Mail.Mailbox mailbox2 = ((ActiveUp.Net.Mail.Imap4Client)Session["imapobject\uFFFD"]).SelectMailbox(dBoxes.SelectedItem.Text);
                        mailbox2.Append(message.ToMimeString());
                    }
                    string[] sArr6 = System.IO.Directory.GetFiles(Page.Server.MapPath(Application["writedirectory\uFFFD"].ToString()));
                    for (int i4 = 0; i4 < sArr6.Length; i4++)
                    {
                        string s5 = sArr6[i4];
                        if (s5.IndexOf(Session.SessionID) != -1)
                            System.IO.File.Delete(s5);
                    }
                }
                catch (System.Exception e1)
                {
                    Page.RegisterStartupScript("ShowError\uFFFD", "<script>ShowErrorDialog('\uFFFD" + ((Language)Session["language\uFFFD"]).Words[83].ToString() + "','\uFFFD" + System.Text.RegularExpressions.Regex.Escape(e1.Message + e1.StackTrace).Replace("'\uFFFD", "\\'\uFFFD") + "');</script>\uFFFD");
                }
            }
            else
            {
                System.Web.HttpCookie httpCookie5 = new System.Web.HttpCookie("folder\uFFFD", dBoxes.SelectedItem.Text);
                System.DateTime dateTime5 = System.DateTime.Now;
                httpCookie5.Expires = dateTime5.AddMonths(2);
                Response.Cookies.Add(httpCookie5);
                ActiveUp.Net.Mail.Mailbox mailbox3 = ((ActiveUp.Net.Mail.Imap4Client)Session["imapobject\uFFFD"]).SelectMailbox(dBoxes.SelectedItem.Text);
                mailbox3.Append(message.ToMimeString());
            }
            string[] sArr8 = System.IO.Directory.GetFiles(Page.Server.MapPath(Application["writedirectory\uFFFD"].ToString()));
            for (int i5 = 0; i5 < sArr8.Length; i5++)
            {
                string s6 = sArr8[i5];
                if (s6.IndexOf(Session.SessionID) != -1)
                    System.IO.File.Delete(s6);
            }
        }
        catch (System.Exception e2)
        {
            Page.RegisterStartupScript("ShowError\uFFFD", "<script>ShowErrorDialog('\uFFFD" + ((Language)Session["language\uFFFD"]).Words[82].ToString() + "','\uFFFD" + System.Text.RegularExpressions.Regex.Escape(e2.Message + e2.StackTrace).Replace("'\uFFFD", "\\'\uFFFD") + "');</script>\uFFFD");
        }
    }

    public void SetLanguage(string code)
    {

        for (int i = 0; i < pForm.Controls.Count; i++)
        {
            if ((pForm.Controls[i].GetType() == typeof(System.Web.UI.WebControls.Label)) && pForm.Controls[i].ID.StartsWith("lt\uFFFD"))
                ((System.Web.UI.WebControls.Label)pForm.Controls[i]).Text = ((Language)Session["language"]).Words[System.Convert.ToInt32(pForm.Controls[i].ID.Replace("lt\uFFFD", "\uFFFD"))].ToString();
        }
        lAttach.Text = "<img src=\"icons/attach_off.gif\" style=\"width:16px;height:16px;\" align=\"absmiddle\" alt=\"\" name=\"attach\" />&nbsp;&nbsp;\uFFFD" + ((Language)Session["language"]).Words[27].ToString();
        lInsertImage.Text = "<img src=\"icons/pictures_off.gif\" style=\"width:16px;height:16px;\" align=\"absmiddle\" alt=\"\" name=\"pictures\" />&nbsp;&nbsp;\uFFFD" + ((Language)Session["language"]).Words[66].ToString();
        lCompose.Text = ((Language)Session["language\uFFFD"]).Words[33].ToString();
        System.Web.UI.HtmlControls.HtmlInputButton bb = (System.Web.UI.HtmlControls.HtmlInputButton)pForm.FindControl("iSubmit");

        // TODO: Review it
        //((System.Web.UI.HtmlControls.HtmlInputButton)pForm.FindControl("iSubmit")).Value = ((Language)Session["language"]).Words[26].ToString();
        //((System.Web.UI.HtmlControls.HtmlInputButton)pForm.FindControl("iSave")).Value = ((Language)Session["language"]).Words[16].ToString();

    }


    protected void TextBox4_TextChanged(object sender, EventArgs e)
    {

    }
}
