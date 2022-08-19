using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CAB RID: 3243
	public class YieldRequest
	{
		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06005AE8 RID: 23272 RVA: 0x00259221 File Offset: 0x00257421
		// (set) Token: 0x06005AE9 RID: 23273 RVA: 0x00259229 File Offset: 0x00257429
		public bool Forced { get; internal set; }

		// Token: 0x040052A5 RID: 21157
		public DynValue[] ReturnValues;
	}
}
