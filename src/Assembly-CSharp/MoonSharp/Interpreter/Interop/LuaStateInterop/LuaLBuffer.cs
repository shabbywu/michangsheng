using System;
using System.Text;

namespace MoonSharp.Interpreter.Interop.LuaStateInterop
{
	// Token: 0x02000D36 RID: 3382
	public class LuaLBuffer
	{
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06005F48 RID: 24392 RVA: 0x00269607 File Offset: 0x00267807
		// (set) Token: 0x06005F49 RID: 24393 RVA: 0x0026960F File Offset: 0x0026780F
		public StringBuilder StringBuilder { get; private set; }

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06005F4A RID: 24394 RVA: 0x00269618 File Offset: 0x00267818
		// (set) Token: 0x06005F4B RID: 24395 RVA: 0x00269620 File Offset: 0x00267820
		public LuaState LuaState { get; private set; }

		// Token: 0x06005F4C RID: 24396 RVA: 0x00269629 File Offset: 0x00267829
		public LuaLBuffer(LuaState l)
		{
			this.StringBuilder = new StringBuilder();
			this.LuaState = l;
		}
	}
}
