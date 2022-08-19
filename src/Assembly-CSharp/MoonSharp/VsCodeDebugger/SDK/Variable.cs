using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000D97 RID: 3479
	public class Variable
	{
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06006320 RID: 25376 RVA: 0x0027A61B File Offset: 0x0027881B
		// (set) Token: 0x06006321 RID: 25377 RVA: 0x0027A623 File Offset: 0x00278823
		public string name { get; private set; }

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06006322 RID: 25378 RVA: 0x0027A62C File Offset: 0x0027882C
		// (set) Token: 0x06006323 RID: 25379 RVA: 0x0027A634 File Offset: 0x00278834
		public string value { get; private set; }

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06006324 RID: 25380 RVA: 0x0027A63D File Offset: 0x0027883D
		// (set) Token: 0x06006325 RID: 25381 RVA: 0x0027A645 File Offset: 0x00278845
		public int variablesReference { get; private set; }

		// Token: 0x06006326 RID: 25382 RVA: 0x0027A64E File Offset: 0x0027884E
		public Variable(string name, string value, int variablesReference = 0)
		{
			this.name = name;
			this.value = value;
			this.variablesReference = variablesReference;
		}
	}
}
