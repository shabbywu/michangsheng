using System;
using System.Text;

namespace MoonSharp.Interpreter.Interop.LuaStateInterop
{
	// Token: 0x0200113E RID: 4414
	public class LuaLBuffer
	{
		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06006B1A RID: 27418 RVA: 0x00049042 File Offset: 0x00047242
		// (set) Token: 0x06006B1B RID: 27419 RVA: 0x0004904A File Offset: 0x0004724A
		public StringBuilder StringBuilder { get; private set; }

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06006B1C RID: 27420 RVA: 0x00049053 File Offset: 0x00047253
		// (set) Token: 0x06006B1D RID: 27421 RVA: 0x0004905B File Offset: 0x0004725B
		public LuaState LuaState { get; private set; }

		// Token: 0x06006B1E RID: 27422 RVA: 0x00049064 File Offset: 0x00047264
		public LuaLBuffer(LuaState l)
		{
			this.StringBuilder = new StringBuilder();
			this.LuaState = l;
		}
	}
}
