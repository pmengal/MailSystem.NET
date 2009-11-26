using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Resources;

namespace ActiveQLibrary.CustomControl
{
	/// <summary>
	/// Summary description for MenuItemEx.
	/// </summary>
	public class MenuItemVS : MenuItem
	{
		static ColorGroup group = ColorGroup.GetColorGroup();
		static Color bgColor  = group.bgColor;
		static Color stripeColor = group.stripeColor;
		static Color selectionColor  = group.selectionColor;
		static Color borderColor = group.borderColor;
		
		static int iconSize = SystemInformation.SmallIconSize.Width + 5;
		static int itemHeight;
		static bool doColorUpdate = false;
		string shortcuttext = "";
		Bitmap icon = null;
		EventHandler clickHandler = null;
		
		// We could use an image list to associate
		// the menu items with an bitmap instead of
		// assigning a whole Bitmap object to the menu item
		ImageList imageList = null;
		int imageIndex = -1;


		static int BITMAP_SIZE = 16;
		static int STRIPE_WIDTH = iconSize + 2;
		protected static ImageList menuImages;
		static Color transparentColor = Color.FromArgb(192, 192, 192);
								
		static MenuItemVS()
		{
			// Initialize menu glyphs: checkmark and bullet
			menuImages = new ImageList();
			menuImages.ImageSize = new Size(BITMAP_SIZE, BITMAP_SIZE);
			/*Assembly thisAssembly = Assembly.GetAssembly(Type.GetType("ActiveQLibrary.CustomControl.MenuItemEx"));
			ResourceManager rm = new ResourceManager("Resources.ImagesMenu", thisAssembly);
			Bitmap glyphs = (Bitmap)rm.GetObject("Glyphs");
			glyphs.MakeTransparent(transparentColor);
			menuImages.Images.AddStrip(glyphs);*/

			Image img = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ActiveQLibrary.CustomControl.check.bmp"));
			Bitmap glyphs = new Bitmap(img,BITMAP_SIZE,BITMAP_SIZE);
			//glyphs.MakeTransparent(Color.FromArgb(0,0,0));
			menuImages.Images.AddStrip((Image)glyphs);

			img = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ActiveQLibrary.CustomControl.bullet.bmp"));
			glyphs = new Bitmap(img,BITMAP_SIZE,BITMAP_SIZE);
			//glyphs.MakeTransparent(Color.FromArgb(0,0,0));
			menuImages.Images.AddStrip((Image)glyphs);
		}
		
		// constructors
		// ---------------------------------------------------
		public MenuItemVS() : this(null, null)
		{
		}	

		public MenuItemVS(string name, EventHandler handler, Shortcut shortcut) : this(name, handler)
		{
			Initialize(null, shortcut, handler, null, -1);
		}
		
		public MenuItemVS(string name, Bitmap icon, Shortcut shortcut, EventHandler handler) : this(name, handler)
		{
			Initialize(icon, shortcut, handler, null, -1);
		}

		public MenuItemVS(string name, ImageList imageList, int imageIndex, Shortcut shortcut, EventHandler handler) : this(name, handler)
		{
			Initialize(icon, shortcut, handler, imageList, imageIndex);
		}

		public MenuItemVS(string name, EventHandler handler) : base(name, handler)
		{
			Initialize(null, Shortcut.None, handler, null, -1);
		}

		private void Initialize(Bitmap bitmap, Shortcut shortcut, EventHandler handler, ImageList list, int imageIndex)
		{
			OwnerDraw = true;
			this.Shortcut = shortcut;
			icon = bitmap;
			clickHandler = handler;
			imageList = list;
			this.imageIndex = imageIndex;

		}

		static public MenuItem CloneMenu(MenuItemVS currentItem)
		{
			MenuItemVS clonedItem = new MenuItemVS(currentItem.Text, (Bitmap)currentItem.Icon, 
				(Shortcut)currentItem.Shortcut, currentItem.ClickHandler);
			// Preserve the enable and check state
			clonedItem.Enabled = currentItem.Enabled;
			clonedItem.Checked = currentItem.Checked;
			clonedItem.RadioCheck = currentItem.RadioCheck;

			foreach (MenuItemVS item in currentItem.MenuItems)
			{
				clonedItem.MenuItems.Add(CloneMenu(item));
			}
			return clonedItem;
		}

		public Bitmap Icon 
		{
			get 
			{
				return icon;
			}
			set 
			{
				icon = value;
			}
		}

		public string ShortcutText 
		{
			get 
			{
				return shortcuttext;
			}
			set 
			{
				shortcuttext = value;
			}
		}

		public Color TransparentColor
		{
			get 
			{
				return transparentColor;
			}
			set
			{
				transparentColor = value;
			}
		}

		public ImageList ImageList
		{
			set 
			{ 
				imageList = value; 
			}
			get { return imageList; }
		}

		public int ImageIndex
		{
			set { imageIndex = value; }
			get { return imageIndex;  }
		}

		public EventHandler ClickHandler
		{
			set { clickHandler = value;}
			get { return clickHandler; }
		}

		protected override void OnSelect(EventArgs e)
		{
			// This is to support popup menus when using this class
			// in conjunction with a toolbar that behaves like a menu
			Menu parent = Parent;
			while (!(parent is CommandBarMenu) &&   !(parent == null) )
			{
				if (parent is MenuItemVS)
					parent = (parent as MenuItemVS).Parent;
				else if (parent is MenuItem)
					parent = (parent as MenuItem).Parent;
				else if ( parent == Parent.GetMainMenu() )
					parent = null;
				else
					parent = null;
				
			}
				
			if ( parent is CommandBarMenu )
			{
				CommandBarMenu cbm = (CommandBarMenu)parent;
				cbm.SelectedMenuItem = this;
			}

			base.OnSelect(e);
		}

		static public void UpdateMenuColors(object sender, EventArgs e)
		{
			doColorUpdate = true;
		}

		private void DoUpdateMenuColors()
		{
			ColorGroup group = ColorGroup.GetColorGroup();
			bgColor  = group.bgColor;
			stripeColor = group.stripeColor;
			selectionColor  = group.selectionColor;
			borderColor = group.borderColor;
			
			doColorUpdate = false;

		}
	            		
		// overrides
		// ---------------------------------------------------------
		protected override void OnMeasureItem(MeasureItemEventArgs e)
		{
			base.OnMeasureItem(e);
			
			// measure shortcut text
			if (Shortcut != Shortcut.None) 
			{
				string text = "";
				int    key  = (int)Shortcut;
				int    ch   = key & 0xFF;
				if (((int)Keys.Control & key) > 0)
					text += "Ctrl+";
				if (((int)Keys.Shift & key) > 0)
					text += "Shift+";
				if (((int)Keys.Alt & key) > 0)
					text += "Alt+";
				
				if (ch >= (int)Shortcut.F1 && ch <= (int)Shortcut.F12)
					text += "F" + (ch - (int)Shortcut.F1 + 1);
				else 
				{
					if ( Shortcut == Shortcut.Del) 
					{
						text += "Del";
					}
					else 
					{
						text += (char)ch;
					}
				}
				shortcuttext = text;
			} 
			
			if (Text == "-") 
			{
				e.ItemHeight = 8;
				e.ItemWidth  = 4;
				return;
			}
				
			bool topLevel = Parent == Parent.GetMainMenu();
			string tempShortcutText = shortcuttext;
			if ( topLevel ) 
			{
				tempShortcutText = "";
			}
			int textwidth = (int)(e.Graphics.MeasureString(Text + tempShortcutText, SystemInformation.MenuFont).Width);
			int extraHeight = 2;
			e.ItemHeight  = SystemInformation.MenuHeight + extraHeight;
			if ( topLevel )
				e.ItemWidth  = textwidth - 5; 
			else
				e.ItemWidth   =  textwidth + 45;

			// save menu item heihgt for later use
			itemHeight = e.ItemHeight;
			
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			
			base.OnDrawItem(e);

			if ( doColorUpdate) 
			{
				DoUpdateMenuColors();
			}

			Graphics g = e.Graphics;
			Rectangle bounds = e.Bounds;
			bool selected = (e.State & DrawItemState.Selected) > 0;
			bool toplevel = (Parent == Parent.GetMainMenu());
			bool hasIcon  = Icon != null;
			bool enabled = Enabled;

			// Try to  speed up drawing top level a little bit
			if ( toplevel )
			{
				DrawBackground(g, bounds, e.State, toplevel, hasIcon, enabled);
				DrawMenuText(g, bounds, Text, shortcuttext, Enabled, toplevel, e.State);
				return;
			}
			
			DrawBackground(g, bounds, e.State, toplevel, hasIcon, enabled);
			if (hasIcon)
				DrawIcon(g, Icon, bounds, selected, Enabled, Checked);
			else if ( imageList != null && imageIndex != -1 )
			{
				DrawIcon(g, imageList.Images[imageIndex], bounds, selected, Enabled, Checked);
			}

			else
				if (Checked && !hasIcon)
				DrawMenuGlyph(g, bounds, selected, true);
			else if ( RadioCheck )
				DrawMenuGlyph(g, bounds, selected, false);
			
			if (Text == "-") 
			{
				DrawSeparator(g, bounds);
			} 
			else 
			{
				DrawMenuText(g, bounds, Text, shortcuttext, Enabled, toplevel, e.State);
			}
		}

		public void DrawMenuGlyph(Graphics g, Rectangle bounds, bool selected,  bool bCheckMark)
		{
			int checkTop = bounds.Top + (itemHeight - BITMAP_SIZE)/2;
			int checkLeft = (bounds.Left + ( STRIPE_WIDTH - BITMAP_SIZE)/2)+1;
			
			g.FillRectangle(new SolidBrush(selected ? group.darkSelectionColor:selectionColor), 
				bounds.Left+1, bounds.Top+1, STRIPE_WIDTH-3, bounds.Height-3);
			
			menuImages.Draw(g, checkLeft, checkTop, bCheckMark?0:1);
			g.DrawRectangle(new Pen(borderColor), bounds.Left+1, bounds.Top+1, STRIPE_WIDTH-3, bounds.Height-3);
		}

		
		public void DrawIcon(Graphics g, Image icon, Rectangle bounds, bool selected, bool enabled, bool isChecked)
		{
			// make icon transparent
			Color transparentColor  = Color.FromArgb(0, 128, 128);
			Bitmap tempIcon = (Bitmap)icon;
			tempIcon.MakeTransparent(transparentColor);

			int iconTop = bounds.Top + (itemHeight - BITMAP_SIZE)/2;
			int iconLeft = bounds.Left + ( STRIPE_WIDTH - BITMAP_SIZE)/2;
			if (enabled) 
			{
				if (selected) 
				{
					if ( isChecked ) 
					{
						DrawCheckedRectangle(g, bounds);
						g.DrawImage(icon, iconLeft + 1, iconTop);
					}
					else 
					{
						ControlPaint.DrawImageDisabled(g, icon, iconLeft + 1, iconTop, Color.Black);
						g.DrawImage(icon, iconLeft, iconTop-1);
					}
				} 
				else 
				{
					if ( isChecked ) 
						DrawCheckedRectangle(g, bounds);
					g.DrawImage(icon, iconLeft + 1, iconTop);
				}
			} 
			else 
			{
				ControlPaint.DrawImageDisabled(g, icon, iconLeft + 1, iconTop, SystemColors.HighlightText);
			}
		}


		private void DrawCheckedRectangle(Graphics g, Rectangle bounds)
		{
			int checkTop = bounds.Top + (itemHeight - BITMAP_SIZE)/2;
			int checkLeft = bounds.Left + ( STRIPE_WIDTH - BITMAP_SIZE)/2;
			g.FillRectangle(new SolidBrush(selectionColor), bounds.Left+1, bounds.Top+1, STRIPE_WIDTH-3, bounds.Height-3);
			g.DrawRectangle(new Pen(borderColor), bounds.Left+1, bounds.Top+1, STRIPE_WIDTH-3, bounds.Height-3);
		}

	
		public void DrawSeparator(Graphics g, Rectangle bounds)
		{
			int y = bounds.Y + bounds.Height / 2;
			g.DrawLine(new Pen(SystemColors.ControlDark), bounds.X + iconSize + 7, y, bounds.X + bounds.Width - 2, y);
		}
		
		public void DrawBackground(Graphics g, Rectangle bounds, DrawItemState state, bool toplevel, bool hasicon, bool enabled)
		{
			bool selected = (state & DrawItemState.Selected) > 0;
			if (selected || ((state & DrawItemState.HotLight) > 0)) 
			{
				
				if (toplevel && selected) 
				{   // draw toplevel, selected menuitem
					bounds.Inflate(-1, 0);
					g.FillRectangle(new SolidBrush(stripeColor), bounds);
					ControlPaint.DrawBorder3D(g, bounds.Left, bounds.Top, bounds.Width, 
						bounds.Height, Border3DStyle.Flat, Border3DSide.Top | Border3DSide.Left | Border3DSide.Right);
				} 
				else 
				{   // draw menuitem hotlighted
					if ( enabled ) 
					{   
						g.FillRectangle(new SolidBrush(selectionColor), bounds);
						g.DrawRectangle(new Pen(borderColor), bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
					}
					else 
					{
						// Check if menu item was selected by using the mouse or the keyboard
						RECT rc = new RECT();
						IntPtr parentHandle = Parent.Handle;
						uint index = (uint)Index;
						bool success = WindowsAPI.GetMenuItemRect(IntPtr.Zero, parentHandle, index, ref rc);
						Rectangle menuRect = new Rectangle(rc.left, rc.top, rc.right-rc.left, rc.bottom-rc.top);
						Point mp = Control.MousePosition;
						
						if ( !menuRect.Contains(mp) ) 
						{
							// Menu was selected by using keyboard
							g.FillRectangle(new SolidBrush(bgColor), bounds);
							g.DrawRectangle(new Pen(borderColor), bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
						}
						else 
						{
							// Menu was selected by using mouse
							g.FillRectangle(new SolidBrush(stripeColor), bounds);
							bounds.X += STRIPE_WIDTH;
							bounds.Width -= STRIPE_WIDTH;
							g.FillRectangle(new SolidBrush(bgColor), bounds);
						}
					}
				}
			} 
			else 
			{
				if (!toplevel) 
				{   // draw menuitem, unselected
					g.FillRectangle(new SolidBrush(stripeColor), bounds);
					bounds.X += STRIPE_WIDTH;
					bounds.Width -= STRIPE_WIDTH;
					g.FillRectangle(new SolidBrush(bgColor), bounds);
				} 
				else 
				{
					// draw toplevel, unselected menuitem
					g.FillRectangle(SystemBrushes.Control, bounds);
				}
			}
		}

		public void DrawMenuText(Graphics g, Rectangle bounds, string text, string shortcut, bool enabled, bool toplevel, DrawItemState state )
		{
			StringFormat stringformat = new StringFormat();
			stringformat.HotkeyPrefix = HotkeyPrefix.Show;
		
			// if 3D background happens to be black, as it is the case when
			// using a high contrast color theme, then make sure text is white
			bool highContrast = false;
			bool whiteHighContrast = false;
			if ( SystemColors.Control.ToArgb() == Color.FromArgb(255,0,0,0).ToArgb() ) highContrast = true;
			if ( SystemColors.Control.ToArgb() == Color.FromArgb(255,255,255,255).ToArgb() ) whiteHighContrast = true;

			// if menu is a top level, extract the ampersand that indicates the shortcut character
			// so that the menu text is centered
			string textTopMenu = text;
			if ( toplevel ) 
			{
				int index = text.IndexOf("&");
				if ( index != -1 ) 
				{
					// remove it
					text = text.Remove(index,1);
				}
			}
			
			int textwidth = (int)(g.MeasureString(text, SystemInformation.MenuFont).Width);
			int x = toplevel ? bounds.Left + (bounds.Width - textwidth) / 2: bounds.Left + iconSize + 10;
			int topGap = 4;
			if ( toplevel ) topGap = 2;
			int y = bounds.Top + topGap;
			Brush brush = null;
			
			if (!enabled)
				brush = new SolidBrush(Color.FromArgb(120, SystemColors.MenuText));
			else if ( highContrast ) 
				brush = new SolidBrush(Color.FromArgb(255, SystemColors.MenuText));
			else 
				brush = new SolidBrush(Color.Black);

			if ( whiteHighContrast && ( (state & DrawItemState.HotLight) > 0 
				|| ( (state & DrawItemState.Selected) > 0 && !toplevel )) )
				brush = new SolidBrush(Color.FromArgb(255, Color.White));
			
			if ( toplevel ) text = textTopMenu;
			g.DrawString(text, SystemInformation.MenuFont, brush, x, y, stringformat);
					
			// don't draw the shortcut for top level menus
			// in case there was actually one
			if ( !toplevel ) 
			{
				// draw shortcut right aligned
				stringformat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
				g.DrawString(shortcut, SystemInformation.MenuFont, brush, bounds.Width - 10 , bounds.Top + topGap, stringformat);
			}
		}


	}

}