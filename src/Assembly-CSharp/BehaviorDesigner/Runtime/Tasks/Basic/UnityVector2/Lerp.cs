using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector2
{
	// Token: 0x020014D0 RID: 5328
	[TaskCategory("Basic/Vector2")]
	[TaskDescription("Lerp the Vector2 by an amount.")]
	public class Lerp : Action
	{
		// Token: 0x06007F78 RID: 32632 RVA: 0x00056675 File Offset: 0x00054875
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.Lerp(this.fromVector2.Value, this.toVector2.Value, this.lerpAmount.Value);
			return 2;
		}

		// Token: 0x06007F79 RID: 32633 RVA: 0x002CA0E8 File Offset: 0x002C82E8
		public override void OnReset()
		{
			this.fromVector2 = (this.toVector2 = (this.storeResult = Vector2.zero));
			this.lerpAmount = 0f;
		}

		// Token: 0x04006C50 RID: 27728
		[Tooltip("The from value")]
		public SharedVector2 fromVector2;

		// Token: 0x04006C51 RID: 27729
		[Tooltip("The to value")]
		public SharedVector2 toVector2;

		// Token: 0x04006C52 RID: 27730
		[Tooltip("The amount to lerp")]
		public SharedFloat lerpAmount;

		// Token: 0x04006C53 RID: 27731
		[Tooltip("The lerp resut")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
