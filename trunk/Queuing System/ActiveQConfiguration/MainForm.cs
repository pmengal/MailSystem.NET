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
using System.Reflection;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Diagnostics;


using ActiveQLibrary;
using ActiveQLibrary.Serialization.ConfigGlobal;

namespace ActiveQConfiguration
{


	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{

		/// <summary>
		/// Required designer variable.
		/// </summary>
		/// 

		private static LinearGradientBrush brush;
		private System.ComponentModel.IContainer components;

		private ActiveQConfiguration.ManageElement _meXmlFile;
		private ActiveQConfiguration.ManageElement _meMailDir;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox _cbDeleteWhenProcessed;
		private ActiveQLibrary.CustomControl.NumEdit _tbIntervalTask;
		private ActiveQLibrary.CustomControl.NumEdit _tbIntervalMail;
		private ActiveQLibrary.CustomControl.NumEdit _tbWorker;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private ActiveQLibrary.CustomControl.ButtonXP _bOk;
		private ActiveQLibrary.CustomControl.ButtonXP _bApply;
		private ActiveQLibrary.CustomControl.ButtonXP _bReset;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel8;
		private System.Windows.Forms.Panel panel9;
		private System.Windows.Forms.Panel panel11;
		private System.Windows.Forms.ToolTip _toolTipOk;
		private System.Windows.Forms.ToolTip _toolTipApply;
		private System.Windows.Forms.ToolTip _toolTipReset;
		private ActiveQConfiguration.ManageElementSmtpServer _meSmtpServer;
		private System.Windows.Forms.ToolTip _toolTipErrorLog;
		private System.Windows.Forms.ToolTip _toolTipEventLog;
		private System.Windows.Forms.ToolTip _toolTipIntervalMail;
		private System.Windows.Forms.ToolTip _toolTipIntervalTask;
		private System.Windows.Forms.ToolTip _toolTipSimJobs;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Panel panel6;
		private ActiveQLibrary.CustomControl.NumEdit _tbMaxBytesEvent;
		private ActiveQLibrary.CustomControl.NumEdit _tbMaxBytesError;
		private System.Windows.Forms.TextBox _tbActiveMailLicense;

		private string _configFile = ActiveQLibrary.Global.GetImagePath(Assembly.GetExecutingAssembly().Location) + @"\Config.xml";		

		//private PageConfigGlobal _pageConfigGlobal = new PageConfigGlobal();

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			/*Controls.AddRange(new Control[] {
												_pageConfigGlobal
											} );*/

			Global.Initialize();

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this._meXmlFile = new ActiveQConfiguration.ManageElement();
			this._meMailDir = new ActiveQConfiguration.ManageElement();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this._cbDeleteWhenProcessed = new System.Windows.Forms.CheckBox();
			this._tbIntervalTask = new ActiveQLibrary.CustomControl.NumEdit();
			this._tbIntervalMail = new ActiveQLibrary.CustomControl.NumEdit();
			this._tbWorker = new ActiveQLibrary.CustomControl.NumEdit();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this._bOk = new ActiveQLibrary.CustomControl.ButtonXP();
			this._bApply = new ActiveQLibrary.CustomControl.ButtonXP();
			this._bReset = new ActiveQLibrary.CustomControl.ButtonXP();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel7 = new System.Windows.Forms.Panel();
			this._tbActiveMailLicense = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.panel6 = new System.Windows.Forms.Panel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.label8 = new System.Windows.Forms.Label();
			this._tbMaxBytesEvent = new ActiveQLibrary.CustomControl.NumEdit();
			this._tbMaxBytesError = new ActiveQLibrary.CustomControl.NumEdit();
			this.label9 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel8 = new System.Windows.Forms.Panel();
			this.panel9 = new System.Windows.Forms.Panel();
			this._meSmtpServer = new ActiveQConfiguration.ManageElementSmtpServer();
			this.panel11 = new System.Windows.Forms.Panel();
			this._toolTipOk = new System.Windows.Forms.ToolTip(this.components);
			this._toolTipReset = new System.Windows.Forms.ToolTip(this.components);
			this._toolTipApply = new System.Windows.Forms.ToolTip(this.components);
			this._toolTipErrorLog = new System.Windows.Forms.ToolTip(this.components);
			this._toolTipEventLog = new System.Windows.Forms.ToolTip(this.components);
			this._toolTipIntervalMail = new System.Windows.Forms.ToolTip(this.components);
			this._toolTipIntervalTask = new System.Windows.Forms.ToolTip(this.components);
			this._toolTipSimJobs = new System.Windows.Forms.ToolTip(this.components);
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.panel7.SuspendLayout();
			this.panel6.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel8.SuspendLayout();
			this.panel9.SuspendLayout();
			this.panel11.SuspendLayout();
			this.panel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// _meXmlFile
			// 
			this._meXmlFile.FirstColor = System.Drawing.Color.Transparent;
			this._meXmlFile.FontText = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this._meXmlFile.LastColor = System.Drawing.SystemColors.Window;
			this._meXmlFile.Location = new System.Drawing.Point(8, 8);
			this._meXmlFile.Name = "_meXmlFile";
			this._meXmlFile.Pen = false;
			this._meXmlFile.PenColor = System.Drawing.Color.Silver;
			this._meXmlFile.Size = new System.Drawing.Size(400, 173);
			this._meXmlFile.SizePen = 0;
			this._meXmlFile.TabIndex = 9;
			this._meXmlFile.TextAddButtonToolTip = "Add a xml file";
			this._meXmlFile.TextColor = System.Drawing.Color.DimGray;
			this._meXmlFile.TextContents = "XML Tasks";
			this._meXmlFile.TextRemoveButtonToolTip = "Remove a xml file";
			this._meXmlFile.TypeAdd = ActiveQConfiguration.ManageElement.TypeAddition.fileXml;
			// 
			// _meMailDir
			// 
			this._meMailDir.FirstColor = System.Drawing.Color.Transparent;
			this._meMailDir.FontText = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this._meMailDir.LastColor = System.Drawing.SystemColors.Window;
			this._meMailDir.Location = new System.Drawing.Point(8, 8);
			this._meMailDir.Name = "_meMailDir";
			this._meMailDir.Pen = false;
			this._meMailDir.PenColor = System.Drawing.Color.Silver;
			this._meMailDir.Size = new System.Drawing.Size(400, 174);
			this._meMailDir.SizePen = 0;
			this._meMailDir.TabIndex = 8;
			this._meMailDir.TextAddButtonToolTip = "Add a directoy";
			this._meMailDir.TextColor = System.Drawing.Color.DimGray;
			this._meMailDir.TextContents = "Mail directories";
			this._meMailDir.TextRemoveButtonToolTip = "Remove a directoy";
			this._meMailDir.TypeAdd = ActiveQConfiguration.ManageElement.TypeAddition.dir;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(328, 8);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(32, 16);
			this.label7.TabIndex = 40;
			this.label7.Text = "sec.";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(152, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(32, 16);
			this.label6.TabIndex = 39;
			this.label6.Text = "sec.";
			// 
			// _cbDeleteWhenProcessed
			// 
			this._cbDeleteWhenProcessed.Location = new System.Drawing.Point(168, 8);
			this._cbDeleteWhenProcessed.Name = "_cbDeleteWhenProcessed";
			this._cbDeleteWhenProcessed.Size = new System.Drawing.Size(168, 16);
			this._cbDeleteWhenProcessed.TabIndex = 34;
			this._cbDeleteWhenProcessed.Text = "Delete mail when processed";
			// 
			// _tbIntervalTask
			// 
			this._tbIntervalTask.InputType = ActiveQLibrary.CustomControl.NumEdit.NumEditType.Integer;
			this._tbIntervalTask.Location = new System.Drawing.Point(288, 5);
			this._tbIntervalTask.Name = "_tbIntervalTask";
			this._tbIntervalTask.Size = new System.Drawing.Size(32, 20);
			this._tbIntervalTask.TabIndex = 32;
			this._tbIntervalTask.Text = "";
			// 
			// _tbIntervalMail
			// 
			this._tbIntervalMail.InputType = ActiveQLibrary.CustomControl.NumEdit.NumEditType.Integer;
			this._tbIntervalMail.Location = new System.Drawing.Point(112, 6);
			this._tbIntervalMail.Name = "_tbIntervalMail";
			this._tbIntervalMail.Size = new System.Drawing.Size(32, 20);
			this._tbIntervalMail.TabIndex = 30;
			this._tbIntervalMail.Text = "";
			// 
			// _tbWorker
			// 
			this._tbWorker.InputType = ActiveQLibrary.CustomControl.NumEdit.NumEditType.Integer;
			this._tbWorker.Location = new System.Drawing.Point(112, 5);
			this._tbWorker.Name = "_tbWorker";
			this._tbWorker.Size = new System.Drawing.Size(43, 20);
			this._tbWorker.TabIndex = 28;
			this._tbWorker.Text = "";
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Location = new System.Drawing.Point(184, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 16);
			this.label3.TabIndex = 31;
			this.label3.Text = "Interval check task : ";
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 16);
			this.label2.TabIndex = 29;
			this.label2.Text = "Interval check mail : ";
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 16);
			this.label1.TabIndex = 27;
			this.label1.Text = "Simultaneous jobs : ";
			// 
			// _bOk
			// 
			this._bOk._Image = null;
			this._bOk.DefaultScheme = false;
			this._bOk.Image = ((System.Drawing.Bitmap)(resources.GetObject("_bOk.Image")));
			this._bOk.Location = new System.Drawing.Point(242, 2);
			this._bOk.Name = "_bOk";
			this._bOk.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Silver;
			this._bOk.Size = new System.Drawing.Size(80, 30);
			this._bOk.SizeImgButton = new System.Drawing.Size(24, 24);
			this._bOk.TabIndex = 43;
			this._bOk.Text = "Ok";
			this._bOk.Click += new System.EventHandler(this._bOk_Click);
			// 
			// _bApply
			// 
			this._bApply._Image = null;
			this._bApply.DefaultScheme = false;
			this._bApply.Image = ((System.Drawing.Bitmap)(resources.GetObject("_bApply.Image")));
			this._bApply.Location = new System.Drawing.Point(144, 2);
			this._bApply.Name = "_bApply";
			this._bApply.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Silver;
			this._bApply.Size = new System.Drawing.Size(88, 30);
			this._bApply.SizeImgButton = new System.Drawing.Size(24, 24);
			this._bApply.TabIndex = 42;
			this._bApply.Text = "Apply";
			this._bApply.Click += new System.EventHandler(this._bApply_Click);
			// 
			// _bReset
			// 
			this._bReset._Image = null;
			this._bReset.DefaultScheme = false;
			this._bReset.Image = ((System.Drawing.Bitmap)(resources.GetObject("_bReset.Image")));
			this._bReset.Location = new System.Drawing.Point(46, 2);
			this._bReset.Name = "_bReset";
			this._bReset.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Silver;
			this._bReset.Size = new System.Drawing.Size(88, 30);
			this._bReset.SizeImgButton = new System.Drawing.Size(0, 0);
			this._bReset.TabIndex = 41;
			this._bReset.Text = "Reset";
			this._bReset.Click += new System.EventHandler(this._bReset_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.panel7,
																				 this.panel6,
																				 this.panel3,
																				 this.panel2});
			this.panel1.Location = new System.Drawing.Point(8, 8);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(384, 168);
			this.panel1.TabIndex = 44;
			// 
			// panel7
			// 
			this.panel7.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel7.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this._tbActiveMailLicense,
																				 this.label10});
			this.panel7.Location = new System.Drawing.Point(8, 128);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(368, 32);
			this.panel7.TabIndex = 5;
			// 
			// _tbActiveMailLicense
			// 
			this._tbActiveMailLicense.Location = new System.Drawing.Point(106, 5);
			this._tbActiveMailLicense.Name = "_tbActiveMailLicense";
			this._tbActiveMailLicense.Size = new System.Drawing.Size(252, 20);
			this._tbActiveMailLicense.TabIndex = 40;
			this._tbActiveMailLicense.Text = "";
			// 
			// label10
			// 
			this.label10.BackColor = System.Drawing.Color.Transparent;
			this.label10.Location = new System.Drawing.Point(6, 8);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(106, 16);
			this.label10.TabIndex = 39;
			this.label10.Text = "ActiveMail license :";
			// 
			// panel6
			// 
			this.panel6.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel6.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.panel5,
																				 this.label4,
																				 this.label5});
			this.panel6.Location = new System.Drawing.Point(8, 88);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(368, 32);
			this.panel6.TabIndex = 4;
			// 
			// panel5
			// 
			this.panel5.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel5.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.label8,
																				 this._tbMaxBytesEvent,
																				 this._tbMaxBytesError,
																				 this.label9});
			this.panel5.Location = new System.Drawing.Point(-1, -1);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(368, 32);
			this.panel5.TabIndex = 40;
			// 
			// label8
			// 
			this.label8.BackColor = System.Drawing.Color.Transparent;
			this.label8.Location = new System.Drawing.Point(6, 8);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(121, 16);
			this.label8.TabIndex = 39;
			this.label8.Text = "Max size event logfile :";
			// 
			// _tbMaxBytesEvent
			// 
			this._tbMaxBytesEvent.InputType = ActiveQLibrary.CustomControl.NumEdit.NumEditType.Integer;
			this._tbMaxBytesEvent.Location = new System.Drawing.Point(127, 5);
			this._tbMaxBytesEvent.Name = "_tbMaxBytesEvent";
			this._tbMaxBytesEvent.Size = new System.Drawing.Size(52, 20);
			this._tbMaxBytesEvent.TabIndex = 36;
			this._tbMaxBytesEvent.Text = "";
			// 
			// _tbMaxBytesError
			// 
			this._tbMaxBytesError.InputType = ActiveQLibrary.CustomControl.NumEdit.NumEditType.Integer;
			this._tbMaxBytesError.Location = new System.Drawing.Point(302, 5);
			this._tbMaxBytesError.Name = "_tbMaxBytesError";
			this._tbMaxBytesError.Size = new System.Drawing.Size(52, 20);
			this._tbMaxBytesError.TabIndex = 38;
			this._tbMaxBytesError.Text = "";
			// 
			// label9
			// 
			this.label9.BackColor = System.Drawing.Color.Transparent;
			this.label9.Location = new System.Drawing.Point(182, 9);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(116, 16);
			this.label9.TabIndex = 37;
			this.label9.Text = "Max size error logfile :";
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Location = new System.Drawing.Point(6, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(121, 16);
			this.label4.TabIndex = 39;
			this.label4.Text = "Max size event logfile :";
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Location = new System.Drawing.Point(182, 9);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(116, 16);
			this.label5.TabIndex = 37;
			this.label5.Text = "Max size error logfile :";
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel3.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this._tbIntervalMail,
																				 this.label6,
																				 this.label2,
																				 this.label7,
																				 this._tbIntervalTask,
																				 this.label3});
			this.panel3.Location = new System.Drawing.Point(8, 48);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(368, 32);
			this.panel3.TabIndex = 1;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(224)), ((System.Byte)(224)));
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this._tbWorker,
																				 this.label1,
																				 this._cbDeleteWhenProcessed});
			this.panel2.Location = new System.Drawing.Point(8, 8);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(368, 32);
			this.panel2.TabIndex = 0;
			// 
			// panel8
			// 
			this.panel8.BackColor = System.Drawing.Color.LightSteelBlue;
			this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel8.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this._meMailDir});
			this.panel8.Location = new System.Drawing.Point(402, 8);
			this.panel8.Name = "panel8";
			this.panel8.Size = new System.Drawing.Size(416, 188);
			this.panel8.TabIndex = 45;
			// 
			// panel9
			// 
			this.panel9.BackColor = System.Drawing.Color.LightSteelBlue;
			this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel9.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this._meXmlFile});
			this.panel9.Location = new System.Drawing.Point(402, 204);
			this.panel9.Name = "panel9";
			this.panel9.Size = new System.Drawing.Size(416, 188);
			this.panel9.TabIndex = 46;
			// 
			// _meSmtpServer
			// 
			this._meSmtpServer.FirstColor = System.Drawing.Color.Transparent;
			this._meSmtpServer.FontText = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this._meSmtpServer.LastColor = System.Drawing.Color.White;
			this._meSmtpServer.Location = new System.Drawing.Point(5, 6);
			this._meSmtpServer.Name = "_meSmtpServer";
			this._meSmtpServer.Pen = false;
			this._meSmtpServer.PenColor = System.Drawing.Color.Silver;
			this._meSmtpServer.Size = new System.Drawing.Size(368, 152);
			this._meSmtpServer.SizePen = 0;
			this._meSmtpServer.TabIndex = 0;
			this._meSmtpServer.TextAddButtonToolTip = "Add a SMTP server";
			this._meSmtpServer.TextColor = System.Drawing.Color.DimGray;
			this._meSmtpServer.TextContents = "SMTP Server";
			this._meSmtpServer.TextRemoveButtonToolTip = "Remove a SMTP server";
			this._meSmtpServer.TextTestToolTip = "Test a SMTP server";
			// 
			// panel11
			// 
			this.panel11.BackColor = System.Drawing.Color.LightSteelBlue;
			this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel11.Controls.AddRange(new System.Windows.Forms.Control[] {
																				  this._bReset,
																				  this._bApply,
																				  this._bOk});
			this.panel11.Location = new System.Drawing.Point(8, 356);
			this.panel11.Name = "panel11";
			this.panel11.Size = new System.Drawing.Size(384, 37);
			this.panel11.TabIndex = 48;
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.Color.LightSteelBlue;
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel4.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this._meSmtpServer});
			this.panel4.Location = new System.Drawing.Point(8, 185);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(385, 165);
			this.panel4.TabIndex = 49;
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(822, 397);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel4,
																		  this.panel11,
																		  this.panel9,
																		  this.panel8,
																		  this.panel1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ActiveQ Configuration";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this._meSmtpServer_Paint);
			this.panel1.ResumeLayout(false);
			this.panel7.ResumeLayout(false);
			this.panel6.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel8.ResumeLayout(false);
			this.panel9.ResumeLayout(false);
			this.panel11.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());

			/*Process instance = RunningInstance();
			if (instance == null)
			{
				Application.Run(new MainForm());
			}

			else
			{
				MessageBox.Show("ActiveQ Configuration is already started.","ActiveQConfiguration",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}*/
		}

		private void _meSmtpServer_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			/*brush = new LinearGradientBrush(new Rectangle(this.ClientRectangle.X,this.ClientRectangle.Y,this.ClientRectangle.Width,this.ClientRectangle.Height), System.Drawing.SystemColors.Info, System.Drawing.SystemColors.Window, 90.0f); 

			float[] relativeIntensities = {0.0f, 0.3f, 1.0f};
			float[] relativePositions   = {0.0f, 0.7f, 1.0f};

			Blend blend = new Blend();
			blend.Factors = relativeIntensities;
			blend.Positions = relativePositions;
			brush.Blend = blend;


			e.Graphics.FillRectangle(brush,this.ClientRectangle.X,this.ClientRectangle.Y,this.ClientRectangle.Width,this.ClientRectangle.Height);*/
		}

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			_toolTipReset.SetToolTip(_bReset,"Reset all changes to the previous values");
			_toolTipApply.SetToolTip(_bApply,"Apply all changes");
			_toolTipOk.SetToolTip(_bOk,"Apply all changes et quit the application");

			_toolTipSimJobs.SetToolTip(_tbWorker,"This defines the number of threads ActiveQ will use to send queued emails");
			_toolTipIntervalMail.SetToolTip(_tbIntervalMail,"Interval expressed in second between each scan of mail directories");
			_toolTipIntervalTask.SetToolTip(_tbIntervalTask,"Interval expressed in second between each scan of xml files");
			_toolTipEventLog.SetToolTip(_tbMaxBytesEvent,"Maximum size specified in mega bytes");
			_toolTipErrorLog.SetToolTip(_tbMaxBytesError,"Maximum size specified in mega bytes");

			LoadConfigurationFile();
		}

		private void LoadConfigurationFile()
		{
			try
            {
                // Check if configuration exist, if not create the default
                if (!File.Exists(_configFile))
                    ActiveQLibrary.Global.CreateDefaultConfig(System.Environment.CurrentDirectory + @"\Config.xml", System.Environment.CurrentDirectory + @"\Pickup", false);


				TextReader reader = new StreamReader(_configFile);
				XmlSerializer serialize = new XmlSerializer(typeof(ActiveQLibrary.Serialization.ConfigGlobal.Config));
				ActiveQLibrary.Serialization.ConfigGlobal.Config config = (ActiveQLibrary.Serialization.ConfigGlobal.Config)serialize.Deserialize(reader);
				reader.Close();

				this._tbWorker.Text = config.Threads.ToString();
				this._tbActiveMailLicense.Text = config.ActiveMailLicense;
				this._tbIntervalMail.Text = config.Readers.MailPickUp.ToString();
				this._tbIntervalTask.Text = config.Readers.XmlPickup.ToString();
				if (config.DeleteMailWhenProcessed == true)
					this._cbDeleteWhenProcessed.Checked = true;
				else
					this._cbDeleteWhenProcessed.Checked = false;
				this._tbMaxBytesEvent.Text = config.LogFiles.MaxSizeEvent.ToString();
				this._tbMaxBytesError.Text = config.LogFiles.MaxSizeError.ToString();

				this._meMailDir.AddListItem(config.MailPickupDirectories);
				this._meXmlFile.AddListItem(config.XmlPickupFiles);

				foreach(SmtpServer smtpServer in config.SmtpServers)
				{
					if (smtpServer.Password.Trim() != "")
						smtpServer.Password = Encryption.Decrypt(smtpServer.Password.Trim());
				}

				this._meSmtpServer.AddListItem(config.SmtpServers);
			}

			catch
			{
				this.Hide();
				MessageBox.Show(string.Format("Unable to load '{0}'",_configFile),"Loading configuration file",MessageBoxButtons.OK,MessageBoxIcon.Error);
				Application.Exit();			
					
			}
		}

		private void _bReset_Click(object sender, System.EventArgs e)
		{
			this._cbDeleteWhenProcessed.Checked = false;
			this._tbWorker.Text = "10";
			this._tbIntervalMail.Text = "1";
			this._tbIntervalTask.Text = "1";
			this._tbMaxBytesEvent.Text = "0";
			this._tbMaxBytesError.Text = "0";

			this._meMailDir.ClearList();
			this._meSmtpServer.ClearList();
			this._meXmlFile.ClearList();

			LoadConfigurationFile();	
		}

		private void _bApply_Click(object sender, System.EventArgs e)
		{
			ApplyChanges();
		}

		private void ApplyChanges()
		{
			try
			{
				ActiveQLibrary.Serialization.ConfigGlobal.Config config = new ActiveQLibrary.Serialization.ConfigGlobal.Config();

				config.Threads = Int32.Parse(_tbWorker.Text);
				config.Readers.MailPickUp = Int32.Parse(_tbIntervalMail.Text);
				config.Readers.XmlPickup = Int32.Parse(_tbIntervalTask.Text);
				config.DeleteMailWhenProcessed = this._cbDeleteWhenProcessed.Checked;
				config.LogFiles.MaxSizeEvent = Int32.Parse(this._tbMaxBytesEvent.Text);
				config.LogFiles.MaxSizeError = Int32.Parse(this._tbMaxBytesError.Text);
				config.ActiveMailLicense = _tbActiveMailLicense.Text;

				foreach(string dir in _meMailDir.ListBoxItem.Items)
				{
					config.MailPickupDirectories.Add(dir);
				}

				foreach(ListViewItem lvITem in _meSmtpServer.ListViewItem.Items)
				{
					int index = _meSmtpServer.IndexElement(lvITem.Text,Int32.Parse(lvITem.SubItems[1].Text));
					if (index != -1)
					{
						if (((SmtpServer)_meSmtpServer.ListItemSmtp[index]).Username.Trim() != "" &&
							((SmtpServer)_meSmtpServer.ListItemSmtp[index]).Password.Trim() != "")
								config.SmtpServers.Add(new SmtpServer(((SmtpServer)_meSmtpServer.ListItemSmtp[index]).Host,
																((SmtpServer)_meSmtpServer.ListItemSmtp[index]).Port,
																((SmtpServer)_meSmtpServer.ListItemSmtp[index]).Username,
																Encryption.Encrypt(((SmtpServer)_meSmtpServer.ListItemSmtp[index]).Password)));
						else
							config.SmtpServers.Add(new SmtpServer(((SmtpServer)_meSmtpServer.ListItemSmtp[index]).Host,
								((SmtpServer)_meSmtpServer.ListItemSmtp[index]).Port,
								"",
								""));
					}

				}

				foreach(string xml in _meXmlFile.ListBoxItem.Items)
				{
					config.XmlPickupFiles.Add(xml);
				}

				XmlSerializer serialize = new XmlSerializer( typeof(ActiveQLibrary.Serialization.ConfigGlobal.Config));
				TextWriter writer = new StreamWriter(_configFile);
				serialize.Serialize( writer, config );
				writer.Close();

				try
				{
					ServiceController sc = new ServiceController("ActiveQ");
					sc.ExecuteCommand(230);
				}

				catch
				{
				}
			}

			catch(Exception ex)
			{
				MessageBox.Show(string.Format("Unable write data in '{0}'\n{1}\n{2}",_configFile,ex.Source,ex.Message),"Writing configuration file",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		private void _bOk_Click(object sender, System.EventArgs e)
		{
			ApplyChanges();
			Application.Exit();
		}

		private static Process RunningInstance()
		{
			Process current = Process.GetCurrentProcess();
			Process[] processes = Process.GetProcessesByName (current.ProcessName);

			//Loop through the running processes in with the same name
			foreach (Process process in processes)
			{
				//Ignore the current process
				if (process.Id != current.Id)
				{
					//Make sure that the process is running from the exe file.
					if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") ==
						current.MainModule.FileName)
					{
						//Return the other process instance.
						return process;
					}
				}
			}

			//No other instance was found, return null.
			return null;
		}
		
	}
}
