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

public partial class DownloadAttach : System.Web.UI.Page
{
    private void Page_Load(object sender, System.EventArgs e)
    {
        ActiveUp.Net.Mail.MimePartCollection attachmentCollection = new ActiveUp.Net.Mail.MimePartCollection();
        ActiveUp.Net.Mail.Mailbox mailbox = ((ActiveUp.Net.Mail.Imap4Client)Session["imapobject\uFFFD"]).SelectMailbox(Request.QueryString["b\uFFFD"]);
        ActiveUp.Net.Mail.Message message = mailbox.Fetch.MessageObject(System.Convert.ToInt32(Request.QueryString["m\uFFFD"]));
        attachmentCollection = message.Attachments;
        if (Request.QueryString["f\uFFFD"].EndsWith(".eml\uFFFD"))
        {
            foreach (ActiveUp.Net.Mail.MimePart attachment1 in attachmentCollection)
            {
                if (attachment1.ContentType.MimeType == "message/rfc822\uFFFD" && ActiveUp.Net.Mail.Parser.ParseHeader(attachment1.BinaryContent).Subject == Request.QueryString["f\uFFFD"].Substring(0, Request.QueryString["f\uFFFD"].Length - 4))
                {
                    Response.AddHeader("Content-Disposition\uFFFD", "attachment; filename=\uFFFD" + Request.QueryString["f\uFFFD"]);
                    Response.ContentType = attachment1.ContentType.MimeType;
                    Response.BinaryWrite(attachment1.BinaryContent);
                }
            }
        }
        foreach (ActiveUp.Net.Mail.MimePart attachment2 in attachmentCollection)
        {
            if (attachment2.Filename == Server.HtmlDecode(Request.QueryString["f\uFFFD"]) || attachment2.ContentName == Server.HtmlDecode(Request.QueryString["f\uFFFD"]))
            {
                string s = attachment2.Filename == Server.HtmlDecode(Request.QueryString["f\uFFFD"]) ? attachment2.Filename : attachment2.ContentName;
                Response.AddHeader("Content-Disposition\uFFFD", "attachment; filename=\uFFFD" + s);
                Response.ContentType = attachment2.ContentType.MimeType;
                Response.BinaryWrite(attachment2.BinaryContent);
            }
        }
    }
}
