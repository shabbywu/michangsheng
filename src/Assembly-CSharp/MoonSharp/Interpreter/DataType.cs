using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000C9B RID: 3227
	public enum DataType
	{
		// Token: 0x04005264 RID: 21092
		Nil,
		// Token: 0x04005265 RID: 21093
		Void,
		// Token: 0x04005266 RID: 21094
		Boolean,
		// Token: 0x04005267 RID: 21095
		Number,
		// Token: 0x04005268 RID: 21096
		String,
		// Token: 0x04005269 RID: 21097
		Function,
		// Token: 0x0400526A RID: 21098
		Table,
		// Token: 0x0400526B RID: 21099
		Tuple,
		// Token: 0x0400526C RID: 21100
		UserData,
		// Token: 0x0400526D RID: 21101
		Thread,
		// Token: 0x0400526E RID: 21102
		ClrFunction,
		// Token: 0x0400526F RID: 21103
		TailCallRequest,
		// Token: 0x04005270 RID: 21104
		YieldRequest
	}
}
