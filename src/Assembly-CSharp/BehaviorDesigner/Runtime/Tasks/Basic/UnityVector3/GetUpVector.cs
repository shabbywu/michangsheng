using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x020014BB RID: 5307
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Stores the up vector value.")]
	public class GetUpVector : Action
	{
		// Token: 0x06007F3C RID: 32572 RVA: 0x000563C0 File Offset: 0x000545C0
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.up;
			return 2;
		}

		// Token: 0x06007F3D RID: 32573 RVA: 0x000563D3 File Offset: 0x000545D3
		public override void OnReset()
		{
			this.storeResult = Vector3.zero;
		}

		// Token: 0x04006C15 RID: 27669
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
