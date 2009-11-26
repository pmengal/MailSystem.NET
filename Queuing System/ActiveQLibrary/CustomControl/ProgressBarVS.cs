using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace ActiveQLibrary.CustomControl
{
	#region Enumerations
	public enum ProgressBarProperty
	{
		BackgroundColor,
		ForegroundColor,
		BorderColor,
		Border3D,
		Enable3DBorder,
		Value,
		Step,
		Minimun,
		Maximun,
		Smooth,
		ShowProgressText,
		ProgressText,
		BackgroundBitmap,
		ForegroundBitmap,
		ProgressTextHiglightColor,
		ProgressTextColor,
		GradientStartColor,
		GradientMiddleColor,
		GradientEndColor,
        WaitingGradientSize,
		WaitingSpeed,
		WaitingStep,
		Orientation
	}

	#endregion
    
	#region Delegates
	// I put the delegate outside the class so that the user does not have
	// to prefix the progressbar class name to use this delegate
	// Putting them inside the class just make them hard to use

	// Declare the property change event signature
	public delegate void ProgressBarPropertyChangedHandler(ProgressBarVS pogressBar, ProgressBarProperty prop);
	#endregion

	/// <summary>
	/// Summary description for FlatProgressBar.
	/// </summary>
	[ToolboxItem(false)]
	public class ProgressBarVS : System.Windows.Forms.Control
	{
		
		#region Enumerations
		// We need to know how we are going to draw the progress bar
		// this won't come from the user setting as a flag but how the
		// progress bar is constructed
		private enum ProgressBarType 
		{ 
			Standard, 
			Bitmap, 
			Gradient, 
			WaitingGradient,
			GradientTube,
			WaitingGradientTube
		}
		#endregion

		#region Events
		public event ProgressBarPropertyChangedHandler PropertyChanged;
		#endregion
		
		#region Class Variables
		
		// Standard progress bar
		Color backgroundColor;
		Color foregroundColor;
		Color borderColor;
		
		// Property backers
		int _value = 0;
		int step = 1;
		int min = 0;
		int max = 100;
		bool smooth = false;
		Border3DStyle border3D = Border3DStyle.Flat;
		bool enable3DBorder = false;
		
		// Text related
		bool showProgressText = false;
		string progressText = string.Empty;
		Color progressTextHiglightColor = Color.Empty;
		Color progressTextColor = Color.Empty;
		
		// Progress Bar type
		ProgressBarType barType = ProgressBarType.Standard;
		
		// Bitmap progress Bar
		Bitmap foregroundBitmap = null;
        Bitmap backgroundBitmap = null;
		
		// Gradient progress bar
		Color gradientStartColor = Color.Empty;
		Color gradientMiddleColor = Color.Empty;
		Color gradientEndColor = Color.Empty;

		// Waiting progress bar
		System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
		int waitingGradientSize = 30;
		int waitingSpeed = 25;
		int waitingStep = 5;
		int waitingPos = 0;
		const int TUBE_WIDTH = 20;
		int tubeOffset = 0;

		// Gradient tube helpers
		Color lightColor = Color.Empty;
		Color lighterColor = Color.Empty;
		Color lightestColor = Color.Empty;
		Color darkColor = Color.Empty;
		Color darkerColor = Color.Empty;

		// Vertical/Horizontal progress bar
		Orientation orientation = Orientation.Horizontal;

		#endregion
        
		#region Constructors
		// Default contructor to draw a "Standard" progress Bar
		public ProgressBarVS()
		{
			InitializeProgressControl(ProgressBarType.Standard, ColorUtil.VSNetControlColor, 
				ColorUtil.VSNetBorderColor, ColorUtil.VSNetBorderColor, null, null, Color.Empty, Color.Empty, Color.Empty);
		}

		public ProgressBarVS(Bitmap foregroundBitmap, Bitmap backgroundBitmap)
		{
			InitializeProgressControl(ProgressBarType.Bitmap, ColorUtil.VSNetControlColor, 
				ColorUtil.VSNetBorderColor, ColorUtil.VSNetBorderColor, 
				foregroundBitmap, backgroundBitmap, Color.Empty, Color.Empty, Color.Empty);
		}

		public ProgressBarVS(Bitmap foregroundBitmap)
		{
			InitializeProgressControl(ProgressBarType.Bitmap, ColorUtil.VSNetControlColor, 
				ColorUtil.VSNetBorderColor,ColorUtil.VSNetBorderColor, 
				foregroundBitmap, null, Color.Empty, Color.Empty, Color.Empty);
		}

		public ProgressBarVS(Color gradientStartColor)
		{
			InitializeProgressControl(ProgressBarType.GradientTube, ColorUtil.VSNetControlColor, 
				ColorUtil.VSNetBorderColor, ColorUtil.VSNetBorderColor, 
				foregroundBitmap, null, gradientStartColor, Color.Empty, Color.Empty);

			// Initialize helper colors
			InitializeGradientTubeColors(gradientStartColor);
			
   		}

		public ProgressBarVS(Color gradientStartColor, bool waiting)
		{
			
			ProgressBarType type = ProgressBarType.GradientTube;
			if ( waiting == true )
			{
				type = ProgressBarType.WaitingGradientTube;
				// Initialize timer callback
				timer.Tick += new EventHandler(OnWaitingTick);
				timer.Interval = waitingSpeed;
			}
			
			InitializeProgressControl(type, ColorUtil.VSNetControlColor, 
				ColorUtil.VSNetBorderColor, ColorUtil.VSNetBorderColor, 
				foregroundBitmap, null, gradientStartColor, Color.Empty, Color.Empty);
            
			// Initialize helper colors
			InitializeGradientTubeColors(gradientStartColor);
		}

		public ProgressBarVS(Color gradientStartColor, Color gradientEndColor)
		{
			InitializeProgressControl(ProgressBarType.Gradient, ColorUtil.VSNetControlColor, 
				ColorUtil.VSNetBorderColor, ColorUtil.VSNetBorderColor, 
				foregroundBitmap, null, gradientStartColor, Color.Empty, gradientEndColor);
		}

		public ProgressBarVS(Color gradientStartColor, Color gradientEndColor, bool waiting)
		{
			ProgressBarType type = ProgressBarType.Gradient;
			if ( waiting == true )
			{
				type = ProgressBarType.WaitingGradient;
				// Initialize timer callback
				timer.Tick += new EventHandler(OnWaitingTick);
				timer.Interval = waitingSpeed;

			}

			InitializeProgressControl(type, ColorUtil.VSNetControlColor, 
				ColorUtil.VSNetBorderColor, ColorUtil.VSNetBorderColor, 
				foregroundBitmap, null, gradientStartColor, Color.Empty, gradientEndColor);
		}

		public ProgressBarVS(Color gradientStartColor, Color gradientMiddleColor, Color gradientEndColor)
		{
			InitializeProgressControl(ProgressBarType.Gradient, ColorUtil.VSNetControlColor, 
				ColorUtil.VSNetBorderColor, ColorUtil.VSNetBorderColor, 
				foregroundBitmap, null, gradientStartColor, gradientMiddleColor, gradientEndColor);
		}

		void InitializeProgressControl(ProgressBarType barType, Color backgroundColor, 
			Color foregroundColor, Color borderColor, Bitmap foregroundBitmap, Bitmap backgroundBitmap, Color gradientStartColor,
			Color gradientMiddleColor, Color gradientEndColor)
		{
			// Setup Double buffering 
			SetStyle(ControlStyles.ResizeRedraw 
			|ControlStyles.AllPaintingInWmPaint|ControlStyles.UserPaint|ControlStyles.DoubleBuffer, true);
			
			this.barType = barType;
			this.backgroundColor = backgroundColor;
			this.foregroundColor = foregroundColor;
			this.borderColor = borderColor;
			this.foregroundBitmap = foregroundBitmap;
			this.backgroundBitmap = backgroundBitmap;
			this.gradientStartColor = gradientStartColor;
			this.gradientMiddleColor = gradientMiddleColor;
			this.gradientEndColor = gradientEndColor;
		}

		#endregion

		#region Overrides
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			
			// Get window area
			RECT rc = new RECT();
			WindowsAPI.GetWindowRect(Handle, ref rc);

			// Convert to a client size rectangle
			Rectangle rect = new Rectangle(0, 0, rc.right - rc.left, rc.bottom - rc.top);

			Graphics g = e.Graphics;
			Rectangle workRect = WorkRect;
			DrawBackground(g, workRect);
			DrawBorder(g, rect);
			
			if ( _value != 0 )
				DrawForeground(g, workRect);
					
		}
	
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			// Initialize waitingPos
			Rectangle rc = ClientRectangle;
			if ( orientation == Orientation.Horizontal )
                waitingPos = rc.Width/2;
			else
				waitingPos = rc.Height/2;
		}
		#endregion

		#region Properties
		public Color BackgroundColor
		{ 
			set 
			{ 
				if ( backgroundColor != value )
				{
					backgroundColor = value;
					FirePropertyChange(ProgressBarProperty.BackgroundColor);
				}
			}
			get { return backgroundColor; }
		}

		public Color ForegroundColor
		{ 
			set 
			{ 
				if ( foregroundColor != value )
				{
					foregroundColor = value;
					FirePropertyChange(ProgressBarProperty.ForegroundColor);
				}
			}
			get { return foregroundColor; }
		}

		public Color BorderColor
		{ 
			set 
			{ 
				if ( borderColor != value )
				{
					borderColor = value;
					FirePropertyChange(ProgressBarProperty.BorderColor);
				}
			}
			get { return borderColor; }
		}

		public int Value
		{ 
			set
			{ 
				if ( _value != value )
				{
					if ( !(value <= max && value >= min) )
					{
						// Throw exception to indicate out of range condition
						string message = "ProgressBarEx Value: " + value.ToString() 
							+ " is out of range. It needs to be between " +
							min.ToString() + " and " + max.ToString();
						ArgumentOutOfRangeException outRangeException = new ArgumentOutOfRangeException("Value", message);
						throw(outRangeException);
					}
					_value = value; 
					FirePropertyChange(ProgressBarProperty.Value);
				}
			}
			get { return _value; }
		}

		public new Size Size
		{ 
			set 
			{ 
				// Make sure width and height dimensions are always
				// an even number so that we can do round math
				//  when we draw the progress bar segments
				Size newSize = value;
				if ( newSize.Width % 2 != 0) newSize.Width++;
				if ( newSize.Height % 2 != 0) newSize.Height++;
				base.Size = newSize;
			}
			get { return base.Size; }
		}

		public int Step
		{ 
			set 
			{ 
				if ( step != value )
				{
					step = value;
					FirePropertyChange(ProgressBarProperty.Step);
				}
			}
			get { return step; }
		}

		public int Minimum
		{ 
			set 
			{ 
				if ( min != value )
				{
					if ( value >= max )
					{
						// Throw exception to indicate out of range condition
						string message = "ProgressBarEx Minimum Value: " 
							+ value.ToString() + " is out of range. It needs to be less than " +
							"Maximun value: " + max.ToString();	
						ArgumentOutOfRangeException outRangeException = new ArgumentOutOfRangeException("Value", message);
						throw(outRangeException);
					}
					min = value;
					FirePropertyChange(ProgressBarProperty.Minimun);
				}
			}
			get { return min; }
		}

		public int Maximum
		{ 
			set 
			{
				if ( max != value )
				{
					if ( value <= min )
					{
						// Throw exception to indicate out of range condition
						string message = "ProgressBarEx Maximum Value: " + value.ToString() 
							+ " is out of range. It needs to be greater than " +
							"Minimum value: " + min.ToString();	
						ArgumentOutOfRangeException outRangeException = new ArgumentOutOfRangeException("Value", message);
						throw(outRangeException);
					}
					max = value;
					FirePropertyChange(ProgressBarProperty.Maximun);
				}
			}
			get { return max; }
		}

		public bool Smooth
		{
			set 
			{
				if ( smooth != value )
				{
					smooth = value;
					FirePropertyChange(ProgressBarProperty.Smooth);
				}
			}
			get { return smooth; }
		}

		public Border3DStyle Border3D
		{
			set 
			{
				if ( border3D != value )
				{
					border3D = value;
					FirePropertyChange(ProgressBarProperty.Border3D);
				}
			}
			get { return border3D; }
		}

		public bool Enable3DBorder
		{
			set 
			{
				if ( enable3DBorder != value )
				{
					enable3DBorder = value;
					FirePropertyChange(ProgressBarProperty.Enable3DBorder);
				}
			}
			get { return enable3DBorder; }
		}

		public bool ShowProgressText
		{
			set 
			{
				if ( showProgressText != value )
				{
					showProgressText = value;
					FirePropertyChange(ProgressBarProperty.ShowProgressText);
				}
			}
			get { return showProgressText; }
		}

		public string ProgressText
		{
			set 
			{
				if ( progressText != value )
				{
					progressText = value;
					FirePropertyChange(ProgressBarProperty.ProgressText);
				}
			}
			get { return progressText; }
		}

		public Color ProgressTextHiglightColor
		{
			set 
			{
				if ( progressTextHiglightColor != value )
				{
					progressTextHiglightColor = value;
					FirePropertyChange(ProgressBarProperty.ProgressTextHiglightColor);
				}
			}
			get { return progressTextHiglightColor; }
		}

		public Color ProgressTextColor
		{
			set 
			{
				if ( progressTextColor != value )
				{
					progressTextColor = value;
					FirePropertyChange(ProgressBarProperty.ProgressTextColor);
				}
			}
			get { return progressTextColor; }
		}

		public Bitmap ForegroundBitmap
		{
			set 
			{
				if ( foregroundBitmap != value )
				{
					foregroundBitmap = value;
					FirePropertyChange(ProgressBarProperty.ForegroundBitmap);
				}
			}
			get { return foregroundBitmap; }
		}

		public Bitmap BackgroundBitmap
		{
			set 
			{
				if ( backgroundBitmap != value )
				{
					backgroundBitmap = value;
					FirePropertyChange(ProgressBarProperty.BackgroundBitmap);
				}
			}
			get { return backgroundBitmap; }
		}

		public Color GradientStartColor
		{
			set 
			{
				if ( gradientStartColor != value )
				{
					gradientStartColor = value;
					if ( barType == ProgressBarType.GradientTube || barType == ProgressBarType.WaitingGradientTube )
						InitializeGradientTubeColors(value);
					FirePropertyChange(ProgressBarProperty.GradientStartColor);
				}
			}
			get { return gradientStartColor; }
		}

		public Color GradientMiddleColor
		{
			set 
			{
				if ( gradientMiddleColor != value )
				{
					gradientMiddleColor = value;
					FirePropertyChange(ProgressBarProperty.GradientMiddleColor);
				}
			}
			get { return gradientMiddleColor; }
		}

		public Color GradientEndColor
		{
			set 
			{
				if ( gradientEndColor != value )
				{
					gradientEndColor = value;
					FirePropertyChange(ProgressBarProperty.GradientEndColor);
				}
			}
			get { return gradientEndColor; }
		}

		public int WaitingGradientSize
		{
			set 
			{
				if ( waitingGradientSize != value )
				{
					waitingGradientSize = value;
					// Keep the size between 0 and 100
					if ( waitingGradientSize < 0 )
						waitingGradientSize = 10;
					else if ( waitingGradientSize > 100 )
						waitingGradientSize = 80;
					
					FirePropertyChange(ProgressBarProperty.WaitingGradientSize);
				}
			}
			get { return waitingGradientSize; }
		}

		public int WaitingSpeed
		{
			set 
			{
				if ( waitingSpeed != value )
				{
					bool timerEnabled = timer.Enabled;
					if ( timerEnabled )
						timer.Stop();
					
					waitingSpeed = value;
					timer.Interval = value;
					FirePropertyChange(ProgressBarProperty.WaitingSpeed);
					
					if ( timerEnabled )
                        timer.Start();
				}
			}
			get { return waitingSpeed; }
		}

		public int WaitingStep
		{
			set 
			{
				if ( waitingStep != value )
				{
					waitingStep = value;
					FirePropertyChange(ProgressBarProperty.WaitingStep);
				}
			}
			get { return waitingStep; }
		}

		public Orientation Orientation
		{
			set 
			{
				if ( orientation != value )
				{
					orientation = value;
					FirePropertyChange(ProgressBarProperty.Orientation);
				}
			}
			get { return orientation; }

		}
		#endregion

		#region Methods
		public void PerformStep()
		{
			if ( _value < max )
				_value += step;
			if ( _value > max )
				_value = max;
			FirePropertyChange(ProgressBarProperty.Step);
		}

		public void StartWaiting()
		{
			// Only if we have a waiting progress bar
			if ( barType != ProgressBarType.WaitingGradient && barType !=  ProgressBarType.WaitingGradientTube )
				return;
            timer.Start();

		}

		public void StopWaiting()
		{
			// Only if we have a waiting progress bar
			if ( barType != ProgressBarType.WaitingGradient && barType !=  ProgressBarType.WaitingGradientTube )
				return;
			timer.Stop();

		}
		#endregion

		#region Implementation
		// Methods
		void FirePropertyChange(ProgressBarProperty property)
		{
			// Fire event if we need to
			if (PropertyChanged != null)
				PropertyChanged(this, property);
			// Force a repaint of the control
			Invalidate();
		}

		void DrawBorder(Graphics g, Rectangle windowRect)
		{
			if ( enable3DBorder == false )
			{
				using ( Pen p = new Pen(borderColor) )
				{
					g.DrawRectangle(p, windowRect.Left, windowRect.Top,
						windowRect.Width-1, windowRect.Height-1);
				}
			}
			else 
			{
				ControlPaint.DrawBorder3D(g, windowRect, border3D);
			}
		}

		void DrawBackground(Graphics g, Rectangle workRect)
		{
			if ( barType == ProgressBarType.Standard )
			{
				DrawStandardBackground(g, workRect);
			}
			else if ( barType == ProgressBarType.Bitmap ) 
			{
				DrawBitmapBackground(g, workRect);
			}
			else if ( barType == ProgressBarType.Gradient )
			{
				DrawGradientBackground(g, workRect);
			}
			else if ( barType == ProgressBarType.GradientTube || barType == ProgressBarType.WaitingGradientTube )
			{
				DrawGradientTubeBackground(g, workRect);
			}
		}

		void DrawStandardBackground(Graphics g, Rectangle workRect)
		{
			using ( Brush b = new SolidBrush(backgroundColor) )
			{
				g.FillRectangle(b, workRect);
			}
		}

		void DrawBitmapBackground(Graphics g, Rectangle workRect)
		{
			if (  backgroundBitmap != null )
			{
				// If we strech the bitmap most likely than not the bitmap
				// won't look good. I will draw the background bitmap just 
				// by sampling a portion of the bitmap equal to the segment width
				// -- if we were drawing segments --- and draw this over and over
				// without leaving gaps
				if ( orientation == Orientation.Horizontal )
				{
					int segmentWidth = (workRect.Height)*3/4;
					segmentWidth -= 2;
								
					Rectangle drawingRect = new Rectangle(workRect.Left, workRect.Top, segmentWidth, workRect.Height);
					for ( int i = 0; i < workRect.Width; i += segmentWidth) 
					{
						g.DrawImage(backgroundBitmap, drawingRect.Left + i, drawingRect.Top,
							segmentWidth, workRect.Height);
						// If last segment does not fit, just draw a portion of it
						if ( i + segmentWidth > workRect.Width )
							g.DrawImage(backgroundBitmap, drawingRect.Left + i + segmentWidth, drawingRect.Top,
								workRect.Width - (drawingRect.Left + i + segmentWidth), workRect.Height);
					}
				}
				else
				{
					int segmentHeight = (workRect.Width)*3/4;
					segmentHeight -= 2;
								
					Rectangle drawingRect = new Rectangle(workRect.Left, workRect.Top, workRect.Width, segmentHeight);
					for ( int i = 0; i < workRect.Height; i += segmentHeight) 
					{
						g.DrawImage(backgroundBitmap, drawingRect.Left, drawingRect.Top + i,
							workRect.Width, segmentHeight);
						// If last segment does not fit, just draw a portion of it
						if ( i + segmentHeight > workRect.Height )
							g.DrawImage(backgroundBitmap, drawingRect.Left, drawingRect.Top + i + segmentHeight, workRect.Width,
								workRect.Height - (drawingRect.Top + i + segmentHeight));
					}
				}
			}
			else 
			{
				g.FillRectangle(new SolidBrush(backgroundColor), workRect);
			}
		}

		void DrawGradientBackground(Graphics g, Rectangle workRect)
		{
			// Same as the standard background
			using ( Brush b = new SolidBrush(backgroundColor) )
			{
				g.FillRectangle(b, workRect);
			}
		}

		void DrawGradientTubeBackground(Graphics g, Rectangle workRect)
		{
			// Draw background using a lighter shade of the gradientStartColor
			using ( Brush b = new SolidBrush(lightestColor) )
			{
				g.FillRectangle(b, workRect);
			}
		}

		void DrawForeground(Graphics g, Rectangle workRect)
		{
			if ( barType == ProgressBarType.Standard )
			{
				DrawStandardForeground(g, workRect);
			}
			else if ( barType == ProgressBarType.Bitmap ) 
			{
				DrawBitmapForeground(g, workRect);
			}
			else if ( barType == ProgressBarType.Gradient )
			{
				DrawGradientForeground(g, workRect);
			}
			else if ( barType == ProgressBarType.WaitingGradient )
			{
				DrawWaitingGradientForeground(g, workRect);
			}
			else if ( barType == ProgressBarType.GradientTube )
			{
				DrawGradientTubeForeground(g, workRect);
			}
			else if ( barType == ProgressBarType.WaitingGradientTube )
			{
				DrawWaitingGradientTubeForeground(g, workRect);
			}

		}

		void DrawStandardForeground(Graphics g, Rectangle workRect)
		{
			if ( smooth )
				DrawStandardForegroundSmooth(g, workRect);
			else
				DrawStandardForegroundSegmented(g, workRect);

		}

		void DrawBitmapForeground(Graphics g, Rectangle workRect)
		{
		
			if ( orientation == Orientation.Horizontal )
			{
				// We should have a valid foreground bitmap if the type of
				// the progress bar is bitmap
				Debug.Assert(foregroundBitmap != null);
			
				// If we strech the bitmap most likely than not the bitmap
				// won't look good. I will draw the foreground bitmap just 
				// by sampling a portion of the bitmap equal to the segment width
				// -- if we were drawing segments --- and draw this over and over
				// without leaving gaps
				int segmentWidth = (workRect.Height)*3/4;
				segmentWidth -= 2;
				int segmentTopGap = 1;
				int segmentLeftGap = 2;
				if ( smooth )
					segmentLeftGap = 0;
                        
				Rectangle segmentRect = new Rectangle(workRect.Left, 
					workRect.Top, segmentWidth, workRect.Height);
			   			
				int progressWidth = (GetScaledValue());
				if ( progressWidth < 0 ) progressWidth = 0;
						
				for ( int i = 0; i < progressWidth; i += segmentRect.Width+segmentLeftGap )
				{
					if ( i+segmentRect.Width+segmentLeftGap > progressWidth 
						&& (i+segmentRect.Width+segmentLeftGap > WorkRect.Width) ) 
					{
						// if we are about to leave because next segment does not fit
						// draw the portion that fits
						int partialWidth = progressWidth-i;
						Rectangle drawingRect = new Rectangle(segmentRect.Left+i, 
							segmentRect.Top+segmentTopGap, partialWidth, segmentRect.Height-(segmentTopGap*2));
						g.DrawImage(foregroundBitmap, drawingRect, 0, 0, drawingRect.Width, drawingRect.Height, GraphicsUnit.Pixel);
						break;
					}
					Rectangle completeSegment = new Rectangle(segmentRect.Left+i, segmentRect.Top+segmentTopGap, 
						segmentRect.Width, segmentRect.Height-(segmentTopGap*2));
					g.DrawImage(foregroundBitmap, completeSegment, 0, 0, 
						completeSegment.Width, completeSegment.Height, GraphicsUnit.Pixel);
				}
			}
			else
			{
				// We should have a valid foreground bitmap if the type of
				// the progress bar is bitmap
				Debug.Assert(foregroundBitmap != null);
			
				// If we strech the bitmap most likely than not the bitmap
				// won't look good. I will draw the foreground bitmap just 
				// by sampling a portion of the bitmap equal to the segment width
				// -- if we were drawing segments --- and draw this over and over
				// without leaving gaps
				int segmentHeight = (workRect.Width)*3/4;
				segmentHeight -= 2;
				int segmentTopGap = 2;
				int segmentLeftGap = 1;
				if ( smooth )
					segmentTopGap = 0;
                        
				Rectangle segmentRect = new Rectangle(workRect.Left, 
					workRect.Top, workRect.Width, segmentHeight);
			   			
				int progressHeight = (GetScaledValue());
				if ( progressHeight < 0 ) progressHeight = 0;

				for ( int i = 0; i < progressHeight; i += segmentRect.Height+segmentTopGap )
				{
					int top = (workRect.Bottom - segmentRect.Height) - i;
					if ( top < workRect.Top ) 
					{
						// if we are about to leave because next segment does not fit
						// draw the portion that fits
						int partialHeight = progressHeight-i;
						Rectangle drawingRect = new Rectangle(segmentRect.Left+segmentLeftGap, 
							workRect.Top, segmentRect.Width-(segmentLeftGap*2), partialHeight);
						g.DrawImage(foregroundBitmap, drawingRect, 0, 0, drawingRect.Width, drawingRect.Height, GraphicsUnit.Pixel);
						break;
					}
					Rectangle completeSegment = new Rectangle(segmentRect.Left+segmentLeftGap, top, 
						segmentRect.Width-(segmentLeftGap*2), segmentRect.Height);
					g.DrawImage(foregroundBitmap, completeSegment, 0, 0, 
						completeSegment.Width, completeSegment.Height, GraphicsUnit.Pixel);
				}

			}
		}

        void DrawGradientForeground(Graphics g, Rectangle workRect)
		{
			// Three color gradient?
			bool useMiddleColor = false;
			if ( gradientMiddleColor != Color.Empty )
				useMiddleColor = true;

			if ( useMiddleColor )
				DrawThreeColorsGradient(g, workRect);
			else
				DrawTwoColorsGradient(g, workRect);
		}

		void DrawWaitingGradientForeground(Graphics g, Rectangle workRect)
		{
			if ( orientation == Orientation.Horizontal )
				DrawWaitingGradientForegroundHorizontal(g, workRect);
			else
				DrawWaitingGradientForegroundVertical(g, workRect);
		}

		void DrawWaitingGradientForegroundHorizontal(Graphics g, Rectangle workRect)
		{
			Rectangle rc = workRect;
			Region oldRegion = g.Clip;
			g.Clip = new Region(rc);

			// Boolean helpers
			bool useLeftBrush = false;
			bool useRightBrush = false;

			// Initialize rectangles
			Rectangle left = rc;
			Rectangle right = rc;
			Rectangle leftFill = rc;
			Rectangle rightFill = rc;
			Rectangle partialLeft = rc;
			Rectangle partialRight = rc;

			int gradientSize = (int)((float)(waitingGradientSize/2)*((float)rc.Width/100));
            
			// Adjust rectangles using waiting bar type settings
			left.Width = gradientSize;
			left.X = waitingPos - gradientSize;
			right.X = waitingPos;
			right.Width = gradientSize;
			leftFill.Width = waitingPos - gradientSize;
			rightFill.X = waitingPos + gradientSize;
			rightFill.Width = ClientRectangle.Width - (waitingPos + gradientSize);
			
			if ( (waitingPos + gradientSize) > rc.Width )
			{
				useRightBrush = true;
				partialRight.X = -(rc.Width - waitingPos + rc.Left);
				partialRight.Width = gradientSize;
				leftFill.X = partialRight.X + gradientSize;
				leftFill.Width -= (partialRight.X + gradientSize);
			}

			if ( waitingPos < gradientSize )
			{
				useLeftBrush = true;
				partialLeft.X = (rc.Width - gradientSize) + waitingPos;
				partialLeft.Width = gradientSize;
				rightFill.X = right.X + gradientSize;
				rightFill.Width = partialLeft.X - rightFill.X;
			}

			// Paint partial right rectangle
			if ( useRightBrush )
			{
				using ( Brush b = new LinearGradientBrush(partialRight, gradientEndColor, gradientStartColor, 0, true) )
				{
					g.FillRectangle(b, partialRight);
					// There is a bug in the LinearGradientBrush logic
					// that causes the first line in the gradient to use the EndColor of the gradient 
					// instead of using the StartColor of the gradient
					// To compensate, draw a one pixel wide line using the start color
					using ( Pen p = new Pen(gradientEndColor) )
					{
						g.DrawLine(p, partialRight.Left, partialRight.Top, partialRight.Left, partialRight.Bottom);
					}
				}
			}

			// Partial left rectangle
			if ( useLeftBrush )
			{
				using ( Brush b = new LinearGradientBrush(partialLeft, gradientStartColor, gradientEndColor, 0, true) )
				{
					g.FillRectangle(b, partialLeft);
					// Same bug fix as above
					using ( Pen p = new Pen(gradientStartColor) )
					{
						g.DrawLine(p, partialLeft.Left, partialLeft.Top, partialLeft.Left, partialLeft.Bottom);
					}
				}
			}

			// Paint left fill rectangle
			using ( Brush b = new SolidBrush(gradientStartColor) )
			{
				g.FillRectangle(b, leftFill);
			}

			// Paint left rectangle
			if ( left.Width > 0 )
			{
				using ( Brush b = new LinearGradientBrush(left, gradientStartColor, gradientEndColor, LinearGradientMode.Horizontal) )
				{
					g.FillRectangle(b, left);
					// Same bug fix as above
					using ( Pen p = new Pen(gradientStartColor) )
					{
						g.DrawLine(p, left.Left, left.Top, left.Left, left.Bottom);
					}
				}
			}
		
			// Paint right rectangle
			if ( right.Width > 0 )
			{
				using ( Brush b = new LinearGradientBrush(right, gradientEndColor, gradientStartColor, 0, true) )
				{
					g.FillRectangle(b, right);
					// Same bug fix as above
					using ( Pen p = new Pen(gradientEndColor) )
					{
						g.DrawLine(p, right.Left, right.Top, right.Left, right.Bottom);
					}
				}
			}
	
			// Paint right fill rectangle
			using ( Brush b = new SolidBrush(gradientStartColor) )
			{
				g.FillRectangle(b, rightFill);
			}

			// Put back old region
			g.Clip = oldRegion;

		}
				
		void DrawWaitingGradientForegroundVertical(Graphics g, Rectangle workRect)
		{
			Rectangle rc = workRect;
			Region oldRegion = g.Clip;
			g.Clip = new Region(rc);

			// Boolean helpers
			bool useLeftBrush = false;
			bool useRightBrush = false;

			// Initialize rectangles
			Rectangle left = rc;
			Rectangle right = rc;
			Rectangle leftFill = rc;
			Rectangle rightFill = rc;
			Rectangle partialLeft = rc;
			Rectangle partialRight = rc;

			int gradientSize = (int)((float)(waitingGradientSize/2)*((float)rc.Height/100));
            
			// Adjust rectangles using waiting bar type settings
			left.Height = gradientSize;
			left.Y = waitingPos;
			right.Y = waitingPos - gradientSize;
			right.Height = gradientSize;
			leftFill.Y = waitingPos + gradientSize;
			leftFill.Height = rc.Bottom - (waitingPos + gradientSize);
			rightFill.Y = rc.Top;
			rightFill.Height = right.Y - rc.Top;
						
			if ( (waitingPos - gradientSize) < rc.Top )
			{
				useRightBrush = true;
				partialRight.Y = rc.Bottom - (gradientSize - waitingPos);
				partialRight.Height = gradientSize;
				leftFill.Height -= (gradientSize - waitingPos);
			}

			if ( waitingPos + gradientSize > rc.Bottom )
			{
				useLeftBrush = true;
				partialLeft.Y = rc.Top - (rc.Bottom - waitingPos);
				partialLeft.Height = gradientSize;
				rightFill.Height -=  gradientSize - (rc.Bottom - waitingPos);
				rightFill.Y = right.Y - rightFill.Height;
			}

			// Paint partial right rectangle
			if ( useRightBrush )
			{
				using ( Brush b = new LinearGradientBrush(partialRight, gradientStartColor, gradientEndColor, LinearGradientMode.Vertical) )
				{
					g.FillRectangle(b, partialRight);
					// To fix bug
					using ( Pen p = new Pen(gradientStartColor) )
					{
						g.DrawLine(p, partialRight.Left, partialRight.Top, partialRight.Right, partialRight.Top);
					}
				}
			}

			// Partial left rectangle
			if ( useLeftBrush )
			{
				using ( Brush b = new LinearGradientBrush(partialLeft, gradientEndColor, gradientStartColor, LinearGradientMode.Vertical) )
				{
					g.FillRectangle(b, partialLeft);
					// To fix bug
					using ( Pen p = new Pen(gradientEndColor) )
					{
						g.DrawLine(p, partialLeft.Left, partialLeft.Top, partialLeft.Right, partialLeft.Top);
					}
				}
			}

			// Paint left fill rectangle
			using ( Brush b = new SolidBrush(gradientStartColor) )
			{
				g.FillRectangle(b, leftFill);
			}

			// Paint left rectangle
			if ( left.Width > 0 )
			{
				using ( Brush b = new LinearGradientBrush(left, gradientEndColor, gradientStartColor, LinearGradientMode.Vertical) )
				{
					g.FillRectangle(b, left);
				}
				// To fix bug
				using ( Pen p = new Pen(gradientEndColor) )
				{
					g.DrawLine(p, left.Left, left.Top, left.Right, left.Top);
				}
			}
		
			// Paint right rectangle
			if ( right.Width > 0 )
			{
				using ( Brush b = new LinearGradientBrush(right, gradientStartColor, gradientEndColor, LinearGradientMode.Vertical) )
				{
					g.FillRectangle(b, right);
				}
				// To fix bug
				using ( Pen p = new Pen(gradientStartColor) )
				{
					g.DrawLine(p, right.Left, right.Top, right.Right, right.Top);
				}
			}
	
			// Paint right fill rectangle
			using ( Brush b = new SolidBrush(gradientStartColor) )
			{
				g.FillRectangle(b, rightFill);
			}

			// Put back old region
			g.Clip = oldRegion;
			
		}

		void DrawGradientTubeForeground(Graphics g, Rectangle workRect)
		{
			Pen colorPen = new Pen(gradientStartColor);
			Pen lightPen = new Pen(lightColor);
			Pen lighterPen = new Pen(lighterColor);
			Pen darkPen = new Pen(darkColor);
			Pen darkerPen = new Pen(darkerColor);

			int _value = GetScaledValue();
			Rectangle rc = Rectangle.Empty;
			
			
			if ( orientation == Orientation.Horizontal )
			{
				rc = new Rectangle(workRect.Left, workRect.Top, _value, workRect.Height);

				// Set pixels
				IntPtr hDC = g.GetHdc();

				WindowsAPI.SetPixel(hDC, rc.Left+1, rc.Top+1, 
					ColorUtil.RGB(lightColor.R, lightColor.G, lightColor.B));
				WindowsAPI.SetPixel(hDC, rc.Left+1, rc.Bottom-3, 
					ColorUtil.RGB(lightColor.R, lightColor.G, lightColor.B));

				WindowsAPI.SetPixel(hDC, rc.Left+1, rc.Top+2, 
					ColorUtil.RGB(lighterColor.R, lighterColor.G, lighterColor.B));
				WindowsAPI.SetPixel(hDC, rc.Left+1, rc.Bottom-4, 
					ColorUtil.RGB(lighterColor.R, lighterColor.G, lighterColor.B));

				WindowsAPI.SetPixel(hDC, rc.Left+1, rc.Top, 
					ColorUtil.RGB(gradientStartColor.R, gradientStartColor.G, gradientStartColor.B));
				WindowsAPI.SetPixel(hDC, rc.Left+1, rc.Bottom-2, 
					ColorUtil.RGB(gradientStartColor.R, gradientStartColor.G, gradientStartColor.B));
		
				WindowsAPI.SetPixel(hDC, rc.Left+1, rc.Bottom-1, 
					ColorUtil.RGB(darkColor.R, darkColor.G, darkColor.B));
            		
				g.ReleaseHdc(hDC);
			
				// Draw regular color lines
				g.DrawLine(lighterPen, rc.Left+2, rc.Top+3, rc.Right-3, rc.Top+3);
				g.DrawLine(lighterPen, rc.Left+2, rc.Bottom-5, rc.Right-3, rc.Bottom-5);
				g.DrawLine(lighterPen, rc.Right-3, rc.Top+3, rc.Right-3, rc.Bottom-5);
			
				g.DrawLine(lightPen, rc.Left+2, rc.Top+2, rc.Right-3, rc.Top+2);
				g.DrawLine(lightPen, rc.Left+2, rc.Bottom-4, rc.Right-3, rc.Bottom-4);
				g.DrawLine(lightPen, rc.Left, rc.Top, rc.Left, rc.Bottom-1);

				g.DrawLine(colorPen, rc.Left+2, rc.Top+1, rc.Right-2, rc.Top+1);
				g.DrawLine(colorPen, rc.Left+2, rc.Bottom-3, rc.Right-3, rc.Bottom-3);

				g.DrawLine(darkPen, rc.Left+2, rc.Bottom-2, rc.Right-3, rc.Bottom-2);
				g.DrawLine(darkPen, rc.Right-2, rc.Top+2, rc.Right-2, rc.Bottom-2);
				g.DrawLine(darkPen, rc.Left+2, rc.Top, rc.Right-1, rc.Top);

				g.DrawLine(darkerPen, rc.Left+2, rc.Bottom-1, rc.Right-2, rc.Bottom-1);
				g.DrawLine(darkerPen, rc.Right-1, rc.Top+1, rc.Right-1, rc.Bottom-1);
			}
			else
			{
				int top = workRect.Top + workRect.Height - _value;
				rc = new Rectangle(workRect.Left, top, workRect.Width, _value);

				// Set pixels
				IntPtr hDC = g.GetHdc();

				WindowsAPI.SetPixel(hDC, rc.Left, rc.Top+1, 
					ColorUtil.RGB(gradientStartColor.R, 
					gradientStartColor.G, gradientStartColor.B));

				WindowsAPI.SetPixel(hDC, rc.Right-2, rc.Top+1, 
					ColorUtil.RGB(gradientStartColor.R, 
					gradientStartColor.G, gradientStartColor.B));

				WindowsAPI.SetPixel(hDC, rc.Left+1, rc.Top+1, 
					ColorUtil.RGB(lightColor.R, 
					lightColor.G, lightColor.B));

				WindowsAPI.SetPixel(hDC, rc.Right-3, rc.Top+1, 
					ColorUtil.RGB(lightColor.R, 
					lightColor.G, lightColor.B));

				WindowsAPI.SetPixel(hDC, rc.Left+2, rc.Top+1, 
					ColorUtil.RGB(lighterColor.R, 
					lighterColor.G, lighterColor.B));

				WindowsAPI.SetPixel(hDC, rc.Right-4, rc.Top+1, 
					ColorUtil.RGB(lighterColor.R, 
					lighterColor.G, lighterColor.B));

				WindowsAPI.SetPixel(hDC, rc.Right-1, rc.Top+1, 
					ColorUtil.RGB(darkColor.R, 
					darkColor.G, darkColor.B));
				
				g.ReleaseHdc(hDC);

				g.DrawLine(colorPen, rc.Left, top, rc.Right-1, top);
				g.DrawLine(colorPen, rc.Left+1, top+2, rc.Left+1, rc.Bottom-2);
				g.DrawLine(colorPen, rc.Right-3, top+2, rc.Right-3, rc.Bottom-3);
				g.DrawLine(lightPen, rc.Left+2, top+2, rc.Left+2, rc.Bottom-3);
				g.DrawLine(lightPen, rc.Right-4, top+2, rc.Right-4, rc.Bottom-3);
				g.DrawLine(lighterPen, rc.Left+3, top+2, rc.Left+3, rc.Bottom-3);
				g.DrawLine(lighterPen, rc.Right-5, top+2, rc.Right-5, rc.Bottom-3);
				g.DrawLine(darkPen, rc.Left, top+2, rc.Left, rc.Bottom-1);
				g.DrawLine(darkPen, rc.Left+2, rc.Bottom-2, rc.Right-3, rc.Bottom-2);
				g.DrawLine(darkPen, rc.Right-2, top+2, rc.Right-2, rc.Bottom-2);
				g.DrawLine(darkerPen, rc.Left+1, rc.Bottom-1, rc.Right-1, rc.Bottom-1);
				g.DrawLine(darkerPen, rc.Right-1, top+2, rc.Right-1, rc.Bottom-1);
				
			}
																			
			// Cleanup
			colorPen.Dispose();	
			lightPen.Dispose();
			lighterPen.Dispose();
			darkPen.Dispose();
			darkerPen.Dispose();

		}
				
		void DrawWaitingGradientTubeForeground(Graphics g, Rectangle workRect)
		{
			
			Rectangle rc = workRect;
			Pen colorPen = new Pen(gradientStartColor);
			Pen lightPen = new Pen(lightColor);
			Pen lighterPen = new Pen(lighterColor);
			Pen darkPen = new Pen(darkColor);
			Pen darkerPen = new Pen(darkerColor);

			Region oldRegion = g.Clip;
			g.Clip = new Region(rc);

			if ( orientation == Orientation.Horizontal )
			{
				// Prepare helper settings
				int numOfBands = (rc.Width/TUBE_WIDTH) + 2;
				int adjust = rc.Left - TUBE_WIDTH + tubeOffset;
				int height = rc.Height;
				int bottom = rc.Bottom - 1;
				int xPos = 0;
			
				for ( int i = 0; i < numOfBands; i++ )
				{
					xPos = adjust + (i*TUBE_WIDTH);
            						
					// Draw regular color lines
					g.DrawLine(darkerPen, xPos+1, rc.Top, xPos + height, bottom);
					g.DrawLine(darkPen, xPos+2, rc.Top, xPos + height + 1, bottom);
					g.DrawLine(darkPen, xPos+10, rc.Top, xPos + height + 9, bottom);
					g.DrawLine(colorPen, xPos+3, rc.Top, xPos + height + 2, bottom);
					g.DrawLine(colorPen, xPos+9, rc.Top, xPos + height + 8, bottom);
					g.DrawLine(lightPen, xPos+4, rc.Top, xPos + height + 3, bottom);
					g.DrawLine(lightPen, xPos+8, rc.Top, xPos + height + 7, bottom);
					g.DrawLine(lighterPen, xPos+5, rc.Top, xPos + height + 4, bottom);
					g.DrawLine(lighterPen, xPos+7, rc.Top, xPos + height + 6, bottom);
				}
			}
			else
			{
				// Prepare helper settings
				int numOfBands = (rc.Height/TUBE_WIDTH) + 2;
				int adjust = rc.Bottom - TUBE_WIDTH + tubeOffset;
				int height = rc.Width + 1;
				int xPos1 = rc.Left;
				int xPos2 = rc.Right + 1;
				int yPos = rc.Top + 1;
			
				for ( int i = 0; i < numOfBands; i++ )
				{
					yPos = adjust - (i*TUBE_WIDTH);
            						
					// Draw regular color lines
					g.DrawLine(darkerPen, xPos1, yPos, xPos2, yPos + height);
					g.DrawLine(darkPen, xPos1, yPos+1, xPos2, yPos + height+1);
					g.DrawLine(darkPen, xPos1, yPos+9, xPos2, yPos+height+9);
					g.DrawLine(colorPen, xPos1, yPos+2, xPos2, yPos+height+2);
					g.DrawLine(colorPen, xPos1, yPos+8, xPos2, yPos+height+8);
					g.DrawLine(lightPen, xPos1, yPos+3, xPos2, yPos+height+3);
					g.DrawLine(lightPen, xPos1, yPos+7, xPos2, yPos+height+7);
					g.DrawLine(lighterPen, xPos1, yPos+4, xPos2, yPos+height+4);
					g.DrawLine(lighterPen, xPos1, yPos+6, xPos2, yPos+height+6);
				}
			}
																			
			// Cleanup
			colorPen.Dispose();	
			lightPen.Dispose();
			lighterPen.Dispose();
			darkPen.Dispose();
			darkerPen.Dispose();
			g.Clip = oldRegion;

		}
				
		void DrawTwoColorsGradient(Graphics g, Rectangle workRect)
		{
			// Calculate color distance
			int redStep = Math.Max(gradientEndColor.R, gradientStartColor.R) 
				- Math.Min(gradientEndColor.R, gradientStartColor.R);
			int greenStep = Math.Max(gradientEndColor.G, gradientStartColor.G) 
				- Math.Min(gradientEndColor.G, gradientStartColor.G);
			int blueStep = Math.Max(gradientEndColor.B, gradientStartColor.B) 
				- Math.Min(gradientEndColor.B, gradientStartColor.B);

			// Do we need to increase or decrease
			int redDirection; 
			if ( gradientEndColor.R > gradientStartColor.R ) 
				redDirection = 1;
			else
				redDirection = -1;

			int greenDirection;
			if (  gradientEndColor.G >  gradientStartColor.G )
				greenDirection = 1;
			else
				greenDirection = -1;

			int blueDirection;
			if ( gradientEndColor.B > gradientStartColor.B )
				blueDirection = 1;
			else
				blueDirection = -1;
						
			// how many segements we need to draw
			int topGap = 1;
			int leftGap = 2;
			if ( smooth ) 
			{
				leftGap = 0;
				topGap = 0;
			}

			if ( orientation == Orientation.Horizontal )
			{
				// The progress control won't allow its height to be anything other than
				// and even number since the width of the segment needs to be a perfect 3/4
				// of the control (height - 4) -- Four pixels are padding --
				int segmentWidth = (workRect.Height)*3/4;
				segmentWidth -= 2;

				int numOfSegments = (workRect.Width)/(segmentWidth + leftGap);
				// No point to continue if we don't have enough are for even one segment
				if ( numOfSegments == 0 )
					return;
			
				// calculate the actual RGB steps for every segment
				redStep /= numOfSegments;
				greenStep /= numOfSegments;
				blueStep /= numOfSegments;

				Rectangle segmentRect = new Rectangle(workRect.Left, 
					workRect.Top, segmentWidth, workRect.Height);
			   			
				int progressWidth = (GetScaledValue());
				if ( progressWidth < 0 ) progressWidth = 0;
				int counter = 0;
				for ( int i = 0; i < progressWidth; i += segmentRect.Width+leftGap )
				{
					// Check we stay within bounds
					int red = gradientStartColor.R+(redStep*counter*redDirection);
					int green = gradientStartColor.G+(greenStep*counter*greenDirection);
					int blue = gradientStartColor.B+(blueStep*counter*blueDirection);
					// Make gettting the current color safe
					CheckBounds(ref red, ref green, ref blue);
					Color currentColor = Color.FromArgb(red, green, blue);
					
					if ( i+segmentRect.Width+leftGap > progressWidth 
						&& (i+segmentRect.Width+leftGap > workRect.Width-leftGap) ) 
					{
						// if we are about to leave because next segment does not fit
						// draw the portion that fits
						int partialWidth = progressWidth-i;
						Rectangle drawingRect = new Rectangle(segmentRect.Left+i, 
							segmentRect.Top+topGap, partialWidth, segmentRect.Height-(topGap*2));
						g.FillRectangle(new SolidBrush(currentColor), drawingRect);
						break;
					}
					Rectangle completeSegment = new Rectangle(segmentRect.Left+i, 
						segmentRect.Top+topGap, segmentRect.Width, segmentRect.Height-(topGap*2));
					g.FillRectangle(new SolidBrush(currentColor), completeSegment);
					counter++;
				}
			}
			else
			{
				topGap = 2;
				leftGap = 1;
				if ( smooth ) 
				{
					leftGap = 0;
					topGap = 0;
				}

				// The progress control won't allow its height to be anything other than
				// and even number 
				int segmentHeight = (workRect.Width)*3/4;
				segmentHeight -= 2;

				int numOfSegments = (workRect.Height)/(segmentHeight + topGap);
				// No point to continue if we don't have enough are for even one segment
				if ( numOfSegments == 0 )
					return;
			
				// calculate the actual RGB steps for every segment
				redStep /= numOfSegments;
				greenStep /= numOfSegments;
				blueStep /= numOfSegments;

				Rectangle segmentRect = new Rectangle(workRect.Left, 
					workRect.Top, workRect.Width, segmentHeight);
			   			
				int progressHeight = GetScaledValue();
				if ( progressHeight < 0 ) progressHeight = 0;
				int counter = 0;
				for ( int i = 0; i < progressHeight; i += segmentRect.Height+topGap )
				{
					// Check we stay within bounds
					int red = gradientStartColor.R+(redStep*counter*redDirection);
					int green = gradientStartColor.G+(greenStep*counter*greenDirection);
					int blue = gradientStartColor.B+(blueStep*counter*blueDirection);
					// Make gettting the current color safe
					CheckBounds(ref red, ref green, ref blue);
					Color currentColor = Color.FromArgb(red, green, blue);
					
					int top = (workRect.Bottom - segmentRect.Height) - i;
					if ( top < workRect.Top ) 
					{
						// if we are about to leave because next segment does not fit
						// draw the portion that fits
						int partialHeight = progressHeight-i;
						Rectangle drawingRect = new Rectangle(segmentRect.Left+leftGap, 
							workRect.Top, segmentRect.Width-(leftGap*2), partialHeight);
						g.FillRectangle(new SolidBrush(currentColor), drawingRect);
						break;
					}
					
					Rectangle completeSegment = new Rectangle(segmentRect.Left+leftGap, top, 
						segmentRect.Width-(leftGap*2), segmentRect.Height);
					g.FillRectangle(new SolidBrush(currentColor), completeSegment);
					counter++;
				}
			}

		}

		void DrawThreeColorsGradient(Graphics g, Rectangle workRect)
		{
			// Calculate color distance for the first half
			int redStepFirst = Math.Max(gradientStartColor.R, gradientMiddleColor.R) 
				- Math.Min(gradientStartColor.R, gradientMiddleColor.R);
			int greenStepFirst = Math.Max(gradientStartColor.G, gradientMiddleColor.G) 
				- Math.Min(gradientStartColor.G, gradientMiddleColor.G);
			int blueStepFirst = Math.Max(gradientStartColor.B, gradientMiddleColor.B) 
				- Math.Min(gradientStartColor.B, gradientMiddleColor.B);
   
			// Calculate color distance for the second half
			int redStepSecond = Math.Max(gradientEndColor.R, gradientMiddleColor.R) 
				- Math.Min(gradientEndColor.R, gradientMiddleColor.R);
			int greenStepSecond = Math.Max(gradientEndColor.G, gradientMiddleColor.G) 
				- Math.Min(gradientEndColor.G, gradientMiddleColor.G);
			int blueStepSecond = Math.Max(gradientEndColor.B, gradientMiddleColor.B) 
				- Math.Min(gradientEndColor.B, gradientMiddleColor.B);
			
			// Do we need to increase or decrease for the first half
			int redDirectionFirst; 
			if ( gradientStartColor.R < gradientMiddleColor.R ) 
				redDirectionFirst = 1;
			else
				redDirectionFirst = -1;

			int greenDirectionFirst;
			if (  gradientStartColor.G <  gradientMiddleColor.G )
				greenDirectionFirst = 1;
			else
				greenDirectionFirst = -1;

			int blueDirectionFirst;
			if ( gradientStartColor.B < gradientMiddleColor.B )
				blueDirectionFirst = 1;
			else
				blueDirectionFirst = -1;

			// Do we need to increase or decrease for the second half
			int redDirectionSecond; 
			if ( gradientMiddleColor.R < gradientEndColor.R ) 
				redDirectionSecond = 1;
			else
				redDirectionSecond = -1;

			int greenDirectionSecond;
			if (  gradientMiddleColor.G <  gradientEndColor.G )
				greenDirectionSecond = 1;
			else
				greenDirectionSecond = -1;

			int blueDirectionSecond;
			if ( gradientMiddleColor.B < gradientEndColor.B )
				blueDirectionSecond = 1;
			else
				blueDirectionSecond = -1;

			// how many segements we need to draw
			int topGap = 1;
			int leftGap = 2;
			if ( smooth ) 
			{
				topGap = 0;
				leftGap = 0;
			}

			if ( orientation == Orientation.Horizontal )
			{

				// The progress control won't allow its height to be anything other than
				// and even number since the width of the segment needs to be a perfect 3/4
				// of the control (height - 4) -- Four pixels are padding --
				int segmentWidth = (workRect.Height)*3/4;
				segmentWidth -= 2;
				
				int numOfSegments = (workRect.Width)/(segmentWidth + leftGap);
				
				// calculate the actual RGB step for every segment
				int segments = (numOfSegments/2);
				// Be safe
				if ( segments == 0 )
					segments = 1;

				redStepFirst /= segments;
				greenStepFirst /= segments;;
				blueStepFirst /= segments;
				redStepSecond /= segments;
				greenStepSecond /= segments;
				blueStepSecond /= segments;;

				Rectangle segmentRect = new Rectangle(workRect.Left, 
					workRect.Top, segmentWidth, workRect.Height);
				   			
				int progressWidth = GetScaledValue();
				if ( progressWidth < 0 ) progressWidth = 0;
				int counter = 0;
				bool counterReset = true;
				int red;
				int green;
				int blue;

				for ( int i = 0; i < progressWidth; i += segmentRect.Width+leftGap )
				{
					Color currentColor = Color.Empty;
					if ( i < (workRect.Width)/2 )
					{
						
						red  = gradientStartColor.R+(redStepFirst*counter*redDirectionFirst);
						green = gradientStartColor.G+(greenStepFirst*counter*greenDirectionFirst);
						blue = gradientStartColor.B+(blueStepFirst*counter*blueDirectionFirst);
						// Check we stay within bounds
						CheckBounds(ref red, ref green, ref blue);
						currentColor = Color.FromArgb(red, green, blue);
					}
					else
					{
						if ( counterReset )
						{
							counterReset = false;
							counter = 0;
						}

						red = gradientMiddleColor.R+(redStepSecond*counter*redDirectionSecond);
						green = gradientMiddleColor.G+(greenStepSecond*counter*greenDirectionSecond);
						blue = gradientMiddleColor.B+(blueStepSecond*counter*blueDirectionSecond);
						// Check we stay within bounds
						CheckBounds(ref red, ref green, ref blue);
						currentColor = Color.FromArgb(red,green, blue); 
					}
									
					if ( i+segmentRect.Width+leftGap > progressWidth 
						&& (i+segmentRect.Width+leftGap > workRect.Width-leftGap) ) 
					{
						// if we are about to leave because next segment does not fit
						// draw the portion that fits
						int partialWidth = progressWidth-i;
						Rectangle drawingRect = new Rectangle(segmentRect.Left+i, 
							segmentRect.Top+topGap, partialWidth, segmentRect.Height-(topGap*2));
						g.FillRectangle(new SolidBrush(currentColor), drawingRect);
						break;
					}
					Rectangle completeSegment = new Rectangle(segmentRect.Left+i, 
						segmentRect.Top+topGap, segmentRect.Width, segmentRect.Height-(topGap*2));
					g.FillRectangle(new SolidBrush(currentColor), completeSegment);
					counter++;
				}
			}
			else
			{
				topGap = 2;
				leftGap = 1;
				if ( smooth ) 
				{
					topGap = 0;
					leftGap = 0;
				}

				// The progress control won't allow its height to be anything other than
				// and even number since the width of the segment needs to be a perfect 3/4
				// of the control (height - 4) -- Four pixels are padding --
				int segmentHeight = (workRect.Width)*3/4;
				segmentHeight -= 2;
				
				int numOfSegments = (workRect.Height)/(segmentHeight + topGap);
				
				// calculate the actual RGB step for every segment
				int segments = (numOfSegments/2);
				// Be safe
				if ( segments == 0 )
					segments = 1;

				redStepFirst /= segments;
				greenStepFirst /= segments;;
				blueStepFirst /= segments;
				redStepSecond /= segments;
				greenStepSecond /= segments;
				blueStepSecond /= segments;;

				Rectangle segmentRect = new Rectangle(workRect.Left, 
					workRect.Top, workRect.Width, segmentHeight);
				   			
				int progressHeight = GetScaledValue();
				if ( progressHeight < 0 ) progressHeight = 0;
				int counter = 0;
				bool counterReset = true;
				int red;
				int green;
				int blue;

				for ( int i = 0; i < progressHeight; i += segmentRect.Height+topGap )
				{
					Color currentColor = Color.Empty;
					if ( i < (workRect.Height)/2 )
					{
						
						red  = gradientStartColor.R+(redStepFirst*counter*redDirectionFirst);
						green = gradientStartColor.G+(greenStepFirst*counter*greenDirectionFirst);
						blue = gradientStartColor.B+(blueStepFirst*counter*blueDirectionFirst);
						// Check we stay within bounds
						CheckBounds(ref red, ref green, ref blue);
						currentColor = Color.FromArgb(red, green, blue);
					}
					else
					{
						if ( counterReset )
						{
							counterReset = false;
							counter = 0;
						}

						red = gradientMiddleColor.R+(redStepSecond*counter*redDirectionSecond);
						green = gradientMiddleColor.G+(greenStepSecond*counter*greenDirectionSecond);
						blue = gradientMiddleColor.B+(blueStepSecond*counter*blueDirectionSecond);
						// Check we stay within bounds
						CheckBounds(ref red, ref green, ref blue);
						currentColor = Color.FromArgb(red,green, blue); 
					}

					int top = (workRect.Bottom - segmentRect.Height) - i;
					if ( top < workRect.Top ) 
					{
						// if we are about to leave because next segment does not fit
						// draw the portion that fits
						int partialHeight = progressHeight-i;
						Rectangle drawingRect = new Rectangle(segmentRect.Left+leftGap, 
							workRect.Top, segmentRect.Width-(leftGap*2), partialHeight);
						g.FillRectangle(new SolidBrush(currentColor), drawingRect);
						break;
					}
					
					Rectangle completeSegment = new Rectangle(segmentRect.Left+leftGap, top, 
						segmentRect.Width-(leftGap*2), segmentRect.Height);
					g.FillRectangle(new SolidBrush(currentColor), completeSegment);
					counter++;
				}
			}

		}

		void CheckBounds(ref int red, ref int green, ref int blue)
		{
			// Make sure we stay in bounds
			if ( red < 0 ) red = 0;
			else if ( red > 255 ) red = 255;
			
			if ( green < 0 ) green = 0;
			else if ( green > 255 ) green = 255;
			
			if ( blue < 0 ) blue = 0;
			else if ( blue > 255 ) blue = 255;
		}

		void DrawStandardForegroundSegmented(Graphics g, Rectangle workRect)
		{
			if ( orientation == Orientation.Horizontal )
			{
				// The progress control won't allow its height to be anything other than
				// and even number since the width of the segment needs to be a perfect 3/4
				// of the control (height - 4) -- Four pixels are padding for the border--
				int segmentWidth = (workRect.Height)*3/4;
				int segmentTopGap = 1;
				int segmentLeftGap = 2;
						
				// One pixel gap of the sides
				segmentWidth -= 2;
                        
				Rectangle segmentRect = new Rectangle(workRect.Left, 
					workRect.Top, segmentWidth, workRect.Height);
			
				int progressWidth = GetScaledValue();
				if ( progressWidth < 0 ) progressWidth = 0;
				
				for ( int i = 0; i < progressWidth; i += segmentRect.Width+segmentLeftGap )
				{
					if ( i+segmentRect.Width+segmentLeftGap > progressWidth && (i+segmentRect.Width+workRect.Left > workRect.Width) ) 
					{
						// if we are about to leave because next segment does not fit
						// draw the portion that fits
						int partialWidth = progressWidth-i;
						g.FillRectangle(new SolidBrush(foregroundColor), 
							segmentRect.Left+i, segmentRect.Top+segmentTopGap, partialWidth, segmentRect.Height-(segmentTopGap*2));
						break;
					}
					g.FillRectangle(new SolidBrush(foregroundColor), segmentRect.Left+i, segmentRect.Top+segmentTopGap, 
						segmentRect.Width, segmentRect.Height-(segmentTopGap*2));
				}
			}
			else
			{
				int segmentHeight = (workRect.Width)*3/4;
				int segmentTopGap = 2;
				int segmentLeftGap = 1;
			
				// One pixel gap on the top and bottom
				segmentHeight -= 2;
                        
				Rectangle segmentRect = new Rectangle(workRect.Left, 
					workRect.Top, workRect.Width, segmentHeight);
			
				int progressHeight = GetScaledValue();
				if ( progressHeight < 0 ) progressHeight = 0;
				for ( int i = 0; i < progressHeight; i += segmentRect.Height+segmentTopGap )
				{
					int top = (workRect.Bottom - segmentRect.Height) - i;
					if ( top < workRect.Top ) 
					{
						// if we are about to leave because next segment does not fit
						// draw the portion that fits
						int partialHeight = progressHeight-i;
						g.FillRectangle(new SolidBrush(foregroundColor), 
							segmentRect.Left+segmentLeftGap, WorkRect.Top, 
							segmentRect.Width-(segmentLeftGap*2), partialHeight);
						break;
					}
					
					Rectangle fillRect =  new Rectangle(segmentRect.Left+segmentLeftGap, top, 
						segmentRect.Width-(segmentLeftGap*2), segmentRect.Height);
						g.FillRectangle(new SolidBrush(foregroundColor), fillRect);
					
				}

			}
		}

		void DrawStandardForegroundSmooth(Graphics g, Rectangle workRect)
		{
			int progressValue = GetScaledValue();
            bool horizontalOrientation = (orientation == Orientation.Horizontal);
			int top = 0;

			if ( horizontalOrientation )
			{
				g.FillRectangle(new SolidBrush(foregroundColor), workRect.Left, workRect.Top, 
					progressValue, workRect.Height);
			}
			else
			{
				top = workRect.Top + (workRect.Height - progressValue);
				g.FillRectangle(new SolidBrush(foregroundColor), workRect.Left, top,
					workRect.Width, progressValue);
			}
						
			if ( ShowProgressText)
			{
				int percent = 0;
				if ( horizontalOrientation )
                    percent = GetScaledValue()*100/workRect.Width;
				else
					percent = GetScaledValue()*100/workRect.Height;

				string text;
				if ( progressText == string.Empty )
                    text = percent.ToString() + " " + "%";	
				else
					text = progressText;

				Size size = TextUtil.GetTextSize(g, text, Font);
			
				// Draw first part of the text in hightlight color in case it needs to be
				Rectangle clipRect = Rectangle.Empty;
				Point pos = Point.Empty;
				int topGap = 0;
				if ( enable3DBorder == true )
					topGap = 1;
				
				if ( horizontalOrientation )
				{
					clipRect = new Rectangle(workRect.Left, workRect.Top,
						progressValue, workRect.Height);
					pos = new Point((workRect.Width - size.Width)/2, 
						(workRect.Height - size.Height)/2 + topGap);
				}
				else
				{
					clipRect = new Rectangle(workRect.Left, top,
						workRect.Width, progressValue);
					pos = new Point((workRect.Width - size.Height)/2,
						(workRect.Height - size.Width)/2); 
						
				}

				g.Clip = new Region(clipRect);
				Color textColor = progressTextHiglightColor;
				if ( textColor == Color.Empty )
					textColor = SystemColors.HighlightText;
				if ( horizontalOrientation )
				{
					g.DrawString(text, Font, new SolidBrush(textColor), pos);
				}
				else
				{
					StringFormat format = new StringFormat();
					format.FormatFlags = StringFormatFlags.DirectionVertical;
					g.DrawString(text, Font, new SolidBrush(textColor), pos, format);
				}

				// Draw rest in control text color if it needs to be
				if ( horizontalOrientation )
				{
					clipRect = new Rectangle(progressValue+workRect.Left, workRect.Top,
						workRect.Width, workRect.Height);
				}
				else
				{
					clipRect = new Rectangle(workRect.Left, workRect.Top, 
						workRect.Width,  workRect.Height - progressValue);

				}
				g.Clip = new Region(clipRect);
				textColor = progressTextColor;
				if ( textColor == Color.Empty )
					textColor = SystemColors.ControlText;
				if ( horizontalOrientation )
				{
					g.DrawString(text, Font, new SolidBrush(textColor), pos);
				}
				else
				{
					StringFormat format = new StringFormat();
					format.FormatFlags = StringFormatFlags.DirectionVertical;
					g.DrawString(text, Font, new SolidBrush(textColor), pos, format);
				}
			}
		}

		void OnWaitingTick(object sender, EventArgs e)
		{
			// Increase position
			if ( barType == ProgressBarType.WaitingGradient )
			{
				if ( orientation == Orientation.Horizontal )
					waitingPos += waitingStep;
				else
					waitingPos -= waitingStep;

				// Reset
				Rectangle rc = WorkRect;
				if ( orientation == Orientation.Horizontal && waitingPos > rc.Width )
				{
					
					waitingPos = rc.Left;
				}
				else if ( orientation == Orientation.Vertical && waitingPos < rc.Top )
				{
					waitingPos = rc.Bottom;
				}
			}
			else if (  barType == ProgressBarType.WaitingGradientTube )
			{
				if ( orientation == Orientation.Horizontal )
				{
					if ( ++tubeOffset > TUBE_WIDTH-1)
						tubeOffset = 0;
				}
				else
				{
					if (--tubeOffset < 0 )
                        tubeOffset = TUBE_WIDTH;
				}
			}
			
			// Repaint control
			Invalidate();

		}

		int GetScaledValue()
		{
			int scaledValue = _value;
			Rectangle rc = ClientRectangle;
			if ( enable3DBorder )
				rc.Inflate(-2, -2);
			else
				rc.Inflate(-1,-1);
			if ( orientation == Orientation.Horizontal )
                scaledValue = (_value-min)*rc.Width/(max - min);
			else
				scaledValue = (_value-min)*rc.Height/(max - min);
			return scaledValue;
		}
		
		void InitializeGradientTubeColors(Color color)
		{
			// The ControlPaint class does not gives me the distribuition of color
			// that I am looking for
			lightColor = ColorUtil.LightColor(color, 51);
			lighterColor = ColorUtil.LightColor(lightColor, 51);
			lightestColor = ColorUtil.LightColor(lighterColor, 51);
			darkColor = ColorUtil.DarkColor(color, 51);
			darkerColor = ColorUtil.DarkColor(darkColor, 51);
		}


		// Properties
		internal Rectangle WorkRect
		{
			get 
			{
				Rectangle rc = ClientRectangle;
				if ( enable3DBorder )
					rc.Inflate(-2, -2);
				else
					rc.Inflate(-1, -1);
				return rc;
			}
		}


		#endregion
	}
}
