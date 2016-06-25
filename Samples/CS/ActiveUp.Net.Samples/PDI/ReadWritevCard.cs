using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActiveUp.Net.Samples.Utils;
using ActiveUp.Net.Groupware.vCard;
using System.IO;

namespace ActiveUp.Net.Samples.PDI
{
    public partial class ReadWritevCard : ActiveUp.Net.Samples.Utils.MasterForm
    {
        public ReadWritevCard(SamplesConfiguration config)
        {
            this.Config = config;
            InitializeComponent();
            InitializeSample();
        }

        protected override void InitializeSample()
        {
            base.InitializeSample();

            this.toEmailTextbox.Text = Config.ToEmail;
            this.smtpServerAddressTextbox.Text = Config.MainSmtpServer;
        }

        private void openVcardButton_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openvCardDialog.ShowDialog();

            if (result == DialogResult.OK)
                this.LoadvCard(this.openvCardDialog.FileName);
        }

        private void LoadvCard(string p)
        {
            ClearForm();

            try
            {
                vCard vcard = vCard.LoadFromFile(p);

                this.firstNameTextbox.Text = vcard.Name.GivenName;
                this.lastNameTextbox.Text = vcard.Name.FamilyName;
                if (vcard.Organization.Count > 0)
                    this.companyTextbox.Text = vcard.Organization[0].ToString();
                foreach (TelephoneNumber telephoneNumber in vcard.TelephoneNumbers)
                {
                    if (telephoneNumber.IsWork)
                        this.officePhoneTextbox.Text = telephoneNumber.Number;
                    else if (telephoneNumber.IsHome)
                        this.homePhoneTextbox.Text = telephoneNumber.Number;
                    else if (telephoneNumber.IsCellular)
                        this.mobilePhoneTextbox.Text = telephoneNumber.Number;
                }
                if (vcard.EmailAddresses.Count > 0)
                    this.emailTextbox.Text = vcard.EmailAddresses[0].Address;
                this.birthdayDatePicker.Value = vcard.Birthday;

                if (vcard.Photo != null)
                {
                    MemoryStream stream = new MemoryStream(vcard.Photo);
                    this.photoPictureBox.Image = Image.FromStream(stream);
                }

                this.rawDataTextbox.Text = vcard.GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading the vCard. Please ensure this is a compatible format.");
            }
            finally
            {
                ClearForm();
            }
        }

        private void saveAsButton_Click(object sender, EventArgs e)
        {
            DialogResult result = this.savevCardDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    vCard card = new vCard();

                    card.Name.GivenName = this.firstNameTextbox.Text;
                    card.Name.FamilyName = this.lastNameTextbox.Text;

                    if (this.companyTextbox.Text != string.Empty)
                        card.Organization.Add(this.companyTextbox.Text);

                    if (this.officePhoneTextbox.Text != string.Empty)
                        card.TelephoneNumbers.Add(this.officePhoneTextbox.Text, TelephoneNumberSingleType.Work);

                    if (this.homePhoneTextbox.Text != string.Empty)
                        card.TelephoneNumbers.Add(this.homePhoneTextbox.Text, TelephoneNumberSingleType.Home);

                    if (this.mobilePhoneTextbox.Text != string.Empty)
                        card.TelephoneNumbers.Add(this.mobilePhoneTextbox.Text, TelephoneNumberSingleType.Cellular);

                    if (this.emailTextbox.Text != string.Empty)
                        card.EmailAddresses.Add(this.emailTextbox.Text);

                    if (this.birthdayDatePicker.Value != DateTime.Now)
                        card.Birthday = this.birthdayDatePicker.Value;

                    if (this.photoPictureBox.Image != null)
                    {
                        Image image = this.photoPictureBox.Image;
                        MemoryStream stream = new MemoryStream();
                        image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        card.Photo = stream.ToArray();
                    }

                    card.SaveToFile(this.savevCardDialog.FileName);

                    MessageBox.Show(string.Format("The file {0} was saved successfully.", Path.GetFileName(this.savevCardDialog.FileName)));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while trying to save the vCard file.");
                }
            }
        }

        private void selectPhotoButton_Click(object sender, EventArgs e)
        {
            DialogResult result = this.selectPhotoDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.photoPictureBox.Image = Image.FromFile(this.selectPhotoDialog.FileName);
            }
        }

        private void createNewButton_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            this.firstNameTextbox.Text = string.Empty;
            this.lastNameTextbox.Text = string.Empty;
            this.companyTextbox.Text = string.Empty;
            this.officePhoneTextbox.Text = string.Empty;
            this.homePhoneTextbox.Text = string.Empty;
            this.mobilePhoneTextbox.Text = string.Empty;
            this.emailTextbox.Text = string.Empty;
            this.birthdayDatePicker.Value = DateTime.Now;
            this.rawDataTextbox.Text = string.Empty;
        }

    }
}
