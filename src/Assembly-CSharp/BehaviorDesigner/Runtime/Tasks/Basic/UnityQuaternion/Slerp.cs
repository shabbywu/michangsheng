using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x02001576 RID: 5494
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Spherically lerp between two quaternions.")]
	public class Slerp : Action
	{
		// Token: 0x060081CC RID: 33228 RVA: 0x00058D2C File Offset: 0x00056F2C
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Slerp(this.fromQuaternion.Value, this.toQuaternion.Value, this.amount.Value);
			return 2;
		}

		// Token: 0x060081CD RID: 33229 RVA: 0x002CC83C File Offset: 0x002CAA3C
		public override void OnReset()
		{
			this.fromQuaternion = (this.toQuaternion = (this.storeResult = Quaternion.identity));
			this.amount = 0f;
		}

		// Token: 0x04006E80 RID: 28288
		[Tooltip("The from rotation")]
		public SharedQuaternion fromQuaternion;

		// Token: 0x04006E81 RID: 28289
		[Tooltip("The to rotation")]
		public SharedQuaternion toQuaternion;

		// Token: 0x04006E82 RID: 28290
		[Tooltip("The amount to lerp")]
		public SharedFloat amount;

		// Token: 0x04006E83 RID: 28291
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
