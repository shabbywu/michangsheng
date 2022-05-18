using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace MoonSharp.Interpreter.Interop.LuaStateInterop
{
	// Token: 0x02001140 RID: 4416
	internal static class Tools
	{
		// Token: 0x06006B2C RID: 27436 RVA: 0x002916D4 File Offset: 0x0028F8D4
		public static bool IsNumericType(object o)
		{
			return o is byte || o is sbyte || o is short || o is ushort || o is int || o is uint || o is long || o is ulong || o is float || o is double || o is decimal;
		}

		// Token: 0x06006B2D RID: 27437 RVA: 0x0029173C File Offset: 0x0028F93C
		public static bool IsPositive(object Value, bool ZeroIsPositive)
		{
			Type type = Value.GetType();
			if (type == typeof(sbyte))
			{
				if (!ZeroIsPositive)
				{
					return (sbyte)Value > 0;
				}
				return (sbyte)Value >= 0;
			}
			else if (type == typeof(short))
			{
				if (!ZeroIsPositive)
				{
					return (short)Value > 0;
				}
				return (short)Value >= 0;
			}
			else if (type == typeof(int))
			{
				if (!ZeroIsPositive)
				{
					return (int)Value > 0;
				}
				return (int)Value >= 0;
			}
			else if (type == typeof(long))
			{
				if (!ZeroIsPositive)
				{
					return (long)Value > 0L;
				}
				return (long)Value >= 0L;
			}
			else
			{
				if (type == typeof(byte))
				{
					return ZeroIsPositive || (byte)Value > 0;
				}
				if (type == typeof(ushort))
				{
					return ZeroIsPositive || (ushort)Value > 0;
				}
				if (type == typeof(uint))
				{
					return ZeroIsPositive || (uint)Value > 0U;
				}
				if (type == typeof(ulong))
				{
					return ZeroIsPositive || (ulong)Value > 0UL;
				}
				if (type == typeof(float))
				{
					if (!ZeroIsPositive)
					{
						return (float)Value > 0f;
					}
					return (float)Value >= 0f;
				}
				else if (type == typeof(double))
				{
					if (!ZeroIsPositive)
					{
						return (double)Value > 0.0;
					}
					return (double)Value >= 0.0;
				}
				else if (type == typeof(decimal))
				{
					if (!ZeroIsPositive)
					{
						return (decimal)Value > 0m;
					}
					return (decimal)Value >= 0m;
				}
				else
				{
					if (type == typeof(char))
					{
						return ZeroIsPositive || (char)Value > '\0';
					}
					return ZeroIsPositive;
				}
			}
		}

		// Token: 0x06006B2E RID: 27438 RVA: 0x00291950 File Offset: 0x0028FB50
		public static object ToUnsigned(object Value)
		{
			Type type = Value.GetType();
			if (type == typeof(sbyte))
			{
				return (byte)((sbyte)Value);
			}
			if (type == typeof(short))
			{
				return (ushort)((short)Value);
			}
			if (type == typeof(int))
			{
				return (uint)((int)Value);
			}
			if (type == typeof(long))
			{
				return (ulong)((long)Value);
			}
			if (type == typeof(byte))
			{
				return Value;
			}
			if (type == typeof(ushort))
			{
				return Value;
			}
			if (type == typeof(uint))
			{
				return Value;
			}
			if (type == typeof(ulong))
			{
				return Value;
			}
			if (type == typeof(float))
			{
				return (uint)((float)Value);
			}
			if (type == typeof(double))
			{
				return (ulong)((double)Value);
			}
			if (type == typeof(decimal))
			{
				return (ulong)((decimal)Value);
			}
			return null;
		}

		// Token: 0x06006B2F RID: 27439 RVA: 0x00291A90 File Offset: 0x0028FC90
		public static object ToInteger(object Value, bool Round)
		{
			Type type = Value.GetType();
			if (type == typeof(sbyte))
			{
				return Value;
			}
			if (type == typeof(short))
			{
				return Value;
			}
			if (type == typeof(int))
			{
				return Value;
			}
			if (type == typeof(long))
			{
				return Value;
			}
			if (type == typeof(byte))
			{
				return Value;
			}
			if (type == typeof(ushort))
			{
				return Value;
			}
			if (type == typeof(uint))
			{
				return Value;
			}
			if (type == typeof(ulong))
			{
				return Value;
			}
			if (type == typeof(float))
			{
				return Round ? ((int)Math.Round((double)((float)Value))) : ((int)((float)Value));
			}
			if (type == typeof(double))
			{
				return Round ? ((long)Math.Round((double)Value)) : ((long)((double)Value));
			}
			if (type == typeof(decimal))
			{
				return Round ? Math.Round((decimal)Value) : ((decimal)Value);
			}
			return null;
		}

		// Token: 0x06006B30 RID: 27440 RVA: 0x00291BD4 File Offset: 0x0028FDD4
		public static long UnboxToLong(object Value, bool Round)
		{
			Type type = Value.GetType();
			if (type == typeof(sbyte))
			{
				return (long)((sbyte)Value);
			}
			if (type == typeof(short))
			{
				return (long)((short)Value);
			}
			if (type == typeof(int))
			{
				return (long)((int)Value);
			}
			if (type == typeof(long))
			{
				return (long)Value;
			}
			if (type == typeof(byte))
			{
				return (long)((ulong)((byte)Value));
			}
			if (type == typeof(ushort))
			{
				return (long)((ulong)((ushort)Value));
			}
			if (type == typeof(uint))
			{
				return (long)((ulong)((uint)Value));
			}
			if (type == typeof(ulong))
			{
				return (long)((ulong)Value);
			}
			if (type == typeof(float))
			{
				if (!Round)
				{
					return (long)((float)Value);
				}
				return (long)Math.Round((double)((float)Value));
			}
			else if (type == typeof(double))
			{
				if (!Round)
				{
					return (long)((double)Value);
				}
				return (long)Math.Round((double)Value);
			}
			else
			{
				if (!(type == typeof(decimal)))
				{
					return 0L;
				}
				if (!Round)
				{
					return (long)((decimal)Value);
				}
				return (long)Math.Round((decimal)Value);
			}
		}

		// Token: 0x06006B31 RID: 27441 RVA: 0x00049154 File Offset: 0x00047354
		public static string ReplaceMetaChars(string input)
		{
			return Regex.Replace(input, "(\\\\)(\\d{3}|[^\\d])?", new MatchEvaluator(Tools.ReplaceMetaCharsMatch));
		}

		// Token: 0x06006B32 RID: 27442 RVA: 0x00291D40 File Offset: 0x0028FF40
		private static string ReplaceMetaCharsMatch(Match m)
		{
			if (m.Groups[2].Length == 3)
			{
				return Convert.ToChar(Convert.ToByte(m.Groups[2].Value, 8)).ToString();
			}
			string value = m.Groups[2].Value;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(value);
			if (num <= 3876335077U)
			{
				if (num <= 3809224601U)
				{
					if (num != 890022063U)
					{
						if (num == 3809224601U)
						{
							if (value == "f")
							{
								return "\f";
							}
						}
					}
					else if (value == "0")
					{
						return "\0";
					}
				}
				else if (num != 3826002220U)
				{
					if (num == 3876335077U)
					{
						if (value == "b")
						{
							return "\b";
						}
					}
				}
				else if (value == "a")
				{
					return "\a";
				}
			}
			else if (num <= 4044111267U)
			{
				if (num != 3943445553U)
				{
					if (num == 4044111267U)
					{
						if (value == "t")
						{
							return "\t";
						}
					}
				}
				else if (value == "n")
				{
					return "\n";
				}
			}
			else if (num != 4077666505U)
			{
				if (num == 4144776981U)
				{
					if (value == "r")
					{
						return "\r";
					}
				}
			}
			else if (value == "v")
			{
				return "\v";
			}
			return m.Groups[2].Value;
		}

		// Token: 0x06006B33 RID: 27443 RVA: 0x0004916D File Offset: 0x0004736D
		public static void fprintf(TextWriter Destination, string Format, params object[] Parameters)
		{
			Destination.Write(Tools.sprintf(Format, Parameters));
		}

		// Token: 0x06006B34 RID: 27444 RVA: 0x00291ED4 File Offset: 0x002900D4
		public static string sprintf(string Format, params object[] Parameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			int num = 0;
			stringBuilder.Append(Format);
			Match match = Tools.r.Match(stringBuilder.ToString());
			while (match.Success)
			{
				int num2 = num;
				if (match.Groups[1] != null && match.Groups[1].Value.Length > 0)
				{
					num2 = Convert.ToInt32(match.Groups[1].Value.Substring(0, match.Groups[1].Value.Length - 1)) - 1;
				}
				bool alternate = false;
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				if (match.Groups[2] != null && match.Groups[2].Value.Length > 0)
				{
					string value = match.Groups[2].Value;
					alternate = (value.IndexOf('#') >= 0);
					flag = (value.IndexOf('-') >= 0);
					flag2 = (value.IndexOf('+') >= 0);
					flag3 = (value.IndexOf(' ') >= 0);
					flag5 = (value.IndexOf('\'') >= 0);
					if (flag2 && flag3)
					{
						flag3 = false;
					}
				}
				char c = ' ';
				int num3 = int.MinValue;
				if (match.Groups[3] != null && match.Groups[3].Value.Length > 0)
				{
					num3 = Convert.ToInt32(match.Groups[3].Value);
					flag4 = (match.Groups[3].Value[0] == '0');
				}
				if (flag4)
				{
					c = '0';
				}
				if (flag && flag4)
				{
					c = ' ';
				}
				int num4 = int.MinValue;
				if (match.Groups[4] != null && match.Groups[4].Value.Length > 0)
				{
					num4 = Convert.ToInt32(match.Groups[4].Value);
				}
				char c2 = '\0';
				if (match.Groups[5] != null && match.Groups[5].Value.Length > 0)
				{
					c2 = match.Groups[5].Value[0];
				}
				char c3 = '\0';
				if (match.Groups[6] != null && match.Groups[6].Value.Length > 0)
				{
					c3 = match.Groups[6].Value[0];
				}
				if (num4 == -2147483648 && c3 != 's' && c3 != 'c' && char.ToUpper(c3) != 'X' && c3 != 'o')
				{
					num4 = 6;
				}
				object obj;
				if (Parameters == null || num2 >= Parameters.Length)
				{
					obj = null;
				}
				else
				{
					obj = Parameters[num2];
					if (c2 == 'h')
					{
						if (obj is int)
						{
							obj = (short)((int)obj);
						}
						else if (obj is long)
						{
							obj = (short)((long)obj);
						}
						else if (obj is uint)
						{
							obj = (ushort)((uint)obj);
						}
						else if (obj is ulong)
						{
							obj = (ushort)((ulong)obj);
						}
					}
					else if (c2 == 'l')
					{
						if (obj is short)
						{
							obj = (long)((short)obj);
						}
						else if (obj is int)
						{
							obj = (long)((int)obj);
						}
						else if (obj is ushort)
						{
							obj = (ulong)((ushort)obj);
						}
						else if (obj is uint)
						{
							obj = (ulong)((uint)obj);
						}
					}
				}
				text = string.Empty;
				if (c3 <= 'E')
				{
					if (c3 != '%')
					{
						if (c3 != 'E')
						{
							goto IL_702;
						}
						text = Tools.FormatNumber("E", alternate, num3, num4, flag, flag2, flag3, c, obj);
						num++;
					}
					else
					{
						text = "%";
					}
				}
				else if (c3 != 'G')
				{
					if (c3 != 'X')
					{
						switch (c3)
						{
						case 'c':
							if (Tools.IsNumericType(obj))
							{
								text = Convert.ToChar(obj).ToString();
							}
							else if (obj is char)
							{
								text = ((char)obj).ToString();
							}
							else if (obj is string && ((string)obj).Length > 0)
							{
								text = ((string)obj)[0].ToString();
							}
							num++;
							break;
						case 'd':
						case 'i':
							text = Tools.FormatNumber(flag5 ? "n" : "d", alternate, num3, int.MinValue, flag, flag2, flag3, c, obj);
							num++;
							break;
						case 'e':
							text = Tools.FormatNumber("e", alternate, num3, num4, flag, flag2, flag3, c, obj);
							num++;
							break;
						case 'f':
							text = Tools.FormatNumber(flag5 ? "n" : "f", alternate, num3, num4, flag, flag2, flag3, c, obj);
							num++;
							break;
						case 'g':
							text = Tools.FormatNumber("g", alternate, num3, num4, flag, flag2, flag3, c, obj);
							num++;
							break;
						case 'h':
						case 'j':
						case 'k':
						case 'l':
						case 'm':
						case 'q':
						case 'r':
						case 't':
						case 'v':
						case 'w':
							goto IL_702;
						case 'n':
							text = Tools.FormatNumber("d", alternate, num3, int.MinValue, flag, flag2, flag3, c, match.Index);
							break;
						case 'o':
							text = Tools.FormatOct("o", alternate, num3, int.MinValue, flag, c, obj);
							num++;
							break;
						case 'p':
							if (obj is IntPtr)
							{
								text = "0x" + ((IntPtr)obj).ToString("x");
							}
							num++;
							break;
						case 's':
							text = obj.ToString();
							if (num4 >= 0)
							{
								text = text.Substring(0, num4);
							}
							if (num3 != -2147483648)
							{
								if (flag)
								{
									text = text.PadRight(num3, c);
								}
								else
								{
									text = text.PadLeft(num3, c);
								}
							}
							num++;
							break;
						case 'u':
							text = Tools.FormatNumber(flag5 ? "n" : "d", alternate, num3, int.MinValue, flag, false, false, c, Tools.ToUnsigned(obj));
							num++;
							break;
						case 'x':
							text = Tools.FormatHex("x", alternate, num3, num4, flag, c, obj);
							num++;
							break;
						default:
							goto IL_702;
						}
					}
					else
					{
						text = Tools.FormatHex("X", alternate, num3, num4, flag, c, obj);
						num++;
					}
				}
				else
				{
					text = Tools.FormatNumber("G", alternate, num3, num4, flag, flag2, flag3, c, obj);
					num++;
				}
				IL_70C:
				stringBuilder.Remove(match.Index, match.Length);
				stringBuilder.Insert(match.Index, text);
				match = Tools.r.Match(stringBuilder.ToString(), match.Index + text.Length);
				continue;
				IL_702:
				text = string.Empty;
				num++;
				goto IL_70C;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006B35 RID: 27445 RVA: 0x00292640 File Offset: 0x00290840
		private static string FormatOct(string NativeFormat, bool Alternate, int FieldLength, int FieldPrecision, bool Left2Right, char Padding, object Value)
		{
			string text = string.Empty;
			string format = "{0" + ((FieldLength != int.MinValue) ? ("," + (Left2Right ? "-" : string.Empty) + FieldLength.ToString()) : string.Empty) + "}";
			if (Tools.IsNumericType(Value))
			{
				text = Convert.ToString(Tools.UnboxToLong(Value, true), 8);
				if (Left2Right || Padding == ' ')
				{
					if (Alternate && text != "0")
					{
						text = "0" + text;
					}
					text = string.Format(format, text);
				}
				else
				{
					if (FieldLength != -2147483648)
					{
						text = text.PadLeft(FieldLength - ((Alternate && text != "0") ? 1 : 0), Padding);
					}
					if (Alternate && text != "0")
					{
						text = "0" + text;
					}
				}
			}
			return text;
		}

		// Token: 0x06006B36 RID: 27446 RVA: 0x00292724 File Offset: 0x00290924
		private static string FormatHex(string NativeFormat, bool Alternate, int FieldLength, int FieldPrecision, bool Left2Right, char Padding, object Value)
		{
			string text = string.Empty;
			string format = "{0" + ((FieldLength != int.MinValue) ? ("," + (Left2Right ? "-" : string.Empty) + FieldLength.ToString()) : string.Empty) + "}";
			string format2 = "{0:" + NativeFormat + ((FieldPrecision != int.MinValue) ? FieldPrecision.ToString() : string.Empty) + "}";
			if (Tools.IsNumericType(Value))
			{
				text = string.Format(format2, Value);
				if (Left2Right || Padding == ' ')
				{
					if (Alternate)
					{
						text = ((NativeFormat == "x") ? "0x" : "0X") + text;
					}
					text = string.Format(format, text);
				}
				else
				{
					if (FieldLength != -2147483648)
					{
						text = text.PadLeft(FieldLength - (Alternate ? 2 : 0), Padding);
					}
					if (Alternate)
					{
						text = ((NativeFormat == "x") ? "0x" : "0X") + text;
					}
				}
			}
			return text;
		}

		// Token: 0x06006B37 RID: 27447 RVA: 0x00292828 File Offset: 0x00290A28
		private static string FormatNumber(string NativeFormat, bool Alternate, int FieldLength, int FieldPrecision, bool Left2Right, bool PositiveSign, bool PositiveSpace, char Padding, object Value)
		{
			string text = string.Empty;
			string format = "{0" + ((FieldLength != int.MinValue) ? ("," + (Left2Right ? "-" : string.Empty) + FieldLength.ToString()) : string.Empty) + "}";
			string format2 = "{0:" + NativeFormat + ((FieldPrecision != int.MinValue) ? FieldPrecision.ToString() : "0") + "}";
			if (Tools.IsNumericType(Value))
			{
				text = string.Format(CultureInfo.InvariantCulture, format2, Value);
				if (Left2Right || Padding == ' ')
				{
					if (Tools.IsPositive(Value, true))
					{
						text = (PositiveSign ? "+" : (PositiveSpace ? " " : string.Empty)) + text;
					}
					text = string.Format(format, text);
				}
				else
				{
					if (text.StartsWith("-"))
					{
						text = text.Substring(1);
					}
					if (FieldLength != -2147483648)
					{
						if (PositiveSign)
						{
							text = text.PadLeft(FieldLength - 1, Padding);
						}
						else
						{
							text = text.PadLeft(FieldLength, Padding);
						}
					}
					if (Tools.IsPositive(Value, true))
					{
						text = (PositiveSign ? "+" : "") + text;
					}
					else
					{
						text = "-" + text;
					}
				}
			}
			return text;
		}

		// Token: 0x040060DC RID: 24796
		internal static Regex r = new Regex("\\%(\\d*\\$)?([\\'\\#\\-\\+ ]*)(\\d*)(?:\\.(\\d+))?([hl])?([dioxXucsfeEgGpn%])");
	}
}
