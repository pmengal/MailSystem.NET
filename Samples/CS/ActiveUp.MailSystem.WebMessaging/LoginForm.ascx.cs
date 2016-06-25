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


public partial class LoginForm : System.Web.UI.UserControl
{
        //private MailboxContent _boundMailboxContent;
        //public System.Web.UI.WebControls.TextBox iLogin;
        //public System.Web.UI.HtmlControls.HtmlInputButton iLoginButton;
        //public System.Web.UI.WebControls.TextBox iPassword;

       
        private string FromBase64(string input)
        {
            // trial
            return null;
        }

        public void Login(object sender, System.EventArgs e)
        {
            try
            {
                ActiveUp.Net.Mail.Imap4Client imap4Client = new ActiveUp.Net.Mail.Imap4Client();
                imap4Client.Connect((string)Application["server\uFFFD"], System.Convert.ToInt32(Application["port\uFFFD"]));
                System.Web.Security.FormsAuthentication.SetAuthCookie(iLogin.Text + "|\uFFFD" + iPassword.Text, false);
                imap4Client.Login(iLogin.Text, iPassword.Text);
                Session.Add("login\uFFFD", iLogin.Text);
                Session.Add("imapobject\uFFFD", imap4Client);
                System.Web.HttpCookie httpCookie = new System.Web.HttpCookie("login\uFFFD", iLogin.Text);
                System.DateTime dateTime = System.DateTime.Now;
                httpCookie.Expires = dateTime.AddMonths(2);
                Response.Cookies.Add(httpCookie);
                Visible = false;
                EnableViewState = false;
             //   ((_Default)Page).SetWebmailLanguage(null, null);
                //BoundMailboxContent.BoundTopNavigation.Enable();
                //BoundMailboxContent.BoundTree.LoadTrees();
                //BoundMailboxContent.BoundTopNavigation.LoadList();
                //BoundMailboxContent.LoadMailbox(Application["startfolder\uFFFD"].ToString(), true);
            }
            catch (System.Exception e1)
            {
                Visible = true;
                EnableViewState = true;
                iLogin.Text = System.String.Empty;
                //    Page.RegisterStartupScript("ShowError\uFFFD", "<script>ShowErrorDialog('\uFFFD" + Language.Get(Server.MapPath("languages/\uFFFD"), Application["defaultlanguage\uFFFD"].ToString()).Words[96].ToString() + "','\uFFFD" + System.Text.RegularExpressions.Regex.Escape(e1.Message + e1.StackTrace).Replace("'\uFFFD", "\\'\uFFFD") + "');</script>\uFFFD");
                Page.RegisterStartupScript("ShowError\uFFFD", "<script>ShowErrorDialog('\uFFFD" + Language.Get(Server.MapPath("languages/"), Application["defaultlanguage\uFFFD"].ToString()).Words[96].ToString() + "','\uFFFD" + System.Text.RegularExpressions.Regex.Escape(e1.Message + e1.StackTrace).Replace("'\uFFFD", "\\'\uFFFD") + "');</script>\uFFFD");
            }
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            // trial
        }

        public void SetLanguage(string code)
        {
            //  TODO: review the commented line
            //Language language = Language.Get(Server.MapPath("languages/\uFFFD"), code);
            Language language = Language.Get(Server.MapPath("languages/"), code);
            for (int i = 0; i < Controls.Count; i++)
            {
                if ((Controls[i].GetType() == typeof(System.Web.UI.WebControls.Label)) && Controls[i].ID.StartsWith("lt\uFFFD"))
                {
                    //  TODO: review the commented line
                    //((System.Web.UI.WebControls.Label)Controls[i]).Text = language.Words[System.Convert.ToInt32(Controls[i].ID.Replace("lt\uFFFD", "\uFFFD"))].ToString();
                    ((System.Web.UI.WebControls.Label)Controls[i]).Text = language.Words[System.Convert.ToInt32(Controls[i].ID.Replace("lt", ""))].ToString();
                }
            }
            iLoginButton.Value = language.Words[99].ToString();
        }
}
