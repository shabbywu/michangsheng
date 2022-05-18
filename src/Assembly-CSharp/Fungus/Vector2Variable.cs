using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001391 RID: 5009
	[VariableInfo("Other", "Vector2", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class Vector2Variable : VariableBase<Vector2>
	{
		// Token: 0x06007936 RID: 31030 RVA: 0x002B8358 File Offset: 0x002B6558
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

		// Token: 0x06007937 RID: 31031 RVA: 0x002B83B0 File Offset: 0x002B65B0
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

		// Token: 0x04006907 RID: 26887
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x04006908 RID: 26888
		public static readonly SetOperator[] setOperators = new SetOperator[]
		{
			SetOperator.Assign,
			SetOperator.Add,
			SetOperator.Subtract
		};
	}
}
