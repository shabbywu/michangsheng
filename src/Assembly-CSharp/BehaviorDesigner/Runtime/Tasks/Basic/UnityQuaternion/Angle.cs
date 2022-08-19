using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x020010B2 RID: 4274
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Stores the angle in degrees between two rotations.")]
	public class Angle : Action
	{
		// Token: 0x060073B4 RID: 29620 RVA: 0x002B08BB File Offset: 0x002AEABB
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Angle(this.firstRotation.Value, this.secondRotation.Value);
			return 2;
		}

		// Token: 0x060073B5 RID: 29621 RVA: 0x002B08E4 File Offset: 0x002AEAE4
		public override void OnReset()
		{
			this.firstRotation = (this.secondRotation = Quaternion.identity);
			this.storeResult = 0f;
		}

		// Token: 0x04005F64 RID: 24420
		[Tooltip("The first rotation")]
		public SharedQuaternion firstRotation;

		// Token: 0x04005F65 RID: 24421
		[Tooltip("The second rotation")]
		public SharedQuaternion secondRotation;

		// Token: 0x04005F66 RID: 24422
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
