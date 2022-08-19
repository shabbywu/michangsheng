using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000ED9 RID: 3801
	[VariableInfo("Other", "Color", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class ColorVariable : VariableBase<Color>
	{
		// Token: 0x06006B2E RID: 27438 RVA: 0x00295CA1 File Offset: 0x00293EA1
		protected static bool ColorsEqual(Color a, Color b)
		{
			return ColorUtility.ToHtmlStringRGBA(a) == ColorUtility.ToHtmlStringRGBA(b);
		}

		// Token: 0x06006B2F RID: 27439 RVA: 0x00295CB4 File Offset: 0x00293EB4
		public virtual bool Evaluate(CompareOperator compareOperator, Color value)
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
					result = !ColorVariable.ColorsEqual(this.Value, value);
				}
			}
			else
			{
				result = ColorVariable.ColorsEqual(this.Value, value);
			}
			return result;
		}

		// Token: 0x06006B30 RID: 27440 RVA: 0x00295D10 File Offset: 0x00293F10
		public override void Apply(SetOperator setOperator, Color value)
		{
			switch (setOperator)
			{
			case SetOperator.Assign:
				this.Value = value;
				return;
			case SetOperator.Add:
				this.Value += value;
				return;
			case SetOperator.Subtract:
				this.Value -= value;
				return;
			case SetOperator.Multiply:
				this.Value *= value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x04005A70 RID: 23152
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x04005A71 RID: 23153
		public static readonly SetOperator[] setOperators = new SetOperator[]
		{
			SetOperator.Assign,
			SetOperator.Add,
			SetOperator.Subtract,
			SetOperator.Multiply
		};
	}
}
