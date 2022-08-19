using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x02001017 RID: 4119
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Lerp the Vector2 by an amount.")]
	public class Lerp : Action
	{
		// Token: 0x0600717E RID: 29054 RVA: 0x002ABDBE File Offset: 0x002A9FBE
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.Lerp(this.fromVector2.Value, this.toVector2.Value, this.lerpAmount.Value);
			return 2;
		}

		// Token: 0x0600717F RID: 29055 RVA: 0x002ABDF4 File Offset: 0x002A9FF4
		public override void OnReset()
		{
			this.fromVector2 = (this.toVector2 = (this.storeResult = Vector2.zero));
			this.lerpAmount = 0f;
		}

		// Token: 0x04005D54 RID: 23892
		[Tooltip("The from value")]
		public SharedVector2 fromVector2;

		// Token: 0x04005D55 RID: 23893
		[Tooltip("The to value")]
		public SharedVector2 toVector2;

		// Token: 0x04005D56 RID: 23894
		[Tooltip("The amount to lerp")]
		public SharedFloat lerpAmount;

		// Token: 0x04005D57 RID: 23895
		[Tooltip("The lerp resut")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
