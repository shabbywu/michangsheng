using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000F06 RID: 3846
	public static class PODTypeFactory
	{
		// Token: 0x06006C40 RID: 27712 RVA: 0x002986AC File Offset: 0x002968AC
		public static Color color(float r, float g, float b, float a)
		{
			return new Color(r, g, b, a);
		}

		// Token: 0x06006C41 RID: 27713 RVA: 0x002986B7 File Offset: 0x002968B7
		public static Vector2 vector2(float x, float y)
		{
			return new Vector2(x, y);
		}

		// Token: 0x06006C42 RID: 27714 RVA: 0x002986C0 File Offset: 0x002968C0
		public static Vector3 vector3(float x, float y, float z)
		{
			return new Vector3(x, y, z);
		}

		// Token: 0x06006C43 RID: 27715 RVA: 0x002986CA File Offset: 0x002968CA
		public static Vector4 vector4(float x, float y, float z, float w)
		{
			return new Vector4(x, y, z, w);
		}

		// Token: 0x06006C44 RID: 27716 RVA: 0x002986D5 File Offset: 0x002968D5
		public static Quaternion quaternion(float x, float y, float z)
		{
			return Quaternion.Euler(x, y, z);
		}

		// Token: 0x06006C45 RID: 27717 RVA: 0x002986DF File Offset: 0x002968DF
		public static Rect rect(float x, float y, float width, float height)
		{
			return new Rect(x, y, width, height);
		}
	}
}
