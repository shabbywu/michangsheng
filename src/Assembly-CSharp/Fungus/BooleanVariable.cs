using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001378 RID: 4984
	[VariableInfo("", "Boolean", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class BooleanVariable : VariableBase<bool>
	{
		// Token: 0x060078C4 RID: 30916 RVA: 0x002B7CEC File Offset: 0x002B5EEC
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

		// Token: 0x060078C5 RID: 30917 RVA: 0x000520B9 File Offset: 0x000502B9
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

		// Token: 0x040068D5 RID: 26837
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x040068D6 RID: 26838
		public static readonly SetOperator[] setOperators = new SetOperator[]
		{
			SetOperator.Assign,
			SetOperator.Negate
		};
	}
}
