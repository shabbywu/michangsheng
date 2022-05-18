using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x0200156F RID: 5487
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Stores the quaternion of a euler vector.")]
	public class Euler : Action
	{
		// Token: 0x060081B7 RID: 33207 RVA: 0x00058BD8 File Offset: 0x00056DD8
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Euler(this.eulerVector.Value);
			return 2;
		}

		// Token: 0x060081B8 RID: 33208 RVA: 0x00058BF6 File Offset: 0x00056DF6
		public override void OnReset()
		{
			this.eulerVector = Vector3.zero;
			this.storeResult = Quaternion.identity;
		}

		// Token: 0x04006E6D RID: 28269
		[Tooltip("The euler vector")]
		public SharedVector3 eulerVector;

		// Token: 0x04006E6E RID: 28270
		[Tooltip("The stored quaternion")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
