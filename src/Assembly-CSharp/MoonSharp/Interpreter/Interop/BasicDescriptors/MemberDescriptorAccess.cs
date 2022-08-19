using System;

namespace MoonSharp.Interpreter.Interop.BasicDescriptors
{
	// Token: 0x02000D44 RID: 3396
	[Flags]
	public enum MemberDescriptorAccess
	{
		// Token: 0x0400549E RID: 21662
		CanRead = 1,
		// Token: 0x0400549F RID: 21663
		CanWrite = 2,
		// Token: 0x040054A0 RID: 21664
		CanExecute = 4
	}
}
