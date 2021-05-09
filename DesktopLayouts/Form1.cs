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
			var title = windowUnderCursor.GetTitle();



			var text = cursorPosition.ToString() + "\n" + title;

			// Console.WriteLine(text);

			label1.Text = text;
		}
	}

}
