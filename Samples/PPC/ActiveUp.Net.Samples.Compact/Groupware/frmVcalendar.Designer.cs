namespace ActiveUp.Net.Samples.Compact
{
    partial class frmVcalendar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnReadCard = new System.Windows.Forms.Button();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.btnWrite = new System.Windows.Forms.Button();
            this.pnlInterface = new System.Windows.Forms.Panel();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.pnlInterface.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReadCard
            // 
            this.btnReadCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReadCard.Location = new System.Drawing.Point(0, 0);
            this.btnReadCard.Name = "btnReadCard";
            this.btnReadCard.Size = new System.Drawing.Size(238, 33);
            this.btnReadCard.TabIndex = 4;
            this.btnReadCard.Text = "Read Calendar";
            this.btnReadCard.Click += new System.EventHandler(this.btnReadCard_Click_1);
            // 
            // dlgOpen
            // 
            this.dlgOpen.Filter = "vCal Files|*.ics";
            this.dlgOpen.InitialDirectory = "\\Storage Card\\";
            // 
            // btnWrite
            // 
            this.btnWrite.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnWrite.Location = new System.Drawing.Point(0, 262);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(238, 33);
            this.btnWrite.TabIndex = 5;
            this.btnWrite.Text = "Write Calendar";
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click_1);
            // 
            // pnlInterface
            // 
            this.pnlInterface.Controls.Add(this.dtEnd);
            this.pnlInterface.Controls.Add(this.dtStart);
            this.pnlInterface.Controls.Add(this.label4);
            this.pnlInterface.Controls.Add(this.label3);
            this.pnlInterface.Controls.Add(this.txtDescription);
            this.pnlInterface.Controls.Add(this.label2);
            this.pnlInterface.Controls.Add(this.txtName);
            this.pnlInterface.Controls.Add(this.label1);
            this.pnlInterface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInterface.Location = new System.Drawing.Point(0, 0);
            this.pnlInterface.Name = "pnlInterface";
            this.pnlInterface.Size = new System.Drawing.Size(238, 295);
            // 
            // dlgSave
            // 
            this.dlgSave.Filter = "vCal Files|*.ics";
            this.dlgSave.InitialDirectory = "\\Storage Card\\";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 24);
            this.label1.Text = "Event Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(94, 44);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(141, 23);
            this.txtName.TabIndex = 1;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(94, 73);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(141, 102);
            this.txtDescription.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 24);
            this.label2.Text = "Description";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 24);
            this.label3.Text = "Start";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 24);
            this.label4.Text = "End";
            // 
            // dtStart
            // 
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtStart.Location = new System.Drawing.Point(94, 179);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(99, 24);
            this.dtStart.TabIndex = 11;
            // 
            // dtEnd
            // 
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtEnd.Location = new System.Drawing.Point(94, 204);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(99, 24);
            this.dtEnd.TabIndex = 12;
            // 
            // frmVcalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.btnReadCard);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.pnlInterface);
            this.Name = "frmVcalendar";
            this.Text = "Calendar";
            this.pnlInterface.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReadCard;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Panel pnlInterface;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.DateTimePicker dtEnd;
    }
}