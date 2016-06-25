using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Resources;
using System.Drawing.Text;
using System.Reflection;
using System.Collections;
using System.ComponentModel;

namespace ActiveQLibrary.CustomControl
{
	#region Enumerations
	public enum SortedListViewFormatType
	{
		String,
		Numeric,
		Date,
		Custom
	}

	public enum SortedListViewSortDirection
	{
		Ascending,
		Descending
	}
	#endregion

	#region Interfaces
	public interface IListViewEmbeddedControl
	{
		void FastRender(Graphics g, Rectangle bounds);
	}
	#endregion

	#region Delegates
	public delegate int ListSortEvent(ListViewItem item1, ListViewItem item2);	
	#endregion

	#region Helper Classes
	internal class HeaderIconHelper
	{
		#region Class Variables
		int headerIndex;
		int iconIndex;
		#endregion
		
		#region Constructor
		public HeaderIconHelper(int HeaderIndex, int IconIndex)
		{
			headerIndex = HeaderIndex;
			iconIndex = IconIndex;
		}
		#endregion

		#region Properties
		public int HeaderIndex
		{
			set { headerIndex = value; }
			get { return headerIndex; }
		}

		public int IconIndex
		{
			set { iconIndex = value; }
			get { return iconIndex; }
		}

		#endregion
	}

	internal class RowSorterHelper
	{
		#region Class Variables
		int columnIndex;
		SortedListViewFormatType format;
		ListSortEvent sortEvent = null;
		#endregion
		
		#region Constructors
		public RowSorterHelper(int columnIndex, SortedListViewFormatType format)
		{
			this.columnIndex = columnIndex;
			this.format = format;
		}

		public RowSorterHelper(int columnIndex, SortedListViewFormatType format, ListSortEvent sortEvent)
		{
			this.columnIndex = columnIndex;
			this.format = format;
			this.sortEvent = sortEvent;
		}

		#endregion

		#region Properties
		public int ColumnIndex
		{
			set { columnIndex = value; }
			get { return columnIndex; }
		}

		public SortedListViewFormatType Format
		{
			set { format = value; }
			get { return format; }
		}

		public ListSortEvent SortEvent
		{
			set { sortEvent = value; }
			get { return sortEvent; }
		}

		#endregion
	}

	internal class HeaderHook : System.Windows.Forms.NativeWindow
	{
		#region Class Variables
		ListViewVS listView = null;
		Point mouseDownPos = Point.Empty;
		bool gotLeftButtonDoubleClick = false;
		#endregion
		
		#region Constructors
		public HeaderHook(ListViewVS lv)
		{
			listView = lv;
		}
		#endregion

		#region Overrides
		protected override  void WndProc(ref Message m)
		{
			int message = m.Msg;
			Point mousePos = new Point(0,0);

			if ( message == (int)Msg.WM_LBUTTONDBLCLK )
			{
				gotLeftButtonDoubleClick = true;
			}

			if ( message == (int)Msg.WM_LBUTTONDOWN || 
				message == (int)Msg.WM_LBUTTONUP) 
			{
				mousePos = WindowsAPI.GetPointFromLPARAM((int)m.LParam);
				listView.LastMousePosition = mousePos;
			}

			if ( listView.SortingEnabled && 
				message == (int)Msg.WM_LBUTTONDOWN && listView.Tracking == false ) 
			{
				mouseDownPos = mousePos; 
				for (int i = 0; i < listView.Columns.Count; i++ )
				{
					Rectangle rc = listView.GetHeaderItemRect(i);
					if ( rc.Contains(mousePos))
					{
						listView.PressedHeaderItem = i;
						WindowsAPI.InvalidateRect(m.HWnd, IntPtr.Zero, 0);
					}
				}
			}
			
			if ( listView.SortingEnabled && 
				message == (int)Msg.WM_LBUTTONUP && listView.Tracking == false 
				&& gotLeftButtonDoubleClick == false )
			{
				listView.PressedHeaderItem = -1;
				// Don't do any sorting if the mouse is being actually used
				// to drag the header 
				bool mouseMoved = (Math.Abs(mousePos.X-mouseDownPos.X))>5;
				
				if ( !mouseMoved )
				{
					for (int i = 0; i < listView.Columns.Count; i++ )
					{
						Rectangle rc = listView.GetHeaderItemRect(i);
						if ( rc.Contains(mousePos))
						{
							listView.LastSortedColumn = i;
							if ( listView.SortOrder == SortOrder.None)
							{
								// We will set the sorting to descending
								// because the default sorting already took place
								// and default sorting is ascending
								listView.SortOrder = SortOrder.Descending;
								
								// Make embedded controls invisible
                                // so that only the ones that need to be showing
								// will be made visible after the sort is done
								listView.MakeEmbeddedControlsInvisible();

								listView.Sort();

							}
							else 
							{
								if ( listView.SortOrder == SortOrder.Ascending )
									listView.SortOrder = SortOrder.Descending;
								else
									listView.SortOrder = SortOrder.Ascending;
								listView.MakeEmbeddedControlsInvisible();
								listView.Sort();
							}
						}
					}
				}

				mouseDownPos = Point.Empty;
				// update item
				WindowsAPI.InvalidateRect(m.HWnd, IntPtr.Zero, 0);

			}

			if ( message == (int)Msg.WM_LBUTTONUP )
			{
				gotLeftButtonDoubleClick = false;
			}

			base.WndProc(ref m);

		}

		#endregion
	}
    
	public class CompareListItems : IComparer
	{
		#region Class Variables
		ListViewVS listView = null;
		#endregion
		
		#region Constructors
		public CompareListItems(ListViewVS lv)
		{
			listView = lv;
		}
		#endregion

		#region Methods
		public int Compare(object obj1, object obj2)
		{
			ListViewItem item1 = (ListViewItem)obj1;
			ListViewItem item2 = (ListViewItem)obj2;
			
			// Bail out if something is not quite rigth
			if ( item1 == null || item2 == null)
				return 0;
			
			RowSorterHelper rs = listView.GetRowSorterHelper();
			string string1 = item1.Text;
			string string2 = item2.Text;
			int result = 0;
												
			if ( listView.LastSortedColumn != 0 )
			{
				// adjust the objets if we have to sort subitems
				// This routine gets called the moment an item gets inserted
				// even if the column for which we are sorting is not there yet
				// To make this routing more resilient, check if the index is
				// valid for the subitems collecion, and to even go a step further
				// -- since I have seen a bug where the subitems collection "Length" gets
				// incremented by more than one element and the bogus element gets to be null--
				// check that the subitems is not null even though the collection returns it
				if ( listView.LastSortedColumn >= item1.SubItems.Count ||
					listView.LastSortedColumn >= item2.SubItems.Count )
					return result;

				// Don't trust subitems collection
				// Check for null objects
				if ( item1.SubItems[listView.LastSortedColumn] == null 
					|| item2.SubItems[listView.LastSortedColumn] == null )
					return result;

				string1 = item1.SubItems[listView.LastSortedColumn].Text;
				string2 = item2.SubItems[listView.LastSortedColumn].Text;
				Debug.Assert(string1 != null && string2 != null);
			}

			if ( rs != null )
			{
				if ( rs.Format == SortedListViewFormatType.String)
					result = CompareStrings(string1, string2, listView.SortOrder);
				else if ( rs.Format == SortedListViewFormatType.Numeric )
					result = CompareNumbers(string1, string2, listView.SortOrder);
				else if ( rs.Format == SortedListViewFormatType.Date )
					result = CompareDates(string1, string2, listView.SortOrder);
				else if ( rs.Format == SortedListViewFormatType.Custom)
				{
					if ( rs.SortEvent != null )
					{
						result = rs.SortEvent((ListViewItem)obj1, (ListViewItem)obj2);
						if ( listView.SortOrder == SortOrder.Descending )
							result *= -1;
					}
				}
			}
			else if ( rs == null )
			{
				// Consider column as strings
				result = CompareStrings(string1, string2, listView.SortOrder);
			}
			
			return result;
		}

		#endregion

		#region Implementation
		int CompareStrings(string string1, string string2, SortOrder sortOrder)
		{
			int result = string.Compare(string1, string2);
			if ( sortOrder == SortOrder.Descending)
				result *= -1;
			return result;
		}

		int CompareNumbers(string string1, string string2, SortOrder sortOrder)
		{
			// Parse the object as if the were floating number that will take
			// care of both cases: integers and floats
			// -- exceptions will be thrown if they cannot be parsed
			float float1 = float.Parse(string1);
			float float2 = float.Parse(string2);
			int result = float1.CompareTo(float2);
			if ( sortOrder == SortOrder.Descending)
				result *= -1;
			return result;
			
		}

		int CompareDates(string string1, string string2, SortOrder sortOrder)
		{
			// Parse the object as if the were floating number that will take
			// care of both cases: integers and floats
			// -- exceptions will be thrown if they cannot be parsed
			DateTime date1 = DateTime.Parse(string1);
			DateTime date2 = DateTime.Parse(string2);
			int result = DateTime.Compare(date1, date2);
			if ( sortOrder == SortOrder.Descending)
				result *= -1;
			return result;
			
		}
		#endregion
	}

	public class ListViewEmbeddedControl
	{
		#region Class Variables
		int row = -1;
		int col = -1;
		Control control = null;
		#endregion

		#region Constructor
		public ListViewEmbeddedControl(Control control)
		{
			// Row a Col will be dynamically updated
			// to be able to support sorting in combination
			// with embedded controls
			this.control = control;
		}
		#endregion

		#region Properties
		public Control Control
		{
			get { return control; }
		}
		public int Row
		{
			set { row = value; }
			get { return row; }
		}

		public int Col
		{
			set { col = value; }
			get { return col; }
		}

		#endregion
	
	}
    
	#endregion

	/// <summary>
	/// Summary description for SortableListView.
	/// </summary>
	public class ListViewVS : ListView
	{
		
		#region Class Variables
		// Keeps track of the header control handle
		// so that we can distinguish between notification and
		// reflected messages
		IntPtr hHeader = IntPtr.Zero;

		// Keep track of what column was sorted last
		int lastSortedColumn = 0;
		bool setInitialSortColumn = false;

		HeaderHook headerHook = null;
		bool tracking = false;
		int pressedHeaderItem = -1;

		// To keep track if the cursor
		// hit a header divider
		Point lastMousePosition;

		// ImageList for the check boxes
		// in case user decide to use checkboxes
		ImageList checkBoxesImageList;

		// Header Icons
		ImageList headerImageList;
		ArrayList headerIconsList = new ArrayList();

		// Sorting helper 
		ArrayList rowSorterList = new ArrayList();
		
		// We only support small 16x16 icons
		const int IMAGE_WIDTH = 16;
		const int TEXT_TO_ARROW_GAP = 15;
		const int ARROW_WIDTH = 12;
		const int BUFFER_SIZE = 1024;

		// Support to use a custom color for the header
		Color headerColor = Color.Empty;

		// Don't use the Sorting property of the ListView class
		// it has the nasty side effect of causing the header control
		// to be destroyed
		// Keep track of the what sorting order on this variable
		SortOrder sortOrder = SortOrder.Ascending;
		bool sortingEnabled = true;
		bool paintSortedColumnBackground = true;
		
		// Checkbook look
		bool checkBookLookEnabled = false;
		Color checkBookOddRowBackColor = Color.Empty;
		Color checkBookOddRowForeColor = Color.Empty;
		Color checkBookEvenRowBackColor = Color.Empty;
		Color checkBookEvenRowForeColor = Color.Empty;

		// Embedded Controls
		Hashtable embeddedControlsHashTable = null;
        
		#endregion

		#region Constructors
		public ListViewVS()
		{
		
			SetStyle(ControlStyles.UserPaint, false);
						
			// Control needs to have full row select and detail
			// view enable otherwise it won't behave as intended
			FullRowSelect = true;
			View = View.Details;
			HeaderStyle = ColumnHeaderStyle.Nonclickable;
			InitializeCheckBoxesImageList();

			// Initialize embedded cell controls
			embeddedControlsHashTable = new Hashtable();

		}

		private void InitializeCheckBoxesImageList()
		{
			checkBoxesImageList = new ImageList();
			checkBoxesImageList.ImageSize = new Size(16, 16);

			/*string[] s = Assembly.GetExecutingAssembly().GetManifestResourceNames();
			System.IO.Stream a = Assembly.GetExecutingAssembly().GetManifestResourceStream("ActiveQLibrary.CustomControl.check.bmp");*/

			Image img = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("ActiveQLibrary.CustomControl.check.bmp"));
			Bitmap checkBox = new Bitmap(img,16,16);
			checkBox.MakeTransparent(Color.FromArgb(0, 128, 128));
			checkBoxesImageList.Images.AddStrip(checkBox);
		}
		#endregion

		#region Overrides
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);

			if ( !RecreatingHandle && sortingEnabled )
			{
				ListViewItemSorter = new CompareListItems(this);
			}
					
			// Now that the list control has been created
			// get a hold of the header control so that we can
			// subclass it
			// -- Header control always has a control ID equal zero
			hHeader = WindowsAPI.GetDlgItem(Handle, 0);
			Debug.Assert(hHeader != IntPtr.Zero, "Fail to get Header Control Windows Handle...");
			headerHook = new HeaderHook(this);
			headerHook.AssignHandle(hHeader);

			// Check if we have any ListViewItemEx instance
			// so that we can add the embedded control to the
			// ListView controls collection
            UpdateEmbeddedControls();
		}
	
		protected override  void WndProc(ref Message message)
		{
			base.WndProc(ref message);

			switch (message.Msg)
			{
				case (int)Msg.WM_ERASEBKGND:
					IntPtr hDC = (IntPtr)message.WParam;
					PaintBackground(hDC);
					break;

				case (int)Msg.WM_VSCROLL:
				case (int)Msg.WM_KEYDOWN:
				case (int)Msg.WM_MOUSEWHEEL:
					OnVerticalScroll(ref message);
					break;
				
					// Notification messages come from the header
				case (int)Msg.WM_NOTIFY:
					NMHDR nm1 = (NMHDR) message.GetLParam(typeof(NMHDR));
				switch(nm1.code)
				{
					case (int)NotificationMessages.NM_CUSTOMDRAW:
						NotifyHeaderCustomDraw(ref message); 
						break;
					case (int)HeaderControlNotifications.HDN_BEGINTRACKW:
						Tracking = true;
						break;
					case (int)HeaderControlNotifications.HDN_ENDTRACKW:
						Tracking = false;
						if ( embeddedControlsHashTable.Count > 0 )
							Invalidate();
						// In case we don't received the mouse up message
						// because it is up outside the header client area
						PressedHeaderItem = -1;
						break;
					default:
						break;
				}
					break;
				
					// Reflected Messages come from the list itself
				case (int)ReflectedMessages.OCM_NOTIFY:
					NMHDR nm2 = (NMHDR) message.GetLParam(typeof(NMHDR));	
				switch (nm2.code)
				{
					case (int)NotificationMessages.NM_CUSTOMDRAW:
						NotifyListCustomDraw(ref message); 
						break;
					case (int)ListViewNotifications.LVN_GETDISPINFOW:
						break;
					default:
						break;
				}
					break;
					// Default 
				default:
					break;
			}
		}
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			if ( HideSelection == false )
                Invalidate();
		}
		#endregion

		#region Properties
		public int InitialSortedColumn
		{
			set 
			{ 
				if ( setInitialSortColumn == false )
				{	
					setInitialSortColumn = true;
					lastSortedColumn = value; 
				}
			}
		}

		public ImageList HeaderImageList
		{
			set { headerImageList = value; }
			get { return headerImageList; }
		}

		public Color HeaderColor
		{
			set { headerColor = value; }
			get { return headerColor; }
		}

		public SortOrder SortOrder
		{
			set { sortOrder = value; }
			get { return sortOrder; }
		}
		public bool SortingEnabled
		{
			set { sortingEnabled = value; }
			get { return sortingEnabled; }
		}
		public bool PaintSortedColumnBackground
		{
			set { 
				if ( paintSortedColumnBackground != value )
				{
					paintSortedColumnBackground = value;
					Invalidate(); 
				}
			}
			get { return paintSortedColumnBackground; }
		}
		
		public bool CheckBookLookEnabled
		{
			set { checkBookLookEnabled = value; }
			get { return checkBookLookEnabled; }
		}
		public Color CheckBookOddRowBackColor
		{
			set { checkBookOddRowBackColor = value; }
			get { return checkBookOddRowBackColor; }
		}

		public Color CheckBookOddRowForeColor
		{
			set { checkBookOddRowForeColor = value; }
			get { return checkBookOddRowForeColor; }
		}

		public Color CheckBookEvenRowBackColor
		{
			set { checkBookEvenRowBackColor = value; }
			get { return checkBookEvenRowBackColor; }
		}

		public Color CheckBookEvenRowForeColor
		{
			set { checkBookEvenRowForeColor = value; }
			get { return checkBookEvenRowForeColor; }
		}
	
		#endregion
		
		#region Methods

		public void AddEmbeddedControl(ListViewItem.ListViewSubItem subItem, Control control)
		{
			if ( subItem == null || control == null )
				throw new Exception("Invalid parameter");

			// Add control and associate the control with the ListViewItem hash code
			embeddedControlsHashTable.Add(subItem.GetHashCode(), new ListViewEmbeddedControl(control));
            
		}

		public void RemoveEmbeddedControl(ListViewItem.ListViewSubItem subItem, Control control)
		{
			// Check for a valid parameter
			if ( subItem == null || control == null )
				throw new Exception("Invalid parameter");
		
			// Remove the control associate with this subitem
			embeddedControlsHashTable.Remove(subItem.GetHashCode());
		}

		public void SetHeaderIcon(int headerIndex, int iconIndex)
		{
			// Associate an specific header with an specific image index
			// in the headerImageList
			headerIconsList.Add(new HeaderIconHelper(headerIndex, iconIndex));
		}

		public void SetColumnSortFormat(int columnIndex, SortedListViewFormatType format)
		{
			rowSorterList.Add(new RowSorterHelper(columnIndex, format));
		}

		public void SetColumnSortFormat(int columnIndex, SortedListViewFormatType format, ListSortEvent callBack)
		{
			rowSorterList.Add(new RowSorterHelper(columnIndex, format, callBack));
		}
		
		public Rectangle GetHeaderItemRect(int index)
		{
			RECT rc = new RECT();
			WindowsAPI.SendMessage(hHeader, (int)HeaderControlMessages.HDM_GETITEMRECT, index, ref rc);
			return new Rectangle(rc.left, rc.top, rc.right-rc.left,rc.bottom-rc.top);
		}

		public void EnabledResizeRedraw(bool enable)
		{
			SetStyle(ControlStyles.ResizeRedraw, enable);
		}
		public void UpdateSubItemBackColor(int row, int col, Color background)
		{
			// Get the container item
			ListViewItem item = Items[row];
			Debug.Assert(item != null);
			Debug.Assert(col < item.SubItems.Count);
			ListViewItem.ListViewSubItem subItem = item.SubItems[col];
			Debug.Assert(subItem != null);

			// Call the function that does the actual work
			UpdateSubItem(row, col, background, subItem.ForeColor, subItem.Text, subItem.Font);
		}
		
		public void UpdateSubItemForeColor(int row, int col, Color foreground)
		{
			// Get the container item
			ListViewItem item = Items[row];
			Debug.Assert(item != null);
			Debug.Assert(col < item.SubItems.Count);
			ListViewItem.ListViewSubItem subItem = item.SubItems[col];
			Debug.Assert(subItem != null);

			// Call the function that does the actual work
			UpdateSubItem(row, col, subItem.BackColor, foreground, subItem.Text, subItem.Font);
		}
		
		public void UpdateSubItem(int row, int col, Color background, Color foreground)
		{
			// Get the container item
			ListViewItem item = Items[row];
			Debug.Assert(item != null);
			Debug.Assert(col < item.SubItems.Count);
			ListViewItem.ListViewSubItem subItem = item.SubItems[col];
			Debug.Assert(subItem != null);

			// Call the function that does the actual work
			UpdateSubItem(row, col, background, foreground, subItem.Text, subItem.Font);
            
		}

		public void UpdateSubItem(int row, int col, string text, Font font)
		{
			// Get the container item
			ListViewItem item = Items[row];
			Debug.Assert(item != null);
			Debug.Assert(col < item.SubItems.Count);
			ListViewItem.ListViewSubItem subItem = item.SubItems[col];
			Debug.Assert(subItem != null);

			// Call the function that does the actual work
			UpdateSubItem(row, col, subItem.BackColor, subItem.ForeColor, text, font);
		}

		public void UpdateSubItem(int row, int col, string text)
		{
			// Get the container item
			ListViewItem item = Items[row];
			Debug.Assert(item != null);
			Debug.Assert(col < item.SubItems.Count);
			ListViewItem.ListViewSubItem subItem = item.SubItems[col];
			Debug.Assert(subItem != null);

			// Call the function that does the actual work
			UpdateSubItem(row, col, subItem.BackColor, subItem.ForeColor, text, subItem.Font);
		}

		public void UpdateSubItem(int row, int col, Font font)
		{
			// Get the container item
			ListViewItem item = Items[row];
			Debug.Assert(item != null);
			Debug.Assert(col < item.SubItems.Count);
			ListViewItem.ListViewSubItem subItem = item.SubItems[col];
			Debug.Assert(subItem != null);

			// Call the function that does the actual work
			UpdateSubItem(row, col, subItem.BackColor, subItem.ForeColor, subItem.Text, font);
		}
			
		#endregion

		#region Implementation
		
		#region Properties
		internal int LastSortedColumn
		{
			set { lastSortedColumn = value; Invalidate(); }
			get { return lastSortedColumn; }
		}

		internal bool Tracking
		{
			set { tracking = value; }
			get { return tracking; }
		}

		internal int PressedHeaderItem
		{
			set { pressedHeaderItem = value; }
			get { return pressedHeaderItem; }
		}
        
		internal Point LastMousePosition
		{
			set { lastMousePosition = value; }
			get { return lastMousePosition; }
		}

		#endregion
		
		#region Methods
		void OnVerticalScroll(ref Message m)
		{
            // Scrolling support for embedded controls
			Rectangle viewPort = GetListViewPort();
			foreach ( DictionaryEntry de in embeddedControlsHashTable )
			{
				ListViewEmbeddedControl ec = (ListViewEmbeddedControl)de.Value;
				Rectangle rc = GetSubItemRect(ec.Row, ec.Col);
				if ( rc.Bottom <= viewPort.Top || rc.Top >= viewPort.Bottom )
				{ 
					// Hide the control if it is out of sight
					ec.Control.Visible = false;
				}
			}
		}

		Rectangle GetListViewPort()
		{
			// Calculate the list rectangle only
			// including the part where items are actually drawn
			// That is excluding the Header control area and the 
			// vertical and horizontal scrollbars
			Rectangle rc = ClientRectangle;
			Rectangle headerRect = GetHeaderCtrlRect();
			rc = new Rectangle(rc.Left, rc.Top+headerRect.Height, rc.Width, rc.Height-headerRect.Height);

			// Add the difference between the width of the element and the width
			// of the header control so that we hide item only when fully hidden
			ListViewItem item = TopItem;
			if ( item != null )
			{
				Rectangle firstRow = item.Bounds;
				int gap = Math.Abs(firstRow.Height-firstRow.Top);
				rc = new Rectangle(rc.Left, rc.Top+gap, rc.Width, rc.Height-gap);
			}
			
			return rc;
		}
	
		void UpdateEmbeddedControls()
		{
			foreach (DictionaryEntry de in embeddedControlsHashTable )
			{
				ListViewEmbeddedControl item = (ListViewEmbeddedControl)de.Value;
				Control currentControl = item.Control;
				Debug.Assert(currentControl != null);
                Controls.Add(currentControl);
			}
		}
		void UpdateSubItem(int row, int col, Color background, Color foreground, string text, Font font)
		{

			// This function smartly update the sub item properties without
			// producing the flickering problem that the original ListView displays

			// Lock painting
			WindowsAPI.SendMessage(Handle, Msg.WM_SETREDRAW, 0, 0);
            
			// Get the container item
			ListViewItem item = Items[row];
			Debug.Assert(item != null);
			Debug.Assert(col < item.SubItems.Count);
			ListViewItem.ListViewSubItem subItem = item.SubItems[col];
			Debug.Assert(subItem != null);
			
			// Change the new value of the subitem properties
			subItem.BackColor = background;
			subItem.ForeColor = foreground;
			subItem.Text = text;
			subItem.Font = font;

			// Update the invalid region 
			WindowsAPI.ValidateRect(Handle, IntPtr.Zero);
			
			Rectangle rc = GetSubItemRect(row,col);
			RECT rect = ConversionUtil.RECTFromRectangle(rc);

			// Make the invalid region only the area of the sub item
			WindowsAPI.InvalidateRect(Handle, ref rect, 0);

			// Paint it
			WindowsAPI.SendMessage(Handle, Msg.WM_SETREDRAW, 1, 0);
		}

		void PaintBackground(IntPtr hDC)
		{
			if ( checkBookLookEnabled || SortingEnabled == false || paintSortedColumnBackground == false )
				return;
			
			Graphics g = Graphics.FromHdc(hDC);
		
			if ( lastSortedColumn == -1 )
				return;
			
			// If we don't have the column set yet, don't paint it
			if ( lastSortedColumn > Columns.Count )
				return;

			// If we don't have any items, don't paint background yet
			// since we won't be able to get the right rectangle for
			// the column we are going to paint if there are not items yet
			if ( Items.Count == 0 )
				return;

			Rectangle rc;
			if ( lastSortedColumn != 0 )
			{
				rc = GetSubItemRect(0, lastSortedColumn);
			}
			else
			{
				rc = GetSubItemRect(0, lastSortedColumn);
				Rectangle headerRect = GetHeaderItemRect(lastSortedColumn);
				rc = new Rectangle(headerRect.Left, rc.Top, headerRect.Width, Height);
            }
			using ( Brush b = new SolidBrush(Color.FromArgb(247,247,247)) )
			{
				g.FillRectangle(b, rc.Left, rc.Top, rc.Width, Height);
			}
		}

		bool NotifyListCustomDraw(ref Message m)
		{
			m.Result = (IntPtr)CustomDrawReturnFlags.CDRF_DODEFAULT;
			NMCUSTOMDRAW nmcd = (NMCUSTOMDRAW)m.GetLParam(typeof(NMCUSTOMDRAW));
			IntPtr thisHandle = Handle;
			
			if ( nmcd.hdr.hwndFrom != Handle)
				return false;
			switch (nmcd.dwDrawStage)
			{
				case (int)CustomDrawDrawStateFlags.CDDS_PREPAINT:
					// Ask for Item painting notifications
					m.Result = (IntPtr)CustomDrawReturnFlags.CDRF_NOTIFYITEMDRAW;
					break;
				case (int)CustomDrawDrawStateFlags.CDDS_ITEMPREPAINT:
					m.Result = (IntPtr)CustomDrawReturnFlags.CDRF_NOTIFYSUBITEMDRAW;
					break;
				case (int)(CustomDrawDrawStateFlags.CDDS_ITEMPREPAINT | CustomDrawDrawStateFlags.CDDS_SUBITEM):
					// Draw background
					DoListCustomDrawing(ref m);
					break;
				default:
					break;

			}
			return false;
		}

		void DoListCustomDrawing(ref Message m)
		{
			NMLVCUSTOMDRAW lvcd = (NMLVCUSTOMDRAW)m.GetLParam(typeof(NMLVCUSTOMDRAW));
			int row = (int)lvcd.nmcd.dwItemSpec;
			int physicalCol = lvcd.iSubItem;

			// Get actual item index
			int logicalCol = OrderToIndex(physicalCol);
                       
			// If we don't have any items we must be doing something wrong
			// because the list is only going to request custom drawing of items
			// in the list, if we have items in the list, the Items cannot possibly
			// be zero -- probably the user is using a different thread to update the items
			// instead of serializing the update through the UI Thread
			Debug.Assert(Items.Count != 0);
			ListViewItem lvi = Items[row];
			Rectangle rc;
			if ( logicalCol == 0 && physicalCol != logicalCol )
			{
				rc = GetSubItemRect(row, physicalCol);
				Rectangle headerRect = GetHeaderItemRect(logicalCol);
				rc = new Rectangle(headerRect.Left, rc.Top, headerRect.Width, rc.Height);
			}
			else
				rc = GetSubItemRect(row, logicalCol);

			// Draw the item
			// We did not need to actually paint the items that are not selected
			// but doing all the painting ourselves eliminates some random bugs where
			// the list sometimes did not update a subitem  that was not selected anymore
			// leaving the subitem with a different background color 
			// than the rest of the row
			Graphics g = Graphics.FromHdc(lvcd.nmcd.hdc);

			ListViewItem.ListViewSubItem subItem = null;
			subItem	= Items[row].SubItems[logicalCol];
			Debug.Assert(subItem != null);

			Font  subItemFont = subItem.Font;
			Color subItemBackColor = subItem.BackColor;
			Color subItemForeColor = subItem.ForeColor;
			
         	ListViewEmbeddedControl ec = GetEmbeddedControl(subItem);
			if ( ec != null )
			{
				// just paint the background to cleanup any previous selection
				// Paint this subitem background
				PaintSubItemBackground(g, row, logicalCol, physicalCol, rc, subItemBackColor);

				// We don't need to do anything with the control
				// when it comes to painting as it will paint itself
                // all we need to do is to resize it and exit
				// Put structure back in the message
				Control control = ec.Control;
				control.Bounds = new Rectangle(rc.Left+1, rc.Top+1, rc.Width-2, rc.Height-2);
				
				IListViewEmbeddedControl iec = control as IListViewEmbeddedControl;
				if ( iec != null && Tracking == true)
				{
					// If this control implement fast rendering
					// and we are actually tracking column resizing
					// paint the control using the FastRender method
					control.Visible = false;
					iec.FastRender(g, control.Bounds);
				}
				else
				{
					if ( control.Visible == false )
						control.Visible = true;
				}
				
				// Update row and col
				ec.Row = row;
				ec.Col = physicalCol;

				Marshal.StructureToPtr(lvcd, m.LParam, true);
				m.Result = 	(IntPtr)CustomDrawReturnFlags.CDRF_SKIPDEFAULT;
				return;
			}
		
			// Change foreground color if we need to
			if ( checkBookLookEnabled )
			{
				bool even = (row % 2) == 0;
				Color textColor = Color.Empty;
				if ( even )
					textColor = checkBookEvenRowForeColor;
				else
					textColor = checkBookOddRowForeColor;
				
				if ( textColor == Color.Empty || IsRowSelected(row))
					// Use System Color for odd rows
					textColor = SystemColors.ControlText;
				subItemForeColor = textColor;
			}
				
			// Paint this subitem background
			PaintSubItemBackground(g, row, logicalCol, physicalCol, rc, subItemBackColor);
									
			// Draw Text
			string text = GetSubItemText(row, logicalCol);
			Size textSize = TextUtil.GetTextSize(g, text, Font);
			int gap = 4;
			Point pos = new Point(rc.Left+gap ,rc.Top + ((rc.Height - textSize.Height) / 2));
			
			// I use the Windows API instead of the Graphics object to draw the string
			// because the Graphics object draws ellipes without living blank spaces in between
			// the DrawText API adds those blank spaces in between 
			int ellipsingTringgering = 8;
			
			if ( CheckBoxes && logicalCol == 0 )
			{
				// draw checkbox
				int checkIndex = 0;
				if ( lvi.Checked)
					checkIndex = 1;
				g.DrawImage(checkBoxesImageList.Images[checkIndex], rc.Left + gap, rc.Top);
				pos.X += IMAGE_WIDTH;
				rc.Width = rc.Width - IMAGE_WIDTH; 
			}
			else if ( logicalCol == 0 && CheckBoxes == false && lvi.ImageIndex != -1 && lvi.ImageList != null )
			{
				ImageList imageList = lvi.ImageList;
				Image image = imageList.Images[lvi.ImageIndex];
				if ( image != null )
				{
					g.DrawImage(imageList.Images[lvi.ImageIndex], rc.Left + gap, rc.Top);
					pos.X += IMAGE_WIDTH;
					rc.Width = rc.Width - IMAGE_WIDTH; 
				}
			}

			Rectangle drawRect = new Rectangle(pos.X+2, pos.Y, rc.Width-gap-ellipsingTringgering, rc.Height);
			if ( subItemForeColor != Color.Empty )
			{
				TextUtil.DrawText(g, text, subItemFont, drawRect, subItemForeColor);
			}
			else
			{
				TextUtil.DrawText(g, text, subItemFont, drawRect);
			}
			g.Dispose();
			
			// Put structure back in the message
			Marshal.StructureToPtr(lvcd, m.LParam, true);
			m.Result = 	(IntPtr)CustomDrawReturnFlags.CDRF_SKIPDEFAULT;

		}

		ListViewEmbeddedControl GetEmbeddedControl(ListViewItem.ListViewSubItem subItem)
		{
			int hashCode = subItem.GetHashCode();
			return (ListViewEmbeddedControl)embeddedControlsHashTable[hashCode];
            
		}
		void PaintSubItemBackground(Graphics g, int row, int logicalCol, int physicalCol, Rectangle rc, Color subItemBackColor)
		{
			int lastColumn = Columns.Count;
			int subItemOffset = 2;
			if ( GridLines )
			{
				subItemOffset = 3;
			}
            						
			// Draw Fill Rectangle
			if ( IsRowSelected(row) )
			{
				if ( Focused || checkBookLookEnabled )
				{
					// Control has focus
					Debug.WriteLine("Control has focus...");
					int width = rc.Width;
					if ( physicalCol == lastColumn-1 )
						width = width - 1;
					
					if ( subItemBackColor != SystemColors.Window && checkBookLookEnabled == false )
					{
						using ( Brush b = new SolidBrush(subItemBackColor) )
						{
							g.FillRectangle(b, rc.Left, rc.Top+1, width, rc.Height-subItemOffset);
						}
					}
					else
					{
						using ( Brush b = new SolidBrush(ColorUtil.VSNetSelectionColor) )
						{
							g.FillRectangle(b, rc.Left, rc.Top+1, width, rc.Height-subItemOffset);
						}
					}
				
					// Draw Border
					if ( physicalCol == 0 )
					{
						Color borderColor = ColorUtil.VSNetBorderColor;
						Rectangle rcBorder = GetRowRect(row);
						int heightOffset = 1;
						if ( GridLines )
							heightOffset = 2;
											
						using ( Pen p = new Pen(borderColor) )
						{
							g.DrawRectangle(p, rcBorder.Left+1, rcBorder.Top, rcBorder.Width-2, rcBorder.Height-heightOffset);
						}
					}
				}
				else if ( !HideSelection)
				{
					// If the list does not has the focus
					// but we don't hide the selection
					if ( subItemBackColor != SystemColors.Window && checkBookLookEnabled == false)
					{
						using ( Brush b = new SolidBrush(subItemBackColor) )
						{
							g.FillRectangle(b, rc.Left, rc.Top+1, rc.Width, rc.Height-subItemOffset);
						}
					}
					else
					{
						using ( Brush b = new SolidBrush(ColorUtil.VSNetControlColor) )
						{
							g.FillRectangle(b, rc.Left, rc.Top+1, rc.Width, rc.Height-subItemOffset);
						}
					}
				}
				else 
				{
					// Hide selection but paint subitem if it has a color set
					if ( subItemBackColor != SystemColors.Window)
					{
						using ( Brush b = new SolidBrush(subItemBackColor) )
						{
							g.FillRectangle(b, rc.Left, rc.Top+1, rc.Width, rc.Height-subItemOffset);
						}
					}
				}
			}
			else 
			{
				if ( checkBookLookEnabled )
				{
					bool even = (row % 2) == 0;
					Color rowColor = Color.Empty;
					if ( even )
					{
						rowColor = checkBookEvenRowBackColor;
						if ( rowColor == Color.Empty )
						{
							// Use control color for even rows
							rowColor = ColorUtil.VSNetControlColor;
						}
					}
					else
					{
						rowColor = checkBookOddRowBackColor;
						if ( rowColor == Color.Empty )
						{
							// Use System Color for odd rows
							rowColor = SystemColors.Window;
						}
					}
					
					using ( Brush b = new SolidBrush(rowColor) )
					{
						g.FillRectangle(b, rc.Left, rc.Top, rc.Width, rc.Height);
					}
				}
				else
				{
					if ( logicalCol == lastSortedColumn )
					{
						if ( subItemBackColor != SystemColors.Window)
						{
							using ( Brush b = new SolidBrush(subItemBackColor) )
							{
								g.FillRectangle(b, rc.Left, rc.Top, rc.Width, rc.Height);
							}
						}
						else
						{
							if ( sortingEnabled && paintSortedColumnBackground )
							{
								using ( Brush b = new SolidBrush(Color.FromArgb(247,247,247)) )
								{
									g.FillRectangle(b, rc.Left, rc.Top, rc.Width, rc.Height);
								}
							}
							else
							{
								using ( Brush b = new SolidBrush(SystemColors.Window) )
								{
									g.FillRectangle(b, rc.Left, rc.Top, rc.Width, rc.Height);
								}
							}
						}
					}
					else 
					{
						if ( subItemBackColor != SystemColors.Window)
						{
							using ( Brush b = new SolidBrush(subItemBackColor) )
							{
								g.FillRectangle(b, rc.Left, rc.Top, rc.Width, rc.Height);
							}
						}
						else
						{
							using ( Brush b = new SolidBrush(SystemColors.Window) )
							{
								g.FillRectangle(b, rc.Left, rc.Top, rc.Width, rc.Height);
							}
						}
					}
				}
			}
		}
		
		bool NotifyHeaderCustomDraw(ref Message m)
		{
			m.Result = (IntPtr)CustomDrawReturnFlags.CDRF_DODEFAULT;
			NMCUSTOMDRAW nmcd = (NMCUSTOMDRAW)m.GetLParam(typeof(NMCUSTOMDRAW));
			if ( nmcd.hdr.hwndFrom != hHeader)
				return false;
			
			switch (nmcd.dwDrawStage)
			{
				case (int)CustomDrawDrawStateFlags.CDDS_PREPAINT:
					// Ask for Item painting notifications
					m.Result = (IntPtr)CustomDrawReturnFlags.CDRF_NOTIFYITEMDRAW;
					break;
				case (int)CustomDrawDrawStateFlags.CDDS_ITEMPREPAINT:
					DoHeaderCustomDrawing(ref m);			
					break;
				case (int)NotificationMessages.NM_NCHITTEST:
					break;
				default:
					break;

			}
			return false;
		}

		void DoHeaderCustomDrawing(ref Message m)
		{
			NMCUSTOMDRAW nmcd = (NMCUSTOMDRAW)m.GetLParam(typeof(NMCUSTOMDRAW));
			Graphics g = Graphics.FromHdc(nmcd.hdc);
			
			Rectangle rc = GetHeaderCtrlRect();
			int col = (int)nmcd.dwItemSpec;
			rc = GetHeaderItemRect(col);
				
			int itemRight = rc.Left + rc.Width;
			using ( Brush b = new SolidBrush(SystemColors.ScrollBar) )
			{
				g.FillRectangle(b, rc.Left, rc.Top, rc.Width, rc.Height);
			}
		
			if ( col == PressedHeaderItem && !IsCursorOnDivider() && Tracking == false )
			{
				PressedHeaderItem = -1;
				rc.Inflate(-1, -1);
				using ( Brush b = new SolidBrush(ColorUtil.VSNetPressedColor) )
				{
					g.FillRectangle(b, rc.Left, rc.Top, rc.Width, rc.Height);
				}

			}
			else
			{
				if ( headerColor == Color.Empty )
				{
					ControlPaint.DrawBorder3D(g, rc.Left, rc.Top, rc.Width, 
						rc.Height-1, Border3DStyle.RaisedInner, Border3DSide.All);
				}
				else 
				{
					using ( Pen p = new Pen(ColorUtil.VSNetBorderColor))
					{
						g.DrawRectangle(p, rc.Left, rc.Top, rc.Width, rc.Height);
					}
					using ( Brush b = new SolidBrush(ColorUtil.VSNetControlColor) )
					{																														
						rc.Inflate(-1,-1);
						g.FillRectangle(b, rc.Left, rc.Top, rc.Width, rc.Height);
					}
				}
			}

			string text = GetHeaderItemText(col);
			Size textSize = TextUtil.GetTextSize(g, text, Font);
			int gap = 4;
			Point pos = new Point(rc.Left+gap ,rc.Top + ((rc.Height - textSize.Height) / 2));

			int headerImageIndex;
			if ( headerIconsList != null && HasHeaderImage(col, out headerImageIndex) )
			{
				if ( headerImageIndex != -1 )
				{
					Image image = headerImageList.Images[headerImageIndex];
					if ( image != null )
					{
						int top = (rc.Height - image.Height)/2;
						g.DrawImage(headerImageList.Images[headerImageIndex], rc.Left + gap, top);
						pos.X += IMAGE_WIDTH;
						rc.Width = rc.Width - IMAGE_WIDTH;
					}
				}
			}
			
			// Draw arrow glyph
			if ( sortingEnabled && col == lastSortedColumn)
			{
				int Left = pos.X+2;
				Left += textSize.Width + TEXT_TO_ARROW_GAP;
				Rectangle arrowRect = new Rectangle(Left, rc.Top, ARROW_WIDTH, rc.Height); 
				if ( itemRight >= (Left + ARROW_WIDTH + 4) ) 
				{
					if ( SortOrder == SortOrder.Ascending  || SortOrder == SortOrder.None )
						DrawUpArrow(g, arrowRect);
					else
						DrawDownArrow(g, arrowRect);
				}
			}

			// I use the Windows API instead of the Graphics object to draw the string
			// because the Graphics object draws ellipes without living blank spaces in between
			// the DrawText API adds those blank spaces in between 
			int ellipsingTringgering = 8;
			Rectangle drawRect = new Rectangle(pos.X+2, pos.Y, rc.Width-gap-ellipsingTringgering, rc.Height);
			TextUtil.DrawText(g, text, Font, drawRect);
			g.Dispose();
          				
			m.Result = 	(IntPtr)CustomDrawReturnFlags.CDRF_SKIPDEFAULT;
          
		}

		bool HasHeaderImage(int headerIndex, out int imageIndex)
		{
			imageIndex = -1;
			for ( int i = 0; i < headerIconsList.Count; i++ )
			{
				HeaderIconHelper hih = (HeaderIconHelper)headerIconsList[i];
				if ( hih != null && hih.HeaderIndex == headerIndex )
				{
					imageIndex = hih.IconIndex;
					return true;
				}
			}

			return false;

		}

		void DrawUpArrow(Graphics g, Rectangle rc)
		{
			int xTop = rc.Left + rc.Width/2;
			int yTop = (rc.Height - 6)/2;
            
			int xLeft = xTop - 6;
			int yLeft = yTop + 6;
            
			int xRight = xTop + 6;
			int yRight = yTop + 6;

			using ( Pen p = new Pen(SystemColors.ControlDarkDark))
			{
				g.DrawLine(p, xLeft, yLeft, xTop, yTop);
			}
			using ( Pen p = new Pen(Color.White) )
			{
				g.DrawLine(p, xRight, yRight, xTop, yTop);
			}
			using ( Pen p = new Pen(Color.White) )
			{
				g.DrawLine(p, xLeft, yLeft, xRight, yRight);
			}
		}

		void DrawDownArrow(Graphics g, Rectangle rc)
		{
			int xBottom = rc.Left + rc.Width/2;
			            
			int xLeft = xBottom - 6;
			int yLeft = (rc.Height - 6)/2;;
            
			int xRight = xBottom + 6;
			int yRight = (rc.Height - 6)/2;

			int yBottom = yRight + 6;

			using ( Pen p = new Pen(SystemColors.ControlDarkDark) )
			{
				g.DrawLine(p, xLeft, yLeft, xBottom, yBottom);
			}
			using ( Pen p = new Pen(Color.White) )
			{
				g.DrawLine(p, xRight, yRight, xBottom, yBottom);
			}
			using ( Pen p = new Pen(SystemColors.ControlDarkDark) )
			{
				g.DrawLine(p, xLeft, yLeft, xRight, yRight);
			}

		}
       
		string GetSubItemText(int row, int col)
		{
			// I am going to use the Windows API since using the .NET
			// ListViewSubItem.Text property is causing the nasty side
			// effect of changing the text when I draw the string using TextUtil.DrawText,
			// even though that is not my intention at all.
			// I am not sure about why this is happening but using the API solves the problem
			LVITEM lvi = new LVITEM();
			lvi.iItem = row;
			lvi.mask = ListViewItemFlags.LVIF_TEXT;
			lvi.iSubItem = col;
			lvi.cchTextMax = BUFFER_SIZE;
			lvi.pszText = Marshal.AllocHGlobal(BUFFER_SIZE);
			WindowsAPI.SendMessage(Handle, ListViewMessages.LVM_GETITEMTEXTW, row, ref lvi);
			string text = Marshal.PtrToStringAuto(lvi.pszText);
			return text;
		}

		Rectangle GetSubItemRect(int row, int col)
		{
			RECT rc = new RECT();
			rc.top = col;
			rc.left = (int)SubItemPortion.LVIR_BOUNDS;
			WindowsAPI.SendMessage(Handle, (int)ListViewMessages.LVM_GETSUBITEMRECT,  row, ref rc);
			
			if ( col == 0 )
			{
				// The LVM_GETSUBITEMRECT message does not give us the rectangle for the first subitem
				// since it is not considered a subitem
				// obtain the rectangle for the header control and calculate from there
				Rectangle headerRect = GetHeaderItemRect(col);
				return new Rectangle(rc.left, rc.top, headerRect.Width, rc.bottom-rc.top);
			}
			
			return new Rectangle(rc.left, rc.top, rc.right-rc.left, rc.bottom-rc.top);
		}

		Rectangle GetRowRect(int row)
		{
			RECT rc = new RECT();
			rc.top = 0;
			rc.left = (int)SubItemPortion.LVIR_BOUNDS;
			WindowsAPI.SendMessage(Handle, (int)ListViewMessages.LVM_GETSUBITEMRECT,  row, ref rc);
			return new Rectangle(rc.left, rc.top, rc.right-rc.left, rc.bottom-rc.top);
		}

		bool IsRowSelected(int row)
		{
			Debug.Assert(row >= 0 && row < Items.Count);
			ListViewItem lvi = Items[row];
			return lvi.Selected;
		}

		Rectangle GetHeaderCtrlRect()
		{
			RECT rc = new RECT();
			WindowsAPI.GetClientRect(hHeader, ref rc);
			return new Rectangle(rc.left, rc.top, rc.right-rc.left,rc.bottom-rc.top);
		
		}

		protected bool IsCursorOnDivider()
		{
			HD_HITTESTINFO hti = new HD_HITTESTINFO();
			hti.pt.x = LastMousePosition.X;
			hti.pt.y = LastMousePosition.Y;
			WindowsAPI.SendMessage(hHeader, HeaderControlMessages.HDM_HITTEST, 0, ref hti); 
			bool hit = (hti.flags == HeaderControlHitTestFlags.HHT_ONDIVIDER);
			return hit;
		}

		protected string GetHeaderItemText(int index)
		{
			// I get the bug that I get on the ListView if 
			// I use the columns collection to retreive the text
			// That's why I prefer to use the Windows API

			HDITEM hdi = new HDITEM();
			hdi.mask = HeaderItemFlags.HDI_TEXT;
			hdi.cchTextMax =  BUFFER_SIZE;
			hdi.pszText = Marshal.AllocHGlobal(BUFFER_SIZE);
			WindowsAPI.SendMessage(hHeader, HeaderControlMessages.HDM_GETITEMW, index, ref hdi);
			string text = Marshal.PtrToStringAuto(hdi.pszText);
			return text;
		}

		int IndexToOrder(int index)
		{
			for ( int i = 0; i < Columns.Count; i++ )
			{
				int currentIndex = OrderToIndex(i);
				if ( index == currentIndex )
					return i;
			}

			// Should not get here
			Debug.Assert(false);
			return -1;
		}

		internal RowSorterHelper GetRowSorterHelper()
		{
			for ( int i = 0; i < rowSorterList.Count; i++ )
			{
				RowSorterHelper rs = (RowSorterHelper)rowSorterList[i];
				if ( rs != null && rs.ColumnIndex == LastSortedColumn )
				{
					return rs;
				}
			}
			return null;
		}

		internal int OrderToIndex(int order)
		{
			int result = WindowsAPI.SendMessage(hHeader, HeaderControlMessages.HDM_ORDERTOINDEX, order, 0);
			return result;
		}

		internal void MakeEmbeddedControlsInvisible()
		{
			foreach (DictionaryEntry de in embeddedControlsHashTable )
			{
				ListViewEmbeddedControl item = (ListViewEmbeddedControl)de.Value;
				Control currentControl = item.Control;
				Debug.Assert(currentControl != null);
				currentControl.Visible = false;
				
			}
		}

		#endregion 
		
		#endregion
	}
	
}
