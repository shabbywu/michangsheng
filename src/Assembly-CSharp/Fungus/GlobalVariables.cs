using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EC5 RID: 3781
	public class GlobalVariables : MonoBehaviour
	{
		// Token: 0x06006ADA RID: 27354 RVA: 0x00294313 File Offset: 0x00292513
		private void Awake()
		{
			this.holder = new GameObject("GlobalVariables").AddComponent<Flowchart>();
			this.holder.transform.parent = base.transform;
		}

		// Token: 0x06006ADB RID: 27355 RVA: 0x00294340 File Offset: 0x00292540
		public Variable GetVariable(string variableKey)
		{
			Variable result = null;
			this.variables.TryGetValue(variableKey, out result);
			return result;
		}

		// Token: 0x06006ADC RID: 27356 RVA: 0x00294360 File Offset: 0x00292560
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

		// Token: 0x04005A17 RID: 23063
		private Flowchart holder;

		// Token: 0x04005A18 RID: 23064
		private Dictionary<string, Variable> variables = new Dictionary<string, Variable>();
	}
}
