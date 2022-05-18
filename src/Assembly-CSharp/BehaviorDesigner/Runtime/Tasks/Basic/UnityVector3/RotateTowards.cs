using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x020014C4 RID: 5316
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Rotate the current rotation to the target rotation.")]
	public class RotateTowards : Action
	{
		// Token: 0x06007F54 RID: 32596 RVA: 0x002C9E24 File Offset: 0x002C8024
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.RotateTowards(this.currentRotation.Value, this.targetRotation.Value, this.maxDegreesDelta.Value * 0.017453292f * Time.deltaTime, this.maxMagnitudeDelta.Value);
			return 2;
		}

		// Token: 0x06007F55 RID: 32597 RVA: 0x002C9E7C File Offset: 0x002C807C
		public override void OnReset()
		{
			this.currentRotation = (this.targetRotation = (this.storeResult = Vector3.zero));
			this.maxDegreesDelta = (this.maxMagnitudeDelta = 0f);
		}

		// Token: 0x04006C31 RID: 27697
		[Tooltip("The current rotation in euler angles")]
		public SharedVector3 currentRotation;

		// Token: 0x04006C32 RID: 27698
		[Tooltip("The target rotation in euler angles")]
		public SharedVector3 targetRotation;

		// Token: 0x04006C33 RID: 27699
		[Tooltip("The maximum delta of the degrees")]
		public SharedFloat maxDegreesDelta;

		// Token: 0x04006C34 RID: 27700
		[Tooltip("The maximum delta of the magnitude")]
		public SharedFloat maxMagnitudeDelta;

		// Token: 0x04006C35 RID: 27701
		[Tooltip("The rotation resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
