using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001087 RID: 4231
	public enum InteropAccessMode
	{
		// Token: 0x04005E8F RID: 24207
		Reflection,
		// Token: 0x04005E90 RID: 24208
		LazyOptimized,
		// Token: 0x04005E91 RID: 24209
		Preoptimized,
		// Token: 0x04005E92 RID: 24210
		BackgroundOptimized,
		// Token: 0x04005E93 RID: 24211
		Hardwired,
		// Token: 0x04005E94 RID: 24212
		HideMembers,
		// Token: 0x04005E95 RID: 24213
		NoReflectionAllowed,
		// Token: 0x04005E96 RID: 24214
		Default
	}
}
