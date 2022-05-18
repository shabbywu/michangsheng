using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001EE RID: 494
public class FBScreen
{
	// Token: 0x1700022B RID: 555
	// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x0000FF85 File Offset: 0x0000E185
	// (set) Token: 0x06000FE5 RID: 4069 RVA: 0x0000FF8C File Offset: 0x0000E18C
	public static bool FullScreen
	{
		get
		{
			return Screen.fullScreen;
		}
		set
		{
			Screen.fullScreen = value;
		}
	}

	// Token: 0x1700022C RID: 556
	// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x0000FF94 File Offset: 0x0000E194
	public static bool Resizable
	{
		get
		{
			return FBScreen.resizable;
		}
	}

	// Token: 0x1700022D RID: 557
	// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x0000FF9B File Offset: 0x0000E19B
	public static int Width
	{
		get
		{
			return Screen.width;
		}
	}

	// Token: 0x1700022E RID: 558
	// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x0000FD0E File Offset: 0x0000DF0E
	public static int Height
	{
		get
		{
			return Screen.height;
		}
	}

	// Token: 0x06000FE9 RID: 4073 RVA: 0x0000FFA2 File Offset: 0x0000E1A2
	public static void SetResolution(int width, int height, bool fullscreen, int preferredRefreshRate = 0, params FBScreen.Layout[] layoutParams)
	{
		Screen.SetResolution(width, height, fullscreen, preferredRefreshRate);
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x0000FFAD File Offset: 0x0000E1AD
	public static void SetAspectRatio(int width, int height, params FBScreen.Layout[] layoutParams)
	{
		Screen.SetResolution(Screen.height / height * width, Screen.height, Screen.fullScreen);
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x000042DD File Offset: 0x000024DD
	public static void SetUnityPlayerEmbedCSS(string key, string value)
	{
	}

	// Token: 0x06000FEC RID: 4076 RVA: 0x0000FFC7 File Offset: 0x0000E1C7
	public static FBScreen.Layout.OptionLeft Left(float amount)
	{
		return new FBScreen.Layout.OptionLeft
		{
			Amount = amount
		};
	}

	// Token: 0x06000FED RID: 4077 RVA: 0x0000FFD5 File Offset: 0x0000E1D5
	public static FBScreen.Layout.OptionTop Top(float amount)
	{
		return new FBScreen.Layout.OptionTop
		{
			Amount = amount
		};
	}

	// Token: 0x06000FEE RID: 4078 RVA: 0x0000FFE3 File Offset: 0x0000E1E3
	public static FBScreen.Layout.OptionCenterHorizontal CenterHorizontal()
	{
		return new FBScreen.Layout.OptionCenterHorizontal();
	}

	// Token: 0x06000FEF RID: 4079 RVA: 0x0000FFEA File Offset: 0x0000E1EA
	public static FBScreen.Layout.OptionCenterVertical CenterVertical()
	{
		return new FBScreen.Layout.OptionCenterVertical();
	}

	// Token: 0x06000FF0 RID: 4080 RVA: 0x000042DD File Offset: 0x000024DD
	private static void SetLayout(IEnumerable<FBScreen.Layout> parameters)
	{
	}

	// Token: 0x04000C71 RID: 3185
	private static bool resizable;

	// Token: 0x020001EF RID: 495
	public class Layout
	{
		// Token: 0x020001F0 RID: 496
		public class OptionLeft : FBScreen.Layout
		{
			// Token: 0x04000C72 RID: 3186
			public float Amount;
		}

		// Token: 0x020001F1 RID: 497
		public class OptionTop : FBScreen.Layout
		{
			// Token: 0x04000C73 RID: 3187
			public float Amount;
		}

		// Token: 0x020001F2 RID: 498
		public class OptionCenterHorizontal : FBScreen.Layout
		{
		}

		// Token: 0x020001F3 RID: 499
		public class OptionCenterVertical : FBScreen.Layout
		{
		}
	}
}
