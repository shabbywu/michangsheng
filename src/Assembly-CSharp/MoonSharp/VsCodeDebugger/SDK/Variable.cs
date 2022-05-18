using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011C3 RID: 4547
	public class Variable
	{
		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06006F66 RID: 28518 RVA: 0x0004BAE1 File Offset: 0x00049CE1
		// (set) Token: 0x06006F67 RID: 28519 RVA: 0x0004BAE9 File Offset: 0x00049CE9
		public string name { get; private set; }

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06006F68 RID: 28520 RVA: 0x0004BAF2 File Offset: 0x00049CF2
		// (set) Token: 0x06006F69 RID: 28521 RVA: 0x0004BAFA File Offset: 0x00049CFA
		public string value { get; private set; }

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06006F6A RID: 28522 RVA: 0x0004BB03 File Offset: 0x00049D03
		// (set) Token: 0x06006F6B RID: 28523 RVA: 0x0004BB0B File Offset: 0x00049D0B
		public int variablesReference { get; private set; }

		// Token: 0x06006F6C RID: 28524 RVA: 0x0004BB14 File Offset: 0x00049D14
		public Variable(string name, string value, int variablesReference = 0)
		{
			this.name = name;
			this.value = value;
			this.variablesReference = variablesReference;
		}
	}
}
