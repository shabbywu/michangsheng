using System;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class LTBezier
{
	// Token: 0x0600012E RID: 302 RVA: 0x00060F64 File Offset: 0x0005F164
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

	// Token: 0x0600012F RID: 303 RVA: 0x00061060 File Offset: 0x0005F260
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

	// Token: 0x06000130 RID: 304 RVA: 0x00004F7C File Offset: 0x0000317C
	private Vector3 bezierPoint(float t)
	{
		return ((this.aa * t + this.bb) * t + this.cc) * t + this.a;
	}

	// Token: 0x06000131 RID: 305 RVA: 0x00004FB7 File Offset: 0x000031B7
	public Vector3 point(float t)
	{
		return this.bezierPoint(this.map(t));
	}

	// Token: 0x040000F4 RID: 244
	public float length;

	// Token: 0x040000F5 RID: 245
	private Vector3 a;

	// Token: 0x040000F6 RID: 246
	private Vector3 aa;

	// Token: 0x040000F7 RID: 247
	private Vector3 bb;

	// Token: 0x040000F8 RID: 248
	private Vector3 cc;

	// Token: 0x040000F9 RID: 249
	private float len;

	// Token: 0x040000FA RID: 250
	private float[] arcLengths;
}
