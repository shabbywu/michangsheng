using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001376 RID: 4982
	[VariableInfo("Other", "AudioSource", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class AudioSourceVariable : VariableBase<AudioSource>
	{
		// Token: 0x060078BB RID: 30907 RVA: 0x002B7C94 File Offset: 0x002B5E94
		public virtual bool Evaluate(CompareOperator compareOperator, AudioSource value)
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

		// Token: 0x060078BC RID: 30908 RVA: 0x00051FE1 File Offset: 0x000501E1
		public override void Apply(SetOperator setOperator, AudioSource value)
		{
			if (setOperator == SetOperator.Assign)
			{
				this.Value = value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x040068D1 RID: 26833
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x040068D2 RID: 26834
		public static readonly SetOperator[] setOperators = new SetOperator[1];
	}
}
