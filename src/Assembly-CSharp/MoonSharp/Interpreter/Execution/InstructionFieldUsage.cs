using System;

namespace MoonSharp.Interpreter.Execution
{
	// Token: 0x02001155 RID: 4437
	[Flags]
	internal enum InstructionFieldUsage
	{
		// Token: 0x04006123 RID: 24867
		None = 0,
		// Token: 0x04006124 RID: 24868
		Symbol = 1,
		// Token: 0x04006125 RID: 24869
		SymbolList = 2,
		// Token: 0x04006126 RID: 24870
		Name = 4,
		// Token: 0x04006127 RID: 24871
		Value = 8,
		// Token: 0x04006128 RID: 24872
		NumVal = 16,
		// Token: 0x04006129 RID: 24873
		NumVal2 = 32,
		// Token: 0x0400612A RID: 24874
		NumValAsCodeAddress = 32784
	}
}
