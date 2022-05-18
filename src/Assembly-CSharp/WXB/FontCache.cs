using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x020009A4 RID: 2468
	public static class FontCache
	{
		// Token: 0x06003EEA RID: 16106 RVA: 0x001B866C File Offset: 0x001B686C
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

		// Token: 0x06003EEB RID: 16107 RVA: 0x001B878C File Offset: 0x001B698C
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

		// Token: 0x040038A4 RID: 14500
		private static CharacterInfo s_Info;

		// Token: 0x040038A5 RID: 14501
		private static Dictionary<long, int> FontLineHeight = new Dictionary<long, int>();

		// Token: 0x040038A6 RID: 14502
		private static TextGenerator sCachedTextGenerator = new TextGenerator();

		// Token: 0x040038A7 RID: 14503
		private static Dictionary<long, int> FontAdvances = new Dictionary<long, int>();
	}
}
