using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011C1 RID: 4545
	public class StackFrame
	{
		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06006F50 RID: 28496 RVA: 0x0004B9DD File Offset: 0x00049BDD
		// (set) Token: 0x06006F51 RID: 28497 RVA: 0x0004B9E5 File Offset: 0x00049BE5
		public int id { get; private set; }

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06006F52 RID: 28498 RVA: 0x0004B9EE File Offset: 0x00049BEE
		// (set) Token: 0x06006F53 RID: 28499 RVA: 0x0004B9F6 File Offset: 0x00049BF6
		public Source source { get; private set; }

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06006F54 RID: 28500 RVA: 0x0004B9FF File Offset: 0x00049BFF
		// (set) Token: 0x06006F55 RID: 28501 RVA: 0x0004BA07 File Offset: 0x00049C07
		public int line { get; private set; }

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06006F56 RID: 28502 RVA: 0x0004BA10 File Offset: 0x00049C10
		// (set) Token: 0x06006F57 RID: 28503 RVA: 0x0004BA18 File Offset: 0x00049C18
		public int column { get; private set; }

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06006F58 RID: 28504 RVA: 0x0004BA21 File Offset: 0x00049C21
		// (set) Token: 0x06006F59 RID: 28505 RVA: 0x0004BA29 File Offset: 0x00049C29
		public string name { get; private set; }

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06006F5A RID: 28506 RVA: 0x0004BA32 File Offset: 0x00049C32
		// (set) Token: 0x06006F5B RID: 28507 RVA: 0x0004BA3A File Offset: 0x00049C3A
		public int? endLine { get; private set; }

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06006F5C RID: 28508 RVA: 0x0004BA43 File Offset: 0x00049C43
		// (set) Token: 0x06006F5D RID: 28509 RVA: 0x0004BA4B File Offset: 0x00049C4B
		public int? endColumn { get; private set; }

		// Token: 0x06006F5E RID: 28510 RVA: 0x0004BA54 File Offset: 0x00049C54
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
