using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x020014BE RID: 5310
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Lerp the Vector3 by an amount.")]
	public class Lerp : Action
	{
		// Token: 0x06007F45 RID: 32581 RVA: 0x00056425 File Offset: 0x00054625
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.Lerp(this.fromVector3.Value, this.toVector3.Value, this.lerpAmount.Value);
			return 2;
		}

		// Token: 0x06007F46 RID: 32582 RVA: 0x002C9C6C File Offset: 0x002C7E6C
		public override void OnReset()
		{
			this.fromVector3 = (this.toVector3 = (this.storeResult = Vector3.zero));
			this.lerpAmount = 0f;
		}

		// Token: 0x04006C1C RID: 27676
		[Tooltip("The from value")]
		public SharedVector3 fromVector3;

		// Token: 0x04006C1D RID: 27677
		[Tooltip("The to value")]
		public SharedVector3 toVector3;

		// Token: 0x04006C1E RID: 27678
		[Tooltip("The amount to lerp")]
		public SharedFloat lerpAmount;

		// Token: 0x04006C1F RID: 27679
		[Tooltip("The lerp resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
