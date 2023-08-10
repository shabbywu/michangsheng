using System;
using UnityEngine;

namespace Fungus;

[Serializable]
[VariableInfo("Other", "Object", 0)]
[AddComponentMenu("")]
public class ObjectVariable : VariableBase<Object>
{
	public static readonly CompareOperator[] compareOperators = new CompareOperator[2]
	{
		CompareOperator.Equals,
		CompareOperator.NotEquals
	};

	public static readonly SetOperator[] setOperators = new SetOperator[1];

	public virtual bool Evaluate(CompareOperator compareOperator, Object value)
	{
		bool result = false;
		switch (compareOperator)
		{
		case CompareOperator.Equals:
			result = Value == value;
			break;
		case CompareOperator.NotEquals:
			result = Value != value;
			break;
		default:
			Debug.LogError((object)("The " + compareOperator.ToString() + " comparison operator is not valid."));
			break;
		}
		return result;
	}

	public override void Apply(SetOperator setOperator, Object value)
	{
		if (setOperator == SetOperator.Assign)
		{
			Value = value;
		}
		else
		{
			Debug.LogError((object)("The " + setOperator.ToString() + " set operator is not valid."));
		}
	}
}
