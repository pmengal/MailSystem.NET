using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace ActiveQLibrary.Form
{
	/// <summary>
	/// Summary description for PageButtons.
	/// </summary>
	public class PageButtons : System.Windows.Forms.UserControl
	{
		//private static LinearGradientBrush brush0;

		private ActiveQLibrary.CustomControl.ButtonXP _bStdMail;
		private ActiveQLibrary.CustomControl.ButtonXP _bSchMail;
		private ActiveQLibrary.CustomControl.ButtonXP _bLog;
		private ActiveQLibrary.CustomControl.ButtonXP _bOverview;
		private ActiveQLibrary.CustomControl.ButtonXP _bTask;
		private System.Windows.Forms.ToolTip _toolTipMainLog;
		private System.Windows.Forms.ToolTip _toolTipOverview;
		private System.Windows.Forms.ToolTip _toolTipStdMail;
		private System.Windows.Forms.ToolTip _toolTipSchMail;
		private System.Windows.Forms.ToolTip _toolTipTask;
		private ActiveQLibrary.CustomControl.ButtonXP _bHelp;
		private ActiveQLibrary.CustomControl.ButtonXP _bAbout;
		private System.Windows.Forms.ToolTip _toolTipAbout;
		private System.Windows.Forms.ToolTip _toolTipHelp;
		private ActiveQLibrary.CustomControl.ButtonXP _bProgress;
		private System.ComponentModel.IContainer components;

		public PageButtons()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PageButtons));
			this._bLog = new ActiveQLibrary.CustomControl.ButtonXP();
			this._bOverview = new ActiveQLibrary.CustomControl.ButtonXP();
			this._bStdMail = new ActiveQLibrary.CustomControl.ButtonXP();
			this._bSchMail = new ActiveQLibrary.CustomControl.ButtonXP();
			this._bTask = new ActiveQLibrary.CustomControl.ButtonXP();
			this._toolTipMainLog = new System.Windows.Forms.ToolTip(this.components);
			this._toolTipOverview = new System.Windows.Forms.ToolTip(this.components);
			this._toolTipStdMail = new System.Windows.Forms.ToolTip(this.components);
			this._toolTipSchMail = new System.Windows.Forms.ToolTip(this.components);
			this._toolTipTask = new System.Windows.Forms.ToolTip(this.components);
			this._bHelp = new ActiveQLibrary.CustomControl.ButtonXP();
			this._bAbout = new ActiveQLibrary.CustomControl.ButtonXP();
			this._toolTipAbout = new System.Windows.Forms.ToolTip(this.components);
			this._toolTipHelp = new System.Windows.Forms.ToolTip(this.components);
			this._bProgress = new ActiveQLibrary.CustomControl.ButtonXP();
			this.SuspendLayout();
			// 
			// _bLog
			// 
			this._bLog._Image = null;
			this._bLog.BackColor = System.Drawing.Color.Transparent;
			this._bLog.DefaultScheme = false;
			this._bLog.Image = ((System.Drawing.Bitmap)(resources.GetObject("_bLog.Image")));
			this._bLog.Location = new System.Drawing.Point(8, 8);
			this._bLog.Name = "_bLog";
			this._bLog.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Blue;
			this._bLog.Size = new System.Drawing.Size(53, 39);
			this._bLog.SizeImgButton = new System.Drawing.Size(32, 32);
			this._bLog.TabIndex = 0;
			// 
			// _bOverview
			// 
			this._bOverview._Image = null;
			this._bOverview.BackColor = System.Drawing.Color.Transparent;
			this._bOverview.DefaultScheme = false;
			this._bOverview.Image = ((System.Drawing.Bitmap)(resources.GetObject("_bOverview.Image")));
			this._bOverview.Location = new System.Drawing.Point(64, 9);
			this._bOverview.Name = "_bOverview";
			this._bOverview.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Blue;
			this._bOverview.Size = new System.Drawing.Size(53, 39);
			this._bOverview.SizeImgButton = new System.Drawing.Size(32, 32);
			this._bOverview.TabIndex = 1;
			// 
			// _bStdMail
			// 
			this._bStdMail._Image = null;
			this._bStdMail.BackColor = System.Drawing.Color.Transparent;
			this._bStdMail.DefaultScheme = false;
			this._bStdMail.Image = ((System.Drawing.Bitmap)(resources.GetObject("_bStdMail.Image")));
			this._bStdMail.Location = new System.Drawing.Point(120, 8);
			this._bStdMail.Name = "_bStdMail";
			this._bStdMail.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Blue;
			this._bStdMail.Size = new System.Drawing.Size(53, 39);
			this._bStdMail.SizeImgButton = new System.Drawing.Size(32, 32);
			this._bStdMail.TabIndex = 2;
			// 
			// _bSchMail
			// 
			this._bSchMail._Image = null;
			this._bSchMail.BackColor = System.Drawing.Color.Transparent;
			this._bSchMail.DefaultScheme = false;
			this._bSchMail.Image = ((System.Drawing.Bitmap)(resources.GetObject("_bSchMail.Image")));
			this._bSchMail.Location = new System.Drawing.Point(176, 8);
			this._bSchMail.Name = "_bSchMail";
			this._bSchMail.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Blue;
			this._bSchMail.Size = new System.Drawing.Size(53, 39);
			this._bSchMail.SizeImgButton = new System.Drawing.Size(32, 32);
			this._bSchMail.TabIndex = 3;
			// 
			// _bTask
			// 
			this._bTask._Image = null;
			this._bTask.BackColor = System.Drawing.Color.Transparent;
			this._bTask.DefaultScheme = false;
			this._bTask.Image = ((System.Drawing.Bitmap)(resources.GetObject("_bTask.Image")));
			this._bTask.Location = new System.Drawing.Point(232, 8);
			this._bTask.Name = "_bTask";
			this._bTask.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Blue;
			this._bTask.Size = new System.Drawing.Size(53, 39);
			this._bTask.SizeImgButton = new System.Drawing.Size(32, 32);
			this._bTask.TabIndex = 4;
			// 
			// _bHelp
			// 
			this._bHelp._Image = null;
			this._bHelp.BackColor = System.Drawing.Color.Transparent;
			this._bHelp.DefaultScheme = false;
			this._bHelp.Image = ((System.Drawing.Bitmap)(resources.GetObject("_bHelp.Image")));
			this._bHelp.Location = new System.Drawing.Point(288, 8);
			this._bHelp.Name = "_bHelp";
			this._bHelp.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Blue;
			this._bHelp.Size = new System.Drawing.Size(53, 39);
			this._bHelp.SizeImgButton = new System.Drawing.Size(32, 32);
			this._bHelp.TabIndex = 5;
			// 
			// _bAbout
			// 
			this._bAbout._Image = null;
			this._bAbout.BackColor = System.Drawing.Color.Transparent;
			this._bAbout.DefaultScheme = false;
			this._bAbout.Enabled = false;
			this._bAbout.Image = ((System.Drawing.Bitmap)(resources.GetObject("_bAbout.Image")));
			this._bAbout.Location = new System.Drawing.Point(344, 8);
			this._bAbout.Name = "_bAbout";
			this._bAbout.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Blue;
			this._bAbout.Size = new System.Drawing.Size(53, 39);
			this._bAbout.SizeImgButton = new System.Drawing.Size(32, 32);
			this._bAbout.TabIndex = 6;
			// 
			// _bProgress
			// 
			this._bProgress._Image = null;
			this._bProgress.BackColor = System.Drawing.Color.Transparent;
			this._bProgress.DefaultScheme = false;
			this._bProgress.Enabled = false;
			this._bProgress.Image = ((System.Drawing.Bitmap)(resources.GetObject("_bProgress.Image")));
			this._bProgress.Location = new System.Drawing.Point(400, 8);
			this._bProgress.Name = "_bProgress";
			this._bProgress.Scheme = ActiveQLibrary.CustomControl.ButtonXP.Schemes.Blue;
			this._bProgress.Size = new System.Drawing.Size(53, 39);
			this._bProgress.SizeImgButton = new System.Drawing.Size(32, 32);
			this._bProgress.TabIndex = 7;
			this._bProgress.Visible = false;
			// 
			// PageButtons
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this._bProgress,
																		  this._bAbout,
																		  this._bHelp,
																		  this._bTask,
																		  this._bSchMail,
																		  this._bStdMail,
																		  this._bOverview,
																		  this._bLog});
			this.Name = "PageButtons";
			this.Size = new System.Drawing.Size(474, 55);
			this.Load += new System.EventHandler(this.PageButtons_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.PageButtons_Paint);
			this.ResumeLayout(false);

		}
		#endregion

		private void PageButtons_Load(object sender, System.EventArgs e)
		{
			this._toolTipMainLog.SetToolTip(_bLog,"Main log");
			this._toolTipOverview.SetToolTip(_bOverview,"Overview");
			this._toolTipStdMail.SetToolTip(_bStdMail,"Standard mail");
			this._toolTipSchMail.SetToolTip(_bSchMail,"Scheduled mail");
			this._toolTipTask.SetToolTip(_bTask,"Task");
			this._toolTipHelp.SetToolTip(_bHelp,"Help");
			this._toolTipAbout.SetToolTip(_bAbout,"About");
		}

		private void PageButtons_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
						
			/*brush0 = new LinearGradientBrush(new Rectangle(this.ClientRectangle.X,this.ClientRectangle.Y,this.ClientRectangle.Width,this.ClientRectangle.Height), Color.FromArgb(244, 243, 238), Color.FromArgb(244, 243, 238), 90.0f); 

			float[] relativeIntensities = {0.0f, 0.3f, 1.0f};
			float[] relativePositions   = {0.0f, 0.7f, 1.0f};

			Blend blend = new Blend();
			blend.Factors = relativeIntensities;
			blend.Positions = relativePositions;
			brush0.Blend = blend;


			e.Graphics.FillRectangle(brush0,this.ClientRectangle.X,this.ClientRectangle.Y,this.ClientRectangle.Width,this.ClientRectangle.Height);*/
		}

		#region Properties

		public ActiveQLibrary.CustomControl.ButtonXP ButtonLog
		{
			get
			{
				return _bLog;
			}
		}

		public ActiveQLibrary.CustomControl.ButtonXP ButtonOverview
		{
			get
			{
				return _bOverview;
			}
		}

		public ActiveQLibrary.CustomControl.ButtonXP ButtonStandardMail
		{
			get
			{
				return _bStdMail;
			}
		}

		public ActiveQLibrary.CustomControl.ButtonXP ButtonScheduledMail
		{
			get
			{
				return _bSchMail;
			}
		}

		public ActiveQLibrary.CustomControl.ButtonXP ButtonTask
		{
			get
			{
				return _bTask;
			}
		}

		public ActiveQLibrary.CustomControl.ButtonXP ButtonHelp
		{
			get
			{
				return _bHelp;
			}
		}

		public ActiveQLibrary.CustomControl.ButtonXP ButtonAbout
		{
			get
			{
				return _bAbout;
			}
		}

		public ActiveQLibrary.CustomControl.ButtonXP ButtonProgress
		{
			get
			{
				return _bProgress;
			}
		}

		#endregion
	}
}
