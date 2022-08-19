using System;
using System.Collections.Generic;
using System.Linq;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000DA3 RID: 3491
	public class StackTraceResponseBody : ResponseBody
	{
		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06006343 RID: 25411 RVA: 0x0027A803 File Offset: 0x00278A03
		// (set) Token: 0x06006344 RID: 25412 RVA: 0x0027A80B File Offset: 0x00278A0B
		public StackFrame[] stackFrames { get; private set; }

		// Token: 0x06006345 RID: 25413 RVA: 0x0027A814 File Offset: 0x00278A14
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
