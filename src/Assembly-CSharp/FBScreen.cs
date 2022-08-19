using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000122 RID: 290
public class FBScreen
{
	// Token: 0x170001F6 RID: 502
	// (get) Token: 0x06000DDB RID: 3547 RVA: 0x0005292E File Offset: 0x00050B2E
	// (set) Token: 0x06000DDC RID: 3548 RVA: 0x00052935 File Offset: 0x00050B35
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

	// Token: 0x170001F7 RID: 503
	// (get) Token: 0x06000DDD RID: 3549 RVA: 0x0005293D File Offset: 0x00050B3D
	public static bool Resizable
	{
		get
		{
			return FBScreen.resizable;
		}
	}

	// Token: 0x170001F8 RID: 504
	// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00052944 File Offset: 0x00050B44
	public static int Width
	{
		get
		{
			return Screen.width;
		}
	}

	// Token: 0x170001F9 RID: 505
	// (get) Token: 0x06000DDF RID: 3551 RVA: 0x00051BC8 File Offset: 0x0004FDC8
	public static int Height
	{
		get
		{
			return Screen.height;
		}
	}

	// Token: 0x06000DE0 RID: 3552 RVA: 0x0005294B File Offset: 0x00050B4B
	public static void SetResolution(int width, int height, bool fullscreen, int preferredRefreshRate = 0, params FBScreen.Layout[] layoutParams)
	{
		Screen.SetResolution(width, height, fullscreen, preferredRefreshRate);
	}

	// Token: 0x06000DE1 RID: 3553 RVA: 0x00052956 File Offset: 0x00050B56
	public static void SetAspectRatio(int width, int height, params FBScreen.Layout[] layoutParams)
	{
		Screen.SetResolution(Screen.height / height * width, Screen.height, Screen.fullScreen);
	}

	// Token: 0x06000DE2 RID: 3554 RVA: 0x00004095 File Offset: 0x00002295
	public static void SetUnityPlayerEmbedCSS(string key, string value)
	{
	}

	// Token: 0x06000DE3 RID: 3555 RVA: 0x00052970 File Offset: 0x00050B70
	public static FBScreen.Layout.OptionLeft Left(float amount)
	{
		return new FBScreen.Layout.OptionLeft
		{
			Amount = amount
		};
	}

	// Token: 0x06000DE4 RID: 3556 RVA: 0x0005297E File Offset: 0x00050B7E
	public static FBScreen.Layout.OptionTop Top(float amount)
	{
		return new FBScreen.Layout.OptionTop
		{
			Amount = amount
		};
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x0005298C File Offset: 0x00050B8C
	public static FBScreen.Layout.OptionCenterHorizontal CenterHorizontal()
	{
		return new FBScreen.Layout.OptionCenterHorizontal();
	}

	// Token: 0x06000DE6 RID: 3558 RVA: 0x00052993 File Offset: 0x00050B93
	public static FBScreen.Layout.OptionCenterVertical CenterVertical()
	{
		return new FBScreen.Layout.OptionCenterVertical();
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x00004095 File Offset: 0x00002295
	private static void SetLayout(IEnumerable<FBScreen.Layout> parameters)
	{
	}

	// Token: 0x040009DB RID: 2523
	private static bool resizable;

	// Token: 0x0200128D RID: 4749
	public class Layout
	{
		// Token: 0x02001751 RID: 5969
		public class OptionLeft : FBScreen.Layout
		{
			// Token: 0x0400758F RID: 30095
			public float Amount;
		}

		// Token: 0x02001752 RID: 5970
		public class OptionTop : FBScreen.Layout
		{
			// Token: 0x04007590 RID: 30096
			public float Amount;
		}

		// Token: 0x02001753 RID: 5971
		public class OptionCenterHorizontal : FBScreen.Layout
		{
		}

		// Token: 0x02001754 RID: 5972
		public class OptionCenterVertical : FBScreen.Layout
		{
		}
	}
}
