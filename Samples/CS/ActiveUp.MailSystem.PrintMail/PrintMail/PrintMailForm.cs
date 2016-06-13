using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Drawing.Printing;

namespace PrintMail
{
    public partial class PrintMailForm : Form
    {
        Controller controller;

        public PrintMailForm()
        {
            controller = new Controller();           
            InitializeComponent();
        }

        private void ReadXmlSettings()
        {
            XmlTextReader xr = new XmlTextReader(Path.Combine(Path.GetTempPath(), Constants.SETT_FILE_NAME));
            string printer = String.Empty;
            int i = 0;

            while (xr.Read())
            {
                XmlNodeType nType = xr.NodeType;

                if (nType == XmlNodeType.Text)
                {
                    if (i == 0) textBoxPop3.Text = xr.Value;
                    else if (i == 1) textBoxLogin.Text = xr.Value;
                    else if (i == 2) textBoxPassword.Text = xr.Value;
                    else if (i == 3)
                    {                       
                        foreach (string item in comboBox1.Items)
                        {
                            if (item == xr.Value)
                            {
                                comboBox1.SelectedItem = item;                               
                            }
                        }                       
                        printer = xr.Value;
                    }
                    i++;
                }
            }
            xr.Close();
            this.controller.Connect(textBoxPop3.Text, textBoxLogin.Text, textBoxPassword.Text, printer);          
        }

        private void SaveXmlSettings(string pop3, string login, string password, string printer)
        {
            XmlDocument doc = new XmlDocument();
            string filename = Path.Combine(Path.GetTempPath(), Constants.SETT_FILE_NAME);
            XmlTextWriter xw = new XmlTextWriter(filename, null);
            xw.WriteStartDocument();
            xw.WriteStartElement("Settings");
            xw.WriteElementString("pop3", pop3);
            xw.WriteElementString("login", login);
            xw.WriteElementString("password", password);
            xw.WriteElementString("printer", printer);
            xw.WriteEndElement();
            xw.WriteEndDocument();
            xw.Flush();
            xw.Close();

            this.controller.Connect(pop3, login, password, printer);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.LoadPrinters();

            if (this.ExistFileSettings())
            {
                this.ReadXmlSettings();
            }            
        }

        private void LoadPrinters()
        {
            foreach (String printer in PrinterSettings.InstalledPrinters)
            {
                comboBox1.Items.Add(printer.ToString());
            }
        }

        private void toolStripSave_Click(object sender, EventArgs e)
        {
            string pop3 = textBoxPop3.Text;
            string login = textBoxLogin.Text;
            string password = textBoxPassword.Text;
            string item = String.Empty;

            if(comboBox1.SelectedItem != null)  item = (string)comboBox1.SelectedItem.ToString();

            this.SaveXmlSettings(pop3, login, password,item);
        }

        private void toolStripStart_Click(object sender, EventArgs e)
        {
            this.controller.StartThread();            
        }

        private void toolStripStop_Click(object sender, EventArgs e)
        {
            this.controller.StopThread();
        }

        private bool ExistFileSettings()
        {
            string file = Path.Combine(Path.GetTempPath(), Constants.SETT_FILE_NAME);
            if (File.Exists(file)) return true;
            else return false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBoxPop3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}