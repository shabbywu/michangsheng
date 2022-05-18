using System;
using System.IO;
using System.Text;
using MoonSharp.Interpreter.CoreLib.StringLib;
using MoonSharp.Interpreter.Interop.LuaStateInterop;

namespace MoonSharp.Interpreter.CoreLib
{
	// Token: 0x0200119C RID: 4508
	[MoonSharpModule(Namespace = "string")]
	public class StringModule
	{
		// Token: 0x06006E46 RID: 28230 RVA: 0x0029CEE4 File Offset: 0x0029B0E4
		public static void MoonSharpInit(Table globalTable, Table stringTable)
		{
			Table table = new Table(globalTable.OwnerScript);
			table.Set("__index", DynValue.NewTable(stringTable));
			globalTable.OwnerScript.SetTypeMetatable(DataType.String, table);
		}

		// Token: 0x06006E47 RID: 28231 RVA: 0x0029CF1C File Offset: 0x0029B11C
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

		// Token: 0x06006E48 RID: 28232 RVA: 0x0029CFAC File Offset: 0x0029B1AC
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

		// Token: 0x06006E49 RID: 28233 RVA: 0x0029D048 File Offset: 0x0029B248
		[MoonSharpModuleMethod]
		public static DynValue @byte(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue vs = args.AsType(0, "byte", DataType.String, false);
			DynValue vi = args.AsType(1, "byte", DataType.Number, true);
			DynValue vj = args.AsType(2, "byte", DataType.Number, true);
			return StringModule.PerformByteLike(vs, vi, vj, (int i) => StringModule.Unicode2Ascii(i));
		}

		// Token: 0x06006E4A RID: 28234 RVA: 0x0029D0A8 File Offset: 0x0029B2A8
		[MoonSharpModuleMethod]
		public static DynValue unicode(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue vs = args.AsType(0, "unicode", DataType.String, false);
			DynValue vi = args.AsType(1, "unicode", DataType.Number, true);
			DynValue vj = args.AsType(2, "unicode", DataType.Number, true);
			return StringModule.PerformByteLike(vs, vi, vj, (int i) => i);
		}

		// Token: 0x06006E4B RID: 28235 RVA: 0x0004B2E4 File Offset: 0x000494E4
		private static int Unicode2Ascii(int i)
		{
			if (i >= 0 && i < 255)
			{
				return i;
			}
			return 63;
		}

		// Token: 0x06006E4C RID: 28236 RVA: 0x0029D108 File Offset: 0x0029B308
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

		// Token: 0x06006E4D RID: 28237 RVA: 0x0029D170 File Offset: 0x0029B370
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

		// Token: 0x06006E4E RID: 28238 RVA: 0x0004B2F6 File Offset: 0x000494F6
		[MoonSharpModuleMethod]
		public static DynValue len(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewNumber((double)args.AsType(0, "len", DataType.String, false).String.Length);
		}

		// Token: 0x06006E4F RID: 28239 RVA: 0x0004B316 File Offset: 0x00049516
		[MoonSharpModuleMethod]
		public static DynValue match(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return executionContext.EmulateClassicCall(args, "match", new Func<LuaState, int>(KopiLua_StringLib.str_match));
		}

		// Token: 0x06006E50 RID: 28240 RVA: 0x0004B330 File Offset: 0x00049530
		[MoonSharpModuleMethod]
		public static DynValue gmatch(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return executionContext.EmulateClassicCall(args, "gmatch", new Func<LuaState, int>(KopiLua_StringLib.str_gmatch));
		}

		// Token: 0x06006E51 RID: 28241 RVA: 0x0004B34A File Offset: 0x0004954A
		[MoonSharpModuleMethod]
		public static DynValue gsub(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return executionContext.EmulateClassicCall(args, "gsub", new Func<LuaState, int>(KopiLua_StringLib.str_gsub));
		}

		// Token: 0x06006E52 RID: 28242 RVA: 0x0004B364 File Offset: 0x00049564
		[MoonSharpModuleMethod]
		public static DynValue find(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return executionContext.EmulateClassicCall(args, "find", new Func<LuaState, int>(KopiLua_StringLib.str_find));
		}

		// Token: 0x06006E53 RID: 28243 RVA: 0x0004B37E File Offset: 0x0004957E
		[MoonSharpModuleMethod]
		public static DynValue lower(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewString(args.AsType(0, "lower", DataType.String, false).String.ToLower());
		}

		// Token: 0x06006E54 RID: 28244 RVA: 0x0004B39D File Offset: 0x0004959D
		[MoonSharpModuleMethod]
		public static DynValue upper(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return DynValue.NewString(args.AsType(0, "upper", DataType.String, false).String.ToUpper());
		}

		// Token: 0x06006E55 RID: 28245 RVA: 0x0029D1C4 File Offset: 0x0029B3C4
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

		// Token: 0x06006E56 RID: 28246 RVA: 0x0004B3BC File Offset: 0x000495BC
		[MoonSharpModuleMethod]
		public static DynValue format(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return executionContext.EmulateClassicCall(args, "format", new Func<LuaState, int>(KopiLua_StringLib.str_format));
		}

		// Token: 0x06006E57 RID: 28247 RVA: 0x0029D294 File Offset: 0x0029B494
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

		// Token: 0x06006E58 RID: 28248 RVA: 0x0029D2E4 File Offset: 0x0029B4E4
		[MoonSharpModuleMethod]
		public static DynValue sub(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			DynValue dynValue = args.AsType(0, "sub", DataType.String, false);
			DynValue start = args.AsType(1, "sub", DataType.Number, true);
			DynValue end = args.AsType(2, "sub", DataType.Number, true);
			return DynValue.NewString(StringRange.FromLuaRange(start, end, new int?(-1)).ApplyToString(dynValue.String));
		}

		// Token: 0x06006E59 RID: 28249 RVA: 0x0029D33C File Offset: 0x0029B53C
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

		// Token: 0x06006E5A RID: 28250 RVA: 0x0029D394 File Offset: 0x0029B594
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

		// Token: 0x06006E5B RID: 28251 RVA: 0x0029D3EC File Offset: 0x0029B5EC
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

		// Token: 0x04006256 RID: 25174
		public const string BASE64_DUMP_HEADER = "MoonSharp_dump_b64::";
	}
}
