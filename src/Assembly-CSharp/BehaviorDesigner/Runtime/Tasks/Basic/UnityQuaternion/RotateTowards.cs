using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x02001575 RID: 5493
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Stores the quaternion after a rotation.")]
	public class RotateTowards : Action
	{
		// Token: 0x060081C9 RID: 33225 RVA: 0x00058CF8 File Offset: 0x00056EF8
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.RotateTowards(this.fromQuaternion.Value, this.toQuaternion.Value, this.maxDeltaDegrees.Value);
			return 2;
		}

		// Token: 0x060081CA RID: 33226 RVA: 0x002CC7FC File Offset: 0x002CA9FC
		public override void OnReset()
		{
			this.fromQuaternion = (this.toQuaternion = (this.storeResult = Quaternion.identity));
			this.maxDeltaDegrees = 0f;
		}

		// Token: 0x04006E7C RID: 28284
		[Tooltip("The from rotation")]
		public SharedQuaternion fromQuaternion;

		// Token: 0x04006E7D RID: 28285
		[Tooltip("The to rotation")]
		public SharedQuaternion toQuaternion;

		// Token: 0x04006E7E RID: 28286
		[Tooltip("The maximum degrees delta")]
		public SharedFloat maxDeltaDegrees;

		// Token: 0x04006E7F RID: 28287
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
