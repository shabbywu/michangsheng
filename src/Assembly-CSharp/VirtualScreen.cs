using System;
using UnityEngine;

// Token: 0x02000117 RID: 279
public class VirtualScreen : MonoSingleton<VirtualScreen>
{
	// Token: 0x1400002E RID: 46
	// (add) Token: 0x06000D76 RID: 3446 RVA: 0x00050B88 File Offset: 0x0004ED88
	// (remove) Token: 0x06000D77 RID: 3447 RVA: 0x00050BBC File Offset: 0x0004EDBC
	public static event VirtualScreen.On_ScreenResizeHandler On_ScreenResize;

	// Token: 0x06000D78 RID: 3448 RVA: 0x00050BF0 File Offset: 0x0004EDF0
	private void Awake()
	{
		this.realWidth = (this.oldRealWidth = (float)Screen.width);
		this.realHeight = (this.oldRealHeight = (float)Screen.height);
		this.ComputeScreen();
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x00050C30 File Offset: 0x0004EE30
	private void Update()
	{
		this.realWidth = (float)Screen.width;
		this.realHeight = (float)Screen.height;
		if (this.realWidth != this.oldRealWidth || this.realHeight != this.oldRealHeight)
		{
			this.ComputeScreen();
			if (VirtualScreen.On_ScreenResize != null)
			{
				VirtualScreen.On_ScreenResize();
			}
		}
		this.oldRealWidth = this.realWidth;
		this.oldRealHeight = this.realHeight;
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x00050CA0 File Offset: 0x0004EEA0
	public void ComputeScreen()
	{
		VirtualScreen.width = this.virtualWidth;
		VirtualScreen.height = this.virtualHeight;
		VirtualScreen.xRatio = 1f;
		VirtualScreen.yRatio = 1f;
		float num;
		float num2;
		if (Screen.width > Screen.height)
		{
			num = (float)Screen.width / (float)Screen.height;
			num2 = VirtualScreen.width;
		}
		else
		{
			num = (float)Screen.height / (float)Screen.width;
			num2 = VirtualScreen.height;
		}
		float num3 = num2 / num;
		if (Screen.width > Screen.height)
		{
			VirtualScreen.height = num3;
			VirtualScreen.xRatio = (float)Screen.width / VirtualScreen.width;
			VirtualScreen.yRatio = (float)Screen.height / VirtualScreen.height;
			return;
		}
		VirtualScreen.width = num3;
		VirtualScreen.xRatio = (float)Screen.width / VirtualScreen.width;
		VirtualScreen.yRatio = (float)Screen.height / VirtualScreen.height;
	}

	// Token: 0x06000D7B RID: 3451 RVA: 0x00050D82 File Offset: 0x0004EF82
	public static void ComputeVirtualScreen()
	{
		MonoSingleton<VirtualScreen>.instance.ComputeScreen();
	}

	// Token: 0x06000D7C RID: 3452 RVA: 0x00050D8E File Offset: 0x0004EF8E
	public static void SetGuiScaleMatrix()
	{
		GUI.matrix = Matrix4x4.Scale(new Vector3(VirtualScreen.xRatio, VirtualScreen.yRatio, 1f));
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x00050DAE File Offset: 0x0004EFAE
	public static Rect GetRealRect(Rect rect)
	{
		return new Rect(rect.x * VirtualScreen.xRatio, rect.y * VirtualScreen.yRatio, rect.width * VirtualScreen.xRatio, rect.height * VirtualScreen.yRatio);
	}

	// Token: 0x0400097B RID: 2427
	public float virtualWidth = 1024f;

	// Token: 0x0400097C RID: 2428
	public float virtualHeight = 768f;

	// Token: 0x0400097D RID: 2429
	public static float width = 1024f;

	// Token: 0x0400097E RID: 2430
	public static float height = 768f;

	// Token: 0x0400097F RID: 2431
	public static float xRatio = 1f;

	// Token: 0x04000980 RID: 2432
	public static float yRatio = 1f;

	// Token: 0x04000981 RID: 2433
	private float realWidth;

	// Token: 0x04000982 RID: 2434
	private float realHeight;

	// Token: 0x04000983 RID: 2435
	private float oldRealWidth;

	// Token: 0x04000984 RID: 2436
	private float oldRealHeight;

	// Token: 0x02001284 RID: 4740
	// (Invoke) Token: 0x0600798C RID: 31116
	public delegate void On_ScreenResizeHandler();

	// Token: 0x02001285 RID: 4741
	public enum ScreenResolution
	{
		// Token: 0x040065CD RID: 26061
		IPhoneTall,
		// Token: 0x040065CE RID: 26062
		IPhoneWide,
		// Token: 0x040065CF RID: 26063
		IPhone4GTall,
		// Token: 0x040065D0 RID: 26064
		IPhone4GWide,
		// Token: 0x040065D1 RID: 26065
		IPadTall,
		// Token: 0x040065D2 RID: 26066
		IPadWide
	}
}
