using System.Collections.Generic;
using UnityEngine;

public static class ColorEx
{
	public static List<Color> ItemQualityColor = new List<Color>
	{
		new Color(72f / 85f, 72f / 85f, 0.7921569f),
		new Color(0.8f, 0.8862745f, 43f / 85f),
		new Color(0.6745098f, 1f, 0.99607843f),
		new Color(0.94509804f, 61f / 85f, 0.972549f),
		new Color(1f, 0.6745098f, 19f / 51f),
		new Color(1f, 0.69803923f, 0.54509807f)
	};

	public static string ColorToString(this Color color)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		string text = ((int)(color.r * 255f)).ToString("X");
		string text2 = ((int)(color.g * 255f)).ToString("X");
		string text3 = ((int)(color.b * 255f)).ToString("X");
		string text4 = ((int)(color.a * 255f)).ToString("X");
		return text + text2 + text3 + text4;
	}
}
