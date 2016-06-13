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

public partial class SearchEngine : System.Web.UI.UserControl
{
        
        //public System.Web.UI.WebControls.CheckBox cAfter;
        //public System.Web.UI.WebControls.CheckBox cBefore;
        //public System.Web.UI.WebControls.CheckBox cOn;
        //public System.Web.UI.WebControls.DropDownList dBoxes;
        //public ActiveUp.WebControls.Calendar iAfter;
        //public ActiveUp.WebControls.Calendar iBefore;
        //public System.Web.UI.WebControls.TextBox iBody;
        //public System.Web.UI.WebControls.TextBox iCc;
        //public System.Web.UI.WebControls.TextBox iFrom;
        //public ActiveUp.WebControls.Calendar iOn;
        //public System.Web.UI.WebControls.TextBox iQuick;
        //public System.Web.UI.WebControls.TextBox iSubject;
        //public System.Web.UI.HtmlControls.HtmlInputButton iSubmit;
        //public System.Web.UI.HtmlControls.HtmlInputButton iSubmitQuick;
        //public System.Web.UI.WebControls.TextBox iTo;

        public void LoadList()
        {
            try
            {
                dBoxes.Items.Clear();
                dBoxes.Items.Insert(0, ((Language)Session["language\uFFFD"]).Words[65].ToString());
                foreach (ActiveUp.Net.Mail.Mailbox mailbox in ((ActiveUp.Net.Mail.Imap4Client)Session["imapobject\uFFFD"]).AllMailboxes)
                {
                    dBoxes.Items.Add(mailbox.Name);
                }
                if ((Session["mailbox\uFFFD"] != null) && (dBoxes.Items.IndexOf(new System.Web.UI.WebControls.ListItem((string)Session["mailbox\uFFFD"])) != -1))
                    dBoxes.Items.FindByText((string)Session["mailbox\uFFFD"]).Selected = true;
            }
            catch (System.Exception e)
            {
                Page.RegisterStartupScript("ShowError\uFFFD", "<script>ShowErrorDialog('\uFFFD" + ((Language)Session["language\uFFFD"]).Words[73].ToString() + "','\uFFFD" + System.Text.RegularExpressions.Regex.Escape(e.Message + e.StackTrace).Replace("'\uFFFD", "\\'\uFFFD") + "');</script>\uFFFD");
            }
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            // trial
        }

        public void Search(object sender, System.EventArgs e)
        {
            // trial
        }

        public void SearchQuick(object sender, System.EventArgs e)
        {
            int i1;

            try
            {
                EnvelopeCollection envelopeCollection = new EnvelopeCollection();
                string s = "OR FROM \"\uFFFD" + iQuick.Text + "\" (OR TO \"\uFFFD" + iQuick.Text + "\" (OR CC \"\uFFFD" + iQuick.Text + "\" (OR SUBJECT \"\uFFFD" + iQuick.Text + "\" BODY \"\uFFFD" + iQuick.Text + "\")))\uFFFD";
                for (i1 = 0; i1 < (dBoxes.Items.Count - 1); i1++)
                {
                    ActiveUp.Net.Mail.Mailbox mailbox = ((ActiveUp.Net.Mail.Imap4Client)Session["imapobject\uFFFD"]).SelectMailbox(dBoxes.Items[i1 + 1].Text);
                    int[] iArr1 = mailbox.Search(s);
                    int[] iArr2 = iArr1;
                    for (int i3 = 0; i3 < iArr2.Length; i3++)
                    {
                        int i2 = iArr2[i3];
                        Envelope envelope = new Envelope();
                        string[] sArr3 = new string[] {
                                                        "grun\uFFFD", 
                                                        "subject\uFFFD", 
                                                        "from\uFFFD", 
                                                        "date\uFFFD" };
                        System.Collections.Specialized.NameValueCollection nameValueCollection = mailbox.Fetch.HeaderLines(i2, sArr3);
                        ActiveUp.Net.Mail.FlagCollection flagCollection = mailbox.Fetch.Flags(i2);
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
                        //envelope.Date = new System.DateTime(System.Convert.ToInt32(sArr1[3]), BoundMailboxContent.GetMonth(sArr1[2]), System.Convert.ToInt32(sArr1[1]), System.Convert.ToInt32(sArr1[4].Split(chArr2)[0]), System.Convert.ToInt32(sArr1[4].Split(chArr3)[1]), System.Convert.ToInt32(sArr1[4].Split(chArr4)[2]));
                        envelope.Size = mailbox.Fetch.Size(i2);
                        envelope.Id = i2;
                        envelope.Mailbox = mailbox.Name;
                        envelopeCollection.Add(envelope);
                    }
                }
                //this._boundMailboxContent.LoadMailbox(envelopeCollection, false);
                //this._boundMailboxContent.LFolderText = ((Language)Session["language\uFFFD"]).Words[107].ToString();
                //((System.Web.UI.HtmlControls.HtmlInputCheckBox)this._boundMailboxContent.DMailBoxControl).Disabled = true;
                //BoundMailboxContent.Visible = true;
            }
            catch (System.Exception e1)
            {
                Page.RegisterStartupScript("ShowError\uFFFD", "<script>ShowErrorDialog('\uFFFD" + ((Language)Session["language\uFFFD"]).Words[95].ToString() + "','\uFFFD" + System.Text.RegularExpressions.Regex.Escape(e1.Message + e1.StackTrace).Replace("'\uFFFD", "\\'\uFFFD") + "');</script>\uFFFD");
            }
        }

        public void SetLanguage(string code)
        {
            /// Set Months
            Language language = Language.Get(Server.MapPath("languages/"), code);
            int i;
            for(i = 0;i<12;i++)
            {
                iBefore.Months.SetValue(language.Words[i+53].ToString(), i);
                iAfter.Months.SetValue(language.Words[i + 53].ToString(), i);
                iOn.Months.SetValue(language.Words[i + 53].ToString(), i);
            }

            /// Set other fields
            for (i = 0; i < Controls.Count; i++)
            {
                if ((Controls[i].GetType() == typeof(System.Web.UI.WebControls.Label)) && Controls[i].ID.StartsWith("lt"))
                {
                    ((System.Web.UI.WebControls.Label)Controls[i]).Text = language.Words[System.Convert.ToInt32(Controls[i].ID.Replace("lt", ""))].ToString();
                }
            }

        }
}
