using System;
using System.Diagnostics;
using System.Text;
using UnityEngine;

// Token: 0x020000BE RID: 190
public static class NGUIText
{
	// Token: 0x06000745 RID: 1861 RVA: 0x0000A2D6 File Offset: 0x000084D6
	public static void Update()
	{
		NGUIText.Update(true);
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x0007BEC4 File Offset: 0x0007A0C4
	public static void Update(bool request)
	{
		NGUIText.finalSize = Mathf.RoundToInt((float)NGUIText.fontSize / NGUIText.pixelDensity);
		NGUIText.finalSpacingX = NGUIText.spacingX * NGUIText.fontScale;
		NGUIText.finalLineHeight = ((float)NGUIText.fontSize + NGUIText.spacingY) * NGUIText.fontScale;
		NGUIText.useSymbols = (NGUIText.bitmapFont != null && NGUIText.bitmapFont.hasSymbols && NGUIText.encoding && NGUIText.symbolStyle > NGUIText.SymbolStyle.None);
		if (NGUIText.dynamicFont != null && request)
		{
			NGUIText.dynamicFont.RequestCharactersInTexture(")_-", NGUIText.finalSize, NGUIText.fontStyle);
			if (!NGUIText.dynamicFont.GetCharacterInfo(')', ref NGUIText.mTempChar, NGUIText.finalSize, NGUIText.fontStyle))
			{
				NGUIText.dynamicFont.RequestCharactersInTexture("A", NGUIText.finalSize, NGUIText.fontStyle);
				if (!NGUIText.dynamicFont.GetCharacterInfo('A', ref NGUIText.mTempChar, NGUIText.finalSize, NGUIText.fontStyle))
				{
					NGUIText.baseline = 0f;
					return;
				}
			}
			float yMax = NGUIText.mTempChar.vert.yMax;
			float yMin = NGUIText.mTempChar.vert.yMin;
			NGUIText.baseline = Mathf.Round(yMax + ((float)NGUIText.finalSize - yMax + yMin) * 0.5f);
		}
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x0000A2DE File Offset: 0x000084DE
	public static void Prepare(string text)
	{
		if (NGUIText.dynamicFont != null)
		{
			NGUIText.dynamicFont.RequestCharactersInTexture(text, NGUIText.finalSize, NGUIText.fontStyle);
		}
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x0000A302 File Offset: 0x00008502
	public static BMSymbol GetSymbol(string text, int index, int textLength)
	{
		if (!(NGUIText.bitmapFont != null))
		{
			return null;
		}
		return NGUIText.bitmapFont.MatchSymbol(text, index, textLength);
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x0007C004 File Offset: 0x0007A204
	public static float GetGlyphWidth(int ch, int prev)
	{
		if (NGUIText.bitmapFont != null)
		{
			BMGlyph bmglyph = NGUIText.bitmapFont.bmFont.GetGlyph(ch);
			if (bmglyph != null)
			{
				return NGUIText.fontScale * (float)((prev != 0) ? (bmglyph.advance + bmglyph.GetKerning(prev)) : bmglyph.advance);
			}
		}
		else if (NGUIText.dynamicFont != null && NGUIText.dynamicFont.GetCharacterInfo((char)ch, ref NGUIText.mTempChar, NGUIText.finalSize, NGUIText.fontStyle))
		{
			return NGUIText.mTempChar.width * NGUIText.fontScale * NGUIText.pixelDensity;
		}
		return 0f;
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x0007C09C File Offset: 0x0007A29C
	public static NGUIText.GlyphInfo GetGlyph(int ch, int prev)
	{
		if (NGUIText.bitmapFont != null)
		{
			BMGlyph bmglyph = NGUIText.bitmapFont.bmFont.GetGlyph(ch);
			if (bmglyph != null)
			{
				int num = (prev != 0) ? bmglyph.GetKerning(prev) : 0;
				NGUIText.glyph.v0.x = (float)((prev != 0) ? (bmglyph.offsetX + num) : bmglyph.offsetX);
				NGUIText.glyph.v1.y = (float)(-(float)bmglyph.offsetY);
				NGUIText.glyph.v1.x = NGUIText.glyph.v0.x + (float)bmglyph.width;
				NGUIText.glyph.v0.y = NGUIText.glyph.v1.y - (float)bmglyph.height;
				NGUIText.glyph.u0.x = (float)bmglyph.x;
				NGUIText.glyph.u0.y = (float)(bmglyph.y + bmglyph.height);
				NGUIText.glyph.u1.x = (float)(bmglyph.x + bmglyph.width);
				NGUIText.glyph.u1.y = (float)bmglyph.y;
				NGUIText.glyph.advance = (float)(bmglyph.advance + num);
				NGUIText.glyph.channel = bmglyph.channel;
				NGUIText.glyph.rotatedUVs = false;
				if (NGUIText.fontScale != 1f)
				{
					NGUIText.glyph.v0 *= NGUIText.fontScale;
					NGUIText.glyph.v1 *= NGUIText.fontScale;
					NGUIText.glyph.advance *= NGUIText.fontScale;
				}
				return NGUIText.glyph;
			}
		}
		else if (NGUIText.dynamicFont != null && NGUIText.dynamicFont.GetCharacterInfo((char)ch, ref NGUIText.mTempChar, NGUIText.finalSize, NGUIText.fontStyle))
		{
			NGUIText.glyph.v0.x = NGUIText.mTempChar.vert.xMin;
			NGUIText.glyph.v1.x = NGUIText.glyph.v0.x + NGUIText.mTempChar.vert.width;
			NGUIText.glyph.v0.y = NGUIText.mTempChar.vert.yMax - NGUIText.baseline;
			NGUIText.glyph.v1.y = NGUIText.glyph.v0.y - NGUIText.mTempChar.vert.height;
			NGUIText.glyph.u0.x = NGUIText.mTempChar.uv.xMin;
			NGUIText.glyph.u0.y = NGUIText.mTempChar.uv.yMin;
			NGUIText.glyph.u1.x = NGUIText.mTempChar.uv.xMax;
			NGUIText.glyph.u1.y = NGUIText.mTempChar.uv.yMax;
			NGUIText.glyph.advance = NGUIText.mTempChar.width;
			NGUIText.glyph.channel = 0;
			NGUIText.glyph.rotatedUVs = NGUIText.mTempChar.flipped;
			NGUIText.glyph.v0.x = Mathf.Round(NGUIText.glyph.v0.x);
			NGUIText.glyph.v0.y = Mathf.Round(NGUIText.glyph.v0.y);
			NGUIText.glyph.v1.x = Mathf.Round(NGUIText.glyph.v1.x);
			NGUIText.glyph.v1.y = Mathf.Round(NGUIText.glyph.v1.y);
			float num2 = NGUIText.fontScale * NGUIText.pixelDensity;
			if (num2 != 1f)
			{
				NGUIText.glyph.v0 *= num2;
				NGUIText.glyph.v1 *= num2;
				NGUIText.glyph.advance *= num2;
			}
			return NGUIText.glyph;
		}
		return null;
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x0000A320 File Offset: 0x00008520
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static float ParseAlpha(string text, int index)
	{
		return Mathf.Clamp01((float)(NGUIMath.HexToDecimal(text[index + 1]) << 4 | NGUIMath.HexToDecimal(text[index + 2])) / 255f);
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x0000A34D File Offset: 0x0000854D
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static Color ParseColor(string text, int offset)
	{
		return NGUIText.ParseColor24(text, offset);
	}

	// Token: 0x0600074D RID: 1869 RVA: 0x0007C4B4 File Offset: 0x0007A6B4
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static Color ParseColor24(string text, int offset)
	{
		int num = NGUIMath.HexToDecimal(text[offset]) << 4 | NGUIMath.HexToDecimal(text[offset + 1]);
		int num2 = NGUIMath.HexToDecimal(text[offset + 2]) << 4 | NGUIMath.HexToDecimal(text[offset + 3]);
		int num3 = NGUIMath.HexToDecimal(text[offset + 4]) << 4 | NGUIMath.HexToDecimal(text[offset + 5]);
		float num4 = 0.003921569f;
		return new Color(num4 * (float)num, num4 * (float)num2, num4 * (float)num3);
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x0007C538 File Offset: 0x0007A738
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static Color ParseColor32(string text, int offset)
	{
		int num = NGUIMath.HexToDecimal(text[offset]) << 4 | NGUIMath.HexToDecimal(text[offset + 1]);
		int num2 = NGUIMath.HexToDecimal(text[offset + 2]) << 4 | NGUIMath.HexToDecimal(text[offset + 3]);
		int num3 = NGUIMath.HexToDecimal(text[offset + 4]) << 4 | NGUIMath.HexToDecimal(text[offset + 5]);
		int num4 = NGUIMath.HexToDecimal(text[offset + 6]) << 4 | NGUIMath.HexToDecimal(text[offset + 7]);
		float num5 = 0.003921569f;
		return new Color(num5 * (float)num, num5 * (float)num2, num5 * (float)num3, num5 * (float)num4);
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x0000A356 File Offset: 0x00008556
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static string EncodeColor(Color c)
	{
		return NGUIText.EncodeColor24(c);
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x0000A35E File Offset: 0x0000855E
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static string EncodeAlpha(float a)
	{
		return NGUIMath.DecimalToHex8(Mathf.Clamp(Mathf.RoundToInt(a * 255f), 0, 255));
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x0000A37C File Offset: 0x0000857C
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static string EncodeColor24(Color c)
	{
		return NGUIMath.DecimalToHex24(16777215 & NGUIMath.ColorToInt(c) >> 8);
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x0000A391 File Offset: 0x00008591
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static string EncodeColor32(Color c)
	{
		return NGUIMath.DecimalToHex32(NGUIMath.ColorToInt(c));
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x0007C5E4 File Offset: 0x0007A7E4
	public static bool ParseSymbol(string text, ref int index)
	{
		int num = 1;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		return NGUIText.ParseSymbol(text, ref index, null, false, ref num, ref flag, ref flag2, ref flag3, ref flag4);
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x0007C610 File Offset: 0x0007A810
	public static bool ParseSymbol(string text, ref int index, BetterList<Color> colors, bool premultiply, ref int sub, ref bool bold, ref bool italic, ref bool underline, ref bool strike)
	{
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
			string a = text.Substring(index, 3);
			if (a == "[b]")
			{
				bold = true;
				index += 3;
				return true;
			}
			if (a == "[i]")
			{
				italic = true;
				index += 3;
				return true;
			}
			if (a == "[u]")
			{
				underline = true;
				index += 3;
				return true;
			}
			if (a == "[s]")
			{
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
			string a2 = text.Substring(index, 4);
			if (a2 == "[/b]")
			{
				bold = false;
				index += 4;
				return true;
			}
			if (a2 == "[/i]")
			{
				italic = false;
				index += 4;
				return true;
			}
			if (a2 == "[/u]")
			{
				underline = false;
				index += 4;
				return true;
			}
			if (!(a2 == "[/s]"))
			{
				NGUIText.mAlpha = (float)(NGUIMath.HexToDecimal(text[index + 1]) << 4 | NGUIMath.HexToDecimal(text[index + 2])) / 255f;
				index += 4;
				return true;
			}
			strike = false;
			index += 4;
			return true;
		}
		else
		{
			if (index + 5 > length)
			{
				return false;
			}
			if (text[index + 4] == ']')
			{
				string a3 = text.Substring(index, 5);
				if (a3 == "[sub]")
				{
					sub = 1;
					index += 5;
					return true;
				}
				if (a3 == "[sup]")
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
				string a4 = text.Substring(index, 6);
				if (a4 == "[/sub]")
				{
					sub = 0;
					index += 6;
					return true;
				}
				if (a4 == "[/sup]")
				{
					sub = 0;
					index += 6;
					return true;
				}
				if (a4 == "[/url]")
				{
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
			else
			{
				if (index + 8 > length)
				{
					return false;
				}
				if (text[index + 7] == ']')
				{
					Color color = NGUIText.ParseColor24(text, index + 1);
					if (NGUIText.EncodeColor24(color) != text.Substring(index + 1, 6).ToUpper())
					{
						return false;
					}
					if (colors != null)
					{
						color.a = colors[colors.size - 1].a;
						if (premultiply && color.a != 1f)
						{
							color = Color.Lerp(NGUIText.mInvisible, color, color.a);
						}
						colors.Add(color);
					}
					index += 8;
					return true;
				}
				else
				{
					if (index + 10 > length)
					{
						return false;
					}
					if (text[index + 9] != ']')
					{
						return false;
					}
					Color color2 = NGUIText.ParseColor32(text, index + 1);
					if (NGUIText.EncodeColor32(color2) != text.Substring(index + 1, 8).ToUpper())
					{
						return false;
					}
					if (colors != null)
					{
						if (premultiply && color2.a != 1f)
						{
							color2 = Color.Lerp(NGUIText.mInvisible, color2, color2.a);
						}
						colors.Add(color2);
					}
					index += 10;
					return true;
				}
			}
		}
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x0007C9E0 File Offset: 0x0007ABE0
	public static string StripSymbols(string text)
	{
		if (text != null)
		{
			int i = 0;
			int length = text.Length;
			while (i < length)
			{
				if (text[i] == '[')
				{
					int num = 0;
					bool flag = false;
					bool flag2 = false;
					bool flag3 = false;
					bool flag4 = false;
					int num2 = i;
					if (NGUIText.ParseSymbol(text, ref num2, null, false, ref num, ref flag, ref flag2, ref flag3, ref flag4))
					{
						text = text.Remove(i, num2 - i);
						length = text.Length;
						continue;
					}
				}
				i++;
			}
		}
		return text;
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x0007CA4C File Offset: 0x0007AC4C
	public static void Align(BetterList<Vector3> verts, int indexOffset, float printedWidth)
	{
		switch (NGUIText.alignment)
		{
		case NGUIText.Alignment.Center:
		{
			float num = ((float)NGUIText.rectWidth - printedWidth) * 0.5f;
			if (num < 0f)
			{
				return;
			}
			int num2 = Mathf.RoundToInt((float)NGUIText.rectWidth - printedWidth);
			int num3 = Mathf.RoundToInt((float)NGUIText.rectWidth);
			bool flag = (num2 & 1) == 1;
			bool flag2 = (num3 & 1) == 1;
			if ((flag && !flag2) || (!flag && flag2))
			{
				num += 0.5f * NGUIText.fontScale;
			}
			for (int i = indexOffset; i < verts.size; i++)
			{
				Vector3[] buffer = verts.buffer;
				int num4 = i;
				buffer[num4].x = buffer[num4].x + num;
			}
			return;
		}
		case NGUIText.Alignment.Right:
		{
			float num5 = (float)NGUIText.rectWidth - printedWidth;
			if (num5 < 0f)
			{
				return;
			}
			for (int j = indexOffset; j < verts.size; j++)
			{
				Vector3[] buffer2 = verts.buffer;
				int num6 = j;
				buffer2[num6].x = buffer2[num6].x + num5;
			}
			return;
		}
		case NGUIText.Alignment.Justified:
		{
			if (printedWidth < (float)NGUIText.rectWidth * 0.65f)
			{
				return;
			}
			if (((float)NGUIText.rectWidth - printedWidth) * 0.5f < 1f)
			{
				return;
			}
			int num7 = (verts.size - indexOffset) / 4;
			if (num7 < 1)
			{
				return;
			}
			float num8 = 1f / (float)(num7 - 1);
			float num9 = (float)NGUIText.rectWidth / printedWidth;
			int k = indexOffset + 4;
			int num10 = 1;
			while (k < verts.size)
			{
				float num11 = verts.buffer[k].x;
				float num12 = verts.buffer[k + 2].x;
				float num13 = num12 - num11;
				float num14 = num11 * num9;
				float num15 = num14 + num13;
				float num16 = num12 * num9;
				float num17 = num16 - num13;
				float num18 = (float)num10 * num8;
				num11 = Mathf.Lerp(num14, num17, num18);
				num12 = Mathf.Lerp(num15, num16, num18);
				num11 = Mathf.Round(num11);
				num12 = Mathf.Round(num12);
				verts.buffer[k++].x = num11;
				verts.buffer[k++].x = num11;
				verts.buffer[k++].x = num12;
				verts.buffer[k++].x = num12;
				num10++;
			}
			return;
		}
		default:
			return;
		}
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x0007CC8C File Offset: 0x0007AE8C
	public static int GetClosestCharacter(BetterList<Vector3> verts, Vector2 pos)
	{
		float num = float.MaxValue;
		float num2 = float.MaxValue;
		int result = 0;
		for (int i = 0; i < verts.size; i++)
		{
			float num3 = Mathf.Abs(pos.y - verts[i].y);
			if (num3 <= num2)
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

	// Token: 0x06000758 RID: 1880 RVA: 0x0000A39E File Offset: 0x0000859E
	[DebuggerHidden]
	[DebuggerStepThrough]
	private static bool IsSpace(int ch)
	{
		return ch == 32 || ch == 8202 || ch == 8203;
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x0007CD0C File Offset: 0x0007AF0C
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static void EndLine(ref StringBuilder s)
	{
		int num = s.Length - 1;
		if (num > 0 && NGUIText.IsSpace((int)s[num]))
		{
			s[num] = '\n';
			return;
		}
		s.Append('\n');
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x0007CD4C File Offset: 0x0007AF4C
	[DebuggerHidden]
	[DebuggerStepThrough]
	private static void ReplaceSpaceWithNewline(ref StringBuilder s)
	{
		int num = s.Length - 1;
		if (num > 0 && NGUIText.IsSpace((int)s[num]))
		{
			s[num] = '\n';
		}
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x0007CD80 File Offset: 0x0007AF80
	public static Vector2 CalculatePrintedSize(string text)
	{
		Vector2 zero = Vector2.zero;
		if (!string.IsNullOrEmpty(text))
		{
			if (NGUIText.encoding)
			{
				text = NGUIText.StripSymbols(text);
			}
			NGUIText.Prepare(text);
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			int length = text.Length;
			int prev = 0;
			for (int i = 0; i < length; i++)
			{
				int num4 = (int)text[i];
				if (num4 == 10)
				{
					if (num > num3)
					{
						num3 = num;
					}
					num = 0f;
					num2 += NGUIText.finalLineHeight;
				}
				else if (num4 >= 32)
				{
					BMSymbol bmsymbol = NGUIText.useSymbols ? NGUIText.GetSymbol(text, i, length) : null;
					if (bmsymbol == null)
					{
						float num5 = NGUIText.GetGlyphWidth(num4, prev);
						if (num5 != 0f)
						{
							num5 += NGUIText.finalSpacingX;
							if (Mathf.RoundToInt(num + num5) > NGUIText.rectWidth)
							{
								if (num > num3)
								{
									num3 = num - NGUIText.finalSpacingX;
								}
								num = num5;
								num2 += NGUIText.finalLineHeight;
							}
							else
							{
								num += num5;
							}
							prev = num4;
						}
					}
					else
					{
						float num6 = NGUIText.finalSpacingX + (float)bmsymbol.advance * NGUIText.fontScale;
						if (Mathf.RoundToInt(num + num6) > NGUIText.rectWidth)
						{
							if (num > num3)
							{
								num3 = num - NGUIText.finalSpacingX;
							}
							num = num6;
							num2 += NGUIText.finalLineHeight;
						}
						else
						{
							num += num6;
						}
						i += bmsymbol.sequence.Length - 1;
						prev = 0;
					}
				}
			}
			zero.x = ((num > num3) ? (num - NGUIText.finalSpacingX) : num3);
			zero.y = num2 + NGUIText.finalLineHeight;
		}
		return zero;
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x0007CF0C File Offset: 0x0007B10C
	public static int CalculateOffsetToFit(string text)
	{
		if (string.IsNullOrEmpty(text) || NGUIText.rectWidth < 1)
		{
			return 0;
		}
		NGUIText.Prepare(text);
		int length = text.Length;
		int prev = 0;
		int i = 0;
		int length2 = text.Length;
		while (i < length2)
		{
			BMSymbol bmsymbol = NGUIText.useSymbols ? NGUIText.GetSymbol(text, i, length) : null;
			if (bmsymbol == null)
			{
				char c = text[i];
				float glyphWidth = NGUIText.GetGlyphWidth((int)c, prev);
				if (glyphWidth != 0f)
				{
					NGUIText.mSizes.Add(NGUIText.finalSpacingX + glyphWidth);
				}
				prev = (int)c;
			}
			else
			{
				NGUIText.mSizes.Add(NGUIText.finalSpacingX + (float)bmsymbol.advance * NGUIText.fontScale);
				int j = 0;
				int num = bmsymbol.sequence.Length - 1;
				while (j < num)
				{
					NGUIText.mSizes.Add(0f);
					j++;
				}
				i += bmsymbol.sequence.Length - 1;
				prev = 0;
			}
			i++;
		}
		float num2 = (float)NGUIText.rectWidth;
		int num3 = NGUIText.mSizes.size;
		while (num3 > 0 && num2 > 0f)
		{
			num2 -= NGUIText.mSizes[--num3];
		}
		NGUIText.mSizes.Clear();
		if (num2 < 0f)
		{
			num3++;
		}
		return num3;
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x0007D050 File Offset: 0x0007B250
	public static string GetEndOfLineThatFits(string text)
	{
		int length = text.Length;
		int num = NGUIText.CalculateOffsetToFit(text);
		return text.Substring(num, length - num);
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x0000A3B7 File Offset: 0x000085B7
	public static bool WrapText(string text, out string finalText)
	{
		return NGUIText.WrapText(text, out finalText, false);
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x0007D078 File Offset: 0x0007B278
	public static bool WrapText(string text, out string finalText, bool keepCharCount)
	{
		if (NGUIText.rectWidth < 1 || NGUIText.rectHeight < 1 || NGUIText.finalLineHeight < 1f)
		{
			finalText = "";
			return false;
		}
		float num = (NGUIText.maxLines > 0) ? Mathf.Min((float)NGUIText.rectHeight, NGUIText.finalLineHeight * (float)NGUIText.maxLines) : ((float)NGUIText.rectHeight);
		int num2 = (NGUIText.maxLines > 0) ? NGUIText.maxLines : 1000000;
		num2 = Mathf.FloorToInt(Mathf.Min((float)num2, num / NGUIText.finalLineHeight) + 0.01f);
		if (num2 == 0)
		{
			finalText = "";
			return false;
		}
		if (string.IsNullOrEmpty(text))
		{
			text = " ";
		}
		NGUIText.Prepare(text);
		StringBuilder stringBuilder = new StringBuilder();
		int length = text.Length;
		float num3 = (float)NGUIText.rectWidth;
		int num4 = 0;
		int i = 0;
		int num5 = 1;
		int prev = 0;
		bool flag = true;
		bool flag2 = true;
		bool flag3 = false;
		while (i < length)
		{
			char c = text[i];
			if (c > '⿿')
			{
				flag3 = true;
			}
			if (c == '\n')
			{
				if (num5 == num2)
				{
					break;
				}
				num3 = (float)NGUIText.rectWidth;
				if (num4 < i)
				{
					stringBuilder.Append(text.Substring(num4, i - num4 + 1));
				}
				else
				{
					stringBuilder.Append(c);
				}
				flag = true;
				num5++;
				num4 = i + 1;
				prev = 0;
			}
			else if (NGUIText.encoding && NGUIText.ParseSymbol(text, ref i))
			{
				i--;
			}
			else
			{
				BMSymbol bmsymbol = NGUIText.useSymbols ? NGUIText.GetSymbol(text, i, length) : null;
				float num6;
				if (bmsymbol == null)
				{
					float glyphWidth = NGUIText.GetGlyphWidth((int)c, prev);
					if (glyphWidth == 0f)
					{
						goto IL_2F8;
					}
					num6 = NGUIText.finalSpacingX + glyphWidth;
				}
				else
				{
					num6 = NGUIText.finalSpacingX + (float)bmsymbol.advance * NGUIText.fontScale;
				}
				num3 -= num6;
				if (NGUIText.IsSpace((int)c) && !flag3 && num4 < i)
				{
					int num7 = i - num4 + 1;
					if (num5 == num2 && num3 <= 0f && i < length)
					{
						char c2 = text[i];
						if (c2 < ' ' || NGUIText.IsSpace((int)c2))
						{
							num7--;
						}
					}
					stringBuilder.Append(text.Substring(num4, num7));
					flag = false;
					num4 = i + 1;
				}
				if (Mathf.RoundToInt(num3) < 0)
				{
					if (flag || num5 == num2)
					{
						stringBuilder.Append(text.Substring(num4, Mathf.Max(0, i - num4)));
						bool flag4 = NGUIText.IsSpace((int)c);
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
							NGUIText.ReplaceSpaceWithNewline(ref stringBuilder);
						}
						else
						{
							NGUIText.EndLine(ref stringBuilder);
						}
						flag = true;
						if (flag4)
						{
							num4 = i + 1;
							num3 = (float)NGUIText.rectWidth;
						}
						else
						{
							num4 = i;
							num3 = (float)NGUIText.rectWidth - num6;
						}
						prev = 0;
					}
					else
					{
						flag = true;
						num3 = (float)NGUIText.rectWidth;
						i = num4 - 1;
						prev = 0;
						if (num5++ == num2)
						{
							break;
						}
						if (keepCharCount)
						{
							NGUIText.ReplaceSpaceWithNewline(ref stringBuilder);
							goto IL_2F8;
						}
						NGUIText.EndLine(ref stringBuilder);
						goto IL_2F8;
					}
				}
				else
				{
					prev = (int)c;
				}
				if (bmsymbol != null)
				{
					i += bmsymbol.length - 1;
					prev = 0;
				}
			}
			IL_2F8:
			i++;
		}
		if (num4 < i)
		{
			stringBuilder.Append(text.Substring(num4, i - num4));
		}
		finalText = stringBuilder.ToString();
		return flag2 && (i == length || num5 <= Mathf.Min(NGUIText.maxLines, num2));
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x0007D3CC File Offset: 0x0007B5CC
	public static void Print(string text, BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		int size = verts.size;
		NGUIText.Prepare(text);
		NGUIText.mColors.Add(Color.white);
		NGUIText.mAlpha = 1f;
		int prev = 0;
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = (float)NGUIText.finalSize;
		Color color = NGUIText.tint * NGUIText.gradientBottom;
		Color color2 = NGUIText.tint * NGUIText.gradientTop;
		Color32 color3 = NGUIText.tint;
		int length = text.Length;
		Rect rect = default(Rect);
		float num5 = 0f;
		float num6 = 0f;
		float num7 = num4 * NGUIText.pixelDensity;
		bool flag = false;
		int num8 = 0;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		bool flag5 = false;
		if (NGUIText.bitmapFont != null)
		{
			rect = NGUIText.bitmapFont.uvRect;
			num5 = rect.width / (float)NGUIText.bitmapFont.texWidth;
			num6 = rect.height / (float)NGUIText.bitmapFont.texHeight;
		}
		for (int i = 0; i < length; i++)
		{
			int num9 = (int)text[i];
			float num10 = num;
			if (num9 == 10)
			{
				if (num > num3)
				{
					num3 = num;
				}
				if (NGUIText.alignment != NGUIText.Alignment.Left)
				{
					NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
					size = verts.size;
				}
				num = 0f;
				num2 += NGUIText.finalLineHeight;
				prev = 0;
			}
			else if (num9 < 32)
			{
				prev = num9;
			}
			else if (NGUIText.encoding && NGUIText.ParseSymbol(text, ref i, NGUIText.mColors, NGUIText.premultiply, ref num8, ref flag2, ref flag3, ref flag4, ref flag5))
			{
				Color color4 = NGUIText.tint * NGUIText.mColors[NGUIText.mColors.size - 1];
				color4.a *= NGUIText.mAlpha;
				color3 = color4;
				int j = 0;
				int num11 = NGUIText.mColors.size - 2;
				while (j < num11)
				{
					color4.a *= NGUIText.mColors[j].a;
					j++;
				}
				if (NGUIText.gradient)
				{
					color = NGUIText.gradientBottom * color4;
					color2 = NGUIText.gradientTop * color4;
				}
				i--;
			}
			else
			{
				BMSymbol bmsymbol = NGUIText.useSymbols ? NGUIText.GetSymbol(text, i, length) : null;
				if (bmsymbol != null)
				{
					float num12 = num + (float)bmsymbol.offsetX * NGUIText.fontScale;
					float num13 = num12 + (float)bmsymbol.width * NGUIText.fontScale;
					float num14 = -(num2 + (float)bmsymbol.offsetY * NGUIText.fontScale);
					float num15 = num14 - (float)bmsymbol.height * NGUIText.fontScale;
					if (Mathf.RoundToInt(num + (float)bmsymbol.advance * NGUIText.fontScale) > NGUIText.rectWidth)
					{
						if (num == 0f)
						{
							return;
						}
						if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
						{
							NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
							size = verts.size;
						}
						num12 -= num;
						num13 -= num;
						num15 -= NGUIText.finalLineHeight;
						num14 -= NGUIText.finalLineHeight;
						num = 0f;
						num2 += NGUIText.finalLineHeight;
					}
					verts.Add(new Vector3(num12, num15));
					verts.Add(new Vector3(num12, num14));
					verts.Add(new Vector3(num13, num14));
					verts.Add(new Vector3(num13, num15));
					num += NGUIText.finalSpacingX + (float)bmsymbol.advance * NGUIText.fontScale;
					i += bmsymbol.length - 1;
					prev = 0;
					if (uvs != null)
					{
						Rect uvRect = bmsymbol.uvRect;
						float xMin = uvRect.xMin;
						float yMin = uvRect.yMin;
						float xMax = uvRect.xMax;
						float yMax = uvRect.yMax;
						uvs.Add(new Vector2(xMin, yMin));
						uvs.Add(new Vector2(xMin, yMax));
						uvs.Add(new Vector2(xMax, yMax));
						uvs.Add(new Vector2(xMax, yMin));
					}
					if (cols != null)
					{
						if (NGUIText.symbolStyle == NGUIText.SymbolStyle.Colored)
						{
							for (int k = 0; k < 4; k++)
							{
								cols.Add(color3);
							}
						}
						else
						{
							Color32 item = Color.white;
							item.a = color3.a;
							for (int l = 0; l < 4; l++)
							{
								cols.Add(item);
							}
						}
					}
				}
				else
				{
					NGUIText.GlyphInfo glyphInfo = NGUIText.GetGlyph(num9, prev);
					if (glyphInfo != null)
					{
						prev = num9;
						if (num8 != 0)
						{
							NGUIText.GlyphInfo glyphInfo2 = glyphInfo;
							glyphInfo2.v0.x = glyphInfo2.v0.x * 0.75f;
							NGUIText.GlyphInfo glyphInfo3 = glyphInfo;
							glyphInfo3.v0.y = glyphInfo3.v0.y * 0.75f;
							NGUIText.GlyphInfo glyphInfo4 = glyphInfo;
							glyphInfo4.v1.x = glyphInfo4.v1.x * 0.75f;
							NGUIText.GlyphInfo glyphInfo5 = glyphInfo;
							glyphInfo5.v1.y = glyphInfo5.v1.y * 0.75f;
							if (num8 == 1)
							{
								NGUIText.GlyphInfo glyphInfo6 = glyphInfo;
								glyphInfo6.v0.y = glyphInfo6.v0.y - NGUIText.fontScale * (float)NGUIText.fontSize * 0.4f;
								NGUIText.GlyphInfo glyphInfo7 = glyphInfo;
								glyphInfo7.v1.y = glyphInfo7.v1.y - NGUIText.fontScale * (float)NGUIText.fontSize * 0.4f;
							}
							else
							{
								NGUIText.GlyphInfo glyphInfo8 = glyphInfo;
								glyphInfo8.v0.y = glyphInfo8.v0.y + NGUIText.fontScale * (float)NGUIText.fontSize * 0.05f;
								NGUIText.GlyphInfo glyphInfo9 = glyphInfo;
								glyphInfo9.v1.y = glyphInfo9.v1.y + NGUIText.fontScale * (float)NGUIText.fontSize * 0.05f;
							}
						}
						float num12 = glyphInfo.v0.x + num;
						float num15 = glyphInfo.v0.y - num2;
						float num13 = glyphInfo.v1.x + num;
						float num14 = glyphInfo.v1.y - num2;
						float num16 = glyphInfo.advance;
						if (NGUIText.finalSpacingX < 0f)
						{
							num16 += NGUIText.finalSpacingX;
						}
						if (Mathf.RoundToInt(num + num16) > NGUIText.rectWidth)
						{
							if (num == 0f)
							{
								return;
							}
							if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
							{
								NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
								size = verts.size;
							}
							num12 -= num;
							num13 -= num;
							num15 -= NGUIText.finalLineHeight;
							num14 -= NGUIText.finalLineHeight;
							num = 0f;
							num2 += NGUIText.finalLineHeight;
							num10 = 0f;
						}
						if (NGUIText.IsSpace(num9))
						{
							if (flag4)
							{
								num9 = 95;
							}
							else if (flag5)
							{
								num9 = 45;
							}
						}
						num += ((num8 == 0) ? (NGUIText.finalSpacingX + glyphInfo.advance) : ((NGUIText.finalSpacingX + glyphInfo.advance) * 0.75f));
						if (!NGUIText.IsSpace(num9))
						{
							if (uvs != null)
							{
								if (NGUIText.bitmapFont != null)
								{
									glyphInfo.u0.x = rect.xMin + num5 * glyphInfo.u0.x;
									glyphInfo.u1.x = rect.xMin + num5 * glyphInfo.u1.x;
									glyphInfo.u0.y = rect.yMax - num6 * glyphInfo.u0.y;
									glyphInfo.u1.y = rect.yMax - num6 * glyphInfo.u1.y;
								}
								int m = 0;
								int num17 = flag2 ? 4 : 1;
								while (m < num17)
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
									m++;
								}
							}
							if (cols != null)
							{
								if (glyphInfo.channel == 0 || glyphInfo.channel == 15)
								{
									if (NGUIText.gradient)
									{
										float num18 = num7 + glyphInfo.v0.y / NGUIText.fontScale;
										float num19 = num7 + glyphInfo.v1.y / NGUIText.fontScale;
										num18 /= num7;
										num19 /= num7;
										NGUIText.s_c0 = Color.Lerp(color, color2, num18);
										NGUIText.s_c1 = Color.Lerp(color, color2, num19);
										int n = 0;
										int num20 = flag2 ? 4 : 1;
										while (n < num20)
										{
											cols.Add(NGUIText.s_c0);
											cols.Add(NGUIText.s_c1);
											cols.Add(NGUIText.s_c1);
											cols.Add(NGUIText.s_c0);
											n++;
										}
									}
									else
									{
										int num21 = 0;
										int num22 = flag2 ? 16 : 4;
										while (num21 < num22)
										{
											cols.Add(color3);
											num21++;
										}
									}
								}
								else
								{
									Color color5 = color3;
									color5 *= 0.49f;
									int channel = glyphInfo.channel;
									switch (channel)
									{
									case 1:
										color5.b += 0.51f;
										break;
									case 2:
										color5.g += 0.51f;
										break;
									case 3:
										break;
									case 4:
										color5.r += 0.51f;
										break;
									default:
										if (channel == 8)
										{
											color5.a += 0.51f;
										}
										break;
									}
									Color32 item2 = color5;
									int num23 = 0;
									int num24 = flag2 ? 16 : 4;
									while (num23 < num24)
									{
										cols.Add(item2);
										num23++;
									}
								}
							}
							if (!flag2)
							{
								if (!flag3)
								{
									verts.Add(new Vector3(num12, num15));
									verts.Add(new Vector3(num12, num14));
									verts.Add(new Vector3(num13, num14));
									verts.Add(new Vector3(num13, num15));
								}
								else
								{
									float num25 = (float)NGUIText.fontSize * 0.1f * ((num14 - num15) / (float)NGUIText.fontSize);
									verts.Add(new Vector3(num12 - num25, num15));
									verts.Add(new Vector3(num12 + num25, num14));
									verts.Add(new Vector3(num13 + num25, num14));
									verts.Add(new Vector3(num13 - num25, num15));
								}
							}
							else
							{
								for (int num26 = 0; num26 < 4; num26++)
								{
									float num27 = NGUIText.mBoldOffset[num26 * 2];
									float num28 = NGUIText.mBoldOffset[num26 * 2 + 1];
									float num29 = num27 + (flag3 ? ((float)NGUIText.fontSize * 0.1f * ((num14 - num15) / (float)NGUIText.fontSize)) : 0f);
									verts.Add(new Vector3(num12 - num29, num15 + num28));
									verts.Add(new Vector3(num12 + num29, num14 + num28));
									verts.Add(new Vector3(num13 + num29, num14 + num28));
									verts.Add(new Vector3(num13 - num29, num15 + num28));
								}
							}
							if (flag4 || flag5)
							{
								NGUIText.GlyphInfo glyphInfo10 = NGUIText.GetGlyph(flag5 ? 45 : 95, prev);
								if (glyphInfo10 != null)
								{
									if (uvs != null)
									{
										if (NGUIText.bitmapFont != null)
										{
											glyphInfo10.u0.x = rect.xMin + num5 * glyphInfo10.u0.x;
											glyphInfo10.u1.x = rect.xMin + num5 * glyphInfo10.u1.x;
											glyphInfo10.u0.y = rect.yMax - num6 * glyphInfo10.u0.y;
											glyphInfo10.u1.y = rect.yMax - num6 * glyphInfo10.u1.y;
										}
										float num30 = (glyphInfo10.u0.x + glyphInfo10.u1.x) * 0.5f;
										uvs.Add(new Vector2(num30, glyphInfo10.u0.y));
										uvs.Add(new Vector2(num30, glyphInfo10.u1.y));
										uvs.Add(new Vector2(num30, glyphInfo10.u1.y));
										uvs.Add(new Vector2(num30, glyphInfo10.u0.y));
									}
									if (flag && flag5)
									{
										num15 = (-num2 + glyphInfo10.v0.y) * 0.75f;
										num14 = (-num2 + glyphInfo10.v1.y) * 0.75f;
									}
									else
									{
										num15 = -num2 + glyphInfo10.v0.y;
										num14 = -num2 + glyphInfo10.v1.y;
									}
									verts.Add(new Vector3(num10, num15));
									verts.Add(new Vector3(num10, num14));
									verts.Add(new Vector3(num, num14));
									verts.Add(new Vector3(num, num15));
									if (NGUIText.gradient)
									{
										float num31 = num7 + glyphInfo10.v0.y / NGUIText.fontScale;
										float num32 = num7 + glyphInfo10.v1.y / NGUIText.fontScale;
										num31 /= num7;
										num32 /= num7;
										NGUIText.s_c0 = Color.Lerp(color, color2, num31);
										NGUIText.s_c1 = Color.Lerp(color, color2, num32);
										int num33 = 0;
										int num34 = flag2 ? 4 : 1;
										while (num33 < num34)
										{
											cols.Add(NGUIText.s_c0);
											cols.Add(NGUIText.s_c1);
											cols.Add(NGUIText.s_c1);
											cols.Add(NGUIText.s_c0);
											num33++;
										}
									}
									else
									{
										int num35 = 0;
										int num36 = flag2 ? 16 : 4;
										while (num35 < num36)
										{
											cols.Add(color3);
											num35++;
										}
									}
								}
							}
						}
					}
				}
			}
		}
		if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
		{
			NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
			size = verts.size;
		}
		NGUIText.mColors.Clear();
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x0007E1E0 File Offset: 0x0007C3E0
	public static void PrintCharacterPositions(string text, BetterList<Vector3> verts, BetterList<int> indices)
	{
		if (string.IsNullOrEmpty(text))
		{
			text = " ";
		}
		NGUIText.Prepare(text);
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = (float)NGUIText.fontSize * NGUIText.fontScale * 0.5f;
		int length = text.Length;
		int size = verts.size;
		int prev = 0;
		for (int i = 0; i < length; i++)
		{
			int num5 = (int)text[i];
			verts.Add(new Vector3(num, -num2 - num4));
			indices.Add(i);
			if (num5 == 10)
			{
				if (num > num3)
				{
					num3 = num;
				}
				if (NGUIText.alignment != NGUIText.Alignment.Left)
				{
					NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
					size = verts.size;
				}
				num = 0f;
				num2 += NGUIText.finalLineHeight;
				prev = 0;
			}
			else if (num5 < 32)
			{
				prev = 0;
			}
			else if (NGUIText.encoding && NGUIText.ParseSymbol(text, ref i))
			{
				i--;
			}
			else
			{
				BMSymbol bmsymbol = NGUIText.useSymbols ? NGUIText.GetSymbol(text, i, length) : null;
				if (bmsymbol == null)
				{
					float num6 = NGUIText.GetGlyphWidth(num5, prev);
					if (num6 != 0f)
					{
						num6 += NGUIText.finalSpacingX;
						if (Mathf.RoundToInt(num + num6) > NGUIText.rectWidth)
						{
							if (num == 0f)
							{
								return;
							}
							if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
							{
								NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
								size = verts.size;
							}
							num = num6;
							num2 += NGUIText.finalLineHeight;
						}
						else
						{
							num += num6;
						}
						verts.Add(new Vector3(num, -num2 - num4));
						indices.Add(i + 1);
						prev = num5;
					}
				}
				else
				{
					float num7 = (float)bmsymbol.advance * NGUIText.fontScale + NGUIText.finalSpacingX;
					if (Mathf.RoundToInt(num + num7) > NGUIText.rectWidth)
					{
						if (num == 0f)
						{
							return;
						}
						if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
						{
							NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
							size = verts.size;
						}
						num = num7;
						num2 += NGUIText.finalLineHeight;
					}
					else
					{
						num += num7;
					}
					verts.Add(new Vector3(num, -num2 - num4));
					indices.Add(i + 1);
					i += bmsymbol.sequence.Length - 1;
					prev = 0;
				}
			}
		}
		if (NGUIText.alignment != NGUIText.Alignment.Left && size < verts.size)
		{
			NGUIText.Align(verts, size, num - NGUIText.finalSpacingX);
		}
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x0007E454 File Offset: 0x0007C654
	public static void PrintCaretAndSelection(string text, int start, int end, BetterList<Vector3> caret, BetterList<Vector3> highlight)
	{
		if (string.IsNullOrEmpty(text))
		{
			text = " ";
		}
		NGUIText.Prepare(text);
		int num = end;
		if (start > end)
		{
			end = start;
			start = num;
		}
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = (float)NGUIText.fontSize * NGUIText.fontScale;
		int indexOffset = (caret != null) ? caret.size : 0;
		int num6 = (highlight != null) ? highlight.size : 0;
		int length = text.Length;
		int i = 0;
		int prev = 0;
		bool flag = false;
		bool flag2 = false;
		Vector2 zero = Vector2.zero;
		Vector2 zero2 = Vector2.zero;
		while (i < length)
		{
			if (caret != null && !flag2 && num <= i)
			{
				flag2 = true;
				caret.Add(new Vector3(num2 - 1f, -num3 - num5));
				caret.Add(new Vector3(num2 - 1f, -num3));
				caret.Add(new Vector3(num2 + 1f, -num3));
				caret.Add(new Vector3(num2 + 1f, -num3 - num5));
			}
			int num7 = (int)text[i];
			if (num7 == 10)
			{
				if (num2 > num4)
				{
					num4 = num2;
				}
				if (caret != null && flag2)
				{
					if (NGUIText.alignment != NGUIText.Alignment.Left)
					{
						NGUIText.Align(caret, indexOffset, num2 - NGUIText.finalSpacingX);
					}
					caret = null;
				}
				if (highlight != null)
				{
					if (flag)
					{
						flag = false;
						highlight.Add(zero2);
						highlight.Add(zero);
					}
					else if (start <= i && end > i)
					{
						highlight.Add(new Vector3(num2, -num3 - num5));
						highlight.Add(new Vector3(num2, -num3));
						highlight.Add(new Vector3(num2 + 2f, -num3));
						highlight.Add(new Vector3(num2 + 2f, -num3 - num5));
					}
					if (NGUIText.alignment != NGUIText.Alignment.Left && num6 < highlight.size)
					{
						NGUIText.Align(highlight, num6, num2 - NGUIText.finalSpacingX);
						num6 = highlight.size;
					}
				}
				num2 = 0f;
				num3 += NGUIText.finalLineHeight;
				prev = 0;
			}
			else if (num7 < 32)
			{
				prev = 0;
			}
			else if (NGUIText.encoding && NGUIText.ParseSymbol(text, ref i))
			{
				i--;
			}
			else
			{
				BMSymbol bmsymbol = NGUIText.useSymbols ? NGUIText.GetSymbol(text, i, length) : null;
				float num8 = (bmsymbol != null) ? ((float)bmsymbol.advance * NGUIText.fontScale) : NGUIText.GetGlyphWidth(num7, prev);
				if (num8 != 0f)
				{
					float num9 = num2;
					float num10 = num2 + num8;
					float num11 = -num3 - num5;
					float num12 = -num3;
					if (Mathf.RoundToInt(num10 + NGUIText.finalSpacingX) > NGUIText.rectWidth)
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
							if (NGUIText.alignment != NGUIText.Alignment.Left)
							{
								NGUIText.Align(caret, indexOffset, num2 - NGUIText.finalSpacingX);
							}
							caret = null;
						}
						if (highlight != null)
						{
							if (flag)
							{
								flag = false;
								highlight.Add(zero2);
								highlight.Add(zero);
							}
							else if (start <= i && end > i)
							{
								highlight.Add(new Vector3(num2, -num3 - num5));
								highlight.Add(new Vector3(num2, -num3));
								highlight.Add(new Vector3(num2 + 2f, -num3));
								highlight.Add(new Vector3(num2 + 2f, -num3 - num5));
							}
							if (NGUIText.alignment != NGUIText.Alignment.Left && num6 < highlight.size)
							{
								NGUIText.Align(highlight, num6, num2 - NGUIText.finalSpacingX);
								num6 = highlight.size;
							}
						}
						num9 -= num2;
						num10 -= num2;
						num11 -= NGUIText.finalLineHeight;
						num12 -= NGUIText.finalLineHeight;
						num2 = 0f;
						num3 += NGUIText.finalLineHeight;
					}
					num2 += num8 + NGUIText.finalSpacingX;
					if (highlight != null)
					{
						if (start > i || end <= i)
						{
							if (flag)
							{
								flag = false;
								highlight.Add(zero2);
								highlight.Add(zero);
							}
						}
						else if (!flag)
						{
							flag = true;
							highlight.Add(new Vector3(num9, num11));
							highlight.Add(new Vector3(num9, num12));
						}
					}
					zero..ctor(num10, num11);
					zero2..ctor(num10, num12);
					prev = num7;
				}
			}
			i++;
		}
		if (caret != null)
		{
			if (!flag2)
			{
				caret.Add(new Vector3(num2 - 1f, -num3 - num5));
				caret.Add(new Vector3(num2 - 1f, -num3));
				caret.Add(new Vector3(num2 + 1f, -num3));
				caret.Add(new Vector3(num2 + 1f, -num3 - num5));
			}
			if (NGUIText.alignment != NGUIText.Alignment.Left)
			{
				NGUIText.Align(caret, indexOffset, num2 - NGUIText.finalSpacingX);
			}
		}
		if (highlight != null)
		{
			if (flag)
			{
				highlight.Add(zero2);
				highlight.Add(zero);
			}
			else if (start < i && end == i)
			{
				highlight.Add(new Vector3(num2, -num3 - num5));
				highlight.Add(new Vector3(num2, -num3));
				highlight.Add(new Vector3(num2 + 2f, -num3));
				highlight.Add(new Vector3(num2 + 2f, -num3 - num5));
			}
			if (NGUIText.alignment != NGUIText.Alignment.Left && num6 < highlight.size)
			{
				NGUIText.Align(highlight, num6, num2 - NGUIText.finalSpacingX);
			}
		}
	}

	// Token: 0x04000534 RID: 1332
	public static UIFont bitmapFont;

	// Token: 0x04000535 RID: 1333
	public static Font dynamicFont;

	// Token: 0x04000536 RID: 1334
	public static NGUIText.GlyphInfo glyph = new NGUIText.GlyphInfo();

	// Token: 0x04000537 RID: 1335
	public static int fontSize = 16;

	// Token: 0x04000538 RID: 1336
	public static float fontScale = 1f;

	// Token: 0x04000539 RID: 1337
	public static float pixelDensity = 1f;

	// Token: 0x0400053A RID: 1338
	public static FontStyle fontStyle = 0;

	// Token: 0x0400053B RID: 1339
	public static NGUIText.Alignment alignment = NGUIText.Alignment.Left;

	// Token: 0x0400053C RID: 1340
	public static Color tint = Color.white;

	// Token: 0x0400053D RID: 1341
	public static int rectWidth = 1000000;

	// Token: 0x0400053E RID: 1342
	public static int rectHeight = 1000000;

	// Token: 0x0400053F RID: 1343
	public static int maxLines = 0;

	// Token: 0x04000540 RID: 1344
	public static bool gradient = false;

	// Token: 0x04000541 RID: 1345
	public static Color gradientBottom = Color.white;

	// Token: 0x04000542 RID: 1346
	public static Color gradientTop = Color.white;

	// Token: 0x04000543 RID: 1347
	public static bool encoding = false;

	// Token: 0x04000544 RID: 1348
	public static float spacingX = 0f;

	// Token: 0x04000545 RID: 1349
	public static float spacingY = 0f;

	// Token: 0x04000546 RID: 1350
	public static bool premultiply = false;

	// Token: 0x04000547 RID: 1351
	public static NGUIText.SymbolStyle symbolStyle;

	// Token: 0x04000548 RID: 1352
	public static int finalSize = 0;

	// Token: 0x04000549 RID: 1353
	public static float finalSpacingX = 0f;

	// Token: 0x0400054A RID: 1354
	public static float finalLineHeight = 0f;

	// Token: 0x0400054B RID: 1355
	public static float baseline = 0f;

	// Token: 0x0400054C RID: 1356
	public static bool useSymbols = false;

	// Token: 0x0400054D RID: 1357
	private static Color mInvisible = new Color(0f, 0f, 0f, 0f);

	// Token: 0x0400054E RID: 1358
	private static BetterList<Color> mColors = new BetterList<Color>();

	// Token: 0x0400054F RID: 1359
	private static float mAlpha = 1f;

	// Token: 0x04000550 RID: 1360
	private static CharacterInfo mTempChar;

	// Token: 0x04000551 RID: 1361
	private static BetterList<float> mSizes = new BetterList<float>();

	// Token: 0x04000552 RID: 1362
	private static Color32 s_c0;

	// Token: 0x04000553 RID: 1363
	private static Color32 s_c1;

	// Token: 0x04000554 RID: 1364
	private static float[] mBoldOffset = new float[]
	{
		-0.5f,
		0f,
		0.5f,
		0f,
		0f,
		-0.5f,
		0f,
		0.5f
	};

	// Token: 0x020000BF RID: 191
	public enum Alignment
	{
		// Token: 0x04000556 RID: 1366
		Automatic,
		// Token: 0x04000557 RID: 1367
		Left,
		// Token: 0x04000558 RID: 1368
		Center,
		// Token: 0x04000559 RID: 1369
		Right,
		// Token: 0x0400055A RID: 1370
		Justified
	}

	// Token: 0x020000C0 RID: 192
	public enum SymbolStyle
	{
		// Token: 0x0400055C RID: 1372
		None,
		// Token: 0x0400055D RID: 1373
		Normal,
		// Token: 0x0400055E RID: 1374
		Colored
	}

	// Token: 0x020000C1 RID: 193
	public class GlyphInfo
	{
		// Token: 0x0400055F RID: 1375
		public Vector2 v0;

		// Token: 0x04000560 RID: 1376
		public Vector2 v1;

		// Token: 0x04000561 RID: 1377
		public Vector2 u0;

		// Token: 0x04000562 RID: 1378
		public Vector2 u1;

		// Token: 0x04000563 RID: 1379
		public float advance;

		// Token: 0x04000564 RID: 1380
		public int channel;

		// Token: 0x04000565 RID: 1381
		public bool rotatedUVs;
	}
}
