using System;
using MoonSharp.Interpreter.Compatibility.Frameworks;

namespace MoonSharp.Interpreter.Compatibility
{
	// Token: 0x02000D8D RID: 3469
	public static class Framework
	{
		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x060062A4 RID: 25252 RVA: 0x00279B4E File Offset: 0x00277D4E
		public static FrameworkBase Do
		{
			get
			{
				return Framework.s_FrameworkCurrent;
			}
		}

		// Token: 0x040055A3 RID: 21923
		private static FrameworkCurrent s_FrameworkCurrent = new FrameworkCurrent();
	}
}
