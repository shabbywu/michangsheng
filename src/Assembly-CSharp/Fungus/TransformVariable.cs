using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EEE RID: 3822
	[VariableInfo("Other", "Transform", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class TransformVariable : VariableBase<Transform>
	{
		// Token: 0x06006B8E RID: 27534 RVA: 0x00296B34 File Offset: 0x00294D34
		public virtual bool Evaluate(CompareOperator compareOperator, Transform value)
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

		// Token: 0x06006B8F RID: 27535 RVA: 0x00296B8C File Offset: 0x00294D8C
		public override void Apply(SetOperator setOperator, Transform value)
		{
			if (setOperator == SetOperator.Assign)
			{
				this.Value = value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x04005A9A RID: 23194
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x04005A9B RID: 23195
		public static readonly SetOperator[] setOperators = new SetOperator[1];
	}
}
