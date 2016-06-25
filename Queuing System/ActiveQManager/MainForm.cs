using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.ServiceProcess;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace ActiveQManager
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenu contextMenuNotify;
		private System.Windows.Forms.MenuItem contextMenuNotifyItemShow;
		private System.Windows.Forms.MenuItem contextMenuNotifyItemExit;
		private System.Windows.Forms.MenuItem contextMenuNotifyService;
		private System.Windows.Forms.MenuItem contextMenuNotifyServiceItemContinue;
		private System.Windows.Forms.MenuItem contextMenuNotifyServiceItemPause;
		private System.Windows.Forms.MenuItem contextMenuNotifyServiceItemStart;
		private System.Windows.Forms.MenuItem contextMenuNotifyServiceItemStop;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// The name of the service.
		/// </summary>
		private static string _serviceName = "ActiveQ";

		/// <summary>
		/// Manage the service
		/// </summary>
		private ServiceOperation _serviceOperation;

		/// <summary>
		/// Thread to check the state of the service.
		/// </summary>
		private Thread _threadCheckServiceState;
		private System.Windows.Forms.MenuItem menuItem1;
        private Button btnClose;
        private MenuStrip menuStripService;
        private ToolStripMenuItem serviceToolStripMenuItem;
        private ToolStripMenuItem continueToolStripMenuItem;
        private ToolStripMenuItem pauseToolStripMenuItem;
        private ToolStripMenuItem startToolStripMenuItem;
        private ToolStripMenuItem stopToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStrip toolStripService;
        private ToolStripButton btnContinue;
        private ToolStripButton btnPause;
        private ToolStripButton btnStart;
        private ToolStripButton btnStop;
        private ToolStripButton btnExit;
        private Label lblStatus;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem activityToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private MenuItem menuItemActivity;
        private MenuItem menuItem3;
        private ToolStripButton btnActivity;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem documentationToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripButton btnHelp;

		/// <summary>
		/// Indicates if the thread have to run or not.
		/// </summary>
		private bool _isRunning;
		
		public MainForm()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuNotify = new System.Windows.Forms.ContextMenu();
            this.contextMenuNotifyItemShow = new System.Windows.Forms.MenuItem();
            this.contextMenuNotifyService = new System.Windows.Forms.MenuItem();
            this.contextMenuNotifyServiceItemContinue = new System.Windows.Forms.MenuItem();
            this.contextMenuNotifyServiceItemPause = new System.Windows.Forms.MenuItem();
            this.contextMenuNotifyServiceItemStart = new System.Windows.Forms.MenuItem();
            this.contextMenuNotifyServiceItemStop = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.contextMenuNotifyItemExit = new System.Windows.Forms.MenuItem();
            this.btnClose = new System.Windows.Forms.Button();
            this.menuStripService = new System.Windows.Forms.MenuStrip();
            this.serviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.continueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripService = new System.Windows.Forms.ToolStrip();
            this.btnContinue = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPause = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnStart = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.lblStatus = new System.Windows.Forms.Label();
            this.menuItemActivity = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.btnActivity = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnHelp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStripService.SuspendLayout();
            this.toolStripService.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenu = this.contextMenuNotify;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "ActiveQ Manager";
            this.notifyIcon.Visible = true;
            // 
            // contextMenuNotify
            // 
            this.contextMenuNotify.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemActivity,
            this.menuItem3,
            this.contextMenuNotifyItemShow,
            this.contextMenuNotifyService,
            this.menuItem1,
            this.contextMenuNotifyItemExit});
            // 
            // contextMenuNotifyItemShow
            // 
            this.contextMenuNotifyItemShow.Index = 2;
            this.contextMenuNotifyItemShow.Text = "&Show";
            this.contextMenuNotifyItemShow.Click += new System.EventHandler(this.contextMenuNotifyItemShow_Click);
            // 
            // contextMenuNotifyService
            // 
            this.contextMenuNotifyService.Index = 3;
            this.contextMenuNotifyService.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.contextMenuNotifyServiceItemContinue,
            this.contextMenuNotifyServiceItemPause,
            this.contextMenuNotifyServiceItemStart,
            this.contextMenuNotifyServiceItemStop});
            this.contextMenuNotifyService.Text = "&Service";
            // 
            // contextMenuNotifyServiceItemContinue
            // 
            this.contextMenuNotifyServiceItemContinue.Index = 0;
            this.contextMenuNotifyServiceItemContinue.Text = "&Continue";
            this.contextMenuNotifyServiceItemContinue.Click += new System.EventHandler(this.contextMenuNotifyServiceItemContinue_Click);
            // 
            // contextMenuNotifyServiceItemPause
            // 
            this.contextMenuNotifyServiceItemPause.Index = 1;
            this.contextMenuNotifyServiceItemPause.Text = "&Pause";
            this.contextMenuNotifyServiceItemPause.Click += new System.EventHandler(this.contextMenuNotifyServiceItemPause_Click);
            // 
            // contextMenuNotifyServiceItemStart
            // 
            this.contextMenuNotifyServiceItemStart.Index = 2;
            this.contextMenuNotifyServiceItemStart.Text = "&Start";
            this.contextMenuNotifyServiceItemStart.Click += new System.EventHandler(this.contextMenuNotifyServiceItemStart_Click);
            // 
            // contextMenuNotifyServiceItemStop
            // 
            this.contextMenuNotifyServiceItemStop.Index = 3;
            this.contextMenuNotifyServiceItemStop.Text = "&Stop";
            this.contextMenuNotifyServiceItemStop.Click += new System.EventHandler(this.contextMenuNotifyServiceItemStop_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 4;
            this.menuItem1.Text = "-";
            // 
            // contextMenuNotifyItemExit
            // 
            this.contextMenuNotifyItemExit.Index = 5;
            this.contextMenuNotifyItemExit.Text = "&Exit";
            this.contextMenuNotifyItemExit.Click += new System.EventHandler(this.contextMenuNotifyItemExit_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(208, 111);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // menuStripService
            // 
            this.menuStripService.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serviceToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripService.Location = new System.Drawing.Point(0, 0);
            this.menuStripService.Name = "menuStripService";
            this.menuStripService.Size = new System.Drawing.Size(295, 24);
            this.menuStripService.TabIndex = 2;
            this.menuStripService.Text = "menuStrip1";
            // 
            // serviceToolStripMenuItem
            // 
            this.serviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activityToolStripMenuItem,
            this.toolStripSeparator6,
            this.continueToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.serviceToolStripMenuItem.Name = "serviceToolStripMenuItem";
            this.serviceToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.serviceToolStripMenuItem.Text = "Service";
            // 
            // activityToolStripMenuItem
            // 
            this.activityToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("activityToolStripMenuItem.Image")));
            this.activityToolStripMenuItem.Name = "activityToolStripMenuItem";
            this.activityToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.activityToolStripMenuItem.Text = "Activity";
            this.activityToolStripMenuItem.Click += new System.EventHandler(this.activityToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(149, 6);
            // 
            // continueToolStripMenuItem
            // 
            this.continueToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("continueToolStripMenuItem.Image")));
            this.continueToolStripMenuItem.Name = "continueToolStripMenuItem";
            this.continueToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.continueToolStripMenuItem.Text = "Continue";
            this.continueToolStripMenuItem.Click += new System.EventHandler(this.contextMenuNotifyServiceItemContinue_Click);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pauseToolStripMenuItem.Image")));
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pauseToolStripMenuItem.Text = "Pause";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.contextMenuNotifyServiceItemPause_Click);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("startToolStripMenuItem.Image")));
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.contextMenuNotifyServiceItemStart_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("stopToolStripMenuItem.Image")));
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.contextMenuNotifyServiceItemStop_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.contextMenuNotifyItemExit_Click);
            // 
            // toolStripService
            // 
            this.toolStripService.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnActivity,
            this.toolStripSeparator7,
            this.btnContinue,
            this.toolStripSeparator5,
            this.btnPause,
            this.toolStripSeparator4,
            this.btnStart,
            this.toolStripSeparator3,
            this.btnStop,
            this.toolStripSeparator2,
            this.btnExit,
            this.toolStripSeparator8,
            this.btnHelp});
            this.toolStripService.Location = new System.Drawing.Point(0, 24);
            this.toolStripService.Name = "toolStripService";
            this.toolStripService.Size = new System.Drawing.Size(295, 25);
            this.toolStripService.TabIndex = 3;
            // 
            // btnContinue
            // 
            this.btnContinue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnContinue.Image = ((System.Drawing.Image)(resources.GetObject("btnContinue.Image")));
            this.btnContinue.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(23, 22);
            this.btnContinue.Text = "Continue";
            this.btnContinue.Click += new System.EventHandler(this.contextMenuNotifyServiceItemContinue_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnPause
            // 
            this.btnPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
            this.btnPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(23, 22);
            this.btnPause.Text = "Pause";
            this.btnPause.Click += new System.EventHandler(this.contextMenuNotifyServiceItemPause_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnStart
            // 
            this.btnStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStart.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Image")));
            this.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(23, 22);
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.contextMenuNotifyServiceItemStart_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnStop
            // 
            this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(23, 22);
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.contextMenuNotifyServiceItemStop_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExit
            // 
            this.btnExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(23, 22);
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.contextMenuNotifyItemExit_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(12, 67);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(199, 18);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "The service is running";
            // 
            // menuItemActivity
            // 
            this.menuItemActivity.Index = 0;
            this.menuItemActivity.Text = "&Activity";
            this.menuItemActivity.Click += new System.EventHandler(this.activityToolStripMenuItem_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "-";
            // 
            // btnActivity
            // 
            this.btnActivity.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnActivity.Image = ((System.Drawing.Image)(resources.GetObject("btnActivity.Image")));
            this.btnActivity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActivity.Name = "btnActivity";
            this.btnActivity.Size = new System.Drawing.Size(23, 22);
            this.btnActivity.Text = "Activity";
            this.btnActivity.Click += new System.EventHandler(this.activityToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.documentationToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // documentationToolStripMenuItem
            // 
            this.documentationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("documentationToolStripMenuItem.Image")));
            this.documentationToolStripMenuItem.Name = "documentationToolStripMenuItem";
            this.documentationToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.documentationToolStripMenuItem.Text = "Documentation";
            this.documentationToolStripMenuItem.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(23, 22);
            this.btnHelp.Text = "Help";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(295, 146);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.toolStripService);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.menuStripService);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ActiveQ Manager";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStripService.ResumeLayout(false);
            this.menuStripService.PerformLayout();
            this.toolStripService.ResumeLayout(false);
            this.toolStripService.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Process instance = RunningInstance();
			if (instance == null)
			{
				//There isn't another instance, show our form.
				ServiceController[] sc = ServiceController.GetServices();
				bool flagIsFound = false;

				for (int i = 0 ; i < sc.Length ; i++)
				{
					if (sc[i].ServiceName == _serviceName)
					{
						flagIsFound = true;
						break;
					}
				}

                if (flagIsFound == true)
                {
                    MainForm mainForm = new MainForm();
                    Application.Run(mainForm);
                    mainForm.Visible = false;
                }
                else
                {
                    MessageBox.Show(string.Format("{0} is not installed. Please install the service before trying to access the manager", _serviceName), "ActiveQManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
			}
			else
			{
				//There is another instance of this process.
				MessageBox.Show("ActiveQ Manager is already started.","ActiveQManager",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}

		}

        /// <summary>
        /// Show ActiveQ Manager event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
		private void contextMenuNotifyItemShow_Click(object sender, System.EventArgs e)
		{
			_serviceOperation.Refresh();			

			switch(_serviceOperation.ServiceController.Status)
			{
                // stop pending.
				case ServiceControllerStatus.StopPending:
				{
					MessageBox.Show(string.Format("{0} is currently pending stop. Please wait the service is stopped before trying to show the manager.",_serviceName),"ActiveQManager",MessageBoxButtons.OK,MessageBoxIcon.Information);
				} break;

                // start pending.
				case ServiceControllerStatus.StartPending:
				{
					MessageBox.Show(string.Format("{0} is currently pending start. Please wait the service is started before trying to show the manager.",_serviceName),"ActiveQManager",MessageBoxButtons.OK,MessageBoxIcon.Information);
				} break;

                // default.
				default:
				{
					_serviceOperation.ServiceController.ExecuteCommand(200);
				} break;

			}

            this.ShowInTaskbar = true;
            this.Visible = true;
            this.Activate();
		}

        /// <summary>
        /// Menu item hide event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void contextMenuNotifyItemHide_Click(object sender, System.EventArgs e)
		{
			_serviceOperation.ServiceController.ExecuteCommand(220);
		}

        /// <summary>
        /// Menu item exit event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void contextMenuNotifyItemExit_Click(object sender, System.EventArgs e)
		{
            this._isRunning = false;
			this.Close();
            this.Exit();
		}

        /// <summary>
        /// Menu item continue event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void contextMenuNotifyServiceItemContinue_Click(object sender, System.EventArgs e)
		{
			DisableMenuService();
			_serviceOperation.Continue();
		}

        /// <summary>
        /// Menu item pause event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void contextMenuNotifyServiceItemPause_Click(object sender, System.EventArgs e)
		{
			DisableMenuService();
			_serviceOperation.Pause();
		}

        /// <summary>
        /// Menu item start event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void contextMenuNotifyServiceItemStart_Click(object sender, System.EventArgs e)
		{
			DisableMenuService();
			_serviceOperation.Start();
			
		}

        /// <summary>
        /// Menu item stop event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void contextMenuNotifyServiceItemStop_Click(object sender, System.EventArgs e)
		{
			DisableMenuService();
			_serviceOperation.Stop();
		}

        /// <summary>
        /// Method for disable the menu services items.
        /// </summary>
		public void DisableMenuService()
		{
            this.SetMenuItemsEnabled(false, false, false, false);
		}

        /// <summary>
        /// Method for Enable or Disable Menu Service items based on service controller.
        /// </summary>
        /// <param name="sc">The ServiceController.</param>
		public void EnableDisableMenuService(ServiceController sc)
		{
			sc =  new ServiceController(_serviceName);

            // stoped.
			if (sc.Status == ServiceControllerStatus.Stopped)
			{
                this.SetMenuItemsEnabled(false, false, true, false);
                this.lblStatus.Text = "The service is stopped.";
                Icon icon = new Icon(GetType(), "Stopped.ico");
                this.notifyIcon.Icon = icon;
                this.Icon = icon;
            }

            // paused.
			else if (sc.Status == ServiceControllerStatus.Paused)
			{
                this.SetMenuItemsEnabled(true, false, false, true);
                this.lblStatus.Text = "The service is paused.";
                Icon icon = new Icon(GetType(), "Paused.ico");
                this.notifyIcon.Icon = icon;
                this.Icon = icon;
            }

            // running.
			else if (sc.Status == ServiceControllerStatus.Running)
			{
				if (sc.CanPauseAndContinue)
				{
                    this.SetMenuItemsEnabled(false, true, false, true);
				}
				else
				{
                    this.SetMenuItemsEnabled(false, false, false, true);
				}

                this.lblStatus.Text = "The service is running.";
                Icon icon = new Icon(GetType(), "Started.ico");
                this.notifyIcon.Icon = icon;
                this.Icon = icon;
			}
        }

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			try
			{
				_serviceOperation = new ServiceOperation(new ServiceController(_serviceName));
				_serviceOperation.FinishedOperation += new FinishedServiceOperationEventHandler(OnFinishedOperation);
				_serviceOperation.ErrorOperation += new ErrorServiceOperationEventHandler(OnErrorOperation);

				_threadCheckServiceState = new Thread(new ThreadStart(CheckServiceState));
				_threadCheckServiceState.Start();
								
			}

			catch (Exception ex) 
			{
				MessageBox.Show(string.Format("Error when loading form : ",ex.Message),"ActiveQManager",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}

			EnableDisableMenuService(_serviceOperation.ServiceController);
		}

		private void OnFinishedOperation(object sender,FinishedServiceOperationEventArgs e)
		{
			EnableDisableMenuService(_serviceOperation.ServiceController);
		}

		private void OnErrorOperation(object sender, ErrorServiceOperationEventArgs e)
		{
			MessageBox.Show(string.Format("Error when executing operation {0} : {1}",e.Operation.ToString(),e.Exception.Message),"ActiveQManager",MessageBoxButtons.OK,MessageBoxIcon.Error);
			
			EnableDisableMenuService(_serviceOperation.ServiceController);
			
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

        /// <summary>
        /// Check service state thread.
        /// </summary>
		private void CheckServiceState()
		{
			this._isRunning = true;

            while (this._isRunning)
			{
                EnableDisableMenuService(_serviceOperation.ServiceController);
                Thread.Sleep(500);
			}
		}

        /// <summary>
        /// Event for main form closing.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
            this._isRunning = false;
            this.Visible = false;
        }

        /// <summary>
        /// Method for set the menu items for Toolbar and Menu enabled or not.
        /// </summary>
        /// <param name="continueEnabled">Enable or disable the continue item.</param>
        /// <param name="pauseEnabled">Enable or disable the pause item.</param>
        /// <param name="startEnabled">Enable or disable the start item.</param>
        /// <param name="stopEnabled">Enable or disable the stop item.</param>
        private void SetMenuItemsEnabled(bool continueEnabled, bool pauseEnabled,
            bool startEnabled, bool stopEnabled)
        {
            // continue.
            this.contextMenuNotifyServiceItemContinue.Enabled = continueEnabled;
            this.continueToolStripMenuItem.Enabled = continueEnabled;
            this.btnContinue.Enabled = continueEnabled;

            // pause.
            this.contextMenuNotifyServiceItemPause.Enabled = pauseEnabled;
            this.pauseToolStripMenuItem.Enabled = pauseEnabled;
            this.btnPause.Enabled = pauseEnabled;

            // start.
            this.contextMenuNotifyServiceItemStart.Enabled = startEnabled;
            this.startToolStripMenuItem.Enabled = startEnabled;
            this.btnStart.Enabled = startEnabled;

            // stop.
            this.contextMenuNotifyServiceItemStop.Enabled = stopEnabled;
            this.stopToolStripMenuItem.Enabled = stopEnabled;
            this.btnStop.Enabled = stopEnabled;
        }

        /// <summary>
        /// Button close event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        /// <summary>
        /// Menu item activity click event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void activityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveQLibrary.Form.MainForm main = ActiveQLibrary.Form.ManageForm.Form;
            
            if (main != null)
            {
                main.Show();
            }
            else
            {
                ActiveQLibrary.Form.ManageForm.Initialize();
            }
        }

        /// <summary>
        /// Event for main form closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Exit();
        }

        /// <summary>
        /// Method for exit the current app.
        /// </summary>
        private void Exit()
        {
            if (!this._isRunning)
            {
                Process process = MainForm.RunningInstance();
                if (process != null)
                {
                    process.Close();
                }
            }
        }

        /// <summary>
        /// Event handler for help item click.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void btnHelp_Click(object sender, EventArgs e)
        {
            Process helpProcess = new Process();
            helpProcess.StartInfo.FileName = "Documentation.chm";
            helpProcess.Start();
        }

	}
}
