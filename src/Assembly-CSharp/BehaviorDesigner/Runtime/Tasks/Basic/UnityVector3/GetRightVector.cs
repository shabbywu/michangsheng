using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x020014B9 RID: 5305
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Stores the right vector value.")]
	public class GetRightVector : Action
	{
		// Token: 0x06007F36 RID: 32566 RVA: 0x00056379 File Offset: 0x00054579
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.right;
			return 2;
		}

		// Token: 0x06007F37 RID: 32567 RVA: 0x0005638C File Offset: 0x0005458C
		public override void OnReset()
		{
			this.storeResult = Vector3.zero;
		}

		// Token: 0x04006C12 RID: 27666
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
