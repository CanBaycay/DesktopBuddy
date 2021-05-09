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
		#region Initialization

		public Form1()
		{
			InitializeComponent();
		}

		#endregion

		private bool PreviousKeyState;
		private Point PreviousCursorPosition;
		private CursorWindowLocation InitialWindowLocation;
		private Window InitialWindow;

		private void TestTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			var cursorPosition = Cursor.Position;
			var windowUnderCursor = Window.GetWindowUnderCursor();
			windowUnderCursor.GetTitle(out var title);
			windowUnderCursor.GetWindowPosition(out var windowPosition);
			var keyState = Keyboard.GetKeyState(Keys.LWin) &&
			               Keyboard.GetKeyState(Keys.LControlKey);

			var localCursorPosition = cursorPosition - new Size(windowPosition.X, windowPosition.Y);

			var ratioX = (float)localCursorPosition.X / windowPosition.Width;
			var ratioY = (float)localCursorPosition.Y / windowPosition.Height;

			var x = ratioX < 0.25f ? 0 : ratioX < 0.75f ? 1 : 2;
			var y = ratioY < 0.25f ? 0 : ratioY < 0.75f ? 1 : 2;
			var index = y * 3 + x;
			var cursorWindowLocation = (CursorWindowLocation)index;

			if (PreviousKeyState != keyState)
			{
				PreviousKeyState = keyState;
				PreviousCursorPosition = cursorPosition;
				InitialWindowLocation = cursorWindowLocation;
				InitialWindow = windowUnderCursor;
			}

			if (keyState)
			{
				var diff = new Point(
					cursorPosition.X - PreviousCursorPosition.X,
					cursorPosition.Y - PreviousCursorPosition.Y);

				if (diff.X != 0 || diff.Y != 0)
				{
					switch (InitialWindowLocation)
					{
						case CursorWindowLocation.TopLeft:
							windowPosition.X += diff.X;
							windowPosition.Width -= diff.X;

							windowPosition.Y += diff.Y;
							windowPosition.Height -= diff.Y;
							break;

						case CursorWindowLocation.Top:
							windowPosition.Y += diff.Y;
							windowPosition.Height -= diff.Y;
							break;

						case CursorWindowLocation.TopRight:
							windowPosition.Width += diff.X;

							windowPosition.Y += diff.Y;
							windowPosition.Height -= diff.Y;
							break;

						case CursorWindowLocation.Left:
							windowPosition.X += diff.X;
							windowPosition.Width -= diff.X;
							break;

						case CursorWindowLocation.Center:
							windowPosition.X += diff.X;
							windowPosition.Y += diff.Y;
							break;

						case CursorWindowLocation.Right:
							windowPosition.Width += diff.X;
							break;

						case CursorWindowLocation.BottomLeft:
							windowPosition.X += diff.X;
							windowPosition.Width -= diff.X;

							windowPosition.Height += diff.Y;
							break;

						case CursorWindowLocation.Bottom:
							windowPosition.Height += diff.Y;
							break;

						case CursorWindowLocation.BottomRight:
							windowPosition.Width += diff.X;

							windowPosition.Height += diff.Y;
							break;

						default:
							throw new ArgumentOutOfRangeException();
					}

					InitialWindow.SetWindowPosition(windowPosition);
				}
			}

			PreviousCursorPosition = cursorPosition;

			var text = "CursorPosition : " + cursorPosition + "\n" +
			           "LocalCursorPosition : " + localCursorPosition + "\n" +
			           "X: " + ratioX + "\n" +
			           "Y: " + ratioY + "\n" +
			           cursorWindowLocation + "\n" +
			           title + "\n" +
			           windowPosition + "\n" +
			           keyState + "\n";

			if (keyState)
			{
				text += "\n" + "Applying " + InitialWindowLocation;
			}

			// Console.WriteLine(text);

			label1.Text = text;
		}
	}

}
