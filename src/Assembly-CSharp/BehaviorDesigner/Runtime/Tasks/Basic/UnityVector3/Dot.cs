using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3
{
	// Token: 0x020014B6 RID: 5302
	[TaskCategory("Basic/Vector3")]
	[TaskDescription("Stores the dot product of two Vector3 values.")]
	public class Dot : Action
	{
		// Token: 0x06007F2D RID: 32557 RVA: 0x00056309 File Offset: 0x00054509
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.Dot(this.leftHandSide.Value, this.rightHandSide.Value);
			return 2;
		}

		// Token: 0x06007F2E RID: 32558 RVA: 0x002C9B3C File Offset: 0x002C7D3C
		public override void OnReset()
		{
			this.leftHandSide = (this.rightHandSide = Vector3.zero);
			this.storeResult = 0f;
		}

		// Token: 0x04006C0C RID: 27660
		[Tooltip("The left hand side of the dot product")]
		public SharedVector3 leftHandSide;

		// Token: 0x04006C0D RID: 27661
		[Tooltip("The right hand side of the dot product")]
		public SharedVector3 rightHandSide;

		// Token: 0x04006C0E RID: 27662
		[Tooltip("The dot product result")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
