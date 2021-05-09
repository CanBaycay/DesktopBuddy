using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace DesktopLayouts.Utilities
{

	public static class API
	{
		#region Cursor

		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			public int X;
			public int Y;

			public static implicit operator Point(POINT point)
			{
				return new Point(point.X, point.Y);
			}
		}

		[DllImport("user32.dll")]
		public static extern bool GetCursorPos(out POINT lpPoint);

		#endregion

		#region Window

		[DllImport("user32.dll")]
		public static extern IntPtr WindowFromPoint(Point p);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, [Out] StringBuilder lParam);

		#endregion
	}

}
