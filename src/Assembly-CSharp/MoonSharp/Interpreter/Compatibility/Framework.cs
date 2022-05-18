using System;
using MoonSharp.Interpreter.Compatibility.Frameworks;

namespace MoonSharp.Interpreter.Compatibility
{
	// Token: 0x020011B0 RID: 4528
	public static class Framework
	{
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06006ED5 RID: 28373 RVA: 0x0004B688 File Offset: 0x00049888
		public static FrameworkBase Do
		{
			get
			{
				return Framework.s_FrameworkCurrent;
			}
		}

		// Token: 0x0400627A RID: 25210
		private static FrameworkCurrent s_FrameworkCurrent = new FrameworkCurrent();
	}
}
