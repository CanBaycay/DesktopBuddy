using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using DesktopLayouts.Utilities;
using Cursor = System.Windows.Forms.Cursor;

namespace DesktopLayouts
{

	public enum CursorWindowLocation
	{
		TopLeft = 0,
		Top = 1,
		TopRight = 2,

		Left = 3,
		Center = 4,
		Right = 5,

		BottomLeft = 6,
		Bottom = 7,
		BottomRight = 8,
	}

	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void TestTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			var cursorPosition = Cursor.Position;
			var windowUnderCursor = Window.GetWindowUnderCursor();
			windowUnderCursor.GetTitle(out var title);
			windowUnderCursor.GetWindowPosition(out var position);
			var keyState = Keyboard.GetKeyState(Keys.LWin) &&
			               Keyboard.GetKeyState(Keys.LControlKey);

			var localCursorPosition = cursorPosition - new Size(position.X, position.Y);

			var ratioX = (float)localCursorPosition.X / position.Width;
			var ratioY = (float)localCursorPosition.Y / position.Height;

			var x = ratioX < 0.25f ? 0 : ratioX < 0.75f ? 1 : 2;
			var y = ratioY < 0.25f ? 0 : ratioY < 0.75f ? 1 : 2;
			var index = y * 3 + x;
			var cursorWindowLocation = (CursorWindowLocation)index;


			// WM_ENTERSIZEMOVE


			var text = cursorPosition + "\n" +
			           localCursorPosition + "\n" +
			           "X: " + ratioX + "\n" +
			           "Y: " + ratioY + "\n" +
			           cursorWindowLocation + "\n" +
			           title + "\n" +
			           position + "\n" +
			           keyState;

			// Console.WriteLine(text);

			label1.Text = text;
		}
	}

}
