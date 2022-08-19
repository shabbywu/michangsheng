using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000ED5 RID: 3797
	[VariableInfo("Other", "AudioSource", 0)]
	[AddComponentMenu("")]
	[Serializable]
	public class AudioSourceVariable : VariableBase<AudioSource>
	{
		// Token: 0x06006B1C RID: 27420 RVA: 0x00295A30 File Offset: 0x00293C30
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

		// Token: 0x06006B1D RID: 27421 RVA: 0x00295A88 File Offset: 0x00293C88
		public override void Apply(SetOperator setOperator, AudioSource value)
		{
			if (setOperator == SetOperator.Assign)
			{
				this.Value = value;
				return;
			}
			Debug.LogError("The " + setOperator.ToString() + " set operator is not valid.");
		}

		// Token: 0x04005A68 RID: 23144
		public static readonly CompareOperator[] compareOperators = new CompareOperator[]
		{
			CompareOperator.Equals,
			CompareOperator.NotEquals
		};

		// Token: 0x04005A69 RID: 23145
		public static readonly SetOperator[] setOperators = new SetOperator[1];
	}
}
