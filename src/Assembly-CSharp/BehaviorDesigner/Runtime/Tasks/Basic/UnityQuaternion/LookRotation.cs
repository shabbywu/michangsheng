using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x020010BA RID: 4282
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Stores the quaternion of a forward vector.")]
	public class LookRotation : Action
	{
		// Token: 0x060073CC RID: 29644 RVA: 0x002B0B57 File Offset: 0x002AED57
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.LookRotation(this.forwardVector.Value);
			return 2;
		}

		// Token: 0x060073CD RID: 29645 RVA: 0x002B0B75 File Offset: 0x002AED75
		public override void OnReset()
		{
			this.forwardVector = Vector3.zero;
			this.storeResult = Quaternion.identity;
		}

		// Token: 0x04005F79 RID: 24441
		[Tooltip("The forward vector")]
		public SharedVector3 forwardVector;

		// Token: 0x04005F7A RID: 24442
		[Tooltip("The second Vector3")]
		public SharedVector3 secondVector3;

		// Token: 0x04005F7B RID: 24443
		[Tooltip("The stored quaternion")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
