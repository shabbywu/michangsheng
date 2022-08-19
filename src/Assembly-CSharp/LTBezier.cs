using System;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class LTBezier
{
	// Token: 0x06000128 RID: 296 RVA: 0x000074E0 File Offset: 0x000056E0
	public LTBezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float precision)
	{
		this.a = a;
		this.aa = -a + 3f * (b - c) + d;
		this.bb = 3f * (a + c) - 6f * b;
		this.cc = 3f * (b - a);
		this.len = 1f / precision;
		this.arcLengths = new float[(int)this.len + 1];
		this.arcLengths[0] = 0f;
		Vector3 vector = a;
		float num = 0f;
		int num2 = 1;
		while ((float)num2 <= this.len)
		{
			Vector3 vector2 = this.bezierPoint((float)num2 * precision);
			num += (vector - vector2).magnitude;
			this.arcLengths[num2] = num;
			vector = vector2;
			num2++;
		}
		this.length = num;
	}

	// Token: 0x06000129 RID: 297 RVA: 0x000075DC File Offset: 0x000057DC
	private float map(float u)
	{
		float num = u * this.arcLengths[(int)this.len];
		int i = 0;
		int num2 = (int)this.len;
		int num3 = 0;
		while (i < num2)
		{
			num3 = i + ((int)((float)(num2 - i) / 2f) | 0);
			if (this.arcLengths[num3] < num)
			{
				i = num3 + 1;
			}
			else
			{
				num2 = num3;
			}
		}
		if (this.arcLengths[num3] > num)
		{
			num3--;
		}
		if (num3 < 0)
		{
			num3 = 0;
		}
		return ((float)num3 + (num - this.arcLengths[num3]) / (this.arcLengths[num3 + 1] - this.arcLengths[num3])) / this.len;
	}

	// Token: 0x0600012A RID: 298 RVA: 0x0000766C File Offset: 0x0000586C
	private Vector3 bezierPoint(float t)
	{
		return ((this.aa * t + this.bb) * t + this.cc) * t + this.a;
	}

	// Token: 0x0600012B RID: 299 RVA: 0x000076A7 File Offset: 0x000058A7
	public Vector3 point(float t)
	{
		return this.bezierPoint(this.map(t));
	}

	// Token: 0x040000E5 RID: 229
	public float length;

	// Token: 0x040000E6 RID: 230
	private Vector3 a;

	// Token: 0x040000E7 RID: 231
	private Vector3 aa;

	// Token: 0x040000E8 RID: 232
	private Vector3 bb;

	// Token: 0x040000E9 RID: 233
	private Vector3 cc;

	// Token: 0x040000EA RID: 234
	private float len;

	// Token: 0x040000EB RID: 235
	private float[] arcLengths;
}
