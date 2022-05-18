using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x02001570 RID: 5488
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Stores a rotation which rotates from the first direction to the second.")]
	public class FromToRotation : Action
	{
		// Token: 0x060081BA RID: 33210 RVA: 0x00058C18 File Offset: 0x00056E18
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.FromToRotation(this.fromDirection.Value, this.toDirection.Value);
			return 2;
		}

		// Token: 0x060081BB RID: 33211 RVA: 0x002CC75C File Offset: 0x002CA95C
		public override void OnReset()
		{
			this.fromDirection = (this.toDirection = Vector3.zero);
			this.storeResult = Quaternion.identity;
		}

		// Token: 0x04006E6F RID: 28271
		[Tooltip("The from rotation")]
		public SharedVector3 fromDirection;

		// Token: 0x04006E70 RID: 28272
		[Tooltip("The to rotation")]
		public SharedVector3 toDirection;

		// Token: 0x04006E71 RID: 28273
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
