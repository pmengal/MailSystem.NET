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

public partial class MailboxContent : System.Web.UI.UserControl
{
        
        private string _orderBy;
        public System.Web.UI.WebControls.DropDownList dBoxes;
        //public System.Web.UI.WebControls.Repeater dMailbox;
        //public System.Web.UI.HtmlControls.HtmlInputHidden iCurrent;
        //public System.Web.UI.HtmlControls.HtmlInputHidden iMailboxName;
        //public System.Web.UI.HtmlControls.HtmlInputHidden iServer;
        //public System.Web.UI.HtmlControls.HtmlInputHidden iUsername;
        //public System.Web.UI.WebControls.Label lFolder;

        
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

        public string MoveTo
        {
            get
            {
                return iCurrent.Value;
            }
            set
            {
                iCurrent.Value = value;
            }
        }

        public string OrderBy
        {
            get
            {
                return _orderBy;
            }
            set
            {
                _orderBy = value;
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

        public string LFolderText
        {
            get
            {
                return lFolder.Text;
            }
            set
            {
                lFolder.Text = value;
            }
        }

        public Control DMailBoxControl
        {
            get
            {
                return dMailbox.Controls[0].FindControl("check\uFFFD");
            }
         }

        public MailboxContent()
        {
            _orderBy = "[date] desc\uFFFD";
        }

        public void Cache(string mailboxName)
        {
            try
            {
                ActiveUp.Net.Mail.Mailbox mailbox = ((ActiveUp.Net.Mail.Imap4Client)Session["imapobject\uFFFD"]).SelectMailbox(mailboxName);
                EnvelopeCollection envelopeCollection1 = new EnvelopeCollection();
                EnvelopeCollection envelopeCollection2 = new EnvelopeCollection();
                for (int i1 = 1; i1 <= mailbox.MessageCount; i1++)
                {
                    Envelope envelope = new Envelope();
                    string[] sArr2 = new string[] {
                                                    "grun\uFFFD", 
                                                    "subject\uFFFD", 
                                                    "from\uFFFD", 
                                                    "date\uFFFD" };
                    System.Collections.Specialized.NameValueCollection nameValueCollection = mailbox.Fetch.HeaderLinesPeek(i1, sArr2);
                    ActiveUp.Net.Mail.FlagCollection flagCollection = mailbox.Fetch.Flags(i1);
                    envelope.Read = flagCollection.Merged.IndexOf("\\seen\uFFFD") != -1;
                    envelope.Answered = flagCollection.Merged.IndexOf("\\answered\uFFFD") != -1;
                    envelope.Forwarded = flagCollection.Merged.IndexOf("\\forwarded\uFFFD") != -1;
                    envelope.Marked = flagCollection.Merged.IndexOf("\\flagged\uFFFD") != -1;
                    envelope.Subject = nameValueCollection["subject\uFFFD"];
                    envelope.From = ActiveUp.Net.Mail.Parser.ParseAddresses(nameValueCollection["from\uFFFD"])[0];
                    char[] chArr1 = new char[] { ' ' };
                    string[] sArr1 = nameValueCollection["date\uFFFD"].Split(chArr1);
                    char[] chArr2 = new char[] { ':' };
                    char[] chArr3 = new char[] { ':' };
                    char[] chArr4 = new char[] { ':' };
                    envelope.Date = new System.DateTime(System.Convert.ToInt32(sArr1[3]), GetMonth(sArr1[2]), System.Convert.ToInt32(sArr1[1]), System.Convert.ToInt32(sArr1[4].Split(chArr2)[0]), System.Convert.ToInt32(sArr1[4].Split(chArr3)[1]), System.Convert.ToInt32(sArr1[4].Split(chArr4)[2]));
                    envelope.Size = mailbox.Fetch.Size(i1);
                    envelope.Mailbox = mailbox.Name;
                    envelope.Id = i1;
                    envelopeCollection1.Add(envelope);
                }
                for (int i2 = envelopeCollection1.Count - 1; i2 >= 0; i2--)
                {
                    envelopeCollection2.Add(envelopeCollection1[i2]);
                }
                envelopeCollection2.Save(Page.Server.MapPath(Application["writedirectory\uFFFD"].ToString() + Session["login\uFFFD"] + "_\uFFFD" + Server.UrlEncode(mailbox.Name) + ".xml\uFFFD"));
            }
            catch (System.Exception e)
            {
                Page.RegisterStartupScript("ShowError\uFFFD", "<script>ShowErrorDialog('\uFFFD" + ((Language)Session["language\uFFFD"]).Words[93].ToString() + "','\uFFFD" + System.Text.RegularExpressions.Regex.Escape(e.Message + e.StackTrace).Replace("'\uFFFD", "\\'\uFFFD") + "');</script>\uFFFD");
            }
        }

        public void Delete(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            // trial
        }

        public void Forward(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            // trial
        }

        internal int GetMonth(string month)
        {
            int i;

            object obj = month;
            switch (month)
            {
                case "Jan\uFFFD":
                    i = 1;
                    break;

                case "Feb\uFFFD":
                    i = 2;
                    break;

                case "Mar\uFFFD":
                    i = 3;
                    break;

                case "Apr\uFFFD":
                    i = 4;
                    break;

                case "May\uFFFD":
                    i = 5;
                    break;

                case "Jun\uFFFD":
                    i = 6;
                    break;

                case "Jul\uFFFD":
                    i = 7;
                    break;

                case "Aug\uFFFD":
                    i = 8;
                    break;

                case "Sep\uFFFD":
                    i = 9;
                    break;

                case "Oct\uFFFD":
                    i = 10;
                    break;

                case "Nov\uFFFD":
                    i = 11;
                    break;

                case "Dec\uFFFD":
                    i = 12;
                    break;

                default:
                    i = -1;
                    break;
            }

            return i;
        }

        internal string GetSize(int input)
        {
            // trial
            return null;
        }

        public void LoadMailbox(string mailboxName, bool showThisHideBounds)
        {
            // trial
        }

        public void LoadMailbox(EnvelopeCollection envelopes, bool showThisHideBounds)
        {
            string[] sArr1;

            try
            {
                if (showThisHideBounds)
                    ShowThisHideBounds();
                Visible = true;
                EnableViewState = true;
                System.Data.DataTable dataTable = new System.Data.DataTable();
                dataTable.Columns.Add("Date\uFFFD");
                dataTable.Columns.Add("Size\uFFFD");
                foreach (Envelope envelope in envelopes)
                {
                    if (envelope.Read)
                    {
                        string[] sArr2 = new string[2];
                        System.DateTime dateTime1 = envelope.Date;
                        System.DateTime dateTime2 = envelope.Date;
                        sArr2[0] = dateTime1.Year > 1800 ? dateTime2.ToString("dddd dd MMMM yyyy HH:mm\uFFFD") : envelope.DateString;
                        sArr2[1] = GetSize(envelope.Size);
                        sArr1 = sArr2;
                    }
                    else
                    {
                        string[] sArr3 = new string[2];
                        System.DateTime dateTime3 = envelope.Date;
                        System.DateTime dateTime4 = envelope.Date;
                        sArr3[0] = "<b>\uFFFD" + (dateTime3.Year > 1800 ? dateTime4.ToString("dddd dd MMMM yyyy HH:mm\uFFFD") : envelope.DateString) + "</b>\uFFFD";
                        sArr3[1] = "<b>\uFFFD" + GetSize(envelope.Size) + "</b>\uFFFD";
                        sArr1 = sArr3;
                    }
                    dataTable.Rows.Add(sArr1);
                }
                dMailbox.DataSource = dataTable;
                dMailbox.DataBind();
                for (int i1 = 1; i1 < (dMailbox.Controls.Count - 1); i1++)
                {
                    int i3 = envelopes[i1 - 1].Id;
                    ((System.Web.UI.WebControls.LinkButton)dMailbox.Controls[i1].FindControl("lMSubject\uFFFD")).CommandArgument = envelopes[i1 - 1].Mailbox + "|\uFFFD" + i3.ToString();
                    if (envelopes[i1 - 1].Read)
                        ((System.Web.UI.WebControls.LinkButton)dMailbox.Controls[i1].FindControl("lMSubject\uFFFD")).Text = Server.HtmlEncode((envelopes[i1 - 1].Subject != null) && (envelopes[i1 - 1].Subject.Length > 50) ? envelopes[i1 - 1].Subject.Substring(0, 47) + "...\uFFFD" : (envelopes[i1 - 1].Subject != null) && (envelopes[i1 - 1].Subject.Length > 0) ? envelopes[i1 - 1].Subject : "(\uFFFD" + ((Language)Session["language\uFFFD"]).Words[71].ToString() + ")\uFFFD");
                    else
                        ((System.Web.UI.WebControls.LinkButton)dMailbox.Controls[i1].FindControl("lMSubject\uFFFD")).Text = "<b>\uFFFD" + Server.HtmlEncode((envelopes[i1 - 1].Subject != null) && (envelopes[i1 - 1].Subject.Length > 50) ? envelopes[i1 - 1].Subject.Substring(0, 47) + "...\uFFFD" : (envelopes[i1 - 1].Subject != null) && (envelopes[i1 - 1].Subject.Length > 0) ? envelopes[i1 - 1].Subject : "(\uFFFD" + ((Language)Session["language\uFFFD"]).Words[71].ToString() + ")\uFFFD") + "</b>\uFFFD";
                    ((System.Web.UI.WebControls.LinkButton)dMailbox.Controls[i1].FindControl("lMFrom\uFFFD")).CommandArgument = envelopes[i1 - 1].From.Email;
                    if (envelopes[i1 - 1].Read)
                        ((System.Web.UI.WebControls.LinkButton)dMailbox.Controls[i1].FindControl("lMFrom\uFFFD")).Text = Server.HtmlEncode((envelopes[i1 - 1].From.Name != null) && (envelopes[i1 - 1].From.Name.Length > 50) ? envelopes[i1 - 1].From.Name.Substring(0, 47) + "...\uFFFD" : (envelopes[i1 - 1].From.Name != null) && (envelopes[i1 - 1].From.Name.Length > 0) ? envelopes[i1 - 1].From.Name : envelopes[i1 - 1].From.Email);
                    else
                        ((System.Web.UI.WebControls.LinkButton)dMailbox.Controls[i1].FindControl("lMFrom\uFFFD")).Text = "<b>\uFFFD" + Server.HtmlEncode((envelopes[i1 - 1].From.Name != null) && (envelopes[i1 - 1].From.Name.Length > 50) ? envelopes[i1 - 1].From.Name.Substring(0, 47) + "...\uFFFD" : (envelopes[i1 - 1].From.Name != null) && (envelopes[i1 - 1].From.Name.Length > 0) ? envelopes[i1 - 1].From.Name : envelopes[i1 - 1].From.Email) + "</b>\uFFFD";
                    if (envelopes[i1 - 1].Answered)
                        ((System.Web.UI.WebControls.Image)dMailbox.Controls[i1].FindControl("fIcon\uFFFD")).ImageUrl = "icons/replied.gif\uFFFD";
                    else if (envelopes[i1 - 1].Forwarded)
                        ((System.Web.UI.WebControls.Image)dMailbox.Controls[i1].FindControl("fIcon\uFFFD")).ImageUrl = "icons/forwarded.gif\uFFFD";
                    else if (envelopes[i1 - 1].Read)
                        ((System.Web.UI.WebControls.Image)dMailbox.Controls[i1].FindControl("fIcon\uFFFD")).ImageUrl = "icons/read.gif\uFFFD";
                    else
                        ((System.Web.UI.WebControls.Image)dMailbox.Controls[i1].FindControl("fIcon\uFFFD")).ImageUrl = "icons/unread.gif\uFFFD";
                    if (envelopes[i1 - 1].Marked)
                        ((System.Web.UI.WebControls.Image)dMailbox.Controls[i1].FindControl("fMark\uFFFD")).ImageUrl = "icons/marked.gif\uFFFD";
                    else
                        ((System.Web.UI.WebControls.Image)dMailbox.Controls[i1].FindControl("fMark\uFFFD")).ImageUrl = "icons/unmarked.gif\uFFFD";
                }
                for (int i2 = 0; i2 < dMailbox.Controls[0].Controls.Count; i2++)
                {
                    if ((dMailbox.Controls[0].Controls[i2].GetType() == typeof(System.Web.UI.WebControls.Label)) && dMailbox.Controls[0].Controls[i2].ID.StartsWith("lt\uFFFD"))
                        ((System.Web.UI.WebControls.Label)dMailbox.Controls[0].Controls[i2]).Text = ((Language)Session["language\uFFFD"]).Words[System.Convert.ToInt32(dMailbox.Controls[0].Controls[i2].ID.Replace("lt\uFFFD", "\uFFFD"))].ToString();
                }
                lFolder.Text = MailboxName;
            }
            catch (System.Exception e)
            {
                Page.RegisterStartupScript("ShowError\uFFFD", "<script>ShowErrorDialog('\uFFFD" + ((Language)Session["language\uFFFD"]).Words[92].ToString() + "','\uFFFD" + System.Text.RegularExpressions.Regex.Escape(e.Message + e.StackTrace).Replace("'\uFFFD", "\\'\uFFFD") + "');</script>\uFFFD");
            }
        }

        public void Mark(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            // trial
        }

        public void MarkAsUnread(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            int i;

            try
            {
                ActiveUp.Net.Mail.Mailbox mailbox = ((ActiveUp.Net.Mail.Imap4Client)Session["imapobject\uFFFD"]).SelectMailbox(MailboxName);
                ActiveUp.Net.Mail.FlagCollection flagCollection = new ActiveUp.Net.Mail.FlagCollection();
                flagCollection.Add("Seen\uFFFD");
                EnvelopeCollection envelopeCollection = EnvelopeCollection.Load(Page.Server.MapPath(Application["writedirectory\uFFFD"].ToString() + Session["login\uFFFD"] + "_\uFFFD" + Server.UrlEncode(mailbox.Name) + ".xml\uFFFD"));
                for (i = 1; i < (dMailbox.Controls.Count - 1); i++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)dMailbox.Controls[i].FindControl("select\uFFFD")).Checked)
                    {
                        if (mailbox.Fetch.Flags(dMailbox.Controls.Count - 1 - i).Merged.IndexOf("\\seen\uFFFD") != -1)
                        {
                            mailbox.RemoveFlagsSilent(dMailbox.Controls.Count - 1 - i, flagCollection);
                            envelopeCollection[envelopeCollection.Count - (dMailbox.Controls.Count - 1 - i)].Read = false;
                            continue;
                        }
                        mailbox.AddFlagsSilent(dMailbox.Controls.Count - 1 - i, flagCollection);
                        envelopeCollection[envelopeCollection.Count - (dMailbox.Controls.Count - 1 - i)].Read = true;
                    }
                }
                envelopeCollection.Save(Page.Server.MapPath(Application["writedirectory\uFFFD"].ToString() + Session["login\uFFFD"] + "_\uFFFD" + Server.UrlEncode(mailbox.Name) + ".xml\uFFFD"));
                LoadMailbox(envelopeCollection, true);
            }
            catch (System.Exception e1)
            {
                Page.RegisterStartupScript("ShowError\uFFFD", "<script>ShowErrorDialog('\uFFFD" + ((Language)Session["language\uFFFD"]).Words[94].ToString() + "','\uFFFD" + System.Text.RegularExpressions.Regex.Escape(e1.Message + e1.StackTrace).Replace("'\uFFFD", "\\'\uFFFD") + "');</script>\uFFFD");
            }
        }

        public void MessageTo(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            // trial
        }

        public void Move(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            // trial
        }

        public void Navigate(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            // trial
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            // trial
        }

        public void Reply(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            int i4;

            //BoundComposer.SetLanguage(((Language)Session["language\uFFFD"]).Code);
            int i1 = 0;
            for (int i2 = 1; i2 < (dMailbox.Controls.Count - 1); i2++)
            {
                if (((System.Web.UI.WebControls.CheckBox)dMailbox.Controls[i2].FindControl("select\uFFFD")).Checked)
                    i1++;
            }
            if (i1 == 1)
            {
                for (int i3 = 1; i3 < (dMailbox.Controls.Count - 1); i3++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)dMailbox.Controls[i3].FindControl("select\uFFFD")).Checked)
                    {
                        char[] chArr1 = new char[] { '|' };
                        //BoundComposer.LoadReply(Application["server\uFFFD"].ToString(), Page.User.Identity.Name.Split(chArr1)[0], MailboxName, dMailbox.Controls.Count - 1 - i3, true);
                    }
                }
            }
            else
            {
                EnvelopeCollection envelopeCollection = EnvelopeCollection.Load(Page.Server.MapPath(Application["writedirectory\uFFFD"].ToString() + Session["login\uFFFD"] + "_\uFFFD" + Server.UrlEncode(MailboxName) + ".xml\uFFFD"));
                string s = System.String.Empty;
                for (i4 = 1; i4 < (dMailbox.Controls.Count - 1); i4++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)dMailbox.Controls[i4].FindControl("select\uFFFD")).Checked)
                        s = s + envelopeCollection[envelopeCollection.Count - (dMailbox.Controls.Count - 1 - i4)].From.Email + ",\uFFFD";
                }
                char[] chArr2 = new char[] { ',' };
                //BoundComposer.LoadParams(s.TrimEnd(chArr2), System.String.Empty, System.String.Empty, true);
                envelopeCollection.Save(Page.Server.MapPath(Application["writedirectory\uFFFD"].ToString() + Session["login\uFFFD"] + "_\uFFFD" + Server.UrlEncode(MailboxName) + ".xml\uFFFD"));
            }
        }

        public void SetLanguage(string code)
        {
            for (int i1 = 0; i1 < Controls.Count; i1++)
            {
                if ((Controls[i1].GetType() == typeof(System.Web.UI.WebControls.Label)) && Controls[i1].ID.StartsWith("lt\uFFFD"))
                    ((System.Web.UI.WebControls.Label)Controls[i1]).Text = ((Language)Session["language\uFFFD"]).Words[System.Convert.ToInt32(Controls[i1].ID.Replace("lt\uFFFD", "\uFFFD"))].ToString();
            }
            if (dMailbox.Controls.Count > 1)
            {
                for (int i2 = 0; i2 < dMailbox.Controls[0].Controls.Count; i2++)
                {
                    if ((dMailbox.Controls[0].Controls[i2].GetType() == typeof(System.Web.UI.WebControls.Label)) && dMailbox.Controls[0].Controls[i2].ID.StartsWith("lt\uFFFD"))
                    //  TODO: review the commented line, it was replaced
                    //  ((System.Web.UI.WebControls.Label)dMailbox.Controls[0].Controls[i2]).Text = ((Language)Session["language\uFFFD"]).Words[System.Convert.ToInt32(dMailbox.Controls[0].Controls[i2].ID.Replace("lt\uFFFD", "\uFFFD"))].ToString();
                        ((System.Web.UI.WebControls.Label)dMailbox.Controls[0].Controls[i2]).Text = ((Language)Session["language\uFFFD"]).Words[System.Convert.ToInt32(dMailbox.Controls[0].Controls[i2].ID.Replace("lt", ""))].ToString();
                }
            }
        }

        public void ShowThisHideBounds()
        {
            /*
            BoundMessageDisplay.Visible = false;
            BoundMessageDisplay.EnableViewState = false;
            BoundComposer.Visible = false;
            BoundComposer.EnableViewState = false;
             * */
        }

        public void Zip(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            string s = "\uFFFD";
            for (int i1 = 1; i1 < (dMailbox.Controls.Count - 1); i1++)
            {
                if (((System.Web.UI.WebControls.CheckBox)dMailbox.Controls[i1].FindControl("select\uFFFD")).Checked)
                {
                    int i2 = dMailbox.Controls.Count - 1 - i1;
                    s = s + i2.ToString() + ",\uFFFD";
                }
            }
            if (s.Length > 0)
            {
                string[] sArr = new string[8];
                sArr[0] = "DownloadZip.aspx?t=i&s=\uFFFD";
                sArr[1] = (string)Application["server\uFFFD"];
                sArr[2] = "&u=\uFFFD";
                char[] chArr1 = new char[] { '|' };
                sArr[3] = Page.User.Identity.Name.Split(chArr1)[0];
                sArr[4] = "&b=\uFFFD";
                sArr[5] = MailboxName;
                sArr[6] = "&m=\uFFFD";
                char[] chArr2 = new char[] { ',' };
                sArr[7] = s.TrimEnd(chArr2);
                Response.Redirect(System.String.Concat(sArr));
            }
        }
}
