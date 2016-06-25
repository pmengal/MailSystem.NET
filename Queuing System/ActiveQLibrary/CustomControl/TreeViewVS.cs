using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ActiveQLibrary.CustomControl
{
	/// <summary>
	/// Summary description for TreeViewEx.
	/// </summary>
	public class TreeViewVS : System.Windows.Forms.TreeView
	{
		bool itemHasFocus = false;
		
		public TreeViewVS()
		{
		}

		protected override  void WndProc(ref Message message)
		{
			base.WndProc(ref message);

			switch (message.Msg)
			{
				// Reflected Messages come from the treeview control itself
				case (int)ReflectedMessages.OCM_NOTIFY:
					NMHDR nm2 = (NMHDR) message.GetLParam(typeof(NMHDR));	
					switch (nm2.code)
					{
						case (int)NotificationMessages.NM_CUSTOMDRAW:
							NotifyTreeCustomDraw(ref message); 
						break;
						default:
						break;
					}
					break;
				default:
					break;
			}

		}

		private bool NotifyTreeCustomDraw(ref Message m)
		{
			m.Result = (IntPtr)CustomDrawReturnFlags.CDRF_DODEFAULT;
			NMTVCUSTOMDRAW tvcd = (NMTVCUSTOMDRAW)m.GetLParam(typeof(NMTVCUSTOMDRAW));
			IntPtr thisHandle = Handle;
			
			if ( tvcd.nmcd.hdr.hwndFrom != Handle)
				return false;
			switch (tvcd.nmcd.dwDrawStage)
			{
				case (int)CustomDrawDrawStateFlags.CDDS_PREPAINT:
					// Ask for Item painting notifications
					m.Result = (IntPtr)CustomDrawReturnFlags.CDRF_NOTIFYITEMDRAW;
					break;
				case (int)CustomDrawDrawStateFlags.CDDS_ITEMPREPAINT:
					
					itemHasFocus = false;
					if(	(tvcd.nmcd.uItemState & (int)CustomDrawItemStateFlags.CDIS_FOCUS) != 0)
						itemHasFocus = true;
					
					// Set the text and background text color to the window bakcground
					// text so that we don't see the text being painted
					tvcd.clrText = ColorUtil.RGB(SystemColors.Window.R,
						SystemColors.Window.G, SystemColors.Window.B);
					tvcd.clrTextBk = ColorUtil.RGB(SystemColors.Window.R,
						SystemColors.Window.G, SystemColors.Window.B);
					
					// Put structure back in the message
					Marshal.StructureToPtr(tvcd, m.LParam, true);
					m.Result = (IntPtr)CustomDrawReturnFlags.CDRF_NOTIFYPOSTPAINT;
					break;
				case (int)CustomDrawDrawStateFlags.CDDS_ITEMPOSTPAINT:
					DoTreeCustomDrawing(ref m);
					break;
				default:
					break;

			}
			return false;
		}


		private void DoTreeCustomDrawing(ref Message m)
		{
			NMTVCUSTOMDRAW tvcd = (NMTVCUSTOMDRAW)m.GetLParam(typeof(NMTVCUSTOMDRAW));
			IntPtr hNode = (IntPtr)tvcd.nmcd.dwItemSpec;
			Rectangle rect = GetItemRect(hNode);
			
			// Create a graphic object from the Device context in the message
			Graphics g = Graphics.FromHdc(tvcd.nmcd.hdc);
			// If this item has the focus draw the higlighting rectangle			
			if (itemHasFocus)
			{
				using ( Brush brush = new SolidBrush(ColorUtil.VSNetSelectionColor))
				{
					g.FillRectangle(brush, rect.Left, rect.Top, rect.Width-1, rect.Height-1);
					g.DrawRectangle(SystemPens.Highlight, rect.Left, rect.Top, rect.Width-1, rect.Height-1);
				}
			}
			
			// Draw Text
			string itemText = GetItemText(hNode);
			Size textSize = TextUtil.GetTextSize(g, itemText, Font);
			Point pos = new Point(rect.Left, rect.Top + (rect.Height - textSize.Height)/2);
			g.DrawString(itemText, Font, SystemBrushes.ControlText, pos);
                        					            			
			// Put structure back in the message
			Marshal.StructureToPtr(tvcd, m.LParam, true);
			m.Result = 	(IntPtr)CustomDrawReturnFlags.CDRF_SKIPDEFAULT;

		}


		Rectangle GetItemRect(IntPtr hTreeItem)
		{
			RECT rc = new RECT();
			
			// This is how Microsoft recommends to shovel the handle to the tree Node into
			// the rectangle structure that will be used to send a message to retrieve
			// the bounds of the tree item. Any wonders why Java became a huge success?
			unsafe 
			{ 
				*(IntPtr*)&rc = hTreeItem;
			}
			
			// --I wanted to use the TreeView NET control itself to get the bounds of the current
			// tree node, but a quick inspection through the documentation made me realize that I would 
			// have to loop through the Nodes collection and all the subnodes collections that
			// each node have which would be terribly expensive just to retrieve such information,
			// specially when I need to do this really quick
			// instead let's use the horrible but efficient way--
			WindowsAPI.SendMessage(Handle, (int)TreeViewMessages.TVM_GETITEMRECT, 1, ref rc);
			
			return new Rectangle(rc.left, rc.top, rc.right-rc.left, rc.bottom-rc.top);
		}

		string GetItemText(IntPtr hTreeItem)
		{
            string text;
			TVITEM tvi = new TVITEM();
			tvi.hItem = hTreeItem;
			tvi.mask = (int)TreeViewItemFlags.TVIF_TEXT;
			tvi.cchTextMax = 4096;
			tvi.pszText = Marshal.AllocHGlobal(4096);
			WindowsAPI.SendMessage(Handle, TreeViewMessages.TVM_GETITEMW, 0, ref tvi);
			text = Marshal.PtrToStringAuto(tvi.pszText);
			return text;
		}


		
	}
}
