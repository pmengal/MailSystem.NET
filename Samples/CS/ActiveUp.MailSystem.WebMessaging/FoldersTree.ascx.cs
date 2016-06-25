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

public partial class FoldersTree : System.Web.UI.UserControl
{
    /*
        private FoldersManagement _boundFolders;
        private MailboxContent _boundMailboxContent;
        private MessageDisplay _boundMessageDisplay;
        private TopNavigation _boundTopNavigation;
     */
        private int _selected;
        public System.Web.UI.WebControls.LinkButton lImap;
        public System.Web.UI.WebControls.PlaceHolder pTree;
        //public ActiveUp.WebControls.TreeView tree;

        public int SelectedMailbox
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
            }
        }

        public void LoadTrees()
        {
            // trial
        }

        public void Navigate(object sender, System.EventArgs e)
        {
            // trial
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            if ((Session["imapobject\uFFFD"] != null) && Page.User.Identity.IsAuthenticated)
                LoadTrees();
        }

        private void PutChild(ActiveUp.Net.Mail.Mailbox box)
        {
            foreach (ActiveUp.Net.Mail.Mailbox mailbox in box.SubMailboxes)
            {
                if (mailbox.Name.Replace(box.Name + "/\uFFFD", "\uFFFD").IndexOf("/\uFFFD") == -1)
                {
                    ActiveUp.WebControls.TreeNode treeNode = tree.FindNode(box.Name).AddNode(mailbox.Name, "<span class=\"foldername\">\uFFFD" + mailbox.ShortName + "</span>\uFFFD", System.String.Empty);
                    treeNode.Click += new System.EventHandler(Navigate);
                    PutChild(mailbox);
                }
            }
        }

        public void SetLanguage()
        {
            //tree.Label = "<span class=\"foldername\">\uFFFD" + ((Language)Session["language\uFFFD"]).Words[19].ToString() + "</span>\uFFFD";
        }

}
