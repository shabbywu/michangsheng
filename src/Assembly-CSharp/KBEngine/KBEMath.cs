using System;

namespace KBEngine
{
	// Token: 0x02000BD9 RID: 3033
	public class KBEMath
	{
		// Token: 0x06005437 RID: 21559 RVA: 0x00234918 File Offset: 0x00232B18
		public static float int82angle(sbyte angle, bool half)
		{
			float num = 128f;
			if (half)
			{
				num = 254f;
			}
			return (float)angle * (3.1415927f / num);
		}

		// Token: 0x06005438 RID: 21560 RVA: 0x000B67F5 File Offset: 0x000B49F5
		public static bool almostEqual(float f1, float f2, float epsilon)
		{
			return Math.Abs(f1 - f2) < epsilon;
		}

		// Token: 0x06005439 RID: 21561 RVA: 0x00234940 File Offset: 0x00232B40
		public static bool isNumeric(object v)
		{
			return v is sbyte || v is byte || v is short || v is ushort || v is int || v is uint || v is long || v is ulong || v is char || v is decimal || v is float || v is double || v is short || v is long || v is ushort || v is ulong || v is bool || v is bool;
		}

		// Token: 0x0400507C RID: 20604
		public static float KBE_FLT_MAX = float.MaxValue;
	}
}
