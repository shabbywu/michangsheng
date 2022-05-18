using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011C2 RID: 4546
	public class Scope
	{
		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06006F5F RID: 28511 RVA: 0x0004BA91 File Offset: 0x00049C91
		// (set) Token: 0x06006F60 RID: 28512 RVA: 0x0004BA99 File Offset: 0x00049C99
		public string name { get; private set; }

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06006F61 RID: 28513 RVA: 0x0004BAA2 File Offset: 0x00049CA2
		// (set) Token: 0x06006F62 RID: 28514 RVA: 0x0004BAAA File Offset: 0x00049CAA
		public int variablesReference { get; private set; }

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06006F63 RID: 28515 RVA: 0x0004BAB3 File Offset: 0x00049CB3
		// (set) Token: 0x06006F64 RID: 28516 RVA: 0x0004BABB File Offset: 0x00049CBB
		public bool expensive { get; private set; }

		// Token: 0x06006F65 RID: 28517 RVA: 0x0004BAC4 File Offset: 0x00049CC4
		public Scope(string name, int variablesReference, bool expensive = false)
		{
			this.name = name;
			this.variablesReference = variablesReference;
			this.expensive = expensive;
		}
	}
}
