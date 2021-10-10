using System;
using System.Diagnostics;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using DesktopBuddy.Utilities;
using Microsoft.Win32;
using Cursor = System.Windows.Forms.Cursor;

namespace DesktopBuddy
{

	public enum GrabLocation
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

	public partial class MainForm : Form
	{
		#region Configuration

		public const string ApplicationNameAsKey = "DesktopBuddy";

		#endregion

		#region Initialization

		public MainForm()
		{
			InitializeComponent();
			WindowState = FormWindowState.Minimized;

			SetToInfrequentUpdating();

			LaunchAtStartupCheckbox.Checked = GetStartupRegistry();
		}

		#endregion

		#region Key Press

		public bool IsSmartResizeKeyPressing
		{
			get
			{
				var isPressingLWin = Keyboard.GetKeyState(Keys.LWin);
				// Key combo 1 - Left Control + Left Windows keys
				if (isPressingLWin &&
				    Keyboard.GetKeyState(Keys.LControlKey))
				{
					return true;
				}
				// Key combo 2 - Middle Mouse Button but without any modifier keys
				if (!isPressingLWin &&
				    !Keyboard.GetKeyState(Keys.LControlKey) &&
				    !Keyboard.GetKeyState(Keys.LShiftKey) &&
				    !Keyboard.GetKeyState(Keys.LMenu) &&
				    Keyboard.GetKeyState(Keys.MButton))
				{
					return true;
				}
				return false;
			}
		}

		private bool PreviousKeyState;

		private bool CalculateKeyPress(out bool isSmartResizeKeyPressing,
		                               out bool isSmartResizeKeyDown,
		                               out bool isSmartResizeKeyUp)
		{
			isSmartResizeKeyPressing = IsSmartResizeKeyPressing;

			if (PreviousKeyState != isSmartResizeKeyPressing)
			{
				PreviousKeyState = isSmartResizeKeyPressing;

				isSmartResizeKeyDown = isSmartResizeKeyPressing;
				isSmartResizeKeyUp = !isSmartResizeKeyPressing;
				return true;
			}
			else
			{
				isSmartResizeKeyDown = false;
				isSmartResizeKeyUp = false;
				return isSmartResizeKeyPressing;
			}
		}

		#endregion

		#region Loop Trigger

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			Loop();
		}

		private void SetToFrequentUpdating()
		{
			Timer.Interval = 1000 / 60;
		}

		private void SetToInfrequentUpdating()
		{
			Timer.Interval = 1000 / 10;
		}

		#endregion

		#region Loop

		private Point PreviousCursorPosition;

		private void Loop()
		{
			if (!CalculateKeyPress(out var isSmartResizeKeyPressing,
			                       out var isSmartResizeKeyDown,
			                       out var isSmartResizeKeyUp))
			{
				return;
			}

			if (isSmartResizeKeyDown)
			{
				SetToFrequentUpdating();

				var cursorPosition = Cursor.Position;
				DetectGrabbingDetails(cursorPosition);
				PreviousCursorPosition = cursorPosition;
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
					ApplySmartResize(GrabbedWindow, GrabLocation, resizeDelta);
				}
			}
		}

		#endregion

		#region Grab Window

		private GrabLocation GrabLocation;
		private Window GrabbedWindow;

		private void DetectGrabbingDetails(Point cursorPosition)
		{
			var windowUnderCursor = Window.GetWindowUnderCursor();
			windowUnderCursor.GetWindowPosition(out var windowPosition);

			var localCursorPosition = cursorPosition - new Size(windowPosition.X, windowPosition.Y);

			var ratioX = (float)localCursorPosition.X / windowPosition.Width;
			var ratioY = (float)localCursorPosition.Y / windowPosition.Height;

			var x = ratioX < 0.25f ? 0 : ratioX < 0.75f ? 1 : 2;
			var y = ratioY < 0.25f ? 0 : ratioY < 0.75f ? 1 : 2;
			var index = y * 3 + x;
			var cursorWindowLocation = (GrabLocation)index;

			GrabLocation = cursorWindowLocation;
			GrabbedWindow = windowUnderCursor;
		}

		#endregion

		#region Smart Resize

		private static void ApplySmartResize(Window window, GrabLocation grabLocation, Point resizeDelta)
		{
			window.GetWindowPosition(out var windowPosition);

			switch (grabLocation)
			{
				case GrabLocation.TopLeft:
					windowPosition.X += resizeDelta.X;
					windowPosition.Width -= resizeDelta.X;

					windowPosition.Y += resizeDelta.Y;
					windowPosition.Height -= resizeDelta.Y;
					break;

				case GrabLocation.Top:
					windowPosition.Y += resizeDelta.Y;
					windowPosition.Height -= resizeDelta.Y;
					break;

				case GrabLocation.TopRight:
					windowPosition.Width += resizeDelta.X;

					windowPosition.Y += resizeDelta.Y;
					windowPosition.Height -= resizeDelta.Y;
					break;

				case GrabLocation.Left:
					windowPosition.X += resizeDelta.X;
					windowPosition.Width -= resizeDelta.X;
					break;

				case GrabLocation.Center:
					windowPosition.X += resizeDelta.X;
					windowPosition.Y += resizeDelta.Y;
					break;

				case GrabLocation.Right:
					windowPosition.Width += resizeDelta.X;
					break;

				case GrabLocation.BottomLeft:
					windowPosition.X += resizeDelta.X;
					windowPosition.Width -= resizeDelta.X;

					windowPosition.Height += resizeDelta.Y;
					break;

				case GrabLocation.Bottom:
					windowPosition.Height += resizeDelta.Y;
					break;

				case GrabLocation.BottomRight:
					windowPosition.Width += resizeDelta.X;

					windowPosition.Height += resizeDelta.Y;
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}

			window.SetWindowPosition(windowPosition);
		}

		#endregion

		#region Discord Process Priority Adjuster Tool

		private void DiscordProcessPriorityAdjusterTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			var processes = Process.GetProcessesByName("Discord");
			foreach (var process in processes)
			{
				process.PriorityClass = ProcessPriorityClass.High;
			}
		}

		#endregion

		#region Launch on Startup

		private void LaunchAtStartupCheckbox_CheckedChanged(object sender, EventArgs e)
		{
			SetStartupRegistry(LaunchAtStartupCheckbox.Checked);
		}

		private bool GetStartupRegistry()
		{
			try
			{
				var key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", false);
				return !string.IsNullOrEmpty((string)key.GetValue(ApplicationNameAsKey));
			}
			catch (Exception e)
			{
				return false;
			}
		}

		private void SetStartupRegistry(bool enable)
		{
			var key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

			if (enable)
			{
				key.SetValue(ApplicationNameAsKey, Application.ExecutablePath);
			}
			else
			{
				key.DeleteValue(ApplicationNameAsKey, false);
			}
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
		// 	text += "\n" + "Applying " + GrabLocation;
		// }

		// label1.Text = text;

		#endregion
	}

}
