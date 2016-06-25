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

public partial class FoldersManagement : System.Web.UI.UserControl
{
        
        public System.Web.UI.WebControls.DropDownList dBoxes;
        //public System.Web.UI.HtmlControls.HtmlInputButton iFoldersSubmit;
        //public System.Web.UI.HtmlControls.HtmlInputHidden iId;
        //public System.Web.UI.WebControls.TextBox iMailboxName;
        //public System.Web.UI.WebControls.LinkButton lAdd;
        //public System.Web.UI.WebControls.Label lError;
        //public System.Web.UI.WebControls.Panel pFolders;
        //public System.Web.UI.WebControls.Panel pFoldersForm;
        //public System.Web.UI.WebControls.Repeater rFoldersListing;
   
        public void AddFolders(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            // trial
        }

        public void CreateChild(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            // trial
        }

        public void DeleteFolders(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            try
            {
                ((ActiveUp.Net.Mail.Imap4Client)Session["imapobject\uFFFD"]).DeleteMailbox((string)e.CommandArgument);
                //BoundTree.LoadTrees();
                LoadFoldersListing(null, null);
            }
            catch (System.Exception e1)
            {
                Page.RegisterStartupScript("ShowError\uFFFD", "<script>ShowErrorDialog('\uFFFD" + ((Language)Session["language\uFFFD"]).Words[74].ToString() + "','\uFFFD" + System.Text.RegularExpressions.Regex.Escape(e1.Message + e1.StackTrace).Replace("'\uFFFD", "\\'\uFFFD") + "');</script>\uFFFD");
            }
        }

        public void DoModifyFolders(object sender, System.EventArgs e)
        {
            if (iFoldersSubmit.Value == ((Language)Session["language\uFFFD"]).Words[16].ToString())
            {
                try
                {
                    ((ActiveUp.Net.Mail.Imap4Client)Session["imapobject\uFFFD"]).RenameMailbox(iId.Value, iMailboxName.Text);
                }
                catch (System.Exception e1)
                {
                    Page.RegisterStartupScript("ShowError\uFFFD", "<script>ShowErrorDialog('\uFFFD" + ((Language)Session["language\uFFFD"]).Words[76].ToString() + "','\uFFFD" + System.Text.RegularExpressions.Regex.Escape(e1.Message + e1.StackTrace).Replace("'\uFFFD", "\\'\uFFFD") + "');</script>\uFFFD");
                }
            }
            else if (iFoldersSubmit.Value == ((Language)Session["language\uFFFD"]).Words[20].ToString())
            {
                try
                {
                    ((ActiveUp.Net.Mail.Imap4Client)Session["imapobject\uFFFD"]).SelectMailbox(iId.Value).CreateChild(iMailboxName.Text);
                }
                catch (System.Exception e2)
                {
                    Page.RegisterStartupScript("ShowError\uFFFD", "<script>ShowErrorDialog('\uFFFD" + ((Language)Session["language\uFFFD"]).Words[77].ToString() + "','\uFFFD" + System.Text.RegularExpressions.Regex.Escape(e2.Message + e2.StackTrace).Replace("'\uFFFD", "\\'\uFFFD") + "');</script>\uFFFD");
                }
            }
            else
            {
                try
                {
                    ((ActiveUp.Net.Mail.Imap4Client)Session["imapobject\uFFFD"]).CreateMailbox(iMailboxName.Text);
                }
                catch (System.Exception e3)
                {
                    Page.RegisterStartupScript("ShowError\uFFFD", "<script>ShowErrorDialog('\uFFFD" + ((Language)Session["language\uFFFD"]).Words[75].ToString() + "','\uFFFD" + System.Text.RegularExpressions.Regex.Escape(e3.Message + e3.StackTrace).Replace("'\uFFFD", "\\'\uFFFD") + "');</script>\uFFFD");
                }
            }
            pFoldersForm.Visible = false;
            //BoundTree.LoadTrees();
            LoadFoldersListing(null, null);
        }

        public void LoadFoldersListing(object sender, System.EventArgs e)
        {
            // trial
        }

        public void ModifyFolders(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            iMailboxName.Text = (string)e.CommandArgument;
            iId.Value = (string)e.CommandArgument;
            iFoldersSubmit.Value = ((Language)Session["language\uFFFD"]).Words[16].ToString();
            pFoldersForm.Visible = true;
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            // trial
        }

        public void SetLanguage(string code)
        {
            int i;
            Language language = Language.Get(Server.MapPath("languages/"), code);
            for (i = 0; i < Controls.Count; i++)
            {
                if ((Controls[i].GetType() == typeof(System.Web.UI.WebControls.Label)) && Controls[i].ID.StartsWith("lt"))
                {
                    ((System.Web.UI.WebControls.Label)Controls[i]).Text = language.Words[System.Convert.ToInt32(Controls[i].ID.Replace("lt", ""))].ToString();
                }
            }
        }

        public void ShowFolders()
        {
            // trial
        }

        public void ShowFoldersForm(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            pFolders.Visible = true;
            pFoldersForm.Visible = true;
        }

        public void ShowThisHideBounds()
        {
            /*
            EnableViewState = true;
            Visible = true;
            BoundMessageDisplay.Visible = false;
            BoundMessageDisplay.EnableViewState = false;
            BoundComposer.Visible = false;
            BoundComposer.EnableViewState = false;
            BoundMailboxContent.Visible = false;
            BoundMailboxContent.EnableViewState = false;
             * */
        }
    
}
