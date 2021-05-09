using System;
using System.Drawing;

namespace DesktopLayouts.Utilities
{

	public class Cursor
	{
		public static Point Position
		{
			get
			{
				var success = API.GetCursorPos(out var point);
				if (!success)
				{
					Console.WriteLine($"Failed to get cursor position. The resulting point is '{point}'.");
				}
				return point;
			}
		}
	}

}
