using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000ED7 RID: 3799
	[VariableInfo("", "Boolean", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class BooleanVariable : VariableBase<bool>
	{
		// Token: 0x06006B25 RID: 27429 RVA: 0x00295B60 File Offset: 0x00293D60
		public virtual bool Evaluate(CompareOperator compareOperator, bool booleanValue)
		{
			bool result = false;
			bool value = this.Value;
			if (compareOperator != CompareOperator.Equals)
			{
				if (compareOperator != CompareOperator.NotEquals)
				{
					Debug.LogError("The " + compareOperator.ToString() + " comparison operator is not valid.");
				}
				else
				{
					result = (value != booleanValue);
				}
			}
			else
			{
				result = (value == booleanValue);
			}
			return result;
		}

		// Token: 0x06006B26 RID: 27430 RVA: 0x00295BB4 File Offset: 0x00293DB4
		public override void Apply(SetOperator setOperator, bool value)
		{
			if (setOperator == SetOperator.Assign)
			{
				this.Value = value;
				return;
			}
			if (setOperator != SetOperator.Negate)
			{
				Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
				return;
			}
			this.Value = !value;
		}

		// Token: 0x04005A6C RID: 23148
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x04005A6D RID: 23149
		public static readonly SetOperator[] setOperators = new SetOperator[]
		{
			SetOperator.Assign,
			SetOperator.Negate
		};
	}
}
