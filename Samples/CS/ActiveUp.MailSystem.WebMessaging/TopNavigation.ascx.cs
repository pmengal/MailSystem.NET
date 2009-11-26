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

public partial class TopNavigation : System.Web.UI.UserControl
{
  
        //public System.Web.UI.WebControls.DropDownList dBoxes;
        //public System.Web.UI.WebControls.LinkButton lCompose;
        //public System.Web.UI.WebControls.LinkButton lDelete;
        //public System.Web.UI.WebControls.LinkButton lFolders;
        //public System.Web.UI.WebControls.LinkButton lForward;
        //public System.Web.UI.WebControls.LinkButton lLogout;
        //public System.Web.UI.WebControls.LinkButton lMark;
        //public System.Web.UI.WebControls.LinkButton lMarkAsUnread;
        //public System.Web.UI.WebControls.LinkButton lMove;
        //public System.Web.UI.WebControls.LinkButton lReply;
        //public System.Web.UI.WebControls.LinkButton lSearch;
        //public System.Web.UI.WebControls.LinkButton lZip;
           
        public void Delete(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
           
        }

        public void Forward(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
           
        }

        public void LoadList()
        {
            // trial
        }

        public void LogOut(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {

        }        

        public void MarkAsUnread(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            
        }

        public void Move(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            if ((Session["imapobject\uFFFD"] != null) && Page.User.Identity.IsAuthenticated && !IsPostBack) {
                LoadList();
            }

            this.SetLanguage("");
        }

       
        public void SetLanguage(string code)
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                if ((Controls[i].GetType() == typeof(System.Web.UI.WebControls.Label)) && Controls[i].ID.StartsWith("lt\uFFFD"))
                 //  TODO: review the commented line, it was replaced
                 //  ((System.Web.UI.WebControls.Label)Controls[i]).Text = ((Language)Session["language\uFFFD"]).Words[System.Convert.ToInt32(Controls[i].ID.Replace("lt\uFFFD", "\uFFFD"))].ToString();
                    ((System.Web.UI.WebControls.Label)Controls[i]).Text = ((Language)Session["language\uFFFD"]).Words[System.Convert.ToInt32(Controls[i].ID.Replace("lt", ""))].ToString();
            }
            lCompose.Text = "<img name=\"compose\" src=\"icons/compose_off.gif\" style=\"border-width:0px;\" alt=\"\uFFFD" + ((Language)Session["language\uFFFD"]).Words[0].ToString() + "\" /><br />" + ((Language)Session["language\uFFFD"]).Words[0].ToString();
            lFolders.Text = "<img name=\"folders\" src=\"icons/folders_off.gif\" style=\"border-width:0px;\" alt=\"\uFFFD" + ((Language)Session["language\uFFFD"]).Words[44].ToString() + "\" /><br />" + ((Language)Session["language\uFFFD"]).Words[44].ToString();
            lSearch.Text = "<img name=\"search\" src=\"icons/search_off.gif\" style=\"border-width:0px;\" alt=\"\uFFFD" + ((Language)Session["language\uFFFD"]).Words[45].ToString() + "\" /><br />" + ((Language)Session["language\uFFFD"]).Words[45].ToString();
            lReply.Text = "<img name=\"reply\" src=\"icons/reply_off.gif\" style=\"border-width:0px;\" alt=\"\uFFFD" + ((Language)Session["language\uFFFD"]).Words[37].ToString() + "\" /><br />" + ((Language)Session["language\uFFFD"]).Words[37].ToString();
            lForward.Text = "<img name=\"forward\" src=\"icons/forward_off.gif\" style=\"border-width:0px;\" alt=\"\uFFFD" + ((Language)Session["language\uFFFD"]).Words[38].ToString() + "\" /><br />" + ((Language)Session["language\uFFFD"]).Words[38].ToString();
            lDelete.Text = "<img name=\"delete\" src=\"icons/delete_off.gif\" style=\"border-width:0px;\" alt=\"\uFFFD" + ((Language)Session["language\uFFFD"]).Words[1].ToString() + "\" /><br />" + ((Language)Session["language\uFFFD"]).Words[1].ToString();
            lMarkAsUnread.Text = "<img name=\"markunread\" src=\"icons/markunread_off.gif\" style=\"border-width:0px;\" alt=\"\uFFFD" + ((Language)Session["language\uFFFD"]).Words[2].ToString() + "\" /><br />" + ((Language)Session["language\uFFFD"]).Words[2].ToString();
            lMark.Text = "<img name=\"mark\" src=\"icons/mark_off.gif\" style=\"border-width:0px;\" alt=\"\uFFFD" + ((Language)Session["language\uFFFD"]).Words[3].ToString() + "\" /><br />" + ((Language)Session["language\uFFFD"]).Words[3].ToString();
            lZip.Text = "<img name=\"zip\" src=\"icons/zip_off.gif\" style=\"border-width:0px;\" alt=\"\uFFFD" + ((Language)Session["language\uFFFD"]).Words[4].ToString() + "\" /><br />" + ((Language)Session["language\uFFFD"]).Words[4].ToString();
            lMove.Text = "<img name=\"copy\" src=\"icons/copym_off.gif\" align=\"absmiddle\" style=\"border-width:0px;\" alt=\"\uFFFD" + ((Language)Session["language\uFFFD"]).Words[40].ToString() + "\" />";
            lLogout.Text = "<img name=\"logout\" src=\"icons/logout_off.gif\" style=\"border-width:0px;\" alt=\"\uFFFD" + ((Language)Session["language\uFFFD"]).Words[100].ToString() + "\" /><br />" + ((Language)Session["language\uFFFD"]).Words[100].ToString();
        }

        public void Zip(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            // trial
        }


    protected void lCompose_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Composer.aspx");
    }
    protected void Compose(object sender, CommandEventArgs e)
    {
        //Response.Redirect("~/Composer.aspx");
    }
    protected void Reply(object sender, CommandEventArgs e)
    {

    }
    protected void Folders(object sender, CommandEventArgs e)
    {

    }
    protected void Search(object sender, CommandEventArgs e)
    {
        Response.Redirect("~/SearchEngine.aspx");
    }
    protected void Mark(object sender, CommandEventArgs e)
    {
        Response.Redirect("~/MailboxContent.aspx");
    }
    protected void lReply_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MessageDisplay.aspx");
    }
    protected void lFolders_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/FoldersManagement.aspx");
    }
    protected void lZip_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ConfigurationEmailAccount.aspx");
    }
}
