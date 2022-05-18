using System;

// Token: 0x02000033 RID: 51
public class PinkNumber
{
	// Token: 0x06000380 RID: 896 RVA: 0x0006A518 File Offset: 0x00068718
	public PinkNumber()
	{
		this.max_key = 31;
		this.range = 128U;
		this.rangeBy5 = this.range / 5f;
		this.key = 0;
		this.white_values = new uint[5];
		this.randomGenerator = new Random();
		this.i = 0;
		while (this.i < 5)
		{
			this.white_values[this.i] = (uint)(this.randomGenerator.NextDouble() % 1.0 * (double)this.rangeBy5);
			this.i++;
		}
	}

	// Token: 0x06000381 RID: 897 RVA: 0x0006A5BC File Offset: 0x000687BC
	public float getNextValue()
	{
		this.last_key = this.key;
		this.sum = 0U;
		this.key++;
		if (this.key > this.max_key)
		{
			this.key = 0;
		}
		this.diff = (this.last_key ^ this.key);
		this.sum = 0U;
		this.i = 0;
		while (this.i < 5)
		{
			if ((this.diff & 1 << this.i) > 0)
			{
				this.white_values[this.i] = (uint)(this.randomGenerator.NextDouble() % 1.0 * (double)this.rangeBy5);
			}
			this.sum += this.white_values[this.i];
			this.i++;
		}
		return this.sum / 64f - 1f;
	}

	// Token: 0x04000218 RID: 536
	private int max_key;

	// Token: 0x04000219 RID: 537
	private int key;

	// Token: 0x0400021A RID: 538
	private uint[] white_values;

	// Token: 0x0400021B RID: 539
	private uint range;

	// Token: 0x0400021C RID: 540
	private Random randomGenerator;

	// Token: 0x0400021D RID: 541
	private float rangeBy5;

	// Token: 0x0400021E RID: 542
	private int last_key;

	// Token: 0x0400021F RID: 543
	private uint sum;

	// Token: 0x04000220 RID: 544
	private int diff;

	// Token: 0x04000221 RID: 545
	private int i;
}
