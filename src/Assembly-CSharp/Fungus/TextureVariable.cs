using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200138D RID: 5005
	[VariableInfo("Other", "Texture", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class TextureVariable : VariableBase<Texture>
	{
		// Token: 0x06007924 RID: 31012 RVA: 0x002B82A8 File Offset: 0x002B64A8
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

		// Token: 0x06007925 RID: 31013 RVA: 0x0005299E File Offset: 0x00050B9E
		public override void Apply(SetOperator setOperator, Texture value)
		{
			if (setOperator == SetOperator.Assign)
			{
				this.Value = value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x040068FF RID: 26879
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x04006900 RID: 26880
		public static readonly SetOperator[] setOperators = new SetOperator[1];
	}
}
