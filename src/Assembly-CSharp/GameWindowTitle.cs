using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

// Token: 0x020001BC RID: 444
public class GameWindowTitle
{
	// Token: 0x0600126E RID: 4718
	[DllImport("user32", CharSet = CharSet.Unicode)]
	private static extern bool SetWindowTextW(IntPtr hwnd, string title);

	// Token: 0x0600126F RID: 4719
	[DllImport("user32")]
	private static extern int EnumWindows(GameWindowTitle.EnumWindowsCallBack lpEnumFunc, IntPtr lParam);

	// Token: 0x06001270 RID: 4720
	[DllImport("user32")]
	private static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref IntPtr lpdwProcessId);

	// Token: 0x17000226 RID: 550
	// (get) Token: 0x06001271 RID: 4721 RVA: 0x0007096D File Offset: 0x0006EB6D
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

	// Token: 0x06001272 RID: 4722 RVA: 0x00070990 File Offset: 0x0006EB90
	private void Init()
	{
		IntPtr lParam = (IntPtr)Process.GetCurrentProcess().Id;
		GameWindowTitle.EnumWindows(new GameWindowTitle.EnumWindowsCallBack(this.EnumWindCallback), lParam);
	}

	// Token: 0x06001273 RID: 4723 RVA: 0x000709C0 File Offset: 0x0006EBC0
	public void SetTitle(string title)
	{
		GameWindowTitle.SetWindowTextW(this.myWindowHandle, title);
	}

	// Token: 0x06001274 RID: 4724 RVA: 0x000709D0 File Offset: 0x0006EBD0
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

	// Token: 0x04000D09 RID: 3337
	private IntPtr myWindowHandle;

	// Token: 0x04000D0A RID: 3338
	private static GameWindowTitle instance;

	// Token: 0x020012BE RID: 4798
	// (Invoke) Token: 0x06007A6A RID: 31338
	private delegate bool EnumWindowsCallBack(IntPtr hwnd, IntPtr lParam);
}
