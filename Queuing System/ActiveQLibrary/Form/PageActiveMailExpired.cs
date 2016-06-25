using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace ActiveQLibrary.Form
{
	/// <summary>
	/// Summary description for PageActiveMailExpired.
	/// </summary>
	public class PageActiveMailExpired : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button _bOK;
		private System.Windows.Forms.LinkLabel _linkActiveUp;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PageActiveMailExpired()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PageActiveMailExpired));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this._bOK = new System.Windows.Forms.Button();
			this._linkActiveUp = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Bitmap)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 11);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(40, 40);
			this.pictureBox1.TabIndex = 9;
			this.pictureBox1.TabStop = false;
			// 
			// _bOK
			// 
			this._bOK.BackColor = System.Drawing.Color.Transparent;
			this._bOK.Location = new System.Drawing.Point(244, 70);
			this._bOK.Name = "_bOK";
			this._bOK.TabIndex = 7;
			this._bOK.Text = "OK";
			this._bOK.Click += new System.EventHandler(this._bOK_Click);
			// 
			// _linkActiveUp
			// 
			this._linkActiveUp.Location = new System.Drawing.Point(280, 48);
			this._linkActiveUp.Name = "_linkActiveUp";
			this._linkActiveUp.Size = new System.Drawing.Size(135, 16);
			this._linkActiveUp.TabIndex = 6;
			this._linkActiveUp.TabStop = true;
			this._linkActiveUp.Text = "http://www.activeup.com";
			this._linkActiveUp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkActiveUp_LinkClicked);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(56, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(528, 56);
			this.label1.TabIndex = 5;
			this.label1.Text = @"ActiveQ do not hold the ActiveMail DLL expired to use the mail features. If you own a license of ActiveMail, simply copy the DLL in the directory where you installed ActiveQ. Ensure that the version of the DLL is v1.6+. If you don’t own a license, perhaps you would like to make a try and download a free trial version of the component on our website:";
			// 
			// PageActiveMailExpired
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(568, 102);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pictureBox1,
																		  this._bOK,
																		  this._linkActiveUp,
																		  this.label1});
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PageActiveMailExpired";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ActiveQ : ActiveMail expired";
			this.ResumeLayout(false);

		}
		#endregion

		private void _linkActiveUp_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("IExplore"," http://www.activeup.com");
		}

		private void _bOK_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
