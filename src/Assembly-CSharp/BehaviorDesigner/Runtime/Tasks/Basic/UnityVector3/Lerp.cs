using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x02001006 RID: 4102
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Lerp the Vector3 by an amount.")]
	public class Lerp : Action
	{
		// Token: 0x0600714B RID: 29003 RVA: 0x002AB6F3 File Offset: 0x002A98F3
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.Lerp(this.fromVector3.Value, this.toVector3.Value, this.lerpAmount.Value);
			return 2;
		}

		// Token: 0x0600714C RID: 29004 RVA: 0x002AB728 File Offset: 0x002A9928
		public override void OnReset()
		{
			this.fromVector3 = (this.toVector3 = (this.storeResult = Vector3.zero));
			this.lerpAmount = 0f;
		}

		// Token: 0x04005D24 RID: 23844
		[Tooltip("The from value")]
		public SharedVector3 fromVector3;

		// Token: 0x04005D25 RID: 23845
		[Tooltip("The to value")]
		public SharedVector3 toVector3;

		// Token: 0x04005D26 RID: 23846
		[Tooltip("The amount to lerp")]
		public SharedFloat lerpAmount;

		// Token: 0x04005D27 RID: 23847
		[Tooltip("The lerp resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
