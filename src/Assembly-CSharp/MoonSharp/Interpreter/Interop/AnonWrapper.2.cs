using System;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x020010F2 RID: 4338
	public class AnonWrapper<T> : AnonWrapper
	{
		// Token: 0x060068B6 RID: 26806 RVA: 0x00047C61 File Offset: 0x00045E61
		public AnonWrapper()
		{
		}

		// Token: 0x060068B7 RID: 26807 RVA: 0x00047C69 File Offset: 0x00045E69
		public AnonWrapper(T o)
		{
			this.Value = o;
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x060068B8 RID: 26808 RVA: 0x00047C78 File Offset: 0x00045E78
		// (set) Token: 0x060068B9 RID: 26809 RVA: 0x00047C80 File Offset: 0x00045E80
		public T Value { get; set; }
	}
}
