using System;
using UnityEngine;

// Token: 0x020000DA RID: 218
[Serializable]
public struct SphericalVector
{
	// Token: 0x06000B44 RID: 2884 RVA: 0x00044C5D File Offset: 0x00042E5D
	public SphericalVector(float azimuth, float zenith, float length)
	{
		this.Length = length;
		this.Zenith = zenith;
		this.Azimuth = azimuth;
	}

	// Token: 0x170001D3 RID: 467
	// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00044C74 File Offset: 0x00042E74
	public Vector3 Position
	{
		get
		{
			return this.Length * this.Direction;
		}
	}

	// Token: 0x170001D4 RID: 468
	// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00044C88 File Offset: 0x00042E88
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

	// Token: 0x06000B47 RID: 2887 RVA: 0x00044CE8 File Offset: 0x00042EE8
	public override string ToString()
	{
		return string.Format("Azimuth {0:0.0000} : Zenith {1:0.0000} : Length {2:0.0000}]", this.Azimuth, this.Zenith, this.Length);
	}

	// Token: 0x04000773 RID: 1907
	public float Length;

	// Token: 0x04000774 RID: 1908
	public float Zenith;

	// Token: 0x04000775 RID: 1909
	public float Azimuth;
}
