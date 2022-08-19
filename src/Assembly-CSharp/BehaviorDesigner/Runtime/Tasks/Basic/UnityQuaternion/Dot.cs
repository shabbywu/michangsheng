using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x020010B4 RID: 4276
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Stores the dot product between two rotations.")]
	public class Dot : Action
	{
		// Token: 0x060073BA RID: 29626 RVA: 0x002B0975 File Offset: 0x002AEB75
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Dot(this.leftRotation.Value, this.rightRotation.Value);
			return 2;
		}

		// Token: 0x060073BB RID: 29627 RVA: 0x002B09A0 File Offset: 0x002AEBA0
		public override void OnReset()
		{
			this.leftRotation = (this.rightRotation = Quaternion.identity);
			this.storeResult = 0f;
		}

		// Token: 0x04005F6A RID: 24426
		[Tooltip("The first rotation")]
		public SharedQuaternion leftRotation;

		// Token: 0x04005F6B RID: 24427
		[Tooltip("The second rotation")]
		public SharedQuaternion rightRotation;

		// Token: 0x04005F6C RID: 24428
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
