using System;
using System.Text;

namespace MoonSharp.Interpreter.Interop.LuaStateInterop
{
	// Token: 0x02000D34 RID: 3380
	public class CharPtr
	{
		// Token: 0x1700075C RID: 1884
		public char this[int offset]
		{
			get
			{
				return this.chars[this.index + offset];
			}
			set
			{
				this.chars[this.index + offset] = value;
			}
		}

		// Token: 0x1700075D RID: 1885
		public char this[uint offset]
		{
			get
			{
				return this.chars[(int)(checked((IntPtr)(unchecked((long)this.index + (long)((ulong)offset)))))];
			}
			set
			{
				this.chars[(int)(checked((IntPtr)(unchecked((long)this.index + (long)((ulong)offset)))))] = value;
			}
		}

		// Token: 0x1700075E RID: 1886
		public char this[long offset]
		{
			get
			{
				return this.chars[this.index + (int)offset];
			}
			set
			{
				this.chars[this.index + (int)offset] = value;
			}
		}

		// Token: 0x06005EDD RID: 24285 RVA: 0x00268BB8 File Offset: 0x00266DB8
		public static implicit operator CharPtr(string str)
		{
			return new CharPtr(str);
		}

		// Token: 0x06005EDE RID: 24286 RVA: 0x00268BC0 File Offset: 0x00266DC0
		public static implicit operator CharPtr(char[] chars)
		{
			return new CharPtr(chars);
		}

		// Token: 0x06005EDF RID: 24287 RVA: 0x00268BC8 File Offset: 0x00266DC8
		public static implicit operator CharPtr(byte[] bytes)
		{
			return new CharPtr(bytes);
		}

		// Token: 0x06005EE0 RID: 24288 RVA: 0x00268BD0 File Offset: 0x00266DD0
		public CharPtr()
		{
			this.chars = null;
			this.index = 0;
		}

		// Token: 0x06005EE1 RID: 24289 RVA: 0x00268BE6 File Offset: 0x00266DE6
		public CharPtr(string str)
		{
			this.chars = (str + "\0").ToCharArray();
			this.index = 0;
		}

		// Token: 0x06005EE2 RID: 24290 RVA: 0x00268C0B File Offset: 0x00266E0B
		public CharPtr(CharPtr ptr)
		{
			this.chars = ptr.chars;
			this.index = ptr.index;
		}

		// Token: 0x06005EE3 RID: 24291 RVA: 0x00268C2B File Offset: 0x00266E2B
		public CharPtr(CharPtr ptr, int index)
		{
			this.chars = ptr.chars;
			this.index = index;
		}

		// Token: 0x06005EE4 RID: 24292 RVA: 0x00268C46 File Offset: 0x00266E46
		public CharPtr(char[] chars)
		{
			this.chars = chars;
			this.index = 0;
		}

		// Token: 0x06005EE5 RID: 24293 RVA: 0x00268C5C File Offset: 0x00266E5C
		public CharPtr(char[] chars, int index)
		{
			this.chars = chars;
			this.index = index;
		}

		// Token: 0x06005EE6 RID: 24294 RVA: 0x00268C74 File Offset: 0x00266E74
		public CharPtr(byte[] bytes)
		{
			this.chars = new char[bytes.Length];
			for (int i = 0; i < bytes.Length; i++)
			{
				this.chars[i] = (char)bytes[i];
			}
			this.index = 0;
		}

		// Token: 0x06005EE7 RID: 24295 RVA: 0x00268CB5 File Offset: 0x00266EB5
		public CharPtr(IntPtr ptr)
		{
			this.chars = new char[0];
			this.index = 0;
		}

		// Token: 0x06005EE8 RID: 24296 RVA: 0x00268CD0 File Offset: 0x00266ED0
		public static CharPtr operator +(CharPtr ptr, int offset)
		{
			return new CharPtr(ptr.chars, ptr.index + offset);
		}

		// Token: 0x06005EE9 RID: 24297 RVA: 0x00268CE5 File Offset: 0x00266EE5
		public static CharPtr operator -(CharPtr ptr, int offset)
		{
			return new CharPtr(ptr.chars, ptr.index - offset);
		}

		// Token: 0x06005EEA RID: 24298 RVA: 0x00268CD0 File Offset: 0x00266ED0
		public static CharPtr operator +(CharPtr ptr, uint offset)
		{
			return new CharPtr(ptr.chars, ptr.index + (int)offset);
		}

		// Token: 0x06005EEB RID: 24299 RVA: 0x00268CE5 File Offset: 0x00266EE5
		public static CharPtr operator -(CharPtr ptr, uint offset)
		{
			return new CharPtr(ptr.chars, ptr.index - (int)offset);
		}

		// Token: 0x06005EEC RID: 24300 RVA: 0x00268CFA File Offset: 0x00266EFA
		public void inc()
		{
			this.index++;
		}

		// Token: 0x06005EED RID: 24301 RVA: 0x00268D0A File Offset: 0x00266F0A
		public void dec()
		{
			this.index--;
		}

		// Token: 0x06005EEE RID: 24302 RVA: 0x00268D1A File Offset: 0x00266F1A
		public CharPtr next()
		{
			return new CharPtr(this.chars, this.index + 1);
		}

		// Token: 0x06005EEF RID: 24303 RVA: 0x00268D2F File Offset: 0x00266F2F
		public CharPtr prev()
		{
			return new CharPtr(this.chars, this.index - 1);
		}

		// Token: 0x06005EF0 RID: 24304 RVA: 0x00268CD0 File Offset: 0x00266ED0
		public CharPtr add(int ofs)
		{
			return new CharPtr(this.chars, this.index + ofs);
		}

		// Token: 0x06005EF1 RID: 24305 RVA: 0x00268CE5 File Offset: 0x00266EE5
		public CharPtr sub(int ofs)
		{
			return new CharPtr(this.chars, this.index - ofs);
		}

		// Token: 0x06005EF2 RID: 24306 RVA: 0x00268D44 File Offset: 0x00266F44
		public static bool operator ==(CharPtr ptr, char ch)
		{
			return ptr[0] == ch;
		}

		// Token: 0x06005EF3 RID: 24307 RVA: 0x00268D50 File Offset: 0x00266F50
		public static bool operator ==(char ch, CharPtr ptr)
		{
			return ptr[0] == ch;
		}

		// Token: 0x06005EF4 RID: 24308 RVA: 0x00268D5C File Offset: 0x00266F5C
		public static bool operator !=(CharPtr ptr, char ch)
		{
			return ptr[0] != ch;
		}

		// Token: 0x06005EF5 RID: 24309 RVA: 0x00268D6B File Offset: 0x00266F6B
		public static bool operator !=(char ch, CharPtr ptr)
		{
			return ptr[0] != ch;
		}

		// Token: 0x06005EF6 RID: 24310 RVA: 0x00268D7C File Offset: 0x00266F7C
		public static CharPtr operator +(CharPtr ptr1, CharPtr ptr2)
		{
			string text = "";
			int num = 0;
			while (ptr1[num] != '\0')
			{
				text += ptr1[num].ToString();
				num++;
			}
			int num2 = 0;
			while (ptr2[num2] != '\0')
			{
				text += ptr2[num2].ToString();
				num2++;
			}
			return new CharPtr(text);
		}

		// Token: 0x06005EF7 RID: 24311 RVA: 0x00268DE3 File Offset: 0x00266FE3
		public static int operator -(CharPtr ptr1, CharPtr ptr2)
		{
			return ptr1.index - ptr2.index;
		}

		// Token: 0x06005EF8 RID: 24312 RVA: 0x00268DF2 File Offset: 0x00266FF2
		public static bool operator <(CharPtr ptr1, CharPtr ptr2)
		{
			return ptr1.index < ptr2.index;
		}

		// Token: 0x06005EF9 RID: 24313 RVA: 0x00268E02 File Offset: 0x00267002
		public static bool operator <=(CharPtr ptr1, CharPtr ptr2)
		{
			return ptr1.index <= ptr2.index;
		}

		// Token: 0x06005EFA RID: 24314 RVA: 0x00268E15 File Offset: 0x00267015
		public static bool operator >(CharPtr ptr1, CharPtr ptr2)
		{
			return ptr1.index > ptr2.index;
		}

		// Token: 0x06005EFB RID: 24315 RVA: 0x00268E25 File Offset: 0x00267025
		public static bool operator >=(CharPtr ptr1, CharPtr ptr2)
		{
			return ptr1.index >= ptr2.index;
		}

		// Token: 0x06005EFC RID: 24316 RVA: 0x00268E38 File Offset: 0x00267038
		public static bool operator ==(CharPtr ptr1, CharPtr ptr2)
		{
			return (ptr1 == null && ptr2 == null) || (ptr1 != null && ptr2 != null && ptr1.chars == ptr2.chars && ptr1.index == ptr2.index);
		}

		// Token: 0x06005EFD RID: 24317 RVA: 0x00268E79 File Offset: 0x00267079
		public static bool operator !=(CharPtr ptr1, CharPtr ptr2)
		{
			return !(ptr1 == ptr2);
		}

		// Token: 0x06005EFE RID: 24318 RVA: 0x00268E85 File Offset: 0x00267085
		public override bool Equals(object o)
		{
			return this == o as CharPtr;
		}

		// Token: 0x06005EFF RID: 24319 RVA: 0x0000280F File Offset: 0x00000A0F
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06005F00 RID: 24320 RVA: 0x00268E94 File Offset: 0x00267094
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = this.index;
			while (num < this.chars.Length && this.chars[num] != '\0')
			{
				stringBuilder.Append(this.chars[num]);
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005F01 RID: 24321 RVA: 0x00268EE0 File Offset: 0x002670E0
		public string ToString(int length)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = this.index;
			while (num < this.chars.Length && num < length + this.index)
			{
				stringBuilder.Append(this.chars[num]);
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04005467 RID: 21607
		public char[] chars;

		// Token: 0x04005468 RID: 21608
		public int index;
	}
}
