using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EDB RID: 3803
	[VariableInfo("", "Float", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class FloatVariable : VariableBase<float>
	{
		// Token: 0x06006B38 RID: 27448 RVA: 0x00295E58 File Offset: 0x00294058
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

		// Token: 0x06006B39 RID: 27449 RVA: 0x00295EE8 File Offset: 0x002940E8
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

		// Token: 0x04005A74 RID: 23156
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals,
			CompareOperator.LessThan,
			CompareOperator.GreaterThan,
			CompareOperator.LessThanOrEquals,
			CompareOperator.GreaterThanOrEquals
		};

		// Token: 0x04005A75 RID: 23157
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
