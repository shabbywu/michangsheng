using System;
using System.Text;

namespace MoonSharp.Interpreter.Interop.LuaStateInterop
{
	// Token: 0x0200113C RID: 4412
	public class CharPtr
	{
		// Token: 0x170009B9 RID: 2489
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

		// Token: 0x170009BA RID: 2490
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

		// Token: 0x170009BB RID: 2491
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

		// Token: 0x06006AAF RID: 27311 RVA: 0x00048BCF File Offset: 0x00046DCF
		public static implicit operator CharPtr(string str)
		{
			return new CharPtr(str);
		}

		// Token: 0x06006AB0 RID: 27312 RVA: 0x00048BD7 File Offset: 0x00046DD7
		public static implicit operator CharPtr(char[] chars)
		{
			return new CharPtr(chars);
		}

		// Token: 0x06006AB1 RID: 27313 RVA: 0x00048BDF File Offset: 0x00046DDF
		public static implicit operator CharPtr(byte[] bytes)
		{
			return new CharPtr(bytes);
		}

		// Token: 0x06006AB2 RID: 27314 RVA: 0x00048BE7 File Offset: 0x00046DE7
		public CharPtr()
		{
			this.chars = null;
			this.index = 0;
		}

		// Token: 0x06006AB3 RID: 27315 RVA: 0x00048BFD File Offset: 0x00046DFD
		public CharPtr(string str)
		{
			this.chars = (str + "\0").ToCharArray();
			this.index = 0;
		}

		// Token: 0x06006AB4 RID: 27316 RVA: 0x00048C22 File Offset: 0x00046E22
		public CharPtr(CharPtr ptr)
		{
			this.chars = ptr.chars;
			this.index = ptr.index;
		}

		// Token: 0x06006AB5 RID: 27317 RVA: 0x00048C42 File Offset: 0x00046E42
		public CharPtr(CharPtr ptr, int index)
		{
			this.chars = ptr.chars;
			this.index = index;
		}

		// Token: 0x06006AB6 RID: 27318 RVA: 0x00048C5D File Offset: 0x00046E5D
		public CharPtr(char[] chars)
		{
			this.chars = chars;
			this.index = 0;
		}

		// Token: 0x06006AB7 RID: 27319 RVA: 0x00048C73 File Offset: 0x00046E73
		public CharPtr(char[] chars, int index)
		{
			this.chars = chars;
			this.index = index;
		}

		// Token: 0x06006AB8 RID: 27320 RVA: 0x00291054 File Offset: 0x0028F254
		public CharPtr(byte[] bytes)
		{
			this.chars = new char[bytes.Length];
			for (int i = 0; i < bytes.Length; i++)
			{
				this.chars[i] = (char)bytes[i];
			}
			this.index = 0;
		}

		// Token: 0x06006AB9 RID: 27321 RVA: 0x00048C89 File Offset: 0x00046E89
		public CharPtr(IntPtr ptr)
		{
			this.chars = new char[0];
			this.index = 0;
		}

		// Token: 0x06006ABA RID: 27322 RVA: 0x00048CA4 File Offset: 0x00046EA4
		public static CharPtr operator +(CharPtr ptr, int offset)
		{
			return new CharPtr(ptr.chars, ptr.index + offset);
		}

		// Token: 0x06006ABB RID: 27323 RVA: 0x00048CB9 File Offset: 0x00046EB9
		public static CharPtr operator -(CharPtr ptr, int offset)
		{
			return new CharPtr(ptr.chars, ptr.index - offset);
		}

		// Token: 0x06006ABC RID: 27324 RVA: 0x00048CA4 File Offset: 0x00046EA4
		public static CharPtr operator +(CharPtr ptr, uint offset)
		{
			return new CharPtr(ptr.chars, ptr.index + (int)offset);
		}

		// Token: 0x06006ABD RID: 27325 RVA: 0x00048CB9 File Offset: 0x00046EB9
		public static CharPtr operator -(CharPtr ptr, uint offset)
		{
			return new CharPtr(ptr.chars, ptr.index - (int)offset);
		}

		// Token: 0x06006ABE RID: 27326 RVA: 0x00048CCE File Offset: 0x00046ECE
		public void inc()
		{
			this.index++;
		}

		// Token: 0x06006ABF RID: 27327 RVA: 0x00048CDE File Offset: 0x00046EDE
		public void dec()
		{
			this.index--;
		}

		// Token: 0x06006AC0 RID: 27328 RVA: 0x00048CEE File Offset: 0x00046EEE
		public CharPtr next()
		{
			return new CharPtr(this.chars, this.index + 1);
		}

		// Token: 0x06006AC1 RID: 27329 RVA: 0x00048D03 File Offset: 0x00046F03
		public CharPtr prev()
		{
			return new CharPtr(this.chars, this.index - 1);
		}

		// Token: 0x06006AC2 RID: 27330 RVA: 0x00048CA4 File Offset: 0x00046EA4
		public CharPtr add(int ofs)
		{
			return new CharPtr(this.chars, this.index + ofs);
		}

		// Token: 0x06006AC3 RID: 27331 RVA: 0x00048CB9 File Offset: 0x00046EB9
		public CharPtr sub(int ofs)
		{
			return new CharPtr(this.chars, this.index - ofs);
		}

		// Token: 0x06006AC4 RID: 27332 RVA: 0x00048D18 File Offset: 0x00046F18
		public static bool operator ==(CharPtr ptr, char ch)
		{
			return ptr[0] == ch;
		}

		// Token: 0x06006AC5 RID: 27333 RVA: 0x00048D24 File Offset: 0x00046F24
		public static bool operator ==(char ch, CharPtr ptr)
		{
			return ptr[0] == ch;
		}

		// Token: 0x06006AC6 RID: 27334 RVA: 0x00048D30 File Offset: 0x00046F30
		public static bool operator !=(CharPtr ptr, char ch)
		{
			return ptr[0] != ch;
		}

		// Token: 0x06006AC7 RID: 27335 RVA: 0x00048D3F File Offset: 0x00046F3F
		public static bool operator !=(char ch, CharPtr ptr)
		{
			return ptr[0] != ch;
		}

		// Token: 0x06006AC8 RID: 27336 RVA: 0x00291098 File Offset: 0x0028F298
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

		// Token: 0x06006AC9 RID: 27337 RVA: 0x00048D4E File Offset: 0x00046F4E
		public static int operator -(CharPtr ptr1, CharPtr ptr2)
		{
			return ptr1.index - ptr2.index;
		}

		// Token: 0x06006ACA RID: 27338 RVA: 0x00048D5D File Offset: 0x00046F5D
		public static bool operator <(CharPtr ptr1, CharPtr ptr2)
		{
			return ptr1.index < ptr2.index;
		}

		// Token: 0x06006ACB RID: 27339 RVA: 0x00048D6D File Offset: 0x00046F6D
		public static bool operator <=(CharPtr ptr1, CharPtr ptr2)
		{
			return ptr1.index <= ptr2.index;
		}

		// Token: 0x06006ACC RID: 27340 RVA: 0x00048D80 File Offset: 0x00046F80
		public static bool operator >(CharPtr ptr1, CharPtr ptr2)
		{
			return ptr1.index > ptr2.index;
		}

		// Token: 0x06006ACD RID: 27341 RVA: 0x00048D90 File Offset: 0x00046F90
		public static bool operator >=(CharPtr ptr1, CharPtr ptr2)
		{
			return ptr1.index >= ptr2.index;
		}

		// Token: 0x06006ACE RID: 27342 RVA: 0x00291100 File Offset: 0x0028F300
		public static bool operator ==(CharPtr ptr1, CharPtr ptr2)
		{
			return (ptr1 == null && ptr2 == null) || (ptr1 != null && ptr2 != null && ptr1.chars == ptr2.chars && ptr1.index == ptr2.index);
		}

		// Token: 0x06006ACF RID: 27343 RVA: 0x00048DA3 File Offset: 0x00046FA3
		public static bool operator !=(CharPtr ptr1, CharPtr ptr2)
		{
			return !(ptr1 == ptr2);
		}

		// Token: 0x06006AD0 RID: 27344 RVA: 0x00048DAF File Offset: 0x00046FAF
		public override bool Equals(object o)
		{
			return this == o as CharPtr;
		}

		// Token: 0x06006AD1 RID: 27345 RVA: 0x00004050 File Offset: 0x00002250
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06006AD2 RID: 27346 RVA: 0x00291144 File Offset: 0x0028F344
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

		// Token: 0x06006AD3 RID: 27347 RVA: 0x00291190 File Offset: 0x0028F390
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

		// Token: 0x040060C9 RID: 24777
		public char[] chars;

		// Token: 0x040060CA RID: 24778
		public int index;
	}
}
