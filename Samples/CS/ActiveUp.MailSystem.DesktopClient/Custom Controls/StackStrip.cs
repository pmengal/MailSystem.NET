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
	public class StackStrip : BaseStackStrip
	{
		const			int				DEFAULT_ITEM_COUNT=4;
		protected event EventHandler	_itemHeightChanged;

		private	ToolStripButton			_last=null;
		private bool					_ignore=false;
		private	Font					_font;

		#region Public API
		public StackStrip() : base()
		{
			// Check Dock
			this.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
		}

		public int ItemCount
		{
			get { return this.Items.Count; }
		}

		public int InitialDisplayCount
		{
			get { return (Math.Min(ItemCount, DEFAULT_ITEM_COUNT)); }
		}

		public event EventHandler ItemHeightChanged
		{
			add { _itemHeightChanged += new EventHandler(value); }
			remove { _itemHeightChanged -= new EventHandler(value); }
		}

		public int ItemHeight
		{
			get { return ((ItemCount > 0) ? this.Items[0].Height : 0); }
		}
		#endregion

		#region Protected API
		protected override void OnSetRenderer(ToolStripProfessionalRenderer pr)
		{
			// Do base work
			base.OnSetRenderer(pr);

			// Button Painting
			pr.RenderButtonBackground += new ToolStripItemRenderEventHandler(StackStrip_RenderButtonBackground);
		}

		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			// Use base rendering
		}

		protected override void OnSetFonts()
		{
			// Call base
			_font = new Font(SystemFonts.IconTitleFont, FontStyle.Bold);

			// Update if different
			if (this.Font != _font)
			{
				this.Font = _font;

				// Notify container
				OnItemHeightChanged(EventArgs.Empty);
			}
		}

		protected virtual void OnItemHeightChanged(EventArgs e)
		{
			if (null != _itemHeightChanged)
			{
				_itemHeightChanged(this, e);
			}
		}

		protected override void OnItemAdded(ToolStripItemEventArgs e)
		{
			ToolStripButton button = e.Item as ToolStripButton;

			if (null != button)
			{
				// Hook Click
				button.CheckedChanged += new EventHandler(StackStripButton_CheckedChanged);
			}
		}
		#endregion

		#region Event Handlers
		void StackStrip_RenderButtonBackground(object sender, ToolStripItemRenderEventArgs e)
		{
			Graphics	g = e.Graphics;
			Rectangle	bounds = new Rectangle(Point.Empty, e.Item.Size);

			Color		gradientBegin = StackStripRenderer.ColorTable.ImageMarginGradientMiddle;
			Color		gradientEnd = StackStripRenderer.ColorTable.ImageMarginGradientEnd;

			ToolStripButton button = e.Item as ToolStripButton;
			if (button.Pressed || button.Checked)
			{
				gradientBegin = StackStripRenderer.ColorTable.ButtonPressedGradientBegin;
				gradientEnd = StackStripRenderer.ColorTable.ButtonPressedGradientEnd;
			}
			else if (button.Selected)
			{
				gradientBegin = StackStripRenderer.ColorTable.ButtonSelectedGradientBegin;
				gradientEnd = StackStripRenderer.ColorTable.ButtonSelectedGradientEnd;
			}

			// Draw Button Background
			using (Brush b = new LinearGradientBrush(bounds, gradientBegin, gradientEnd, LinearGradientMode.Vertical))
			{
				g.FillRectangle(b, bounds);
			}

			// Draw Outilne
			e.Graphics.DrawRectangle(SystemPens.ControlDarkDark, bounds);
		}

		void StackStripButton_CheckedChanged(object sender, EventArgs e)
		{
			ToolStripButton button = (sender as ToolStripButton);

			// Should never be null - but in case someone adds a label
			if (_ignore || (null != button))
			{
				if (button.Checked)
				{
					if ((_last != button) && (null != _last))
					{
						// Unset
						_ignore = true;
						_last.Checked = false;
						_ignore = false;
					}

					_last = button;
				}
				else
				{
					// Make sure something is checked
					bool	foundItem=false;

					foreach (ToolStripItem item in this.Items)
					{
						ToolStripButton btn = (item as ToolStripButton);

						if (null != btn)
						{
							if (btn.Checked)
							{
								foundItem = true;
								break;
							}
						}
					}

					// Verify
					if (!foundItem)
					{
						// Select the last item
						_last = button;

						_ignore = true;
						button.Checked = true;
						_ignore = false;
					}
				}
			}
		}
		#endregion
	}
}
