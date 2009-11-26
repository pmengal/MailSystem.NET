using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.ServiceProcess;

using ActiveQLibrary.CustomControl;

namespace ActiveQLibrary.Form
{
	public enum WM
	{
		WM_QUERYENDSESSION = 0x0011
	}	

	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : PageForm
	{
		private MenuItemVS _miDisplay;
		private MenuItemVS _miAbout;
		private MenuItemVS _miDisplayMainLog;
		private MenuItemVS _miDisplayOverview;
		private MenuItemVS _miDisplayStandardMail;
		private MenuItemVS _miDisplayScheduledMail;
		private MenuItemVS _miDisplayTask;
		private System.Windows.Forms.MainMenu _mainMenu;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			// button event handler
			base.PButtons.ButtonLog.Click += new System.EventHandler(ButtonLog_Click);
			base.PButtons.ButtonOverview.Click += new System.EventHandler(ButtonOverview_Click);
			base.PButtons.ButtonStandardMail.Click += new System.EventHandler(ButtonStandardMail_Click);
			base.PButtons.ButtonScheduledMail.Click += new System.EventHandler(ButtonScheduledMail_Click);
			base.PButtons.ButtonTask.Click += new System.EventHandler(ButtonTask_Click);
			base.PButtons.ButtonHelp.Click += new System.EventHandler(ButtonHelp_Click);
			base.PButtons.ButtonProgress.Click += new System.EventHandler(ButtonProgress_Click);

			ShowPage(TypePage.MainLog);
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
			this._mainMenu = new System.Windows.Forms.MainMenu();
			this._miDisplay = new ActiveQLibrary.CustomControl.MenuItemVS();
			this._miDisplayMainLog = new ActiveQLibrary.CustomControl.MenuItemVS();
			this._miDisplayOverview = new ActiveQLibrary.CustomControl.MenuItemVS();
			this._miDisplayStandardMail = new ActiveQLibrary.CustomControl.MenuItemVS();
			this._miDisplayScheduledMail = new ActiveQLibrary.CustomControl.MenuItemVS();
			this._miDisplayTask = new ActiveQLibrary.CustomControl.MenuItemVS();
			this._miAbout = new ActiveQLibrary.CustomControl.MenuItemVS();
			this.SuspendLayout();
			// 
			// _mainMenu
			// 
			this._mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this._miDisplay,
																					  this._miAbout});
			// 
			// _miDisplay
			// 
			this._miDisplay.ClickHandler = null;
			this._miDisplay.Icon = null;
			this._miDisplay.ImageIndex = -1;
			this._miDisplay.ImageList = null;
			this._miDisplay.Index = 0;
			this._miDisplay.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this._miDisplayMainLog,
																					   this._miDisplayOverview,
																					   this._miDisplayStandardMail,
																					   this._miDisplayScheduledMail,
																					   this._miDisplayTask});
			this._miDisplay.OwnerDraw = true;
			this._miDisplay.ShortcutText = "";
			this._miDisplay.Text = "&Display";
			this._miDisplay.TransparentColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(192)));
			// 
			// _miDisplayMainLog
			// 
			this._miDisplayMainLog.Checked = true;
			this._miDisplayMainLog.ClickHandler = null;
			this._miDisplayMainLog.Icon = null;
			this._miDisplayMainLog.ImageIndex = -1;
			this._miDisplayMainLog.ImageList = null;
			this._miDisplayMainLog.Index = 0;
			this._miDisplayMainLog.OwnerDraw = true;
			this._miDisplayMainLog.ShortcutText = "";
			this._miDisplayMainLog.Text = "&Main log";
			this._miDisplayMainLog.TransparentColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(192)));
			this._miDisplayMainLog.Click += new System.EventHandler(this._miDisplayMainLog_Click);
			// 
			// _miDisplayOverview
			// 
			this._miDisplayOverview.ClickHandler = null;
			this._miDisplayOverview.Icon = null;
			this._miDisplayOverview.ImageIndex = -1;
			this._miDisplayOverview.ImageList = null;
			this._miDisplayOverview.Index = 1;
			this._miDisplayOverview.OwnerDraw = true;
			this._miDisplayOverview.ShortcutText = "";
			this._miDisplayOverview.Text = "&Overview";
			this._miDisplayOverview.TransparentColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(192)));
			this._miDisplayOverview.Click += new System.EventHandler(this._miDisplayOverview_Click);
			// 
			// _miDisplayStandardMail
			// 
			this._miDisplayStandardMail.ClickHandler = null;
			this._miDisplayStandardMail.Icon = null;
			this._miDisplayStandardMail.ImageIndex = -1;
			this._miDisplayStandardMail.ImageList = null;
			this._miDisplayStandardMail.Index = 2;
			this._miDisplayStandardMail.OwnerDraw = true;
			this._miDisplayStandardMail.ShortcutText = "";
			this._miDisplayStandardMail.Text = "&Standard mail";
			this._miDisplayStandardMail.TransparentColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(192)));
			this._miDisplayStandardMail.Click += new System.EventHandler(this._miDisplayStandardMail_Click);
			// 
			// _miDisplayScheduledMail
			// 
			this._miDisplayScheduledMail.ClickHandler = null;
			this._miDisplayScheduledMail.Icon = null;
			this._miDisplayScheduledMail.ImageIndex = -1;
			this._miDisplayScheduledMail.ImageList = null;
			this._miDisplayScheduledMail.Index = 3;
			this._miDisplayScheduledMail.OwnerDraw = true;
			this._miDisplayScheduledMail.ShortcutText = "";
			this._miDisplayScheduledMail.Text = "S&cheduled mail";
			this._miDisplayScheduledMail.TransparentColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(192)));
			this._miDisplayScheduledMail.Click += new System.EventHandler(this._miDisplayScheduledMail_Click);
			// 
			// _miDisplayTask
			// 
			this._miDisplayTask.ClickHandler = null;
			this._miDisplayTask.Icon = null;
			this._miDisplayTask.ImageIndex = -1;
			this._miDisplayTask.ImageList = null;
			this._miDisplayTask.Index = 4;
			this._miDisplayTask.OwnerDraw = true;
			this._miDisplayTask.ShortcutText = "";
			this._miDisplayTask.Text = "&Task";
			this._miDisplayTask.TransparentColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(192)));
			this._miDisplayTask.Click += new System.EventHandler(this._miDisplayTask_Click);
			// 
			// _miAbout
			// 
			this._miAbout.ClickHandler = null;
			this._miAbout.Enabled = false;
			this._miAbout.Icon = null;
			this._miAbout.ImageIndex = -1;
			this._miAbout.ImageList = null;
			this._miAbout.Index = 1;
			this._miAbout.OwnerDraw = true;
			this._miAbout.ShortcutText = "";
			this._miAbout.Text = "&About";
			this._miAbout.TransparentColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(192)));
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(474, 429);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Menu = this._mainMenu;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ActiveQ";
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			if (Global.ActiveMailAsm == null)
			{
				base.PElemAll.DisableStdMail();
				base.PScheduledMail.Enabled = false;
				base.PStandardMail.Enabled = false;
				
				
			}

			Global.IsInitialized = true;
		}

		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.Hide();

			e.Cancel = true;
		}

		private void ButtonLog_Click(object sender, System.EventArgs e)
		{
			UnCheckAllItemDisplayMenu();
			this._miDisplayMainLog.Checked = true;
			this.ShowPage(TypePage.MainLog);
		}

		private void ButtonOverview_Click(object sender, System.EventArgs e)
		{
			UnCheckAllItemDisplayMenu();
			this._miDisplayOverview.Checked = true;
			this.ShowPage(TypePage.ElemAll);
		}

		private void ButtonStandardMail_Click(object sender, System.EventArgs e)
		{
			UnCheckAllItemDisplayMenu();
			this._miDisplayStandardMail.Checked = true;
			this.ShowPage(TypePage.DetailsStdMail);
		}

		private void ButtonScheduledMail_Click(object sender, System.EventArgs e)
		{
			UnCheckAllItemDisplayMenu();
			this._miDisplayScheduledMail.Checked = true;
			this.ShowPage(TypePage.DetailsSchMail);
		}

		private void ButtonTask_Click(object sender, System.EventArgs e)
		{
			UnCheckAllItemDisplayMenu();
			this._miDisplayTask.Checked = true;
			this.ShowPage(TypePage.DetailsSchTask);
		}

		private void ButtonHelp_Click(object sender, System.EventArgs e)
		{
			Process helpProcess = new Process();
			helpProcess.StartInfo.FileName = Global.GetImagePath(Assembly.GetExecutingAssembly().Location) + @"\Documentation.chm";
			helpProcess.Start();
		}

		private void ButtonProgress_Click(object sender, System.EventArgs e)
		{
			this.ShowPage(TypePage.Progress);
		}


		private void _miDisplayMainLog_Click(object sender, System.EventArgs e)
		{
			UnCheckAllItemDisplayMenu();
			this._miDisplayMainLog.Checked = true;
			this.ShowPage(TypePage.MainLog);
		}

		private void _miDisplayOverview_Click(object sender, System.EventArgs e)
		{
			UnCheckAllItemDisplayMenu();
			this._miDisplayOverview.Checked = true;
			this.ShowPage(TypePage.ElemAll);
		}

		private void _miDisplayStandardMail_Click(object sender, System.EventArgs e)
		{
			UnCheckAllItemDisplayMenu();
			this._miDisplayStandardMail.Checked = true;
			this.ShowPage(TypePage.DetailsStdMail);
		}

		private void _miDisplayScheduledMail_Click(object sender, System.EventArgs e)
		{
			UnCheckAllItemDisplayMenu();
			this._miDisplayScheduledMail.Checked = true;
			this.ShowPage(TypePage.DetailsSchMail);
		}

		private void _miDisplayTask_Click(object sender, System.EventArgs e)
		{
			UnCheckAllItemDisplayMenu();
			this._miDisplayTask.Checked = true;
			this.ShowPage(TypePage.DetailsSchTask);
		}

		private void UnCheckAllItemDisplayMenu()
		{
			this._miDisplayMainLog.Checked = false;
			this._miDisplayOverview.Checked = false;
			this._miDisplayStandardMail.Checked = false;
			this._miDisplayScheduledMail.Checked = false;
			this._miDisplayTask.Checked = false;
		}

		protected override  void WndProc(ref Message message)
		{
			if (message.Msg == (int)WM.WM_QUERYENDSESSION)
			{
				ServiceController sc = new ServiceController("ActiveQ");	
				sc.ExecuteCommand(221);
				
			}
			else
				base.WndProc(ref message);
			
		}
	}
}
