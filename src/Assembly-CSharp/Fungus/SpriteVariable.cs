using System;
using UnityEngine;

namespace Fungus;

[Serializable]
[VariableInfo("Other", "Sprite", 0)]
[AddComponentMenu("")]
public class SpriteVariable : VariableBase<Sprite>
{
	public static readonly CompareOperator[] compareOperators = new CompareOperator[2]
	{
		CompareOperator.Equals,
		CompareOperator.NotEquals
	};

	public static readonly SetOperator[] setOperators = new SetOperator[1];

	public virtual bool Evaluate(CompareOperator compareOperator, Sprite value)
	{
		bool result = false;
		switch (compareOperator)
		{
		case CompareOperator.Equals:
			result = (Object)(object)Value == (Object)(object)value;
			break;
		case CompareOperator.NotEquals:
			result = (Object)(object)Value != (Object)(object)value;
			break;
		default:
			Debug.LogError((object)("The " + compareOperator.ToString() + " comparison operator is not valid."));
			break;
		}
		return result;
	}

	public override void Apply(SetOperator setOperator, Sprite value)
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
