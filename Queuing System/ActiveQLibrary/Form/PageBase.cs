using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace ActiveQLibrary.Form
{
	
	#region enum

	public enum TypePage
	{
		ElemAll = 0,
		DetailsStdMail,
		DetailsSchMail,
		DetailsSchTask,
		MainLog,
		Progress
	}

	#endregion	

	/// <summary>
	/// Summary description for BasePage.
	/// </summary>
	public class PageBase : System.Windows.Forms.UserControl
	{
		private static LinearGradientBrush brush0;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Type of page
		/// </summary>
		private TypePage _type;

		public PageBase()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

		}

		public PageBase(TypePage Type)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call
			_type = Type;

			
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
			// 
			// PageBase
			// 
			this.Location = new System.Drawing.Point(0, 40);
			this.Name = "PageBase";
			this.Size = new System.Drawing.Size(472, 376);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.PageBase_Paint);

		}
		#endregion

		private void PageBase_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			
			//brush0 = new LinearGradientBrush(new Rectangle(this.ClientRectangle.X,this.ClientRectangle.Y,this.ClientRectangle.Width,this.ClientRectangle.Height), Color.FromArgb(244, 243, 238), Color.LightSteelBlue, 90.0f); 
			brush0 = new LinearGradientBrush(new Rectangle(this.ClientRectangle.X,this.ClientRectangle.Y,this.ClientRectangle.Width,this.ClientRectangle.Height), System.Drawing.SystemColors.Control, System.Drawing.SystemColors.Window, 90.0f); 

			float[] relativeIntensities = {0.0f, 0.3f, 1.0f};
			float[] relativePositions   = {0.0f, 0.7f, 1.0f};

			Blend blend = new Blend();
			blend.Factors = relativeIntensities;
			blend.Positions = relativePositions;
			brush0.Blend = blend;


			e.Graphics.FillRectangle(brush0,this.ClientRectangle.X,this.ClientRectangle.Y,this.ClientRectangle.Width,this.ClientRectangle.Height);
		}

	
		#region Properties

		/// <summary>
		/// Get / set the type of page.
		/// </summary>
		public TypePage Type
		{
			get
			{
				return _type;
			}

			set
			{
				_type = value;
			}
		}

		#endregion

	}

}
