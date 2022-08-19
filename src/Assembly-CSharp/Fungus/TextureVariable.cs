using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EEC RID: 3820
	[VariableInfo("Other", "Texture", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class TextureVariable : VariableBase<Texture>
	{
		// Token: 0x06006B85 RID: 27525 RVA: 0x00296A04 File Offset: 0x00294C04
		public virtual bool Evaluate(CompareOperator compareOperator, Texture value)
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

		// Token: 0x06006B86 RID: 27526 RVA: 0x00296A5C File Offset: 0x00294C5C
		public override void Apply(SetOperator setOperator, Texture value)
		{
			if (setOperator == SetOperator.Assign)
			{
				this.Value = value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x04005A96 RID: 23190
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x04005A97 RID: 23191
		public static readonly SetOperator[] setOperators = new SetOperator[1];
	}
}
