// Copyright 2001-2010 - Active Up SPRLU (http://www.agilecomponents.com)
//
// This file is part of MailSystem.NET.
// MailSystem.NET is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// MailSystem.NET is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with SharpMap; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ActiveQConfiguration
{
	/// <summary>
	/// Description résumée de PageActiveMailNotFound.
	/// </summary>
	public class PageActiveMailNotFound : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel _linkActiveUp;
		private System.Windows.Forms.Button _bOK;
		private System.Windows.Forms.PictureBox pictureBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PageActiveMailNotFound()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PageActiveMailNotFound));
			this.label1 = new System.Windows.Forms.Label();
			this._linkActiveUp = new System.Windows.Forms.LinkLabel();
			this._bOK = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(56, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(528, 56);
			this.label1.TabIndex = 0;
			this.label1.Text = @"ActiveQ do not hold the ActiveMail DLL required to use the mail features. If you own a license of ActiveMail, simply copy the DLL in the directory where you installed ActiveQ. Ensure that the version of the DLL is v1.6+. If you don’t own a license, perhaps you would like to make a try and download a free trial version of the component on our website:";
			// 
			// _linkActiveUp
			// 
			this._linkActiveUp.Location = new System.Drawing.Point(276, 53);
			this._linkActiveUp.Name = "_linkActiveUp";
			this._linkActiveUp.Size = new System.Drawing.Size(135, 16);
			this._linkActiveUp.TabIndex = 1;
			this._linkActiveUp.TabStop = true;
			this._linkActiveUp.Text = "http://www.activeup.com";
			this._linkActiveUp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkActiveUp_LinkClicked);
			// 
			// _bOK
			// 
			this._bOK.BackColor = System.Drawing.Color.Transparent;
			this._bOK.Location = new System.Drawing.Point(256, 75);
			this._bOK.Name = "_bOK";
			this._bOK.TabIndex = 2;
			this._bOK.Text = "OK";
			this._bOK.Click += new System.EventHandler(this._bOK_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox1.Image = ((System.Drawing.Bitmap)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 16);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(40, 40);
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;
			// 
			// PageActiveMailNotFound
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(570, 104);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.pictureBox1,
																		  this._bOK,
																		  this._linkActiveUp,
																		  this.label1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PageActiveMailNotFound";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ActiveQ : ActiveMail not found";
			this.Load += new System.EventHandler(this.PageActiveMailExpired_Load);
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

		private void PageActiveMailExpired_Load(object sender, System.EventArgs e)
		{
			
		}
	}
}
