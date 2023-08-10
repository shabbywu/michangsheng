using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public struct VariableReference
{
	public Variable variable;

	public T Get<T>()
	{
		T result = default(T);
		VariableBase<T> variableBase = variable as VariableBase<T>;
		if ((Object)(object)variableBase != (Object)null)
		{
			return variableBase.Value;
		}
		return result;
	}

	public void Set<T>(T val)
	{
		VariableBase<T> variableBase = variable as VariableBase<T>;
		if ((Object)(object)variableBase != (Object)null)
		{
			variableBase.Value = val;
		}
	}
}
