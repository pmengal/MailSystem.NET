#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#endregion

using System.Drawing.Drawing2D;

namespace ActiveUp.MailSystem.DesktopClient
{
	public class HeaderStrip : ToolStrip
	{
		private AreaHeaderStyle					_headerStyle = AreaHeaderStyle.Large;
		private ToolStripProfessionalRenderer	_pr=null;

		#region Public API
		public HeaderStrip()
		{
			// Check Dock
			this.Dock = DockStyle.Top;
			this.GripStyle = ToolStripGripStyle.Hidden;
			this.AutoSize = false;

			// Set renderer - override background painting
			SetRenderer();

			// Track Preference Changes
			Microsoft.Win32.SystemEvents.UserPreferenceChanged += new Microsoft.Win32.UserPreferenceChangedEventHandler(HeaderStrip_UserPreferenceChanged);

			// Setup Headers
			SetHeaderStyle();
		}

		[DefaultValue(AreaHeaderStyle.Large)]
		public AreaHeaderStyle HeaderStyle
		{
			get { return _headerStyle; }
			set
			{
				// Save value
				if (_headerStyle != value)
				{
					_headerStyle = value;

					// Set Header Style
					SetHeaderStyle();
				}
			}
		}
		#endregion

		#region Overrides
		protected override void OnRendererChanged(EventArgs e)
		{
			// Call the base
			base.OnRendererChanged(e);

			// Work around bug with setting renderer in the constructor
			SetRenderer();
		}
		#endregion

		#region Private Implementation
		private void SetHeaderStyle()
		{
			// Get system font
			Font	font = SystemFonts.MenuFont;

			if (_headerStyle == AreaHeaderStyle.Large)
			{
				this.Font = new Font("Arial", font.SizeInPoints + 3.75F, FontStyle.Bold);
				this.ForeColor = System.Drawing.Color.White;
			}
			else
			{
				this.Font = font;
				this.ForeColor = System.Drawing.Color.Black;
			}

			// Only way to calculate size
			ToolStripLabel	tsl = new ToolStripLabel();
			tsl.Font =		this.Font;
			tsl.Text =		"I";

			// Set Size
			this.Height = tsl.GetPreferredSize(Size.Empty).Height + 6;
		}

		private void SetRenderer()
		{
			// Set renderer - override background painting
			if ((this.Renderer is ToolStripProfessionalRenderer) && (this.Renderer != _pr))
			{
				if (_pr == null)
				{
					// Only swap out if we're setup to use a professional renderer
					_pr = new ToolStripProfessionalRenderer();

					// Square edges
					_pr.RoundedEdges = false;

					// Improve painting (use our colors)
					_pr.RenderToolStripBackground += new ToolStripRenderEventHandler(Renderer_RenderToolStripBackground);
				}

				// User our renderer
				this.Renderer = _pr;
			}
		}
		#endregion

		#region Event Handlers
		void Renderer_RenderToolStripBackground(object sender, ToolStripRenderEventArgs e)
		{
			Color start;
			Color end;

			if (this.Renderer is ToolStripProfessionalRenderer)
			{
				ToolStripProfessionalRenderer pr = (this.Renderer as ToolStripProfessionalRenderer);

				// Setup colors from the provided renderer
				if (_headerStyle == AreaHeaderStyle.Large)
				{
					start = pr.ColorTable.OverflowButtonGradientMiddle;
					end = pr.ColorTable.OverflowButtonGradientEnd;
				}
				else
				{
					start = pr.ColorTable.MenuStripGradientEnd;
					end = pr.ColorTable.MenuStripGradientBegin;
				}

				// Size to paint
				Rectangle bounds = new Rectangle(Point.Empty, e.ToolStrip.Size);

				// Make sure we need to do work
				if ((bounds.Width > 0) && (bounds.Height > 0))
				{
					using (Brush b = new LinearGradientBrush(bounds, start, end, LinearGradientMode.Vertical))
					{
						e.Graphics.FillRectangle(b, bounds);
					}
				}
			}
		}


		private void HeaderStrip_UserPreferenceChanged(object sender, Microsoft.Win32.UserPreferenceChangedEventArgs e)
		{
			SetHeaderStyle();
		}
		#endregion
	}

	#region AreaHeaderStyle
	public enum AreaHeaderStyle
	{
		Large = 0000,
		Small = 0001
	}
	#endregion
}
