using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EE3 RID: 3811
	[VariableInfo("Other", "Object", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class ObjectVariable : VariableBase<Object>
	{
		// Token: 0x06006B5C RID: 27484 RVA: 0x002964A0 File Offset: 0x002946A0
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

		// Token: 0x06006B5D RID: 27485 RVA: 0x002964F8 File Offset: 0x002946F8
		public override void Apply(SetOperator setOperator, Object value)
		{
			if (setOperator == SetOperator.Assign)
			{
				this.Value = value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x04005A84 RID: 23172
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x04005A85 RID: 23173
		public static readonly SetOperator[] setOperators = new SetOperator[1];
	}
}
