using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

// Token: 0x020002BA RID: 698
public class GameWindowTitle
{
	// Token: 0x06001519 RID: 5401
	[DllImport("user32", CharSet = CharSet.Unicode)]
	private static extern bool SetWindowTextW(IntPtr hwnd, string title);

	// Token: 0x0600151A RID: 5402
	[DllImport("user32")]
	private static extern int EnumWindows(GameWindowTitle.EnumWindowsCallBack lpEnumFunc, IntPtr lParam);

	// Token: 0x0600151B RID: 5403
	[DllImport("user32")]
	private static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref IntPtr lpdwProcessId);

	// Token: 0x1700026E RID: 622
	// (get) Token: 0x0600151C RID: 5404 RVA: 0x00013546 File Offset: 0x00011746
	public static GameWindowTitle Inst
	{
		get
		{
			if (GameWindowTitle.instance == null)
			{
				GameWindowTitle.instance = new GameWindowTitle();
				GameWindowTitle.instance.Init();
			}
			return GameWindowTitle.instance;
		}
	}

	// Token: 0x0600151D RID: 5405 RVA: 0x000BE330 File Offset: 0x000BC530
	private void Init()
	{
		IntPtr lParam = (IntPtr)Process.GetCurrentProcess().Id;
		GameWindowTitle.EnumWindows(new GameWindowTitle.EnumWindowsCallBack(this.EnumWindCallback), lParam);
	}

	// Token: 0x0600151E RID: 5406 RVA: 0x00013568 File Offset: 0x00011768
	public void SetTitle(string title)
	{
		GameWindowTitle.SetWindowTextW(this.myWindowHandle, title);
	}

	// Token: 0x0600151F RID: 5407 RVA: 0x000BE360 File Offset: 0x000BC560
	private bool EnumWindCallback(IntPtr hwnd, IntPtr lParam)
	{
		IntPtr zero = IntPtr.Zero;
		GameWindowTitle.GetWindowThreadProcessId(hwnd, ref zero);
		if (zero == lParam)
		{
			this.myWindowHandle = hwnd;
			return false;
		}
		return true;
	}

	// Token: 0x04001031 RID: 4145
	private IntPtr myWindowHandle;

	// Token: 0x04001032 RID: 4146
	private static GameWindowTitle instance;

	// Token: 0x020002BB RID: 699
	// (Invoke) Token: 0x06001522 RID: 5410
	private delegate bool EnumWindowsCallBack(IntPtr hwnd, IntPtr lParam);
}
