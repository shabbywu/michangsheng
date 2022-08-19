using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EE5 RID: 3813
	[VariableInfo("Other", "Rigidbody2D", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class Rigidbody2DVariable : VariableBase<Rigidbody2D>
	{
		// Token: 0x06006B65 RID: 27493 RVA: 0x002965D0 File Offset: 0x002947D0
		public virtual bool Evaluate(CompareOperator compareOperator, Rigidbody2D value)
		{
			bool result = false;
			if (compareOperator != CompareOperator.Equals)
			{
				if (compareOperator != CompareOperator.NotEquals)
				{
					Debug.LogError("The " + compareOperator.ToString() + " comparison operator is not valid.");
				}
				else
				{
					result = (this.Value != value);
				}
			}
			else
			{
				result = (this.Value == value);
			}
			return result;
		}

		// Token: 0x06006B66 RID: 27494 RVA: 0x00296628 File Offset: 0x00294828
		public override void Apply(SetOperator setOperator, Rigidbody2D value)
		{
			if (setOperator == SetOperator.Assign)
			{
				this.Value = value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x04005A88 RID: 23176
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x04005A89 RID: 23177
		public static readonly SetOperator[] setOperators = new SetOperator[1];
	}
}
