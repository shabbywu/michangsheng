using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002B8 RID: 696
public static class ColorEx
{
	// Token: 0x06001515 RID: 5397 RVA: 0x000BE158 File Offset: 0x000BC358
	public static string ColorToString(this Color color)
	{
		string str = ((int)(color.r * 255f)).ToString("X");
		string str2 = ((int)(color.g * 255f)).ToString("X");
		string str3 = ((int)(color.b * 255f)).ToString("X");
		string str4 = ((int)(color.a * 255f)).ToString("X");
		return str + str2 + str3 + str4;
	}

	// Token: 0x04001030 RID: 4144
	public static List<Color> ItemQualityColor = new List<Color>
	{
		new Color(0.84705883f, 0.84705883f, 0.7921569f),
		new Color(0.8f, 0.8862745f, 0.5058824f),
		new Color(0.6745098f, 1f, 0.99607843f),
		new Color(0.94509804f, 0.7176471f, 0.972549f),
		new Color(1f, 0.6745098f, 0.37254903f),
		new Color(1f, 0.69803923f, 0.54509807f)
	};
}
