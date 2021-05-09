using System.Windows.Forms;

namespace DesktopLayouts.Utilities
{

	public static class Keyboard
	{
		public static bool GetKeyState(Keys key)
		{
			return API.GetAsyncKeyState(key) != 0;
		}
	}

}
