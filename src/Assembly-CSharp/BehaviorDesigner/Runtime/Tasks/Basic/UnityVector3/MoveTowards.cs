using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x02001007 RID: 4103
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Move from the current position to the target position.")]
	public class MoveTowards : Action
	{
		// Token: 0x0600714E RID: 29006 RVA: 0x002AB767 File Offset: 0x002A9967
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.MoveTowards(this.currentPosition.Value, this.targetPosition.Value, this.speed.Value * Time.deltaTime);
			return 2;
		}

		// Token: 0x0600714F RID: 29007 RVA: 0x002AB7A4 File Offset: 0x002A99A4
		public override void OnReset()
		{
			this.currentPosition = (this.targetPosition = (this.storeResult = Vector3.zero));
			this.speed = 0f;
		}

		// Token: 0x04005D28 RID: 23848
		[Tooltip("The current position")]
		public SharedVector3 currentPosition;

		// Token: 0x04005D29 RID: 23849
		[Tooltip("The target position")]
		public SharedVector3 targetPosition;

		// Token: 0x04005D2A RID: 23850
		[Tooltip("The movement speed")]
		public SharedFloat speed;

		// Token: 0x04005D2B RID: 23851
		[Tooltip("The move resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
