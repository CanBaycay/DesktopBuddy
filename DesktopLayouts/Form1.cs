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
			Timer.Interval = 1000 / 60;
		}

		private void SetToInfrequentUpdating()
		{
			Timer.Interval = 1000 / 10;
		}

		#endregion

		private Point PreviousCursorPosition;
		private CursorWindowLocation InitialWindowLocation;
		private Window InitialWindow;

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
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
				var resizeDelta = new Point(
					cursorPosition.X - PreviousCursorPosition.X,
					cursorPosition.Y - PreviousCursorPosition.Y);
				PreviousCursorPosition = cursorPosition;

				if (!resizeDelta.IsEmpty)
				{
					ApplySmartResize(InitialWindow, InitialWindowLocation, resizeDelta);
				}
			}
		}

		#region Smart Resize

		private static void ApplySmartResize(Window window, CursorWindowLocation grabLocation, Point resizeDelta)
		{
			window.GetWindowPosition(out var windowPosition);

			switch (grabLocation)
			{
				case CursorWindowLocation.TopLeft:
					windowPosition.X += resizeDelta.X;
					windowPosition.Width -= resizeDelta.X;

					windowPosition.Y += resizeDelta.Y;
					windowPosition.Height -= resizeDelta.Y;
					break;

				case CursorWindowLocation.Top:
					windowPosition.Y += resizeDelta.Y;
					windowPosition.Height -= resizeDelta.Y;
					break;

				case CursorWindowLocation.TopRight:
					windowPosition.Width += resizeDelta.X;

					windowPosition.Y += resizeDelta.Y;
					windowPosition.Height -= resizeDelta.Y;
					break;

				case CursorWindowLocation.Left:
					windowPosition.X += resizeDelta.X;
					windowPosition.Width -= resizeDelta.X;
					break;

				case CursorWindowLocation.Center:
					windowPosition.X += resizeDelta.X;
					windowPosition.Y += resizeDelta.Y;
					break;

				case CursorWindowLocation.Right:
					windowPosition.Width += resizeDelta.X;
					break;

				case CursorWindowLocation.BottomLeft:
					windowPosition.X += resizeDelta.X;
					windowPosition.Width -= resizeDelta.X;

					windowPosition.Height += resizeDelta.Y;
					break;

				case CursorWindowLocation.Bottom:
					windowPosition.Height += resizeDelta.Y;
					break;

				case CursorWindowLocation.BottomRight:
					windowPosition.Width += resizeDelta.X;

					windowPosition.Height += resizeDelta.Y;
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}

			window.SetWindowPosition(windowPosition);
		}

		#endregion

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
