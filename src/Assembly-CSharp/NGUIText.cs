using System.Diagnostics;
using System.Text;
using UnityEngine;

public static class NGUIText
{
	public enum Alignment
	{
		Automatic,
		Left,
		Center,
		Right,
		Justified
	}

	public enum SymbolStyle
	{
		None,
		Normal,
		Colored
	}

	public class GlyphInfo
	{
		public Vector2 v0;

		public Vector2 v1;

		public Vector2 u0;

		public Vector2 u1;

		public float advance;

		public int channel;

		public bool rotatedUVs;
	}

	public static UIFont bitmapFont;

	public static Font dynamicFont;

	public static GlyphInfo glyph = new GlyphInfo();

	public static int fontSize = 16;

	public static float fontScale = 1f;

	public static float pixelDensity = 1f;

	public static FontStyle fontStyle = (FontStyle)0;

	public static Alignment alignment = Alignment.Left;

	public static Color tint = Color.white;

	public static int rectWidth = 1000000;

	public static int rectHeight = 1000000;

	public static int maxLines = 0;

	public static bool gradient = false;

	public static Color gradientBottom = Color.white;

	public static Color gradientTop = Color.white;

	public static bool encoding = false;

	public static float spacingX = 0f;

	public static float spacingY = 0f;

	public static bool premultiply = false;

	public static SymbolStyle symbolStyle;

	public static int finalSize = 0;

	public static float finalSpacingX = 0f;

	public static float finalLineHeight = 0f;

	public static float baseline = 0f;

	public static bool useSymbols = false;

	private static Color mInvisible = new Color(0f, 0f, 0f, 0f);

	private static BetterList<Color> mColors = new BetterList<Color>();

	private static float mAlpha = 1f;

	private static CharacterInfo mTempChar;

	private static BetterList<float> mSizes = new BetterList<float>();

	private static Color32 s_c0;

	private static Color32 s_c1;

	private static float[] mBoldOffset = new float[8] { -0.5f, 0f, 0.5f, 0f, 0f, -0.5f, 0f, 0.5f };

	public static void Update()
	{
		Update(request: true);
	}

	public static void Update(bool request)
	{
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		finalSize = Mathf.RoundToInt((float)fontSize / pixelDensity);
		finalSpacingX = spacingX * fontScale;
		finalLineHeight = ((float)fontSize + spacingY) * fontScale;
		useSymbols = (Object)(object)bitmapFont != (Object)null && bitmapFont.hasSymbols && encoding && symbolStyle != SymbolStyle.None;
		if (!((Object)(object)dynamicFont != (Object)null && request))
		{
			return;
		}
		dynamicFont.RequestCharactersInTexture(")_-", finalSize, fontStyle);
		if (!dynamicFont.GetCharacterInfo(')', ref mTempChar, finalSize, fontStyle))
		{
			dynamicFont.RequestCharactersInTexture("A", finalSize, fontStyle);
			if (!dynamicFont.GetCharacterInfo('A', ref mTempChar, finalSize, fontStyle))
			{
				baseline = 0f;
				return;
			}
		}
		float yMax = ((Rect)(ref mTempChar.vert)).yMax;
		float yMin = ((Rect)(ref mTempChar.vert)).yMin;
		baseline = Mathf.Round(yMax + ((float)finalSize - yMax + yMin) * 0.5f);
	}

	public static void Prepare(string text)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)dynamicFont != (Object)null)
		{
			dynamicFont.RequestCharactersInTexture(text, finalSize, fontStyle);
		}
	}

	public static BMSymbol GetSymbol(string text, int index, int textLength)
	{
		if (!((Object)(object)bitmapFont != (Object)null))
		{
			return null;
		}
		return bitmapFont.MatchSymbol(text, index, textLength);
	}

	public static float GetGlyphWidth(int ch, int prev)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)bitmapFont != (Object)null)
		{
			BMGlyph bMGlyph = bitmapFont.bmFont.GetGlyph(ch);
			if (bMGlyph != null)
			{
				return fontScale * (float)((prev != 0) ? (bMGlyph.advance + bMGlyph.GetKerning(prev)) : bMGlyph.advance);
			}
		}
		else if ((Object)(object)dynamicFont != (Object)null && dynamicFont.GetCharacterInfo((char)ch, ref mTempChar, finalSize, fontStyle))
		{
			return mTempChar.width * fontScale * pixelDensity;
		}
		return 0f;
	}

	public static GlyphInfo GetGlyph(int ch, int prev)
	{
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)bitmapFont != (Object)null)
		{
			BMGlyph bMGlyph = bitmapFont.bmFont.GetGlyph(ch);
			if (bMGlyph != null)
			{
				int num = ((prev != 0) ? bMGlyph.GetKerning(prev) : 0);
				glyph.v0.x = ((prev != 0) ? (bMGlyph.offsetX + num) : bMGlyph.offsetX);
				glyph.v1.y = -bMGlyph.offsetY;
				glyph.v1.x = glyph.v0.x + (float)bMGlyph.width;
				glyph.v0.y = glyph.v1.y - (float)bMGlyph.height;
				glyph.u0.x = bMGlyph.x;
				glyph.u0.y = bMGlyph.y + bMGlyph.height;
				glyph.u1.x = bMGlyph.x + bMGlyph.width;
				glyph.u1.y = bMGlyph.y;
				glyph.advance = bMGlyph.advance + num;
				glyph.channel = bMGlyph.channel;
				glyph.rotatedUVs = false;
				if (fontScale != 1f)
				{
					GlyphInfo glyphInfo = glyph;
					glyphInfo.v0 *= fontScale;
					GlyphInfo glyphInfo2 = glyph;
					glyphInfo2.v1 *= fontScale;
					glyph.advance *= fontScale;
				}
				return glyph;
			}
		}
		else if ((Object)(object)dynamicFont != (Object)null && dynamicFont.GetCharacterInfo((char)ch, ref mTempChar, finalSize, fontStyle))
		{
			glyph.v0.x = ((Rect)(ref mTempChar.vert)).xMin;
			glyph.v1.x = glyph.v0.x + ((Rect)(ref mTempChar.vert)).width;
			glyph.v0.y = ((Rect)(ref mTempChar.vert)).yMax - baseline;
			glyph.v1.y = glyph.v0.y - ((Rect)(ref mTempChar.vert)).height;
			glyph.u0.x = ((Rect)(ref mTempChar.uv)).xMin;
			glyph.u0.y = ((Rect)(ref mTempChar.uv)).yMin;
			glyph.u1.x = ((Rect)(ref mTempChar.uv)).xMax;
			glyph.u1.y = ((Rect)(ref mTempChar.uv)).yMax;
			glyph.advance = mTempChar.width;
			glyph.channel = 0;
			glyph.rotatedUVs = mTempChar.flipped;
			glyph.v0.x = Mathf.Round(glyph.v0.x);
			glyph.v0.y = Mathf.Round(glyph.v0.y);
			glyph.v1.x = Mathf.Round(glyph.v1.x);
			glyph.v1.y = Mathf.Round(glyph.v1.y);
			float num2 = fontScale * pixelDensity;
			if (num2 != 1f)
			{
				GlyphInfo glyphInfo3 = glyph;
				glyphInfo3.v0 *= num2;
				GlyphInfo glyphInfo4 = glyph;
				glyphInfo4.v1 *= num2;
				glyph.advance *= num2;
			}
			return glyph;
		}
		return null;
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static float ParseAlpha(string text, int index)
	{
		return Mathf.Clamp01((float)((NGUIMath.HexToDecimal(text[index + 1]) << 4) | NGUIMath.HexToDecimal(text[index + 2])) / 255f);
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static Color ParseColor(string text, int offset)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return ParseColor24(text, offset);
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static Color ParseColor24(string text, int offset)
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		int num = (NGUIMath.HexToDecimal(text[offset]) << 4) | NGUIMath.HexToDecimal(text[offset + 1]);
		int num2 = (NGUIMath.HexToDecimal(text[offset + 2]) << 4) | NGUIMath.HexToDecimal(text[offset + 3]);
		int num3 = (NGUIMath.HexToDecimal(text[offset + 4]) << 4) | NGUIMath.HexToDecimal(text[offset + 5]);
		float num4 = 0.003921569f;
		return new Color(num4 * (float)num, num4 * (float)num2, num4 * (float)num3);
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static Color ParseColor32(string text, int offset)
	{
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		int num = (NGUIMath.HexToDecimal(text[offset]) << 4) | NGUIMath.HexToDecimal(text[offset + 1]);
		int num2 = (NGUIMath.HexToDecimal(text[offset + 2]) << 4) | NGUIMath.HexToDecimal(text[offset + 3]);
		int num3 = (NGUIMath.HexToDecimal(text[offset + 4]) << 4) | NGUIMath.HexToDecimal(text[offset + 5]);
		int num4 = (NGUIMath.HexToDecimal(text[offset + 6]) << 4) | NGUIMath.HexToDecimal(text[offset + 7]);
		float num5 = 0.003921569f;
		return new Color(num5 * (float)num, num5 * (float)num2, num5 * (float)num3, num5 * (float)num4);
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static string EncodeColor(Color c)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return EncodeColor24(c);
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static string EncodeAlpha(float a)
	{
		return NGUIMath.DecimalToHex8(Mathf.Clamp(Mathf.RoundToInt(a * 255f), 0, 255));
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static string EncodeColor24(Color c)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		return NGUIMath.DecimalToHex24(0xFFFFFF & (NGUIMath.ColorToInt(c) >> 8));
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static string EncodeColor32(Color c)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return NGUIMath.DecimalToHex32(NGUIMath.ColorToInt(c));
	}

	public static bool ParseSymbol(string text, ref int index)
	{
		int sub = 1;
		bool bold = false;
		bool italic = false;
		bool underline = false;
		bool strike = false;
		return ParseSymbol(text, ref index, null, premultiply: false, ref sub, ref bold, ref italic, ref underline, ref strike);
	}

	public static bool ParseSymbol(string text, ref int index, BetterList<Color> colors, bool premultiply, ref int sub, ref bool bold, ref bool italic, ref bool underline, ref bool strike)
	{
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		int length = text.Length;
		if (index + 3 > length || text[index] != '[')
		{
			return false;
		}
		if (text[index + 2] == ']')
		{
			if (text[index + 1] == '-')
			{
				if (colors != null && colors.size > 1)
				{
					colors.RemoveAt(colors.size - 1);
				}
				index += 3;
				return true;
			}
			switch (text.Substring(index, 3))
			{
			case "[b]":
				bold = true;
				index += 3;
				return true;
			case "[i]":
				italic = true;
				index += 3;
				return true;
			case "[u]":
				underline = true;
				index += 3;
				return true;
			case "[s]":
				strike = true;
				index += 3;
				return true;
			}
		}
		if (index + 4 > length)
		{
			return false;
		}
		if (text[index + 3] == ']')
		{
			switch (text.Substring(index, 4))
			{
			case "[/b]":
				bold = false;
				index += 4;
				return true;
			case "[/i]":
				italic = false;
				index += 4;
				return true;
			case "[/u]":
				underline = false;
				index += 4;
				return true;
			case "[/s]":
				strike = false;
				index += 4;
				return true;
			default:
				mAlpha = (float)((NGUIMath.HexToDecimal(text[index + 1]) << 4) | NGUIMath.HexToDecimal(text[index + 2])) / 255f;
				index += 4;
				return true;
			}
		}
		if (index + 5 > length)
		{
			return false;
		}
		if (text[index + 4] == ']')
		{
			string text2 = text.Substring(index, 5);
			if (text2 == "[sub]")
			{
				sub = 1;
				index += 5;
				return true;
			}
			if (text2 == "[sup]")
			{
				sub = 2;
				index += 5;
				return true;
			}
		}
		if (index + 6 > length)
		{
			return false;
		}
		if (text[index + 5] == ']')
		{
			switch (text.Substring(index, 6))
			{
			case "[/sub]":
				sub = 0;
				index += 6;
				return true;
			case "[/sup]":
				sub = 0;
				index += 6;
				return true;
			case "[/url]":
				index += 6;
				return true;
			}
		}
		if (text[index + 1] == 'u' && text[index + 2] == 'r' && text[index + 3] == 'l' && text[index + 4] == '=')
		{
			int num = text.IndexOf(']', index + 4);
			if (num != -1)
			{
				index = num + 1;
				return true;
			}
			index = text.Length;
			return true;
		}
		if (index + 8 > length)
		{
			return false;
		}
		if (text[index + 7] == ']')
		{
			Color val = ParseColor24(text, index + 1);
			if (EncodeColor24(val) != text.Substring(index + 1, 6).ToUpper())
			{
				return false;
			}
			if (colors != null)
			{
				val.a = colors[colors.size - 1].a;
				if (premultiply && val.a != 1f)
				{
					val = Color.Lerp(mInvisible, val, val.a);
				}
				colors.Add(val);
			}
			index += 8;
			return true;
		}
		if (index + 10 > length)
		{
			return false;
		}
		if (text[index + 9] == ']')
		{
			Color val2 = ParseColor32(text, index + 1);
			if (EncodeColor32(val2) != text.Substring(index + 1, 8).ToUpper())
			{
				return false;
			}
			if (colors != null)
			{
				if (premultiply && val2.a != 1f)
				{
					val2 = Color.Lerp(mInvisible, val2, val2.a);
				}
				colors.Add(val2);
			}
			index += 10;
			return true;
		}
		return false;
	}

	public static string StripSymbols(string text)
	{
		if (text != null)
		{
			int num = 0;
			int length = text.Length;
			while (num < length)
			{
				if (text[num] == '[')
				{
					int sub = 0;
					bool bold = false;
					bool italic = false;
					bool underline = false;
					bool strike = false;
					int index = num;
					if (ParseSymbol(text, ref index, null, premultiply: false, ref sub, ref bold, ref italic, ref underline, ref strike))
					{
						text = text.Remove(num, index - num);
						length = text.Length;
						continue;
					}
				}
				num++;
			}
		}
		return text;
	}

	public static void Align(BetterList<Vector3> verts, int indexOffset, float printedWidth)
	{
		switch (alignment)
		{
		case Alignment.Right:
		{
			float num15 = (float)rectWidth - printedWidth;
			if (!(num15 < 0f))
			{
				for (int j = indexOffset; j < verts.size; j++)
				{
					verts.buffer[j].x += num15;
				}
			}
			break;
		}
		case Alignment.Center:
		{
			float num12 = ((float)rectWidth - printedWidth) * 0.5f;
			if (!(num12 < 0f))
			{
				int num13 = Mathf.RoundToInt((float)rectWidth - printedWidth);
				int num14 = Mathf.RoundToInt((float)rectWidth);
				bool flag = (num13 & 1) == 1;
				bool flag2 = (num14 & 1) == 1;
				if ((flag && !flag2) || (!flag && flag2))
				{
					num12 += 0.5f * fontScale;
				}
				for (int i = indexOffset; i < verts.size; i++)
				{
					verts.buffer[i].x += num12;
				}
			}
			break;
		}
		case Alignment.Justified:
		{
			if (printedWidth < (float)rectWidth * 0.65f || ((float)rectWidth - printedWidth) * 0.5f < 1f)
			{
				break;
			}
			int num = (verts.size - indexOffset) / 4;
			if (num >= 1)
			{
				float num2 = 1f / (float)(num - 1);
				float num3 = (float)rectWidth / printedWidth;
				int num4 = indexOffset + 4;
				int num5 = 1;
				while (num4 < verts.size)
				{
					float x = verts.buffer[num4].x;
					float x2 = verts.buffer[num4 + 2].x;
					float num6 = x2 - x;
					float num7 = x * num3;
					float num8 = num7 + num6;
					float num9 = x2 * num3;
					float num10 = num9 - num6;
					float num11 = (float)num5 * num2;
					x = Mathf.Lerp(num7, num10, num11);
					x2 = Mathf.Lerp(num8, num9, num11);
					x = Mathf.Round(x);
					x2 = Mathf.Round(x2);
					verts.buffer[num4++].x = x;
					verts.buffer[num4++].x = x;
					verts.buffer[num4++].x = x2;
					verts.buffer[num4++].x = x2;
					num5++;
				}
			}
			break;
		}
		}
	}

	public static int GetClosestCharacter(BetterList<Vector3> verts, Vector2 pos)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		float num = float.MaxValue;
		float num2 = float.MaxValue;
		int result = 0;
		for (int i = 0; i < verts.size; i++)
		{
			float num3 = Mathf.Abs(pos.y - verts[i].y);
			if (!(num3 > num2))
			{
				float num4 = Mathf.Abs(pos.x - verts[i].x);
				if (num3 < num2)
				{
					num2 = num3;
					num = num4;
					result = i;
				}
				else if (num4 < num)
				{
					num = num4;
					result = i;
				}
			}
		}
		return result;
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	private static bool IsSpace(int ch)
	{
		if (ch != 32 && ch != 8202)
		{
			return ch == 8203;
		}
		return true;
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	public static void EndLine(ref StringBuilder s)
	{
		int num = s.Length - 1;
		if (num > 0 && IsSpace(s[num]))
		{
			s[num] = '\n';
		}
		else
		{
			s.Append('\n');
		}
	}

	[DebuggerHidden]
	[DebuggerStepThrough]
	private static void ReplaceSpaceWithNewline(ref StringBuilder s)
	{
		int num = s.Length - 1;
		if (num > 0 && IsSpace(s[num]))
		{
			s[num] = '\n';
		}
	}

	public static Vector2 CalculatePrintedSize(string text)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		Vector2 zero = Vector2.zero;
		if (!string.IsNullOrEmpty(text))
		{
			if (encoding)
			{
				text = StripSymbols(text);
			}
			Prepare(text);
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			int length = text.Length;
			int num4 = 0;
			int prev = 0;
			for (int i = 0; i < length; i++)
			{
				num4 = text[i];
				if (num4 == 10)
				{
					if (num > num3)
					{
						num3 = num;
					}
					num = 0f;
					num2 += finalLineHeight;
				}
				else
				{
					if (num4 < 32)
					{
						continue;
					}
					BMSymbol bMSymbol = (useSymbols ? GetSymbol(text, i, length) : null);
					if (bMSymbol == null)
					{
						float glyphWidth = GetGlyphWidth(num4, prev);
						if (glyphWidth == 0f)
						{
							continue;
						}
						glyphWidth += finalSpacingX;
						if (Mathf.RoundToInt(num + glyphWidth) > rectWidth)
						{
							if (num > num3)
							{
								num3 = num - finalSpacingX;
							}
							num = glyphWidth;
							num2 += finalLineHeight;
						}
						else
						{
							num += glyphWidth;
						}
						prev = num4;
						continue;
					}
					float num5 = finalSpacingX + (float)bMSymbol.advance * fontScale;
					if (Mathf.RoundToInt(num + num5) > rectWidth)
					{
						if (num > num3)
						{
							num3 = num - finalSpacingX;
						}
						num = num5;
						num2 += finalLineHeight;
					}
					else
					{
						num += num5;
					}
					i += bMSymbol.sequence.Length - 1;
					prev = 0;
				}
			}
			zero.x = ((num > num3) ? (num - finalSpacingX) : num3);
			zero.y = num2 + finalLineHeight;
		}
		return zero;
	}

	public static int CalculateOffsetToFit(string text)
	{
		if (string.IsNullOrEmpty(text) || rectWidth < 1)
		{
			return 0;
		}
		Prepare(text);
		int length = text.Length;
		int prev = 0;
		int i = 0;
		for (int length2 = text.Length; i < length2; i++)
		{
			BMSymbol bMSymbol = (useSymbols ? GetSymbol(text, i, length) : null);
			if (bMSymbol == null)
			{
				char num = text[i];
				float glyphWidth = GetGlyphWidth(num, prev);
				if (glyphWidth != 0f)
				{
					mSizes.Add(finalSpacingX + glyphWidth);
				}
				prev = num;
				continue;
			}
			mSizes.Add(finalSpacingX + (float)bMSymbol.advance * fontScale);
			int j = 0;
			for (int num2 = bMSymbol.sequence.Length - 1; j < num2; j++)
			{
				mSizes.Add(0f);
			}
			i += bMSymbol.sequence.Length - 1;
			prev = 0;
		}
		float num3 = rectWidth;
		int num4 = mSizes.size;
		while (num4 > 0 && num3 > 0f)
		{
			num3 -= mSizes[--num4];
		}
		mSizes.Clear();
		if (num3 < 0f)
		{
			num4++;
		}
		return num4;
	}

	public static string GetEndOfLineThatFits(string text)
	{
		int length = text.Length;
		int num = CalculateOffsetToFit(text);
		return text.Substring(num, length - num);
	}

	public static bool WrapText(string text, out string finalText)
	{
		return WrapText(text, out finalText, keepCharCount: false);
	}

	public static bool WrapText(string text, out string finalText, bool keepCharCount)
	{
		if (rectWidth < 1 || rectHeight < 1 || finalLineHeight < 1f)
		{
			finalText = "";
			return false;
		}
		float num = ((maxLines > 0) ? Mathf.Min((float)rectHeight, finalLineHeight * (float)maxLines) : ((float)rectHeight));
		int num2 = ((maxLines > 0) ? maxLines : 1000000);
		num2 = Mathf.FloorToInt(Mathf.Min((float)num2, num / finalLineHeight) + 0.01f);
		if (num2 == 0)
		{
			finalText = "";
			return false;
		}
		if (string.IsNullOrEmpty(text))
		{
			text = " ";
		}
		Prepare(text);
		StringBuilder s = new StringBuilder();
		int length = text.Length;
		float num3 = rectWidth;
		int num4 = 0;
		int i = 0;
		int num5 = 1;
		int prev = 0;
		bool flag = true;
		bool flag2 = true;
		bool flag3 = false;
		for (; i < length; i++)
		{
			char c = text[i];
			if (c > '\u2fff')
			{
				flag3 = true;
			}
			if (c == '\n')
			{
				if (num5 == num2)
				{
					break;
				}
				num3 = rectWidth;
				if (num4 < i)
				{
					s.Append(text.Substring(num4, i - num4 + 1));
				}
				else
				{
					s.Append(c);
				}
				flag = true;
				num5++;
				num4 = i + 1;
				prev = 0;
				continue;
			}
			if (encoding && ParseSymbol(text, ref i))
			{
				i--;
				continue;
			}
			BMSymbol bMSymbol = (useSymbols ? GetSymbol(text, i, length) : null);
			float num6;
			if (bMSymbol == null)
			{
				float glyphWidth = GetGlyphWidth(c, prev);
				if (glyphWidth == 0f)
				{
					continue;
				}
				num6 = finalSpacingX + glyphWidth;
			}
			else
			{
				num6 = finalSpacingX + (float)bMSymbol.advance * fontScale;
			}
			num3 -= num6;
			if (IsSpace(c) && !flag3 && num4 < i)
			{
				int num7 = i - num4 + 1;
				if (num5 == num2 && num3 <= 0f && i < length)
				{
					char c2 = text[i];
					if (c2 < ' ' || IsSpace(c2))
					{
						num7--;
					}
				}
				s.Append(text.Substring(num4, num7));
				flag = false;
				num4 = i + 1;
				prev = c;
			}
			if (Mathf.RoundToInt(num3) < 0)
			{
				if (!flag && num5 != num2)
				{
					flag = true;
					num3 = rectWidth;
					i = num4 - 1;
					prev = 0;
					if (num5++ == num2)
					{
						break;
					}
					if (keepCharCount)
					{
						ReplaceSpaceWithNewline(ref s);
					}
					else
					{
						EndLine(ref s);
					}
					continue;
				}
				s.Append(text.Substring(num4, Mathf.Max(0, i - num4)));
				bool flag4 = IsSpace(c);
				if (!flag4 && !flag3)
				{
					flag2 = false;
				}
				if (num5++ == num2)
				{
					num4 = i;
					break;
				}
				if (keepCharCount)
				{
					ReplaceSpaceWithNewline(ref s);
				}
				else
				{
					EndLine(ref s);
				}
				flag = true;
				if (flag4)
				{
					num4 = i + 1;
					num3 = rectWidth;
				}
				else
				{
					num4 = i;
					num3 = (float)rectWidth - num6;
				}
				prev = 0;
			}
			else
			{
				prev = c;
			}
			if (bMSymbol != null)
			{
				i += bMSymbol.length - 1;
				prev = 0;
			}
		}
		if (num4 < i)
		{
			s.Append(text.Substring(num4, i - num4));
		}
		finalText = s.ToString();
		if (flag2)
		{
			if (i != length)
			{
				return num5 <= Mathf.Min(maxLines, num2);
			}
			return true;
		}
		return false;
	}

	public static void Print(string text, BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a50: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0859: Unknown result type (might be due to invalid IL or missing references)
		//IL_085b: Unknown result type (might be due to invalid IL or missing references)
		//IL_085f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0864: Unknown result type (might be due to invalid IL or missing references)
		//IL_0869: Unknown result type (might be due to invalid IL or missing references)
		//IL_086e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0870: Unknown result type (might be due to invalid IL or missing references)
		//IL_0874: Unknown result type (might be due to invalid IL or missing references)
		//IL_0879: Unknown result type (might be due to invalid IL or missing references)
		//IL_087e: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0901: Unknown result type (might be due to invalid IL or missing references)
		//IL_0908: Unknown result type (might be due to invalid IL or missing references)
		//IL_090d: Unknown result type (might be due to invalid IL or missing references)
		//IL_097e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0980: Unknown result type (might be due to invalid IL or missing references)
		//IL_0985: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0adb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af0: Unknown result type (might be due to invalid IL or missing references)
		//IL_078a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0728: Unknown result type (might be due to invalid IL or missing references)
		//IL_074b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0758: Unknown result type (might be due to invalid IL or missing references)
		//IL_077b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0893: Unknown result type (might be due to invalid IL or missing references)
		//IL_089e: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0998: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d31: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d46: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8a: Unknown result type (might be due to invalid IL or missing references)
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		int size = verts.size;
		Prepare(text);
		mColors.Add(Color.white);
		mAlpha = 1f;
		int num = 0;
		int prev = 0;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = finalSize;
		Color val = tint * gradientBottom;
		Color val2 = tint * gradientTop;
		Color32 val3 = Color32.op_Implicit(tint);
		int length = text.Length;
		Rect val4 = default(Rect);
		float num6 = 0f;
		float num7 = 0f;
		float num8 = num5 * pixelDensity;
		bool flag = false;
		int sub = 0;
		bool bold = false;
		bool italic = false;
		bool underline = false;
		bool strike = false;
		float num9 = 0f;
		if ((Object)(object)bitmapFont != (Object)null)
		{
			val4 = bitmapFont.uvRect;
			num6 = ((Rect)(ref val4)).width / (float)bitmapFont.texWidth;
			num7 = ((Rect)(ref val4)).height / (float)bitmapFont.texHeight;
		}
		for (int i = 0; i < length; i++)
		{
			num = text[i];
			num9 = num2;
			if (num == 10)
			{
				if (num2 > num4)
				{
					num4 = num2;
				}
				if (alignment != Alignment.Left)
				{
					Align(verts, size, num2 - finalSpacingX);
					size = verts.size;
				}
				num2 = 0f;
				num3 += finalLineHeight;
				prev = 0;
				continue;
			}
			if (num < 32)
			{
				prev = num;
				continue;
			}
			if (encoding && ParseSymbol(text, ref i, mColors, premultiply, ref sub, ref bold, ref italic, ref underline, ref strike))
			{
				Color val5 = tint * mColors[mColors.size - 1];
				val5.a *= mAlpha;
				val3 = Color32.op_Implicit(val5);
				int j = 0;
				for (int num10 = mColors.size - 2; j < num10; j++)
				{
					val5.a *= mColors[j].a;
				}
				if (gradient)
				{
					val = gradientBottom * val5;
					val2 = gradientTop * val5;
				}
				i--;
				continue;
			}
			BMSymbol bMSymbol = (useSymbols ? GetSymbol(text, i, length) : null);
			float num11;
			float num12;
			float num14;
			float num13;
			if (bMSymbol != null)
			{
				num11 = num2 + (float)bMSymbol.offsetX * fontScale;
				num12 = num11 + (float)bMSymbol.width * fontScale;
				num13 = 0f - (num3 + (float)bMSymbol.offsetY * fontScale);
				num14 = num13 - (float)bMSymbol.height * fontScale;
				if (Mathf.RoundToInt(num2 + (float)bMSymbol.advance * fontScale) > rectWidth)
				{
					if (num2 == 0f)
					{
						return;
					}
					if (alignment != Alignment.Left && size < verts.size)
					{
						Align(verts, size, num2 - finalSpacingX);
						size = verts.size;
					}
					num11 -= num2;
					num12 -= num2;
					num14 -= finalLineHeight;
					num13 -= finalLineHeight;
					num2 = 0f;
					num3 += finalLineHeight;
					num9 = 0f;
				}
				verts.Add(new Vector3(num11, num14));
				verts.Add(new Vector3(num11, num13));
				verts.Add(new Vector3(num12, num13));
				verts.Add(new Vector3(num12, num14));
				num2 += finalSpacingX + (float)bMSymbol.advance * fontScale;
				i += bMSymbol.length - 1;
				prev = 0;
				if (uvs != null)
				{
					Rect uvRect = bMSymbol.uvRect;
					float xMin = ((Rect)(ref uvRect)).xMin;
					float yMin = ((Rect)(ref uvRect)).yMin;
					float xMax = ((Rect)(ref uvRect)).xMax;
					float yMax = ((Rect)(ref uvRect)).yMax;
					uvs.Add(new Vector2(xMin, yMin));
					uvs.Add(new Vector2(xMin, yMax));
					uvs.Add(new Vector2(xMax, yMax));
					uvs.Add(new Vector2(xMax, yMin));
				}
				if (cols == null)
				{
					continue;
				}
				if (symbolStyle == SymbolStyle.Colored)
				{
					for (int k = 0; k < 4; k++)
					{
						cols.Add(val3);
					}
					continue;
				}
				Color32 item = Color32.op_Implicit(Color.white);
				item.a = val3.a;
				for (int l = 0; l < 4; l++)
				{
					cols.Add(item);
				}
				continue;
			}
			GlyphInfo glyphInfo = GetGlyph(num, prev);
			if (glyphInfo == null)
			{
				continue;
			}
			prev = num;
			if (sub != 0)
			{
				glyphInfo.v0.x *= 0.75f;
				glyphInfo.v0.y *= 0.75f;
				glyphInfo.v1.x *= 0.75f;
				glyphInfo.v1.y *= 0.75f;
				if (sub == 1)
				{
					glyphInfo.v0.y -= fontScale * (float)fontSize * 0.4f;
					glyphInfo.v1.y -= fontScale * (float)fontSize * 0.4f;
				}
				else
				{
					glyphInfo.v0.y += fontScale * (float)fontSize * 0.05f;
					glyphInfo.v1.y += fontScale * (float)fontSize * 0.05f;
				}
			}
			num11 = glyphInfo.v0.x + num2;
			num14 = glyphInfo.v0.y - num3;
			num12 = glyphInfo.v1.x + num2;
			num13 = glyphInfo.v1.y - num3;
			float num15 = glyphInfo.advance;
			if (finalSpacingX < 0f)
			{
				num15 += finalSpacingX;
			}
			if (Mathf.RoundToInt(num2 + num15) > rectWidth)
			{
				if (num2 == 0f)
				{
					return;
				}
				if (alignment != Alignment.Left && size < verts.size)
				{
					Align(verts, size, num2 - finalSpacingX);
					size = verts.size;
				}
				num11 -= num2;
				num12 -= num2;
				num14 -= finalLineHeight;
				num13 -= finalLineHeight;
				num2 = 0f;
				num3 += finalLineHeight;
				num9 = 0f;
			}
			if (IsSpace(num))
			{
				if (underline)
				{
					num = 95;
				}
				else if (strike)
				{
					num = 45;
				}
			}
			num2 += ((sub == 0) ? (finalSpacingX + glyphInfo.advance) : ((finalSpacingX + glyphInfo.advance) * 0.75f));
			if (IsSpace(num))
			{
				continue;
			}
			if (uvs != null)
			{
				if ((Object)(object)bitmapFont != (Object)null)
				{
					glyphInfo.u0.x = ((Rect)(ref val4)).xMin + num6 * glyphInfo.u0.x;
					glyphInfo.u1.x = ((Rect)(ref val4)).xMin + num6 * glyphInfo.u1.x;
					glyphInfo.u0.y = ((Rect)(ref val4)).yMax - num7 * glyphInfo.u0.y;
					glyphInfo.u1.y = ((Rect)(ref val4)).yMax - num7 * glyphInfo.u1.y;
				}
				int m = 0;
				for (int num16 = ((!bold) ? 1 : 4); m < num16; m++)
				{
					if (glyphInfo.rotatedUVs)
					{
						uvs.Add(glyphInfo.u0);
						uvs.Add(new Vector2(glyphInfo.u1.x, glyphInfo.u0.y));
						uvs.Add(glyphInfo.u1);
						uvs.Add(new Vector2(glyphInfo.u0.x, glyphInfo.u1.y));
					}
					else
					{
						uvs.Add(glyphInfo.u0);
						uvs.Add(new Vector2(glyphInfo.u0.x, glyphInfo.u1.y));
						uvs.Add(glyphInfo.u1);
						uvs.Add(new Vector2(glyphInfo.u1.x, glyphInfo.u0.y));
					}
				}
			}
			if (cols != null)
			{
				if (glyphInfo.channel == 0 || glyphInfo.channel == 15)
				{
					if (gradient)
					{
						float num17 = num8 + glyphInfo.v0.y / fontScale;
						float num18 = num8 + glyphInfo.v1.y / fontScale;
						num17 /= num8;
						num18 /= num8;
						s_c0 = Color32.op_Implicit(Color.Lerp(val, val2, num17));
						s_c1 = Color32.op_Implicit(Color.Lerp(val, val2, num18));
						int n = 0;
						for (int num19 = ((!bold) ? 1 : 4); n < num19; n++)
						{
							cols.Add(s_c0);
							cols.Add(s_c1);
							cols.Add(s_c1);
							cols.Add(s_c0);
						}
					}
					else
					{
						int num20 = 0;
						for (int num21 = (bold ? 16 : 4); num20 < num21; num20++)
						{
							cols.Add(val3);
						}
					}
				}
				else
				{
					Color val6 = Color32.op_Implicit(val3);
					val6 *= 0.49f;
					switch (glyphInfo.channel)
					{
					case 1:
						val6.b += 0.51f;
						break;
					case 2:
						val6.g += 0.51f;
						break;
					case 4:
						val6.r += 0.51f;
						break;
					case 8:
						val6.a += 0.51f;
						break;
					}
					Color32 item2 = Color32.op_Implicit(val6);
					int num22 = 0;
					for (int num23 = (bold ? 16 : 4); num22 < num23; num22++)
					{
						cols.Add(item2);
					}
				}
			}
			if (!bold)
			{
				if (!italic)
				{
					verts.Add(new Vector3(num11, num14));
					verts.Add(new Vector3(num11, num13));
					verts.Add(new Vector3(num12, num13));
					verts.Add(new Vector3(num12, num14));
				}
				else
				{
					float num24 = (float)fontSize * 0.1f * ((num13 - num14) / (float)fontSize);
					verts.Add(new Vector3(num11 - num24, num14));
					verts.Add(new Vector3(num11 + num24, num13));
					verts.Add(new Vector3(num12 + num24, num13));
					verts.Add(new Vector3(num12 - num24, num14));
				}
			}
			else
			{
				for (int num25 = 0; num25 < 4; num25++)
				{
					float num26 = mBoldOffset[num25 * 2];
					float num27 = mBoldOffset[num25 * 2 + 1];
					float num28 = num26 + (italic ? ((float)fontSize * 0.1f * ((num13 - num14) / (float)fontSize)) : 0f);
					verts.Add(new Vector3(num11 - num28, num14 + num27));
					verts.Add(new Vector3(num11 + num28, num13 + num27));
					verts.Add(new Vector3(num12 + num28, num13 + num27));
					verts.Add(new Vector3(num12 - num28, num14 + num27));
				}
			}
			if (!(underline || strike))
			{
				continue;
			}
			GlyphInfo glyphInfo2 = GetGlyph(strike ? 45 : 95, prev);
			if (glyphInfo2 == null)
			{
				continue;
			}
			if (uvs != null)
			{
				if ((Object)(object)bitmapFont != (Object)null)
				{
					glyphInfo2.u0.x = ((Rect)(ref val4)).xMin + num6 * glyphInfo2.u0.x;
					glyphInfo2.u1.x = ((Rect)(ref val4)).xMin + num6 * glyphInfo2.u1.x;
					glyphInfo2.u0.y = ((Rect)(ref val4)).yMax - num7 * glyphInfo2.u0.y;
					glyphInfo2.u1.y = ((Rect)(ref val4)).yMax - num7 * glyphInfo2.u1.y;
				}
				float num29 = (glyphInfo2.u0.x + glyphInfo2.u1.x) * 0.5f;
				uvs.Add(new Vector2(num29, glyphInfo2.u0.y));
				uvs.Add(new Vector2(num29, glyphInfo2.u1.y));
				uvs.Add(new Vector2(num29, glyphInfo2.u1.y));
				uvs.Add(new Vector2(num29, glyphInfo2.u0.y));
			}
			if (flag && strike)
			{
				num14 = (0f - num3 + glyphInfo2.v0.y) * 0.75f;
				num13 = (0f - num3 + glyphInfo2.v1.y) * 0.75f;
			}
			else
			{
				num14 = 0f - num3 + glyphInfo2.v0.y;
				num13 = 0f - num3 + glyphInfo2.v1.y;
			}
			verts.Add(new Vector3(num9, num14));
			verts.Add(new Vector3(num9, num13));
			verts.Add(new Vector3(num2, num13));
			verts.Add(new Vector3(num2, num14));
			if (gradient)
			{
				float num30 = num8 + glyphInfo2.v0.y / fontScale;
				float num31 = num8 + glyphInfo2.v1.y / fontScale;
				num30 /= num8;
				num31 /= num8;
				s_c0 = Color32.op_Implicit(Color.Lerp(val, val2, num30));
				s_c1 = Color32.op_Implicit(Color.Lerp(val, val2, num31));
				int num32 = 0;
				for (int num33 = ((!bold) ? 1 : 4); num32 < num33; num32++)
				{
					cols.Add(s_c0);
					cols.Add(s_c1);
					cols.Add(s_c1);
					cols.Add(s_c0);
				}
			}
			else
			{
				int num34 = 0;
				for (int num35 = (bold ? 16 : 4); num34 < num35; num34++)
				{
					cols.Add(val3);
				}
			}
		}
		if (alignment != Alignment.Left && size < verts.size)
		{
			Align(verts, size, num2 - finalSpacingX);
			size = verts.size;
		}
		mColors.Clear();
	}

	public static void PrintCharacterPositions(string text, BetterList<Vector3> verts, BetterList<int> indices)
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		if (string.IsNullOrEmpty(text))
		{
			text = " ";
		}
		Prepare(text);
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = (float)fontSize * fontScale * 0.5f;
		int length = text.Length;
		int size = verts.size;
		int num5 = 0;
		int prev = 0;
		for (int i = 0; i < length; i++)
		{
			num5 = text[i];
			verts.Add(new Vector3(num, 0f - num2 - num4));
			indices.Add(i);
			if (num5 == 10)
			{
				if (num > num3)
				{
					num3 = num;
				}
				if (alignment != Alignment.Left)
				{
					Align(verts, size, num - finalSpacingX);
					size = verts.size;
				}
				num = 0f;
				num2 += finalLineHeight;
				prev = 0;
				continue;
			}
			if (num5 < 32)
			{
				prev = 0;
				continue;
			}
			if (encoding && ParseSymbol(text, ref i))
			{
				i--;
				continue;
			}
			BMSymbol bMSymbol = (useSymbols ? GetSymbol(text, i, length) : null);
			if (bMSymbol == null)
			{
				float glyphWidth = GetGlyphWidth(num5, prev);
				if (glyphWidth == 0f)
				{
					continue;
				}
				glyphWidth += finalSpacingX;
				if (Mathf.RoundToInt(num + glyphWidth) > rectWidth)
				{
					if (num == 0f)
					{
						return;
					}
					if (alignment != Alignment.Left && size < verts.size)
					{
						Align(verts, size, num - finalSpacingX);
						size = verts.size;
					}
					num = glyphWidth;
					num2 += finalLineHeight;
				}
				else
				{
					num += glyphWidth;
				}
				verts.Add(new Vector3(num, 0f - num2 - num4));
				indices.Add(i + 1);
				prev = num5;
				continue;
			}
			float num6 = (float)bMSymbol.advance * fontScale + finalSpacingX;
			if (Mathf.RoundToInt(num + num6) > rectWidth)
			{
				if (num == 0f)
				{
					return;
				}
				if (alignment != Alignment.Left && size < verts.size)
				{
					Align(verts, size, num - finalSpacingX);
					size = verts.size;
				}
				num = num6;
				num2 += finalLineHeight;
			}
			else
			{
				num += num6;
			}
			verts.Add(new Vector3(num, 0f - num2 - num4));
			indices.Add(i + 1);
			i += bMSymbol.sequence.Length - 1;
			prev = 0;
		}
		if (alignment != Alignment.Left && size < verts.size)
		{
			Align(verts, size, num - finalSpacingX);
		}
	}

	public static void PrintCaretAndSelection(string text, int start, int end, BetterList<Vector3> caret, BetterList<Vector3> highlight)
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		if (string.IsNullOrEmpty(text))
		{
			text = " ";
		}
		Prepare(text);
		int num = end;
		if (start > end)
		{
			end = start;
			start = num;
		}
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = (float)fontSize * fontScale;
		int indexOffset = caret?.size ?? 0;
		int num6 = highlight?.size ?? 0;
		int length = text.Length;
		int i = 0;
		int num7 = 0;
		int prev = 0;
		bool flag = false;
		bool flag2 = false;
		Vector2 zero = Vector2.zero;
		Vector2 zero2 = Vector2.zero;
		for (; i < length; i++)
		{
			if (caret != null && !flag2 && num <= i)
			{
				flag2 = true;
				caret.Add(new Vector3(num2 - 1f, 0f - num3 - num5));
				caret.Add(new Vector3(num2 - 1f, 0f - num3));
				caret.Add(new Vector3(num2 + 1f, 0f - num3));
				caret.Add(new Vector3(num2 + 1f, 0f - num3 - num5));
			}
			num7 = text[i];
			if (num7 == 10)
			{
				if (num2 > num4)
				{
					num4 = num2;
				}
				if (caret != null && flag2)
				{
					if (alignment != Alignment.Left)
					{
						Align(caret, indexOffset, num2 - finalSpacingX);
					}
					caret = null;
				}
				if (highlight != null)
				{
					if (flag)
					{
						flag = false;
						highlight.Add(Vector2.op_Implicit(zero2));
						highlight.Add(Vector2.op_Implicit(zero));
					}
					else if (start <= i && end > i)
					{
						highlight.Add(new Vector3(num2, 0f - num3 - num5));
						highlight.Add(new Vector3(num2, 0f - num3));
						highlight.Add(new Vector3(num2 + 2f, 0f - num3));
						highlight.Add(new Vector3(num2 + 2f, 0f - num3 - num5));
					}
					if (alignment != Alignment.Left && num6 < highlight.size)
					{
						Align(highlight, num6, num2 - finalSpacingX);
						num6 = highlight.size;
					}
				}
				num2 = 0f;
				num3 += finalLineHeight;
				prev = 0;
				continue;
			}
			if (num7 < 32)
			{
				prev = 0;
				continue;
			}
			if (encoding && ParseSymbol(text, ref i))
			{
				i--;
				continue;
			}
			BMSymbol bMSymbol = (useSymbols ? GetSymbol(text, i, length) : null);
			float num8 = ((bMSymbol != null) ? ((float)bMSymbol.advance * fontScale) : GetGlyphWidth(num7, prev));
			if (num8 == 0f)
			{
				continue;
			}
			float num9 = num2;
			float num10 = num2 + num8;
			float num11 = 0f - num3 - num5;
			float num12 = 0f - num3;
			if (Mathf.RoundToInt(num10 + finalSpacingX) > rectWidth)
			{
				if (num2 == 0f)
				{
					return;
				}
				if (num2 > num4)
				{
					num4 = num2;
				}
				if (caret != null && flag2)
				{
					if (alignment != Alignment.Left)
					{
						Align(caret, indexOffset, num2 - finalSpacingX);
					}
					caret = null;
				}
				if (highlight != null)
				{
					if (flag)
					{
						flag = false;
						highlight.Add(Vector2.op_Implicit(zero2));
						highlight.Add(Vector2.op_Implicit(zero));
					}
					else if (start <= i && end > i)
					{
						highlight.Add(new Vector3(num2, 0f - num3 - num5));
						highlight.Add(new Vector3(num2, 0f - num3));
						highlight.Add(new Vector3(num2 + 2f, 0f - num3));
						highlight.Add(new Vector3(num2 + 2f, 0f - num3 - num5));
					}
					if (alignment != Alignment.Left && num6 < highlight.size)
					{
						Align(highlight, num6, num2 - finalSpacingX);
						num6 = highlight.size;
					}
				}
				num9 -= num2;
				num10 -= num2;
				num11 -= finalLineHeight;
				num12 -= finalLineHeight;
				num2 = 0f;
				num3 += finalLineHeight;
			}
			num2 += num8 + finalSpacingX;
			if (highlight != null)
			{
				if (start > i || end <= i)
				{
					if (flag)
					{
						flag = false;
						highlight.Add(Vector2.op_Implicit(zero2));
						highlight.Add(Vector2.op_Implicit(zero));
					}
				}
				else if (!flag)
				{
					flag = true;
					highlight.Add(new Vector3(num9, num11));
					highlight.Add(new Vector3(num9, num12));
				}
			}
			((Vector2)(ref zero))._002Ector(num10, num11);
			((Vector2)(ref zero2))._002Ector(num10, num12);
			prev = num7;
		}
		if (caret != null)
		{
			if (!flag2)
			{
				caret.Add(new Vector3(num2 - 1f, 0f - num3 - num5));
				caret.Add(new Vector3(num2 - 1f, 0f - num3));
				caret.Add(new Vector3(num2 + 1f, 0f - num3));
				caret.Add(new Vector3(num2 + 1f, 0f - num3 - num5));
			}
			if (alignment != Alignment.Left)
			{
				Align(caret, indexOffset, num2 - finalSpacingX);
			}
		}
		if (highlight != null)
		{
			if (flag)
			{
				highlight.Add(Vector2.op_Implicit(zero2));
				highlight.Add(Vector2.op_Implicit(zero));
			}
			else if (start < i && end == i)
			{
				highlight.Add(new Vector3(num2, 0f - num3 - num5));
				highlight.Add(new Vector3(num2, 0f - num3));
				highlight.Add(new Vector3(num2 + 2f, 0f - num3));
				highlight.Add(new Vector3(num2 + 2f, 0f - num3 - num5));
			}
			if (alignment != Alignment.Left && num6 < highlight.size)
			{
				Align(highlight, num6, num2 - finalSpacingX);
			}
		}
	}
}
