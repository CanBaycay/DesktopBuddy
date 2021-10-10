using System;
using System.Drawing;
using System.Text;

namespace DesktopBuddy.Utilities
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

			var parentWindow = API.GetAncestor(handler, API.GetAncestorFlags.GetRoot);
			// TODO: Error handling.

			return new Window(parentWindow);
		}

		public static Window GetWindowUnderCursor()
		{
			return GetWindowAt(Cursor.Position);
		}

		#endregion

		#region Title

		public bool GetTitle(out string title)
		{
			var length = API.GetWindowTextLength(hWnd);
			var stringBuilder = new StringBuilder(length + 1);
			if (API.GetWindowText(hWnd, stringBuilder, stringBuilder.Capacity) != 0)
			{
				title = stringBuilder.ToString();
				return true;
			}
			title = string.Empty;
			return false;
		}

		// public string GetTitleRaw()
		// {
		// 	var length = (int)API.SendMessage(hWnd, (uint)WM.GETTEXTLENGTH, IntPtr.Zero, null);
		// 	var stringBuilder = new StringBuilder(length + 1);
		// 	API.SendMessage(hWnd, (uint)WM.GETTEXT, (IntPtr)stringBuilder.Capacity, stringBuilder);
		// 	return stringBuilder.ToString();
		// }

		#endregion

		#region Get/Set Window Pos

		public bool GetWindowPosition(out Rectangle rectangle)
		{
			if (API.GetWindowRect(hWnd, out var rect))
			{
				rectangle = new Rectangle(rect.Left,
				                          rect.Top,
				                          rect.Right - rect.Left,
				                          rect.Bottom - rect.Top);
				return true;
			}
			rectangle = default(Rectangle);
			return false;
		}

		public bool SetWindowPosition(Rectangle rectangle)
		{
			return API.SetWindowPos(hWnd, 0,
			                        rectangle.X, rectangle.Y,
			                        rectangle.Width, rectangle.Height,
			                        // API.SWP_ASYNCWINDOWPOS |
			                        API.SWP_SHOWWINDOW |
			                        API.SWP_NOACTIVATE |
			                        API.SWP_NOOWNERZORDER) != 0;
		}

		#endregion
	}

}
