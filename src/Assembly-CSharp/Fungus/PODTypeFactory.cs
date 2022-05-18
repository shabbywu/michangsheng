using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x020013AB RID: 5035
	public static class PODTypeFactory
	{
		// Token: 0x060079F4 RID: 31220 RVA: 0x00053313 File Offset: 0x00051513
		public static Color color(float r, float g, float b, float a)
		{
			return new Color(r, g, b, a);
		}

		// Token: 0x060079F5 RID: 31221 RVA: 0x0005331E File Offset: 0x0005151E
		public static Vector2 vector2(float x, float y)
		{
			return new Vector2(x, y);
		}

		// Token: 0x060079F6 RID: 31222 RVA: 0x00053327 File Offset: 0x00051527
		public static Vector3 vector3(float x, float y, float z)
		{
			return new Vector3(x, y, z);
		}

		// Token: 0x060079F7 RID: 31223 RVA: 0x00053331 File Offset: 0x00051531
		public static Vector4 vector4(float x, float y, float z, float w)
		{
			return new Vector4(x, y, z, w);
		}

		// Token: 0x060079F8 RID: 31224 RVA: 0x0005333C File Offset: 0x0005153C
		public static Quaternion quaternion(float x, float y, float z)
		{
			return Quaternion.Euler(x, y, z);
		}

		// Token: 0x060079F9 RID: 31225 RVA: 0x00053346 File Offset: 0x00051546
		public static Rect rect(float x, float y, float width, float height)
		{
			return new Rect(x, y, width, height);
		}
	}
}
