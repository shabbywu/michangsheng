using System.Collections.Generic;
using UnityEngine;

namespace WXB;

public static class FontCache
{
	private static CharacterInfo s_Info;

	private static Dictionary<long, int> FontLineHeight = new Dictionary<long, int>();

	private static TextGenerator sCachedTextGenerator = new TextGenerator();

	private static Dictionary<long, int> FontAdvances = new Dictionary<long, int>();

	public static int GetLineHeight(Font font, int size, FontStyle fs)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		long key = (long)fs | ((long)size << 24) | ((long)((Object)font).GetInstanceID() << 32);
		int value = 0;
		if (FontLineHeight.TryGetValue(key, out value))
		{
			return value;
		}
		TextGenerationSettings val = default(TextGenerationSettings);
		val.generationExtents = new Vector2(1000f, 1000f);
		if ((Object)(object)font != (Object)null && font.dynamic)
		{
			val.fontSize = size;
		}
		val.textAnchor = (TextAnchor)6;
		val.lineSpacing = 1f;
		val.alignByGeometry = false;
		val.scaleFactor = 1f;
		val.font = font;
		val.fontSize = size;
		val.fontStyle = fs;
		val.resizeTextForBestFit = false;
		val.updateBounds = false;
		val.horizontalOverflow = (HorizontalWrapMode)1;
		val.verticalOverflow = (VerticalWrapMode)0;
		string text = "a\na";
		sCachedTextGenerator.Populate(text, val);
		IList<UIVertex> verts = sCachedTextGenerator.verts;
		value = (int)(verts[0].position.y - verts[5].position.y);
		FontLineHeight.Add(key, value);
		return value;
	}

	public static int GetAdvance(Font font, int size, FontStyle fs, char ch)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		long key = ch | ((long)fs << 16) | ((long)size << 24) | ((long)((Object)font).GetInstanceID() << 32);
		int value = 0;
		if (FontAdvances.TryGetValue(key, out value))
		{
			return value;
		}
		for (int i = 0; i < 2; i++)
		{
			if (font.GetCharacterInfo(ch, ref s_Info, size, fs))
			{
				value = (short)((CharacterInfo)(ref s_Info)).advance;
				FontAdvances.Add(key, value);
				return value;
			}
			font.RequestCharactersInTexture(new string(ch, 1), size, fs);
		}
		FontAdvances.Add(key, 0);
		return 0;
	}
}
