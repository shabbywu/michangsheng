using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D0E RID: 3342
	public class AnonWrapper<T> : AnonWrapper
	{
		// Token: 0x06005D87 RID: 23943 RVA: 0x00262ED5 File Offset: 0x002610D5
		public AnonWrapper()
		{
		}

		// Token: 0x06005D88 RID: 23944 RVA: 0x00262EDD File Offset: 0x002610DD
		public AnonWrapper(T o)
		{
			this.Value = o;
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06005D89 RID: 23945 RVA: 0x00262EEC File Offset: 0x002610EC
		// (set) Token: 0x06005D8A RID: 23946 RVA: 0x00262EF4 File Offset: 0x002610F4
		public T Value { get; set; }
	}
}
