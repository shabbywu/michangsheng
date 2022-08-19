using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000ED3 RID: 3795
	[VariableInfo("Other", "Animator", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class AnimatorVariable : VariableBase<Animator>
	{
		// Token: 0x06006B13 RID: 27411 RVA: 0x00295900 File Offset: 0x00293B00
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

		// Token: 0x06006B14 RID: 27412 RVA: 0x00295958 File Offset: 0x00293B58
		public override void Apply(SetOperator setOperator, Animator value)
		{
			if (setOperator == SetOperator.Assign)
			{
				this.Value = value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x04005A64 RID: 23140
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x04005A65 RID: 23141
		public static readonly SetOperator[] setOperators = new SetOperator[1];
	}
}
