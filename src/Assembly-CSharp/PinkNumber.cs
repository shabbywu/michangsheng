using System;

// Token: 0x02000028 RID: 40
public class PinkNumber
{
	// Token: 0x06000362 RID: 866 RVA: 0x00012F40 File Offset: 0x00011140
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

	// Token: 0x06000363 RID: 867 RVA: 0x00012FE4 File Offset: 0x000111E4
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

	// Token: 0x040001F4 RID: 500
	private int max_key;

	// Token: 0x040001F5 RID: 501
	private int key;

	// Token: 0x040001F6 RID: 502
	private uint[] white_values;

	// Token: 0x040001F7 RID: 503
	private uint range;

	// Token: 0x040001F8 RID: 504
	private Random randomGenerator;

	// Token: 0x040001F9 RID: 505
	private float rangeBy5;

	// Token: 0x040001FA RID: 506
	private int last_key;

	// Token: 0x040001FB RID: 507
	private uint sum;

	// Token: 0x040001FC RID: 508
	private int diff;

	// Token: 0x040001FD RID: 509
	private int i;
}
