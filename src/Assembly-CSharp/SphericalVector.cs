using System;
using UnityEngine;

// Token: 0x02000151 RID: 337
[Serializable]
public struct SphericalVector
{
	// Token: 0x06000C33 RID: 3123 RVA: 0x0000E382 File Offset: 0x0000C582
	public SphericalVector(float azimuth, float zenith, float length)
	{
		this.Length = length;
		this.Zenith = zenith;
		this.Azimuth = azimuth;
	}

	// Token: 0x170001EE RID: 494
	// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0000E399 File Offset: 0x0000C599
	public Vector3 Position
	{
		get
		{
			return this.Length * this.Direction;
		}
	}

	// Token: 0x170001EF RID: 495
	// (get) Token: 0x06000C35 RID: 3125 RVA: 0x00096974 File Offset: 0x00094B74
	public Vector3 Direction
	{
		get
		{
			float num = this.Zenith * 3.1415927f / 2f;
			Vector3 result;
			result.y = Mathf.Sin(num);
			float num2 = Mathf.Cos(num);
			float num3 = this.Azimuth * 3.1415927f;
			result.x = num2 * Mathf.Sin(num3);
			result.z = num2 * Mathf.Cos(num3);
			return result;
		}
	}

	// Token: 0x06000C36 RID: 3126 RVA: 0x0000E3AC File Offset: 0x0000C5AC
	public override string ToString()
	{
		return string.Format("Azimuth {0:0.0000} : Zenith {1:0.0000} : Length {2:0.0000}]", this.Azimuth, this.Zenith, this.Length);
	}

	// Token: 0x0400094E RID: 2382
	public float Length;

	// Token: 0x0400094F RID: 2383
	public float Zenith;

	// Token: 0x04000950 RID: 2384
	public float Azimuth;
}
