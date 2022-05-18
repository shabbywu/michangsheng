using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001067 RID: 4199
	public enum DataType
	{
		// Token: 0x04005E2D RID: 24109
		Nil,
		// Token: 0x04005E2E RID: 24110
		Void,
		// Token: 0x04005E2F RID: 24111
		Boolean,
		// Token: 0x04005E30 RID: 24112
		Number,
		// Token: 0x04005E31 RID: 24113
		String,
		// Token: 0x04005E32 RID: 24114
		Function,
		// Token: 0x04005E33 RID: 24115
		Table,
		// Token: 0x04005E34 RID: 24116
		Tuple,
		// Token: 0x04005E35 RID: 24117
		UserData,
		// Token: 0x04005E36 RID: 24118
		Thread,
		// Token: 0x04005E37 RID: 24119
		ClrFunction,
		// Token: 0x04005E38 RID: 24120
		TailCallRequest,
		// Token: 0x04005E39 RID: 24121
		YieldRequest
	}
}
