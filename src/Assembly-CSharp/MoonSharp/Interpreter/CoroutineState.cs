using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000C9A RID: 3226
	public enum CoroutineState
	{
		// Token: 0x0400525D RID: 21085
		Main,
		// Token: 0x0400525E RID: 21086
		NotStarted,
		// Token: 0x0400525F RID: 21087
		Suspended,
		// Token: 0x04005260 RID: 21088
		ForceSuspended,
		// Token: 0x04005261 RID: 21089
		Running,
		// Token: 0x04005262 RID: 21090
		Dead
	}
}
