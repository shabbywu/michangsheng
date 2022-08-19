using System;
using System.Collections.Generic;
using System.Text;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x02000D7F RID: 3455
	[MoonSharpModule(Namespace = "os")]
	public class OsTimeModule
	{
		// Token: 0x06006218 RID: 25112 RVA: 0x00276614 File Offset: 0x00274814
		private static DynValue GetUnixTime(DateTime dateTime, DateTime? epoch = null)
		{
			double totalSeconds = (dateTime - (epoch ?? OsTimeModule.Epoch)).TotalSeconds;
			if (totalSeconds < 0.0)
			{
				return DynValue.Nil;
			}
			return DynValue.NewNumber(totalSeconds);
		}

		// Token: 0x06006219 RID: 25113 RVA: 0x00276664 File Offset: 0x00274864
		private static DateTime FromUnixTime(double unixtime)
		{
			TimeSpan t = TimeSpan.FromSeconds(unixtime);
			return OsTimeModule.Epoch + t;
		}

		// Token: 0x0600621A RID: 25114 RVA: 0x00276683 File Offset: 0x00274883
		[MoonSharpModuleMethod]
		public static DynValue clock(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return OsTimeModule.GetUnixTime(DateTime.UtcNow, new DateTime?(OsTimeModule.Time0));
		}

		// Token: 0x0600621B RID: 25115 RVA: 0x0027669C File Offset: 0x0027489C
		[MoonSharpModuleMethod]
		public static DynValue difftime(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "difftime", DataType.Number, false);
			DynValue dynValue2 = args.AsType(1, "difftime", DataType.Number, true);
			if (dynValue2.IsNil())
			{
				return DynValue.NewNumber(dynValue.Number);
			}
			return DynValue.NewNumber(dynValue.Number - dynValue2.Number);
		}

		// Token: 0x0600621C RID: 25116 RVA: 0x002766F0 File Offset: 0x002748F0
		[MoonSharpModuleMethod]
		public static DynValue time(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DateTime dateTime = DateTime.UtcNow;
			if (args.Count > 0)
			{
				DynValue dynValue = args.AsType(0, "time", DataType.Table, true);
				if (dynValue.Type == DataType.Table)
				{
					dateTime = OsTimeModule.ParseTimeTable(dynValue.Table);
				}
			}
			return OsTimeModule.GetUnixTime(dateTime, null);
		}

		// Token: 0x0600621D RID: 25117 RVA: 0x00276740 File Offset: 0x00274940
		private static DateTime ParseTimeTable(Table t)
		{
			int second = OsTimeModule.GetTimeTableField(t, "sec") ?? 0;
			int minute = OsTimeModule.GetTimeTableField(t, "min") ?? 0;
			int hour = OsTimeModule.GetTimeTableField(t, "hour") ?? 12;
			int? timeTableField = OsTimeModule.GetTimeTableField(t, "day");
			int? timeTableField2 = OsTimeModule.GetTimeTableField(t, "month");
			int? timeTableField3 = OsTimeModule.GetTimeTableField(t, "year");
			if (timeTableField == null)
			{
				throw new ScriptRuntimeException("field 'day' missing in date table");
			}
			if (timeTableField2 == null)
			{
				throw new ScriptRuntimeException("field 'month' missing in date table");
			}
			if (timeTableField3 == null)
			{
				throw new ScriptRuntimeException("field 'year' missing in date table");
			}
			return new DateTime(timeTableField3.Value, timeTableField2.Value, timeTableField.Value, hour, minute, second);
		}

		// Token: 0x0600621E RID: 25118 RVA: 0x00276830 File Offset: 0x00274A30
		private static int? GetTimeTableField(Table t, string key)
		{
			double? num = t.Get(key).CastToNumber();
			if (num != null)
			{
				return new int?((int)num.Value);
			}
			return null;
		}

		// Token: 0x0600621F RID: 25119 RVA: 0x0027686C File Offset: 0x00274A6C
		[MoonSharpModuleMethod]
		public static DynValue date(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DateTime dateTime = DateTime.UtcNow;
			DynValue dynValue = args.AsType(0, "date", DataType.String, true);
			DynValue dynValue2 = args.AsType(1, "date", DataType.Number, true);
			string text = dynValue.IsNil() ? "%c" : dynValue.String;
			if (dynValue2.IsNotNil())
			{
				dateTime = OsTimeModule.FromUnixTime(dynValue2.Number);
			}
			bool v = false;
			if (text.StartsWith("!"))
			{
				text = text.Substring(1);
			}
			else
			{
				try
				{
					dateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.Local);
					v = dateTime.IsDaylightSavingTime();
				}
				catch (TimeZoneNotFoundException)
				{
				}
			}
			if (text == "*t")
			{
				Table table = new Table(executionContext.GetScript());
				table.Set("year", DynValue.NewNumber((double)dateTime.Year));
				table.Set("month", DynValue.NewNumber((double)dateTime.Month));
				table.Set("day", DynValue.NewNumber((double)dateTime.Day));
				table.Set("hour", DynValue.NewNumber((double)dateTime.Hour));
				table.Set("min", DynValue.NewNumber((double)dateTime.Minute));
				table.Set("sec", DynValue.NewNumber((double)dateTime.Second));
				table.Set("wday", DynValue.NewNumber((double)(dateTime.DayOfWeek + 1)));
				table.Set("yday", DynValue.NewNumber((double)dateTime.DayOfYear));
				table.Set("isdst", DynValue.NewBoolean(v));
				return DynValue.NewTable(table);
			}
			return DynValue.NewString(OsTimeModule.StrFTime(text, dateTime));
		}

		// Token: 0x06006220 RID: 25120 RVA: 0x00276A10 File Offset: 0x00274C10
		private static string StrFTime(string format, DateTime d)
		{
			Dictionary<char, string> dictionary = new Dictionary<char, string>
			{
				{
					'a',
					"ddd"
				},
				{
					'A',
					"dddd"
				},
				{
					'b',
					"MMM"
				},
				{
					'B',
					"MMMM"
				},
				{
					'c',
					"f"
				},
				{
					'd',
					"dd"
				},
				{
					'D',
					"MM/dd/yy"
				},
				{
					'F',
					"yyyy-MM-dd"
				},
				{
					'g',
					"yy"
				},
				{
					'G',
					"yyyy"
				},
				{
					'h',
					"MMM"
				},
				{
					'H',
					"HH"
				},
				{
					'I',
					"hh"
				},
				{
					'm',
					"MM"
				},
				{
					'M',
					"mm"
				},
				{
					'p',
					"tt"
				},
				{
					'r',
					"h:mm:ss tt"
				},
				{
					'R',
					"HH:mm"
				},
				{
					'S',
					"ss"
				},
				{
					'T',
					"HH:mm:ss"
				},
				{
					'y',
					"yy"
				},
				{
					'Y',
					"yyyy"
				},
				{
					'x',
					"d"
				},
				{
					'X',
					"T"
				},
				{
					'z',
					"zzz"
				},
				{
					'Z',
					"zzz"
				}
			};
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			foreach (char c in format)
			{
				if (c == '%')
				{
					if (flag)
					{
						stringBuilder.Append('%');
						flag = false;
					}
					else
					{
						flag = true;
					}
				}
				else if (!flag)
				{
					stringBuilder.Append(c);
				}
				else if (c != 'O' && c != 'E')
				{
					flag = false;
					if (dictionary.ContainsKey(c))
					{
						stringBuilder.Append(d.ToString(dictionary[c]));
					}
					else if (c == 'e')
					{
						string text = d.ToString("%d");
						if (text.Length < 2)
						{
							text = " " + text;
						}
						stringBuilder.Append(text);
					}
					else if (c == 'n')
					{
						stringBuilder.Append('\n');
					}
					else if (c == 't')
					{
						stringBuilder.Append('\t');
					}
					else if (c == 'C')
					{
						stringBuilder.Append(d.Year / 100);
					}
					else if (c == 'j')
					{
						stringBuilder.Append(d.DayOfYear.ToString("000"));
					}
					else if (c == 'u')
					{
						int num = (int)d.DayOfWeek;
						if (num == 0)
						{
							num = 7;
						}
						stringBuilder.Append(num);
					}
					else if (c == 'w')
					{
						int dayOfWeek = (int)d.DayOfWeek;
						stringBuilder.Append(dayOfWeek);
					}
					else if (c == 'U')
					{
						stringBuilder.Append("??");
					}
					else if (c == 'V')
					{
						stringBuilder.Append("??");
					}
					else
					{
						if (c != 'W')
						{
							throw new ScriptRuntimeException("bad argument #1 to 'date' (invalid conversion specifier '{0}')", new object[]
							{
								format
							});
						}
						stringBuilder.Append("??");
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04005591 RID: 21905
		private static DateTime Time0 = DateTime.UtcNow;

		// Token: 0x04005592 RID: 21906
		private static DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	}
}
