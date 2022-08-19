using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Tree;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x02000D50 RID: 3408
	internal class ScriptLoadingContext
	{
		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06005FF7 RID: 24567 RVA: 0x0026D202 File Offset: 0x0026B402
		// (set) Token: 0x06005FF8 RID: 24568 RVA: 0x0026D20A File Offset: 0x0026B40A
		public Script Script { get; private set; }

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06005FF9 RID: 24569 RVA: 0x0026D213 File Offset: 0x0026B413
		// (set) Token: 0x06005FFA RID: 24570 RVA: 0x0026D21B File Offset: 0x0026B41B
		public BuildTimeScope Scope { get; set; }

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06005FFB RID: 24571 RVA: 0x0026D224 File Offset: 0x0026B424
		// (set) Token: 0x06005FFC RID: 24572 RVA: 0x0026D22C File Offset: 0x0026B42C
		public SourceCode Source { get; set; }

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06005FFD RID: 24573 RVA: 0x0026D235 File Offset: 0x0026B435
		// (set) Token: 0x06005FFE RID: 24574 RVA: 0x0026D23D File Offset: 0x0026B43D
		public bool Anonymous { get; set; }

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06005FFF RID: 24575 RVA: 0x0026D246 File Offset: 0x0026B446
		// (set) Token: 0x06006000 RID: 24576 RVA: 0x0026D24E File Offset: 0x0026B44E
		public bool IsDynamicExpression { get; set; }

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06006001 RID: 24577 RVA: 0x0026D257 File Offset: 0x0026B457
		// (set) Token: 0x06006002 RID: 24578 RVA: 0x0026D25F File Offset: 0x0026B45F
		public Lexer Lexer { get; set; }

		// Token: 0x06006003 RID: 24579 RVA: 0x0026D268 File Offset: 0x0026B468
		public ScriptLoadingContext(Script s)
		{
			this.Script = s;
		}
	}
}
