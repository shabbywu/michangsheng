using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x020010B5 RID: 4277
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Stores the quaternion of a euler vector.")]
	public class Euler : Action
	{
		// Token: 0x060073BD RID: 29629 RVA: 0x002B09D6 File Offset: 0x002AEBD6
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Euler(this.eulerVector.Value);
			return 2;
		}

		// Token: 0x060073BE RID: 29630 RVA: 0x002B09F4 File Offset: 0x002AEBF4
		public override void OnReset()
		{
			this.eulerVector = Vector3.zero;
			this.storeResult = Quaternion.identity;
		}

		// Token: 0x04005F6D RID: 24429
		[Tooltip("The euler vector")]
		public SharedVector3 eulerVector;

		// Token: 0x04005F6E RID: 24430
		[Tooltip("The stored quaternion")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
