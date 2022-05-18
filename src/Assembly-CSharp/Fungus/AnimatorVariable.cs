using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001374 RID: 4980
	[VariableInfo("Other", "Animator", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class AnimatorVariable : VariableBase<Animator>
	{
		// Token: 0x060078B2 RID: 30898 RVA: 0x002B7C3C File Offset: 0x002B5E3C
		public virtual bool Evaluate(CompareOperator compareOperator, Animator value)
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

		// Token: 0x060078B3 RID: 30899 RVA: 0x00051F09 File Offset: 0x00050109
		public override void Apply(SetOperator setOperator, Animator value)
		{
			if (setOperator == SetOperator.Assign)
			{
				this.Value = value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x040068CD RID: 26829
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x040068CE RID: 26830
		public static readonly SetOperator[] setOperators = new SetOperator[1];
	}
}
