using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000DA0 RID: 3488
	public class OutputEvent : Event
	{
		// Token: 0x0600633E RID: 25406 RVA: 0x0027A7C7 File Offset: 0x002789C7
		public OutputEvent(string cat, string outpt) : base("output", new
		{
			category = cat,
			output = outpt
		})
		{
		}
	}
}
