using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EE9 RID: 3817
	[VariableInfo("", "String", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class StringVariable : VariableBase<string>
	{
		// Token: 0x06006B77 RID: 27511 RVA: 0x00296830 File Offset: 0x00294A30
		public virtual bool Evaluate(CompareOperator compareOperator, string stringValue)
		{
			string value = this.Value;
			bool result = false;
			if (compareOperator != CompareOperator.Equals)
			{
				if (compareOperator != CompareOperator.NotEquals)
				{
					Debug.LogError("The " + compareOperator.ToString() + " comparison operator is not valid.");
				}
				else
				{
					result = (value != stringValue);
				}
			}
			else
			{
				result = (value == stringValue);
			}
			return result;
		}

		// Token: 0x06006B78 RID: 27512 RVA: 0x00296887 File Offset: 0x00294A87
		public override void Apply(SetOperator setOperator, string value)
		{
			if (setOperator == SetOperator.Assign)
			{
				this.Value = value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x04005A90 RID: 23184
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x04005A91 RID: 23185
		public static readonly SetOperator[] setOperators = new SetOperator[1];
	}
}
