using System;
using UnityEngine;

// Token: 0x020001D7 RID: 471
public class VirtualScreen : MonoSingleton<VirtualScreen>
{
	// Token: 0x1400002E RID: 46
	// (add) Token: 0x06000F4B RID: 3915 RVA: 0x000A18E0 File Offset: 0x0009FAE0
	// (remove) Token: 0x06000F4C RID: 3916 RVA: 0x000A1914 File Offset: 0x0009FB14
	public static event VirtualScreen.On_ScreenResizeHandler On_ScreenResize;

	// Token: 0x06000F4D RID: 3917 RVA: 0x000A1948 File Offset: 0x0009FB48
	private void Awake()
	{
		this.realWidth = (this.oldRealWidth = (float)Screen.width);
		this.realHeight = (this.oldRealHeight = (float)Screen.height);
		this.ComputeScreen();
	}

	// Token: 0x06000F4E RID: 3918 RVA: 0x000A1988 File Offset: 0x0009FB88
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

	// Token: 0x06000F4F RID: 3919 RVA: 0x000A19F8 File Offset: 0x0009FBF8
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

	// Token: 0x06000F50 RID: 3920 RVA: 0x0000F8F5 File Offset: 0x0000DAF5
	public static void ComputeVirtualScreen()
	{
		MonoSingleton<VirtualScreen>.instance.ComputeScreen();
	}

	// Token: 0x06000F51 RID: 3921 RVA: 0x0000F901 File Offset: 0x0000DB01
	public static void SetGuiScaleMatrix()
	{
		GUI.matrix = Matrix4x4.Scale(new Vector3(VirtualScreen.xRatio, VirtualScreen.yRatio, 1f));
	}

	// Token: 0x06000F52 RID: 3922 RVA: 0x0000F921 File Offset: 0x0000DB21
	public static Rect GetRealRect(Rect rect)
	{
		return new Rect(rect.x * VirtualScreen.xRatio, rect.y * VirtualScreen.yRatio, rect.width * VirtualScreen.xRatio, rect.height * VirtualScreen.yRatio);
	}

	// Token: 0x04000BF8 RID: 3064
	public float virtualWidth = 1024f;

	// Token: 0x04000BF9 RID: 3065
	public float virtualHeight = 768f;

	// Token: 0x04000BFA RID: 3066
	public static float width = 1024f;

	// Token: 0x04000BFB RID: 3067
	public static float height = 768f;

	// Token: 0x04000BFC RID: 3068
	public static float xRatio = 1f;

	// Token: 0x04000BFD RID: 3069
	public static float yRatio = 1f;

	// Token: 0x04000BFE RID: 3070
	private float realWidth;

	// Token: 0x04000BFF RID: 3071
	private float realHeight;

	// Token: 0x04000C00 RID: 3072
	private float oldRealWidth;

	// Token: 0x04000C01 RID: 3073
	private float oldRealHeight;

	// Token: 0x020001D8 RID: 472
	// (Invoke) Token: 0x06000F56 RID: 3926
	public delegate void On_ScreenResizeHandler();

	// Token: 0x020001D9 RID: 473
	public enum ScreenResolution
	{
		// Token: 0x04000C03 RID: 3075
		IPhoneTall,
		// Token: 0x04000C04 RID: 3076
		IPhoneWide,
		// Token: 0x04000C05 RID: 3077
		IPhone4GTall,
		// Token: 0x04000C06 RID: 3078
		IPhone4GWide,
		// Token: 0x04000C07 RID: 3079
		IPadTall,
		// Token: 0x04000C08 RID: 3080
		IPadWide
	}
}
