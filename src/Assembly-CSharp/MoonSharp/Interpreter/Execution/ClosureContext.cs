using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x02001158 RID: 4440
	internal class ClosureContext : List<DynValue>
	{
		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06006BC4 RID: 27588 RVA: 0x00049678 File Offset: 0x00047878
		// (set) Token: 0x06006BC5 RID: 27589 RVA: 0x00049680 File Offset: 0x00047880
		public string[] Symbols { get; private set; }

		// Token: 0x06006BC6 RID: 27590 RVA: 0x00049689 File Offset: 0x00047889
		internal ClosureContext(SymbolRef[] symbols, IEnumerable<DynValue> values)
		{
			this.Symbols = (from s in symbols
			select s.i_Name).ToArray<string>();
			base.AddRange(values);
		}

		// Token: 0x06006BC7 RID: 27591 RVA: 0x000496C8 File Offset: 0x000478C8
		internal ClosureContext()
		{
			this.Symbols = new string[0];
		}
	}
}
