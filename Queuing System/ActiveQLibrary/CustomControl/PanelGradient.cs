using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace ActiveQLibrary.CustomControl
{
	/// <summary>
	/// Description résumée de PanelGradient.
	/// </summary>
	public class PanelGradient : Panel
	{
		protected Color gradientColorOne = Color.White;
		protected Color gradientColorTwo = Color.Blue;
		protected LinearGradientMode lgm = LinearGradientMode.ForwardDiagonal;
		protected BorderStyle bdstyle = BorderStyle.FixedSingle;

		public PanelGradient()
		{
			//
			// TODO : ajoutez ici la logique du constructeur
			//
		}

		[
		DefaultValue(typeof(Color),"White"),
		Description("The first gradient color."),
		Category("Appearance"),
		]
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

		[
		DefaultValue(typeof(Color),"Blue"),
		Description("The second gradient color."),
		Category("Appearance"),
		]
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

		[
		DefaultValue(typeof(Border3DStyle),"Bump"),
		Description("BorderStyle"),
		Category("Appearance"),
		]
		new public BorderStyle BorderStyle
		{
			get
			{
				return bdstyle;
			}
			set
			{
				bdstyle = value;
				Invalidate();
			}
		}

		protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
		{
			Graphics gfx = pevent.Graphics;
			
			Rectangle rect = new Rectangle (0,0,this.Width,this.Height);
			
			// Dispose of brush resources after use
			using (LinearGradientBrush lgb = new LinearGradientBrush(rect, gradientColorOne,gradientColorTwo,lgm))
				gfx.FillRectangle(lgb,rect);
			
			ControlPaint.DrawBorder(gfx,rect,Color.Black,ButtonBorderStyle.Solid);
		}
	}
}
