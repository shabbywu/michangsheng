using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x020014BF RID: 5311
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Move from the current position to the target position.")]
	public class MoveTowards : Action
	{
		// Token: 0x06007F48 RID: 32584 RVA: 0x00056459 File Offset: 0x00054659
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.MoveTowards(this.currentPosition.Value, this.targetPosition.Value, this.speed.Value * Time.deltaTime);
			return 2;
		}

		// Token: 0x06007F49 RID: 32585 RVA: 0x002C9CAC File Offset: 0x002C7EAC
		public override void OnReset()
		{
			this.currentPosition = (this.targetPosition = (this.storeResult = Vector3.zero));
			this.speed = 0f;
		}

		// Token: 0x04006C20 RID: 27680
		[Tooltip("The current position")]
		public SharedVector3 currentPosition;

		// Token: 0x04006C21 RID: 27681
		[Tooltip("The target position")]
		public SharedVector3 targetPosition;

		// Token: 0x04006C22 RID: 27682
		[Tooltip("The movement speed")]
		public SharedFloat speed;

		// Token: 0x04006C23 RID: 27683
		[Tooltip("The move resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
