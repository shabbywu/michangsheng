using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200138F RID: 5007
	[VariableInfo("Other", "Transform", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class TransformVariable : VariableBase<Transform>
	{
		// Token: 0x0600792D RID: 31021 RVA: 0x002B8300 File Offset: 0x002B6500
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

		// Token: 0x0600792E RID: 31022 RVA: 0x00052A76 File Offset: 0x00050C76
		public override void Apply(SetOperator setOperator, Transform value)
		{
			if (setOperator == SetOperator.Assign)
			{
				this.Value = value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x04006903 RID: 26883
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x04006904 RID: 26884
		public static readonly SetOperator[] setOperators = new SetOperator[1];
	}
}
