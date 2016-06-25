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

using ActiveQLibrary.Serialization.ConfigGlobal;

namespace ActiveQConfiguration
{
	/// <summary>
	/// Description résumée de SmtpServerForm.
	/// </summary>
	public class SmtpServerForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.TextBox _tbPassword;
		private System.Windows.Forms.TextBox _tbUsername;
		private System.Windows.Forms.TextBox _tbServer;
		private ActiveQLibrary.CustomControl.NumEdit _tbPort;
		private ActiveQLibrary.CustomControl.ButtonXP _bOK;
		private ActiveQLibrary.CustomControl.ButtonXP _bTest;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolTip _tolltipTest;
		private ActiveQLibrary.CustomControl.ButtonXP _bCancel;

		private SmtpServer _smtpServer = null;

		public SmtpServerForm()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
		}

		public SmtpServerForm(SmtpServer smtpServer)
		{
			InitializeComponent();

			_smtpServer = smtpServer;
		}

		/// <summary>
		/// Nettoyage des ressources utilisées.
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
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SmtpServerForm));
			this.panel5 = new System.Windows.Forms.Panel();
			this._tbPort = new ActiveQLibrary.CustomControl.NumEdit();
			this.label4 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this._tbPassword = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this._tbUsername = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this._tbServer = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.panel6 = new System.Windows.Forms.Panel();
			this._bTest = new ActiveQLibrary.CustomControl.ButtonXP();
			this._bCancel = new ActiveQLibrary.CustomControl.ButtonXP();
			this._bOK = new ActiveQLibrary.CustomControl.ButtonXP();
			this._tolltipTest = new System.Windows.Forms.ToolTip(this.components);
			this.panel5.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel6.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel5
			// 
			this.panel5.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.panel5.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this._tbPort,
																				 this.label4});
			this.panel5.Location = new System.Drawing.Point(8, 48);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(280, 32);
			this.panel5.TabIndex = 1;
			// 
			// _tbPort
			// 
			this._tbPort.InputType = ActiveQLibrary.CustomControl.NumEdit.NumEditType.Integer;
			this._tbPort.Location = new System.Drawing.Point(69, 6);
			this._tbPort.Name = "_tbPort";
			this._tbPort.Size = new System.Drawing.Size(49, 20);
			this._tbPort.TabIndex = 1;
			this._tbPort.Text = "";
			this._tbPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this._tbPort_KeyDown);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(64, 16);
			this.label4.TabIndex = 0;
			this.label4.Text = "Port           :";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.panel4,
																				 this.panel3,
																				 this.panel5,
																				 this.panel2});
			this.panel1.Location = new System.Drawing.Point(4, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(296, 168);
			this.panel1.TabIndex = 0;
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.panel4.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this._tbPassword,
																				 this.label3});
			this.panel4.Location = new System.Drawing.Point(8, 128);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(280, 32);
			this.panel4.TabIndex = 3;
			// 
			// _tbPassword
			// 
			this._tbPassword.Location = new System.Drawing.Point(68, 6);
			this._tbPassword.Name = "_tbPassword";
			this._tbPassword.PasswordChar = '*';
			this._tbPassword.Size = new System.Drawing.Size(204, 20);
			this._tbPassword.TabIndex = 1;
			this._tbPassword.Text = "";
			this._tbPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this._tbPassword_KeyDown);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "Password  :";
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.panel3.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this._tbUsername,
																				 this.label2});
			this.panel3.Location = new System.Drawing.Point(8, 88);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(280, 32);
			this.panel3.TabIndex = 2;
			// 
			// _tbUsername
			// 
			this._tbUsername.Location = new System.Drawing.Point(68, 6);
			this._tbUsername.Name = "_tbUsername";
			this._tbUsername.Size = new System.Drawing.Size(204, 20);
			this._tbUsername.TabIndex = 1;
			this._tbUsername.Text = "";
			this._tbUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this._tbUsername_KeyDown);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 16);
			this.label2.TabIndex = 0;
			this.label2.Text = "Username :";
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.panel2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this._tbServer,
																				 this.label1});
			this.panel2.Location = new System.Drawing.Point(8, 8);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(280, 32);
			this.panel2.TabIndex = 0;
			// 
			// _tbServer
			// 
			this._tbServer.Location = new System.Drawing.Point(68, 6);
			this._tbServer.Name = "_tbServer";
			this._tbServer.Size = new System.Drawing.Size(204, 20);
			this._tbServer.TabIndex = 1;
			this._tbServer.Text = "";
			this._tbServer.KeyDown += new System.Windows.Forms.KeyEventHandler(this._tbServer_KeyDown);
			this._tbServer.TextChanged += new System.EventHandler(this._tbServer_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Host          :";
			// 
			// panel6
			// 
			this.panel6.BackColor = System.Drawing.Color.LightSteelBlue;
			this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel6.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this._bTest,
																				 this._bCancel,
																				 this._bOK});
			this.panel6.Location = new System.Drawing.Point(4, 176);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(296, 40);
			this.panel6.TabIndex = 1;
			// 
			// _bTest
			// 
			this._bTest._Image = null;
			this._bTest.DefaultScheme = false;
			this._bTest.Image = null;
			this._bTest.Location = new System.Drawing.Point(5, 2);
			this._bTest.Name = "_bTest";
			this._bTest.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Silver;
			this._bTest.Size = new System.Drawing.Size(97, 32);
			this._bTest.SizeImgButton = new System.Drawing.Size(0, 0);
			this._bTest.TabIndex = 2;
			this._bTest.Text = "Test";
			this._bTest.Click += new System.EventHandler(this._bTest_Click);
			// 
			// _bCancel
			// 
			this._bCancel._Image = null;
			this._bCancel.DefaultScheme = false;
			this._bCancel.Image = ((System.Drawing.Bitmap)(resources.GetObject("_bCancel.Image")));
			this._bCancel.Location = new System.Drawing.Point(200, 3);
			this._bCancel.Name = "_bCancel";
			this._bCancel.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Silver;
			this._bCancel.Size = new System.Drawing.Size(90, 32);
			this._bCancel.SizeImgButton = new System.Drawing.Size(0, 0);
			this._bCancel.TabIndex = 1;
			this._bCancel.Text = "Cancel";
			this._bCancel.Click += new System.EventHandler(this._bCancel_Click);
			// 
			// _bOK
			// 
			this._bOK._Image = null;
			this._bOK.DefaultScheme = false;
			this._bOK.Image = ((System.Drawing.Bitmap)(resources.GetObject("_bOK.Image")));
			this._bOK.Location = new System.Drawing.Point(127, 3);
			this._bOK.Name = "_bOK";
			this._bOK.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Silver;
			this._bOK.Size = new System.Drawing.Size(73, 32);
			this._bOK.SizeImgButton = new System.Drawing.Size(0, 0);
			this._bOK.TabIndex = 0;
			this._bOK.Text = "OK";
			this._bOK.Click += new System.EventHandler(this._bOK_Click);
			// 
			// SmtpServerForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(303, 220);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel6,
																		  this.panel1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SmtpServerForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "SMTP server";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SmtpServerForm_KeyDown);
			this.Load += new System.EventHandler(this.SmtpServerForm_Load);
			this.panel5.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel6.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void _bTest_Click(object sender, System.EventArgs e)
		{
			
			if (_tbPort.Text.Trim() != "")
				Global.TestSmtpServer(_tbServer.Text,Int32.Parse(_tbPort.Text),_tbUsername.Text,_tbPassword.Text);
			else
				Global.TestSmtpServer(_tbServer.Text,25,_tbUsername.Text,_tbPassword.Text);
		}

		private void SmtpServerForm_Load(object sender, System.EventArgs e)
		{
			
			string toolTipText = "";

			switch (Global.StateActiveMail)
			{
				case StateLibraryActiveMail.notfound:
				{
					toolTipText = @"ActiveQ do not hold the ActiveMail DLL required to use the mail features. If you own a license of ActiveMail, simply copy the DLL in the directory where you installed ActiveQ. Ensure that the version of the DLL is v1.4+. If you don’t own a license, perhaps you would like to make a try and download a free trial version of the component on our website: http://www.activeup.com";	
					_tolltipTest.SetToolTip(_bTest,toolTipText);
					_bTest.Enabled = false;
				} break;

				case StateLibraryActiveMail.expired:
				{
					toolTipText = @"ActiveQ do not hold the ActiveMail DLL expired to use the mail features. If you own a license of ActiveMail, simply copy the DLL in the directory where you installed ActiveQ. Ensure that the version of the DLL is v1.4+. If you don’t own a license, perhaps you would like to make a try and download a free trial version of the component on our website: http://www.activeup.com";
					_tolltipTest.SetToolTip(_bTest,toolTipText);
					_bTest.Enabled = false;
				} break;

				case  StateLibraryActiveMail.ok:
				{
					toolTipText = "Test if all the parameters of the stmp server are correct and works";
					_tolltipTest.SetToolTip(_bTest,toolTipText);
					_bTest.Enabled = false;

				} break;

				default : break;
			}

			if (_smtpServer != null)
			{
				this._tbServer.Text = _smtpServer.Host;
				this._tbPort.Text = _smtpServer.Port.ToString();
				this._tbUsername.Text = _smtpServer.Username;
				this._tbPassword.Text = _smtpServer.Password;
				this._bOK.Enabled = true;
			}
			else
				this._bOK.Enabled = false;
		}

		private void _bOK_Click(object sender, System.EventArgs e)
		{
			OK();
		}

		private void _bCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void _tbServer_TextChanged(object sender, System.EventArgs e)
		{
			if (_tbServer.Text.Trim() == "")
			{
				_bTest.Enabled = false;
				_bOK.Enabled = false;
			}
			else
			{
				_bTest.Enabled = true;
				_bOK.Enabled = true;
			}
		}

		private void SmtpServerForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				OK();
		}

		private void _tbServer_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				OK();
				
		}

		private void _tbPort_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				OK();
		}

		private void _tbUsername_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				OK();
		}

		private void _tbPassword_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				OK();
		}

		private void OK()
		{
			if (_bOK.Enabled == true)
			{
				if (_tbPort.Text.Trim() == "")
					_tbPort.Text = "25";

				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		#region Properties

		public string Host
		{
			get
			{
				return _tbServer.Text;
			}
		}

		public int Port
		{
			get
			{
				return Int32.Parse(_tbPort.Text);
			}
		}

		public string Username
		{
			get
			{
				return _tbUsername.Text;
			}

		}

		public string Password
		{
			get
			{
				return _tbPassword.Text;
			}
		}

		#endregion
	}
}
