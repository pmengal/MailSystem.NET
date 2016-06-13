using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Groupware.vCalendar;

namespace ActiveUp.Net.Samples.Compact
{
    public partial class frmVcalendar : Form
    {
        private vCalendar cal;
        public frmVcalendar()
        {
            InitializeComponent();
            cal = new vCalendar();
        }


        private void btnReadCard_Click_1(object sender, EventArgs e)
        {
            DialogResult dresult = dlgOpen.ShowDialog();
            System.Threading.Thread.Sleep(0);
            if (dresult != DialogResult.Cancel)
            {
                string FileName = dlgOpen.FileName;
                cal = vCalendar.LoadFromFile(FileName);
                if (cal.Events.Count > 0)
                {
                    txtDescription.Text = cal.Events[0].Description;
                    txtName.Text = cal.Events[0].Summary;
                    dtStart.Value = cal.Events[0].Start;
                    dtEnd.Value = cal.Events[0].End;
                }
            }
        }

        private void btnWrite_Click_1(object sender, EventArgs e)
        {
            DialogResult dresult = dlgSave.ShowDialog();
            System.Threading.Thread.Sleep(0);
            if (dresult != DialogResult.Cancel)
            {
                string FileName = dlgSave.FileName;
                if (cal.Events.Count == 0)
                {
                    cal.Events.Add(new vEvent());
                }
                
                
                cal.Events[0].Description = txtDescription.Text;
                cal.Events[0].Summary = txtName.Text;
                cal.Events[0].Start = dtStart.Value;
                cal.Events[0].End = dtEnd.Value.AddMinutes(10);
                cal.SaveToFile(FileName);
            }
        }
    }
}