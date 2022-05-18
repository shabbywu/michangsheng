using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x020014B8 RID: 5304
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Stores the magnitude of the Vector3.")]
	public class GetMagnitude : Action
	{
		// Token: 0x06007F33 RID: 32563 RVA: 0x002C9B74 File Offset: 0x002C7D74
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector3Variable.Value.magnitude;
			return 2;
		}

		// Token: 0x06007F34 RID: 32564 RVA: 0x00056357 File Offset: 0x00054557
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.storeResult = 0f;
		}

		// Token: 0x04006C10 RID: 27664
		[Tooltip("The Vector3 to get the magnitude of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04006C11 RID: 27665
		[Tooltip("The magnitude of the vector")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
