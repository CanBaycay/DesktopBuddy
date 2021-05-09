using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace DesktopLayouts.Utilities
{

	public static class API
	{
		#region Keyboard

		[DllImport("user32.dll")]
		public static extern short GetAsyncKeyState(Keys vKey);

		#endregion

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

		public enum GetWindowType : uint
		{
			/// <summary>
			/// The retrieved handle identifies the window of the same type that is highest in the Z order.
			/// <para/>
			/// If the specified window is a topmost window, the handle identifies a topmost window.
			/// If the specified window is a top-level window, the handle identifies a top-level window.
			/// If the specified window is a child window, the handle identifies a sibling window.
			/// </summary>
			GW_HWNDFIRST = 0,
			/// <summary>
			/// The retrieved handle identifies the window of the same type that is lowest in the Z order.
			/// <para />
			/// If the specified window is a topmost window, the handle identifies a topmost window.
			/// If the specified window is a top-level window, the handle identifies a top-level window.
			/// If the specified window is a child window, the handle identifies a sibling window.
			/// </summary>
			GW_HWNDLAST = 1,
			/// <summary>
			/// The retrieved handle identifies the window below the specified window in the Z order.
			/// <para />
			/// If the specified window is a topmost window, the handle identifies a topmost window.
			/// If the specified window is a top-level window, the handle identifies a top-level window.
			/// If the specified window is a child window, the handle identifies a sibling window.
			/// </summary>
			GW_HWNDNEXT = 2,
			/// <summary>
			/// The retrieved handle identifies the window above the specified window in the Z order.
			/// <para />
			/// If the specified window is a topmost window, the handle identifies a topmost window.
			/// If the specified window is a top-level window, the handle identifies a top-level window.
			/// If the specified window is a child window, the handle identifies a sibling window.
			/// </summary>
			GW_HWNDPREV = 3,
			/// <summary>
			/// The retrieved handle identifies the specified window's owner window, if any.
			/// </summary>
			GW_OWNER = 4,
			/// <summary>
			/// The retrieved handle identifies the child window at the top of the Z order,
			/// if the specified window is a parent window; otherwise, the retrieved handle is NULL.
			/// The function examines only child windows of the specified window. It does not examine descendant windows.
			/// </summary>
			GW_CHILD = 5,
			/// <summary>
			/// The retrieved handle identifies the enabled popup window owned by the specified window (the
			/// search uses the first such window found using GW_HWNDNEXT); otherwise, if there are no enabled
			/// popup windows, the retrieved handle is that of the specified window.
			/// </summary>
			GW_ENABLEDPOPUP = 6
		}

		public enum GetAncestorFlags
		{
			/// <summary>
			/// Retrieves the parent window. This does not include the owner, as it does with the GetParent function.
			/// </summary>
			GetParent = 1,
			/// <summary>
			/// Retrieves the root window by walking the chain of parent windows.
			/// </summary>
			GetRoot = 2,
			/// <summary>
			/// Retrieves the owned root window by walking the chain of parent and owner windows returned by GetParent.
			/// </summary>
			GetRootOwner = 3
		}

		public const UInt32 SWP_NOSIZE = 0x0001;
		public const UInt32 SWP_NOMOVE = 0x0002;
		public const UInt32 SWP_NOZORDER = 0x0004;
		public const UInt32 SWP_NOREDRAW = 0x0008;
		public const UInt32 SWP_NOACTIVATE = 0x0010;
		public const UInt32 SWP_FRAMECHANGED = 0x0020;
		public const UInt32 SWP_SHOWWINDOW = 0x0040;
		public const UInt32 SWP_HIDEWINDOW = 0x0080;
		public const UInt32 SWP_NOCOPYBITS = 0x0100;
		public const UInt32 SWP_NOOWNERZORDER = 0x0200;
		public const UInt32 SWP_NOSENDCHANGING = 0x0400;
		public const UInt32 SWP_DRAWFRAME = SWP_FRAMECHANGED;
		public const UInt32 SWP_NOREPOSITION = SWP_NOOWNERZORDER;

		public const UInt32 SWP_DEFERERASE = 0x2000;
		public const UInt32 SWP_ASYNCWINDOWPOS = 0x4000;

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr GetWindow(IntPtr hWnd, GetWindowType uCmd);

		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern IntPtr GetAncestor(IntPtr hWnd, GetAncestorFlags flags);

		[DllImport("user32.dll")]
		public static extern IntPtr WindowFromPoint(Point p);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, [Out] StringBuilder lParam);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int Left; // x position of upper-left corner
			public int Top; // y position of upper-left corner
			public int Right; // x position of lower-right corner
			public int Bottom; // y position of lower-right corner
		}

		[DllImport("user32.dll", EntryPoint = "SetWindowPos")]
		public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, uint wFlags);

		#endregion
	}

}
