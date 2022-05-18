using System;

namespace KBEngine
{
	// Token: 0x02000F5C RID: 3932
	public class KBEMath
	{
		// Token: 0x06005E75 RID: 24181 RVA: 0x002621B0 File Offset: 0x002603B0
		public static float int82angle(sbyte angle, bool half)
		{
			float num = 128f;
			if (half)
			{
				num = 254f;
			}
			return (float)angle * (3.1415927f / num);
		}

		// Token: 0x06005E76 RID: 24182 RVA: 0x00017EAD File Offset: 0x000160AD
		public static bool almostEqual(float f1, float f2, float epsilon)
		{
			return Math.Abs(f1 - f2) < epsilon;
		}

		// Token: 0x06005E77 RID: 24183 RVA: 0x002621D8 File Offset: 0x002603D8
		public static bool isNumeric(object v)
		{
			return v is sbyte || v is byte || v is short || v is ushort || v is int || v is uint || v is long || v is ulong || v is char || v is decimal || v is float || v is double || v is short || v is long || v is ushort || v is ulong || v is bool || v is bool;
		}

		// Token: 0x04005B1D RID: 23325
		public static float KBE_FLT_MAX = float.MaxValue;
	}
}
