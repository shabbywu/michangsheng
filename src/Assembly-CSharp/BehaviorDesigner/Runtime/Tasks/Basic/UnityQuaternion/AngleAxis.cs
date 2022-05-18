using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x0200156D RID: 5485
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Stores the rotation which rotates the specified degrees around the specified axis.")]
	public class AngleAxis : Action
	{
		// Token: 0x060081B1 RID: 33201 RVA: 0x00058B54 File Offset: 0x00056D54
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.AngleAxis(this.degrees.Value, this.axis.Value);
			return 2;
		}

		// Token: 0x060081B2 RID: 33202 RVA: 0x00058B7D File Offset: 0x00056D7D
		public override void OnReset()
		{
			this.degrees = 0f;
			this.axis = Vector3.zero;
			this.storeResult = Quaternion.identity;
		}

		// Token: 0x04006E67 RID: 28263
		[Tooltip("The number of degrees")]
		public SharedFloat degrees;

		// Token: 0x04006E68 RID: 28264
		[Tooltip("The axis direction")]
		public SharedVector3 axis;

		// Token: 0x04006E69 RID: 28265
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}
