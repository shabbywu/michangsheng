using System;
using UnityEngine;

namespace Fungus;

[Serializable]
[VariableInfo("Other", "Vector3", 0)]
[AddComponentMenu("")]
public class Vector3Variable : VariableBase<Vector3>
{
	public static readonly CompareOperator[] compareOperators = new CompareOperator[2]
	{
		CompareOperator.Equals,
		CompareOperator.NotEquals
	};

	public static readonly SetOperator[] setOperators = new SetOperator[3]
	{
		SetOperator.Assign,
		SetOperator.Add,
		SetOperator.Subtract
	};

	public virtual bool Evaluate(CompareOperator compareOperator, Vector3 value)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
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

	public override void Apply(SetOperator setOperator, Vector3 value)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
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
		default:
			Debug.LogError((object)("The " + setOperator.ToString() + " set operator is not valid."));
			break;
		}
	}
}
