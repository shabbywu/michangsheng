using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001384 RID: 4996
	[VariableInfo("Other", "Object", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class ObjectVariable : VariableBase<Object>
	{
		// Token: 0x060078FB RID: 30971 RVA: 0x002B8148 File Offset: 0x002B6348
		public virtual bool Evaluate(CompareOperator compareOperator, Object value)
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

		// Token: 0x060078FC RID: 30972 RVA: 0x0005259C File Offset: 0x0005079C
		public override void Apply(SetOperator setOperator, Object value)
		{
			if (setOperator == SetOperator.Assign)
			{
				this.Value = value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x040068ED RID: 26861
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x040068EE RID: 26862
		public static readonly SetOperator[] setOperators = new SetOperator[1];
	}
}
