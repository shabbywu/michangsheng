using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public class GameWindowTitle
{
	private delegate bool EnumWindowsCallBack(IntPtr hwnd, IntPtr lParam);

	private IntPtr myWindowHandle;

	private static GameWindowTitle instance;

	public static GameWindowTitle Inst
	{
		get
		{
			if (instance == null)
			{
				instance = new GameWindowTitle();
				instance.Init();
			}
			return instance;
		}
	}

	[DllImport("user32", CharSet = CharSet.Unicode)]
	private static extern bool SetWindowTextW(IntPtr hwnd, string title);

	[DllImport("user32")]
	private static extern int EnumWindows(EnumWindowsCallBack lpEnumFunc, IntPtr lParam);

	[DllImport("user32")]
	private static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref IntPtr lpdwProcessId);

	private void Init()
	{
		IntPtr lParam = (IntPtr)Process.GetCurrentProcess().Id;
		EnumWindows(EnumWindCallback, lParam);
	}

	public void SetTitle(string title)
	{
		SetWindowTextW(myWindowHandle, title);
	}

	private bool EnumWindCallback(IntPtr hwnd, IntPtr lParam)
	{
		IntPtr lpdwProcessId = IntPtr.Zero;
		GetWindowThreadProcessId(hwnd, ref lpdwProcessId);
		if (lpdwProcessId == lParam)
		{
			myWindowHandle = hwnd;
			return false;
		}
		return true;
	}
}
