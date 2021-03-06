using System;
using System.Globalization;
using System.Text;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Tree
{
	// Token: 0x0200109F RID: 4255
	internal static class LexerUtils
	{
		// Token: 0x060066ED RID: 26349 RVA: 0x00285D24 File Offset: 0x00283F24
		public static double ParseNumber(Token T)
		{
			string text = T.Text;
			double result;
			if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
			{
				throw new SyntaxErrorException(T, "malformed number near '{0}'", new object[]
				{
					text
				});
			}
			return result;
		}

		// Token: 0x060066EE RID: 26350 RVA: 0x00285D64 File Offset: 0x00283F64
		public static double ParseHexInteger(Token T)
		{
			string text = T.Text;
			if (text.Length < 2 || (text[0] != '0' && char.ToUpper(text[1]) != 'X'))
			{
				throw new InternalErrorException("hex numbers must start with '0x' near '{0}'.", new object[]
				{
					text
				});
			}
			ulong num;
			if (!ulong.TryParse(text.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num))
			{
				throw new SyntaxErrorException(T, "malformed number near '{0}'", new object[]
				{
					text
				});
			}
			return num;
		}

		// Token: 0x060066EF RID: 26351 RVA: 0x00285DE4 File Offset: 0x00283FE4
		public static string ReadHexProgressive(string s, ref double d, out int digits)
		{
			digits = 0;
			for (int i = 0; i < s.Length; i++)
			{
				char c = s[i];
				if (!LexerUtils.CharIsHexDigit(c))
				{
					return s.Substring(i);
				}
				int num = LexerUtils.HexDigit2Value(c);
				d *= 16.0;
				d += (double)num;
				digits++;
			}
			return string.Empty;
		}

		// Token: 0x060066F0 RID: 26352 RVA: 0x00285E48 File Offset: 0x00284048
		public static double ParseHexFloat(Token T)
		{
			string text = T.Text;
			double result;
			try
			{
				if (text.Length < 2 || (text[0] != '0' && char.ToUpper(text[1]) != 'X'))
				{
					throw new InternalErrorException("hex float must start with '0x' near '{0}'", new object[]
					{
						text
					});
				}
				text = text.Substring(2);
				double num = 0.0;
				int num2 = 0;
				int num3;
				text = LexerUtils.ReadHexProgressive(text, ref num, out num3);
				if (text.Length > 0 && text[0] == '.')
				{
					text = text.Substring(1);
					text = LexerUtils.ReadHexProgressive(text, ref num, out num2);
				}
				num2 *= -4;
				if (text.Length > 0 && char.ToUpper(text[0]) == 'P')
				{
					if (text.Length == 1)
					{
						throw new SyntaxErrorException(T, "invalid hex float format near '{0}'", new object[]
						{
							text
						});
					}
					text = text.Substring((text[1] == '+') ? 2 : 1);
					int num4 = int.Parse(text, CultureInfo.InvariantCulture);
					num2 += num4;
				}
				result = num * Math.Pow(2.0, (double)num2);
			}
			catch (FormatException)
			{
				throw new SyntaxErrorException(T, "malformed number near '{0}'", new object[]
				{
					text
				});
			}
			return result;
		}

		// Token: 0x060066F1 RID: 26353 RVA: 0x00285F80 File Offset: 0x00284180
		public static int HexDigit2Value(char c)
		{
			if (c >= '0' && c <= '9')
			{
				return (int)(c - '0');
			}
			if (c >= 'A' && c <= 'F')
			{
				return (int)('\n' + (c - 'A'));
			}
			if (c >= 'a' && c <= 'f')
			{
				return (int)('\n' + (c - 'a'));
			}
			throw new InternalErrorException("invalid hex digit near '{0}'", new object[]
			{
				c
			});
		}

		// Token: 0x060066F2 RID: 26354 RVA: 0x00046DFE File Offset: 0x00044FFE
		public static bool CharIsDigit(char c)
		{
			return c >= '0' && c <= '9';
		}

		// Token: 0x060066F3 RID: 26355 RVA: 0x00285FDC File Offset: 0x002841DC
		public static bool CharIsHexDigit(char c)
		{
			return LexerUtils.CharIsDigit(c) || c == 'a' || c == 'b' || c == 'c' || c == 'd' || c == 'e' || c == 'f' || c == 'A' || c == 'B' || c == 'C' || c == 'D' || c == 'E' || c == 'F';
		}

		// Token: 0x060066F4 RID: 26356 RVA: 0x00046E0F File Offset: 0x0004500F
		public static string AdjustLuaLongString(string str)
		{
			if (str.StartsWith("\r\n"))
			{
				str = str.Substring(2);
			}
			else if (str.StartsWith("\n"))
			{
				str = str.Substring(1);
			}
			return str;
		}

		// Token: 0x060066F5 RID: 26357 RVA: 0x00286030 File Offset: 0x00284230
		public static string UnescapeLuaString(Token token, string str)
		{
			if (!Framework.Do.StringContainsChar(str, '\\'))
			{
				return str;
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			string text = "";
			string text2 = "";
			bool flag3 = false;
			int i = 0;
			IL_3B4:
			while (i < str.Length)
			{
				char c = str[i];
				while (flag)
				{
					if (text2.Length == 0 && !flag2 && num == 0)
					{
						if (c == 'a')
						{
							stringBuilder.Append('\a');
							flag = false;
							flag3 = false;
						}
						else if (c != '\r')
						{
							if (c == '\n')
							{
								stringBuilder.Append('\n');
								flag = false;
							}
							else if (c == 'b')
							{
								stringBuilder.Append('\b');
								flag = false;
							}
							else if (c == 'f')
							{
								stringBuilder.Append('\f');
								flag = false;
							}
							else if (c == 'n')
							{
								stringBuilder.Append('\n');
								flag = false;
							}
							else if (c == 'r')
							{
								stringBuilder.Append('\r');
								flag = false;
							}
							else if (c == 't')
							{
								stringBuilder.Append('\t');
								flag = false;
							}
							else if (c == 'v')
							{
								stringBuilder.Append('\v');
								flag = false;
							}
							else if (c == '\\')
							{
								stringBuilder.Append('\\');
								flag = false;
								flag3 = false;
							}
							else if (c == '"')
							{
								stringBuilder.Append('"');
								flag = false;
								flag3 = false;
							}
							else if (c == '\'')
							{
								stringBuilder.Append('\'');
								flag = false;
								flag3 = false;
							}
							else if (c == '[')
							{
								stringBuilder.Append('[');
								flag = false;
								flag3 = false;
							}
							else if (c == ']')
							{
								stringBuilder.Append(']');
								flag = false;
								flag3 = false;
							}
							else if (c == 'x')
							{
								flag2 = true;
							}
							else if (c == 'u')
							{
								num = 1;
							}
							else if (c == 'z')
							{
								flag3 = true;
								flag = false;
							}
							else
							{
								if (!LexerUtils.CharIsDigit(c))
								{
									throw new SyntaxErrorException(token, "invalid escape sequence near '\\{0}'", new object[]
									{
										c
									});
								}
								text2 += c.ToString();
							}
						}
					}
					else if (num == 1)
					{
						if (c != '{')
						{
							throw new SyntaxErrorException(token, "'{' expected near '\\u'");
						}
						num = 2;
					}
					else if (num == 2)
					{
						if (c == '}')
						{
							int i2 = int.Parse(text2, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
							stringBuilder.Append(LexerUtils.ConvertUtf32ToChar(i2));
							num = 0;
							text2 = string.Empty;
							flag = false;
						}
						else
						{
							if (text2.Length >= 8)
							{
								throw new SyntaxErrorException(token, "'}' missing, or unicode code point too large after '\\u'");
							}
							text2 += c.ToString();
						}
					}
					else if (flag2)
					{
						if (!LexerUtils.CharIsHexDigit(c))
						{
							throw new SyntaxErrorException(token, "hexadecimal digit expected near '\\{0}{1}{2}'", new object[]
							{
								text,
								text2,
								c
							});
						}
						text2 += c.ToString();
						if (text2.Length == 2)
						{
							int i3 = int.Parse(text2, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
							stringBuilder.Append(LexerUtils.ConvertUtf32ToChar(i3));
							flag3 = false;
							flag = false;
						}
					}
					else if (text2.Length > 0)
					{
						if (LexerUtils.CharIsDigit(c))
						{
							text2 += c.ToString();
						}
						if (text2.Length == 3 || !LexerUtils.CharIsDigit(c))
						{
							int num2 = int.Parse(text2, CultureInfo.InvariantCulture);
							if (num2 > 255)
							{
								throw new SyntaxErrorException(token, "decimal escape too large near '\\{0}'", new object[]
								{
									text2
								});
							}
							stringBuilder.Append(LexerUtils.ConvertUtf32ToChar(num2));
							flag3 = false;
							flag = false;
							if (!LexerUtils.CharIsDigit(c))
							{
								continue;
							}
						}
					}
					IL_3AE:
					i++;
					goto IL_3B4;
				}
				if (c == '\\')
				{
					flag = true;
					flag2 = false;
					text2 = "";
					goto IL_3AE;
				}
				if (!flag3 || !char.IsWhiteSpace(c))
				{
					stringBuilder.Append(c);
					flag3 = false;
					goto IL_3AE;
				}
				goto IL_3AE;
			}
			if (flag && !flag2 && text2.Length > 0)
			{
				int i4 = int.Parse(text2, CultureInfo.InvariantCulture);
				stringBuilder.Append(LexerUtils.ConvertUtf32ToChar(i4));
				flag = false;
			}
			if (flag)
			{
				throw new SyntaxErrorException(token, "unfinished string near '\"{0}\"'", new object[]
				{
					stringBuilder.ToString()
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060066F6 RID: 26358 RVA: 0x00046E40 File Offset: 0x00045040
		private static string ConvertUtf32ToChar(int i)
		{
			return char.ConvertFromUtf32(i);
		}
	}
}
