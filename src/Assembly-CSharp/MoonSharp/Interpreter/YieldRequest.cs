using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x0200107A RID: 4218
	public class YieldRequest
	{
		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x060065DA RID: 26074 RVA: 0x00046278 File Offset: 0x00044478
		// (set) Token: 0x060065DB RID: 26075 RVA: 0x00046280 File Offset: 0x00044480
		public bool Forced { get; internal set; }

		// Token: 0x04005E7A RID: 24186
		public DynValue[] ReturnValues;
	}
}
