using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CAD RID: 3245
	[Serializable]
	public class InternalErrorException : InterpreterException
	{
		// Token: 0x06005AED RID: 23277 RVA: 0x00259259 File Offset: 0x00257459
		internal InternalErrorException(string message) : base(message)
		{
		}

		// Token: 0x06005AEE RID: 23278 RVA: 0x00259262 File Offset: 0x00257462
		internal InternalErrorException(string format, params object[] args) : base(format, args)
		{
		}
	}
}
