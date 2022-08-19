using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EDD RID: 3805
	[VariableInfo("Other", "GameObject", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class GameObjectVariable : VariableBase<GameObject>
	{
		// Token: 0x06006B41 RID: 27457 RVA: 0x0029604C File Offset: 0x0029424C
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

		// Token: 0x06006B42 RID: 27458 RVA: 0x002960A3 File Offset: 0x002942A3
		public override void Apply(SetOperator setOperator, GameObject value)
		{
			if (setOperator == SetOperator.Assign)
			{
				this.Value = value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x04005A78 RID: 23160
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x04005A79 RID: 23161
		public static readonly SetOperator[] setOperators = new SetOperator[1];
	}
}
