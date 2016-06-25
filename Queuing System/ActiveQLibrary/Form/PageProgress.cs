using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ActiveQLibrary.Form
{
	/// <summary>
	/// Summary description for PageProgress.
	/// </summary>
	public class PageProgress : PageBase
	{
		#region delegate

		delegate void NewValueProgressBarDelegate(string Job, int Value);

		#endregion

		private System.Windows.Forms.ColumnHeader Job;
		private ActiveQLibrary.CustomControl.ListViewVS _lvProgress;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ColumnHeader columnHeader1;

		private System.Collections.Hashtable _listItem = new System.Collections.Hashtable();

		public PageProgress(TypePage Type) : base(Type)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call
			this.Location = new Point(0,52);
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._lvProgress = new ActiveQLibrary.CustomControl.ListViewVS();
			this.Job = new System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// _lvProgress
			// 
			this._lvProgress.CheckBookEvenRowBackColor = System.Drawing.Color.Empty;
			this._lvProgress.CheckBookEvenRowForeColor = System.Drawing.Color.Empty;
			this._lvProgress.CheckBookLookEnabled = false;
			this._lvProgress.CheckBookOddRowBackColor = System.Drawing.Color.Empty;
			this._lvProgress.CheckBookOddRowForeColor = System.Drawing.Color.Empty;
			this._lvProgress.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						  this.Job,
																						  this.columnHeader1});
			this._lvProgress.FullRowSelect = true;
			this._lvProgress.GridLines = true;
			this._lvProgress.HeaderColor = System.Drawing.Color.Empty;
			this._lvProgress.HeaderImageList = null;
			this._lvProgress.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this._lvProgress.Location = new System.Drawing.Point(64, 40);
			this._lvProgress.Name = "_lvProgress";
			this._lvProgress.PaintSortedColumnBackground = true;
			this._lvProgress.Size = new System.Drawing.Size(296, 264);
			this._lvProgress.SortingEnabled = true;
			this._lvProgress.SortOrder = System.Windows.Forms.SortOrder.None;
			this._lvProgress.TabIndex = 0;
			this._lvProgress.View = System.Windows.Forms.View.Details;
			// 
			// Job
			// 
			this.Job.Text = "Job";
			this.Job.Width = 90;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Progress";
			this.columnHeader1.Width = 200;
			// 
			// PageProgress
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this._lvProgress});
			this.Location = new System.Drawing.Point(0, 0);
			this.Name = "PageProgress";
			this.ResumeLayout(false);

		}
		#endregion

		public void AddItem(ProgressItem _newItem)
		{
			if (_newItem != null)
			{
				_listItem.Add(_newItem.job,_newItem);
				
				ListViewItem.ListViewSubItem subItem = null;
				ListViewItem lvi =_lvProgress.Items.Add(_newItem.job);
				lvi.ImageIndex = 0;
				subItem = lvi.SubItems.Add(_newItem.job+"@ProgressBar");
				_lvProgress.AddEmbeddedControl(subItem, _newItem.ProgressBarControl);
			}
		}

		public void NewValueProgressBar(string Job, int Value)
		{
			NewValueProgressBarDelegate newValueDelegate = new NewValueProgressBarDelegate(NewValueProgressBarFct);
			_lvProgress.Invoke(newValueDelegate,new object[] {Job,Value});
		}

		public void NewValueProgressBarFct(string Job, int Value)
		{
			((ProgressItem)_listItem[Job]).ProgressBarControl.Value = Value;
		}
	}
}
