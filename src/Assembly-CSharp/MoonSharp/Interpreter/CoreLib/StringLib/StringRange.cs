using System;

namespace MoonSharp.Interpreter.CoreLib.StringLib
{
	// Token: 0x02000D86 RID: 3462
	internal class StringRange
	{
		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x0600626E RID: 25198 RVA: 0x002792EE File Offset: 0x002774EE
		// (set) Token: 0x0600626F RID: 25199 RVA: 0x002792F6 File Offset: 0x002774F6
		public int Start { get; set; }

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06006270 RID: 25200 RVA: 0x002792FF File Offset: 0x002774FF
		// (set) Token: 0x06006271 RID: 25201 RVA: 0x00279307 File Offset: 0x00277507
		public int End { get; set; }

		// Token: 0x06006272 RID: 25202 RVA: 0x00279310 File Offset: 0x00277510
		public StringRange()
		{
			this.Start = 0;
			this.End = 0;
		}

		// Token: 0x06006273 RID: 25203 RVA: 0x00279326 File Offset: 0x00277526
		public StringRange(int start, int end)
		{
			this.Start = start;
			this.End = end;
		}

		// Token: 0x06006274 RID: 25204 RVA: 0x0027933C File Offset: 0x0027753C
		public static StringRange FromLuaRange(DynValue start, DynValue end, int? defaultEnd = null)
		{
			int num = start.IsNil() ? 1 : ((int)start.Number);
			int end2 = end.IsNil() ? (defaultEnd ?? num) : ((int)end.Number);
			return new StringRange(num, end2);
		}

		// Token: 0x06006275 RID: 25205 RVA: 0x0027938C File Offset: 0x0027758C
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

		// Token: 0x06006276 RID: 25206 RVA: 0x00279409 File Offset: 0x00277609
		public int Length()
		{
			return this.End - this.Start + 1;
		}
	}
}
