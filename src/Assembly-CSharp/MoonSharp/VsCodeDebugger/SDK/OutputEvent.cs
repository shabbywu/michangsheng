using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011CC RID: 4556
	public class OutputEvent : Event
	{
		// Token: 0x06006F84 RID: 28548 RVA: 0x0004BC8D File Offset: 0x00049E8D
		public OutputEvent(string cat, string outpt) : base("output", new
		{
			category = cat,
			output = outpt
		})
		{
		}
	}
}
