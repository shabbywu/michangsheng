using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.Math
{
	// Token: 0x020015C6 RID: 5574
	[TaskCategory("Basic/Math")]
	[TaskDescription("Lerp the float by an amount.")]
	public class Lerp : Action
	{
		// Token: 0x060082D4 RID: 33492 RVA: 0x00059C18 File Offset: 0x00057E18
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Mathf.Lerp(this.fromValue.Value, this.toValue.Value, this.lerpAmount.Value);
			return 2;
		}

		// Token: 0x060082D5 RID: 33493 RVA: 0x002CE2E8 File Offset: 0x002CC4E8
		public override void OnReset()
		{
			this.fromValue = (this.toValue = (this.lerpAmount = (this.storeResult = 0f)));
		}

		// Token: 0x04006FA8 RID: 28584
		[Tooltip("The from value")]
		public SharedFloat fromValue;

		// Token: 0x04006FA9 RID: 28585
		[Tooltip("The to value")]
		public SharedFloat toValue;

		// Token: 0x04006FAA RID: 28586
		[Tooltip("The amount to lerp")]
		public SharedFloat lerpAmount;

		// Token: 0x04006FAB RID: 28587
		[Tooltip("The lerp resut")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
