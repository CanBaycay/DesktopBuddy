using System.Windows.Forms;

namespace DesktopBuddy.Utilities
{

	public static class Keyboard
	{
		public static bool GetKeyState(Keys key)
		{
			return API.GetAsyncKeyState(key) != 0;
		}
	}

}
