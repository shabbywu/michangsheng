using System;

namespace MoonSharp.VsCodeDebugger.SDK
{
	// Token: 0x020011CD RID: 4557
	public class Capabilities : ResponseBody
	{
		// Token: 0x040062AD RID: 25261
		public bool supportsConfigurationDoneRequest;

		// Token: 0x040062AE RID: 25262
		public bool supportsFunctionBreakpoints;

		// Token: 0x040062AF RID: 25263
		public bool supportsConditionalBreakpoints;

		// Token: 0x040062B0 RID: 25264
		public bool supportsEvaluateForHovers;

		// Token: 0x040062B1 RID: 25265
		public object[] exceptionBreakpointFilters;
	}
}
