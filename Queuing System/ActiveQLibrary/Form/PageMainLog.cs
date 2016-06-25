using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ActiveQLibrary.Form
{
	/// <summary>
	/// Summary description for PageMainLog.
	/// </summary>
	public class PageMainLog : PageBase
	{
		private System.Windows.Forms.ListBox _lbMainLog;
		private System.Windows.Forms.Panel panel1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PageMainLog(TypePage Type) : base(Type)
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
			this._lbMainLog = new System.Windows.Forms.ListBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// _lbMainLog
			// 
			this._lbMainLog.HorizontalScrollbar = true;
			this._lbMainLog.Location = new System.Drawing.Point(3, 4);
			this._lbMainLog.Name = "_lbMainLog";
			this._lbMainLog.Size = new System.Drawing.Size(451, 355);
			this._lbMainLog.TabIndex = 0;
			this._lbMainLog.SelectedIndexChanged += new System.EventHandler(this._lbMainLog_SelectedIndexChanged);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this._lbMainLog});
			this.panel1.Location = new System.Drawing.Point(6, 8);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(462, 365);
			this.panel1.TabIndex = 1;
			// 
			// PageMainLog
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel1});
			this.Location = new System.Drawing.Point(0, 0);
			this.Name = "PageMainLog";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public void AddLog(string Elem)
		{
			_lbMainLog.Items.Add(Elem);
			_lbMainLog.SelectedIndex = _lbMainLog.Items.Count - 1;
			_lbMainLog.ClearSelected();
		}

		private void _lbMainLog_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			_lbMainLog.ClearSelected();
		}

		public void ClearLog()
		{
			_lbMainLog.Items.Clear();
		}
	}
}
