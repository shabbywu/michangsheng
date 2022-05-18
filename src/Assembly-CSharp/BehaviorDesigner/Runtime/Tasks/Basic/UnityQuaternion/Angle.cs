using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x0200156C RID: 5484
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Stores the angle in degrees between two rotations.")]
	public class Angle : Action
	{
		// Token: 0x060081AE RID: 33198 RVA: 0x00058B2B File Offset: 0x00056D2B
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Angle(this.firstRotation.Value, this.secondRotation.Value);
			return 2;
		}

		// Token: 0x060081AF RID: 33199 RVA: 0x002CC6EC File Offset: 0x002CA8EC
		public override void OnReset()
		{
			this.firstRotation = (this.secondRotation = Quaternion.identity);
			this.storeResult = 0f;
		}

		// Token: 0x04006E64 RID: 28260
		[Tooltip("The first rotation")]
		public SharedQuaternion firstRotation;

		// Token: 0x04006E65 RID: 28261
		[Tooltip("The second rotation")]
		public SharedQuaternion secondRotation;

		// Token: 0x04006E66 RID: 28262
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
