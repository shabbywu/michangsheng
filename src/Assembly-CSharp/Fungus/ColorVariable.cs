using System;
using UnityEngine;

namespace Fungus;

[Serializable]
[VariableInfo("Other", "Color", 0)]
[AddComponentMenu("")]
public class ColorVariable : VariableBase<Color>
{
	public static readonly CompareOperator[] compareOperators = new CompareOperator[2]
	{
		CompareOperator.Equals,
		CompareOperator.NotEquals
	};

	public static readonly SetOperator[] setOperators = new SetOperator[4]
	{
		SetOperator.Assign,
		SetOperator.Add,
		SetOperator.Subtract,
		SetOperator.Multiply
	};

	protected static bool ColorsEqual(Color a, Color b)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return ColorUtility.ToHtmlStringRGBA(a) == ColorUtility.ToHtmlStringRGBA(b);
	}

	public virtual bool Evaluate(CompareOperator compareOperator, Color value)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		bool result = false;
		switch (compareOperator)
		{
		case CompareOperator.Equals:
			result = ColorsEqual(Value, value);
			break;
		case CompareOperator.NotEquals:
			result = !ColorsEqual(Value, value);
			break;
		default:
			Debug.LogError((object)("The " + compareOperator.ToString() + " comparison operator is not valid."));
			break;
		}
		return result;
	}

	public override void Apply(SetOperator setOperator, Color value)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
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
		default:
			Debug.LogError((object)("The " + setOperator.ToString() + " set operator is not valid."));
			break;
		}
	}
}
