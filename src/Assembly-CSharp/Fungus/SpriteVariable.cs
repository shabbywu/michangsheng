using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EE7 RID: 3815
	[VariableInfo("Other", "Sprite", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class SpriteVariable : VariableBase<Sprite>
	{
		// Token: 0x06006B6E RID: 27502 RVA: 0x00296700 File Offset: 0x00294900
		public virtual bool Evaluate(CompareOperator compareOperator, Sprite value)
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

		// Token: 0x06006B6F RID: 27503 RVA: 0x00296758 File Offset: 0x00294958
		public override void Apply(SetOperator setOperator, Sprite value)
		{
			if (setOperator == SetOperator.Assign)
			{
				this.Value = value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x04005A8C RID: 23180
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x04005A8D RID: 23181
		public static readonly SetOperator[] setOperators = new SetOperator[1];
	}
}
