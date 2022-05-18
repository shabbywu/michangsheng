using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020015C7 RID: 5575
	[TaskCategory("Basic/Math")]
	[TaskDescription("Lerp the angle by an amount.")]
	public class LerpAngle : Action
	{
		// Token: 0x060082D7 RID: 33495 RVA: 0x00059C4C File Offset: 0x00057E4C
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Mathf.LerpAngle(this.fromValue.Value, this.toValue.Value, this.lerpAmount.Value);
			return 2;
		}

		// Token: 0x060082D8 RID: 33496 RVA: 0x002CE320 File Offset: 0x002CC520
		public override void OnReset()
		{
			this.fromValue = (this.toValue = (this.lerpAmount = (this.storeResult = 0f)));
		}

		// Token: 0x04006FAC RID: 28588
		[Tooltip("The from value")]
		public SharedFloat fromValue;

		// Token: 0x04006FAD RID: 28589
		[Tooltip("The to value")]
		public SharedFloat toValue;

		// Token: 0x04006FAE RID: 28590
		[Tooltip("The amount to lerp")]
		public SharedFloat lerpAmount;

		// Token: 0x04006FAF RID: 28591
		[Tooltip("The lerp resut")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
