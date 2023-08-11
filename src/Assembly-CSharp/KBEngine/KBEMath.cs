using System;

namespace KBEngine;

public class KBEMath
{
	public static float KBE_FLT_MAX = float.MaxValue;

	public static float int82angle(sbyte angle, bool half)
	{
		float num = 128f;
		if (half)
		{
			num = 254f;
		}
		return (float)angle * ((float)Math.PI / num);
	}

	public static bool almostEqual(float f1, float f2, float epsilon)
	{
		return Math.Abs(f1 - f2) < epsilon;
	}

	public static bool isNumeric(object v)
	{
		if (!(v is sbyte) && !(v is byte) && !(v is short) && !(v is ushort) && !(v is int) && !(v is uint) && !(v is long) && !(v is ulong) && !(v is char) && !(v is decimal) && !(v is float) && !(v is double) && !(v is short) && !(v is long) && !(v is ushort) && !(v is ulong) && !(v is bool))
		{
			return v is bool;
		}
		return true;
	}
}
