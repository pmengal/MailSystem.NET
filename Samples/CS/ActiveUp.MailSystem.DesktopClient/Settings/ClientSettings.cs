using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ActiveUp.MailSystem.DesktopClient
{
    public class ClientSettings
    {
        // Properties
        private Guid _DefaultAccount;
        private bool _StartWithWindows;
        private bool _IsDefaultMailClient;

        public Guid DefaultAccountID
        {
            get { return _DefaultAccount;  }
            set { _DefaultAccount = value; }
        }

        public bool StartWithWindows
        {
            get { return _StartWithWindows; }
            set { _StartWithWindows = value; }
        }

        public bool IsDefaultMailClient
        {
            get { return _IsDefaultMailClient; }
            set { _IsDefaultMailClient = value; }
        }


        /// <summary>
        /// Saves the Client Settings
        /// </summary>
        /// <param name="file">File path to save to</param>
        /// <param name="c">ClientSettings object</param>
        public static void Save(string file,ClientSettings c)
        {
            try
            {
                if (File.Exists(file))
                    File.Delete(file);

                // TODO: MAke this binary
                System.Xml.Serialization.XmlSerializer xs
                   = new System.Xml.Serialization.XmlSerializer(c.GetType());
                StreamWriter writer = File.CreateText(file);
                xs.Serialize(writer, c);
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Loads ClientSettings from a file.
        /// </summary>
        /// <param name="file">File path to load from.</param>
        /// <returns>An instance of ClientSettings class.</returns>
        public static ClientSettings Load(string file)
        {
            if (!File.Exists(file))
            {
                ClientSettings cfg = new ClientSettings();
                Save(file, cfg);

                return cfg;
            }

            // TODO: MAke this binary
            System.Xml.Serialization.XmlSerializer xs
               = new System.Xml.Serialization.XmlSerializer(
                  typeof(ClientSettings));
            StreamReader reader = File.OpenText(file);
            ClientSettings c = new ClientSettings();
            try
            {
                c = (ClientSettings)xs.Deserialize(reader);
            }
            catch (Exception)
            {
            }
            reader.Close();
            return c;
        }


    }
}
