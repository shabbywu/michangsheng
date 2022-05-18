using System;

namespace Fungus
{
	// Token: 0x02001373 RID: 4979
	[Serializable]
	public struct VariableReference
	{
		// Token: 0x060078B0 RID: 30896 RVA: 0x002B7BDC File Offset: 0x002B5DDC
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

		// Token: 0x060078B1 RID: 30897 RVA: 0x002B7C10 File Offset: 0x002B5E10
		public void Set<T>(T val)
		{
			VariableBase<T> variableBase = this.variable as VariableBase<T>;
			if (variableBase != null)
			{
				variableBase.Value = val;
			}
		}

		// Token: 0x040068CC RID: 26828
		public Variable variable;
	}
}
