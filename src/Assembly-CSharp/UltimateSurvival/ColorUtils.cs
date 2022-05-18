using System;
using System.Globalization;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000906 RID: 2310
	public static class ColorUtils
	{
		// Token: 0x06003B19 RID: 15129 RVA: 0x0002AD5E File Offset: 0x00028F5E
		public static string ColorToHex(Color32 color)
		{
			return "#" + color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
		}

		// Token: 0x06003B1A RID: 15130 RVA: 0x001AB3A0 File Offset: 0x001A95A0
		public static Color32 HexToColor(string hex)
		{
			byte b = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
			byte b2 = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
			byte b3 = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
			return new Color32(b, b2, b3, byte.MaxValue);
		}
	}
}
