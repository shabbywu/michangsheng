using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EF2 RID: 3826
	[VariableInfo("Other", "Vector3", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class Vector3Variable : VariableBase<Vector3>
	{
		// Token: 0x06006BA0 RID: 27552 RVA: 0x00296DE8 File Offset: 0x00294FE8
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

		// Token: 0x06006BA1 RID: 27553 RVA: 0x00296E40 File Offset: 0x00295040
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

		// Token: 0x04005AA2 RID: 23202
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x04005AA3 RID: 23203
		public static readonly SetOperator[] setOperators = new SetOperator[]
		{
			SetOperator.Assign,
			SetOperator.Add,
			SetOperator.Subtract
		};
	}
}
