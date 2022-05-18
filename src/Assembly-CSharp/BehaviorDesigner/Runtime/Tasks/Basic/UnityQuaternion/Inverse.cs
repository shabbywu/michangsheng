using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x02001572 RID: 5490
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Stores the inverse of the specified quaternion.")]
	public class Inverse : Action
	{
		// Token: 0x060081C0 RID: 33216 RVA: 0x00058C66 File Offset: 0x00056E66
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Inverse(this.targetQuaternion.Value);
			return 2;
		}

		// Token: 0x060081C1 RID: 33217 RVA: 0x002CC794 File Offset: 0x002CA994
		public override void OnReset()
		{
			this.targetQuaternion = (this.storeResult = Quaternion.identity);
		}

		// Token: 0x04006E73 RID: 28275
		[Tooltip("The target quaternion")]
		public SharedQuaternion targetQuaternion;

		// Token: 0x04006E74 RID: 28276
		[Tooltip("The stored quaternion")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
