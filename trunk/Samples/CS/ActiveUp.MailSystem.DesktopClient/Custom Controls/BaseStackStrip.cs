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
using System.Diagnostics;

namespace ActiveUp.MailSystem.DesktopClient
{
	[ToolboxItem(false)]
	public class BaseStackStrip : ToolStrip
	{
		private ToolStripProfessionalRenderer	_pr=null;

		#region Public API
		public BaseStackStrip()
		{
			// Check Dock
			this.Dock = DockStyle.Fill;
			this.GripStyle = ToolStripGripStyle.Hidden;
			this.Margin = new Padding(0);
			this.CanOverflow = false;
			this.AutoSize = false;

			// Set renderer - override background painting
			SetRenderer();

			// Setup Fonts
			OnSetFonts();

			// Track Preference Changes
			Microsoft.Win32.SystemEvents.UserPreferenceChanged += new Microsoft.Win32.UserPreferenceChangedEventHandler(StackStrip_UserPreferenceChanged);
		}
		#endregion

		#region Protected API
		protected virtual void OnSetRenderer(ToolStripProfessionalRenderer pr)
		{
			// Handled by sub-classes
		}

		protected virtual void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			if (StackStripRenderer != null)
			{
				// Setup colors from the provided renderer
				Color start = StackStripRenderer.ColorTable.ToolStripGradientMiddle;
				Color end = StackStripRenderer.ColorTable.ToolStripGradientEnd;
				
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

				// Draw border
				//e.Graphics.DrawRectangle(SystemPens.ControlDarkDark, bounds);
				e.Graphics.DrawLine(SystemPens.ControlDarkDark, bounds.X, bounds.Y, bounds.Width - 1, bounds.Y);
				e.Graphics.DrawLine(SystemPens.ControlDarkDark, bounds.X, bounds.Y, bounds.X, bounds.Height - 1);
				e.Graphics.DrawLine(SystemPens.ControlDarkDark, bounds.X + bounds.Width - 1, bounds.Y, bounds.X + bounds.Width - 1, bounds.Height - 1);
			}
		}

		protected ToolStripProfessionalRenderer StackStripRenderer
		{
			get { return _pr; }
		}

		protected virtual void OnSetFonts()
		{
			// Base off system fonts
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

		#region Private API
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
					_pr.RenderToolStripBackground += new ToolStripRenderEventHandler(BaseStackStrip_RenderToolStripBackground);

					// Call overridable method
					OnSetRenderer(_pr);
				}

				// User our renderer
				this.Renderer = _pr;
			}
		}
		#endregion

		#region Event Handlers
		void BaseStackStrip_RenderToolStripBackground(object sender, ToolStripRenderEventArgs e)
		{
			// Call override
			OnRenderToolStripBackground(e);
		}

		private void StackStrip_UserPreferenceChanged(object sender, Microsoft.Win32.UserPreferenceChangedEventArgs e)
		{
			OnSetFonts();
		}
		#endregion
	}
}
