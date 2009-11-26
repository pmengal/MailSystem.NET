using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace ActiveQLibrary.CustomControl
{
	/// <summary>
	/// Summary description for LabelGradient.
	/// </summary>
		
	public class LabelGradient : Label
	{
		 
		#region Implementtation Member Fields
		protected Color gradientColorOne = Color.White;
		protected Color gradientColorTwo = Color.Blue;
		protected LinearGradientMode lgm = LinearGradientMode.ForwardDiagonal;
		protected Border3DStyle b3dstyle = Border3DStyle.Bump;
		#endregion


		#region GradientColorOne Properties
		[
		DefaultValue(typeof(Color),"White"),
		Description("The first gradient color."),
		Category("Appearance"),
		]

		//GradientColorOne Properties
		public Color GradientColorOne
		{
			get 
			{
				return gradientColorOne;
            }
			set
			{
				gradientColorOne = value;
				Invalidate();
			}
		}
		#endregion
		
		#region GradientColorTwo Properties
		[
		DefaultValue(typeof(Color),"Blue"),
		Description("The second gradient color."),
		Category("Appearance"),
		]

		//GradientColorTwo Properties
		public Color GradientColorTwo
		{
			get 
			{
				return gradientColorTwo;
			}
			set
			{
				gradientColorTwo = value;
				Invalidate();
			}
		}

		#endregion

		#region LinearGradientMode Properties
		//LinearGradientMode Properties
		[
		DefaultValue(typeof(LinearGradientMode),"ForwardDiagonal"),
		Description("Gradient Mode"),
		Category("Appearance"),
		]
		
		public LinearGradientMode GradientMode
		{
			get 
			{
				return lgm;
			}
			
			set
			{
				lgm = value;
				Invalidate();
			}
		}
		#endregion
        
		#region Border3DStyle Properties
		//Border3DStyle Properties
		[
		DefaultValue(typeof(Border3DStyle),"Bump"),
		Description("BorderStyle"),
		Category("Appearance"),
		]

		// hide BorderStyle inherited from the base class
		new public Border3DStyle BorderStyle
		{
			get
			{
				return b3dstyle;
			}
			set
			{
				b3dstyle = value;
				Invalidate();
			}
		}
		#endregion

		#region Removed Properties
		
		// Remove BackColor Property
		[
		Browsable(false),
		EditorBrowsable(EditorBrowsableState.Never)
		]
		public override System.Drawing.Color BackColor
		{
			get	
			{
				return new System.Drawing.Color();
			}
			set	{;}
		}
		
		#endregion

		public LabelGradient()
		{
			//
			// TODO: Add constructor logic here
            //
		}

		protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
		{
			Graphics gfx = pevent.Graphics;
			
			Rectangle rect = new Rectangle (0,0,this.Width,this.Height);
			
			// Dispose of brush resources after use
			using (LinearGradientBrush lgb = new LinearGradientBrush(rect, gradientColorOne,gradientColorTwo,lgm))
				gfx.FillRectangle(lgb,rect);
			
			ControlPaint.DrawBorder3D(gfx,rect,b3dstyle);
		}

	}
}
