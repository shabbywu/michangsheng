using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x02000691 RID: 1681
	public static class FontCache
	{
		// Token: 0x0600352F RID: 13615 RVA: 0x0017037C File Offset: 0x0016E57C
		public static int GetLineHeight(Font font, int size, FontStyle fs)
		{
			long key = fs | (long)size << 24 | (long)font.GetInstanceID() << 32;
			int num = 0;
			if (FontCache.FontLineHeight.TryGetValue(key, out num))
			{
				return num;
			}
			TextGenerationSettings textGenerationSettings = default(TextGenerationSettings);
			textGenerationSettings.generationExtents = new Vector2(1000f, 1000f);
			if (font != null && font.dynamic)
			{
				textGenerationSettings.fontSize = size;
			}
			textGenerationSettings.textAnchor = 6;
			textGenerationSettings.lineSpacing = 1f;
			textGenerationSettings.alignByGeometry = false;
			textGenerationSettings.scaleFactor = 1f;
			textGenerationSettings.font = font;
			textGenerationSettings.fontSize = size;
			textGenerationSettings.fontStyle = fs;
			textGenerationSettings.resizeTextForBestFit = false;
			textGenerationSettings.updateBounds = false;
			textGenerationSettings.horizontalOverflow = 1;
			textGenerationSettings.verticalOverflow = 0;
			string text = "a\na";
			FontCache.sCachedTextGenerator.Populate(text, textGenerationSettings);
			IList<UIVertex> verts = FontCache.sCachedTextGenerator.verts;
			num = (int)(verts[0].position.y - verts[5].position.y);
			FontCache.FontLineHeight.Add(key, num);
			return num;
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x0017049C File Offset: 0x0016E69C
		public static int GetAdvance(Font font, int size, FontStyle fs, char ch)
		{
			long key = (long)((ulong)ch | (ulong)((ulong)fs << 16) | (ulong)((ulong)((long)size) << 24) | (ulong)((ulong)((long)font.GetInstanceID()) << 32));
			int num = 0;
			if (FontCache.FontAdvances.TryGetValue(key, out num))
			{
				return num;
			}
			for (int i = 0; i < 2; i++)
			{
				if (font.GetCharacterInfo(ch, ref FontCache.s_Info, size, fs))
				{
					num = (int)((short)FontCache.s_Info.advance);
					FontCache.FontAdvances.Add(key, num);
					return num;
				}
				font.RequestCharactersInTexture(new string(ch, 1), size, fs);
			}
			FontCache.FontAdvances.Add(key, 0);
			return 0;
		}

		// Token: 0x04002EEC RID: 12012
		private static CharacterInfo s_Info;

		// Token: 0x04002EED RID: 12013
		private static Dictionary<long, int> FontLineHeight = new Dictionary<long, int>();

		// Token: 0x04002EEE RID: 12014
		private static TextGenerator sCachedTextGenerator = new TextGenerator();

		// Token: 0x04002EEF RID: 12015
		private static Dictionary<long, int> FontAdvances = new Dictionary<long, int>();
	}
}
