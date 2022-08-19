using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x02000DA1 RID: 3489
	public class Capabilities : ResponseBody
	{
		// Token: 0x040055C6 RID: 21958
		public bool supportsConfigurationDoneRequest;

		// Token: 0x040055C7 RID: 21959
		public bool supportsFunctionBreakpoints;

		// Token: 0x040055C8 RID: 21960
		public bool supportsConditionalBreakpoints;

		// Token: 0x040055C9 RID: 21961
		public bool supportsEvaluateForHovers;

		// Token: 0x040055CA RID: 21962
		public object[] exceptionBreakpointFilters;
	}
}
