using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Groupware;
using ActiveUp.Net.Groupware.vCard;
using System.IO;

namespace ActiveUp.Net.Samples.CompactSP
{
    public partial class frmvCard : Form
    {
        private vCard card;

        public frmvCard()
        {
            InitializeComponent();
            vCard card = new vCard();
            
        }

        private void btnReadCard_Click(object sender, EventArgs e)
        {
            DialogResult dresult = dlgOpen.ShowDialog();
            if (dresult != DialogResult.Cancel)
            {
                System.Threading.Thread.Sleep(0);
                string FileName = dlgOpen.FileName;
                card = vCard.LoadFromFile(FileName);
                if (card != null)
                {
                    txtFirstName.Text = card.FullName;
                    txtLastName.Text = card.Nickname;
                    txtEmail.Text = card.EmailAddresses[0].Address;
                    dtBirth.Value = card.Birthday;
                }
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            if (card == null)
            {
                card = new vCard();
            }
            
            DialogResult dresult = dlgSave.ShowDialog();
            if (dresult != DialogResult.Cancel)
            {
                string FileName = dlgSave.FileName;
                card.FullName = txtFirstName.Text;
                card.Nickname = txtLastName.Text;
                if (card.EmailAddresses.Count == 0)
                    card.EmailAddresses.Add(txtEmail.Text, true);
                else
                    card.EmailAddresses[0].Address = txtEmail.Text;
                card.Birthday = dtBirth.Value;
                card.SaveToFile(FileName);
            }
        }

    }
}