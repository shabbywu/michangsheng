using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CAC RID: 3244
	[Serializable]
	public class DynamicExpressionException : ScriptRuntimeException
	{
		// Token: 0x06005AEB RID: 23275 RVA: 0x00259232 File Offset: 0x00257432
		public DynamicExpressionException(string format, params object[] args) : base("<dynamic>: " + format, args)
		{
		}

		// Token: 0x06005AEC RID: 23276 RVA: 0x00259246 File Offset: 0x00257446
		public DynamicExpressionException(string message) : base("<dynamic>: " + message)
		{
		}
	}
}
