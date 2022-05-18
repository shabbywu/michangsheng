using System;

namespace MoonSharp.Interpreter.Interop.LuaStateInterop
{
	// Token: 0x0200113D RID: 4413
	public class LuaBase
	{
		// Token: 0x06006AD4 RID: 27348 RVA: 0x00048DBD File Offset: 0x00046FBD
		protected static DynValue GetArgument(LuaState L, int pos)
		{
			return L.At(pos);
		}

		// Token: 0x06006AD5 RID: 27349 RVA: 0x00048DC6 File Offset: 0x00046FC6
		protected static DynValue ArgAsType(LuaState L, int pos, DataType type, bool allowNil = false)
		{
			return LuaBase.GetArgument(L, pos).CheckType(L.FunctionName, type, pos - 1, allowNil ? (TypeValidationFlags.AllowNil | TypeValidationFlags.AutoConvert) : TypeValidationFlags.AutoConvert);
		}

		// Token: 0x06006AD6 RID: 27350 RVA: 0x002911DC File Offset: 0x0028F3DC
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

		// Token: 0x06006AD7 RID: 27351 RVA: 0x00291250 File Offset: 0x0028F450
		protected static string LuaLCheckLString(LuaState L, int argNum, out uint l)
		{
			string @string = LuaBase.ArgAsType(L, argNum, DataType.String, false).String;
			l = (uint)@string.Length;
			return @string;
		}

		// Token: 0x06006AD8 RID: 27352 RVA: 0x00048DE5 File Offset: 0x00046FE5
		protected static void LuaPushInteger(LuaState L, int val)
		{
			L.Push(DynValue.NewNumber((double)val));
		}

		// Token: 0x06006AD9 RID: 27353 RVA: 0x00048DF4 File Offset: 0x00046FF4
		protected static int LuaToBoolean(LuaState L, int p)
		{
			if (!LuaBase.GetArgument(L, p).CastToBool())
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06006ADA RID: 27354 RVA: 0x00048E07 File Offset: 0x00047007
		protected static string LuaToLString(LuaState luaState, int p, out uint l)
		{
			return LuaBase.LuaLCheckLString(luaState, p, out l);
		}

		// Token: 0x06006ADB RID: 27355 RVA: 0x00291278 File Offset: 0x0028F478
		protected static string LuaToString(LuaState luaState, int p)
		{
			uint num;
			return LuaBase.LuaLCheckLString(luaState, p, out num);
		}

		// Token: 0x06006ADC RID: 27356 RVA: 0x00048E11 File Offset: 0x00047011
		protected static void LuaLAddValue(LuaLBuffer b)
		{
			b.StringBuilder.Append(b.LuaState.Pop().ToPrintString());
		}

		// Token: 0x06006ADD RID: 27357 RVA: 0x00048E2F File Offset: 0x0004702F
		protected static void LuaLAddLString(LuaLBuffer b, CharPtr s, uint p)
		{
			b.StringBuilder.Append(s.ToString((int)p));
		}

		// Token: 0x06006ADE RID: 27358 RVA: 0x00048E44 File Offset: 0x00047044
		protected static void LuaLAddString(LuaLBuffer b, string s)
		{
			b.StringBuilder.Append(s.ToString());
		}

		// Token: 0x06006ADF RID: 27359 RVA: 0x00291290 File Offset: 0x0028F490
		protected static int LuaLOptInteger(LuaState L, int pos, int def)
		{
			DynValue dynValue = LuaBase.ArgAsType(L, pos, DataType.Number, true);
			if (dynValue.IsNil())
			{
				return def;
			}
			return (int)dynValue.Number;
		}

		// Token: 0x06006AE0 RID: 27360 RVA: 0x00048E58 File Offset: 0x00047058
		protected static int LuaLCheckInteger(LuaState L, int pos)
		{
			return (int)LuaBase.ArgAsType(L, pos, DataType.Number, false).Number;
		}

		// Token: 0x06006AE1 RID: 27361 RVA: 0x00048E69 File Offset: 0x00047069
		protected static void LuaLArgCheck(LuaState L, bool condition, int argNum, string message)
		{
			if (!condition)
			{
				LuaBase.LuaLArgError(L, argNum, message);
			}
		}

		// Token: 0x06006AE2 RID: 27362 RVA: 0x00048E76 File Offset: 0x00047076
		protected static int LuaLCheckInt(LuaState L, int argNum)
		{
			return LuaBase.LuaLCheckInteger(L, argNum);
		}

		// Token: 0x06006AE3 RID: 27363 RVA: 0x00048E7F File Offset: 0x0004707F
		protected static int LuaGetTop(LuaState L)
		{
			return L.Count;
		}

		// Token: 0x06006AE4 RID: 27364 RVA: 0x00048E87 File Offset: 0x00047087
		protected static int LuaLError(LuaState luaState, string message, params object[] args)
		{
			throw new ScriptRuntimeException(message, args);
		}

		// Token: 0x06006AE5 RID: 27365 RVA: 0x00048E90 File Offset: 0x00047090
		protected static void LuaLAddChar(LuaLBuffer b, char p)
		{
			b.StringBuilder.Append(p);
		}

		// Token: 0x06006AE6 RID: 27366 RVA: 0x000042DD File Offset: 0x000024DD
		protected static void LuaLBuffInit(LuaState L, LuaLBuffer b)
		{
		}

		// Token: 0x06006AE7 RID: 27367 RVA: 0x00048E9F File Offset: 0x0004709F
		protected static void LuaPushLiteral(LuaState L, string literalString)
		{
			L.Push(DynValue.NewString(literalString));
		}

		// Token: 0x06006AE8 RID: 27368 RVA: 0x00048EAD File Offset: 0x000470AD
		protected static void LuaLPushResult(LuaLBuffer b)
		{
			LuaBase.LuaPushLiteral(b.LuaState, b.StringBuilder.ToString());
		}

		// Token: 0x06006AE9 RID: 27369 RVA: 0x002912B8 File Offset: 0x0028F4B8
		protected static void LuaPushLString(LuaState L, CharPtr s, uint len)
		{
			string str = s.ToString((int)len);
			L.Push(DynValue.NewString(str));
		}

		// Token: 0x06006AEA RID: 27370 RVA: 0x000042DD File Offset: 0x000024DD
		protected static void LuaLCheckStack(LuaState L, int n, string message)
		{
		}

		// Token: 0x06006AEB RID: 27371 RVA: 0x00048EC5 File Offset: 0x000470C5
		protected static string LUA_QL(string p)
		{
			return "'" + p + "'";
		}

		// Token: 0x06006AEC RID: 27372 RVA: 0x00048ED7 File Offset: 0x000470D7
		protected static void LuaPushNil(LuaState L)
		{
			L.Push(DynValue.Nil);
		}

		// Token: 0x06006AED RID: 27373 RVA: 0x000042DD File Offset: 0x000024DD
		protected static void LuaAssert(bool p)
		{
		}

		// Token: 0x06006AEE RID: 27374 RVA: 0x00048EE4 File Offset: 0x000470E4
		protected static string LuaLTypeName(LuaState L, int p)
		{
			return L.At(p).Type.ToErrorTypeString();
		}

		// Token: 0x06006AEF RID: 27375 RVA: 0x002912DC File Offset: 0x0028F4DC
		protected static int LuaIsString(LuaState L, int p)
		{
			DynValue dynValue = L.At(p);
			if (dynValue.Type != DataType.String && dynValue.Type != DataType.Number)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06006AF0 RID: 27376 RVA: 0x00291308 File Offset: 0x0028F508
		protected static void LuaPop(LuaState L, int p)
		{
			for (int i = 0; i < p; i++)
			{
				L.Pop();
			}
		}

		// Token: 0x06006AF1 RID: 27377 RVA: 0x00291328 File Offset: 0x0028F528
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

		// Token: 0x06006AF2 RID: 27378 RVA: 0x00048EF7 File Offset: 0x000470F7
		protected static int LuaLOptInt(LuaState L, int pos, int def)
		{
			return LuaBase.LuaLOptInteger(L, pos, def);
		}

		// Token: 0x06006AF3 RID: 27379 RVA: 0x00291368 File Offset: 0x0028F568
		protected static CharPtr LuaLCheckString(LuaState L, int p)
		{
			uint num;
			return LuaBase.LuaLCheckLString(L, p, out num);
		}

		// Token: 0x06006AF4 RID: 27380 RVA: 0x00291278 File Offset: 0x0028F478
		protected static string LuaLCheckStringStr(LuaState L, int p)
		{
			uint num;
			return LuaBase.LuaLCheckLString(L, p, out num);
		}

		// Token: 0x06006AF5 RID: 27381 RVA: 0x00048F01 File Offset: 0x00047101
		protected static void LuaLArgError(LuaState L, int arg, string p)
		{
			throw ScriptRuntimeException.BadArgument(arg - 1, L.FunctionName, p);
		}

		// Token: 0x06006AF6 RID: 27382 RVA: 0x00048F12 File Offset: 0x00047112
		protected static double LuaLCheckNumber(LuaState L, int pos)
		{
			return LuaBase.ArgAsType(L, pos, DataType.Number, false).Number;
		}

		// Token: 0x06006AF7 RID: 27383 RVA: 0x00291384 File Offset: 0x0028F584
		protected static void LuaPushValue(LuaState L, int arg)
		{
			DynValue v = L.At(arg);
			L.Push(v);
		}

		// Token: 0x06006AF8 RID: 27384 RVA: 0x002913A0 File Offset: 0x0028F5A0
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

		// Token: 0x06006AF9 RID: 27385 RVA: 0x00048F22 File Offset: 0x00047122
		protected static int memcmp(CharPtr ptr1, CharPtr ptr2, uint size)
		{
			return LuaBase.memcmp(ptr1, ptr2, (int)size);
		}

		// Token: 0x06006AFA RID: 27386 RVA: 0x00291448 File Offset: 0x0028F648
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

		// Token: 0x06006AFB RID: 27387 RVA: 0x00291488 File Offset: 0x0028F688
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

		// Token: 0x06006AFC RID: 27388 RVA: 0x002914C4 File Offset: 0x0028F6C4
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

		// Token: 0x06006AFD RID: 27389 RVA: 0x00048F2C File Offset: 0x0004712C
		protected static bool isalpha(char c)
		{
			return char.IsLetter(c);
		}

		// Token: 0x06006AFE RID: 27390 RVA: 0x00048F34 File Offset: 0x00047134
		protected static bool iscntrl(char c)
		{
			return char.IsControl(c);
		}

		// Token: 0x06006AFF RID: 27391 RVA: 0x00048F3C File Offset: 0x0004713C
		protected static bool isdigit(char c)
		{
			return char.IsDigit(c);
		}

		// Token: 0x06006B00 RID: 27392 RVA: 0x00048F44 File Offset: 0x00047144
		protected static bool islower(char c)
		{
			return char.IsLower(c);
		}

		// Token: 0x06006B01 RID: 27393 RVA: 0x00048F4C File Offset: 0x0004714C
		protected static bool ispunct(char c)
		{
			return char.IsPunctuation(c);
		}

		// Token: 0x06006B02 RID: 27394 RVA: 0x00048F54 File Offset: 0x00047154
		protected static bool isspace(char c)
		{
			return c == ' ' || (c >= '\t' && c <= '\r');
		}

		// Token: 0x06006B03 RID: 27395 RVA: 0x00048F6C File Offset: 0x0004716C
		protected static bool isupper(char c)
		{
			return char.IsUpper(c);
		}

		// Token: 0x06006B04 RID: 27396 RVA: 0x00048F74 File Offset: 0x00047174
		protected static bool isalnum(char c)
		{
			return char.IsLetterOrDigit(c);
		}

		// Token: 0x06006B05 RID: 27397 RVA: 0x00048F7C File Offset: 0x0004717C
		protected static bool isxdigit(char c)
		{
			return "0123456789ABCDEFabcdef".IndexOf(c) >= 0;
		}

		// Token: 0x06006B06 RID: 27398 RVA: 0x00048F8F File Offset: 0x0004718F
		protected static bool isgraph(char c)
		{
			return !char.IsControl(c) && !char.IsWhiteSpace(c);
		}

		// Token: 0x06006B07 RID: 27399 RVA: 0x00048FA4 File Offset: 0x000471A4
		protected static bool isalpha(int c)
		{
			return char.IsLetter((char)c);
		}

		// Token: 0x06006B08 RID: 27400 RVA: 0x00048FAD File Offset: 0x000471AD
		protected static bool iscntrl(int c)
		{
			return char.IsControl((char)c);
		}

		// Token: 0x06006B09 RID: 27401 RVA: 0x00048FB6 File Offset: 0x000471B6
		protected static bool isdigit(int c)
		{
			return char.IsDigit((char)c);
		}

		// Token: 0x06006B0A RID: 27402 RVA: 0x00048FBF File Offset: 0x000471BF
		protected static bool islower(int c)
		{
			return char.IsLower((char)c);
		}

		// Token: 0x06006B0B RID: 27403 RVA: 0x00048FC8 File Offset: 0x000471C8
		protected static bool ispunct(int c)
		{
			return (ushort)c != 32 && !LuaBase.isalnum((char)c);
		}

		// Token: 0x06006B0C RID: 27404 RVA: 0x00048FDC File Offset: 0x000471DC
		protected static bool isspace(int c)
		{
			return (ushort)c == 32 || ((ushort)c >= 9 && (ushort)c <= 13);
		}

		// Token: 0x06006B0D RID: 27405 RVA: 0x00048FF7 File Offset: 0x000471F7
		protected static bool isupper(int c)
		{
			return char.IsUpper((char)c);
		}

		// Token: 0x06006B0E RID: 27406 RVA: 0x00049000 File Offset: 0x00047200
		protected static bool isalnum(int c)
		{
			return char.IsLetterOrDigit((char)c);
		}

		// Token: 0x06006B0F RID: 27407 RVA: 0x00049009 File Offset: 0x00047209
		protected static bool isgraph(int c)
		{
			return !char.IsControl((char)c) && !char.IsWhiteSpace((char)c);
		}

		// Token: 0x06006B10 RID: 27408 RVA: 0x00049020 File Offset: 0x00047220
		protected static char tolower(char c)
		{
			return char.ToLower(c);
		}

		// Token: 0x06006B11 RID: 27409 RVA: 0x00049028 File Offset: 0x00047228
		protected static char toupper(char c)
		{
			return char.ToUpper(c);
		}

		// Token: 0x06006B12 RID: 27410 RVA: 0x00049030 File Offset: 0x00047230
		protected static char tolower(int c)
		{
			return char.ToLower((char)c);
		}

		// Token: 0x06006B13 RID: 27411 RVA: 0x00049039 File Offset: 0x00047239
		protected static char toupper(int c)
		{
			return char.ToUpper((char)c);
		}

		// Token: 0x06006B14 RID: 27412 RVA: 0x00291518 File Offset: 0x0028F718
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

		// Token: 0x06006B15 RID: 27413 RVA: 0x00291558 File Offset: 0x0028F758
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

		// Token: 0x06006B16 RID: 27414 RVA: 0x00291590 File Offset: 0x0028F790
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

		// Token: 0x06006B17 RID: 27415 RVA: 0x002915D4 File Offset: 0x0028F7D4
		protected static int strlen(CharPtr str)
		{
			int num = 0;
			while (str[num] != '\0')
			{
				num++;
			}
			return num;
		}

		// Token: 0x06006B18 RID: 27416 RVA: 0x002915F4 File Offset: 0x0028F7F4
		public static void sprintf(CharPtr buffer, CharPtr str, params object[] argv)
		{
			string str2 = Tools.sprintf(str.ToString(), argv);
			LuaBase.strcpy(buffer, str2);
		}

		// Token: 0x040060CB RID: 24779
		protected const int LUA_TNONE = -1;

		// Token: 0x040060CC RID: 24780
		protected const int LUA_TNIL = 0;

		// Token: 0x040060CD RID: 24781
		protected const int LUA_TBOOLEAN = 1;

		// Token: 0x040060CE RID: 24782
		protected const int LUA_TLIGHTUSERDATA = 2;

		// Token: 0x040060CF RID: 24783
		protected const int LUA_TNUMBER = 3;

		// Token: 0x040060D0 RID: 24784
		protected const int LUA_TSTRING = 4;

		// Token: 0x040060D1 RID: 24785
		protected const int LUA_TTABLE = 5;

		// Token: 0x040060D2 RID: 24786
		protected const int LUA_TFUNCTION = 6;

		// Token: 0x040060D3 RID: 24787
		protected const int LUA_TUSERDATA = 7;

		// Token: 0x040060D4 RID: 24788
		protected const int LUA_TTHREAD = 8;

		// Token: 0x040060D5 RID: 24789
		protected const int LUA_MULTRET = -1;

		// Token: 0x040060D6 RID: 24790
		protected const string LUA_INTFRMLEN = "l";
	}
}
