using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion
{
	// Token: 0x0200156E RID: 5486
	[TaskCategory("Basic/Quaternion")]
	[TaskDescription("Stores the dot product between two rotations.")]
	public class Dot : Action
	{
		// Token: 0x060081B4 RID: 33204 RVA: 0x00058BAF File Offset: 0x00056DAF
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Dot(this.leftRotation.Value, this.rightRotation.Value);
			return 2;
		}

		// Token: 0x060081B5 RID: 33205 RVA: 0x002CC724 File Offset: 0x002CA924
		public override void OnReset()
		{
			this.leftRotation = (this.rightRotation = Quaternion.identity);
			this.storeResult = 0f;
		}

		// Token: 0x04006E6A RID: 28266
		[Tooltip("The first rotation")]
		public SharedQuaternion leftRotation;

		// Token: 0x04006E6B RID: 28267
		[Tooltip("The second rotation")]
		public SharedQuaternion rightRotation;

		// Token: 0x04006E6C RID: 28268
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
