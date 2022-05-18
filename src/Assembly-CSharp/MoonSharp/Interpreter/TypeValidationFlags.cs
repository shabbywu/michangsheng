using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001076 RID: 4214
	[Flags]
	public enum TypeValidationFlags
	{
		// Token: 0x04005E6F RID: 24175
		None = 0,
		// Token: 0x04005E70 RID: 24176
		AllowNil = 1,
		// Token: 0x04005E71 RID: 24177
		AutoConvert = 2,
		// Token: 0x04005E72 RID: 24178
		Default = 2
	}
}
