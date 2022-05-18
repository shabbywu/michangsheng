using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011CF RID: 4559
	public class StackTraceResponseBody : ResponseBody
	{
		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06006F89 RID: 28553 RVA: 0x0004BCC9 File Offset: 0x00049EC9
		// (set) Token: 0x06006F8A RID: 28554 RVA: 0x0004BCD1 File Offset: 0x00049ED1
		public StackFrame[] stackFrames { get; private set; }

		// Token: 0x06006F8B RID: 28555 RVA: 0x0004BCDA File Offset: 0x00049EDA
		public StackTraceResponseBody(List<StackFrame> frames = null)
		{
			if (frames == null)
			{
				this.stackFrames = new StackFrame[0];
				return;
			}
			this.stackFrames = frames.ToArray<StackFrame>();
		}
	}
}
