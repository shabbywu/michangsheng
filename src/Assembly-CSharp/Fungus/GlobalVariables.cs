using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001363 RID: 4963
	public class GlobalVariables : MonoBehaviour
	{
		// Token: 0x06007875 RID: 30837 RVA: 0x00051D32 File Offset: 0x0004FF32
		private void Awake()
		{
			this.holder = new GameObject("GlobalVariables").AddComponent<Flowchart>();
			this.holder.transform.parent = base.transform;
		}

		// Token: 0x06007876 RID: 30838 RVA: 0x002B66F0 File Offset: 0x002B48F0
		public Variable GetVariable(string variableKey)
		{
			Variable result = null;
			this.variables.TryGetValue(variableKey, out result);
			return result;
		}

		// Token: 0x06007877 RID: 30839 RVA: 0x002B6710 File Offset: 0x002B4910
		public VariableBase<T> GetOrAddVariable<T>(string variableKey, T defaultvalue, Type type)
		{
			Variable variable = null;
			VariableBase<T> variableBase;
			if (this.variables.TryGetValue(variableKey, out variable) && variable != null)
			{
				variableBase = (variable as VariableBase<T>);
				if (variableBase != null)
				{
					return variableBase;
				}
				Debug.LogError("A fungus variable of name " + variableKey + " already exists, but of a different type");
			}
			else
			{
				variableBase = (this.holder.gameObject.AddComponent(type) as VariableBase<T>);
				variableBase.Value = defaultvalue;
				variableBase.Key = variableKey;
				variableBase.Scope = VariableScope.Public;
				this.variables[variableKey] = variableBase;
				this.holder.Variables.Add(variableBase);
			}
			return variableBase;
		}

		// Token: 0x04006876 RID: 26742
		private Flowchart holder;

		// Token: 0x04006877 RID: 26743
		private Dictionary<string, Variable> variables = new Dictionary<string, Variable>();
	}
}
