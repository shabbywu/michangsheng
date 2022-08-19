using System;

// Token: 0x020000B0 RID: 176
[Serializable]
public class UISpriteData
{
	// Token: 0x170001BA RID: 442
	// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0003E961 File Offset: 0x0003CB61
	public bool hasBorder
	{
		get
		{
			return (this.borderLeft | this.borderRight | this.borderTop | this.borderBottom) != 0;
		}
	}

	// Token: 0x170001BB RID: 443
	// (get) Token: 0x06000A5C RID: 2652 RVA: 0x0003E981 File Offset: 0x0003CB81
	public bool hasPadding
	{
		get
		{
			return (this.paddingLeft | this.paddingRight | this.paddingTop | this.paddingBottom) != 0;
		}
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x0003E9A1 File Offset: 0x0003CBA1
	public void SetRect(int x, int y, int width, int height)
	{
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x0003E9C0 File Offset: 0x0003CBC0
	public void SetPadding(int left, int bottom, int right, int top)
	{
		this.paddingLeft = left;
		this.paddingBottom = bottom;
		this.paddingRight = right;
		this.paddingTop = top;
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x0003E9DF File Offset: 0x0003CBDF
	public void SetBorder(int left, int bottom, int right, int top)
	{
		this.borderLeft = left;
		this.borderBottom = bottom;
		this.borderRight = right;
		this.borderTop = top;
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x0003EA00 File Offset: 0x0003CC00
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

	// Token: 0x06000A61 RID: 2657 RVA: 0x0003EAA9 File Offset: 0x0003CCA9
	public void CopyBorderFrom(UISpriteData sd)
	{
		this.borderLeft = sd.borderLeft;
		this.borderRight = sd.borderRight;
		this.borderTop = sd.borderTop;
		this.borderBottom = sd.borderBottom;
	}

	// Token: 0x0400064D RID: 1613
	public string name = "Sprite";

	// Token: 0x0400064E RID: 1614
	public int x;

	// Token: 0x0400064F RID: 1615
	public int y;

	// Token: 0x04000650 RID: 1616
	public int width;

	// Token: 0x04000651 RID: 1617
	public int height;

	// Token: 0x04000652 RID: 1618
	public int borderLeft;

	// Token: 0x04000653 RID: 1619
	public int borderRight;

	// Token: 0x04000654 RID: 1620
	public int borderTop;

	// Token: 0x04000655 RID: 1621
	public int borderBottom;

	// Token: 0x04000656 RID: 1622
	public int paddingLeft;

	// Token: 0x04000657 RID: 1623
	public int paddingRight;

	// Token: 0x04000658 RID: 1624
	public int paddingTop;

	// Token: 0x04000659 RID: 1625
	public int paddingBottom;
}
