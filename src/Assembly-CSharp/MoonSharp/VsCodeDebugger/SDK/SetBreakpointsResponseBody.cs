using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011D4 RID: 4564
	public class SetBreakpointsResponseBody : ResponseBody
	{
		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06006F9C RID: 28572 RVA: 0x0004BDE6 File Offset: 0x00049FE6
		// (set) Token: 0x06006F9D RID: 28573 RVA: 0x0004BDEE File Offset: 0x00049FEE
		public Breakpoint[] breakpoints { get; private set; }

		// Token: 0x06006F9E RID: 28574 RVA: 0x0004BDF7 File Offset: 0x00049FF7
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
