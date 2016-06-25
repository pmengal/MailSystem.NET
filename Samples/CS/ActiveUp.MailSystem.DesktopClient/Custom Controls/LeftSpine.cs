using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Windows.Forms.Design;

namespace ActiveUp.MailSystem.DesktopClient
{
	[Designer(typeof(ParentControlDesigner))]
	public partial class LeftSpine : UserControl
	{
		private int _visibleCount = -1;
		private int _lastCount = -1;
		private int	_maxHeight;
        FolderView folderView;

		#region Setup
		public LeftSpine()
		{
			// Set Defaults
			this.Dock = DockStyle.Fill;
			this.TabStop = false;

			// Initialize
			InitializeComponent();

            // load the default pane.
            folderView = new ActiveUp.MailSystem.DesktopClient.FolderView();
        }

		private void StackBar_Load(object sender, EventArgs e)
		{
			// Setup
			this.stackStrip.ItemHeightChanged += new EventHandler(stackStrip1_ItemHeightChanged);

			// Add Overflow items
			AddOverflowItems();

			// Set Height
			InitializeSplitter();

			// Set parent padding
			this.Parent.Padding = new Padding(3, 3, 0, 3);
		}

		private void AddOverflowItems()
		{
			ToolStripButton item;
			Color			trans = Color.FromArgb(238,238,238);
			string			name;

			// Add items to overflow
			for (int idx = this.stackStrip.ItemCount - 1; idx >= 0; idx--)
			{
				name = this.stackStrip.Items[idx].Tag as string;

				if (null != name)
				{
					item = new ToolStripButton(GetBitmapResource(name));
					item.ImageTransparentColor = trans;
					item.Alignment = ToolStripItemAlignment.Right;
					item.Name = name;

					this.overflowStrip.Items.Add(item);
				}
			}
		}

		private Bitmap GetBitmapResource(string name)
		{
			return Properties.Resources.ResourceManager.GetObject(name) as Bitmap;
		}
		#endregion

		#region Event Handlers
		private void splitContainer1_Paint(object sender, PaintEventArgs e)
		{
			ProfessionalColorTable	pct = new ProfessionalColorTable();
			Rectangle				bounds = (sender as SplitContainer).SplitterRectangle;

			int			squares;
			int			maxSquares = 9;
			int			squareSize = 4;
			int			boxSize = 2;

			// Make sure we need to do work
			if ((bounds.Width > 0) && (bounds.Height > 0))
			{
				Graphics	g = e.Graphics;

				// Setup colors from the provided renderer
				Color		begin = pct.OverflowButtonGradientMiddle;
				Color		end = pct.OverflowButtonGradientEnd;

				// Make sure we need to do work
				using (Brush b = new LinearGradientBrush(bounds, begin, end, LinearGradientMode.Vertical))
				{
					g.FillRectangle(b, bounds);
				}

				// Calculate squares
				if ((bounds.Width > squareSize) && (bounds.Height > squareSize))
				{
					squares = Math.Min((bounds.Width / squareSize), maxSquares);

					// Calculate start
					int		start = (bounds.Width - (squares * squareSize)) / 2;
					int		Y = bounds.Y  + 1;

					// Get brushes
					Brush dark = new SolidBrush(pct.GripDark);
					Brush middle = new SolidBrush(pct.ToolStripBorder);
					Brush light = new SolidBrush(pct.ToolStripDropDownBackground);

					// Draw
					for (int idx = 0; idx < squares; idx++)
					{
						// Draw grips
						g.FillRectangle(dark, start, Y, boxSize, boxSize);
						g.FillRectangle(light, start + 1, Y+1, boxSize, boxSize);
						g.FillRectangle(middle, start + 1, Y+1, 1, 1);
						start += squareSize;
					}

					dark.Dispose();
					middle.Dispose();
					light.Dispose();
				}
			}
		}

		void stackStrip1_ItemHeightChanged(object sender, EventArgs e)
		{
			InitializeSplitter();
		}

		private void stackStripSplitter_SplitterMoved(object sender, SplitterEventArgs e)
		{
			if ((_maxHeight > 0) && ((this.stackStripSplitter.Height - e.SplitY) > _maxHeight))
			{
				// Limit to max height
				this.stackStripSplitter.SplitterDistance = this.stackStripSplitter.Height - _maxHeight;

				// Set to max
				_visibleCount = this.stackStrip.ItemCount;
			}
			else if ((_visibleCount > 0) && (this.stackStrip.ItemCount > 0))
			{
				// Make sure overflow is correct
				_visibleCount = (this.stackStrip.Height / this.stackStrip.ItemHeight);

				// See if this changed
				if (_lastCount != _visibleCount)
				{
					int		count = this.overflowStrip.Items.Count;

					// Update overflow items
					for (int idx = 1; idx < count; idx++)
					{
						this.overflowStrip.Items[idx].Visible = (idx < (count - _visibleCount));
					}

					// Update last
					_lastCount = _visibleCount;
				}
			}
		}
		#endregion

		#region Private API
		private void InitializeSplitter()
		{
			// Set slider increment
			if (this.stackStrip.ItemHeight > 0)
			{
				this.stackStripSplitter.SplitterIncrement = this.stackStrip.ItemHeight;

				// Find visible count
				if (_visibleCount < 0)
				{
					_visibleCount = this.stackStrip.InitialDisplayCount;
				}

				// Setup BaseStackStrip
				this.overflowStrip.Height = this.stackStrip.ItemHeight;

				// Set splitter distance
				int min = this.stackStrip.ItemHeight + this.overflowStrip.Height;
				int distance = this.stackStripSplitter.Height - this.stackStripSplitter.SplitterWidth - ((_visibleCount * this.stackStrip.ItemHeight) + this.overflowStrip.Height);

				// Set Max
				_maxHeight = (this.stackStrip.ItemHeight * this.stackStrip.ItemCount) + this.overflowStrip.Height + this.stackStripSplitter.SplitterWidth;

				// In case it's sized too small on startup
				if (distance < 0)
				{
					distance = min;
				}

				// Set Min/Max
				this.stackStripSplitter.SplitterDistance = distance;
				this.stackStripSplitter.Panel1MinSize = min;
			}
		}
		#endregion

        #region Properties

        public FolderView FolderView
        {
            get { return this.folderView; }
        }
	

        #endregion

        #region Events
        
        /// <summary>
        /// Mail toolstrip menu item click event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        public void toolStripButtonMail_Click(object sender, EventArgs e)
        {
            this.CheckAllFalse();
            this.toolStripButtonMail.Checked = true;
            this.toolStripLabel1.Text = this.toolStripButtonMail.Text;
            this.toolStripLabel2.Text = "All Mail Folders";
            this.panelComponents.Controls.Clear();
            
            folderView.BackColor = System.Drawing.SystemColors.Window;
            folderView.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            folderView.Dock = System.Windows.Forms.DockStyle.Fill;
            folderView.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            panelComponents.Controls.Add(folderView);
        }

        /// <summary>
        /// Calendar toolstrip menu item click event handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        public void toolStripButtonCalendar_Click(object sender, EventArgs e)
        {
            this.CheckAllFalse();
            this.toolStripButtonCalendar.Checked = true;
            this.toolStripLabel1.Text = this.toolStripButtonCalendar.Text;
            this.toolStripLabel2.Text = "All Calendar Items";
            this.panelComponents.Controls.Clear();
            
            MonthCalendar calendar = new MonthCalendar();
            calendar.Dock = System.Windows.Forms.DockStyle.Fill;
            calendar.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            panelComponents.Controls.Add(calendar);
        }

        public void toolStripButtonContacts_Click(object sender, EventArgs e)
        {
            this.CheckAllFalse();
            this.toolStripButtonContacts.Checked = true;
            this.toolStripLabel1.Text = this.toolStripButtonContacts.Text;
            this.toolStripLabel2.Text = "All Contact Items";
            this.panelComponents.Controls.Clear();

            Label label = new Label();
            label.Text = "\r\nThis is a sample application. This function is not implemented yet.";
            label.Dock = DockStyle.Fill;
            this.panelComponents.Controls.Add(label);
        }

        public void toolStripButtonTasks_Click(object sender, EventArgs e)
        {
            this.CheckAllFalse();
            this.toolStripButtonTasks.Checked = true;
            this.toolStripLabel1.Text = this.toolStripButtonTasks.Text;
            this.toolStripLabel2.Text = "All Task Items";
            this.panelComponents.Controls.Clear();
            
            Label label = new Label();
            label.Text = "\r\nThis is a sample application. This function is not implemented yet.";
            label.Dock = DockStyle.Fill;
            this.panelComponents.Controls.Add(label);
        }

        public void toolStripButtonNotes_Click(object sender, EventArgs e)
        {
            this.CheckAllFalse();
            this.toolStripButtonNotes.Checked = true;
            this.toolStripLabel1.Text = this.toolStripButtonNotes.Text;
            this.toolStripLabel2.Text = "All Note Items";
            this.panelComponents.Controls.Clear();
            
            Label label = new Label();
            label.Text = "\r\nThis is a sample application. This function is not implemented yet.";
            label.Dock = DockStyle.Fill;
            this.panelComponents.Controls.Add(label);
        }

        public void toolStripButtonFolders_Click(object sender, EventArgs e)
        {
            this.CheckAllFalse();
            this.toolStripButtonFolders.Checked = true;
            this.toolStripLabel1.Text = this.toolStripButtonFolders.Text;
            this.toolStripLabel2.Text = "All Folder Items";
            this.panelComponents.Controls.Clear();

            FolderView folderView = new ActiveUp.MailSystem.DesktopClient.FolderView();
            folderView.BackColor = System.Drawing.SystemColors.Window;
            folderView.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            folderView.Dock = System.Windows.Forms.DockStyle.Fill;
            folderView.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            panelComponents.Controls.Add(folderView);
        }

        public void toolStripButtonShotcuts_Click(object sender, EventArgs e)
        {
            this.CheckAllFalse();
            this.toolStripButtonShotcuts.Checked = true;
            this.toolStripLabel1.Text = this.toolStripButtonShotcuts.Text;
            this.toolStripLabel2.Text = "All Shortcut Items";
            this.panelComponents.Controls.Clear();

            Label label = new Label();
            label.Text = "\r\nThis is a sample application. This function is not implemented yet.";
            label.Dock = DockStyle.Fill;
            this.panelComponents.Controls.Add(label);
        }

        /// <summary>
        /// Check all tool strip buttons false.
        /// </summary>
        private void CheckAllFalse()
        {
            this.toolStripButtonMail.Checked = false;
            this.toolStripButtonCalendar.Checked = false;
            this.toolStripButtonContacts.Checked = false;
            this.toolStripButtonTasks.Checked = false;
            this.toolStripButtonNotes.Checked = false;
            this.toolStripButtonFolders.Checked = false;
            this.toolStripButtonShotcuts.Checked = false;
        }

        #endregion

    }
}
