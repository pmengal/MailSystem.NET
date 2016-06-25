using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Groupware.vCalendar;
using ActiveUp.Net.Samples.Utils;
using System.IO;

namespace ActiveUp.Net.Samples.PDI
{
    public partial class ReadWritevCalendar : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public ReadWritevCalendar(SamplesConfiguration config)
        {
            this.Config = config;
            InitializeComponent();
            InitializeSample();
        }

        protected override void InitializeSample()
        {
            base.InitializeSample();

            //this.toEmailTextbox.Text = Config.ToEmail;
            //this.smtpServerAddressTextbox.Text = Config.MainSmtpServer;
        }

        private void openVcalendarButton_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openvCalendarDialog.ShowDialog();

            if (result == DialogResult.OK)
                this.LoadvCalendar(this.openvCalendarDialog.FileName);
        }

        private void LoadvCalendar(string p)
        {
            ClearForm();

            try
            {
                StreamReader streamReader = new StreamReader(p);
                string content = streamReader.ReadToEnd();
                streamReader.Close();
                vCalendar calendar = ActiveUp.Net.Groupware.vCalendar.Parser.Parse(content);

                if (calendar.Events.Count > 0)
                {
                    vEvent fEvent = calendar.Events[0];
                    this.objectTextbox.Text = fEvent.Summary;
                    this.placeTextbox.Text = fEvent.Location;
                    this.startDate.Value = this.startTime.Value = fEvent.Start;
                    this.endDate.Value = this.endTime.Value = fEvent.End;
                }
                else
                    MessageBox.Show("No events found in the vCalendar file.");
                
                this.rawDataTextbox.Text = content;
            }
            catch (Exception ex)
            {
                ClearForm();
                throw ex;
                MessageBox.Show("Error while loading the vCard. Please ensure this is a compatible format.");
            }
            finally
            {
                
            }
        }

        private void ClearForm()
        {
            this.objectTextbox.Text = string.Empty;
            this.placeTextbox.Text = string.Empty;
            this.startTime.Value = this.startDate.Value = this.endDate.Value = this.endTime.Value = DateTime.Now;
        }

        private void createNewButton_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void saveAsButton_Click(object sender, EventArgs e)
        {
            this.savevCalendarDialog.ShowDialog();

            vCalendar calendar = new vCalendar();
            vEvent newEvent = new vEvent();
            newEvent.Summary = this.objectTextbox.Text;
            newEvent.Location = this.placeTextbox.Text;
            newEvent.Start = DateTime.Now;
            newEvent.End = DateTime.Now;
            calendar.Events.Add(newEvent);
            calendar.SaveToFile(this.savevCalendarDialog.FileName);
        }
    }
}
