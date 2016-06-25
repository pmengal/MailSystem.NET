using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace ActiveQLibrary.CustomControl
{
	/// <summary>
	/// Windows API Functions
	/// </summary>
	public class WindowsAPI
	{
		public delegate int CompareFunc(IntPtr param1, IntPtr param2, IntPtr sortParam);

		// No need to construct this object
		private WindowsAPI()
		{
		}

		// C# representation of the IMalloc interface.
		[InterfaceType ( ComInterfaceType.InterfaceIsIUnknown ),
			Guid ( "00000002-0000-0000-C000-000000000046" )]
			public interface IMalloc
		{
			[PreserveSig] IntPtr Alloc ( [In] int cb );
			[PreserveSig] IntPtr Realloc ( [In] IntPtr pv, [In] int cb );
			[PreserveSig] void   Free ( [In] IntPtr pv );
			[PreserveSig] int    GetSize ( [In] IntPtr pv );
			[PreserveSig] int    DidAlloc ( IntPtr pv );
			[PreserveSig] void   HeapMinimize ( );
		}

		[DllImport("User32.DLL")]
		public static extern IntPtr GetActiveWindow ( );

		public class Shell32
		{
			// Styles used in the BROWSEINFO.ulFlags field.
			[Flags]    
				public enum BffStyles 
			{
				RestrictToFilesystem = 0x0001, // BIF_RETURNONLYFSDIRS
				RestrictToDomain =     0x0002, // BIF_DONTGOBELOWDOMAIN
				RestrictToSubfolders = 0x0008, // BIF_RETURNFSANCESTORS
				ShowTextBox =          0x0010, // BIF_EDITBOX
				ValidateSelection =    0x0020, // BIF_VALIDATE
				NewDialogStyle =       0x0040, // BIF_NEWDIALOGSTYLE
				BrowseForComputer =    0x1000, // BIF_BROWSEFORCOMPUTER
				BrowseForPrinter =     0x2000, // BIF_BROWSEFORPRINTER
				BrowseForEverything =  0x4000, // BIF_BROWSEINCLUDEFILES
			}

			// Delegate type used in BROWSEINFO.lpfn field.
			public delegate int BFFCALLBACK ( IntPtr hwnd, uint uMsg, IntPtr lParam, IntPtr lpData );

			[StructLayout ( LayoutKind.Sequential, Pack=8 )]
				public struct BROWSEINFO
			{
				public IntPtr       hwndOwner;
				public IntPtr       pidlRoot;
				public IntPtr       pszDisplayName;
				[MarshalAs ( UnmanagedType.LPTStr )]
				public string       lpszTitle;
				public int          ulFlags;
				[MarshalAs ( UnmanagedType.FunctionPtr )]
				public BFFCALLBACK  lpfn;
				public IntPtr       lParam;
				public int          iImage;
			}
		}

		public static Point GetPointFromLPARAM(int lParam)
		{
			return new Point(X_LPARAM(lParam), Y_LPARAM(lParam));
		}

		#region Win32 Macro-Like helpers
		public static int X_LPARAM(int lParam)
		{
			return (lParam & 0xffff);
		}
	 
		public static int Y_LPARAM(int lParam)
		{
			return (lParam >> 16);
		}
		#endregion

		[DllImport("gdi32.dll")]
		static public extern uint GetPixel(IntPtr hDC, int XPos, int YPos);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, Msg msg, int wParam, int lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref RECT lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref POINT lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern void SendMessage(IntPtr hWnd, ToolBarMessages msg, int wParam, ref TBBUTTON lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern void SendMessage(IntPtr hWnd, ToolBarMessages msg, int wParam, ref TBBUTTONINFO lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, RebarMessages msg, int wParam, ref REBARBANDINFO lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern void SendMessage(IntPtr hWnd, TreeViewMessages msg, int wParam, ref TVITEM lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern void SendMessage(IntPtr hWnd, TreeViewMessages msg, int wParam, ref TVINSERTSTRUCT lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern void SendMessage(IntPtr hWnd, TreeViewMessages msg, int wParam, ref TVSORTCB lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, TreeViewMessages msg, int wParam, ref TVHITTESTINFO hti);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern void SendMessage(IntPtr hWnd, ListViewMessages msg, int wParam, ref LVITEM lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern void SendMessage(IntPtr hWnd, HeaderControlMessages msg, int wParam, ref HDITEM lParam);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern void SendMessage(IntPtr hWnd, HeaderControlMessages msg, int wParam, ref HD_HITTESTINFO hti);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, HeaderControlMessages msg, int wParam, int lParam);
		
		[DllImport("gdi32.dll")]
		static public extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

		[DllImport("gdi32.dll")]
		static public extern bool DeleteObject(IntPtr hObject);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public extern static int DrawText(IntPtr hdc, string lpString, int nCount, ref RECT lpRect, int uFormat);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public extern static int DrawText(IntPtr hdc, string lpString, int nCount, ref RECT lpRect, DrawTextFormatFlags flags);

		[DllImport("gdi32")]
		public static extern int SetBkMode(IntPtr hDC, BackgroundMode mode);

		[DllImport ( "Shell32.DLL" )]
		public static extern int SHGetMalloc ( out IMalloc ppMalloc );

		[DllImport ( "Shell32.DLL" )]
		public static extern int SHGetSpecialFolderLocation ( IntPtr hwndOwner, int nFolder, out IntPtr ppidl );

		[DllImport ( "Shell32.DLL" )]
		public static extern int SHGetPathFromIDList ( 	IntPtr pidl, StringBuilder Path );

		[DllImport ( "Shell32.DLL", CharSet=CharSet.Auto )]
		public static extern IntPtr SHBrowseForFolder ( ref Shell32.BROWSEINFO bi );

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern bool GetMenuItemRect(IntPtr hWnd, IntPtr hMenu, uint Item, ref RECT rc);

		[DllImport("gdi32")]
		public static extern uint SetPixel(IntPtr hDC, int x, int y, uint color);

		[DllImport("User32.dll", CharSet=CharSet.Auto)]
		public static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public extern static int GetClientRect(IntPtr hWnd, ref RECT rc);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public extern static IntPtr GetDlgItem(IntPtr hDlg, int nControlID);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public extern static int InvalidateRect(IntPtr hWnd,  ref RECT rc, int bErase);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public extern static int InvalidateRect(IntPtr hWnd,  IntPtr rc, int bErase);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern bool ValidateRect(IntPtr hWnd, ref RECT rcInvalidated);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern bool ValidateRect(IntPtr hWnd, IntPtr rc);

		[DllImport("gdi32")]
		public static extern uint SetTextColor(IntPtr hDC, uint colorRef);

	}

}
