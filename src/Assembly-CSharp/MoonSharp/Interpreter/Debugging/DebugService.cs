using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x02000D66 RID: 3430
	public sealed class DebugService : IScriptPrivateResource
	{
		// Token: 0x060060EF RID: 24815 RVA: 0x002729FE File Offset: 0x00270BFE
		internal DebugService(Script script, Processor processor)
		{
			this.OwnerScript = script;
			this.m_Processor = processor;
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060060F0 RID: 24816 RVA: 0x00272A14 File Offset: 0x00270C14
		// (set) Token: 0x060060F1 RID: 24817 RVA: 0x00272A1C File Offset: 0x00270C1C
		public Script OwnerScript { get; private set; }

		// Token: 0x060060F2 RID: 24818 RVA: 0x00272A25 File Offset: 0x00270C25
		public HashSet<int> ResetBreakPoints(SourceCode src, HashSet<int> lines)
		{
			return this.m_Processor.ResetBreakPoints(src, lines);
		}

		// Token: 0x04005562 RID: 21858
		private Processor m_Processor;
	}
}
