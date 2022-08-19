using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CBB RID: 3259
	[Flags]
	public enum CoreModules
	{
		// Token: 0x040052C5 RID: 21189
		None = 0,
		// Token: 0x040052C6 RID: 21190
		Basic = 64,
		// Token: 0x040052C7 RID: 21191
		GlobalConsts = 1,
		// Token: 0x040052C8 RID: 21192
		TableIterators = 2,
		// Token: 0x040052C9 RID: 21193
		Metatables = 4,
		// Token: 0x040052CA RID: 21194
		String = 8,
		// Token: 0x040052CB RID: 21195
		LoadMethods = 16,
		// Token: 0x040052CC RID: 21196
		Table = 32,
		// Token: 0x040052CD RID: 21197
		ErrorHandling = 128,
		// Token: 0x040052CE RID: 21198
		Math = 256,
		// Token: 0x040052CF RID: 21199
		Coroutine = 512,
		// Token: 0x040052D0 RID: 21200
		Bit32 = 1024,
		// Token: 0x040052D1 RID: 21201
		OS_Time = 2048,
		// Token: 0x040052D2 RID: 21202
		OS_System = 4096,
		// Token: 0x040052D3 RID: 21203
		IO = 8192,
		// Token: 0x040052D4 RID: 21204
		Debug = 16384,
		// Token: 0x040052D5 RID: 21205
		Dynamic = 32768,
		// Token: 0x040052D6 RID: 21206
		Json = 65536,
		// Token: 0x040052D7 RID: 21207
		Preset_HardSandbox = 1387,
		// Token: 0x040052D8 RID: 21208
		Preset_SoftSandbox = 102383,
		// Token: 0x040052D9 RID: 21209
		Preset_Default = 114687,
		// Token: 0x040052DA RID: 21210
		Preset_Complete = 131071
	}
}
