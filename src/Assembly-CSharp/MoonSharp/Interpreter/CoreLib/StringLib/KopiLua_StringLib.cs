using System;
using MoonSharp.Interpreter.Interop.LuaStateInterop;

namespace MoonSharp.Interpreter.CoreLib.StringLib
{
	// Token: 0x020011A3 RID: 4515
	internal class KopiLua_StringLib : LuaBase
	{
		// Token: 0x06006E75 RID: 28277 RVA: 0x0004B411 File Offset: 0x00049611
		private static int posrelat(int pos, uint len)
		{
			if (pos < 0)
			{
				pos += (int)(len + 1U);
			}
			if (pos < 0)
			{
				return 0;
			}
			return pos;
		}

		// Token: 0x06006E76 RID: 28278 RVA: 0x0029DC58 File Offset: 0x0029BE58
		private static int check_capture(KopiLua_StringLib.MatchState ms, int l)
		{
			l -= 49;
			if (l < 0 || l >= ms.level || ms.capture[l].len == -1)
			{
				return LuaBase.LuaLError(ms.L, "invalid capture index {0}", new object[]
				{
					l + 1
				});
			}
			return l;
		}

		// Token: 0x06006E77 RID: 28279 RVA: 0x0029DCAC File Offset: 0x0029BEAC
		private static int capture_to_close(KopiLua_StringLib.MatchState ms)
		{
			int i = ms.level;
			for (i--; i >= 0; i--)
			{
				if (ms.capture[i].len == -1)
				{
					return i;
				}
			}
			return LuaBase.LuaLError(ms.L, "invalid pattern capture", Array.Empty<object>());
		}

		// Token: 0x06006E78 RID: 28280 RVA: 0x0029DCF8 File Offset: 0x0029BEF8
		private static CharPtr classend(KopiLua_StringLib.MatchState ms, CharPtr p)
		{
			p = new CharPtr(p);
			char c = p[0];
			p = p.next();
			if (c == '%')
			{
				if (p[0] == '\0')
				{
					LuaBase.LuaLError(ms.L, "malformed pattern (ends with " + LuaBase.LUA_QL("%") + ")", Array.Empty<object>());
				}
				return p + 1;
			}
			if (c != '[')
			{
				return p;
			}
			if (p[0] == '^')
			{
				p = p.next();
			}
			do
			{
				if (p[0] == '\0')
				{
					LuaBase.LuaLError(ms.L, "malformed pattern (missing " + LuaBase.LUA_QL("]") + ")", Array.Empty<object>());
				}
				c = p[0];
				p = p.next();
				if (c == '%' && p[0] != '\0')
				{
					p = p.next();
				}
			}
			while (p[0] != ']');
			return p + 1;
		}

		// Token: 0x06006E79 RID: 28281 RVA: 0x0029DDE8 File Offset: 0x0029BFE8
		private static int match_class(char c, char cl)
		{
			char c2 = LuaBase.tolower(cl);
			switch (c2)
			{
			case 'a':
			{
				bool flag = LuaBase.isalpha(c);
				goto IL_D1;
			}
			case 'b':
			case 'e':
			case 'f':
				break;
			case 'c':
			{
				bool flag = LuaBase.iscntrl(c);
				goto IL_D1;
			}
			case 'd':
			{
				bool flag = LuaBase.isdigit(c);
				goto IL_D1;
			}
			case 'g':
			{
				bool flag = LuaBase.isgraph(c);
				goto IL_D1;
			}
			default:
				if (c2 == 'l')
				{
					bool flag = LuaBase.islower(c);
					goto IL_D1;
				}
				switch (c2)
				{
				case 'p':
				{
					bool flag = LuaBase.ispunct(c);
					goto IL_D1;
				}
				case 's':
				{
					bool flag = LuaBase.isspace(c);
					goto IL_D1;
				}
				case 'u':
				{
					bool flag = LuaBase.isupper(c);
					goto IL_D1;
				}
				case 'w':
				{
					bool flag = LuaBase.isalnum(c);
					goto IL_D1;
				}
				case 'x':
				{
					bool flag = LuaBase.isxdigit(c);
					goto IL_D1;
				}
				case 'z':
				{
					bool flag = c == '\0';
					goto IL_D1;
				}
				}
				break;
			}
			if (cl != c)
			{
				return 0;
			}
			return 1;
			IL_D1:
			if (!LuaBase.islower(cl))
			{
				bool flag;
				if (flag)
				{
					return 0;
				}
				return 1;
			}
			else
			{
				bool flag;
				if (!flag)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x06006E7A RID: 28282 RVA: 0x0029DEDC File Offset: 0x0029C0DC
		private static int matchbracketclass(int c, CharPtr p, CharPtr ec)
		{
			int num = 1;
			if (p[1] == '^')
			{
				num = 0;
				p = p.next();
			}
			while ((p = p.next()) < ec)
			{
				if (p == '%')
				{
					p = p.next();
					if (KopiLua_StringLib.match_class((char)c, p[0]) != 0)
					{
						return num;
					}
				}
				else if (p[1] == '-' && p + 2 < ec)
				{
					p += 2;
					if ((int)((byte)p[-2]) <= c && c <= (int)((byte)p[0]))
					{
						return num;
					}
				}
				else if ((int)((byte)p[0]) == c)
				{
					return num;
				}
			}
			if (num != 0)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06006E7B RID: 28283 RVA: 0x0029DF88 File Offset: 0x0029C188
		private static int singlematch(int c, CharPtr p, CharPtr ep)
		{
			char c2 = p[0];
			if (c2 == '%')
			{
				return KopiLua_StringLib.match_class((char)c, p[1]);
			}
			if (c2 == '.')
			{
				return 1;
			}
			if (c2 == '[')
			{
				return KopiLua_StringLib.matchbracketclass(c, p, ep - 1);
			}
			if ((int)((byte)p[0]) != c)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06006E7C RID: 28284 RVA: 0x0029DFDC File Offset: 0x0029C1DC
		private static CharPtr matchbalance(KopiLua_StringLib.MatchState ms, CharPtr s, CharPtr p)
		{
			if (p[0] == '\0' || p[1] == '\0')
			{
				LuaBase.LuaLError(ms.L, "unbalanced pattern", Array.Empty<object>());
			}
			if (s[0] != p[0])
			{
				return null;
			}
			int num = (int)p[0];
			int num2 = (int)p[1];
			int num3 = 1;
			while ((s = s.next()) < ms.src_end)
			{
				if ((int)s[0] == num2)
				{
					if (--num3 == 0)
					{
						return s + 1;
					}
				}
				else if ((int)s[0] == num)
				{
					num3++;
				}
			}
			return null;
		}

		// Token: 0x06006E7D RID: 28285 RVA: 0x0029E078 File Offset: 0x0029C278
		private static CharPtr max_expand(KopiLua_StringLib.MatchState ms, CharPtr s, CharPtr p, CharPtr ep)
		{
			int i = 0;
			while (s + i < ms.src_end)
			{
				if (KopiLua_StringLib.singlematch((int)((byte)s[i]), p, ep) == 0)
				{
					break;
				}
				i++;
			}
			while (i >= 0)
			{
				CharPtr charPtr = KopiLua_StringLib.match(ms, s + i, ep + 1);
				if (charPtr != null)
				{
					return charPtr;
				}
				i--;
			}
			return null;
		}

		// Token: 0x06006E7E RID: 28286 RVA: 0x0029E0E0 File Offset: 0x0029C2E0
		private static CharPtr min_expand(KopiLua_StringLib.MatchState ms, CharPtr s, CharPtr p, CharPtr ep)
		{
			CharPtr charPtr;
			for (;;)
			{
				charPtr = KopiLua_StringLib.match(ms, s, ep + 1);
				if (charPtr != null)
				{
					break;
				}
				if (!(s < ms.src_end) || KopiLua_StringLib.singlematch((int)((byte)s[0]), p, ep) == 0)
				{
					goto IL_43;
				}
				s = s.next();
			}
			return charPtr;
			IL_43:
			return null;
		}

		// Token: 0x06006E7F RID: 28287 RVA: 0x0029E134 File Offset: 0x0029C334
		private static CharPtr start_capture(KopiLua_StringLib.MatchState ms, CharPtr s, CharPtr p, int what)
		{
			int level = ms.level;
			if (level >= 32)
			{
				LuaBase.LuaLError(ms.L, "too many captures", Array.Empty<object>());
			}
			ms.capture[level].init = s;
			ms.capture[level].len = what;
			ms.level = level + 1;
			CharPtr charPtr = KopiLua_StringLib.match(ms, s, p);
			if (charPtr == null)
			{
				ms.level--;
			}
			return charPtr;
		}

		// Token: 0x06006E80 RID: 28288 RVA: 0x0029E1A8 File Offset: 0x0029C3A8
		private static CharPtr end_capture(KopiLua_StringLib.MatchState ms, CharPtr s, CharPtr p)
		{
			int num = KopiLua_StringLib.capture_to_close(ms);
			ms.capture[num].len = s - ms.capture[num].init;
			CharPtr charPtr = KopiLua_StringLib.match(ms, s, p);
			if (charPtr == null)
			{
				ms.capture[num].len = -1;
			}
			return charPtr;
		}

		// Token: 0x06006E81 RID: 28289 RVA: 0x0029E1FC File Offset: 0x0029C3FC
		private static CharPtr match_capture(KopiLua_StringLib.MatchState ms, CharPtr s, int l)
		{
			l = KopiLua_StringLib.check_capture(ms, l);
			uint len = (uint)ms.capture[l].len;
			if (ms.src_end - s >= (int)len && LuaBase.memcmp(ms.capture[l].init, s, len) == 0)
			{
				return s + len;
			}
			return null;
		}

		// Token: 0x06006E82 RID: 28290 RVA: 0x0029E250 File Offset: 0x0029C450
		private static CharPtr match(KopiLua_StringLib.MatchState ms, CharPtr s, CharPtr p)
		{
			s = new CharPtr(s);
			p = new CharPtr(p);
			int matchdepth = ms.matchdepth;
			ms.matchdepth = matchdepth - 1;
			if (matchdepth == 0)
			{
				LuaBase.LuaLError(ms.L, "pattern too complex", Array.Empty<object>());
			}
			CharPtr charPtr;
			int num;
			CharPtr charPtr3;
			int num2;
			for (;;)
			{
				char c = p[0];
				if (c == '\0')
				{
					return s;
				}
				switch (c)
				{
				case '$':
					if (p[1] == '\0')
					{
						goto Block_20;
					}
					break;
				case '%':
					c = p[1];
					if (c != 'b')
					{
						if (c != 'f')
						{
							if (LuaBase.isdigit(p[1]))
							{
								s = KopiLua_StringLib.match_capture(ms, s, (int)((byte)p[1]));
								if (s == null)
								{
									goto Block_12;
								}
								p += 2;
								continue;
							}
							else
							{
								charPtr = KopiLua_StringLib.classend(ms, p);
								num = ((s < ms.src_end && KopiLua_StringLib.singlematch((int)((byte)s[0]), p, charPtr) != 0) ? 1 : 0);
								c = charPtr[0];
								switch (c)
								{
								case '*':
									goto IL_25A;
								case '+':
									goto IL_264;
								case ',':
									break;
								case '-':
									goto IL_27A;
								default:
									if (c == '?')
									{
										CharPtr result;
										if (num != 0 && (result = KopiLua_StringLib.match(ms, s + 1, charPtr + 1)) != null)
										{
											return result;
										}
										p = charPtr + 1;
										continue;
									}
									break;
								}
								if (num == 0)
								{
									goto Block_19;
								}
								s = s.next();
								p = charPtr;
								continue;
							}
						}
						else
						{
							p += 2;
							if (p[0] != '[')
							{
								LuaBase.LuaLError(ms.L, string.Concat(new string[]
								{
									"missing ",
									LuaBase.LUA_QL("["),
									" after ",
									LuaBase.LUA_QL("%f"),
									" in pattern"
								}), Array.Empty<object>());
							}
							CharPtr charPtr2 = KopiLua_StringLib.classend(ms, p);
							if (KopiLua_StringLib.matchbracketclass((int)((byte)((s == ms.src_init) ? '\0' : s[-1])), p, charPtr2 - 1) != 0 || KopiLua_StringLib.matchbracketclass((int)((byte)s[0]), p, charPtr2 - 1) == 0)
							{
								goto IL_191;
							}
							p = charPtr2;
							continue;
						}
					}
					else
					{
						s = KopiLua_StringLib.matchbalance(ms, s, p + 2);
						if (s == null)
						{
							goto Block_7;
						}
						p += 4;
						continue;
					}
					break;
				case '(':
					goto IL_6D;
				case ')':
					goto IL_99;
				}
				charPtr3 = KopiLua_StringLib.classend(ms, p);
				num2 = ((s < ms.src_end && KopiLua_StringLib.singlematch((int)((byte)s[0]), p, charPtr3) != 0) ? 1 : 0);
				c = charPtr3[0];
				switch (c)
				{
				case '*':
					goto IL_343;
				case '+':
					goto IL_34E;
				case ',':
					break;
				case '-':
					goto IL_365;
				default:
					if (c == '?')
					{
						CharPtr result2;
						if (num2 != 0 && (result2 = KopiLua_StringLib.match(ms, s + 1, charPtr3 + 1)) != null)
						{
							return result2;
						}
						p = charPtr3 + 1;
						continue;
					}
					break;
				}
				if (num2 == 0)
				{
					goto Block_28;
				}
				s = s.next();
				p = charPtr3;
			}
			IL_6D:
			if (p[1] == ')')
			{
				return KopiLua_StringLib.start_capture(ms, s, p + 2, -2);
			}
			return KopiLua_StringLib.start_capture(ms, s, p + 1, -1);
			IL_99:
			return KopiLua_StringLib.end_capture(ms, s, p + 1);
			Block_7:
			return null;
			IL_191:
			return null;
			Block_12:
			return null;
			IL_25A:
			return KopiLua_StringLib.max_expand(ms, s, p, charPtr);
			IL_264:
			if (num == 0)
			{
				return null;
			}
			return KopiLua_StringLib.max_expand(ms, s + 1, p, charPtr);
			IL_27A:
			return KopiLua_StringLib.min_expand(ms, s, p, charPtr);
			Block_19:
			return null;
			Block_20:
			if (!(s == ms.src_end))
			{
				return null;
			}
			return s;
			IL_343:
			return KopiLua_StringLib.max_expand(ms, s, p, charPtr3);
			IL_34E:
			if (num2 == 0)
			{
				return null;
			}
			return KopiLua_StringLib.max_expand(ms, s + 1, p, charPtr3);
			IL_365:
			return KopiLua_StringLib.min_expand(ms, s, p, charPtr3);
			Block_28:
			return null;
		}

		// Token: 0x06006E83 RID: 28291 RVA: 0x0029E5E4 File Offset: 0x0029C7E4
		private static CharPtr lmemfind(CharPtr s1, uint l1, CharPtr s2, uint l2)
		{
			if (l2 == 0U)
			{
				return s1;
			}
			if (l2 > l1)
			{
				return null;
			}
			l2 -= 1U;
			l1 -= l2;
			CharPtr charPtr;
			while (l1 > 0U && (charPtr = LuaBase.memchr(s1, s2[0], l1)) != null)
			{
				charPtr = charPtr.next();
				if (LuaBase.memcmp(charPtr, s2 + 1, l2) == 0)
				{
					return charPtr - 1;
				}
				l1 -= (uint)(charPtr - s1);
				s1 = charPtr;
			}
			return null;
		}

		// Token: 0x06006E84 RID: 28292 RVA: 0x0029E654 File Offset: 0x0029C854
		private static void push_onecapture(KopiLua_StringLib.MatchState ms, int i, CharPtr s, CharPtr e)
		{
			if (i >= ms.level)
			{
				if (i == 0)
				{
					LuaBase.LuaPushLString(ms.L, s, (uint)(e - s));
					return;
				}
				LuaBase.LuaLError(ms.L, "invalid capture index", Array.Empty<object>());
				return;
			}
			else
			{
				int len = ms.capture[i].len;
				if (len == -1)
				{
					LuaBase.LuaLError(ms.L, "unfinished capture", Array.Empty<object>());
				}
				if (len == -2)
				{
					LuaBase.LuaPushInteger(ms.L, ms.capture[i].init - ms.src_init + 1);
					return;
				}
				LuaBase.LuaPushLString(ms.L, ms.capture[i].init, (uint)len);
				return;
			}
		}

		// Token: 0x06006E85 RID: 28293 RVA: 0x0029E704 File Offset: 0x0029C904
		private static int push_captures(KopiLua_StringLib.MatchState ms, CharPtr s, CharPtr e)
		{
			int num = (ms.level == 0 && s != null) ? 1 : ms.level;
			LuaBase.LuaLCheckStack(ms.L, num, "too many captures");
			for (int i = 0; i < num; i++)
			{
				KopiLua_StringLib.push_onecapture(ms, i, s, e);
			}
			return num;
		}

		// Token: 0x06006E86 RID: 28294 RVA: 0x0029E754 File Offset: 0x0029C954
		private static int str_find_aux(LuaState L, int find)
		{
			uint num;
			CharPtr charPtr = LuaBase.LuaLCheckLString(L, 1, out num);
			uint num2;
			CharPtr charPtr2 = KopiLua_StringLib.PatchPattern(LuaBase.LuaLCheckLString(L, 2, out num2));
			int num3 = KopiLua_StringLib.posrelat(LuaBase.LuaLOptInteger(L, 3, 1), num) - 1;
			if (num3 < 0)
			{
				num3 = 0;
			}
			else if (num3 > (int)num)
			{
				num3 = (int)num;
			}
			if (find != 0 && (LuaBase.LuaToBoolean(L, 4) != 0 || LuaBase.strpbrk(charPtr2, "^$*+?.([%-") == null))
			{
				CharPtr ptr = KopiLua_StringLib.lmemfind(charPtr + num3, (uint)((ulong)num - (ulong)((long)num3)), charPtr2, num2);
				if (ptr != null)
				{
					LuaBase.LuaPushInteger(L, ptr - charPtr + 1);
					LuaBase.LuaPushInteger(L, (int)((long)(ptr - charPtr) + (long)((ulong)num2)));
					return 2;
				}
			}
			else
			{
				KopiLua_StringLib.MatchState matchState = new KopiLua_StringLib.MatchState();
				int num4 = 0;
				if (charPtr2[0] == '^')
				{
					charPtr2 = charPtr2.next();
					num4 = 1;
				}
				CharPtr charPtr3 = charPtr + num3;
				matchState.L = L;
				matchState.matchdepth = 200;
				matchState.src_init = charPtr;
				matchState.src_end = charPtr + num;
				CharPtr charPtr4;
				for (;;)
				{
					matchState.level = 0;
					matchState.matchdepth = 200;
					if ((charPtr4 = KopiLua_StringLib.match(matchState, charPtr3, charPtr2)) != null)
					{
						break;
					}
					if (!((charPtr3 = charPtr3.next()) <= matchState.src_end) || num4 != 0)
					{
						goto IL_186;
					}
				}
				if (find != 0)
				{
					LuaBase.LuaPushInteger(L, charPtr3 - charPtr + 1);
					LuaBase.LuaPushInteger(L, charPtr4 - charPtr);
					return KopiLua_StringLib.push_captures(matchState, null, null) + 2;
				}
				return KopiLua_StringLib.push_captures(matchState, charPtr3, charPtr4);
			}
			IL_186:
			LuaBase.LuaPushNil(L);
			return 1;
		}

		// Token: 0x06006E87 RID: 28295 RVA: 0x0004B425 File Offset: 0x00049625
		public static int str_find(LuaState L)
		{
			return KopiLua_StringLib.str_find_aux(L, 1);
		}

		// Token: 0x06006E88 RID: 28296 RVA: 0x0004B42E File Offset: 0x0004962E
		public static int str_match(LuaState L)
		{
			return KopiLua_StringLib.str_find_aux(L, 0);
		}

		// Token: 0x06006E89 RID: 28297 RVA: 0x0029E8F0 File Offset: 0x0029CAF0
		private static int gmatch_aux(LuaState L, KopiLua_StringLib.GMatchAuxData auxdata)
		{
			KopiLua_StringLib.MatchState matchState = new KopiLua_StringLib.MatchState();
			uint ls = auxdata.LS;
			CharPtr s = auxdata.S;
			CharPtr p = auxdata.P;
			matchState.L = L;
			matchState.matchdepth = 200;
			matchState.src_init = s;
			matchState.src_end = s + ls;
			CharPtr charPtr = s + auxdata.POS;
			while (charPtr <= matchState.src_end)
			{
				matchState.level = 0;
				matchState.matchdepth = 200;
				CharPtr charPtr2;
				if ((charPtr2 = KopiLua_StringLib.match(matchState, charPtr, p)) != null)
				{
					int num = charPtr2 - s;
					if (charPtr2 == charPtr)
					{
						num++;
					}
					auxdata.POS = (uint)num;
					return KopiLua_StringLib.push_captures(matchState, charPtr, charPtr2);
				}
				charPtr = charPtr.next();
			}
			return 0;
		}

		// Token: 0x06006E8A RID: 28298 RVA: 0x0029E9BC File Offset: 0x0029CBBC
		private static DynValue gmatch_aux_2(ScriptExecutionContext executionContext, CallbackArguments args)
		{
			return executionContext.EmulateClassicCall(args, "gmatch", (LuaState L) => KopiLua_StringLib.gmatch_aux(L, (KopiLua_StringLib.GMatchAuxData)executionContext.AdditionalData));
		}

		// Token: 0x06006E8B RID: 28299 RVA: 0x0029E9F4 File Offset: 0x0029CBF4
		public static int str_gmatch(LuaState L)
		{
			CallbackFunction callbackFunction = new CallbackFunction(new Func<ScriptExecutionContext, CallbackArguments, DynValue>(KopiLua_StringLib.gmatch_aux_2), "gmatch");
			string @string = LuaBase.ArgAsType(L, 1, DataType.String, false).String;
			string str = KopiLua_StringLib.PatchPattern(LuaBase.ArgAsType(L, 2, DataType.String, false).String);
			callbackFunction.AdditionalData = new KopiLua_StringLib.GMatchAuxData
			{
				S = new CharPtr(@string),
				P = new CharPtr(str),
				LS = (uint)@string.Length,
				POS = 0U
			};
			L.Push(DynValue.NewCallback(callbackFunction));
			return 1;
		}

		// Token: 0x06006E8C RID: 28300 RVA: 0x0004B437 File Offset: 0x00049637
		private static int gfind_nodef(LuaState L)
		{
			return LuaBase.LuaLError(L, LuaBase.LUA_QL("string.gfind") + " was renamed to " + LuaBase.LUA_QL("string.gmatch"), Array.Empty<object>());
		}

		// Token: 0x06006E8D RID: 28301 RVA: 0x0029EA80 File Offset: 0x0029CC80
		private static void add_s(KopiLua_StringLib.MatchState ms, LuaLBuffer b, CharPtr s, CharPtr e)
		{
			uint num;
			CharPtr charPtr = LuaBase.LuaToLString(ms.L, 3, out num);
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				if (charPtr[num2] != '%')
				{
					LuaBase.LuaLAddChar(b, charPtr[num2]);
				}
				else
				{
					num2 += 1U;
					if (!LuaBase.isdigit(charPtr[num2]))
					{
						if (charPtr[num2] != '%')
						{
							LuaBase.LuaLError(ms.L, "invalid use of '%' in replacement string", Array.Empty<object>());
						}
						LuaBase.LuaLAddChar(b, charPtr[num2]);
					}
					else if (charPtr[num2] == '0')
					{
						LuaBase.LuaLAddLString(b, s, (uint)(e - s));
					}
					else
					{
						KopiLua_StringLib.push_onecapture(ms, (int)(charPtr[num2] - '1'), s, e);
						LuaBase.LuaLAddValue(b);
					}
				}
			}
		}

		// Token: 0x06006E8E RID: 28302 RVA: 0x0029EB44 File Offset: 0x0029CD44
		private static void add_value(KopiLua_StringLib.MatchState ms, LuaLBuffer b, CharPtr s, CharPtr e)
		{
			LuaState l = ms.L;
			switch (LuaBase.LuaType(l, 3))
			{
			case 3:
			case 4:
				KopiLua_StringLib.add_s(ms, b, s, e);
				return;
			case 5:
				KopiLua_StringLib.push_onecapture(ms, 0, s, e);
				LuaBase.LuaGetTable(l, 3);
				break;
			case 6:
			{
				LuaBase.LuaPushValue(l, 3);
				int nargs = KopiLua_StringLib.push_captures(ms, s, e);
				LuaBase.LuaCall(l, nargs, 1);
				break;
			}
			}
			if (LuaBase.LuaToBoolean(l, -1) == 0)
			{
				LuaBase.LuaPop(l, 1);
				LuaBase.LuaPushLString(l, s, (uint)(e - s));
			}
			else if (LuaBase.LuaIsString(l, -1) == 0)
			{
				LuaBase.LuaLError(l, "invalid replacement value (a {0})", new object[]
				{
					LuaBase.LuaLTypeName(l, -1)
				});
			}
			LuaBase.LuaLAddValue(b);
		}

		// Token: 0x06006E8F RID: 28303 RVA: 0x0029EBFC File Offset: 0x0029CDFC
		public static int str_gsub(LuaState L)
		{
			uint num;
			CharPtr charPtr = LuaBase.LuaLCheckLString(L, 1, out num);
			CharPtr charPtr2 = KopiLua_StringLib.PatchPattern(LuaBase.LuaLCheckStringStr(L, 2));
			int num2 = LuaBase.LuaType(L, 3);
			int num3 = LuaBase.LuaLOptInt(L, 4, (int)(num + 1U));
			int num4 = 0;
			if (charPtr2[0] == '^')
			{
				charPtr2 = charPtr2.next();
				num4 = 1;
			}
			int i = 0;
			KopiLua_StringLib.MatchState matchState = new KopiLua_StringLib.MatchState();
			LuaLBuffer b = new LuaLBuffer(L);
			LuaBase.LuaLArgCheck(L, num2 == 3 || num2 == 4 || num2 == 6 || num2 == 5 || num2 == 7, 3, "string/function/table expected");
			LuaBase.LuaLBuffInit(L, b);
			matchState.L = L;
			matchState.matchdepth = 200;
			matchState.src_init = charPtr;
			matchState.src_end = charPtr + num;
			while (i < num3)
			{
				matchState.level = 0;
				matchState.matchdepth = 200;
				CharPtr charPtr3 = KopiLua_StringLib.match(matchState, charPtr, charPtr2);
				if (charPtr3 != null)
				{
					i++;
					KopiLua_StringLib.add_value(matchState, b, charPtr, charPtr3);
				}
				if (charPtr3 != null && charPtr3 > charPtr)
				{
					charPtr = charPtr3;
				}
				else
				{
					if (!(charPtr < matchState.src_end))
					{
						break;
					}
					char p = charPtr[0];
					charPtr = charPtr.next();
					LuaBase.LuaLAddChar(b, p);
				}
				if (num4 != 0)
				{
					break;
				}
			}
			LuaBase.LuaLAddLString(b, charPtr, (uint)(matchState.src_end - charPtr));
			LuaBase.LuaLPushResult(b);
			LuaBase.LuaPushInteger(L, i);
			return 2;
		}

		// Token: 0x06006E90 RID: 28304 RVA: 0x0029ED70 File Offset: 0x0029CF70
		private static void addquoted(LuaState L, LuaLBuffer b, int arg)
		{
			uint num;
			CharPtr charPtr = LuaBase.LuaLCheckLString(L, arg, out num);
			LuaBase.LuaLAddChar(b, '"');
			while (num-- != 0U)
			{
				char c = charPtr[0];
				if (c <= '\r')
				{
					if (c == '\n')
					{
						goto IL_3F;
					}
					if (c != '\r')
					{
						goto IL_6C;
					}
					LuaBase.LuaLAddLString(b, "\\r", 2U);
				}
				else
				{
					if (c == '"' || c == '\\')
					{
						goto IL_3F;
					}
					goto IL_6C;
				}
				IL_D9:
				charPtr = charPtr.next();
				continue;
				IL_3F:
				LuaBase.LuaLAddChar(b, '\\');
				LuaBase.LuaLAddChar(b, charPtr[0]);
				goto IL_D9;
				IL_6C:
				if (charPtr[0] >= '\u0010')
				{
					LuaBase.LuaLAddChar(b, charPtr[0]);
					goto IL_D9;
				}
				bool flag = false;
				if (num >= 1U && char.IsNumber(charPtr[1]))
				{
					flag = true;
				}
				if (flag)
				{
					LuaBase.LuaLAddString(b, string.Format("\\{0:000}", (int)charPtr[0]));
					goto IL_D9;
				}
				LuaBase.LuaLAddString(b, string.Format("\\{0}", (int)charPtr[0]));
				goto IL_D9;
			}
			LuaBase.LuaLAddChar(b, '"');
		}

		// Token: 0x06006E91 RID: 28305 RVA: 0x0029EE70 File Offset: 0x0029D070
		private static CharPtr scanformat(LuaState L, CharPtr strfrmt, CharPtr form)
		{
			CharPtr charPtr = strfrmt;
			while (charPtr[0] != '\0' && LuaBase.strchr("-+ #0", charPtr[0]) != null)
			{
				charPtr = charPtr.next();
			}
			if ((ulong)(charPtr - strfrmt) >= (ulong)((long)("-+ #0".Length + 1)))
			{
				LuaBase.LuaLError(L, "invalid format (repeated flags)", Array.Empty<object>());
			}
			if (LuaBase.isdigit((int)((byte)charPtr[0])))
			{
				charPtr = charPtr.next();
			}
			if (LuaBase.isdigit((int)((byte)charPtr[0])))
			{
				charPtr = charPtr.next();
			}
			if (charPtr[0] == '.')
			{
				charPtr = charPtr.next();
				if (LuaBase.isdigit((int)((byte)charPtr[0])))
				{
					charPtr = charPtr.next();
				}
				if (LuaBase.isdigit((int)((byte)charPtr[0])))
				{
					charPtr = charPtr.next();
				}
			}
			if (LuaBase.isdigit((int)((byte)charPtr[0])))
			{
				LuaBase.LuaLError(L, "invalid format (width or precision too long)", Array.Empty<object>());
			}
			form[0] = '%';
			form = form.next();
			LuaBase.strncpy(form, strfrmt, charPtr - strfrmt + 1);
			form += charPtr - strfrmt + 1;
			form[0] = '\0';
			return charPtr;
		}

		// Token: 0x06006E92 RID: 28306 RVA: 0x0029EFA0 File Offset: 0x0029D1A0
		private static void addintlen(CharPtr form)
		{
			uint num = (uint)LuaBase.strlen(form);
			char value = form[num - 1U];
			LuaBase.strcpy(form + num - 1, "l");
			form[(long)((ulong)num + (ulong)((long)("l".Length + 1)) - 2UL)] = value;
			form[(long)((ulong)num + (ulong)((long)("l".Length + 1)) - 1UL)] = '\0';
		}

		// Token: 0x06006E93 RID: 28307 RVA: 0x0029F010 File Offset: 0x0029D210
		public static int str_format(LuaState L)
		{
			int num = LuaBase.LuaGetTop(L);
			int num2 = 1;
			uint offset;
			CharPtr charPtr = LuaBase.LuaLCheckLString(L, num2, out offset);
			CharPtr ptr = charPtr + offset;
			LuaLBuffer b = new LuaLBuffer(L);
			LuaBase.LuaLBuffInit(L, b);
			while (charPtr < ptr)
			{
				if (charPtr[0] != '%')
				{
					LuaBase.LuaLAddChar(b, charPtr[0]);
					charPtr = charPtr.next();
				}
				else
				{
					if (charPtr[1] != '%')
					{
						charPtr = charPtr.next();
						CharPtr charPtr2 = new char[KopiLua_StringLib.MAX_FORMAT];
						CharPtr charPtr3 = new char[512];
						if (++num2 > num)
						{
							LuaBase.LuaLArgError(L, num2, "no value");
						}
						charPtr = KopiLua_StringLib.scanformat(L, charPtr, charPtr2);
						char c = charPtr[0];
						charPtr = charPtr.next();
						if (c <= 'G')
						{
							if (c != 'E' && c != 'G')
							{
								goto IL_258;
							}
							goto IL_1D9;
						}
						else
						{
							if (c != 'X')
							{
								switch (c)
								{
								case 'c':
									LuaBase.sprintf(charPtr3, charPtr2, new object[]
									{
										(int)LuaBase.LuaLCheckNumber(L, num2)
									});
									goto IL_2A3;
								case 'd':
								case 'i':
									KopiLua_StringLib.addintlen(charPtr2);
									LuaBase.sprintf(charPtr3, charPtr2, new object[]
									{
										(long)LuaBase.LuaLCheckNumber(L, num2)
									});
									goto IL_2A3;
								case 'e':
								case 'f':
								case 'g':
									goto IL_1D9;
								case 'h':
								case 'j':
								case 'k':
								case 'l':
								case 'm':
								case 'n':
								case 'p':
								case 'r':
								case 't':
									goto IL_258;
								case 'o':
								case 'u':
									break;
								case 'q':
									KopiLua_StringLib.addquoted(L, b, num2);
									continue;
								case 's':
								{
									uint num3;
									CharPtr charPtr4 = LuaBase.LuaLCheckLString(L, num2, out num3);
									if (LuaBase.strchr(charPtr2, '.') == null && num3 >= 100U)
									{
										LuaBase.LuaPushValue(L, num2);
										LuaBase.LuaLAddValue(b);
										continue;
									}
									LuaBase.sprintf(charPtr3, charPtr2, new object[]
									{
										charPtr4
									});
									goto IL_2A3;
								}
								default:
									if (c != 'x')
									{
										goto IL_258;
									}
									break;
								}
							}
							KopiLua_StringLib.addintlen(charPtr2);
							LuaBase.sprintf(charPtr3, charPtr2, new object[]
							{
								(ulong)LuaBase.LuaLCheckNumber(L, num2)
							});
						}
						IL_2A3:
						LuaBase.LuaLAddLString(b, charPtr3, (uint)LuaBase.strlen(charPtr3));
						continue;
						IL_1D9:
						LuaBase.sprintf(charPtr3, charPtr2, new object[]
						{
							LuaBase.LuaLCheckNumber(L, num2)
						});
						goto IL_2A3;
						IL_258:
						return LuaBase.LuaLError(L, "invalid option " + LuaBase.LUA_QL("%" + c.ToString()) + " to " + LuaBase.LUA_QL("format"), new object[]
						{
							charPtr[-1]
						});
					}
					LuaBase.LuaLAddChar(b, charPtr[0]);
					charPtr += 2;
				}
			}
			LuaBase.LuaLPushResult(b);
			return 1;
		}

		// Token: 0x06006E94 RID: 28308 RVA: 0x0004B462 File Offset: 0x00049662
		private static string PatchPattern(string charPtr)
		{
			return charPtr.Replace("\0", "%z");
		}

		// Token: 0x0400625C RID: 25180
		public const int LUA_MAXCAPTURES = 32;

		// Token: 0x0400625D RID: 25181
		public const int CAP_UNFINISHED = -1;

		// Token: 0x0400625E RID: 25182
		public const int CAP_POSITION = -2;

		// Token: 0x0400625F RID: 25183
		public const int MAXCCALLS = 200;

		// Token: 0x04006260 RID: 25184
		public const char L_ESC = '%';

		// Token: 0x04006261 RID: 25185
		public const string SPECIALS = "^$*+?.([%-";

		// Token: 0x04006262 RID: 25186
		public const int MAX_ITEM = 512;

		// Token: 0x04006263 RID: 25187
		public const string FLAGS = "-+ #0";

		// Token: 0x04006264 RID: 25188
		public static readonly int MAX_FORMAT = "-+ #0".Length + 1 + ("l".Length + 1) + 10;

		// Token: 0x020011A4 RID: 4516
		public class MatchState
		{
			// Token: 0x06006E97 RID: 28311 RVA: 0x0029F2E8 File Offset: 0x0029D4E8
			public MatchState()
			{
				for (int i = 0; i < 32; i++)
				{
					this.capture[i] = new KopiLua_StringLib.MatchState.capture_();
				}
			}

			// Token: 0x04006265 RID: 25189
			public int matchdepth;

			// Token: 0x04006266 RID: 25190
			public CharPtr src_init;

			// Token: 0x04006267 RID: 25191
			public CharPtr src_end;

			// Token: 0x04006268 RID: 25192
			public LuaState L;

			// Token: 0x04006269 RID: 25193
			public int level;

			// Token: 0x0400626A RID: 25194
			public KopiLua_StringLib.MatchState.capture_[] capture = new KopiLua_StringLib.MatchState.capture_[32];

			// Token: 0x020011A5 RID: 4517
			public class capture_
			{
				// Token: 0x0400626B RID: 25195
				public CharPtr init;

				// Token: 0x0400626C RID: 25196
				public int len;
			}
		}

		// Token: 0x020011A6 RID: 4518
		private class GMatchAuxData
		{
			// Token: 0x0400626D RID: 25197
			public CharPtr S;

			// Token: 0x0400626E RID: 25198
			public CharPtr P;

			// Token: 0x0400626F RID: 25199
			public uint LS;

			// Token: 0x04006270 RID: 25200
			public uint POS;
		}
	}
}
