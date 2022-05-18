using System;

namespace MoonSharp.Interpreter.CoreLib.StringLib
{
	// Token: 0x020011A8 RID: 4520
	internal class StringRange
	{
		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06006E9C RID: 28316 RVA: 0x0004B4B7 File Offset: 0x000496B7
		// (set) Token: 0x06006E9D RID: 28317 RVA: 0x0004B4BF File Offset: 0x000496BF
		public int Start { get; set; }

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06006E9E RID: 28318 RVA: 0x0004B4C8 File Offset: 0x000496C8
		// (set) Token: 0x06006E9F RID: 28319 RVA: 0x0004B4D0 File Offset: 0x000496D0
		public int End { get; set; }

		// Token: 0x06006EA0 RID: 28320 RVA: 0x0004B4D9 File Offset: 0x000496D9
		public StringRange()
		{
			this.Start = 0;
			this.End = 0;
		}

		// Token: 0x06006EA1 RID: 28321 RVA: 0x0004B4EF File Offset: 0x000496EF
		public StringRange(int start, int end)
		{
			this.Start = start;
			this.End = end;
		}

		// Token: 0x06006EA2 RID: 28322 RVA: 0x0029F324 File Offset: 0x0029D524
		public static StringRange FromLuaRange(DynValue start, DynValue end, int? defaultEnd = null)
		{
			int num = start.IsNil() ? 1 : ((int)start.Number);
			int end2 = end.IsNil() ? (defaultEnd ?? num) : ((int)end.Number);
			return new StringRange(num, end2);
		}

		// Token: 0x06006EA3 RID: 28323 RVA: 0x0029F374 File Offset: 0x0029D574
		public string ApplyToString(string value)
		{
			int num = (this.Start < 0) ? (this.Start + value.Length + 1) : this.Start;
			int num2 = (this.End < 0) ? (this.End + value.Length + 1) : this.End;
			if (num < 1)
			{
				num = 1;
			}
			if (num2 > value.Length)
			{
				num2 = value.Length;
			}
			if (num > num2)
			{
				return string.Empty;
			}
			return value.Substring(num - 1, num2 - num + 1);
		}

		// Token: 0x06006EA4 RID: 28324 RVA: 0x0004B505 File Offset: 0x00049705
		public int Length()
		{
			return this.End - this.Start + 1;
		}
	}
}
