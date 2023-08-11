using System;
using UnityEngine;

namespace Fungus;

[Serializable]
[VariableInfo("", "String", 0)]
[AddComponentMenu("")]
public class StringVariable : VariableBase<string>
{
	public static readonly CompareOperator[] compareOperators = new CompareOperator[2]
	{
		CompareOperator.Equals,
		CompareOperator.NotEquals
	};

	public static readonly SetOperator[] setOperators = new SetOperator[1];

	public virtual bool Evaluate(CompareOperator compareOperator, string stringValue)
	{
		string text = Value;
		bool result = false;
		switch (compareOperator)
		{
		case CompareOperator.Equals:
			result = text == stringValue;
			break;
		case CompareOperator.NotEquals:
			result = text != stringValue;
			break;
		default:
			Debug.LogError((object)("The " + compareOperator.ToString() + " comparison operator is not valid."));
			break;
		}
		return result;
	}

	public override void Apply(SetOperator setOperator, string value)
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
