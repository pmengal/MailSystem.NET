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

public partial class MessageDisplay : System.Web.UI.UserControl
{
    
        //private Composer _boundComposer;
        //private MailboxContent _boundMailboxContent;
        //private TopNavigation _boundTopNav;
        //private FoldersTree _boundTree;
        
    //public System.Web.UI.HtmlControls.HtmlInputHidden iCurrent;
        //public System.Web.UI.HtmlControls.HtmlInputHidden iMailboxName;
        //public System.Web.UI.HtmlControls.HtmlInputHidden iServer;
        //public System.Web.UI.HtmlControls.HtmlInputHidden iUsername;
        //public System.Web.UI.WebControls.Label lAttributes;
        //public System.Web.UI.WebControls.LinkButton lBack;
        //public System.Web.UI.WebControls.Label lBody;
        //public System.Web.UI.WebControls.Label lCc;
        //public System.Web.UI.WebControls.Literal lCss;
        //public System.Web.UI.WebControls.Label lDate;
        //public System.Web.UI.WebControls.Label lFrom;
        //public System.Web.UI.WebControls.Literal lHeaders;
        //public System.Web.UI.WebControls.Label lNavigation;
        //public System.Web.UI.WebControls.LinkButton lNext;
        //public System.Web.UI.WebControls.LinkButton lPrevious;
        //public System.Web.UI.WebControls.Label lSubject;
        //public System.Web.UI.WebControls.Label lTo;
        //public System.Web.UI.WebControls.Repeater rAttach;

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

        public void Delete(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            int i;

            try
            {
                ActiveUp.Net.Mail.Mailbox mailbox = ((ActiveUp.Net.Mail.Imap4Client)Session["imapobject\uFFFD"]).SelectMailbox(MailboxName);
                EnvelopeCollection envelopeCollection = EnvelopeCollection.Load(Page.Server.MapPath(Application["writedirectory\uFFFD"].ToString() + Session["login\uFFFD"] + "_\uFFFD" + Server.UrlEncode(mailbox.Name) + ".xml\uFFFD"));
                mailbox.DeleteMessage(MessageId, false);
                mailbox.SourceClient.Expunge();
                envelopeCollection.RemoveAt(envelopeCollection.Count - MessageId);
                for (i = 0; i < (envelopeCollection.Count - MessageId); i++)
                {
                    envelopeCollection[i].Id--;
                }
                envelopeCollection.Save(Page.Server.MapPath(Application["writedirectory\uFFFD"].ToString() + Session["login\uFFFD"] + "_\uFFFD" + Server.UrlEncode(mailbox.Name) + ".xml\uFFFD"));
                //BoundMailboxContent.MailboxName = mailbox.Name;
                //BoundMailboxContent.LoadMailbox(envelopeCollection, true);
            }
            catch (System.Exception e1)
            {
                Page.RegisterStartupScript("ShowError\uFFFD", "<script>ShowErrorDialog('\uFFFD" + ((Language)Session["language\uFFFD"]).Words[85].ToString() + "','\uFFFD" + System.Text.RegularExpressions.Regex.Escape(e1.Message + e1.StackTrace).Replace("'\uFFFD", "\\'\uFFFD") + "');</script>\uFFFD");
            }
        }

        public void Forward(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            //BoundComposer.LoadForward(ImapServer, Username, MailboxName, MessageId, true);
        }

        public void LoadMessage(string server, string username, string mailboxName, int messageId, bool showThisHideBounds)
        {
            // trial
        }

        private void LoadMessage(WebMailMessage message)
        {
            // trial
        }

        public void Mark(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            // trial
        }

        public void MarkAsUnread(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            try
            {
                ActiveUp.Net.Mail.Mailbox mailbox = ((ActiveUp.Net.Mail.Imap4Client)Session["imapobject\uFFFD"]).SelectMailbox(MailboxName);
                ActiveUp.Net.Mail.FlagCollection flagCollection = new ActiveUp.Net.Mail.FlagCollection();
                flagCollection.Add("Seen\uFFFD");
                EnvelopeCollection envelopeCollection = EnvelopeCollection.Load(Page.Server.MapPath(Application["writedirectory\uFFFD"].ToString() + Session["login\uFFFD"] + "_\uFFFD" + Server.UrlEncode(mailbox.Name) + ".xml\uFFFD"));
                if (mailbox.Fetch.Flags(MessageId).Merged.IndexOf("\\seen\uFFFD") != -1)
                {
                    mailbox.RemoveFlagsSilent(MessageId, flagCollection);
                    envelopeCollection[envelopeCollection.Count - MessageId].Read = false;
                }
                else
                {
                    mailbox.AddFlagsSilent(MessageId, flagCollection);
                    envelopeCollection[envelopeCollection.Count - MessageId].Read = true;
                }
                envelopeCollection.Save(Page.Server.MapPath(Application["writedirectory\uFFFD"].ToString() + Session["login\uFFFD"] + "_\uFFFD" + Server.UrlEncode(mailbox.Name) + ".xml\uFFFD"));
                //BoundMailboxContent.MailboxName = mailbox.Name;
                //BoundMailboxContent.LoadMailbox(envelopeCollection, true);
            }
            catch (System.Exception e1)
            {
                Page.RegisterStartupScript("ShowError\uFFFD", "<script>ShowErrorDialog('\uFFFD" + ((Language)Session["language\uFFFD"]).Words[94].ToString() + "','\uFFFD" + System.Text.RegularExpressions.Regex.Escape(e1.Message + e1.StackTrace).Replace("'\uFFFD", "\\'\uFFFD") + "');</script>\uFFFD");
            }
        }

        public void Move(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            try
            {
                ActiveUp.Net.Mail.Mailbox mailbox = ((ActiveUp.Net.Mail.Imap4Client)Session["imapobject\uFFFD"]).SelectMailbox(MailboxName);
               // if (!System.IO.File.Exists(Page.Server.MapPath(Application["writedirectory\uFFFD"].ToString() + Session["login\uFFFD"] + "_\uFFFD" + Server.UrlEncode(this._boundTopNav.DBoxesItem) + ".xml\uFFFD")))
                    //this._boundMailboxContent.Cache(this._boundTopNav.DBoxesItem);
                EnvelopeCollection envelopeCollection1 = EnvelopeCollection.Load(Page.Server.MapPath(Application["writedirectory\uFFFD"].ToString() + Session["login\uFFFD"] + "_\uFFFD" + Server.UrlEncode(mailbox.Name) + ".xml\uFFFD"));
                //EnvelopeCollection envelopeCollection2 = EnvelopeCollection.Load(Page.Server.MapPath(Application["writedirectory\uFFFD"].ToString() + Session["login\uFFFD"] + "_\uFFFD" + Server.UrlEncode(this._boundTopNav.DBoxesItem + ".xml\uFFFD")));
                //mailbox.CopyMessage(MessageId, this._boundTopNav.DBoxesItem);
                Envelope envelope = new Envelope(envelopeCollection1[envelopeCollection1.Count - MessageId]);
                //envelope.Mailbox = this._boundTopNav.DBoxesItem;
                //envelope.Id = envelopeCollection2.Count + 1;
                //if (envelopeCollection2.Count != 0)
                  //  envelopeCollection2.Insert(envelope, 0);
                //else
                 //   envelopeCollection2.Add(envelope);
               // envelopeCollection2.Save(Page.Server.MapPath(Application["writedirectory\uFFFD"].ToString() + Session["login\uFFFD"] + "_\uFFFD" + Server.UrlEncode(this._boundTopNav.DBoxesItem) + ".xml\uFFFD"));
                LoadMessage((string)Application["server\uFFFD"], (string)Application["user\uFFFD"], mailbox.Name, MessageId, true);
            }
            catch (System.Exception e1)
            {
                Page.RegisterStartupScript("ShowError\uFFFD", "<script>ShowErrorDialog('\uFFFD" + ((Language)Session["language\uFFFD"]).Words[87].ToString() + "','\uFFFD" + System.Text.RegularExpressions.Regex.Escape(e1.Message + e1.StackTrace).Replace("'\uFFFD", "\\'\uFFFD") + "');</script>\uFFFD");
            }
        }

        public void Navigate(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            // trial
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            if ((Session["imapobject\uFFFD"] != null) && Page.User.Identity.IsAuthenticated)
                lCss.Text = "<style>\uFFFD" + System.IO.File.OpenText(Page.Server.MapPath("WebMessaging.css\uFFFD")).ReadToEnd() + "</style>\uFFFD";
        }

        public void Reply(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            // trial
        }

        public void SetLanguage(string code)
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                if ((Controls[i].GetType() == typeof(System.Web.UI.WebControls.Label)) && Controls[i].ID.StartsWith("lt\uFFFD"))
                    //  TODO: review the commented line, it was replaced
                    // ((System.Web.UI.WebControls.Label)Controls[i]).Text = ((Language)Session["language\uFFFD"]).Words[System.Convert.ToInt32(Controls[i].ID.Replace("lt\uFFFD", "\uFFFD"))].ToString();
                    ((System.Web.UI.WebControls.Label)Controls[i]).Text = ((Language)Session["language\uFFFD"]).Words[System.Convert.ToInt32(Controls[i].ID.Replace("lt", ""))].ToString();
            }
            lBack.Text = "<img src=\"icons/folders_off.gif\" name=\"back\" alt=\"\" style=\"width:16px;height:16px;\" align=\"absmiddle\" />&nbsp;\uFFFD" + ((Language)Session["language\uFFFD"]).Words[34].ToString();
            lPrevious.Text = " - <img src=\"icons/previous_off.gif\" name=\"previous\" alt=\"\" style=\"width:16px;height:16px;\" align=\"absmiddle\" />&nbsp;\uFFFD" + ((Language)Session["language\uFFFD"]).Words[36].ToString();
            lNext.Text = " - <img src=\"icons/next_off.gif\" name=\"next\" alt=\"\" style=\"width:16px;height:16px;\" align=\"absmiddle\" />&nbsp;\uFFFD" + ((Language)Session["language\uFFFD"]).Words[35].ToString();
        }

        public void ShowThisHideBounds()
        {
            /*
            Visible = true;
            EnableViewState = true;
            BoundMailboxContent.Visible = false;
            BoundMailboxContent.EnableViewState = false;
            BoundComposer.Visible = false;
            BoundComposer.EnableViewState = false;
             * */
        }

        public void Zip(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            // trial
        }
}
