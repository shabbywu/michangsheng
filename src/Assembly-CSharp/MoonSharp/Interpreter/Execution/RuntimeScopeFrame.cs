using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x0200115E RID: 4446
	internal class RuntimeScopeFrame
	{
		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06006BD7 RID: 27607 RVA: 0x00049768 File Offset: 0x00047968
		// (set) Token: 0x06006BD8 RID: 27608 RVA: 0x00049770 File Offset: 0x00047970
		public List<SymbolRef> DebugSymbols { get; private set; }

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06006BD9 RID: 27609 RVA: 0x00049779 File Offset: 0x00047979
		public int Count
		{
			get
			{
				return this.DebugSymbols.Count;
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06006BDA RID: 27610 RVA: 0x00049786 File Offset: 0x00047986
		// (set) Token: 0x06006BDB RID: 27611 RVA: 0x0004978E File Offset: 0x0004798E
		public int ToFirstBlock { get; internal set; }

		// Token: 0x06006BDC RID: 27612 RVA: 0x00049797 File Offset: 0x00047997
		public RuntimeScopeFrame()
		{
			this.DebugSymbols = new List<SymbolRef>();
		}

		// Token: 0x06006BDD RID: 27613 RVA: 0x000497AA File Offset: 0x000479AA
		public override string ToString()
		{
			return string.Format("ScopeFrame : #{0}", this.Count);
		}
	}
}
