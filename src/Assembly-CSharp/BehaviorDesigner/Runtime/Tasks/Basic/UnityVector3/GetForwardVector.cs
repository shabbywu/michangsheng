using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x020014B7 RID: 5303
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Stores the forward vector value.")]
	public class GetForwardVector : Action
	{
		// Token: 0x06007F30 RID: 32560 RVA: 0x00056332 File Offset: 0x00054532
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.forward;
			return 2;
		}

		// Token: 0x06007F31 RID: 32561 RVA: 0x00056345 File Offset: 0x00054545
		public override void OnReset()
		{
			this.storeResult = Vector3.zero;
		}

		// Token: 0x04006C0F RID: 27663
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
