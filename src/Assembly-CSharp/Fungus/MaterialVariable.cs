using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EE1 RID: 3809
	[VariableInfo("Other", "Material", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class MaterialVariable : VariableBase<Material>
	{
		// Token: 0x06006B53 RID: 27475 RVA: 0x00296370 File Offset: 0x00294570
		public virtual bool Evaluate(CompareOperator compareOperator, Material value)
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

		// Token: 0x06006B54 RID: 27476 RVA: 0x002963C8 File Offset: 0x002945C8
		public override void Apply(SetOperator setOperator, Material value)
		{
			if (setOperator == SetOperator.Assign)
			{
				this.Value = value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x04005A80 RID: 23168
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x04005A81 RID: 23169
		public static readonly SetOperator[] setOperators = new SetOperator[1];
	}
}
