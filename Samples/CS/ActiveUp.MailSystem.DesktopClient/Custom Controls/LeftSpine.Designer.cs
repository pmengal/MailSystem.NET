namespace ActiveUp.MailSystem.DesktopClient
{
	partial class LeftSpine
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LeftSpine));
            this.stackStripSplitter = new System.Windows.Forms.SplitContainer();
            this.panelComponents = new System.Windows.Forms.Panel();
            this.headerStrip1 = new ActiveUp.MailSystem.DesktopClient.HeaderStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.headerStrip2 = new ActiveUp.MailSystem.DesktopClient.HeaderStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.stackStrip = new ActiveUp.MailSystem.DesktopClient.StackStrip();
            this.toolStripButtonMail = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCalendar = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonContacts = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonTasks = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNotes = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFolders = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonShotcuts = new System.Windows.Forms.ToolStripButton();
            this.overflowStrip = new ActiveUp.MailSystem.DesktopClient.BaseStackStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.addorRemoveButtonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stackStripSplitter.Panel1.SuspendLayout();
            this.stackStripSplitter.Panel2.SuspendLayout();
            this.stackStripSplitter.SuspendLayout();
            this.headerStrip1.SuspendLayout();
            this.headerStrip2.SuspendLayout();
            this.stackStrip.SuspendLayout();
            this.overflowStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // stackStripSplitter
            // 
            this.stackStripSplitter.BackColor = System.Drawing.Color.Transparent;
            this.stackStripSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stackStripSplitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.stackStripSplitter.Location = new System.Drawing.Point(0, 0);
            this.stackStripSplitter.Name = "stackStripSplitter";
            this.stackStripSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // stackStripSplitter.Panel1
            // 
            this.stackStripSplitter.Panel1.Controls.Add(this.panelComponents);
            this.stackStripSplitter.Panel1.Controls.Add(this.headerStrip1);
            this.stackStripSplitter.Panel1.Controls.Add(this.headerStrip2);
            // 
            // stackStripSplitter.Panel2
            // 
            this.stackStripSplitter.Panel2.Controls.Add(this.stackStrip);
            this.stackStripSplitter.Panel2.Controls.Add(this.overflowStrip);
            this.stackStripSplitter.Size = new System.Drawing.Size(269, 629);
            this.stackStripSplitter.SplitterDistance = 347;
            this.stackStripSplitter.SplitterWidth = 7;
            this.stackStripSplitter.TabIndex = 0;
            this.stackStripSplitter.TabStop = false;
            this.stackStripSplitter.Text = "splitContainer1";
            this.stackStripSplitter.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.stackStripSplitter_SplitterMoved);
            this.stackStripSplitter.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Paint);
            // 
            // panelComponents
            // 
            this.panelComponents.BackColor = System.Drawing.SystemColors.Window;
            this.panelComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComponents.Location = new System.Drawing.Point(0, 44);
            this.panelComponents.Name = "panelComponents";
            this.panelComponents.Size = new System.Drawing.Size(269, 303);
            this.panelComponents.TabIndex = 3;
            // 
            // headerStrip1
            // 
            this.headerStrip1.AutoSize = false;
            this.headerStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.headerStrip1.ForeColor = System.Drawing.Color.Black;
            this.headerStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.headerStrip1.HeaderStyle = ActiveUp.MailSystem.DesktopClient.AreaHeaderStyle.Small;
            this.headerStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2});
            this.headerStrip1.Location = new System.Drawing.Point(0, 25);
            this.headerStrip1.Name = "headerStrip1";
            this.headerStrip1.Size = new System.Drawing.Size(269, 19);
            this.headerStrip1.TabIndex = 0;
            this.headerStrip1.Text = "headerStrip1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(80, 16);
            this.toolStripLabel2.Text = " All Mail Folders";
            // 
            // headerStrip2
            // 
            this.headerStrip2.AutoSize = false;
            this.headerStrip2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.headerStrip2.ForeColor = System.Drawing.Color.White;
            this.headerStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.headerStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.headerStrip2.Location = new System.Drawing.Point(0, 0);
            this.headerStrip2.Name = "headerStrip2";
            this.headerStrip2.Size = new System.Drawing.Size(269, 25);
            this.headerStrip2.TabIndex = 1;
            this.headerStrip2.Text = "headerStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(43, 22);
            this.toolStripLabel1.Text = " Mail";
            // 
            // stackStrip
            // 
            this.stackStrip.AutoSize = false;
            this.stackStrip.CanOverflow = false;
            this.stackStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stackStrip.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
            this.stackStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.stackStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.stackStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonMail,
            this.toolStripButtonCalendar,
            this.toolStripButtonContacts,
            this.toolStripButtonTasks,
            this.toolStripButtonNotes,
            this.toolStripButtonFolders,
            this.toolStripButtonShotcuts});
            this.stackStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.stackStrip.Location = new System.Drawing.Point(0, 0);
            this.stackStrip.Name = "stackStrip";
            this.stackStrip.Padding = new System.Windows.Forms.Padding(0);
            this.stackStrip.Size = new System.Drawing.Size(269, 243);
            this.stackStrip.TabIndex = 0;
            this.stackStrip.Tag = "Read";
            this.stackStrip.Text = "stackStrip1";
            // 
            // toolStripButtonMail
            // 
            this.toolStripButtonMail.Checked = true;
            this.toolStripButtonMail.CheckOnClick = true;
            this.toolStripButtonMail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonMail.Image = global::ActiveUp.MailSystem.DesktopClient.Properties.Resources.Mail24;
            this.toolStripButtonMail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButtonMail.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.toolStripButtonMail.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButtonMail.Name = "toolStripButtonMail";
            this.toolStripButtonMail.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripButtonMail.Size = new System.Drawing.Size(268, 32);
            this.toolStripButtonMail.Tag = "Read";
            this.toolStripButtonMail.Text = "Mail";
            this.toolStripButtonMail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButtonMail.Click += new System.EventHandler(this.toolStripButtonMail_Click);
            // 
            // toolStripButtonCalendar
            // 
            this.toolStripButtonCalendar.CheckOnClick = true;
            this.toolStripButtonCalendar.Image = global::ActiveUp.MailSystem.DesktopClient.Properties.Resources.Calendar24;
            this.toolStripButtonCalendar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButtonCalendar.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.toolStripButtonCalendar.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButtonCalendar.Name = "toolStripButtonCalendar";
            this.toolStripButtonCalendar.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripButtonCalendar.Size = new System.Drawing.Size(268, 32);
            this.toolStripButtonCalendar.Tag = "NewAppointment";
            this.toolStripButtonCalendar.Text = "Calendar";
            this.toolStripButtonCalendar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButtonCalendar.Click += new System.EventHandler(this.toolStripButtonCalendar_Click);
            // 
            // toolStripButtonContacts
            // 
            this.toolStripButtonContacts.CheckOnClick = true;
            this.toolStripButtonContacts.Image = global::ActiveUp.MailSystem.DesktopClient.Properties.Resources.Contacts24;
            this.toolStripButtonContacts.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButtonContacts.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.toolStripButtonContacts.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButtonContacts.Name = "toolStripButtonContacts";
            this.toolStripButtonContacts.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripButtonContacts.Size = new System.Drawing.Size(268, 32);
            this.toolStripButtonContacts.Tag = "NewContact";
            this.toolStripButtonContacts.Text = "Contacts";
            this.toolStripButtonContacts.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButtonContacts.Click += new System.EventHandler(this.toolStripButtonContacts_Click);
            // 
            // toolStripButtonTasks
            // 
            this.toolStripButtonTasks.CheckOnClick = true;
            this.toolStripButtonTasks.Image = global::ActiveUp.MailSystem.DesktopClient.Properties.Resources.Tasks24;
            this.toolStripButtonTasks.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButtonTasks.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.toolStripButtonTasks.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButtonTasks.Name = "toolStripButtonTasks";
            this.toolStripButtonTasks.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripButtonTasks.Size = new System.Drawing.Size(268, 32);
            this.toolStripButtonTasks.Tag = "NewTask";
            this.toolStripButtonTasks.Text = "Tasks";
            this.toolStripButtonTasks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButtonTasks.Click += new System.EventHandler(this.toolStripButtonTasks_Click);
            // 
            // toolStripButtonNotes
            // 
            this.toolStripButtonNotes.CheckOnClick = true;
            this.toolStripButtonNotes.Image = global::ActiveUp.MailSystem.DesktopClient.Properties.Resources.Notes32;
            this.toolStripButtonNotes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButtonNotes.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.toolStripButtonNotes.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButtonNotes.Name = "toolStripButtonNotes";
            this.toolStripButtonNotes.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripButtonNotes.Size = new System.Drawing.Size(268, 32);
            this.toolStripButtonNotes.Tag = "Notes";
            this.toolStripButtonNotes.Text = "Notes";
            this.toolStripButtonNotes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButtonNotes.Click += new System.EventHandler(this.toolStripButtonNotes_Click);
            // 
            // toolStripButtonFolders
            // 
            this.toolStripButtonFolders.CheckOnClick = true;
            this.toolStripButtonFolders.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonFolders.Image")));
            this.toolStripButtonFolders.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButtonFolders.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.toolStripButtonFolders.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButtonFolders.Name = "toolStripButtonFolders";
            this.toolStripButtonFolders.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripButtonFolders.Size = new System.Drawing.Size(268, 32);
            this.toolStripButtonFolders.Tag = "Folder";
            this.toolStripButtonFolders.Text = "Folders";
            this.toolStripButtonFolders.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButtonFolders.Click += new System.EventHandler(this.toolStripButtonFolders_Click);
            // 
            // toolStripButtonShotcuts
            // 
            this.toolStripButtonShotcuts.CheckOnClick = true;
            this.toolStripButtonShotcuts.Image = global::ActiveUp.MailSystem.DesktopClient.Properties.Resources.Shortcut24;
            this.toolStripButtonShotcuts.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButtonShotcuts.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.toolStripButtonShotcuts.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripButtonShotcuts.Name = "toolStripButtonShotcuts";
            this.toolStripButtonShotcuts.Padding = new System.Windows.Forms.Padding(2);
            this.toolStripButtonShotcuts.Size = new System.Drawing.Size(268, 32);
            this.toolStripButtonShotcuts.Tag = "Shortcut";
            this.toolStripButtonShotcuts.Text = "Shortcuts";
            this.toolStripButtonShotcuts.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButtonShotcuts.Click += new System.EventHandler(this.toolStripButtonShotcuts_Click);
            // 
            // overflowStrip
            // 
            this.overflowStrip.AutoSize = false;
            this.overflowStrip.CanOverflow = false;
            this.overflowStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.overflowStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.overflowStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.overflowStrip.Location = new System.Drawing.Point(0, 243);
            this.overflowStrip.Name = "overflowStrip";
            this.overflowStrip.Size = new System.Drawing.Size(269, 32);
            this.overflowStrip.TabIndex = 1;
            this.overflowStrip.Text = "overflowStrip";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addorRemoveButtonsToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Padding = new System.Windows.Forms.Padding(3);
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(19, 32);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // addorRemoveButtonsToolStripMenuItem
            // 
            this.addorRemoveButtonsToolStripMenuItem.Name = "addorRemoveButtonsToolStripMenuItem";
            this.addorRemoveButtonsToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.addorRemoveButtonsToolStripMenuItem.Text = "&Add or Remove Buttons";
            // 
            // LeftSpine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.stackStripSplitter);
            this.Name = "LeftSpine";
            this.Size = new System.Drawing.Size(269, 629);
            this.Load += new System.EventHandler(this.StackBar_Load);
            this.stackStripSplitter.Panel1.ResumeLayout(false);
            this.stackStripSplitter.Panel2.ResumeLayout(false);
            this.stackStripSplitter.ResumeLayout(false);
            this.headerStrip1.ResumeLayout(false);
            this.headerStrip1.PerformLayout();
            this.headerStrip2.ResumeLayout(false);
            this.headerStrip2.PerformLayout();
            this.stackStrip.ResumeLayout(false);
            this.stackStrip.PerformLayout();
            this.overflowStrip.ResumeLayout(false);
            this.overflowStrip.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.SplitContainer stackStripSplitter;
		private HeaderStrip headerStrip1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private HeaderStrip headerStrip2;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private StackStrip stackStrip;
		private System.Windows.Forms.ToolStripButton toolStripButtonMail;
		private System.Windows.Forms.ToolStripButton toolStripButtonCalendar;
		private System.Windows.Forms.ToolStripButton toolStripButtonContacts;
		private System.Windows.Forms.ToolStripButton toolStripButtonTasks;
		private System.Windows.Forms.ToolStripButton toolStripButtonNotes;
		private System.Windows.Forms.ToolStripButton toolStripButtonFolders;
		private System.Windows.Forms.ToolStripButton toolStripButtonShotcuts;
		private BaseStackStrip overflowStrip;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
		private System.Windows.Forms.ToolStripMenuItem addorRemoveButtonsToolStripMenuItem;
        private System.Windows.Forms.Panel panelComponents;
	}
}
