using System;

namespace MoonSharp.Interpreter.Execution.VM
{
	// Token: 0x02001163 RID: 4451
	[Flags]
	internal enum CallStackItemFlags
	{
		// Token: 0x0400614F RID: 24911
		None = 0,
		// Token: 0x04006150 RID: 24912
		EntryPoint = 1,
		// Token: 0x04006151 RID: 24913
		ResumeEntryPoint = 3,
		// Token: 0x04006152 RID: 24914
		CallEntryPoint = 5,
		// Token: 0x04006153 RID: 24915
		TailCall = 16,
		// Token: 0x04006154 RID: 24916
		MethodCall = 32
	}
}
