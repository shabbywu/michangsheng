using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x020010BB RID: 4283
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Stores the quaternion after a rotation.")]
	public class RotateTowards : Action
	{
		// Token: 0x060073CF RID: 29647 RVA: 0x002B0B97 File Offset: 0x002AED97
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.RotateTowards(this.fromQuaternion.Value, this.toQuaternion.Value, this.maxDeltaDegrees.Value);
			return 2;
		}

		// Token: 0x060073D0 RID: 29648 RVA: 0x002B0BCC File Offset: 0x002AEDCC
		public override void OnReset()
		{
			this.fromQuaternion = (this.toQuaternion = (this.storeResult = Quaternion.identity));
			this.maxDeltaDegrees = 0f;
		}

		// Token: 0x04005F7C RID: 24444
		[Tooltip("The from rotation")]
		public SharedQuaternion fromQuaternion;

		// Token: 0x04005F7D RID: 24445
		[Tooltip("The to rotation")]
		public SharedQuaternion toQuaternion;

		// Token: 0x04005F7E RID: 24446
		[Tooltip("The maximum degrees delta")]
		public SharedFloat maxDeltaDegrees;

		// Token: 0x04005F7F RID: 24447
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
