using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x02001573 RID: 5491
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Lerps between two quaternions.")]
	public class Lerp : Action
	{
		// Token: 0x060081C3 RID: 33219 RVA: 0x00058C84 File Offset: 0x00056E84
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Lerp(this.fromQuaternion.Value, this.toQuaternion.Value, this.amount.Value);
			return 2;
		}

		// Token: 0x060081C4 RID: 33220 RVA: 0x002CC7BC File Offset: 0x002CA9BC
		public override void OnReset()
		{
			this.fromQuaternion = (this.toQuaternion = (this.storeResult = Quaternion.identity));
			this.amount = 0f;
		}

		// Token: 0x04006E75 RID: 28277
		[Tooltip("The from rotation")]
		public SharedQuaternion fromQuaternion;

		// Token: 0x04006E76 RID: 28278
		[Tooltip("The to rotation")]
		public SharedQuaternion toQuaternion;

		// Token: 0x04006E77 RID: 28279
		[Tooltip("The amount to lerp")]
		public SharedFloat amount;

		// Token: 0x04006E78 RID: 28280
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
