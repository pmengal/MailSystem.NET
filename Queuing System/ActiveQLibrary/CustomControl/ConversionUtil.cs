using System;
using System.Drawing;

namespace ActiveQLibrary.CustomControl
{
	/// <summary>
	/// Summary description for ConversionUtil.
	/// </summary>
	public class ConversionUtil
	{
		#region Constructors
		// No need to create a instance of this class
		private ConversionUtil()
		{
		
		}

		#endregion

		#region Methods
		static public Rectangle RectangleFromRECT(RECT rc)
		{
			return new Rectangle(rc.left, rc.top, rc.right-rc.left, rc.bottom-rc.top);
		}

		static public RECT RECTFromRectangle(Rectangle rc)
		{
			RECT rect = new RECT();
			rect.left = rc.Left;
			rect.top = rc.Top;
			rect.right = rc.Left + rc.Width;
			rect.bottom = rc.Top + rc.Height;
			return rect;
		}

		#endregion
	}
}
