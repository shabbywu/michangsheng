using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x0200107C RID: 4220
	[Serializable]
	public class InternalErrorException : InterpreterException
	{
		// Token: 0x060065DF RID: 26079 RVA: 0x000462B0 File Offset: 0x000444B0
		internal InternalErrorException(string message) : base(message)
		{
		}

		// Token: 0x060065E0 RID: 26080 RVA: 0x000462B9 File Offset: 0x000444B9
		internal InternalErrorException(string format, params object[] args) : base(format, args)
		{
		}
	}
}
