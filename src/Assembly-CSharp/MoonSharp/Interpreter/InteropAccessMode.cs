using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CB8 RID: 3256
	public enum InteropAccessMode
	{
		// Token: 0x040052BA RID: 21178
		Reflection,
		// Token: 0x040052BB RID: 21179
		LazyOptimized,
		// Token: 0x040052BC RID: 21180
		Preoptimized,
		// Token: 0x040052BD RID: 21181
		BackgroundOptimized,
		// Token: 0x040052BE RID: 21182
		Hardwired,
		// Token: 0x040052BF RID: 21183
		HideMembers,
		// Token: 0x040052C0 RID: 21184
		NoReflectionAllowed,
		// Token: 0x040052C1 RID: 21185
		Default
	}
}
