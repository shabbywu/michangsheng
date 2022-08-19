using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000DA8 RID: 3496
	public class SetBreakpointsResponseBody : ResponseBody
	{
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06006356 RID: 25430 RVA: 0x0027A920 File Offset: 0x00278B20
		// (set) Token: 0x06006357 RID: 25431 RVA: 0x0027A928 File Offset: 0x00278B28
		public Breakpoint[] breakpoints { get; private set; }

		// Token: 0x06006358 RID: 25432 RVA: 0x0027A931 File Offset: 0x00278B31
		public SetBreakpointsResponseBody(List<Breakpoint> bpts = null)
		{
			if (bpts == null)
			{
				this.breakpoints = new Breakpoint[0];
				return;
			}
			this.breakpoints = bpts.ToArray<Breakpoint>();
		}
	}
}
