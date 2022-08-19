using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x020010B8 RID: 4280
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Stores the inverse of the specified quaternion.")]
	public class Inverse : Action
	{
		// Token: 0x060073C6 RID: 29638 RVA: 0x002B0A9B File Offset: 0x002AEC9B
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Inverse(this.targetQuaternion.Value);
			return 2;
		}

		// Token: 0x060073C7 RID: 29639 RVA: 0x002B0ABC File Offset: 0x002AECBC
		public override void OnReset()
		{
			this.targetQuaternion = (this.storeResult = Quaternion.identity);
		}

		// Token: 0x04005F73 RID: 24435
		[Tooltip("The target quaternion")]
		public SharedQuaternion targetQuaternion;

		// Token: 0x04005F74 RID: 24436
		[Tooltip("The stored quaternion")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
