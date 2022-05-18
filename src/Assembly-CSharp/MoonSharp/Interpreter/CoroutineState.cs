using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001066 RID: 4198
	public enum CoroutineState
	{
		// Token: 0x04005E26 RID: 24102
		Main,
		// Token: 0x04005E27 RID: 24103
		NotStarted,
		// Token: 0x04005E28 RID: 24104
		Suspended,
		// Token: 0x04005E29 RID: 24105
		ForceSuspended,
		// Token: 0x04005E2A RID: 24106
		Running,
		// Token: 0x04005E2B RID: 24107
		Dead
	}
}
