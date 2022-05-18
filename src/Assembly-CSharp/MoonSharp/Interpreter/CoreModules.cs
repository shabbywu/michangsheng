using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x0200108F RID: 4239
	[Flags]
	public enum CoreModules
	{
		// Token: 0x04005EA2 RID: 24226
		None = 0,
		// Token: 0x04005EA3 RID: 24227
		Basic = 64,
		// Token: 0x04005EA4 RID: 24228
		GlobalConsts = 1,
		// Token: 0x04005EA5 RID: 24229
		TableIterators = 2,
		// Token: 0x04005EA6 RID: 24230
		Metatables = 4,
		// Token: 0x04005EA7 RID: 24231
		String = 8,
		// Token: 0x04005EA8 RID: 24232
		LoadMethods = 16,
		// Token: 0x04005EA9 RID: 24233
		Table = 32,
		// Token: 0x04005EAA RID: 24234
		ErrorHandling = 128,
		// Token: 0x04005EAB RID: 24235
		Math = 256,
		// Token: 0x04005EAC RID: 24236
		Coroutine = 512,
		// Token: 0x04005EAD RID: 24237
		Bit32 = 1024,
		// Token: 0x04005EAE RID: 24238
		OS_Time = 2048,
		// Token: 0x04005EAF RID: 24239
		OS_System = 4096,
		// Token: 0x04005EB0 RID: 24240
		IO = 8192,
		// Token: 0x04005EB1 RID: 24241
		Debug = 16384,
		// Token: 0x04005EB2 RID: 24242
		Dynamic = 32768,
		// Token: 0x04005EB3 RID: 24243
		Json = 65536,
		// Token: 0x04005EB4 RID: 24244
		Preset_HardSandbox = 1387,
		// Token: 0x04005EB5 RID: 24245
		Preset_SoftSandbox = 102383,
		// Token: 0x04005EB6 RID: 24246
		Preset_Default = 114687,
		// Token: 0x04005EB7 RID: 24247
		Preset_Complete = 131071
	}
}
