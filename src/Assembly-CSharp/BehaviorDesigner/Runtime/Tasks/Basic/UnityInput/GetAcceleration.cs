using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityInput
{
	// Token: 0x020015E2 RID: 5602
	[TaskCategory("Basic/Input")]
	[TaskDescription("Stores the acceleration value.")]
	public class GetAcceleration : Action
	{
		// Token: 0x0600833A RID: 33594 RVA: 0x0005A33E File Offset: 0x0005853E
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Input.acceleration;
			return 2;
		}

		// Token: 0x0600833B RID: 33595 RVA: 0x0005A351 File Offset: 0x00058551
		public override void OnReset()
		{
			this.storeResult = Vector3.zero;
		}

		// Token: 0x0400700B RID: 28683
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedVector3 storeResult;
	}
}
