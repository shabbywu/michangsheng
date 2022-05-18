using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001393 RID: 5011
	[VariableInfo("Other", "Vector3", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class Vector3Variable : VariableBase<Vector3>
	{
		// Token: 0x0600793F RID: 31039 RVA: 0x002B8424 File Offset: 0x002B6624
		public virtual bool Evaluate(CompareOperator compareOperator, Vector3 value)
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

		// Token: 0x06007940 RID: 31040 RVA: 0x002B847C File Offset: 0x002B667C
		public override void Apply(SetOperator setOperator, Vector3 value)
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
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x0400690B RID: 26891
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x0400690C RID: 26892
		public static readonly SetOperator[] setOperators = new SetOperator[]
		{
			SetOperator.Assign,
			SetOperator.Add,
			SetOperator.Subtract
		};
	}
}
