using System;
using System.Collections.Generic;

// Token: 0x0200007F RID: 127
[Serializable]
public class BMGlyph
{
	// Token: 0x06000642 RID: 1602 RVA: 0x000236C0 File Offset: 0x000218C0
	public int GetKerning(int previousChar)
	{
		if (this.kerning != null && previousChar != 0)
		{
			int i = 0;
			int count = this.kerning.Count;
			while (i < count)
			{
				if (this.kerning[i] == previousChar)
				{
					return this.kerning[i + 1];
				}
				i += 2;
			}
		}
		return 0;
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x00023710 File Offset: 0x00021910
	public void SetKerning(int previousChar, int amount)
	{
		if (this.kerning == null)
		{
			this.kerning = new List<int>();
		}
		for (int i = 0; i < this.kerning.Count; i += 2)
		{
			if (this.kerning[i] == previousChar)
			{
				this.kerning[i + 1] = amount;
				return;
			}
		}
		this.kerning.Add(previousChar);
		this.kerning.Add(amount);
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x00023780 File Offset: 0x00021980
	public void Trim(int xMin, int yMin, int xMax, int yMax)
	{
		int num = this.x + this.width;
		int num2 = this.y + this.height;
		if (this.x < xMin)
		{
			int num3 = xMin - this.x;
			this.x += num3;
			this.width -= num3;
			this.offsetX += num3;
		}
		if (this.y < yMin)
		{
			int num4 = yMin - this.y;
			this.y += num4;
			this.height -= num4;
			this.offsetY += num4;
		}
		if (num > xMax)
		{
			this.width -= num - xMax;
		}
		if (num2 > yMax)
		{
			this.height -= num2 - yMax;
		}
	}

	// Token: 0x0400042F RID: 1071
	public int index;

	// Token: 0x04000430 RID: 1072
	public int x;

	// Token: 0x04000431 RID: 1073
	public int y;

	// Token: 0x04000432 RID: 1074
	public int width;

	// Token: 0x04000433 RID: 1075
	public int height;

	// Token: 0x04000434 RID: 1076
	public int offsetX;

	// Token: 0x04000435 RID: 1077
	public int offsetY;

	// Token: 0x04000436 RID: 1078
	public int advance;

	// Token: 0x04000437 RID: 1079
	public int channel;

	// Token: 0x04000438 RID: 1080
	public List<int> kerning;
}
