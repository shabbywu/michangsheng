using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EF0 RID: 3824
	[VariableInfo("Other", "Vector2", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class Vector2Variable : VariableBase<Vector2>
	{
		// Token: 0x06006B97 RID: 27543 RVA: 0x00296C64 File Offset: 0x00294E64
		public virtual bool Evaluate(CompareOperator compareOperator, Vector2 value)
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

		// Token: 0x06006B98 RID: 27544 RVA: 0x00296CBC File Offset: 0x00294EBC
		public override void Apply(SetOperator setOperator, Vector2 value)
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

		// Token: 0x04005A9E RID: 23198
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x04005A9F RID: 23199
		public static readonly SetOperator[] setOperators = new SetOperator[]
		{
			SetOperator.Assign,
			SetOperator.Add,
			SetOperator.Subtract
		};
	}
}
