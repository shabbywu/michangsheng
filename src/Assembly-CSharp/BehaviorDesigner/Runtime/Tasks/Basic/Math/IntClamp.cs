using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020015BF RID: 5567
	[TaskCategory("Basic/Math")]
	[TaskDescription("Clamps the int between two values.")]
	public class IntClamp : Action
	{
		// Token: 0x060082C5 RID: 33477 RVA: 0x00059B4C File Offset: 0x00057D4C
		public override TaskStatus OnUpdate()
		{
			this.intVariable.Value = Mathf.Clamp(this.intVariable.Value, this.minValue.Value, this.maxValue.Value);
			return 2;
		}

		// Token: 0x060082C6 RID: 33478 RVA: 0x002CE094 File Offset: 0x002CC294
		public override void OnReset()
		{
			this.intVariable = (this.minValue = (this.maxValue = 0));
		}

		// Token: 0x04006F8D RID: 28557
		[Tooltip("The int to clamp")]
		public SharedInt intVariable;

		// Token: 0x04006F8E RID: 28558
		[Tooltip("The maximum value of the int")]
		public SharedInt minValue;

		// Token: 0x04006F8F RID: 28559
		[Tooltip("The maximum value of the int")]
		public SharedInt maxValue;
	}
}
