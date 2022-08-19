using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000D95 RID: 3477
	public class StackFrame
	{
		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x0600630A RID: 25354 RVA: 0x0027A517 File Offset: 0x00278717
		// (set) Token: 0x0600630B RID: 25355 RVA: 0x0027A51F File Offset: 0x0027871F
		public int id { get; private set; }

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x0600630C RID: 25356 RVA: 0x0027A528 File Offset: 0x00278728
		// (set) Token: 0x0600630D RID: 25357 RVA: 0x0027A530 File Offset: 0x00278730
		public Source source { get; private set; }

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x0600630E RID: 25358 RVA: 0x0027A539 File Offset: 0x00278739
		// (set) Token: 0x0600630F RID: 25359 RVA: 0x0027A541 File Offset: 0x00278741
		public int line { get; private set; }

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06006310 RID: 25360 RVA: 0x0027A54A File Offset: 0x0027874A
		// (set) Token: 0x06006311 RID: 25361 RVA: 0x0027A552 File Offset: 0x00278752
		public int column { get; private set; }

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06006312 RID: 25362 RVA: 0x0027A55B File Offset: 0x0027875B
		// (set) Token: 0x06006313 RID: 25363 RVA: 0x0027A563 File Offset: 0x00278763
		public string name { get; private set; }

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06006314 RID: 25364 RVA: 0x0027A56C File Offset: 0x0027876C
		// (set) Token: 0x06006315 RID: 25365 RVA: 0x0027A574 File Offset: 0x00278774
		public int? endLine { get; private set; }

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06006316 RID: 25366 RVA: 0x0027A57D File Offset: 0x0027877D
		// (set) Token: 0x06006317 RID: 25367 RVA: 0x0027A585 File Offset: 0x00278785
		public int? endColumn { get; private set; }

		// Token: 0x06006318 RID: 25368 RVA: 0x0027A58E File Offset: 0x0027878E
		public StackFrame(int id, string name, Source source, int line, int column = 0, int? endLine = null, int? endColumn = null)
		{
			this.id = id;
			this.name = name;
			this.source = source;
			this.line = line;
			this.column = column;
			this.endLine = endLine;
			this.endColumn = endColumn;
		}
	}
}
