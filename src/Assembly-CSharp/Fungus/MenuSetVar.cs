using System;
using UnityEngine;

namespace Fungus;

[Serializable]
public class MenuSetVar
{
	[Tooltip("The variable whos value will be set")]
	[VariableProperty(new Type[]
	{
		typeof(BooleanVariable),
		typeof(IntegerVariable),
		typeof(FloatVariable)
	})]
	[SerializeField]
	protected Variable variable;

	[Tooltip("设置的值")]
	[VariableProperty(new Type[]
	{
		typeof(BooleanVariable),
		typeof(IntegerVariable),
		typeof(FloatVariable)
	})]
	[SerializeField]
	protected Variable ToVariable;

	public void setValue()
	{
		Type type = ((object)variable).GetType();
		if (type == typeof(BooleanVariable))
		{
			BooleanVariable obj = variable as BooleanVariable;
			BooleanVariable booleanVariable = ToVariable as BooleanVariable;
			obj.Value = booleanVariable.Value;
		}
		else if (type == typeof(IntegerVariable))
		{
			IntegerVariable obj2 = variable as IntegerVariable;
			IntegerVariable integerVariable = ToVariable as IntegerVariable;
			obj2.Value = integerVariable.Value;
		}
		else if (type == typeof(FloatVariable))
		{
			FloatVariable obj3 = variable as FloatVariable;
			FloatVariable floatVariable = ToVariable as FloatVariable;
			obj3.Value = floatVariable.Value;
		}
	}
}
