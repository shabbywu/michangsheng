using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x020010B3 RID: 4275
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Stores the rotation which rotates the specified degrees around the specified axis.")]
	public class AngleAxis : Action
	{
		// Token: 0x060073B7 RID: 29623 RVA: 0x002B091A File Offset: 0x002AEB1A
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.AngleAxis(this.degrees.Value, this.axis.Value);
			return 2;
		}

		// Token: 0x060073B8 RID: 29624 RVA: 0x002B0943 File Offset: 0x002AEB43
		public override void OnReset()
		{
			this.degrees = 0f;
			this.axis = Vector3.zero;
			this.storeResult = Quaternion.identity;
		}

		// Token: 0x04005F67 RID: 24423
		[Tooltip("The number of degrees")]
		public SharedFloat degrees;

		// Token: 0x04005F68 RID: 24424
		[Tooltip("The axis direction")]
		public SharedVector3 axis;

		// Token: 0x04005F69 RID: 24425
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
