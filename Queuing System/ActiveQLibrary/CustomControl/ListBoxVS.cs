using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Design;

namespace ActiveQLibrary.CustomControl
{
	/// <summary>
	/// Summary description for ActiveListBox.
	/// </summary>
	public class ListBoxVS : System.Windows.Forms.ListBox
	{
		
		#region Variables

		/// <summary>
		/// Image to show with each element
		/// </summary>
		private Image _image;

		#endregion
		
		#region Constructors
		public ListBoxVS() : base()
		{
			DrawMode = DrawMode.OwnerDrawFixed;
			ItemHeight = ItemHeight + 1;

		}

		#endregion

		#region Overrides
		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			Graphics g = e.Graphics;
			Rectangle bounds = e.Bounds;
			bool selected = (e.State & DrawItemState.Selected) > 0;
			bool editSel = (e.State & DrawItemState.ComboBoxEdit ) > 0;
			if ( e.Index != -1 )
				DrawListBoxItem(g, bounds, e.Index, selected, editSel);
			
		}
		#endregion
		
		#region Properties
		public  new ListBox.ObjectCollection Items
		{
			get{ return base.Items; }
		}

		public Image Img
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

		#endregion

		#region Methods
		public void PassMsg(ref Message m)
		{
			base.WndProc(ref m);
		}
		#endregion

		#region Functions
		protected void DrawListBoxItem(Graphics g, Rectangle bounds, int Index, bool selected, bool editSel)
		{
			// Draw List box item
			if ( Index != -1)
			{
				if ( selected )
				{
					// Draw highlight rectangle
					using ( Brush b = new SolidBrush(ColorUtil.VSNetSelectionColor) )
					{
						g.FillRectangle(b, bounds.Left, bounds.Top, bounds.Width, bounds.Height);
					}
					using ( Pen p = new Pen(ColorUtil.VSNetBorderColor) )
					{
						g.DrawRectangle(p, bounds.Left, bounds.Top, bounds.Width-1, bounds.Height-1);
					}
				}
				else
				{
					// Erase highlight rectangle
					g.FillRectangle(SystemBrushes.Window, bounds.Left, bounds.Top, bounds.Width, bounds.Height);
				}
				
				try
				{
					string item = (string)Items[Index];
					Color currentColor = Color.FromName(item);
			
					if (item != "")
					{
						using ( Brush b = new SolidBrush(currentColor) )
						{
							g.FillRectangle(new SolidBrush(currentColor), bounds.Left+2, bounds.Top+2, 20, bounds.Height-4);
						}
						//g.DrawRectangle(Pens.Black, new Rectangle(bounds.Left+1, bounds.Top+1, 21, bounds.Height-3));
						if (_image != null)
						{
							Image imgBestSize = new Bitmap(_image,new Size(16,16));
							g.DrawImage(imgBestSize,new Point(bounds.Left + 1 , bounds.Top));
							g.DrawString(item, SystemInformation.MenuFont, SystemBrushes.ControlText, new Point(bounds.Left + 20 , bounds.Top));
						}
						else
							g.DrawString(item, SystemInformation.MenuFont, SystemBrushes.ControlText, new Point(bounds.Left + 1 , bounds.Top));

					}
				}

				catch
				{
				}
				
			}
		}
		#endregion

	}
}
