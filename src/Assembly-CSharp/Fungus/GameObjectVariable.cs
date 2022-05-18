using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x0200137E RID: 4990
	[VariableInfo("Other", "GameObject", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class GameObjectVariable : VariableBase<GameObject>
	{
		// Token: 0x060078E0 RID: 30944 RVA: 0x002B7F60 File Offset: 0x002B6160
		public virtual bool Evaluate(CompareOperator compareOperator, GameObject gameObjectValue)
		{
			GameObject value = this.Value;
			bool result = false;
			if (compareOperator != CompareOperator.Equals)
			{
				if (compareOperator != CompareOperator.NotEquals)
				{
					Debug.LogError("The " + compareOperator.ToString() + " comparison operator is not valid.");
				}
				else
				{
					result = (value != gameObjectValue);
				}
			}
			else
			{
				result = (value == gameObjectValue);
			}
			return result;
		}

		// Token: 0x060078E1 RID: 30945 RVA: 0x00052330 File Offset: 0x00050530
		public override void Apply(SetOperator setOperator, GameObject value)
		{
			if (setOperator == SetOperator.Assign)
			{
				this.Value = value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x040068E1 RID: 26849
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x040068E2 RID: 26850
		public static readonly SetOperator[] setOperators = new SetOperator[1];
	}
}
