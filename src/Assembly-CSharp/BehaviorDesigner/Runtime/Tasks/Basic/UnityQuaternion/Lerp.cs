using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x020010B9 RID: 4281
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Lerps between two quaternions.")]
	public class Lerp : Action
	{
		// Token: 0x060073C9 RID: 29641 RVA: 0x002B0AE2 File Offset: 0x002AECE2
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Lerp(this.fromQuaternion.Value, this.toQuaternion.Value, this.amount.Value);
			return 2;
		}

		// Token: 0x060073CA RID: 29642 RVA: 0x002B0B18 File Offset: 0x002AED18
		public override void OnReset()
		{
			this.fromQuaternion = (this.toQuaternion = (this.storeResult = Quaternion.identity));
			this.amount = 0f;
		}

		// Token: 0x04005F75 RID: 24437
		[Tooltip("The from rotation")]
		public SharedQuaternion fromQuaternion;

		// Token: 0x04005F76 RID: 24438
		[Tooltip("The to rotation")]
		public SharedQuaternion toQuaternion;

		// Token: 0x04005F77 RID: 24439
		[Tooltip("The amount to lerp")]
		public SharedFloat amount;

		// Token: 0x04005F78 RID: 24440
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
