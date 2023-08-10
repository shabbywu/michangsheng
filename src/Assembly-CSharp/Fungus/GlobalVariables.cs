using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

public class GlobalVariables : MonoBehaviour
{
	private Flowchart holder;

	private Dictionary<string, Variable> variables = new Dictionary<string, Variable>();

	private void Awake()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		holder = new GameObject("GlobalVariables").AddComponent<Flowchart>();
		((Component)holder).transform.parent = ((Component)this).transform;
	}

	public Variable GetVariable(string variableKey)
	{
		Variable value = null;
		variables.TryGetValue(variableKey, out value);
		return value;
	}

	public VariableBase<T> GetOrAddVariable<T>(string variableKey, T defaultvalue, Type type)
	{
		Variable value = null;
		VariableBase<T> variableBase = null;
		if (variables.TryGetValue(variableKey, out value) && (Object)(object)value != (Object)null)
		{
			variableBase = value as VariableBase<T>;
			if ((Object)(object)variableBase != (Object)null)
			{
				return variableBase;
			}
			Debug.LogError((object)("A fungus variable of name " + variableKey + " already exists, but of a different type"));
		}
		else
		{
			variableBase = ((Component)holder).gameObject.AddComponent(type) as VariableBase<T>;
			variableBase.Value = defaultvalue;
			variableBase.Key = variableKey;
			variableBase.Scope = VariableScope.Public;
			variables[variableKey] = variableBase;
			holder.Variables.Add(variableBase);
		}
		return variableBase;
	}
}
