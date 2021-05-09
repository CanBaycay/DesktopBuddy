using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace DesktopLayouts.Utilities
{

	internal class WindowApi
	{
		[DllImport("user32.dll")]
		public static extern IntPtr WindowFromPoint(Point p);

		[DllImport("user32.dll", SetLastError=true, CharSet=CharSet.Auto)]
		public static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, [Out] StringBuilder lParam);
	}

	public struct Window
	{
		#region Initialization

		public Window(IntPtr hWnd)
		{
			this.hWnd = hWnd;
		}

		#endregion

		#region Data

		public readonly IntPtr hWnd;

		#endregion

		#region Get Window

		public static Window GetWindowAt(Point position)
		{
			var handler = WindowApi.WindowFromPoint(position);
			// TODO: Error handling.
			return new Window(handler);
		}

		public static Window GetWindowUnderCursor()
		{
			return GetWindowAt(Cursor.Position);
		}

		#endregion

		#region Title

		public string GetTitle()
		{
			// Allocate correct string length first
			int length = WindowApi.GetWindowTextLength(hWnd);
			var sb = new StringBuilder(length + 1);
			WindowApi.GetWindowText(hWnd, sb, sb.Capacity);
			return sb.ToString();
		}

		public string GetTitleRaw()
		{
			// Allocate correct string length first
			int length = (int)WindowApi.SendMessage(hWnd, (uint)WM.GETTEXTLENGTH, IntPtr.Zero, null);
			var sb = new StringBuilder(length + 1);
			WindowApi.SendMessage(hWnd, (uint)WM.GETTEXT, (IntPtr)sb.Capacity, sb);
			return sb.ToString();
		}

		#endregion
	}

}
