using System;
using System.Drawing;
using System.Runtime.InteropServices;


namespace DesktopLayouts.Utilities
{

	public class Cursor
	{
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

		public static Point Position
		{
			get
			{
				var success = GetCursorPos(out var point);
				if (!success)
				{
					Console.WriteLine($"Failed to get cursor position. The resulting point is '{point}'.");
				}
				return point;
			}
		}
	}

}
