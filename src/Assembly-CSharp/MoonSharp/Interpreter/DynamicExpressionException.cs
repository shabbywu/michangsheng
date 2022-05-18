using System;

namespace MoonSharp.Interpreter
{
	// Token: 0x0200107B RID: 4219
	[Serializable]
	public class DynamicExpressionException : ScriptRuntimeException
	{
		// Token: 0x060065DD RID: 26077 RVA: 0x00046289 File Offset: 0x00044489
		public DynamicExpressionException(string format, params object[] args) : base("<dynamic>: " + format, args)
		{
		}

		// Token: 0x060065DE RID: 26078 RVA: 0x0004629D File Offset: 0x0004449D
		public DynamicExpressionException(string message) : base("<dynamic>: " + message)
		{
		}
	}
}
