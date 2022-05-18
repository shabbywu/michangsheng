using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001380 RID: 4992
	[VariableInfo("", "Integer", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class IntegerVariable : VariableBase<int>
	{
		// Token: 0x060078E9 RID: 30953 RVA: 0x002B7FB8 File Offset: 0x002B61B8
		public virtual bool Evaluate(CompareOperator compareOperator, int integerValue)
		{
			int value = this.Value;
			bool result = false;
			switch (compareOperator)
			{
			case CompareOperator.Equals:
				result = (value == integerValue);
				break;
			case CompareOperator.NotEquals:
				result = (value != integerValue);
				break;
			case CompareOperator.LessThan:
				result = (value < integerValue);
				break;
			case CompareOperator.GreaterThan:
				result = (value > integerValue);
				break;
			case CompareOperator.LessThanOrEquals:
				result = (value <= integerValue);
				break;
			case CompareOperator.GreaterThanOrEquals:
				result = (value >= integerValue);
				break;
			default:
				Debug.LogError("The " + compareOperator.ToString() + " comparison operator is not valid.");
				break;
			}
			return result;
		}

		// Token: 0x060078EA RID: 30954 RVA: 0x002B8048 File Offset: 0x002B6248
		public override void Apply(SetOperator setOperator, int value)
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

		// Token: 0x040068E5 RID: 26853
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals,
			CompareOperator.LessThan,
			CompareOperator.GreaterThan,
			CompareOperator.LessThanOrEquals,
			CompareOperator.GreaterThanOrEquals
		};

		// Token: 0x040068E6 RID: 26854
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
