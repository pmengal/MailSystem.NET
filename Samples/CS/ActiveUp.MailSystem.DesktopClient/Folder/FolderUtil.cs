using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

namespace ActiveUp.MailSystem.DesktopClient.Folder
{
    /// <summary>
    /// This class contains utilities methods for folders.
    /// </summary>
    public class FolderUtil
    {

        #region Save (saveTree, saveNode)
        /// <summary>
        /// Save the TreeView content.
        /// </summary>
        /// <param name="tree">The tree view.</param>
        /// <returns>Error code as int.</returns>
        public static int SaveTree(TreeView tree)
        {
            string filename = GetFolderFile();

            ArrayList al = new ArrayList();
            foreach (TreeNode tn in tree.Nodes)
            {
                al.Add(tn);
            }

            Stream file = File.Open(filename, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                bf.Serialize(file, al);
            }
            catch (System.Runtime.Serialization.SerializationException e)
            {
                MessageBox.Show("Serialization failed : {0}", e.Message);
                return -1;
            }

            file.Close();

            return 0;
        }
        #endregion

        #region Load (loadTree, searchNode)
        /// <summary>
        /// Load the TreeView content.
        /// </summary>
        /// <param name="tree">The tree view.</param>
        /// <returns>Error code as int.</returns>
        public static int LoadTree(TreeView tree)
        {
            string filename = GetFolderFile();
            if (File.Exists(filename))
            {
                tree.Nodes.Clear();

                Stream file = File.Open(filename, FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                object obj = null;
                try
                {
                    obj = bf.Deserialize(file);
                }
                catch (System.Runtime.Serialization.SerializationException e)
                {
                    MessageBox.Show("De-Serialization failed : {0}", e.Message);
                    return -1;
                }
                file.Close();

                ArrayList nodeList = obj as ArrayList;

                foreach (TreeNode node in nodeList)
                {
                    tree.Nodes.Add(node);
                }
                return 0;

            }
            else return -2;
        }

        #endregion

        /// <summary>
        /// Method for get the folder file.
        /// </summary>
        /// <returns>The string file path.</returns>
        private static string GetFolderFile()
        {
            // verify if the messages directory exist, if not create it.
            string directory = Constants.Messages;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string path = Path.Combine(directory, "tree.folders");
            return path;
        }

    }
}
