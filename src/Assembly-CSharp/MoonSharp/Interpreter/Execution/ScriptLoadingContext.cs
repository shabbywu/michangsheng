using System;
using MoonSharp.Interpreter.Debugging;
using MoonSharp.Interpreter.Tree;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x0200115F RID: 4447
	internal class ScriptLoadingContext
	{
		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06006BDE RID: 27614 RVA: 0x000497C1 File Offset: 0x000479C1
		// (set) Token: 0x06006BDF RID: 27615 RVA: 0x000497C9 File Offset: 0x000479C9
		public Script Script { get; private set; }

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06006BE0 RID: 27616 RVA: 0x000497D2 File Offset: 0x000479D2
		// (set) Token: 0x06006BE1 RID: 27617 RVA: 0x000497DA File Offset: 0x000479DA
		public BuildTimeScope Scope { get; set; }

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06006BE2 RID: 27618 RVA: 0x000497E3 File Offset: 0x000479E3
		// (set) Token: 0x06006BE3 RID: 27619 RVA: 0x000497EB File Offset: 0x000479EB
		public SourceCode Source { get; set; }

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06006BE4 RID: 27620 RVA: 0x000497F4 File Offset: 0x000479F4
		// (set) Token: 0x06006BE5 RID: 27621 RVA: 0x000497FC File Offset: 0x000479FC
		public bool Anonymous { get; set; }

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06006BE6 RID: 27622 RVA: 0x00049805 File Offset: 0x00047A05
		// (set) Token: 0x06006BE7 RID: 27623 RVA: 0x0004980D File Offset: 0x00047A0D
		public bool IsDynamicExpression { get; set; }

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06006BE8 RID: 27624 RVA: 0x00049816 File Offset: 0x00047A16
		// (set) Token: 0x06006BE9 RID: 27625 RVA: 0x0004981E File Offset: 0x00047A1E
		public Lexer Lexer { get; set; }

		// Token: 0x06006BEA RID: 27626 RVA: 0x00049827 File Offset: 0x00047A27
		public ScriptLoadingContext(Script s)
		{
			this.Script = s;
		}
	}
}
