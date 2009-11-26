using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;


namespace ActiveQLibrary.CustomControl
{
	public struct BT
	{
		public const int LeftMargin = 7;
		public const int TextMargin = 7;
	}

	public class ButtonXP : System.Windows.Forms.Control
	{

		[DllImport("UxTheme.dll")]
		public static extern int GetCurrentThemeName(StringBuilder pszThemeFileName, int dwMaxNameChars, 
			StringBuilder pszColorBuff, int cchMaxColorChars, 
			StringBuilder pszSizeBuff, int cchMaxSizeChars);



		[DllImport("UxTheme.dll")]
		public static extern bool IsAppThemed();

		private System.ComponentModel.Container components = null;
		public enum States
		{
			Normal,
			MouseOver,
			Pushed
		}
		private States state = States.Normal;

		private Size _size;

		public enum Schemes
		{
			Blue = 0,
			OliveGreen,
			Silver
		}
		private Schemes scheme = Schemes.Silver;

		private Image image;
		private Image _image;
		private Rectangle bounds;
		private bool selected;
		private bool defaultScheme;

		private Rectangle[] rects0; 
		private Rectangle[] rects1; 

		private static Pen						
			pen0, pen1, pen2,
			bluePen01, bluePen02, bluePen03, bluePen04, bluePen05, bluePen06, 
			bluePen07, bluePen08, bluePen09, bluePen10, bluePen11, bluePen12, bluePen13,
			olivePen01, olivePen02, olivePen03, olivePen04, olivePen05, olivePen06, 
			olivePen07, olivePen08, olivePen09, olivePen10, olivePen11, olivePen12,
			olivePen13;

		private static LinearGradientBrush		
			blueBrush01, blueBrush02, blueBrush03,
			oliveBrush01, oliveBrush02, oliveBrush03,
			silverBrush02;
		private static SolidBrush				
			brush1, silverBrush01, silverBrush03;



										

		public ButtonXP()
		{	
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			this.SetStyle(ControlStyles.StandardDoubleClick, false);
			this.SetStyle(ControlStyles.Selectable, true);
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				DisposePensBrushes();
				if(components != null)
				{	
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		public bool DefaultScheme
		{
			get
			{
				return defaultScheme;
			}
			set
			{
				defaultScheme = value;
				if (defaultScheme)
				{
					try
					{
						StringBuilder sb1 = new StringBuilder(256);
						StringBuilder sb2 = new StringBuilder(256);
						StringBuilder sb3 = new StringBuilder(256);
						int i = GetCurrentThemeName(sb1, sb1.Capacity, sb2, sb2.Capacity, sb3, sb3.Capacity);

						string str = sb2.ToString();

						switch(str)
						{
							case @"HomeStead":
								scheme = Schemes.OliveGreen;
								break;
							case @"Metallic":
								scheme = Schemes.Silver;
								break;
							default:
								scheme = Schemes.Blue;
								break;
						}
					}
					catch (Exception)
					{
						return;
					}
					this.Invalidate();
				}
			}

		}

		public Schemes Scheme
		{
			get
			{
				return scheme;
			}
			set
			{
				scheme = value;
				this.Invalidate();
			}
		}

		public Image Image
		{
			get
			{
				return image;
			}
			set
			{
				image = value;
				this.Invalidate();
			}
		}

		public Size SizeImgButton
		{
			get
			{
				return _size;
			}
			set
			{
				_size = value;
				if (_size.Height != 0 && _size.Width != 0 && image != null)
					image = new Bitmap(image, _size);
				this.Invalidate();
			}
		}

		public Image _Image
		{
			get
			{
				return _image;
			}
			set
			{
				_image = value;
				this.Invalidate();
			}
		}


		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				this.Invalidate();
			}
		}

		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			//if (this.Parent != null && !this.Parent.ContainsFocus) return;

	
			if (bounds.Contains(e.X, e.Y))
			{
				if (state == States.Normal)
				{
					state = States.MouseOver;
					this.Invalidate(bounds);
				}
			}
			else
			{
				if (state != States.Normal)
				{
					state = States.Normal;
					this.Invalidate(bounds);
				}
			}
			
		}

		protected override void OnMouseLeave(System.EventArgs e)
		{
			if (state != States.Normal)
			{
				state = States.Normal;
				this.Invalidate(bounds);
			}
		}

		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{

			if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;

			if (bounds.Contains(e.X, e.Y))
			{
				state = States.Pushed;
				this.Focus();
			} 
			else
			{
				state = States.Normal;
			}
			this.Invalidate(bounds);
		}

		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				state = States.Normal;
				this.Invalidate(bounds);
			}
		}


		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			SolidBrush				_brush01, _brush;
			LinearGradientBrush		_brush02, _brush03, _brush04;
			Pen						_pen01, _pen02, _pen03, _pen04, _pen05, _pen06;

			int X = this.Width;
			int Y = this.Height;

			e.Graphics.CompositingQuality = CompositingQuality.GammaCorrected;

			int y = 0;
			int x = 0;
			Point point;

			int _y = (this.Height - this.Font.Height) / 2;
			if (image != null) 
			{
				if (this.Text.Length == 0) x = (X - image.Width)/2;
				else x = BT.LeftMargin;
				y = (this.Height - image.Height) / 2;
				point = new Point(BT.LeftMargin + image.Width + BT.TextMargin, _y);
			}
			else 
			{
				Size size = GetTextSize(e.Graphics, this.Text, this.Font, this.Size);
				int textWidth = size.Width;
				point = new Point((X - textWidth)/2, _y);					
			}
			

			if (!this.Enabled)
			{
				switch (scheme)
				{
					case Schemes.OliveGreen:
						_brush = new SolidBrush(Color.FromArgb(64, 202, 196, 184));
						_brush01 = new SolidBrush(Color.FromArgb(246, 242, 233));
						_pen01 = new Pen(Color.FromArgb(202, 196, 184));
						_pen02 = new Pen(Color.FromArgb(170, 202, 196, 184));
						break;

					case Schemes.Silver:
						_brush = new SolidBrush(Color.FromArgb(64, 196, 195, 191));
						_brush01 = new SolidBrush(Color.FromArgb(241, 241, 237));
						_pen01 = new Pen(Color.FromArgb(196, 195, 191));
						_pen02 = new Pen(Color.FromArgb(170, 196, 195, 191));
						break;

					default:
						_brush = new SolidBrush(Color.FromArgb(64, 201, 199, 186));
						_brush01 = new SolidBrush(Color.FromArgb(245, 244, 234));
						_pen01 = new Pen(Color.FromArgb(201, 199, 186));
						_pen02 = new Pen(Color.FromArgb(170, 201, 199, 186));
						break;
				}

				e.Graphics.FillRectangle(_brush01, 2, 2, X-4, Y-4);
				e.Graphics.DrawLine(_pen01, 3, 1, X-4, 1);
				e.Graphics.DrawLine(_pen01, 3, Y-2, X-4, Y-2);
				e.Graphics.DrawLine(_pen01, 1, 3, 1, Y-4);
				e.Graphics.DrawLine(_pen01, X-2, 3, X-2, Y-4);

				e.Graphics.DrawLine(_pen02, 1, 2, 2, 1);
				e.Graphics.DrawLine(_pen02, 1, Y-3, 2, Y-2);
				e.Graphics.DrawLine(_pen02, X-2, 2, X-3, 1);
				e.Graphics.DrawLine(_pen02, X-2, Y-3, X-3, Y-2);
				e.Graphics.FillRectangles(_brush, rects1);

				if (image != null) ControlPaint.DrawImageDisabled(e.Graphics, image, x, y, this.BackColor);
				e.Graphics.DrawString(this.Text, this.Font, SystemBrushes.ControlDark, point);

				_brush.Dispose();
				_brush01.Dispose();
				_pen01.Dispose();
				_pen02.Dispose();
				return;
			}
			
			LinearGradientBrush _brush05, _brush06;

			switch (scheme)
			{
				case Schemes.OliveGreen:
					oliveBrush01 = new LinearGradientBrush(new Rectangle(2, 2, X-5, Y-7)
						, Color.FromArgb(255, 255, 246), Color.FromArgb(246, 243, 224), 90.0f);

					olivePen01 = new Pen(Color.FromArgb(243, 238, 219));
					olivePen02 = new Pen(Color.FromArgb(236, 225, 201));
					olivePen03 = new Pen(Color.FromArgb(227, 209, 184));

					_brush05 = new LinearGradientBrush(new Rectangle(X-3, 4, 1, Y-5)
						, Color.FromArgb(251, 247, 232), Color.FromArgb(64, 216, 181, 144), 90.0f);
					_brush06 = new LinearGradientBrush(new Rectangle(X-2, 4, 1, Y-5)
						, Color.FromArgb(246, 241, 224), Color.FromArgb(64, 194, 156, 120), 90.0f);

					olivePen04 = new Pen(_brush05);
					olivePen05 = new Pen(_brush06);

					oliveBrush02 = new LinearGradientBrush(new Rectangle(2, 3, X-4, Y-8)
						, Color.FromArgb(177, 203, 128), Color.FromArgb(144, 193, 84), 90.0f);

					olivePen06 = new Pen(Color.FromArgb(194, 209, 143));
					olivePen07 = new Pen(Color.FromArgb(177, 203, 128));
					olivePen08 = new Pen(Color.FromArgb(144, 193, 84));
					olivePen09 = new Pen(Color.FromArgb(168, 167, 102));

					oliveBrush03 = new LinearGradientBrush(new Rectangle(2, 3, X-4, Y-8)
						, Color.FromArgb(237, 190, 150), Color.FromArgb(227, 145, 79), 90.0f);
					olivePen10 = new Pen(Color.FromArgb(252, 197, 149));
					olivePen11 = new Pen(Color.FromArgb(237, 190, 150));
					olivePen12 = new Pen(Color.FromArgb(227, 145, 79));
					olivePen13 = new Pen(Color.FromArgb(207, 114, 37));

					brush1 = new SolidBrush(Color.FromArgb(92, 109, 138, 77));
					pen0 = new Pen(Color.FromArgb(55, 98, 6));
					pen1 = new Pen(Color.FromArgb(109, 138, 77));
					pen2 = new Pen(Color.FromArgb(192, 109, 138, 77));

					_brush05.Dispose();
					_brush06.Dispose();
					break;

				case Schemes.Silver:
					silverBrush01 = new SolidBrush(Color.White);
					silverBrush02 = new LinearGradientBrush(new Rectangle(3, 3, X-6, Y-7)
						, Color.FromArgb(253, 253, 253), Color.FromArgb(201, 200, 220), 90.0f);
					silverBrush03 = new SolidBrush(Color.FromArgb(198, 197, 215));

					float[] relativeIntensities = {0.0f, 0.008f, 1.0f};
					float[] relativePositions   = {0.0f, 0.32f, 1.0f};

					Blend blend = new Blend();
					blend.Factors = relativeIntensities;
					blend.Positions = relativePositions;
					silverBrush02.Blend = blend;

					blueBrush02 = new LinearGradientBrush(new Rectangle(2, 3, X-4, Y-8)
						, Color.FromArgb(186, 211, 245), Color.FromArgb(137, 173, 228), 90.0f);

					bluePen06 = new Pen(Color.FromArgb(206, 231, 255));
					bluePen07 = new Pen(Color.FromArgb(188, 212, 246));
					bluePen08 = new Pen(Color.FromArgb(137, 173, 228));
					bluePen09 = new Pen(Color.FromArgb(105, 130, 238));

					blueBrush03 = new LinearGradientBrush(new Rectangle(2, 3, X-4, Y-8)
						, Color.FromArgb(253, 216, 137), Color.FromArgb(248, 178, 48), 90.0f);

					bluePen10 = new Pen(Color.FromArgb(255, 240, 207));
					bluePen11 = new Pen(Color.FromArgb(253, 216, 137));
					bluePen12 = new Pen(Color.FromArgb(248, 178, 48));
					bluePen13 = new Pen(Color.FromArgb(229, 151, 0));

					brush1 = new SolidBrush(Color.FromArgb(92, 85, 125, 162));
					pen0 = new Pen(Color.FromArgb(0, 60, 116));
					pen1 = new Pen(Color.FromArgb(85, 125, 162));
					pen2 = new Pen(Color.FromArgb(192, 85, 125, 162));
					break;

				default:
					blueBrush01 = new LinearGradientBrush(new Rectangle(2, 2, X-5, Y-7)
						, Color.FromArgb(255, 255, 255), Color.FromArgb(240, 240, 234), 90.0f);
					blueBrush02 = new LinearGradientBrush(new Rectangle(2, 3, X-4, Y-8)
						, Color.FromArgb(186, 211, 245), Color.FromArgb(137, 173, 228), 90.0f);

					bluePen01 = new Pen(Color.FromArgb(236, 235, 230));
					bluePen02 = new Pen(Color.FromArgb(226, 223, 214));
					bluePen03 = new Pen(Color.FromArgb(214, 208, 197));

					_brush05 = new LinearGradientBrush(new Rectangle(X-3, 4, 1, Y-5)
						, Color.FromArgb(245, 244, 242), Color.FromArgb(64, 186, 174, 160), 90.0f);
					_brush06 = new LinearGradientBrush(new Rectangle(X-2, 4, 1, Y-5)
						, Color.FromArgb(240, 238, 234), Color.FromArgb(64, 175, 168, 142), 90.0f);

					bluePen04 = new Pen(_brush05);
					bluePen05 = new Pen(_brush06);

					bluePen06 = new Pen(Color.FromArgb(206, 231, 255));
					bluePen07 = new Pen(Color.FromArgb(188, 212, 246));
					bluePen08 = new Pen(Color.FromArgb(137, 173, 228));
					bluePen09 = new Pen(Color.FromArgb(105, 130, 238));

					blueBrush03 = new LinearGradientBrush(new Rectangle(2, 3, X-4, Y-8)
						, Color.FromArgb(253, 216, 137), Color.FromArgb(248, 178, 48), 90.0f);
					bluePen10 = new Pen(Color.FromArgb(255, 240, 207));
					bluePen11 = new Pen(Color.FromArgb(253, 216, 137));
					bluePen12 = new Pen(Color.FromArgb(248, 178, 48));
					bluePen13 = new Pen(Color.FromArgb(229, 151, 0));

					brush1 = new SolidBrush(Color.FromArgb(92, 85, 125, 162));
					pen0 = new Pen(Color.FromArgb(0, 60, 116));
					pen1 = new Pen(Color.FromArgb(85, 125, 162));
					pen2 = new Pen(Color.FromArgb(192, 85, 125, 162));

					_brush05.Dispose();
					_brush06.Dispose();
					break;				
			}

			LinearGradientBrush brush0 = new LinearGradientBrush(new Rectangle(0, 0, X, Y)
				, Color.FromArgb(64, 171, 168, 137), Color.FromArgb(92, 255, 255, 255), 85.0f);
			e.Graphics.FillRectangle(brush0, new Rectangle(0, 0, X, Y));
			brush0.Dispose();

			switch (state)
			{
				case States.Normal:
				switch (scheme)
				{
					case Schemes.Silver:
						e.Graphics.FillRectangle(silverBrush01, 2, 2, X-4, Y-4);
						e.Graphics.FillRectangle(silverBrush02, 3, 4, X-6, Y-8);
						e.Graphics.FillRectangle(silverBrush03, 2, Y-4, X-4, 2);

						if (selected)
						{
							_pen01 = new Pen(Color.White);
							
							e.Graphics.FillRectangles(blueBrush02, rects0);
							e.Graphics.DrawLine(_pen01, 3, 4, 3, Y-4);
							e.Graphics.DrawLine(_pen01, X-4, 4, X-4, Y-4);
							e.Graphics.DrawLine(bluePen06, 2, 2, X-3, 2);
							e.Graphics.DrawLine(bluePen07, 2, 3, X-3, 3);
							e.Graphics.DrawLine(bluePen08, 2, Y-4, X-3, Y-4);
							e.Graphics.DrawLine(bluePen09, 2, Y-3, X-3, Y-3);

							_pen01.Dispose();
						}
						break;

					case Schemes.OliveGreen:
						e.Graphics.FillRectangle(oliveBrush01, 2, 2, X-4, Y-7);
						e.Graphics.DrawLine(olivePen01, 2, Y-5, X-2, Y-5);
						e.Graphics.DrawLine(olivePen02, 2, Y-4, X-2, Y-4);
						e.Graphics.DrawLine(olivePen03, 2, Y-3, X-2, Y-3);
						e.Graphics.DrawLine(olivePen04, X-4, 4, X-4, Y-5);
						e.Graphics.DrawLine(olivePen05, X-3, 4, X-3, Y-5);

						if (selected)
						{
							e.Graphics.FillRectangles(oliveBrush02, rects0);
							e.Graphics.DrawLine(olivePen06, 2, 2, X-3, 2);
							e.Graphics.DrawLine(olivePen07, 2, 3, X-3, 3);
							e.Graphics.DrawLine(olivePen08, 2, Y-4, X-3, Y-4);
							e.Graphics.DrawLine(olivePen09, 2, Y-3, X-3, Y-3);
						}
						break;

					default:
						e.Graphics.FillRectangle(blueBrush01, 2, 2, X-4, Y-7);
						e.Graphics.DrawLine(bluePen01, 2, Y-5, X-2, Y-5);
						e.Graphics.DrawLine(bluePen02, 2, Y-4, X-2, Y-4);
						e.Graphics.DrawLine(bluePen03, 2, Y-3, X-2, Y-3);
						e.Graphics.DrawLine(bluePen04, X-4, 4, X-4, Y-5);
						e.Graphics.DrawLine(bluePen05, X-3, 4, X-3, Y-5);

						if (selected)
						{
							e.Graphics.FillRectangles(blueBrush02, rects0);
							e.Graphics.DrawLine(bluePen06, 2, 2, X-3, 2);
							e.Graphics.DrawLine(bluePen07, 2, 3, X-3, 3);
							e.Graphics.DrawLine(bluePen08, 2, Y-4, X-3, Y-4);
							e.Graphics.DrawLine(bluePen09, 2, Y-3, X-3, Y-3);
						}
						break;
				}
					break;

				case States.MouseOver:
				switch (scheme)
				{
					case Schemes.Silver:
						e.Graphics.FillRectangle(silverBrush01, 2, 2, X-4, Y-4);
						e.Graphics.FillRectangle(silverBrush02, 3, 4, X-6, Y-8);
						e.Graphics.FillRectangle(silverBrush03, 2, Y-4, X-4, 2);
							
						e.Graphics.FillRectangles(blueBrush03, rects0);
						e.Graphics.DrawLine(bluePen10, 2, 2, X-3, 2);
						e.Graphics.DrawLine(bluePen11, 2, 3, X-3, 3);
						e.Graphics.DrawLine(bluePen12, 2, Y-4, X-3, Y-4);
						e.Graphics.DrawLine(bluePen13, 2, Y-3, X-3, Y-3);
						break;

					case Schemes.OliveGreen:
						e.Graphics.FillRectangle(oliveBrush01, 2, 2, X-4, Y-7);
						e.Graphics.DrawLine(olivePen01, 2, Y-5, X-4, Y-5);
						e.Graphics.DrawLine(olivePen02, 2, Y-4, X-4, Y-4);
						e.Graphics.DrawLine(olivePen03, 2, Y-3, X-4, Y-3);
						e.Graphics.DrawLine(olivePen04, X-4, 4, X-4, Y-5);
						e.Graphics.DrawLine(olivePen05, X-3, 4, X-3, Y-5);

						e.Graphics.FillRectangles(oliveBrush03, rects0);
						e.Graphics.DrawLine(olivePen10, 2, 2, X-3, 2);
						e.Graphics.DrawLine(olivePen11, 2, 3, X-3, 3);
						e.Graphics.DrawLine(olivePen12, 2, Y-4, X-3, Y-4);
						e.Graphics.DrawLine(olivePen13, 2, Y-3, X-3, Y-3);
						break;

					default:
						e.Graphics.FillRectangle(blueBrush01, 2, 2, X-4, Y-7);
						e.Graphics.DrawLine(bluePen01, 2, Y-5, X-4, Y-5);
						e.Graphics.DrawLine(bluePen02, 2, Y-4, X-4, Y-4);
						e.Graphics.DrawLine(bluePen03, 2, Y-3, X-4, Y-3);
						e.Graphics.DrawLine(bluePen04, X-4, 4, X-4, Y-5);
						e.Graphics.DrawLine(bluePen05, X-3, 4, X-3, Y-5);

						e.Graphics.FillRectangles(blueBrush03, rects0);
						e.Graphics.DrawLine(bluePen10, 2, 2, X-3, 2);
						e.Graphics.DrawLine(bluePen11, 2, 3, X-3, 3);
						e.Graphics.DrawLine(bluePen12, 2, Y-4, X-3, Y-4);
						e.Graphics.DrawLine(bluePen13, 2, Y-3, X-3, Y-3);
						break;
				}
					break;

				case States.Pushed:
				switch (scheme)
				{
					case Schemes.Silver:
						_brush01 = new SolidBrush(Color.White);
						_brush02 = new LinearGradientBrush(new Rectangle(3, 3, X-5, Y-8)
							, Color.FromArgb(172, 171, 191), Color.FromArgb(248, 252, 253), 90.0f);

						float[] relativeIntensities = {0.0f, 0.992f, 1.0f};
						float[] relativePositions   = {0.0f, 0.68f, 1.0f};

						Blend blend = new Blend();
						blend.Factors = relativeIntensities;
						blend.Positions = relativePositions;
						_brush02.Blend = blend;
					
						_pen01 = new Pen(Color.FromArgb(172, 171, 189));

						e.Graphics.FillRectangle(_brush01, 2, 2, X-4, Y-4);
						e.Graphics.FillRectangle(_brush02, 3, 4, X-6, Y-9);
						e.Graphics.DrawLine(_pen01, 4, 3, X-4, 3);

						_brush01.Dispose();
						_brush02.Dispose();
						_pen01.Dispose();
						break;

					case Schemes.OliveGreen:
						_brush02 = new LinearGradientBrush(new Rectangle(2, 3, X-4, Y-7)
							, Color.FromArgb(228, 212, 191), Color.FromArgb(229, 217, 195), 90.0f);
						_brush04 = new LinearGradientBrush(new Rectangle(3, 3, X-4, Y-7)
							, Color.FromArgb(232, 219, 197), Color.FromArgb(234, 224, 201), 90.0f);

						_brush03 = new LinearGradientBrush(new Rectangle(2, 2, X-5, Y-7)
							, Color.FromArgb(238, 230, 210), Color.FromArgb(236, 228, 206), 90.0f);
						
						_pen01 = new Pen(Color.FromArgb(223, 205, 180));
						_pen02 = new Pen(Color.FromArgb(231, 217, 195));
						_pen03 = new Pen(Color.FromArgb(242, 236, 216));
						_pen04 = new Pen(Color.FromArgb(248, 244, 228));
						_pen05 = new Pen(_brush02);
						_pen06 = new Pen(_brush04);

						e.Graphics.FillRectangle(_brush03, 2, 4, X-4, Y-8);
						e.Graphics.DrawLine(_pen05, 2, 3, 2, Y-4);
						e.Graphics.DrawLine(_pen06, 3, 3, 3, Y-4);
						e.Graphics.DrawLine(_pen01, 2, 2, X-3, 2);
						e.Graphics.DrawLine(_pen02, 2, 3, X-3, 3);
						e.Graphics.DrawLine(_pen03, 2, Y-4, X-3, Y-4);
						e.Graphics.DrawLine(_pen04, 2, Y-3, X-3, Y-3);

						_brush02.Dispose();
						_brush03.Dispose();
						_brush04.Dispose();
						_pen01.Dispose();
						_pen02.Dispose();
						_pen03.Dispose();
						_pen04.Dispose();
						_pen05.Dispose();
						_pen06.Dispose();
						break;

					default:
						_brush02 = new LinearGradientBrush(new Rectangle(2, 3, X-4, Y-7)
							, Color.FromArgb(216, 212, 203), Color.FromArgb(218, 216, 207), 90.0f);
						_brush04 = new LinearGradientBrush(new Rectangle(3, 3, X-4, Y-7)
							, Color.FromArgb(221, 218, 209), Color.FromArgb(223, 222, 214), 90.0f);

						_brush03 = new LinearGradientBrush(new Rectangle(2, 2, X-5, Y-7)
							, Color.FromArgb(229, 228, 221), Color.FromArgb(226, 226, 218), 90.0f);
						
						_pen01 = new Pen(Color.FromArgb(209, 204, 192));
						_pen02 = new Pen(Color.FromArgb(220, 216, 207));
						_pen03 = new Pen(Color.FromArgb(234, 233, 227));
						_pen04 = new Pen(Color.FromArgb(242, 241, 238));
						_pen05 = new Pen(_brush02);
						_pen06 = new Pen(_brush04);

						e.Graphics.FillRectangle(_brush03, 2, 4, X-4, Y-8);
						e.Graphics.DrawLine(_pen05, 2, 3, 2, Y-4);
						e.Graphics.DrawLine(_pen06, 3, 3, 3, Y-4);
						e.Graphics.DrawLine(_pen01, 2, 2, X-3, 2);
						e.Graphics.DrawLine(_pen02, 2, 3, X-3, 3);
						e.Graphics.DrawLine(_pen03, 2, Y-4, X-3, Y-4);
						e.Graphics.DrawLine(_pen04, 2, Y-3, X-3, Y-3);

						_brush02.Dispose();
						_brush03.Dispose();
						_brush04.Dispose();
						_pen01.Dispose();
						_pen02.Dispose();
						_pen03.Dispose();
						_pen04.Dispose();
						_pen05.Dispose();
						_pen06.Dispose();
						break;
				}
					break;				
			}

			if (image != null) e.Graphics.DrawImage(image, x, y);
			e.Graphics.DrawString(this.Text, this.Font, SystemBrushes.ControlText, point);

				
			e.Graphics.DrawLine(pen1, 1, 3, 3, 1);
			e.Graphics.DrawLine(pen1, X-2, 3, X-4, 1);
			e.Graphics.DrawLine(pen1, 1, Y-4, 3, Y-2);
			e.Graphics.DrawLine(pen1, X-2, Y-4, X-4, Y-2);

			e.Graphics.DrawLine(pen2, 1, 2, 2, 1);
			e.Graphics.DrawLine(pen2, 1, Y-3, 2, Y-2);
			e.Graphics.DrawLine(pen2, X-2, 2, X-3, 1);
			e.Graphics.DrawLine(pen2, X-2, Y-3, X-3, Y-2);						

			e.Graphics.DrawLine(pen0, 3, 1, X-4, 1);
			e.Graphics.DrawLine(pen0, 3, Y-2, X-4, Y-2);
			e.Graphics.DrawLine(pen0, 1, 3, 1, Y-4);
			e.Graphics.DrawLine(pen0, X-2, 3, X-2, Y-4);

			e.Graphics.FillRectangles(brush1, rects1);

			DisposePensBrushes();
		}

		private void CreatePensBrushes()
		{
		}

		private void DisposePensBrushes()
		{
			brush1.Dispose();
			pen0.Dispose();
			pen1.Dispose();
			pen2.Dispose();
			switch (scheme)
			{
				case Schemes.OliveGreen:
					oliveBrush01.Dispose();
					oliveBrush02.Dispose();
					oliveBrush03.Dispose();
					olivePen01.Dispose();
					olivePen02.Dispose();
					olivePen03.Dispose();
					olivePen04.Dispose();
					olivePen05.Dispose();
					olivePen06.Dispose();
					olivePen07.Dispose();
					olivePen08.Dispose();
					olivePen09.Dispose();
					olivePen10.Dispose();
					olivePen11.Dispose();
					olivePen12.Dispose();
					olivePen13.Dispose();
					break;

				case Schemes.Silver:
					silverBrush01.Dispose();
					silverBrush02.Dispose();
					silverBrush03.Dispose();

					blueBrush02.Dispose();
					blueBrush03.Dispose();
					bluePen06.Dispose();
					bluePen07.Dispose();
					bluePen08.Dispose();
					bluePen09.Dispose();
					bluePen10.Dispose();
					bluePen11.Dispose();
					bluePen12.Dispose();
					bluePen13.Dispose();
					break;

				default:
					blueBrush01.Dispose();
					blueBrush02.Dispose();
					blueBrush03.Dispose();
					bluePen01.Dispose();
					bluePen02.Dispose();
					bluePen03.Dispose();
					bluePen04.Dispose();
					bluePen05.Dispose();
					bluePen06.Dispose();
					bluePen07.Dispose();
					bluePen08.Dispose();
					bluePen09.Dispose();
					bluePen10.Dispose();
					bluePen11.Dispose();
					bluePen12.Dispose();
					bluePen13.Dispose();
					break;
			}
		}

		protected override void OnClick(System.EventArgs e)
		{
			if (state == States.Pushed)
			{
				state = States.Normal;
				this.Invalidate(bounds);
				base.OnClick(e);
			}
		}

		private void InitializeComponent()
		{
			// 
			// ButtonXP
			// 
			this.Name = "ButtonXP";
			this.Size = new System.Drawing.Size(86, 22);
		}

		protected override void OnEnter(System.EventArgs e)
		{
			selected = true;
			this.Invalidate(bounds);
		}

		protected override void OnLeave(System.EventArgs e)
		{
			selected = false;
			this.Invalidate(bounds);
		}

		protected override void OnParentChanged(System.EventArgs e)
		{
			if (this.Parent == null) return;

			int X = this.Width;
			int Y = this.Height;

			this.bounds = new Rectangle(0, 0, X, Y);

			rects0 = new Rectangle[2];
			rects0[0] = new Rectangle(2, 4, 2, Y-8);
			rects0[1] = new Rectangle(X-4, 4, 2, Y-8);

			rects1 = new Rectangle[8];
			rects1[0] = new Rectangle(2, 1, 2, 2); 
			rects1[1] =	new Rectangle(1, 2, 2, 2);
			rects1[2] =	new Rectangle(X-4, 1, 2, 2);
			rects1[3] =	new Rectangle(X-3, 2, 2, 2);
			rects1[4] =	new Rectangle(2, Y-3, 2, 2);
			rects1[5] =	new Rectangle(1, Y-4, 2, 2);
			rects1[6] =	new Rectangle(X-4, Y-3, 2, 2);
			rects1[7] =	new Rectangle(X-3, Y-4, 2, 2);

			//this.BackColor = Color.FromArgb(0, this.Parent.BackColor);
			
			Point[] points = {
								 new Point(1, 0),
								 new Point(X-1, 0),
								 new Point(X-1, 1),
								 new Point(X, 1),
								 new Point(X, Y-1),
								 new Point(X-1, Y-1),
								 new Point(X-1, Y),
								 new Point(1, Y),
								 new Point(1, Y-1),
								 new Point(0, Y-1),
								 new Point(0, 1),
								 new Point(1, 1)};

			GraphicsPath path = new GraphicsPath();
			path.AddLines(points);

			this.Region = new Region(path);

		}

		protected override void OnKeyDown(KeyEventArgs ke)
		{
			if (ke.KeyData == Keys.Enter)
			{
				OnMouseDown(new MouseEventArgs(MouseButtons.Left, 1, 2, 2, 0));
			}
		}

		protected override void OnKeyUp(KeyEventArgs ke)
		{
			if (ke.KeyData == Keys.Enter)
			{
				OnClick(new EventArgs());
			}
		}

		protected override void OnResize(System.EventArgs e)
		{
			bounds = new Rectangle(0, 0, this.Width, this.Height);
			OnParentChanged(e);
			this.Invalidate(bounds);
		}

		protected override void OnEnabledChanged(System.EventArgs e)
		{
			this.Invalidate(bounds);
		}

		public static Size GetTextSize(Graphics graphics, string text, Font font, Size size)
		{
			StringFormat format = new StringFormat();
			SizeF stringSize = graphics.MeasureString(text, font, size.Width, format);
			
			return new Size((int)stringSize.Width, (int)stringSize.Height);
		}

	}
}
