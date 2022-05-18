using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001382 RID: 4994
	[VariableInfo("Other", "Material", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class MaterialVariable : VariableBase<Material>
	{
		// Token: 0x060078F2 RID: 30962 RVA: 0x002B80F0 File Offset: 0x002B62F0
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

		// Token: 0x060078F3 RID: 30963 RVA: 0x000524C4 File Offset: 0x000506C4
		public override void Apply(SetOperator setOperator, Material value)
		{
			if (setOperator == SetOperator.Assign)
			{
				this.Value = value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x040068E9 RID: 26857
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x040068EA RID: 26858
		public static readonly SetOperator[] setOperators = new SetOperator[1];
	}
}
