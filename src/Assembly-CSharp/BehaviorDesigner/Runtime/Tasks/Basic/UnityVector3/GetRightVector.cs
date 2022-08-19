using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x02001001 RID: 4097
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Stores the right vector value.")]
	public class GetRightVector : Action
	{
		// Token: 0x0600713C RID: 28988 RVA: 0x002AB57A File Offset: 0x002A977A
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.right;
			return 2;
		}

		// Token: 0x0600713D RID: 28989 RVA: 0x002AB58D File Offset: 0x002A978D
		public override void OnReset()
		{
			this.storeResult = Vector3.zero;
		}

		// Token: 0x04005D1A RID: 23834
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
