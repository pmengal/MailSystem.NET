namespace ActiveUp.Net.Samples.CompactSP.DNS
{
    partial class frmDNSResult
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
            this.lvwResults = new System.Windows.Forms.ListView();
            this.colValue = new System.Windows.Forms.ColumnHeader();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.SuspendLayout();
            // 
            // lvwResults
            // 
            this.lvwResults.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.lvwResults.Columns.Add(this.colValue);
            this.lvwResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwResults.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
            this.lvwResults.FullRowSelect = true;
            this.lvwResults.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwResults.Location = new System.Drawing.Point(0, 0);
            this.lvwResults.Name = "lvwResults";
            this.lvwResults.Size = new System.Drawing.Size(240, 266);
            this.lvwResults.TabIndex = 0;
            this.lvwResults.View = System.Windows.Forms.View.Details;
            this.lvwResults.ItemActivate += new System.EventHandler(this.lvwResults_ItemActivate);
            // 
            // colValue
            // 
            this.colValue.Text = "Value";
            this.colValue.Width = 230;
            // 
            // frmDNSResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(131F, 131F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 266);
            this.Controls.Add(this.lvwResults);
            this.Menu = this.mainMenu1;
            this.Name = "frmDNSResult";
            this.Text = "Results";
            this.Load += new System.EventHandler(this.frmDNSResult_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwResults;
        private System.Windows.Forms.ColumnHeader colValue;
        private System.Windows.Forms.MainMenu mainMenu1;
    }
}