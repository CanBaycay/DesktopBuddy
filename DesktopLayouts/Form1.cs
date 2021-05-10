using System;
using System.Drawing;
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

			SetToInfrequentUpdating();
		}

		#endregion

		#region Key Press

		public bool IsSmartResizeKeyPressing
		{
			get
			{
				return Keyboard.GetKeyState(Keys.LWin) &&
				       Keyboard.GetKeyState(Keys.LControlKey);
			}
		}

		private bool PreviousKeyState;

		#endregion

		#region Loop Frequency

		private void SetToFrequentUpdating()
		{
			TestTimer.Interval = 1000 / 60;
		}

		private void SetToInfrequentUpdating()
		{
			TestTimer.Interval = 1000 / 10;
		}

		#endregion

		private Point PreviousCursorPosition;
		private CursorWindowLocation InitialWindowLocation;
		private Window InitialWindow;

		private void TestTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			var isSmartResizeKeyPressing = IsSmartResizeKeyPressing;

			var isSmartResizeKeyDown = false;
			var isSmartResizeKeyUp = false;

			if (PreviousKeyState != isSmartResizeKeyPressing)
			{
				PreviousKeyState = isSmartResizeKeyPressing;

				isSmartResizeKeyDown = isSmartResizeKeyPressing;
				isSmartResizeKeyUp = !isSmartResizeKeyPressing;
			}

			if (isSmartResizeKeyDown)
			{
				SetToFrequentUpdating();

				var windowUnderCursor = Window.GetWindowUnderCursor();
				windowUnderCursor.GetWindowPosition(out var windowPosition);

				var cursorPosition = Cursor.Position;
				var localCursorPosition = cursorPosition - new Size(windowPosition.X, windowPosition.Y);

				var ratioX = (float)localCursorPosition.X / windowPosition.Width;
				var ratioY = (float)localCursorPosition.Y / windowPosition.Height;

				var x = ratioX < 0.25f ? 0 : ratioX < 0.75f ? 1 : 2;
				var y = ratioY < 0.25f ? 0 : ratioY < 0.75f ? 1 : 2;
				var index = y * 3 + x;
				var cursorWindowLocation = (CursorWindowLocation)index;

				PreviousCursorPosition = cursorPosition;
				InitialWindowLocation = cursorWindowLocation;
				InitialWindow = windowUnderCursor;
			}

			if (isSmartResizeKeyUp)
			{
				SetToInfrequentUpdating();
			}

			if (isSmartResizeKeyPressing)
			{
				var cursorPosition = Cursor.Position;
				var delta = new Point(
					cursorPosition.X - PreviousCursorPosition.X,
					cursorPosition.Y - PreviousCursorPosition.Y);
				PreviousCursorPosition = cursorPosition;

				if (delta.X != 0 || delta.Y != 0)
				{
					InitialWindow.GetWindowPosition(out var windowPosition);

					switch (InitialWindowLocation)
					{
						case CursorWindowLocation.TopLeft:
							windowPosition.X += delta.X;
							windowPosition.Width -= delta.X;

							windowPosition.Y += delta.Y;
							windowPosition.Height -= delta.Y;
							break;

						case CursorWindowLocation.Top:
							windowPosition.Y += delta.Y;
							windowPosition.Height -= delta.Y;
							break;

						case CursorWindowLocation.TopRight:
							windowPosition.Width += delta.X;

							windowPosition.Y += delta.Y;
							windowPosition.Height -= delta.Y;
							break;

						case CursorWindowLocation.Left:
							windowPosition.X += delta.X;
							windowPosition.Width -= delta.X;
							break;

						case CursorWindowLocation.Center:
							windowPosition.X += delta.X;
							windowPosition.Y += delta.Y;
							break;

						case CursorWindowLocation.Right:
							windowPosition.Width += delta.X;
							break;

						case CursorWindowLocation.BottomLeft:
							windowPosition.X += delta.X;
							windowPosition.Width -= delta.X;

							windowPosition.Height += delta.Y;
							break;

						case CursorWindowLocation.Bottom:
							windowPosition.Height += delta.Y;
							break;

						case CursorWindowLocation.BottomRight:
							windowPosition.Width += delta.X;

							windowPosition.Height += delta.Y;
							break;

						default:
							throw new ArgumentOutOfRangeException();
					}

					InitialWindow.SetWindowPosition(windowPosition);
				}
			}
		}

		#region Debug

		// windowUnderCursor.GetTitle(out var title);

		// var text = "CursorPosition : " + cursorPosition + "\n" +
		//            "LocalCursorPosition : " + localCursorPosition + "\n" +
		//            "X: " + ratioX + "\n" +
		//            "Y: " + ratioY + "\n" +
		//            cursorWindowLocation + "\n" +
		//            title + "\n" +
		//            windowPosition + "\n" +
		//            keyState + "\n";
		//
		// if (keyState)
		// {
		// 	text += "\n" + "Applying " + InitialWindowLocation;
		// }

		// label1.Text = text;

		#endregion
	}

}
