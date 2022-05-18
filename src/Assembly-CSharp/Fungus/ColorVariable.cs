using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200137A RID: 4986
	[VariableInfo("Other", "Color", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class ColorVariable : VariableBase<Color>
	{
		// Token: 0x060078CD RID: 30925 RVA: 0x000521A6 File Offset: 0x000503A6
		protected static bool ColorsEqual(Color a, Color b)
		{
			return ColorUtility.ToHtmlStringRGBA(a) == ColorUtility.ToHtmlStringRGBA(b);
		}

		// Token: 0x060078CE RID: 30926 RVA: 0x002B7D40 File Offset: 0x002B5F40
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

		// Token: 0x060078CF RID: 30927 RVA: 0x002B7D9C File Offset: 0x002B5F9C
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

		// Token: 0x040068D9 RID: 26841
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x040068DA RID: 26842
		public static readonly SetOperator[] setOperators = new SetOperator[]
		{
			SetOperator.Assign,
			SetOperator.Add,
			SetOperator.Subtract,
			SetOperator.Multiply
		};
	}
}
