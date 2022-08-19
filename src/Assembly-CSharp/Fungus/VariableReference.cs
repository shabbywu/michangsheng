using System;

namespace Fungus
{
	// Token: 0x02000ED2 RID: 3794
	[Serializable]
	public struct VariableReference
	{
		// Token: 0x06006B11 RID: 27409 RVA: 0x002958A0 File Offset: 0x00293AA0
		public T Get<T>()
		{
			T result = default(T);
			VariableBase<T> variableBase = this.variable as VariableBase<T>;
			if (variableBase != null)
			{
				return variableBase.Value;
			}
			return result;
		}

		// Token: 0x06006B12 RID: 27410 RVA: 0x002958D4 File Offset: 0x00293AD4
		public void Set<T>(T val)
		{
			VariableBase<T> variableBase = this.variable as VariableBase<T>;
			if (variableBase != null)
			{
				variableBase.Value = val;
			}
		}

		// Token: 0x04005A63 RID: 23139
		public Variable variable;
	}
}
