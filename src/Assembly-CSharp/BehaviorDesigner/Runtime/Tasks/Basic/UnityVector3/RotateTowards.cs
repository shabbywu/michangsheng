using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x0200100B RID: 4107
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Rotate the current rotation to the target rotation.")]
	public class RotateTowards : Action
	{
		// Token: 0x0600715A RID: 29018 RVA: 0x002AB960 File Offset: 0x002A9B60
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.RotateTowards(this.currentRotation.Value, this.targetRotation.Value, this.maxDegreesDelta.Value * 0.017453292f * Time.deltaTime, this.maxMagnitudeDelta.Value);
			return 2;
		}

		// Token: 0x0600715B RID: 29019 RVA: 0x002AB9B8 File Offset: 0x002A9BB8
		public override void OnReset()
		{
			this.currentRotation = (this.targetRotation = (this.storeResult = Vector3.zero));
			this.maxDegreesDelta = (this.maxMagnitudeDelta = 0f);
		}

		// Token: 0x04005D35 RID: 23861
		[Tooltip("The current rotation in euler angles")]
		public SharedVector3 currentRotation;

		// Token: 0x04005D36 RID: 23862
		[Tooltip("The target rotation in euler angles")]
		public SharedVector3 targetRotation;

		// Token: 0x04005D37 RID: 23863
		[Tooltip("The maximum delta of the degrees")]
		public SharedFloat maxDegreesDelta;

		// Token: 0x04005D38 RID: 23864
		[Tooltip("The maximum delta of the magnitude")]
		public SharedFloat maxMagnitudeDelta;

		// Token: 0x04005D39 RID: 23865
		[Tooltip("The rotation resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
