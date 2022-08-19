using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x02000D4A RID: 3402
	internal class ClosureContext : List<DynValue>
	{
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06005FE0 RID: 24544 RVA: 0x0026D0CD File Offset: 0x0026B2CD
		// (set) Token: 0x06005FE1 RID: 24545 RVA: 0x0026D0D5 File Offset: 0x0026B2D5
		public string[] Symbols { get; private set; }

		// Token: 0x06005FE2 RID: 24546 RVA: 0x0026D0DE File Offset: 0x0026B2DE
		internal ClosureContext(SymbolRef[] symbols, IEnumerable<DynValue> values)
		{
			this.Symbols = (from s in symbols
			select s.i_Name).ToArray<string>();
			base.AddRange(values);
		}

		// Token: 0x06005FE3 RID: 24547 RVA: 0x0026D11D File Offset: 0x0026B31D
		internal ClosureContext()
		{
			this.Symbols = new string[0];
		}
	}
}
