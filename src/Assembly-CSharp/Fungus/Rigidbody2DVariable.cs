using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001386 RID: 4998
	[VariableInfo("Other", "Rigidbody2D", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class Rigidbody2DVariable : VariableBase<Rigidbody2D>
	{
		// Token: 0x06007904 RID: 30980 RVA: 0x002B81A0 File Offset: 0x002B63A0
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

		// Token: 0x06007905 RID: 30981 RVA: 0x00052674 File Offset: 0x00050874
		public override void Apply(SetOperator setOperator, Rigidbody2D value)
		{
			if (setOperator == SetOperator.Assign)
			{
				this.Value = value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x040068F1 RID: 26865
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x040068F2 RID: 26866
		public static readonly SetOperator[] setOperators = new SetOperator[1];
	}
}
