using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x02001574 RID: 5492
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Stores the quaternion of a forward vector.")]
	public class LookRotation : Action
	{
		// Token: 0x060081C6 RID: 33222 RVA: 0x00058CB8 File Offset: 0x00056EB8
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.LookRotation(this.forwardVector.Value);
			return 2;
		}

		// Token: 0x060081C7 RID: 33223 RVA: 0x00058CD6 File Offset: 0x00056ED6
		public override void OnReset()
		{
			this.forwardVector = Vector3.zero;
			this.storeResult = Quaternion.identity;
		}

		// Token: 0x04006E79 RID: 28281
		[Tooltip("The forward vector")]
		public SharedVector3 forwardVector;

		// Token: 0x04006E7A RID: 28282
		[Tooltip("The second Vector3")]
		public SharedVector3 secondVector3;

		// Token: 0x04006E7B RID: 28283
		[Tooltip("The stored quaternion")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
