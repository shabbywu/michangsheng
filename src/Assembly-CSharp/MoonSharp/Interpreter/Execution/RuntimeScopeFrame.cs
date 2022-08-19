using System;
using System.Collections.Generic;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x02000D4F RID: 3407
	internal class RuntimeScopeFrame
	{
		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06005FF0 RID: 24560 RVA: 0x0026D1A9 File Offset: 0x0026B3A9
		// (set) Token: 0x06005FF1 RID: 24561 RVA: 0x0026D1B1 File Offset: 0x0026B3B1
		public List<SymbolRef> DebugSymbols { get; private set; }

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06005FF2 RID: 24562 RVA: 0x0026D1BA File Offset: 0x0026B3BA
		public int Count
		{
			get
			{
				return this.DebugSymbols.Count;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06005FF3 RID: 24563 RVA: 0x0026D1C7 File Offset: 0x0026B3C7
		// (set) Token: 0x06005FF4 RID: 24564 RVA: 0x0026D1CF File Offset: 0x0026B3CF
		public int ToFirstBlock { get; internal set; }

		// Token: 0x06005FF5 RID: 24565 RVA: 0x0026D1D8 File Offset: 0x0026B3D8
		public RuntimeScopeFrame()
		{
			this.DebugSymbols = new List<SymbolRef>();
		}

		// Token: 0x06005FF6 RID: 24566 RVA: 0x0026D1EB File Offset: 0x0026B3EB
		public override string ToString()
		{
			return string.Format("ScopeFrame : #{0}", this.Count);
		}
	}
}
