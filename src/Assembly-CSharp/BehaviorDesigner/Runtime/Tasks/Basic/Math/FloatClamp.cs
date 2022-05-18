using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020015B9 RID: 5561
	[TaskCategory("Basic/Math")]
	[TaskDescription("Clamps the float between two values.")]
	public class FloatClamp : Action
	{
		// Token: 0x060082B9 RID: 33465 RVA: 0x00059A8A File Offset: 0x00057C8A
		public override TaskStatus OnUpdate()
		{
			this.floatVariable.Value = Mathf.Clamp(this.floatVariable.Value, this.minValue.Value, this.maxValue.Value);
			return 2;
		}

		// Token: 0x060082BA RID: 33466 RVA: 0x002CDE3C File Offset: 0x002CC03C
		public override void OnReset()
		{
			this.floatVariable = (this.minValue = (this.maxValue = 0f));
		}

		// Token: 0x04006F73 RID: 28531
		[Tooltip("The float to clamp")]
		public SharedFloat floatVariable;

		// Token: 0x04006F74 RID: 28532
		[Tooltip("The maximum value of the float")]
		public SharedFloat minValue;

		// Token: 0x04006F75 RID: 28533
		[Tooltip("The maximum value of the float")]
		public SharedFloat maxValue;
	}
}
