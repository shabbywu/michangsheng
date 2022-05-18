using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200138A RID: 5002
	[VariableInfo("", "String", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class StringVariable : VariableBase<string>
	{
		// Token: 0x06007916 RID: 30998 RVA: 0x002B8250 File Offset: 0x002B6450
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

		// Token: 0x06007917 RID: 30999 RVA: 0x00052824 File Offset: 0x00050A24
		public override void Apply(SetOperator setOperator, string value)
		{
			if (setOperator == SetOperator.Assign)
			{
				this.Value = value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x040068F9 RID: 26873
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x040068FA RID: 26874
		public static readonly SetOperator[] setOperators = new SetOperator[1];
	}
}
