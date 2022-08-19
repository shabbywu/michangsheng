using System;
using System.Globalization;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200061F RID: 1567
	public static class ColorUtils
	{
		// Token: 0x060031E5 RID: 12773 RVA: 0x00161A83 File Offset: 0x0015FC83
		public static string ColorToHex(Color32 color)
		{
			return "#" + color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x00161AC4 File Offset: 0x0015FCC4
		public static Color32 HexToColor(string hex)
		{
			byte b = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
			byte b2 = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
			byte b3 = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
			return new Color32(b, b2, b3, byte.MaxValue);
		}
	}
}
