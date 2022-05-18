using System;

// Token: 0x0200011C RID: 284
[Serializable]
public class UISpriteData
{
	// Token: 0x170001D1 RID: 465
	// (get) Token: 0x06000B37 RID: 2871 RVA: 0x0000D3B4 File Offset: 0x0000B5B4
	public bool hasBorder
	{
		get
		{
			return (this.borderLeft | this.borderRight | this.borderTop | this.borderBottom) != 0;
		}
	}

	// Token: 0x170001D2 RID: 466
	// (get) Token: 0x06000B38 RID: 2872 RVA: 0x0000D3D4 File Offset: 0x0000B5D4
	public bool hasPadding
	{
		get
		{
			return (this.paddingLeft | this.paddingRight | this.paddingTop | this.paddingBottom) != 0;
		}
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x0000D3F4 File Offset: 0x0000B5F4
	public void SetRect(int x, int y, int width, int height)
	{
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x0000D413 File Offset: 0x0000B613
	public void SetPadding(int left, int bottom, int right, int top)
	{
		this.paddingLeft = left;
		this.paddingBottom = bottom;
		this.paddingRight = right;
		this.paddingTop = top;
	}

	// Token: 0x06000B3B RID: 2875 RVA: 0x0000D432 File Offset: 0x0000B632
	public void SetBorder(int left, int bottom, int right, int top)
	{
		this.borderLeft = left;
		this.borderBottom = bottom;
		this.borderRight = right;
		this.borderTop = top;
	}

	// Token: 0x06000B3C RID: 2876 RVA: 0x0009117C File Offset: 0x0008F37C
	public void CopyFrom(UISpriteData sd)
	{
		this.name = sd.name;
		this.x = sd.x;
		this.y = sd.y;
		this.width = sd.width;
		this.height = sd.height;
		this.borderLeft = sd.borderLeft;
		this.borderRight = sd.borderRight;
		this.borderTop = sd.borderTop;
		this.borderBottom = sd.borderBottom;
		this.paddingLeft = sd.paddingLeft;
		this.paddingRight = sd.paddingRight;
		this.paddingTop = sd.paddingTop;
		this.paddingBottom = sd.paddingBottom;
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x0000D451 File Offset: 0x0000B651
	public void CopyBorderFrom(UISpriteData sd)
	{
		this.borderLeft = sd.borderLeft;
		this.borderRight = sd.borderRight;
		this.borderTop = sd.borderTop;
		this.borderBottom = sd.borderBottom;
	}

	// Token: 0x040007E4 RID: 2020
	public string name = "Sprite";

	// Token: 0x040007E5 RID: 2021
	public int x;

	// Token: 0x040007E6 RID: 2022
	public int y;

	// Token: 0x040007E7 RID: 2023
	public int width;

	// Token: 0x040007E8 RID: 2024
	public int height;

	// Token: 0x040007E9 RID: 2025
	public int borderLeft;

	// Token: 0x040007EA RID: 2026
	public int borderRight;

	// Token: 0x040007EB RID: 2027
	public int borderTop;

	// Token: 0x040007EC RID: 2028
	public int borderBottom;

	// Token: 0x040007ED RID: 2029
	public int paddingLeft;

	// Token: 0x040007EE RID: 2030
	public int paddingRight;

	// Token: 0x040007EF RID: 2031
	public int paddingTop;

	// Token: 0x040007F0 RID: 2032
	public int paddingBottom;
}
