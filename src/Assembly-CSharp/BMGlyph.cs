using System;
using System.Collections.Generic;

// Token: 0x020000B5 RID: 181
[Serializable]
public class BMGlyph
{
	// Token: 0x060006BC RID: 1724 RVA: 0x00079034 File Offset: 0x00077234
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

	// Token: 0x060006BD RID: 1725 RVA: 0x00079084 File Offset: 0x00077284
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

	// Token: 0x060006BE RID: 1726 RVA: 0x000790F4 File Offset: 0x000772F4
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

	// Token: 0x04000503 RID: 1283
	public int index;

	// Token: 0x04000504 RID: 1284
	public int x;

	// Token: 0x04000505 RID: 1285
	public int y;

	// Token: 0x04000506 RID: 1286
	public int width;

	// Token: 0x04000507 RID: 1287
	public int height;

	// Token: 0x04000508 RID: 1288
	public int offsetX;

	// Token: 0x04000509 RID: 1289
	public int offsetY;

	// Token: 0x0400050A RID: 1290
	public int advance;

	// Token: 0x0400050B RID: 1291
	public int channel;

	// Token: 0x0400050C RID: 1292
	public List<int> kerning;
}
