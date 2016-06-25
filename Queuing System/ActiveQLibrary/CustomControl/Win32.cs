using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ActiveQLibrary.CustomControl
{
	// Reflected Messages
	#region
	public enum ReflectedMessages
	{
		OCM__BASE				= (Msg.WM_USER+0x1c00),
		OCM_COMMAND				= (OCM__BASE + Msg.WM_COMMAND),
		OCM_CTLCOLORBTN			= (OCM__BASE + Msg.WM_CTLCOLORBTN),
		OCM_CTLCOLOREDIT		= (OCM__BASE + Msg.WM_CTLCOLOREDIT),
		OCM_CTLCOLORDLG			= (OCM__BASE + Msg.WM_CTLCOLORDLG),
		OCM_CTLCOLORLISTBOX		= (OCM__BASE + Msg.WM_CTLCOLORLISTBOX),
		OCM_CTLCOLORMSGBOX		= (OCM__BASE + Msg.WM_CTLCOLORMSGBOX),
		OCM_CTLCOLORSCROLLBAR   = (OCM__BASE + Msg.WM_CTLCOLORSCROLLBAR),
		OCM_CTLCOLORSTATIC		= (OCM__BASE + Msg.WM_CTLCOLORSTATIC),
		OCM_CTLCOLOR			= (OCM__BASE + Msg.WM_CTLCOLOR),
		OCM_DRAWITEM			= (OCM__BASE + Msg.WM_DRAWITEM),
		OCM_MEASUREITEM			= (OCM__BASE + Msg.WM_MEASUREITEM),
		OCM_DELETEITEM          = (OCM__BASE + Msg.WM_DELETEITEM),
		OCM_VKEYTOITEM          = (OCM__BASE + Msg.WM_VKEYTOITEM),
		OCM_CHARTOITEM          = (OCM__BASE + Msg.WM_CHARTOITEM),
		OCM_COMPAREITEM         = (OCM__BASE + Msg.WM_COMPAREITEM),
		OCM_HSCROLL             = (OCM__BASE + Msg.WM_HSCROLL),
		OCM_VSCROLL             = (OCM__BASE + Msg.WM_VSCROLL),
		OCM_PARENTNOTIFY        = (OCM__BASE + Msg.WM_PARENTNOTIFY),
		OCM_NOTIFY              = (OCM__BASE + Msg.WM_NOTIFY)
	}
	#endregion

	// Windows Messages
	#region
	public enum Msg
	{
		WM_NULL                   = 0x0000,
		WM_CREATE                 = 0x0001,
		WM_DESTROY                = 0x0002,
		WM_MOVE                   = 0x0003,
		WM_SIZE                   = 0x0005,
		WM_ACTIVATE               = 0x0006,
		WM_SETFOCUS               = 0x0007,
		WM_KILLFOCUS              = 0x0008,
		WM_ENABLE                 = 0x000A,
		WM_SETREDRAW              = 0x000B,
		WM_SETTEXT                = 0x000C,
		WM_GETTEXT                = 0x000D,
		WM_GETTEXTLENGTH          = 0x000E,
		WM_PAINT                  = 0x000F,
		WM_CLOSE                  = 0x0010,
		WM_QUERYENDSESSION        = 0x0011,
		WM_QUIT                   = 0x0012,
		WM_QUERYOPEN              = 0x0013,
		WM_ERASEBKGND             = 0x0014,
		WM_SYSCOLORCHANGE         = 0x0015,
		WM_ENDSESSION             = 0x0016,
		WM_SHOWWINDOW             = 0x0018,
		WM_CTLCOLOR               = 0x0019,
		WM_WININICHANGE           = 0x001A,
		WM_SETTINGCHANGE          = 0x001A,
		WM_DEVMODECHANGE          = 0x001B,
		WM_ACTIVATEAPP            = 0x001C,
		WM_FONTCHANGE             = 0x001D,
		WM_TIMECHANGE             = 0x001E,
		WM_CANCELMODE             = 0x001F,
		WM_SETCURSOR              = 0x0020,
		WM_MOUSEACTIVATE          = 0x0021,
		WM_CHILDACTIVATE          = 0x0022,
		WM_QUEUESYNC              = 0x0023,
		WM_GETMINMAXINFO          = 0x0024,
		WM_PAINTICON              = 0x0026,
		WM_ICONERASEBKGND         = 0x0027,
		WM_NEXTDLGCTL             = 0x0028,
		WM_SPOOLERSTATUS          = 0x002A,
		WM_DRAWITEM               = 0x002B,
		WM_MEASUREITEM            = 0x002C,
		WM_DELETEITEM             = 0x002D,
		WM_VKEYTOITEM             = 0x002E,
		WM_CHARTOITEM             = 0x002F,
		WM_SETFONT                = 0x0030,
		WM_GETFONT                = 0x0031,
		WM_SETHOTKEY              = 0x0032,
		WM_GETHOTKEY              = 0x0033,
		WM_QUERYDRAGICON          = 0x0037,
		WM_COMPAREITEM            = 0x0039,
		WM_GETOBJECT              = 0x003D,
		WM_COMPACTING             = 0x0041,
		WM_COMMNOTIFY             = 0x0044 ,
		WM_WINDOWPOSCHANGING      = 0x0046,
		WM_WINDOWPOSCHANGED       = 0x0047,
		WM_POWER                  = 0x0048,
		WM_COPYDATA               = 0x004A,
		WM_CANCELJOURNAL          = 0x004B,
		WM_NOTIFY                 = 0x004E,
		WM_INPUTLANGCHANGEREQUEST = 0x0050,
		WM_INPUTLANGCHANGE        = 0x0051,
		WM_TCARD                  = 0x0052,
		WM_HELP                   = 0x0053,
		WM_USERCHANGED            = 0x0054,
		WM_NOTIFYFORMAT           = 0x0055,
		WM_CONTEXTMENU            = 0x007B,
		WM_STYLECHANGING          = 0x007C,
		WM_STYLECHANGED           = 0x007D,
		WM_DISPLAYCHANGE          = 0x007E,
		WM_GETICON                = 0x007F,
		WM_SETICON                = 0x0080,
		WM_NCCREATE               = 0x0081,
		WM_NCDESTROY              = 0x0082,
		WM_NCCALCSIZE             = 0x0083,
		WM_NCHITTEST              = 0x0084,
		WM_NCPAINT                = 0x0085,
		WM_NCACTIVATE             = 0x0086,
		WM_GETDLGCODE             = 0x0087,
		WM_SYNCPAINT              = 0x0088,
		WM_NCMOUSEMOVE            = 0x00A0,
		WM_NCLBUTTONDOWN          = 0x00A1,
		WM_NCLBUTTONUP            = 0x00A2,
		WM_NCLBUTTONDBLCLK        = 0x00A3,
		WM_NCRBUTTONDOWN          = 0x00A4,
		WM_NCRBUTTONUP            = 0x00A5,
		WM_NCRBUTTONDBLCLK        = 0x00A6,
		WM_NCMBUTTONDOWN          = 0x00A7,
		WM_NCMBUTTONUP            = 0x00A8,
		WM_NCMBUTTONDBLCLK        = 0x00A9,
		WM_KEYDOWN                = 0x0100,
		WM_KEYUP                  = 0x0101,
		WM_CHAR                   = 0x0102,
		WM_DEADCHAR               = 0x0103,
		WM_SYSKEYDOWN             = 0x0104,
		WM_SYSKEYUP               = 0x0105,
		WM_SYSCHAR                = 0x0106,
		WM_SYSDEADCHAR            = 0x0107,
		WM_KEYLAST                = 0x0108,
		WM_IME_STARTCOMPOSITION   = 0x010D,
		WM_IME_ENDCOMPOSITION     = 0x010E,
		WM_IME_COMPOSITION        = 0x010F,
		WM_IME_KEYLAST            = 0x010F,
		WM_INITDIALOG             = 0x0110,
		WM_COMMAND                = 0x0111,
		WM_SYSCOMMAND             = 0x0112,
		WM_TIMER                  = 0x0113,
		WM_HSCROLL                = 0x0114,
		WM_VSCROLL                = 0x0115,
		WM_INITMENU               = 0x0116,
		WM_INITMENUPOPUP          = 0x0117,
		WM_MENUSELECT             = 0x011F,
		WM_MENUCHAR               = 0x0120,
		WM_ENTERIDLE              = 0x0121,
		WM_MENURBUTTONUP          = 0x0122,
		WM_MENUDRAG               = 0x0123,
		WM_MENUGETOBJECT          = 0x0124,
		WM_UNINITMENUPOPUP        = 0x0125,
		WM_MENUCOMMAND            = 0x0126,
		WM_CTLCOLORMSGBOX         = 0x0132,
		WM_CTLCOLOREDIT           = 0x0133,
		WM_CTLCOLORLISTBOX        = 0x0134,
		WM_CTLCOLORBTN            = 0x0135,
		WM_CTLCOLORDLG            = 0x0136,
		WM_CTLCOLORSCROLLBAR      = 0x0137,
		WM_CTLCOLORSTATIC         = 0x0138,
		WM_MOUSEMOVE              = 0x0200,
		WM_LBUTTONDOWN            = 0x0201,
		WM_LBUTTONUP              = 0x0202,
		WM_LBUTTONDBLCLK          = 0x0203,
		WM_RBUTTONDOWN            = 0x0204,
		WM_RBUTTONUP              = 0x0205,
		WM_RBUTTONDBLCLK          = 0x0206,
		WM_MBUTTONDOWN            = 0x0207,
		WM_MBUTTONUP              = 0x0208,
		WM_MBUTTONDBLCLK          = 0x0209,
		WM_MOUSEWHEEL             = 0x020A,
		WM_PARENTNOTIFY           = 0x0210,
		WM_ENTERMENULOOP          = 0x0211,
		WM_EXITMENULOOP           = 0x0212,
		WM_NEXTMENU               = 0x0213,
		WM_SIZING                 = 0x0214,
		WM_CAPTURECHANGED         = 0x0215,
		WM_MOVING                 = 0x0216,
		WM_DEVICECHANGE           = 0x0219,
		WM_MDICREATE              = 0x0220,
		WM_MDIDESTROY             = 0x0221,
		WM_MDIACTIVATE            = 0x0222,
		WM_MDIRESTORE             = 0x0223,
		WM_MDINEXT                = 0x0224,
		WM_MDIMAXIMIZE            = 0x0225,
		WM_MDITILE                = 0x0226,
		WM_MDICASCADE             = 0x0227,
		WM_MDIICONARRANGE         = 0x0228,
		WM_MDIGETACTIVE           = 0x0229,
		WM_MDISETMENU             = 0x0230,
		WM_ENTERSIZEMOVE          = 0x0231,
		WM_EXITSIZEMOVE           = 0x0232,
		WM_DROPFILES              = 0x0233,
		WM_MDIREFRESHMENU         = 0x0234,
		WM_IME_SETCONTEXT         = 0x0281,
		WM_IME_NOTIFY             = 0x0282,
		WM_IME_CONTROL            = 0x0283,
		WM_IME_COMPOSITIONFULL    = 0x0284,
		WM_IME_SELECT             = 0x0285,
		WM_IME_CHAR               = 0x0286,
		WM_IME_REQUEST            = 0x0288,
		WM_IME_KEYDOWN            = 0x0290,
		WM_IME_KEYUP              = 0x0291,
		WM_MOUSEHOVER             = 0x02A1,
		WM_MOUSELEAVE             = 0x02A3,
		WM_CUT                    = 0x0300,
		WM_COPY                   = 0x0301,
		WM_PASTE                  = 0x0302,
		WM_CLEAR                  = 0x0303,
		WM_UNDO                   = 0x0304,
		WM_RENDERFORMAT           = 0x0305,
		WM_RENDERALLFORMATS       = 0x0306,
		WM_DESTROYCLIPBOARD       = 0x0307,
		WM_DRAWCLIPBOARD          = 0x0308,
		WM_PAINTCLIPBOARD         = 0x0309,
		WM_VSCROLLCLIPBOARD       = 0x030A,
		WM_SIZECLIPBOARD          = 0x030B,
		WM_ASKCBFORMATNAME        = 0x030C,
		WM_CHANGECBCHAIN          = 0x030D,
		WM_HSCROLLCLIPBOARD       = 0x030E,
		WM_QUERYNEWPALETTE        = 0x030F,
		WM_PALETTEISCHANGING      = 0x0310,
		WM_PALETTECHANGED         = 0x0311,
		WM_HOTKEY                 = 0x0312,
		WM_PRINT                  = 0x0317,
		WM_PRINTCLIENT            = 0x0318,
		WM_HANDHELDFIRST          = 0x0358,
		WM_HANDHELDLAST           = 0x035F,
		WM_AFXFIRST               = 0x0360,
		WM_AFXLAST                = 0x037F,
		WM_PENWINFIRST            = 0x0380,
		WM_PENWINLAST             = 0x038F,
		WM_APP                    = 0x8000,
		WM_USER                   = 0x0400,
		WM_REFLECT                = WM_USER + 0x1c00
	}
		#endregion

	// RECT
	#region
	[StructLayout(LayoutKind.Sequential)]
	public struct RECT
	{
		public int left;
		public int top;
		public int right;
		public int bottom;
	}
	#endregion

	#region Header Control Messages
	public enum HeaderControlMessages : int
	{
		HDM_FIRST        =  0x1200,
		HDM_GETITEMRECT  = (HDM_FIRST + 7),
		HDM_HITTEST      = (HDM_FIRST + 6),
		HDM_SETIMAGELIST = (HDM_FIRST + 8),
		HDM_GETITEMW     = (HDM_FIRST + 11),
		HDM_ORDERTOINDEX = (HDM_FIRST + 15)
	}
	#endregion

	#region HDITEM
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	public struct HDITEM
	{
		public	HeaderItemFlags mask;
		public	int     cxy;
		public	IntPtr  pszText;
		public	IntPtr  hbm;
		public	int     cchTextMax;
		public	int     fmt;
		public	int     lParam;
		public	int     iImage;      
		public	int     iOrder;
	}	
	#endregion

	#region HeaderItem flags
	public enum HeaderItemFlags
	{
		HDI_WIDTH               = 0x0001,
		HDI_HEIGHT              = HDI_WIDTH,
		HDI_TEXT                = 0x0002,
		HDI_FORMAT              = 0x0004,
		HDI_LPARAM              = 0x0008,
		HDI_BITMAP              = 0x0010,
		HDI_IMAGE               = 0x0020,
		HDI_DI_SETITEM          = 0x0040,
		HDI_ORDER               = 0x0080
	}
	#endregion

	// NMTVCUSTOMDRAW
	#region
	[StructLayout(LayoutKind.Sequential)]
	public struct NMTVCUSTOMDRAW 
	{
		public NMCUSTOMDRAW nmcd;
		public uint clrText;
		public uint clrTextBk;
		public int iLevel;
	}
	#endregion

	// NMCUSTOMDRAW
	#region
	[StructLayout(LayoutKind.Sequential)]
	public struct NMCUSTOMDRAW
	{
		public NMHDR hdr;
		public int dwDrawStage;
		public IntPtr hdc;
		public RECT rc;
		public int dwItemSpec;
		public int uItemState;
		public int lItemlParam;
	}
	#endregion

	// NMHDR
	#region
	[StructLayout(LayoutKind.Sequential)]
	public struct NMHDR
	{
		public IntPtr hwndFrom;
		public int idFrom;
		public int code;
	}
	#endregion

	// TVITEM
	#region
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	public struct TVITEM 
	{
		public	uint      mask;
		public	IntPtr    hItem;
		public	uint      state;
		public	uint      stateMask;
		public	IntPtr    pszText;
		public	int       cchTextMax;
		public	int       iImage;
		public	int       iSelectedImage;
		public	int       cChildren;
		public	int       lParam;
	} 
	#endregion

	// Custom Draw Draw State Flags
	#region
	public enum CustomDrawDrawStateFlags
	{
		CDDS_PREPAINT           = 0x00000001,
		CDDS_POSTPAINT          = 0x00000002,
		CDDS_PREERASE           = 0x00000003,
		CDDS_POSTERASE          = 0x00000004,
		CDDS_ITEM               = 0x00010000,
		CDDS_ITEMPREPAINT       = (CDDS_ITEM | CDDS_PREPAINT),
		CDDS_ITEMPOSTPAINT      = (CDDS_ITEM | CDDS_POSTPAINT),
		CDDS_ITEMPREERASE       = (CDDS_ITEM | CDDS_PREERASE),
		CDDS_ITEMPOSTERASE      = (CDDS_ITEM | CDDS_POSTERASE),
		CDDS_SUBITEM            = 0x00020000
	}
	#endregion

	// Custom Draw Return Flags
	#region
	public enum CustomDrawReturnFlags
	{
		CDRF_DODEFAULT          = 0x00000000,
		CDRF_NEWFONT            = 0x00000002,
		CDRF_SKIPDEFAULT        = 0x00000004,
		CDRF_NOTIFYPOSTPAINT    = 0x00000010,
		CDRF_NOTIFYITEMDRAW     = 0x00000020,
		CDRF_NOTIFYSUBITEMDRAW  = 0x00000020, 
		CDRF_NOTIFYPOSTERASE    = 0x00000040
	}
	#endregion

	// Custom Draw Item State Flags
	#region
	public enum CustomDrawItemStateFlags
	{
		CDIS_SELECTED       = 0x0001,
		CDIS_GRAYED         = 0x0002,
		CDIS_DISABLED       = 0x0004,
		CDIS_CHECKED        = 0x0008,
		CDIS_FOCUS          = 0x0010,
		CDIS_DEFAULT        = 0x0020,
		CDIS_HOT            = 0x0040,
		CDIS_MARKED         = 0x0080,
		CDIS_INDETERMINATE  = 0x0100
	}
	#endregion

	// Notification Messages
	#region
	public enum NotificationMessages
	{
		NM_FIRST      = (0-0),
		NM_CUSTOMDRAW = (NM_FIRST-12),
		NM_NCHITTEST  = (NM_FIRST-14) 
	}
	#endregion

	// TreeViewItem Flags
	#region
	public enum TreeViewItemFlags
	{
		TVIF_TEXT               = 0x0001,
		TVIF_IMAGE              = 0x0002,
		TVIF_PARAM              = 0x0004,
		TVIF_STATE              = 0x0008,
		TVIF_HANDLE             = 0x0010,
		TVIF_SELECTEDIMAGE      = 0x0020,
		TVIF_CHILDREN           = 0x0040,
		TVIF_INTEGRAL           = 0x0080
	}
	#endregion

	// TreeView Messages
	#region
	public enum TreeViewMessages
	{
		TV_FIRST        =  0x1100,
		TVM_GETITEMRECT = (TV_FIRST + 4),
		TVM_GETITEMW    = (TV_FIRST + 62)
	}
	#endregion

	#region HD_HITTESTINFO
	[StructLayout(LayoutKind.Sequential)]
	public struct HD_HITTESTINFO 
	{  
		public POINT pt;  
		public HeaderControlHitTestFlags flags; 
		public int iItem; 
	}
	#endregion
	// REBARBANDINFO
	#region
	[StructLayout(LayoutKind.Sequential)]
	public struct REBARBANDINFO
	{
		public int cbSize;
		public int fMask;
		public int fStyle;
		public int clrFore;
		public int clrBack;
		public IntPtr lpText;
		public int cch;
		public int iImage;
		public IntPtr hwndChild;
		public int cxMinChild;
		public int cyMinChild;
		public int cx;
		public IntPtr hbmBack;
		public int wID;
		public int cyChild;
		public int cyMaxChild;
		public int cyIntegral;
		public int cxIdeal;
		public int lParam;
		public int cxHeader;
	}
	#endregion

	// POINT
	#region
	[StructLayout(LayoutKind.Sequential)]
	public struct POINT
	{
		public int x;
		public int y;
	}
	#endregion

	
	// TBBUTTONINFO
	#region
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	public struct TBBUTTONINFO
	{
		public int cbSize;
		public int dwMask;
		public int idCommand;
		public int iImage;
		public byte fsState;
		public byte fsStyle;
		public short cx;
		public IntPtr lParam;
		public IntPtr pszText;
		public int cchText;
	}
	#endregion

	// TBBUTTON
	#region
	[StructLayout(LayoutKind.Sequential, Pack=1)]
	public struct TBBUTTON 
	{
		public int iBitmap;
		public int idCommand;
		public byte fsState;
		public byte fsStyle;
		public byte bReserved0;
		public byte bReserved1;
		public int dwData;
		public int iString;
	}
	#endregion

	// Draw Text format flags
	#region
	public enum DrawTextFormatFlags
	{
		DT_TOP              = 0x00000000,
		DT_LEFT             = 0x00000000,
		DT_CENTER           = 0x00000001,
		DT_RIGHT            = 0x00000002,
		DT_VCENTER          = 0x00000004,
		DT_BOTTOM           = 0x00000008,
		DT_WORDBREAK        = 0x00000010,
		DT_SINGLELINE       = 0x00000020,
		DT_EXPANDTABS       = 0x00000040,
		DT_TABSTOP          = 0x00000080,
		DT_NOCLIP           = 0x00000100,
		DT_EXTERNALLEADING  = 0x00000200,
		DT_CALCRECT         = 0x00000400,
		DT_NOPREFIX         = 0x00000800,
		DT_INTERNAL         = 0x00001000,
		DT_EDITCONTROL      = 0x00002000,
		DT_PATH_ELLIPSIS    = 0x00004000,
		DT_END_ELLIPSIS     = 0x00008000,
		DT_MODIFYSTRING     = 0x00010000,
		DT_RTLREADING       = 0x00020000,
		DT_WORD_ELLIPSIS    = 0x00040000
	}

	#endregion

	// Background Mode
	#region
	public enum BackgroundMode
	{
		TRANSPARENT = 1,
		OPAQUE = 2
	}
	#endregion

	#region ListView Messages
	public enum ListViewMessages
	{
		LVM_FIRST           =    0x1000,
		LVM_GETSUBITEMRECT  = (LVM_FIRST + 56),
		LVM_GETITEMSTATE    = (LVM_FIRST + 44),
		LVM_GETITEMTEXTW    = (LVM_FIRST + 115)
	}
	#endregion

	#region Rebar Messages
	public enum RebarMessages
	{
		CCM_FIRST           =    0x2000,
		WM_USER             =    0x0400,
		RB_INSERTBANDA		=	(WM_USER +  1),
		RB_DELETEBAND       =	(WM_USER +  2),
		RB_GETBARINFO		=	(WM_USER +  3),
		RB_SETBARINFO		=	(WM_USER +  4),
		RB_GETBANDINFO		=	(WM_USER +  5),
		RB_SETBANDINFOA		=	(WM_USER +  6),
		RB_SETPARENT		=	(WM_USER +  7),
		RB_HITTEST			=	(WM_USER +  8),
		RB_GETRECT			=	(WM_USER +  9),
		RB_INSERTBANDW		=	(WM_USER +  10),
		RB_SETBANDINFOW		=	(WM_USER +  11),
		RB_GETBANDCOUNT		=	(WM_USER +  12),
		RB_GETROWCOUNT		=	(WM_USER +  13),
		RB_GETROWHEIGHT		=	(WM_USER +  14),
		RB_IDTOINDEX		=	(WM_USER +  16),
		RB_GETTOOLTIPS		=	(WM_USER +  17),
		RB_SETTOOLTIPS		=	(WM_USER +  18),
		RB_SETBKCOLOR		=	(WM_USER +  19),
		RB_GETBKCOLOR		=	(WM_USER +  20), 
		RB_SETTEXTCOLOR		=	(WM_USER +  21),
		RB_GETTEXTCOLOR		=	(WM_USER +  22),
		RB_SIZETORECT		=	(WM_USER +  23), 
		RB_SETCOLORSCHEME	=	(CCM_FIRST + 2),  
		RB_GETCOLORSCHEME	=	(CCM_FIRST + 3), 
		RB_BEGINDRAG		=	(WM_USER + 24),
		RB_ENDDRAG			=	(WM_USER + 25),
		RB_DRAGMOVE			=	(WM_USER + 26),
		RB_GETBARHEIGHT		=	(WM_USER + 27),
		RB_GETBANDINFOW		=	(WM_USER + 28),
		RB_GETBANDINFOA		=	(WM_USER + 29),
		RB_MINIMIZEBAND		=	(WM_USER + 30),
		RB_MAXIMIZEBAND		=	(WM_USER + 31),
		RB_GETDROPTARGET	=	(CCM_FIRST + 4),
		RB_GETBANDBORDERS	=	(WM_USER + 34),  
		RB_SHOWBAND			=	(WM_USER + 35),      
		RB_SETPALETTE		=	(WM_USER + 37),
		RB_GETPALETTE		=	(WM_USER + 38),
		RB_MOVEBAND			=	(WM_USER + 39),
		RB_SETUNICODEFORMAT =   (CCM_FIRST + 5),
		RB_GETUNICODEFORMAT =   (CCM_FIRST + 6)
	}
	#endregion

	#region LVITEM
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	public struct LVITEM
	{
		public	ListViewItemFlags mask;
		public	int iItem;
		public	int iSubItem;
		public	uint state;
		public	uint stateMask;
		public	IntPtr pszText;
		public	int cchTextMax;
		public	int iImage;
		public	int lParam;
		public	int iIndent;
	}
	#endregion

	#region ListViewItem flags
	public enum ListViewItemFlags
	{
		LVIF_TEXT               = 0x0001,
		LVIF_IMAGE              = 0x0002,
		LVIF_PARAM              = 0x0004,
		LVIF_STATE              = 0x0008,
		LVIF_INDENT             = 0x0010,
		LVIF_NORECOMPUTE        = 0x0800
	}
	#endregion

	#region TVHITTESTINFO
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	public struct TVHITTESTINFO
	{
		public POINT  pt;
		public TreeViewHitTestFlags  flags;
		public IntPtr hItem;
	}
	#endregion

	#region TVINSERTSTRUCT
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	public struct TVINSERTSTRUCT
	{
		public uint hParent;
		public uint hInsertAfter;
		public TVITEM   item;
	}
	#endregion

	#region TVSORTCB
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	public struct TVSORTCB
	{
		public IntPtr hParent;
		public WindowsAPI.CompareFunc lpfnCompare;
		public int lParam;
	}
	#endregion

	#region TreeViewHitTest
	public enum TreeViewHitTestFlags
	{
		TVHT_NOWHERE           = 0x0001,
		TVHT_ONITEMICON        = 0x0002,
		TVHT_ONITEMLABEL       = 0x0004,
		TVHT_ONITEM            = (TVHT_ONITEMICON | TVHT_ONITEMLABEL | TVHT_ONITEMSTATEICON),
		TVHT_ONITEMINDENT      = 0x0008,
		TVHT_ONITEMBUTTON      = 0x0010,
		TVHT_ONITEMRIGHT       = 0x0020,
		TVHT_ONITEMSTATEICON   = 0x0040,
		TVHT_ABOVE             = 0x0100,
		TVHT_BELOW             = 0x0200,
		TVHT_TORIGHT           = 0x0400,
		TVHT_TOLEFT            = 0x0800
	}
	#endregion

	#region NMLVCUSTOMDRAW
	[StructLayout(LayoutKind.Sequential)]
	public struct NMLVCUSTOMDRAW 
	{
		public NMCUSTOMDRAW nmcd;
		public uint clrText;
		public uint clrTextBk;
		public int iSubItem;
	} 
	#endregion
	
	#region ToolBar Messages
	public enum ToolBarMessages
	{
		WM_USER                 =  0x0400,
		TB_ENABLEBUTTON         = (WM_USER + 1),
		TB_CHECKBUTTON          = (WM_USER + 2),
		TB_PRESSBUTTON          = (WM_USER + 3),
		TB_HIDEBUTTON           = (WM_USER + 4),
		TB_INDETERMINATE        = (WM_USER + 5),
		TB_MARKBUTTON           = (WM_USER + 6),
		TB_ISBUTTONENABLED      = (WM_USER + 9),
		TB_ISBUTTONCHECKED      = (WM_USER + 10),
		TB_ISBUTTONPRESSED      = (WM_USER + 11),
		TB_ISBUTTONHIDDEN       = (WM_USER + 12),
		TB_ISBUTTONINDETERMINATE= (WM_USER + 13),
		TB_ISBUTTONHIGHLIGHTED  = (WM_USER + 14),
		TB_SETSTATE             = (WM_USER + 17),
		TB_GETSTATE             = (WM_USER + 18),
		TB_ADDBITMAP            = (WM_USER + 19),
		TB_ADDBUTTONSA          = (WM_USER + 20),
		TB_INSERTBUTTONA        = (WM_USER + 21),
		TB_ADDBUTTONS           = (WM_USER + 20),
		TB_INSERTBUTTON         = (WM_USER + 21),
		TB_DELETEBUTTON         = (WM_USER + 22),
		TB_GETBUTTON            = (WM_USER + 23),
		TB_BUTTONCOUNT          = (WM_USER + 24),
		TB_COMMANDTOINDEX       = (WM_USER + 25),
		TB_SAVERESTOREA         = (WM_USER + 26),
		TB_CUSTOMIZE            = (WM_USER + 27),
		TB_ADDSTRINGA           = (WM_USER + 28),
		TB_GETITEMRECT          = (WM_USER + 29),
		TB_BUTTONSTRUCTSIZE     = (WM_USER + 30),
		TB_SETBUTTONSIZE        = (WM_USER + 31),
		TB_SETBITMAPSIZE        = (WM_USER + 32),
		TB_AUTOSIZE             = (WM_USER + 33),
		TB_GETTOOLTIPS          = (WM_USER + 35),
		TB_SETTOOLTIPS          = (WM_USER + 36),
		TB_SETPARENT            = (WM_USER + 37),
		TB_SETROWS              = (WM_USER + 39),
		TB_GETROWS              = (WM_USER + 40),
		TB_GETBITMAPFLAGS       = (WM_USER + 41),
		TB_SETCMDID             = (WM_USER + 42),
		TB_CHANGEBITMAP         = (WM_USER + 43),
		TB_GETBITMAP            = (WM_USER + 44),
		TB_GETBUTTONTEXTA       = (WM_USER + 45),
		TB_GETBUTTONTEXTW       = (WM_USER + 75),
		TB_REPLACEBITMAP        = (WM_USER + 46),
		TB_SETINDENT            = (WM_USER + 47),
		TB_SETIMAGELIST         = (WM_USER + 48),
		TB_GETIMAGELIST         = (WM_USER + 49),
		TB_LOADIMAGES           = (WM_USER + 50),
		TB_GETRECT              = (WM_USER + 51),
		TB_SETHOTIMAGELIST      = (WM_USER + 52),
		TB_GETHOTIMAGELIST      = (WM_USER + 53),
		TB_SETDISABLEDIMAGELIST = (WM_USER + 54),
		TB_GETDISABLEDIMAGELIST = (WM_USER + 55),
		TB_SETSTYLE             = (WM_USER + 56),
		TB_GETSTYLE             = (WM_USER + 57),
		TB_GETBUTTONSIZE        = (WM_USER + 58),
		TB_SETBUTTONWIDTH       = (WM_USER + 59),
		TB_SETMAXTEXTROWS       = (WM_USER + 60),
		TB_GETTEXTROWS          = (WM_USER + 61),
		TB_GETOBJECT            = (WM_USER + 62), 
		TB_GETBUTTONINFOW       = (WM_USER + 63),
		TB_SETBUTTONINFOW       = (WM_USER + 64),
		TB_GETBUTTONINFOA       = (WM_USER + 65),
		TB_SETBUTTONINFOA       = (WM_USER + 66),
		TB_INSERTBUTTONW        = (WM_USER + 67),
		TB_ADDBUTTONSW          = (WM_USER + 68),
		TB_HITTEST              = (WM_USER + 69),
		TB_SETDRAWTEXTFLAGS     = (WM_USER + 70),
		TB_GETHOTITEM           = (WM_USER + 71),
		TB_SETHOTITEM           = (WM_USER + 72), 
		TB_SETANCHORHIGHLIGHT   = (WM_USER + 73),  
		TB_GETANCHORHIGHLIGHT   = (WM_USER + 74),
		TB_SAVERESTOREW         = (WM_USER + 76),
		TB_ADDSTRINGW           = (WM_USER + 77),
		TB_MAPACCELERATORA      = (WM_USER + 78), 
		TB_GETINSERTMARK        = (WM_USER + 79), 
		TB_SETINSERTMARK        = (WM_USER + 80), 
		TB_INSERTMARKHITTEST    = (WM_USER + 81),  
		TB_MOVEBUTTON           = (WM_USER + 82),
		TB_GETMAXSIZE           = (WM_USER + 83),  
		TB_SETEXTENDEDSTYLE     = (WM_USER + 84),  
		TB_GETEXTENDEDSTYLE     = (WM_USER + 85),  
		TB_GETPADDING           = (WM_USER + 86),
		TB_SETPADDING           = (WM_USER + 87),
		TB_SETINSERTMARKCOLOR   = (WM_USER + 88),
		TB_GETINSERTMARKCOLOR   = (WM_USER + 89)
	}
	#endregion

	#region Header Control HitTest Flags
	public enum HeaderControlHitTestFlags : uint
	{
		HHT_NOWHERE             = 0x0001,
		HHT_ONHEADER            = 0x0002,
		HHT_ONDIVIDER           = 0x0004,
		HHT_ONDIVOPEN           = 0x0008,
		HHT_ABOVE               = 0x0100,
		HHT_BELOW               = 0x0200,
		HHT_TORIGHT             = 0x0400,
		HHT_TOLEFT              = 0x0800
	}
	#endregion

	#region Header Control Notifications
	public enum HeaderControlNotifications
	{
		HDN_FIRST       = (0-300),
		HDN_BEGINTRACKW = (HDN_FIRST-26),
		HDN_ENDTRACKW   = (HDN_FIRST-27),
		HDN_ITEMCLICKW  = (HDN_FIRST-22),
	}
	#endregion

	#region List View Notifications
	public enum ListViewNotifications
	{
		LVN_FIRST             =  (0-100),
		LVN_GETDISPINFOW      = (LVN_FIRST-77),
		LVN_SETDISPINFOA      = (LVN_FIRST-51)
	}
	#endregion

	#region List View sub item portion
	public enum SubItemPortion
	{
		LVIR_BOUNDS = 0,
		LVIR_ICON   = 1,
		LVIR_LABEL  = 2
	}
	#endregion
}
