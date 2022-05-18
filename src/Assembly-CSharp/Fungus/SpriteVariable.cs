using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001388 RID: 5000
	[VariableInfo("Other", "Sprite", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class SpriteVariable : VariableBase<Sprite>
	{
		// Token: 0x0600790D RID: 30989 RVA: 0x002B81F8 File Offset: 0x002B63F8
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

		// Token: 0x0600790E RID: 30990 RVA: 0x0005274C File Offset: 0x0005094C
		public override void Apply(SetOperator setOperator, Sprite value)
		{
			if (setOperator == SetOperator.Assign)
			{
				this.Value = value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x040068F5 RID: 26869
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x040068F6 RID: 26870
		public static readonly SetOperator[] setOperators = new SetOperator[1];
	}
}
