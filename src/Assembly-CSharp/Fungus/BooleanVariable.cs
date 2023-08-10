using System;
using UnityEngine;

namespace Fungus;

[Serializable]
[VariableInfo("", "Boolean", 0)]
[AddComponentMenu("")]
public class BooleanVariable : VariableBase<bool>
{
	public static readonly CompareOperator[] compareOperators = new CompareOperator[2]
	{
		CompareOperator.Equals,
		CompareOperator.NotEquals
	};

	public static readonly SetOperator[] setOperators = new SetOperator[2]
	{
		SetOperator.Assign,
		SetOperator.Negate
	};

	public virtual bool Evaluate(CompareOperator compareOperator, bool booleanValue)
	{
		bool result = false;
		bool flag = Value;
		bool flag2 = booleanValue;
		switch (compareOperator)
		{
		case CompareOperator.Equals:
			result = flag == flag2;
			break;
		case CompareOperator.NotEquals:
			result = flag != flag2;
			break;
		default:
			Debug.LogError((object)("The " + compareOperator.ToString() + " comparison operator is not valid."));
			break;
		}
		return result;
	}

	public override void Apply(SetOperator setOperator, bool value)
	{
		switch (setOperator)
		{
		case SetOperator.Assign:
			Value = value;
			break;
		case SetOperator.Negate:
			Value = !value;
			break;
		default:
			Debug.LogError((object)("The " + setOperator.ToString() + " set operator is not valid."));
			break;
		}
	}
}
