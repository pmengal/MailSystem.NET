using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ActiveUp.MailSystem.DesktopClient.Folder
{
    /// <summary>
    /// This class represents a create folder form. 
    /// It's used for create new forders.
    /// </summary>
    public partial class CreateFolderForm : Form
    {

        /// <summary>
        /// Create folder form constructor.
        /// </summary>
        public CreateFolderForm()
        {
            this.InitializeComponent();
            FolderUtil.LoadTree(this.folderTreeView);
        }

        /// <summary>
        /// Method for validate the fields.
        /// </summary>
        /// <returns></returns>
        private bool ValidateFields()
        {
            bool ret = true;
            if (this.txtName.Text.Trim().Equals(string.Empty))
            {
                ret = false;
                string msg = "Enter a name";
                MessageBox.Show(msg, "ActiveUp eMail Client", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return ret;
        }

        /// <summary>
        /// Event handler for button ok.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.ValidateFields())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// Event handler for button cancel.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Property for access the folder name.
        /// </summary>
        public string FolderName
        {
            get { return this.txtName.Text; }
            set { this.txtName.Text = value; }
        }

        /// <summary>
        /// Property for get/set the selected folder.
        /// </summary>
        public string SelectedFolder
        {
            get
            {
                return this.folderTreeView.SelectedNode.Text;
            }
            set
            {
                this.SelectNode(value);
            }
        }

        /// <summary>
        /// Method for select a node.
        /// </summary>
        /// <param name="nodeName">The node name.</param>
        private void SelectNode(string nodeName)
        {
            foreach (TreeNode node in this.folderTreeView.Nodes)
            {
                bool selected = this.SelectNodeRecursive(node, nodeName);
                if (selected)
                {
                    node.Expand();
                }
            }
        }

        /// <summary>
        /// Method for find nodes recursively.
        /// </summary>
        /// <param name="parent">The parent tree node.</param>
        /// <param name="nodeName">The node name to be selected.</param>
        /// <returns>A boolean that indicate if the tree node was selected or not.</returns>
        private bool SelectNodeRecursive(TreeNode parent, string nodeName)
        {
            bool ret = false;
            if (parent.Text.Equals(nodeName))
            {
                this.folderTreeView.SelectedNode = parent;
                ret = true;
            }
            else
            {
                foreach (TreeNode node in parent.Nodes)
                {
                    ret = ret || this.SelectNodeRecursive(node, nodeName);
                }
            }
            return ret;
        }
    }
}