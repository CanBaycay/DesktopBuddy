using System;
using System.Drawing;
using System.Text;

namespace DesktopLayouts.Utilities
{

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
			var handler = API.WindowFromPoint(position);
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
			int length = API.GetWindowTextLength(hWnd);
			var sb = new StringBuilder(length + 1);
			API.GetWindowText(hWnd, sb, sb.Capacity);
			return sb.ToString();
		}

		public string GetTitleRaw()
		{
			// Allocate correct string length first
			int length = (int)API.SendMessage(hWnd, (uint)WM.GETTEXTLENGTH, IntPtr.Zero, null);
			var sb = new StringBuilder(length + 1);
			API.SendMessage(hWnd, (uint)WM.GETTEXT, (IntPtr)sb.Capacity, sb);
			return sb.ToString();
		}

		#endregion
	}

}
