using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace ActiveUp.Net.Samples.Utils
{
    public partial class MasterForm : Form
    {
        private SamplesConfiguration _config;

        public MasterForm()
        {
            //this.Config = config;

            InitializeComponent();
        }

        protected virtual void InitializeSample()
        {
            DisplayHtmlCode();
        }

        private void DisplayHtmlCode()
        {
            try
            {
                this.htmlCode.DocumentText = MasterForm.GetResource(this.GetType().ToString() + ".htm");
            }
            catch 
            {
                this.htmlCode.DocumentText = "Document not found.";
            }
        }

        public SamplesConfiguration Config
        {
            get
            {
                return _config;
            }
            set
            {
                _config = value;
            }
        }

        public void AddLogEntry(string entry)
        {
            DateTime d = DateTime.Now;
            StringBuilder sb = new StringBuilder();
            sb.Append(d.Hour.ToString().PadLeft(2, '0'));
            sb.Append(":");
            sb.Append(d.Minute.ToString().PadLeft(2, '0'));
            sb.Append(":");
            sb.Append(d.Second.ToString().PadLeft(2, '0'));
            sb.Append(".");
            sb.Append(d.Millisecond.ToString().PadLeft(3, '0'));
            sb.Append(" | ");
            sb.Append(entry);

            this.logListBox.Items.Insert(0, sb.ToString());
        }

        private void MasterForm_Shown(object sender, EventArgs e)
        {
            InitializeSample();
        }

        /// <summary>
        /// Get the specified resource from the assembly.
        /// </summary>
        /// <param name="resource">The name of the resource.</param>
        /// <param name="type">The type of the assembly.</param>
        /// <returns>The string representation of the resource.</returns>
        public static string GetResource(string resource, System.Type type)
        {
            string str = null;
            Assembly asm;

            if (type != null)
                asm = Assembly.GetAssembly(type);
            else
                asm = Assembly.GetExecutingAssembly();
            // We check for null just in case the variable is called at design-time.
            if (asm != null)
            {
                // Just for clarity define multiple variables.
                Stream stm = asm.GetManifestResourceStream(resource);
                StreamReader reader = new StreamReader(stm);
                str = reader.ReadToEnd();
                reader.Close();
                stm.Close();
            }

            return str;
        }


        /// <summary>
        /// Get the specified resource from the assembly.
        /// </summary>
        /// <param name="resource">The name of the resource.</param>
        /// <returns>The string representation of the resource.</returns>
        public static string GetResource(string resource)
        {
            return GetResource(resource, null);
        }

        protected void LoadXml(string xmlFile)
        {
            try
            {
                if (!File.Exists(xmlFile))
                {
                    this.htmlCode.DocumentText = "Xml template not found.";
                }

                else
                {
                    this.htmlCode.Navigate(xmlFile);
                }
            }

            catch (Exception ex)
            {
                this.AddLogEntry(string.Format("Xml template read failed. Raison is as follow : {0}", ex.Message));
            }
        }
     
    }
}