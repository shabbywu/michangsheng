using System.Globalization;
using UnityEngine;

namespace UltimateSurvival;

public static class ColorUtils
{
	public static string ColorToHex(Color32 color)
	{
		return "#" + color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
	}

	public static Color32 HexToColor(string hex)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		byte num = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
		byte b2 = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
		return new Color32(num, b, b2, byte.MaxValue);
	}
}
