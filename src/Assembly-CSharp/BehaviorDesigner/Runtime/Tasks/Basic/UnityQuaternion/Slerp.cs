using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x020010BC RID: 4284
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Spherically lerp between two quaternions.")]
	public class Slerp : Action
	{
		// Token: 0x060073D2 RID: 29650 RVA: 0x002B0C0B File Offset: 0x002AEE0B
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Slerp(this.fromQuaternion.Value, this.toQuaternion.Value, this.amount.Value);
			return 2;
		}

		// Token: 0x060073D3 RID: 29651 RVA: 0x002B0C40 File Offset: 0x002AEE40
		public override void OnReset()
		{
			this.fromQuaternion = (this.toQuaternion = (this.storeResult = Quaternion.identity));
			this.amount = 0f;
		}

		// Token: 0x04005F80 RID: 24448
		[Tooltip("The from rotation")]
		public SharedQuaternion fromQuaternion;

		// Token: 0x04005F81 RID: 24449
		[Tooltip("The to rotation")]
		public SharedQuaternion toQuaternion;

		// Token: 0x04005F82 RID: 24450
		[Tooltip("The amount to lerp")]
		public SharedFloat amount;

		// Token: 0x04005F83 RID: 24451
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
