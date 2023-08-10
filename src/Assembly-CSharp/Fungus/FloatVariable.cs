using System;
using UnityEngine;

namespace Fungus;

[Serializable]
[VariableInfo("", "Float", 0)]
[AddComponentMenu("")]
public class FloatVariable : VariableBase<float>
{
	public static readonly CompareOperator[] compareOperators = new CompareOperator[6]
	{
		CompareOperator.Equals,
		CompareOperator.NotEquals,
		CompareOperator.LessThan,
		CompareOperator.GreaterThan,
		CompareOperator.LessThanOrEquals,
		CompareOperator.GreaterThanOrEquals
	};

	public static readonly SetOperator[] setOperators = new SetOperator[6]
	{
		SetOperator.Assign,
		SetOperator.Add,
		SetOperator.Subtract,
		SetOperator.Multiply,
		SetOperator.Divide,
		SetOperator.Remainder
	};

	public virtual bool Evaluate(CompareOperator compareOperator, float floatValue)
	{
		float num = Value;
		bool result = false;
		switch (compareOperator)
		{
		case CompareOperator.Equals:
			result = num == floatValue;
			break;
		case CompareOperator.NotEquals:
			result = num != floatValue;
			break;
		case CompareOperator.LessThan:
			result = num < floatValue;
			break;
		case CompareOperator.GreaterThan:
			result = num > floatValue;
			break;
		case CompareOperator.LessThanOrEquals:
			result = num <= floatValue;
			break;
		case CompareOperator.GreaterThanOrEquals:
			result = num >= floatValue;
			break;
		default:
			Debug.LogError((object)("The " + compareOperator.ToString() + " comparison operator is not valid."));
			break;
		}
		return result;
	}

	public override void Apply(SetOperator setOperator, float value)
	{
		switch (setOperator)
		{
		case SetOperator.Assign:
			Value = value;
			break;
		case SetOperator.Add:
			Value += value;
			break;
		case SetOperator.Subtract:
			Value -= value;
			break;
		case SetOperator.Multiply:
			Value *= value;
			break;
		case SetOperator.Divide:
			Value /= value;
			break;
		case SetOperator.Remainder:
			Value %= value;
			break;
		default:
			Debug.LogError((object)("The " + setOperator.ToString() + " set operator is not valid."));
			break;
		}
	}
}
