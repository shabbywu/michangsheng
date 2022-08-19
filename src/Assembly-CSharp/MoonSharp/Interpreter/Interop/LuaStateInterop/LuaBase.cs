using System;

namespace MoonSharp.Interpreter.Interop.LuaStateInterop
{
	// Token: 0x02000D35 RID: 3381
	public class LuaBase
	{
		// Token: 0x06005F02 RID: 24322 RVA: 0x00268F2B File Offset: 0x0026712B
		protected static DynValue GetArgument(LuaState L, int pos)
		{
			return L.At(pos);
		}

		// Token: 0x06005F03 RID: 24323 RVA: 0x00268F34 File Offset: 0x00267134
		protected static DynValue ArgAsType(LuaState L, int pos, DataType type, bool allowNil = false)
		{
			return LuaBase.GetArgument(L, pos).CheckType(L.FunctionName, type, pos - 1, allowNil ? (TypeValidationFlags.AllowNil | TypeValidationFlags.AutoConvert) : TypeValidationFlags.AutoConvert);
		}

		// Token: 0x06005F04 RID: 24324 RVA: 0x00268F54 File Offset: 0x00267154
		protected static int LuaType(LuaState L, int p)
		{
			switch (LuaBase.GetArgument(L, p).Type)
			{
			case DataType.Nil:
				return 0;
			case DataType.Void:
				return -1;
			case DataType.Boolean:
				return 0;
			case DataType.Number:
				return 3;
			case DataType.String:
				return 4;
			case DataType.Function:
				return 6;
			case DataType.Table:
				return 5;
			case DataType.UserData:
				return 7;
			case DataType.Thread:
				return 8;
			case DataType.ClrFunction:
				return 6;
			}
			throw new ScriptRuntimeException("Can't call LuaType on any type");
		}

		// Token: 0x06005F05 RID: 24325 RVA: 0x00268FC8 File Offset: 0x002671C8
		protected static string LuaLCheckLString(LuaState L, int argNum, out uint l)
		{
			string @string = LuaBase.ArgAsType(L, argNum, DataType.String, false).String;
			l = (uint)@string.Length;
			return @string;
		}

		// Token: 0x06005F06 RID: 24326 RVA: 0x00268FED File Offset: 0x002671ED
		protected static void LuaPushInteger(LuaState L, int val)
		{
			L.Push(DynValue.NewNumber((double)val));
		}

		// Token: 0x06005F07 RID: 24327 RVA: 0x00268FFC File Offset: 0x002671FC
		protected static int LuaToBoolean(LuaState L, int p)
		{
			if (!LuaBase.GetArgument(L, p).CastToBool())
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06005F08 RID: 24328 RVA: 0x0026900F File Offset: 0x0026720F
		protected static string LuaToLString(LuaState luaState, int p, out uint l)
		{
			return LuaBase.LuaLCheckLString(luaState, p, out l);
		}

		// Token: 0x06005F09 RID: 24329 RVA: 0x0026901C File Offset: 0x0026721C
		protected static string LuaToString(LuaState luaState, int p)
		{
			uint num;
			return LuaBase.LuaLCheckLString(luaState, p, out num);
		}

		// Token: 0x06005F0A RID: 24330 RVA: 0x00269032 File Offset: 0x00267232
		protected static void LuaLAddValue(LuaLBuffer b)
		{
			b.StringBuilder.Append(b.LuaState.Pop().ToPrintString());
		}

		// Token: 0x06005F0B RID: 24331 RVA: 0x00269050 File Offset: 0x00267250
		protected static void LuaLAddLString(LuaLBuffer b, CharPtr s, uint p)
		{
			b.StringBuilder.Append(s.ToString((int)p));
		}

		// Token: 0x06005F0C RID: 24332 RVA: 0x00269065 File Offset: 0x00267265
		protected static void LuaLAddString(LuaLBuffer b, string s)
		{
			b.StringBuilder.Append(s.ToString());
		}

		// Token: 0x06005F0D RID: 24333 RVA: 0x0026907C File Offset: 0x0026727C
		protected static int LuaLOptInteger(LuaState L, int pos, int def)
		{
			DynValue dynValue = LuaBase.ArgAsType(L, pos, DataType.Number, true);
			if (dynValue.IsNil())
			{
				return def;
			}
			return (int)dynValue.Number;
		}

		// Token: 0x06005F0E RID: 24334 RVA: 0x002690A4 File Offset: 0x002672A4
		protected static int LuaLCheckInteger(LuaState L, int pos)
		{
			return (int)LuaBase.ArgAsType(L, pos, DataType.Number, false).Number;
		}

		// Token: 0x06005F0F RID: 24335 RVA: 0x002690B5 File Offset: 0x002672B5
		protected static void LuaLArgCheck(LuaState L, bool condition, int argNum, string message)
		{
			if (!condition)
			{
				LuaBase.LuaLArgError(L, argNum, message);
			}
		}

		// Token: 0x06005F10 RID: 24336 RVA: 0x002690C2 File Offset: 0x002672C2
		protected static int LuaLCheckInt(LuaState L, int argNum)
		{
			return LuaBase.LuaLCheckInteger(L, argNum);
		}

		// Token: 0x06005F11 RID: 24337 RVA: 0x002690CB File Offset: 0x002672CB
		protected static int LuaGetTop(LuaState L)
		{
			return L.Count;
		}

		// Token: 0x06005F12 RID: 24338 RVA: 0x002690D3 File Offset: 0x002672D3
		protected static int LuaLError(LuaState luaState, string message, params object[] args)
		{
			throw new ScriptRuntimeException(message, args);
		}

		// Token: 0x06005F13 RID: 24339 RVA: 0x002690DC File Offset: 0x002672DC
		protected static void LuaLAddChar(LuaLBuffer b, char p)
		{
			b.StringBuilder.Append(p);
		}

		// Token: 0x06005F14 RID: 24340 RVA: 0x00004095 File Offset: 0x00002295
		protected static void LuaLBuffInit(LuaState L, LuaLBuffer b)
		{
		}

		// Token: 0x06005F15 RID: 24341 RVA: 0x002690EB File Offset: 0x002672EB
		protected static void LuaPushLiteral(LuaState L, string literalString)
		{
			L.Push(DynValue.NewString(literalString));
		}

		// Token: 0x06005F16 RID: 24342 RVA: 0x002690F9 File Offset: 0x002672F9
		protected static void LuaLPushResult(LuaLBuffer b)
		{
			LuaBase.LuaPushLiteral(b.LuaState, b.StringBuilder.ToString());
		}

		// Token: 0x06005F17 RID: 24343 RVA: 0x00269114 File Offset: 0x00267314
		protected static void LuaPushLString(LuaState L, CharPtr s, uint len)
		{
			string str = s.ToString((int)len);
			L.Push(DynValue.NewString(str));
		}

		// Token: 0x06005F18 RID: 24344 RVA: 0x00004095 File Offset: 0x00002295
		protected static void LuaLCheckStack(LuaState L, int n, string message)
		{
		}

		// Token: 0x06005F19 RID: 24345 RVA: 0x00269135 File Offset: 0x00267335
		protected static string LUA_QL(string p)
		{
			return "'" + p + "'";
		}

		// Token: 0x06005F1A RID: 24346 RVA: 0x00269147 File Offset: 0x00267347
		protected static void LuaPushNil(LuaState L)
		{
			L.Push(DynValue.Nil);
		}

		// Token: 0x06005F1B RID: 24347 RVA: 0x00004095 File Offset: 0x00002295
		protected static void LuaAssert(bool p)
		{
		}

		// Token: 0x06005F1C RID: 24348 RVA: 0x00269154 File Offset: 0x00267354
		protected static string LuaLTypeName(LuaState L, int p)
		{
			return L.At(p).Type.ToErrorTypeString();
		}

		// Token: 0x06005F1D RID: 24349 RVA: 0x00269168 File Offset: 0x00267368
		protected static int LuaIsString(LuaState L, int p)
		{
			DynValue dynValue = L.At(p);
			if (dynValue.Type != DataType.String && dynValue.Type != DataType.Number)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06005F1E RID: 24350 RVA: 0x00269194 File Offset: 0x00267394
		protected static void LuaPop(LuaState L, int p)
		{
			for (int i = 0; i < p; i++)
			{
				L.Pop();
			}
		}

		// Token: 0x06005F1F RID: 24351 RVA: 0x002691B4 File Offset: 0x002673B4
		protected static void LuaGetTable(LuaState L, int p)
		{
			DynValue key = L.Pop();
			DynValue dynValue = L.At(p);
			if (dynValue.Type != DataType.Table)
			{
				throw new NotImplementedException();
			}
			DynValue v = dynValue.Table.Get(key);
			L.Push(v);
		}

		// Token: 0x06005F20 RID: 24352 RVA: 0x002691F1 File Offset: 0x002673F1
		protected static int LuaLOptInt(LuaState L, int pos, int def)
		{
			return LuaBase.LuaLOptInteger(L, pos, def);
		}

		// Token: 0x06005F21 RID: 24353 RVA: 0x002691FC File Offset: 0x002673FC
		protected static CharPtr LuaLCheckString(LuaState L, int p)
		{
			uint num;
			return LuaBase.LuaLCheckLString(L, p, out num);
		}

		// Token: 0x06005F22 RID: 24354 RVA: 0x00269218 File Offset: 0x00267418
		protected static string LuaLCheckStringStr(LuaState L, int p)
		{
			uint num;
			return LuaBase.LuaLCheckLString(L, p, out num);
		}

		// Token: 0x06005F23 RID: 24355 RVA: 0x0026922E File Offset: 0x0026742E
		protected static void LuaLArgError(LuaState L, int arg, string p)
		{
			throw ScriptRuntimeException.BadArgument(arg - 1, L.FunctionName, p);
		}

		// Token: 0x06005F24 RID: 24356 RVA: 0x0026923F File Offset: 0x0026743F
		protected static double LuaLCheckNumber(LuaState L, int pos)
		{
			return LuaBase.ArgAsType(L, pos, DataType.Number, false).Number;
		}

		// Token: 0x06005F25 RID: 24357 RVA: 0x00269250 File Offset: 0x00267450
		protected static void LuaPushValue(LuaState L, int arg)
		{
			DynValue v = L.At(arg);
			L.Push(v);
		}

		// Token: 0x06005F26 RID: 24358 RVA: 0x0026926C File Offset: 0x0026746C
		protected static void LuaCall(LuaState L, int nargs, int nresults = -1)
		{
			DynValue[] topArray = L.GetTopArray(nargs);
			L.Discard(nargs);
			DynValue func = L.Pop();
			DynValue dynValue = L.ExecutionContext.Call(func, topArray);
			if (nresults != 0)
			{
				if (nresults == -1)
				{
					nresults = ((dynValue.Type == DataType.Tuple) ? dynValue.Tuple.Length : 1);
				}
				DynValue[] array;
				if (dynValue.Type != DataType.Tuple)
				{
					(array = new DynValue[1])[0] = dynValue;
				}
				else
				{
					array = dynValue.Tuple;
				}
				DynValue[] array2 = array;
				int i = 0;
				int j = 0;
				while (j < array2.Length)
				{
					if (i >= nresults)
					{
						break;
					}
					L.Push(array2[j]);
					j++;
					i++;
				}
				while (i < nresults)
				{
					L.Push(DynValue.Nil);
				}
			}
		}

		// Token: 0x06005F27 RID: 24359 RVA: 0x00269312 File Offset: 0x00267512
		protected static int memcmp(CharPtr ptr1, CharPtr ptr2, uint size)
		{
			return LuaBase.memcmp(ptr1, ptr2, (int)size);
		}

		// Token: 0x06005F28 RID: 24360 RVA: 0x0026931C File Offset: 0x0026751C
		protected static int memcmp(CharPtr ptr1, CharPtr ptr2, int size)
		{
			int i = 0;
			while (i < size)
			{
				if (ptr1[i] != ptr2[i])
				{
					if (ptr1[i] < ptr2[i])
					{
						return -1;
					}
					return 1;
				}
				else
				{
					i++;
				}
			}
			return 0;
		}

		// Token: 0x06005F29 RID: 24361 RVA: 0x0026935C File Offset: 0x0026755C
		protected static CharPtr memchr(CharPtr ptr, char c, uint count)
		{
			for (uint num = 0U; num < count; num += 1U)
			{
				if (ptr[num] == c)
				{
					return new CharPtr(ptr.chars, (int)((long)ptr.index + (long)((ulong)num)));
				}
			}
			return null;
		}

		// Token: 0x06005F2A RID: 24362 RVA: 0x00269398 File Offset: 0x00267598
		protected static CharPtr strpbrk(CharPtr str, CharPtr charset)
		{
			int num = 0;
			while (str[num] != '\0')
			{
				int num2 = 0;
				while (charset[num2] != '\0')
				{
					if (str[num] == charset[num2])
					{
						return new CharPtr(str.chars, str.index + num);
					}
					num2++;
				}
				num++;
			}
			return null;
		}

		// Token: 0x06005F2B RID: 24363 RVA: 0x002693EC File Offset: 0x002675EC
		protected static bool isalpha(char c)
		{
			return char.IsLetter(c);
		}

		// Token: 0x06005F2C RID: 24364 RVA: 0x002693F4 File Offset: 0x002675F4
		protected static bool iscntrl(char c)
		{
			return char.IsControl(c);
		}

		// Token: 0x06005F2D RID: 24365 RVA: 0x002693FC File Offset: 0x002675FC
		protected static bool isdigit(char c)
		{
			return char.IsDigit(c);
		}

		// Token: 0x06005F2E RID: 24366 RVA: 0x00269404 File Offset: 0x00267604
		protected static bool islower(char c)
		{
			return char.IsLower(c);
		}

		// Token: 0x06005F2F RID: 24367 RVA: 0x0026940C File Offset: 0x0026760C
		protected static bool ispunct(char c)
		{
			return char.IsPunctuation(c);
		}

		// Token: 0x06005F30 RID: 24368 RVA: 0x00269414 File Offset: 0x00267614
		protected static bool isspace(char c)
		{
			return c == ' ' || (c >= '\t' && c <= '\r');
		}

		// Token: 0x06005F31 RID: 24369 RVA: 0x0026942C File Offset: 0x0026762C
		protected static bool isupper(char c)
		{
			return char.IsUpper(c);
		}

		// Token: 0x06005F32 RID: 24370 RVA: 0x00269434 File Offset: 0x00267634
		protected static bool isalnum(char c)
		{
			return char.IsLetterOrDigit(c);
		}

		// Token: 0x06005F33 RID: 24371 RVA: 0x0026943C File Offset: 0x0026763C
		protected static bool isxdigit(char c)
		{
			return "0123456789ABCDEFabcdef".IndexOf(c) >= 0;
		}

		// Token: 0x06005F34 RID: 24372 RVA: 0x0026944F File Offset: 0x0026764F
		protected static bool isgraph(char c)
		{
			return !char.IsControl(c) && !char.IsWhiteSpace(c);
		}

		// Token: 0x06005F35 RID: 24373 RVA: 0x00269464 File Offset: 0x00267664
		protected static bool isalpha(int c)
		{
			return char.IsLetter((char)c);
		}

		// Token: 0x06005F36 RID: 24374 RVA: 0x0026946D File Offset: 0x0026766D
		protected static bool iscntrl(int c)
		{
			return char.IsControl((char)c);
		}

		// Token: 0x06005F37 RID: 24375 RVA: 0x00269476 File Offset: 0x00267676
		protected static bool isdigit(int c)
		{
			return char.IsDigit((char)c);
		}

		// Token: 0x06005F38 RID: 24376 RVA: 0x0026947F File Offset: 0x0026767F
		protected static bool islower(int c)
		{
			return char.IsLower((char)c);
		}

		// Token: 0x06005F39 RID: 24377 RVA: 0x00269488 File Offset: 0x00267688
		protected static bool ispunct(int c)
		{
			return (ushort)c != 32 && !LuaBase.isalnum((char)c);
		}

		// Token: 0x06005F3A RID: 24378 RVA: 0x0026949C File Offset: 0x0026769C
		protected static bool isspace(int c)
		{
			return (ushort)c == 32 || ((ushort)c >= 9 && (ushort)c <= 13);
		}

		// Token: 0x06005F3B RID: 24379 RVA: 0x002694B7 File Offset: 0x002676B7
		protected static bool isupper(int c)
		{
			return char.IsUpper((char)c);
		}

		// Token: 0x06005F3C RID: 24380 RVA: 0x002694C0 File Offset: 0x002676C0
		protected static bool isalnum(int c)
		{
			return char.IsLetterOrDigit((char)c);
		}

		// Token: 0x06005F3D RID: 24381 RVA: 0x002694C9 File Offset: 0x002676C9
		protected static bool isgraph(int c)
		{
			return !char.IsControl((char)c) && !char.IsWhiteSpace((char)c);
		}

		// Token: 0x06005F3E RID: 24382 RVA: 0x002694E0 File Offset: 0x002676E0
		protected static char tolower(char c)
		{
			return char.ToLower(c);
		}

		// Token: 0x06005F3F RID: 24383 RVA: 0x002694E8 File Offset: 0x002676E8
		protected static char toupper(char c)
		{
			return char.ToUpper(c);
		}

		// Token: 0x06005F40 RID: 24384 RVA: 0x002694F0 File Offset: 0x002676F0
		protected static char tolower(int c)
		{
			return char.ToLower((char)c);
		}

		// Token: 0x06005F41 RID: 24385 RVA: 0x002694F9 File Offset: 0x002676F9
		protected static char toupper(int c)
		{
			return char.ToUpper((char)c);
		}

		// Token: 0x06005F42 RID: 24386 RVA: 0x00269504 File Offset: 0x00267704
		protected static CharPtr strchr(CharPtr str, char c)
		{
			int num = str.index;
			while (str.chars[num] != '\0')
			{
				if (str.chars[num] == c)
				{
					return new CharPtr(str.chars, num);
				}
				num++;
			}
			return null;
		}

		// Token: 0x06005F43 RID: 24387 RVA: 0x00269544 File Offset: 0x00267744
		protected static CharPtr strcpy(CharPtr dst, CharPtr src)
		{
			int num = 0;
			while (src[num] != '\0')
			{
				dst[num] = src[num];
				num++;
			}
			dst[num] = '\0';
			return dst;
		}

		// Token: 0x06005F44 RID: 24388 RVA: 0x0026957C File Offset: 0x0026777C
		protected static CharPtr strncpy(CharPtr dst, CharPtr src, int length)
		{
			int i = 0;
			while (src[i] != '\0')
			{
				if (i >= length)
				{
					break;
				}
				dst[i] = src[i];
				i++;
			}
			while (i < length)
			{
				dst[i++] = '\0';
			}
			return dst;
		}

		// Token: 0x06005F45 RID: 24389 RVA: 0x002695C0 File Offset: 0x002677C0
		protected static int strlen(CharPtr str)
		{
			int num = 0;
			while (str[num] != '\0')
			{
				num++;
			}
			return num;
		}

		// Token: 0x06005F46 RID: 24390 RVA: 0x002695E0 File Offset: 0x002677E0
		public static void sprintf(CharPtr buffer, CharPtr str, params object[] argv)
		{
			string str2 = Tools.sprintf(str.ToString(), argv);
			LuaBase.strcpy(buffer, str2);
		}

		// Token: 0x04005469 RID: 21609
		protected const int LUA_TNONE = -1;

		// Token: 0x0400546A RID: 21610
		protected const int LUA_TNIL = 0;

		// Token: 0x0400546B RID: 21611
		protected const int LUA_TBOOLEAN = 1;

		// Token: 0x0400546C RID: 21612
		protected const int LUA_TLIGHTUSERDATA = 2;

		// Token: 0x0400546D RID: 21613
		protected const int LUA_TNUMBER = 3;

		// Token: 0x0400546E RID: 21614
		protected const int LUA_TSTRING = 4;

		// Token: 0x0400546F RID: 21615
		protected const int LUA_TTABLE = 5;

		// Token: 0x04005470 RID: 21616
		protected const int LUA_TFUNCTION = 6;

		// Token: 0x04005471 RID: 21617
		protected const int LUA_TUSERDATA = 7;

		// Token: 0x04005472 RID: 21618
		protected const int LUA_TTHREAD = 8;

		// Token: 0x04005473 RID: 21619
		protected const int LUA_MULTRET = -1;

		// Token: 0x04005474 RID: 21620
		protected const string LUA_INTFRMLEN = "l";
	}
}
