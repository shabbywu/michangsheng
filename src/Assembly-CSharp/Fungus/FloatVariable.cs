using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200137C RID: 4988
	[VariableInfo("", "Float", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class FloatVariable : VariableBase<float>
	{
		// Token: 0x060078D7 RID: 30935 RVA: 0x002B7E28 File Offset: 0x002B6028
		public virtual bool Evaluate(CompareOperator compareOperator, float floatValue)
		{
			float value = this.Value;
			bool result = false;
			switch (compareOperator)
			{
			case CompareOperator.Equals:
				result = (value == floatValue);
				break;
			case CompareOperator.NotEquals:
				result = (value != floatValue);
				break;
			case CompareOperator.LessThan:
				result = (value < floatValue);
				break;
			case CompareOperator.GreaterThan:
				result = (value > floatValue);
				break;
			case CompareOperator.LessThanOrEquals:
				result = (value <= floatValue);
				break;
			case CompareOperator.GreaterThanOrEquals:
				result = (value >= floatValue);
				break;
			default:
				Debug.LogError("The " + compareOperator.ToString() + " comparison operator is not valid.");
				break;
			}
			return result;
		}

		// Token: 0x060078D8 RID: 30936 RVA: 0x002B7EB8 File Offset: 0x002B60B8
		public override void Apply(SetOperator setOperator, float value)
		{
			switch (setOperator)
			{
			case SetOperator.Assign:
				this.Value = value;
				return;
			case SetOperator.Add:
				this.Value += value;
				return;
			case SetOperator.Subtract:
				this.Value -= value;
				return;
			case SetOperator.Multiply:
				this.Value *= value;
				return;
			case SetOperator.Divide:
				this.Value /= value;
				return;
			case SetOperator.Remainder:
				this.Value %= value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x040068DD RID: 26845
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals,
			CompareOperator.LessThan,
			CompareOperator.GreaterThan,
			CompareOperator.LessThanOrEquals,
			CompareOperator.GreaterThanOrEquals
		};

		// Token: 0x040068DE RID: 26846
		public static readonly SetOperator[] setOperators = new SetOperator[]
		{
			SetOperator.Assign,
			SetOperator.Add,
			SetOperator.Subtract,
			SetOperator.Multiply,
			SetOperator.Divide,
			SetOperator.Remainder
		};
	}
}
