using System;
using System.IO;
using System.Text;
using MoonSharp.Interpreter.CoreLib.StringLib;
using MoonSharp.Interpreter.Interop.LuaStateInterop;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x02000D80 RID: 3456
	[MoonSharpModule(Namespace = "string")]
	public class StringModule
	{
		// Token: 0x06006223 RID: 25123 RVA: 0x00276D60 File Offset: 0x00274F60
		public static void MoonSharpInit(Table globalTable, Table stringTable)
		{
			Table table = new Table(globalTable.OwnerScript);
			table.Set("__index", DynValue.NewTable(stringTable));
			globalTable.OwnerScript.SetTypeMetatable(DataType.String, table);
		}

		// Token: 0x06006224 RID: 25124 RVA: 0x00276D98 File Offset: 0x00274F98
		[MoonSharpModuleMethod]
		public static DynValue dump(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue function = args.AsType(0, "dump", DataType.Function, false);
			DynValue result;
			try
			{
				byte[] inArray;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					executionContext.GetScript().Dump(function, memoryStream);
					memoryStream.Seek(0L, SeekOrigin.Begin);
					inArray = memoryStream.ToArray();
				}
				string str = Convert.ToBase64String(inArray);
				result = DynValue.NewString("MoonSharp_dump_b64::" + str);
			}
			catch (Exception ex)
			{
				throw new ScriptRuntimeException(ex.Message);
			}
			return result;
		}

		// Token: 0x06006225 RID: 25125 RVA: 0x00276E28 File Offset: 0x00275028
		[MoonSharpModuleMethod]
		public static DynValue @char(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			StringBuilder stringBuilder = new StringBuilder(args.Count);
			for (int i = 0; i < args.Count; i++)
			{
				DynValue dynValue = args[i];
				double num = 0.0;
				if (dynValue.Type == DataType.String)
				{
					double? num2 = dynValue.CastToNumber();
					if (num2 == null)
					{
						args.AsType(i, "char", DataType.Number, false);
					}
					else
					{
						num = num2.Value;
					}
				}
				else
				{
					args.AsType(i, "char", DataType.Number, false);
					num = dynValue.Number;
				}
				stringBuilder.Append((char)num);
			}
			return DynValue.NewString(stringBuilder.ToString());
		}

		// Token: 0x06006226 RID: 25126 RVA: 0x00276EC4 File Offset: 0x002750C4
		[MoonSharpModuleMethod]
		public static DynValue @byte(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue vs = args.AsType(0, "byte", DataType.String, false);
			DynValue vi = args.AsType(1, "byte", DataType.Number, true);
			DynValue vj = args.AsType(2, "byte", DataType.Number, true);
			return StringModule.PerformByteLike(vs, vi, vj, (int i) => StringModule.Unicode2Ascii(i));
		}

		// Token: 0x06006227 RID: 25127 RVA: 0x00276F24 File Offset: 0x00275124
		[MoonSharpModuleMethod]
		public static DynValue unicode(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue vs = args.AsType(0, "unicode", DataType.String, false);
			DynValue vi = args.AsType(1, "unicode", DataType.Number, true);
			DynValue vj = args.AsType(2, "unicode", DataType.Number, true);
			return StringModule.PerformByteLike(vs, vi, vj, (int i) => i);
		}

		// Token: 0x06006228 RID: 25128 RVA: 0x00276F83 File Offset: 0x00275183
		private static int Unicode2Ascii(int i)
		{
			if (i >= 0 && i < 255)
			{
				return i;
			}
			return 63;
		}

		// Token: 0x06006229 RID: 25129 RVA: 0x00276F98 File Offset: 0x00275198
		private static DynValue PerformByteLike(DynValue vs, DynValue vi, DynValue vj, Func<int, int> filter)
		{
			string text = StringRange.FromLuaRange(vi, vj, null).ApplyToString(vs.String);
			int length = text.Length;
			DynValue[] array = new DynValue[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = DynValue.NewNumber((double)filter((int)text[i]));
			}
			return DynValue.NewTuple(array);
		}

		// Token: 0x0600622A RID: 25130 RVA: 0x00277000 File Offset: 0x00275200
		private static int? AdjustIndex(string s, DynValue vi, int defval)
		{
			if (vi.IsNil())
			{
				return new int?(defval);
			}
			int num = (int)Math.Round(vi.Number, 0);
			if (num == 0)
			{
				return null;
			}
			if (num > 0)
			{
				return new int?(num - 1);
			}
			return new int?(s.Length - num);
		}

		// Token: 0x0600622B RID: 25131 RVA: 0x00277051 File Offset: 0x00275251
		[MoonSharpModuleMethod]
		public static DynValue len(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewNumber((double)args.AsType(0, "len", DataType.String, false).String.Length);
		}

		// Token: 0x0600622C RID: 25132 RVA: 0x00277071 File Offset: 0x00275271
		[MoonSharpModuleMethod]
		public static DynValue match(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return executionContext.EmulateClassicCall(args, "match", new Func<LuaState, int>(KopiLua_StringLib.str_match));
		}

		// Token: 0x0600622D RID: 25133 RVA: 0x0027708B File Offset: 0x0027528B
		[MoonSharpModuleMethod]
		public static DynValue gmatch(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return executionContext.EmulateClassicCall(args, "gmatch", new Func<LuaState, int>(KopiLua_StringLib.str_gmatch));
		}

		// Token: 0x0600622E RID: 25134 RVA: 0x002770A5 File Offset: 0x002752A5
		[MoonSharpModuleMethod]
		public static DynValue gsub(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return executionContext.EmulateClassicCall(args, "gsub", new Func<LuaState, int>(KopiLua_StringLib.str_gsub));
		}

		// Token: 0x0600622F RID: 25135 RVA: 0x002770BF File Offset: 0x002752BF
		[MoonSharpModuleMethod]
		public static DynValue find(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return executionContext.EmulateClassicCall(args, "find", new Func<LuaState, int>(KopiLua_StringLib.str_find));
		}

		// Token: 0x06006230 RID: 25136 RVA: 0x002770D9 File Offset: 0x002752D9
		[MoonSharpModuleMethod]
		public static DynValue lower(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewString(args.AsType(0, "lower", DataType.String, false).String.ToLower());
		}

		// Token: 0x06006231 RID: 25137 RVA: 0x002770F8 File Offset: 0x002752F8
		[MoonSharpModuleMethod]
		public static DynValue upper(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewString(args.AsType(0, "upper", DataType.String, false).String.ToUpper());
		}

		// Token: 0x06006232 RID: 25138 RVA: 0x00277118 File Offset: 0x00275318
		[MoonSharpModuleMethod]
		public static DynValue rep(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "rep", DataType.String, false);
			DynValue dynValue2 = args.AsType(1, "rep", DataType.Number, false);
			DynValue dynValue3 = args.AsType(2, "rep", DataType.String, true);
			if (string.IsNullOrEmpty(dynValue.String) || dynValue2.Number < 1.0)
			{
				return DynValue.NewString("");
			}
			string text = dynValue3.IsNotNil() ? dynValue3.String : null;
			int num = (int)dynValue2.Number;
			StringBuilder stringBuilder = new StringBuilder(dynValue.String.Length * num);
			for (int i = 0; i < num; i++)
			{
				if (i != 0 && text != null)
				{
					stringBuilder.Append(text);
				}
				stringBuilder.Append(dynValue.String);
			}
			return DynValue.NewString(stringBuilder.ToString());
		}

		// Token: 0x06006233 RID: 25139 RVA: 0x002771E6 File Offset: 0x002753E6
		[MoonSharpModuleMethod]
		public static DynValue format(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return executionContext.EmulateClassicCall(args, "format", new Func<LuaState, int>(KopiLua_StringLib.str_format));
		}

		// Token: 0x06006234 RID: 25140 RVA: 0x00277200 File Offset: 0x00275400
		[MoonSharpModuleMethod]
		public static DynValue reverse(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "reverse", DataType.String, false);
			if (string.IsNullOrEmpty(dynValue.String))
			{
				return DynValue.NewString("");
			}
			char[] array = dynValue.String.ToCharArray();
			Array.Reverse(array);
			return DynValue.NewString(new string(array));
		}

		// Token: 0x06006235 RID: 25141 RVA: 0x00277250 File Offset: 0x00275450
		[MoonSharpModuleMethod]
		public static DynValue sub(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "sub", DataType.String, false);
			DynValue start = args.AsType(1, "sub", DataType.Number, true);
			DynValue end = args.AsType(2, "sub", DataType.Number, true);
			return DynValue.NewString(StringRange.FromLuaRange(start, end, new int?(-1)).ApplyToString(dynValue.String));
		}

		// Token: 0x06006236 RID: 25142 RVA: 0x002772A8 File Offset: 0x002754A8
		[MoonSharpModuleMethod]
		public static DynValue startsWith(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "startsWith", DataType.String, true);
			DynValue dynValue2 = args.AsType(1, "startsWith", DataType.String, true);
			if (dynValue.IsNil() || dynValue2.IsNil())
			{
				return DynValue.False;
			}
			return DynValue.NewBoolean(dynValue.String.StartsWith(dynValue2.String));
		}

		// Token: 0x06006237 RID: 25143 RVA: 0x00277300 File Offset: 0x00275500
		[MoonSharpModuleMethod]
		public static DynValue endsWith(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "endsWith", DataType.String, true);
			DynValue dynValue2 = args.AsType(1, "endsWith", DataType.String, true);
			if (dynValue.IsNil() || dynValue2.IsNil())
			{
				return DynValue.False;
			}
			return DynValue.NewBoolean(dynValue.String.EndsWith(dynValue2.String));
		}

		// Token: 0x06006238 RID: 25144 RVA: 0x00277358 File Offset: 0x00275558
		[MoonSharpModuleMethod]
		public static DynValue contains(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "contains", DataType.String, true);
			DynValue dynValue2 = args.AsType(1, "contains", DataType.String, true);
			if (dynValue.IsNil() || dynValue2.IsNil())
			{
				return DynValue.False;
			}
			return DynValue.NewBoolean(dynValue.String.Contains(dynValue2.String));
		}

		// Token: 0x04005593 RID: 21907
		public const string BASE64_DUMP_HEADER = "MoonSharp_dump_b64::";
	}
}
