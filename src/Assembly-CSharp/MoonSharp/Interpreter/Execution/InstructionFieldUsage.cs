using System;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x02000D47 RID: 3399
	[Flags]
	internal enum InstructionFieldUsage
	{
		// Token: 0x040054AA RID: 21674
		None = 0,
		// Token: 0x040054AB RID: 21675
		Symbol = 1,
		// Token: 0x040054AC RID: 21676
		SymbolList = 2,
		// Token: 0x040054AD RID: 21677
		Name = 4,
		// Token: 0x040054AE RID: 21678
		Value = 8,
		// Token: 0x040054AF RID: 21679
		NumVal = 16,
		// Token: 0x040054B0 RID: 21680
		NumVal2 = 32,
		// Token: 0x040054B1 RID: 21681
		NumValAsCodeAddress = 32784
	}
}
