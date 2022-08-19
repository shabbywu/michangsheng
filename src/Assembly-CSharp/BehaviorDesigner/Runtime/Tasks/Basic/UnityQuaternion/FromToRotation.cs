using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x020010B6 RID: 4278
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Stores a rotation which rotates from the first direction to the second.")]
	public class FromToRotation : Action
	{
		// Token: 0x060073C0 RID: 29632 RVA: 0x002B0A16 File Offset: 0x002AEC16
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.FromToRotation(this.fromDirection.Value, this.toDirection.Value);
			return 2;
		}

		// Token: 0x060073C1 RID: 29633 RVA: 0x002B0A40 File Offset: 0x002AEC40
		public override void OnReset()
		{
			this.fromDirection = (this.toDirection = Vector3.zero);
			this.storeResult = Quaternion.identity;
		}

		// Token: 0x04005F6F RID: 24431
		[Tooltip("The from rotation")]
		public SharedVector3 fromDirection;

		// Token: 0x04005F70 RID: 24432
		[Tooltip("The to rotation")]
		public SharedVector3 toDirection;

		// Token: 0x04005F71 RID: 24433
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
