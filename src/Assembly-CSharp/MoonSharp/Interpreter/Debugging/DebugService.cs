using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Execution.VM;

namespace MoonSharp.Interpreter.Debugging
{
	// Token: 0x0200117C RID: 4476
	public sealed class DebugService : IScriptPrivateResource
	{
		// Token: 0x06006CE3 RID: 27875 RVA: 0x0004A45D File Offset: 0x0004865D
		internal DebugService(Script script, Processor processor)
		{
			this.OwnerScript = script;
			this.m_Processor = processor;
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06006CE4 RID: 27876 RVA: 0x0004A473 File Offset: 0x00048673
		// (set) Token: 0x06006CE5 RID: 27877 RVA: 0x0004A47B File Offset: 0x0004867B
		public Script OwnerScript { get; private set; }

		// Token: 0x06006CE6 RID: 27878 RVA: 0x0004A484 File Offset: 0x00048684
		public HashSet<int> ResetBreakPoints(SourceCode src, HashSet<int> lines)
		{
			return this.m_Processor.ResetBreakPoints(src, lines);
		}

		// Token: 0x040061FD RID: 25085
		private Processor m_Processor;
	}
}
